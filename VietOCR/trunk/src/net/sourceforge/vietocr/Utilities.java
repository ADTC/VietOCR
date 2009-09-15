/**
 * Copyright @ 2009 Quan Nguyen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package net.sourceforge.vietocr;

import java.io.*;
import java.net.*;
import java.util.*;
import net.sf.ghost4j.*;

public class Utilities {

    /**
     * 
     * @return the directory of the running jar
     */
    public static File getBaseDir(Object aType) {
        URL dir = aType.getClass().getResource("/" + aType.getClass().getName().replaceAll("\\.", "/") + ".class");
        File dbDir = new File(System.getProperty("user.dir"));

        try {
            if (dir.toString().startsWith("jar:")) {
                dir = new URL(dir.toString().replaceFirst("^jar:", "").replaceFirst("/[^/]+.jar!.*$", ""));
                dbDir = new File(dir.toURI());
            }
        } catch (MalformedURLException mue) {
            mue.printStackTrace();
        } catch (URISyntaxException use) {
            use.printStackTrace();
        }
        return dbDir;
    }

    /**
     * Convert PDF to TIFF format.
     *
     * @param inputPdfFile
     * @return a multi-page TIFF image
     */
    public static File convertPdf2Tiff(File inputPdfFile) throws Exception {
        File[] pngFiles = null;

        try {
            pngFiles = convertPdf2Png(inputPdfFile);
            File tiffFile = File.createTempFile("multipage", ".tif");

            // put PNG images into a single multi-page TIFF image for return
            ImageIOHelper.mergeTiff(pngFiles, tiffFile);
            return tiffFile;
        } catch (UnsatisfiedLinkError ule) {
            throw new RuntimeException(ule.getMessage() + "\nPlease download, install GPL Ghostscript from http://sourceforge.net/projects/ghostscript/files\nand/or set the appropriate environment variable.");
        } catch (NoClassDefFoundError ncdfe) {
            throw new RuntimeException(ncdfe.getMessage() + "\nPlease download, install GPL Ghostscript from http://sourceforge.net/projects/ghostscript/files\nand/or set the appropriate environment variable.");
        } finally {
            if (pngFiles != null) {
                // delete temporary PNG images
                for (File tempFile : pngFiles) {
                    tempFile.delete();
                }
            }
        }
    }

    /**
     * Convert PDF to PNG format.
     *
     * @param inputPdfFile
     * @return an array of PNG images
     */
    public static File[] convertPdf2Png(File inputPdfFile) {
        File imageDir = inputPdfFile.getParentFile();

        //get Ghostscript instance
        Ghostscript gs = Ghostscript.getInstance();

        //prepare Ghostscript interpreter parameters
        //refer to Ghostscript documentation for parameter usage
        String[] gsArgs = new String[10];
        gsArgs[0] = "-gs";
        gsArgs[1] = "-dNOPAUSE";
        gsArgs[2] = "-dBATCH";
        gsArgs[3] = "-dSAFER";
        gsArgs[4] = "-sDEVICE=pnggray";
        gsArgs[5] = "-r300";
        gsArgs[6] = "-dGraphicsAlphaBits=4";
        gsArgs[7] = "-dTextAlphaBits=4";
        gsArgs[8] = "-sOutputFile=" + imageDir.getPath() + "/workingimage%03d.png";
        gsArgs[9] = inputPdfFile.getPath();

        //execute and exit interpreter
        try {
            gs.initialize(gsArgs);
            gs.exit();
        } catch (GhostscriptException e) {
            System.err.println("ERROR: " + e.getMessage());
        }

        // find working files
        File[] workingFiles = imageDir.listFiles(new FilenameFilter() {

            @Override
            public boolean accept(File dir, String name) {
                return name.toLowerCase().matches("workingimage\\d{3}\\.png$");
            }
        });

        return workingFiles;
    }

    /**
     * Split PDF.
     *
     * @param inputPdfFile
     * @return an array of PNG images
     */
    public static void splitPdf(String inputPdfFile, String outputPdfFile, String firstPage, String lastPage) {
        //get Ghostscript instance
        Ghostscript gs = Ghostscript.getInstance();

        //prepare Ghostscript interpreter parameters
        //refer to Ghostscript documentation for parameter usage
        //gs -sDEVICE=pdfwrite -dNOPAUSE -dQUIET -dBATCH -dFirstPage=m -dLastPage=n -sOutputFile=out.pdf in.pdf
        List<String> gsArgs = new ArrayList<String>();
        gsArgs.add("-gs");
        gsArgs.add("-dNOPAUSE");
        gsArgs.add("-dQUIET");
        gsArgs.add("-dBATCH");
        gsArgs.add("-sDEVICE=pdfwrite");

        if (!firstPage.trim().isEmpty()) {
            gsArgs.add("-dFirstPage=" + firstPage);
        }

        if (!lastPage.trim().isEmpty()) {
            gsArgs.add("-dLastPage=" + lastPage);
        }

        gsArgs.add("-sOutputFile=" + outputPdfFile);
        gsArgs.add(inputPdfFile);

        //execute and exit interpreter
        try {
            gs.initialize(gsArgs.toArray(new String[0]));
            gs.exit();
        } catch (GhostscriptException e) {
            System.err.println("ERROR: " + e.getMessage());
            throw new RuntimeException(e.getMessage());
        }
    }
}
