package net.sourceforge.vietocr.utilities;

import com.ice.tar.TarEntry;
import com.ice.tar.TarInputStream;
import java.io.*;
import java.util.zip.*;

public class FileExtractor {

    final static int BUFFER = 1024;

    public static void extractCompressedFile(String compressedArchiveName, String destFolder) {
        if (compressedArchiveName.toLowerCase().endsWith(".zip")) {
            extractZipFile(compressedArchiveName, destFolder);
        } else if (compressedArchiveName.toLowerCase().endsWith(".tar.gz")) {
            extractTGZ(compressedArchiveName, destFolder);
        } else if (compressedArchiveName.toLowerCase().endsWith(".gz")) {
            extractGZip(compressedArchiveName, destFolder);
        }
    }

    public static void extractZipFile(String filename, String destFolder) {
        try {
            ZipInputStream zipinputstream = new ZipInputStream(new FileInputStream(filename));
            ZipEntry zipEntry;

            while ((zipEntry = zipinputstream.getNextEntry()) != null) {
                //for each entry to be extracted
                if (zipEntry.isDirectory()) {
                    continue;
                }
                FileOutputStream fos = new FileOutputStream(new File(destFolder, zipEntry.getName()));
                byte[] buf = new byte[BUFFER];
                int bytesRead;
                while ((bytesRead = zipinputstream.read(buf, 0, BUFFER)) > -1) {
                    fos.write(buf, 0, bytesRead);
                }
                fos.close();
                zipinputstream.closeEntry();
            }
            zipinputstream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void extractGZip(String filename, String destFolder) {
        try {
            File file = new File(filename);
            GZIPInputStream gzipinputstream = new GZIPInputStream(new FileInputStream(file));
            FileOutputStream fos = new FileOutputStream(new File(destFolder, file.getName().substring(0, file.getName().length() - ".gz".length())));
            byte[] buf = new byte[BUFFER];
            int bytesRead;
            while ((bytesRead = gzipinputstream.read(buf, 0, BUFFER)) > -1) {
                fos.write(buf, 0, bytesRead);
            }
            fos.close();
            gzipinputstream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void extractTarFile(String filename, String destFolder) {
        try {
            TarInputStream tarinputstream = new TarInputStream(new FileInputStream(filename));
            TarEntry tarEntry;

            while ((tarEntry = tarinputstream.getNextEntry()) != null) {
                //for each entry to be extracted
                if (tarEntry.isDirectory()) {
                    continue;
                }
                FileOutputStream fos = new FileOutputStream(new File(destFolder, tarEntry.getName()));
                byte[] buf = new byte[BUFFER];
                int bytesRead;
                while ((bytesRead = tarinputstream.read(buf, 0, BUFFER)) > -1) {
                    fos.write(buf, 0, bytesRead);
                }
                fos.close();
            }
            tarinputstream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void extractTGZ(String filename, String destFolder) {
        extractGZip(filename, ".");
        extractTarFile(filename.substring(0, filename.length() - ".gz".length()), destFolder);
    }
}
