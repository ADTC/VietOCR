/**
 * Copyright @ 2008 Quan Nguyen
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
import java.util.*;
import javax.imageio.*;
import javax.imageio.stream.*;
import javax.imageio.metadata.*;
import com.sun.media.imageio.plugins.tiff.*;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.datatransfer.*;
import java.awt.image.*;
import java.nio.ByteBuffer;

public class ImageIOHelper {

    final static String OUTPUT_FILE_NAME = "Tesstmp";
    final static String TIFF_EXT = ".tif";
    final static String TIFF_FORMAT = "tiff";

    /**
     * Creates a list of TIFF image files from an image file. 
     * It basically converts images of other formats to TIFF format,
     * or a multi-page TIFF image to multiple TIFF image files.
     * 
     * @param imageFile input image file
     * @param index an index of the page; -1 means all pages, as in a multi-page TIFF image
     * @return a list of TIFF image files
     * @throws Exception
     */
    public static List<File> createTiffFiles(File imageFile, int index) throws IOException {
        List<File> tiffFiles = new ArrayList<File>();

        String imageFileName = imageFile.getName();
        String imageFormat = imageFileName.substring(imageFileName.lastIndexOf('.') + 1);

        Iterator<ImageReader> readers = ImageIO.getImageReadersByFormatName(imageFormat);
        ImageReader reader = readers.next();

        if (reader == null) {
            throw new RuntimeException("Need to install JAI Image I/O package.\nhttps://jai-imageio.dev.java.net");
        }

        ImageInputStream iis = ImageIO.createImageInputStream(imageFile);
        reader.setInput(iis);
        //Read the stream metadata
//        IIOMetadata streamMetadata = reader.getStreamMetadata();

        //Set up the writeParam
        TIFFImageWriteParam tiffWriteParam = new TIFFImageWriteParam(Locale.US);
        tiffWriteParam.setCompressionMode(ImageWriteParam.MODE_DISABLED);

        //Get tif writer and set output to file
        Iterator<ImageWriter> writers = ImageIO.getImageWritersByFormatName(TIFF_FORMAT);
        ImageWriter writer = writers.next();

        //Read the stream metadata
        IIOMetadata streamMetadata = writer.getDefaultStreamMetadata(tiffWriteParam);

        int imageTotal = reader.getNumImages(true);

        for (int i = 0; i < imageTotal; i++) {
            // all if index == -1; otherwise, only index-th
            if (index == -1 || i == index) {
//                BufferedImage bi = reader.read(i);
//                IIOImage oimage = new IIOImage(bi, null, reader.getImageMetadata(i));
                IIOImage oimage = reader.readAll(i, reader.getDefaultReadParam());
                File tiffFile = File.createTempFile(OUTPUT_FILE_NAME, TIFF_EXT);
                ImageOutputStream ios = ImageIO.createImageOutputStream(tiffFile);
                writer.setOutput(ios);
                writer.write(streamMetadata, oimage, tiffWriteParam);
                ios.close();
                tiffFiles.add(tiffFile);
            }
        }
        writer.dispose();
        reader.dispose();

        return tiffFiles;
    }

    /**
     * Creates a list of TIFF image files from a list of <code>IIOImage</code> objects.
     * 
     * @param imageList a list of <code>IIOImage</code> objects
     * @param index an index of the page; -1 means all pages
     * @return a list of TIFF image files
     * @throws Exception
     */
    public static List<File> createTiffFiles(List<IIOImage> imageList, int index) throws IOException {
        return createTiffFiles(imageList, index, 0, 0);
    }

    public static List<File> createTiffFiles(List<IIOImage> imageList, int index, int dpiX, int dpiY) throws IOException {
        List<File> tiffFiles = new ArrayList<File>();

        //Set up the writeParam
        TIFFImageWriteParam tiffWriteParam = new TIFFImageWriteParam(Locale.US);
        tiffWriteParam.setCompressionMode(ImageWriteParam.MODE_DISABLED);

        //Get tif writer and set output to file
        Iterator<ImageWriter> writers = ImageIO.getImageWritersByFormatName(TIFF_FORMAT);
        ImageWriter writer = writers.next();

        if (writer == null) {
            throw new RuntimeException("Need to install JAI Image I/O package.\nhttps://jai-imageio.dev.java.net");
        }

        //Get the stream metadata
        IIOMetadata streamMetadata = writer.getDefaultStreamMetadata(tiffWriteParam);

        // all if index == -1; otherwise, only index-th
        for (IIOImage oimage : (index == -1 ? imageList : imageList.subList(index, index + 1))) {
            if (dpiX != 0 && dpiY != 0) {
                // Get the default image metadata.
                ImageTypeSpecifier imageType = ImageTypeSpecifier.createFromRenderedImage(oimage.getRenderedImage());
                IIOMetadata imageMetadata = writer.getDefaultImageMetadata(imageType, null);
                imageMetadata = setDPIViaAPI(imageMetadata, dpiX, dpiY);
                oimage.setMetadata(imageMetadata);
            }

            File tiffFile = File.createTempFile(OUTPUT_FILE_NAME, TIFF_EXT);
            ImageOutputStream ios = ImageIO.createImageOutputStream(tiffFile);
            writer.setOutput(ios);
            writer.write(streamMetadata, oimage, tiffWriteParam);
            ios.close();
            tiffFiles.add(tiffFile);
        }
        writer.dispose();

        return tiffFiles;
    }
    
    /**
     * Set DPI using API.
     */
    private static IIOMetadata setDPIViaAPI(IIOMetadata imageMetadata, int dpiX, int dpiY)
            throws IIOInvalidTreeException {
        // Derive the TIFFDirectory from the metadata.
        TIFFDirectory dir = TIFFDirectory.createFromMetadata(imageMetadata);

        // Get {X,Y}Resolution tags.
        BaselineTIFFTagSet base = BaselineTIFFTagSet.getInstance();
        TIFFTag tagXRes = base.getTag(BaselineTIFFTagSet.TAG_X_RESOLUTION);
        TIFFTag tagYRes = base.getTag(BaselineTIFFTagSet.TAG_Y_RESOLUTION);

        // Create {X,Y}Resolution fields.
        TIFFField fieldXRes = new TIFFField(tagXRes, TIFFTag.TIFF_RATIONAL,
                1, new long[][]{{dpiX, 1}});
        TIFFField fieldYRes = new TIFFField(tagYRes, TIFFTag.TIFF_RATIONAL,
                1, new long[][]{{dpiY, 1}});

        // Append {X,Y}Resolution fields to directory.
        dir.addTIFFField(fieldXRes);
        dir.addTIFFField(fieldYRes);

        // Convert to metadata object and return.
        return dir.getAsMetadata();
    }

    /**
     * Gets pixel data of an <code>IIOImage</code> object.
     * 
     * @param image an <code>IIOImage</code> object
     * @return a byte buffer of pixel data
     * @throws Exception
     */
    public static ByteBuffer getImageByteBuffer(IIOImage image) throws IOException {
        //Set up the writeParam
        TIFFImageWriteParam tiffWriteParam = new TIFFImageWriteParam(Locale.US);
        tiffWriteParam.setCompressionMode(ImageWriteParam.MODE_DISABLED);

        //Get tif writer and set output to file
        Iterator<ImageWriter> writers = ImageIO.getImageWritersByFormatName(TIFF_FORMAT);
        ImageWriter writer = writers.next();

        if (writer == null) {
            throw new RuntimeException("Need to install JAI Image I/O package.\nhttps://jai-imageio.dev.java.net");
        }

        //Get the stream metadata
        IIOMetadata streamMetadata = writer.getDefaultStreamMetadata(tiffWriteParam);
        
        ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        ImageOutputStream ios = ImageIO.createImageOutputStream(outputStream);
        writer.setOutput(ios);
        writer.write(streamMetadata, new IIOImage(image.getRenderedImage(), null, null), tiffWriteParam);
//        writer.write(image.getRenderedImage());
        writer.dispose();
//        ImageIO.write(image.getRenderedImage(), "tiff", ios); // this can be used in lieu of writer
        ios.seek(0);
        BufferedImage bi = ImageIO.read(ios);
        byte[] pixelData= ((DataBufferByte) bi.getRaster().getDataBuffer()).getData();
        return ByteBuffer.wrap(pixelData);
    }

    /**
     * Gets a list of <code>IIOImage</code> objects for an image file.
     *
     * @param imageFile input image file. It can be any of the supported formats, including TIFF, JPEG, GIF, PNG, BMP, JPEG, and PDF if GPL Ghostscript is installed
     * @return a list of <code>IIOImage</code> objects
     * @throws Exception
     */
    public static List<IIOImage> getIIOImageList(File imageFile) throws IOException {
        File workingTiffFile = null;

        ImageReader reader = null;
        ImageInputStream iis = null;

        try {
            // convert PDF to TIFF
            if (imageFile.getName().toLowerCase().endsWith(".pdf")) {
                workingTiffFile = ImageHelper.convertPdf2Tiff(imageFile);
                imageFile = workingTiffFile;
            }

            List<IIOImage> iioImageList = new ArrayList<IIOImage>();

            String imageFileName = imageFile.getName();
            String imageFormat = imageFileName.substring(imageFileName.lastIndexOf('.') + 1);
            if (imageFormat.matches("(pbm|pgm|ppm)")) {
                imageFormat = "pnm";
            } else if (imageFormat.equals("jp2")) {
                imageFormat = "jpeg2000";
            }
            Iterator<ImageReader> readers = ImageIO.getImageReadersByFormatName(imageFormat);
            reader = readers.next();

            if (reader == null) {
                throw new RuntimeException("Need to install JAI Image I/O package.\nhttps://jai-imageio.dev.java.net");
            }

            iis = ImageIO.createImageInputStream(imageFile);
            reader.setInput(iis);

            int imageTotal = reader.getNumImages(true);

            for (int i = 0; i < imageTotal; i++) {
//                IIOImage oimage = new IIOImage(reader.read(i), null, reader.getImageMetadata(i));
                IIOImage oimage = reader.readAll(i, reader.getDefaultReadParam());
                iioImageList.add(oimage);
            }

            return iioImageList;
        } finally {
            try {
                if (iis != null) {
                    iis.close();
                }
                if (reader != null) {
                    reader.dispose();
                }
            } catch (Exception e) {
                // ignore
            }
            if (workingTiffFile != null && workingTiffFile.exists()) {
                workingTiffFile.delete();
            }
        }
    }

    /**
     * Merges multiple images into one TIFF image.
     * 
     * @param inputImages an array of image files
     * @param outputTiff the output TIFF file
     * @throws Exception
     */
    public static void mergeTiff(File[] inputImages, File outputTiff) throws IOException {
        List<IIOImage> imageList = new ArrayList<IIOImage>();

        for (int i = 0; i < inputImages.length; i++) {
            imageList.addAll(getIIOImageList(inputImages[i]));
        }

        if (imageList.isEmpty()) {
            // if no image
            return;
        }

        Iterator<ImageWriter> writers = ImageIO.getImageWritersByFormatName(TIFF_FORMAT);
        ImageWriter writer = writers.next();

        //Set up the writeParam
        TIFFImageWriteParam tiffWriteParam = new TIFFImageWriteParam(Locale.US);
        tiffWriteParam.setCompressionMode(ImageWriteParam.MODE_DISABLED);

        //Get the stream metadata
        IIOMetadata streamMetadata = writer.getDefaultStreamMetadata(tiffWriteParam);

        ImageOutputStream ios = ImageIO.createImageOutputStream(outputTiff);
        writer.setOutput(ios);

        IIOImage firstIioImage = imageList.remove(0);
        writer.write(streamMetadata, firstIioImage, tiffWriteParam);

        int i = 1;
        for (IIOImage iioImage : imageList) {
            writer.writeInsert(i++, iioImage, tiffWriteParam);
        }
        ios.close();

        writer.dispose();
    }

    /**
     * Gets an image from Clipboard.
     * 
     * @return image
     */
    public static Image getClipboardImage() {
        Clipboard clipboard = Toolkit.getDefaultToolkit().getSystemClipboard();
        try {
            return (Image) clipboard.getData(DataFlavor.imageFlavor);
        } catch (Exception e) {
            return null;
        }
    }
}
