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
//            extractTGZ(compressedArchiveName, destFolder);
        } else if (compressedArchiveName.toLowerCase().endsWith(".gz")) {
            extractGZip(compressedArchiveName, destFolder);
        }
    }

    public static void extractZipFile(String filename, String destinationname) {
        try {
            ZipInputStream zipinputstream = new ZipInputStream(new FileInputStream(filename));
            ZipEntry zipentry;

            while ((zipentry = zipinputstream.getNextEntry()) != null) {
                //for each entry to be extracted
                if (zipentry.isDirectory()) {
                    continue;
                }
                FileOutputStream fos = new FileOutputStream(new File(destinationname, zipentry.getName()));
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

    public static void extractGZip(String filename, String destinationname) {
        try {
            File file = new File(filename);
            GZIPInputStream gzipinputstream = new GZIPInputStream(new FileInputStream(file));
            FileOutputStream fos = new FileOutputStream(new File(destinationname, file.getName().substring(0, file.getName().length() - ".gz".length())));
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

       public static void extractTarFile(String filename, String destinationname) {
        try {
            TarInputStream tarinputstream = new TarInputStream(new FileInputStream(filename));
            TarEntry tarentry;

            while ((tarentry = tarinputstream.getNextEntry()) != null) {
                //for each entry to be extracted
                if (tarentry.isDirectory()) {
                    continue;
                }
                FileOutputStream fos = new FileOutputStream(new File(destinationname, tarentry.getName()));
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
}
