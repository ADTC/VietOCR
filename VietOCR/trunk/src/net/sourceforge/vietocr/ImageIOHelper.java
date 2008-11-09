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
import java.awt.*;
import java.awt.image.*;
import javax.swing.*;

/**
 *
 * @author Quan Nguyen (nguyenq@users.sf.net)
 */
public class ImageIOHelper {

    final static String TIFF_EXT = ".tif";

    public static ArrayList<File> createImageFiles(File imageFile, int index, String imageFormat) throws Exception {
        ArrayList<File> tempImageFiles = new ArrayList<File>();
        
        try {
            
            Iterator readers = ImageIO.getImageReadersByFormatName(imageFormat);
            ImageReader reader = (ImageReader) readers.next();
            
            ImageInputStream iis = ImageIO.createImageInputStream(imageFile);
            reader.setInput(iis);
            //Read the stream metadata
            IIOMetadata streamMetadata = reader.getStreamMetadata();
            
            //Set up the writeParam
            TIFFImageWriteParam tiffWriteParam = new TIFFImageWriteParam(Locale.US);
            tiffWriteParam.setCompressionMode(ImageWriteParam.MODE_DISABLED);
            
            //Get tif writer and set output to file
            Iterator writers = ImageIO.getImageWritersByFormatName("tiff");
            ImageWriter writer = (ImageWriter)writers.next();
            
            if (index == -1) {
                int imageTotal = reader.getNumImages(true);
                
                for (int i = 0; i < imageTotal; i++) {
//                BufferedImage bi = (BufferedImage) imageList.get(imageIndex).getImage();
                    BufferedImage bi = reader.read(i);
                    IIOImage image = new IIOImage(bi, null, reader.getImageMetadata(i));
                    File tempFile = File.createTempFile("TessTempFile", TIFF_EXT);
                    ImageOutputStream ios = ImageIO.createImageOutputStream(tempFile);
                    writer.setOutput(ios);
                    writer.write(streamMetadata, image, tiffWriteParam);
                    ios.close();
                    tempImageFiles.add(tempFile);
                }
            } else {
                BufferedImage bi = reader.read(index);
                IIOImage image = new IIOImage(bi, null, reader.getImageMetadata(index));
                File tempFile = File.createTempFile("TessTempFile", TIFF_EXT);
                ImageOutputStream ios = ImageIO.createImageOutputStream(tempFile);
                writer.setOutput(ios);
                writer.write(streamMetadata, image, tiffWriteParam);
                ios.close();
                tempImageFiles.add(tempFile);
            }
            writer.dispose();
            reader.dispose();
        } catch (Exception exc) {
            throw exc;
        }
        return tempImageFiles;
    }

    public static ArrayList<File> createImageFiles(ArrayList<ImageIconScalable> imageList, int index, String imageFormat) throws Exception {
        ArrayList<File> tempImageFiles = new ArrayList<File>();

        try {

            //Set up the writeParam
            TIFFImageWriteParam tiffWriteParam = new TIFFImageWriteParam(Locale.US);
            tiffWriteParam.setCompressionMode(ImageWriteParam.MODE_DISABLED);

            //Get tif writer and set output to file
            Iterator writers = ImageIO.getImageWritersByFormatName("tiff");
            ImageWriter writer = (ImageWriter)writers.next();

            if (index == -1) {
                for (ImageIconScalable imageIcon : imageList) {
                    BufferedImage bi = (BufferedImage) imageIcon.getImage();
                    IIOImage image = new IIOImage(bi, null, null);
                    File tempFile = File.createTempFile("TessTempFile", TIFF_EXT);
                    ImageOutputStream ios = ImageIO.createImageOutputStream(tempFile);
                    writer.setOutput(ios);
                    writer.write(null, image, tiffWriteParam);
                    ios.close();
                    tempImageFiles.add(tempFile);
                }
            } else {
                BufferedImage bi = (BufferedImage) imageList.get(index).getImage();
                IIOImage image = new IIOImage(bi, null, null);
                File tempFile = File.createTempFile("TessTempFile", TIFF_EXT);
                ImageOutputStream ios = ImageIO.createImageOutputStream(tempFile);
                writer.setOutput(ios);
                writer.write(null, image, tiffWriteParam);
                ios.close();
                tempImageFiles.add(tempFile);
            }
            writer.dispose();
        } catch (Exception exc) {
            throw exc;
        }
        return tempImageFiles;
    }
        
//    public static File tempImageFile(File imageFile, int index) {
//        String path = imageFile.getPath();
//        StringBuffer strB = new StringBuffer(path);
//        strB.insert(path.lastIndexOf('.'), index);
//        return new File(strB.toString().replaceFirst("(?<=\\.)(\\w+)$", "tif"));
//    }
        
    public static ArrayList<ImageIconScalable> getImageList(File imageFile) {
        ArrayList<ImageIconScalable> al = new ArrayList<ImageIconScalable>();
        try {
            String imageFileName = imageFile.getName();
            String imageFormat = imageFileName.substring(imageFileName.lastIndexOf('.') + 1);
            Iterator readers = ImageIO.getImageReadersByFormatName(imageFormat);
            ImageReader reader = (ImageReader) readers.next();
            
            if (reader == null) {
                JOptionPane.showConfirmDialog(null, "Need to install JAI Image I/O package.\nhttps://jai-imageio.dev.java.net");
                return null;
            }
            
            ImageInputStream iis = ImageIO.createImageInputStream(imageFile);
            reader.setInput(iis);
            int imageTotal = reader.getNumImages(true);
            
            for (int i = 0; i < imageTotal; i++) {
                al.add(new ImageIconScalable(reader.read(i)));
            }
            
            reader.dispose();
        } catch (IOException ioe) {
            System.err.println(ioe.getMessage());
        } catch (Exception e) {
            System.err.println(e.getMessage());
        }
        
        return al;
    }

    public static BufferedImage rotateImage(BufferedImage image, double angle, Color background) {
        double sin = Math.abs(Math.sin(angle)), cos = Math.abs(Math.cos(angle));
        int w = image.getWidth(), h = image.getHeight();
        int neww = (int)Math.floor(w*cos+h*sin), newh = (int)Math.floor(h*cos+w*sin);
        GraphicsConfiguration gc = getDefaultConfiguration();
        BufferedImage result = gc.createCompatibleImage(neww, newh);
        Graphics2D g = result.createGraphics();
        g.setColor(background);
        g.fillRect(0,0,neww,newh);
        g.translate((neww-w)/2, (newh-h)/2);
        g.rotate(angle, w/2, h/2);
        g.drawRenderedImage(image, null);
        return result;
    }

    public static GraphicsConfiguration getDefaultConfiguration() {
        GraphicsEnvironment ge = GraphicsEnvironment.getLocalGraphicsEnvironment();
        GraphicsDevice gd = ge.getDefaultScreenDevice();
        return gd.getDefaultConfiguration();
    }
}
