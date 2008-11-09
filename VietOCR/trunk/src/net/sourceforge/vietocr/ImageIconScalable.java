/**
 * Core Java Technologies Tech Tips, February 20, 2003: Providing a Scalable Image Icon
 * http://java.sun.com/developer/JDCTechTips/2003/tt0220.html#2
 */
package net.sourceforge.vietocr;

import java.awt.*;
import java.awt.geom.AffineTransform;
import java.awt.image.BufferedImage;
import javax.swing.*;
import java.net.*;

public class ImageIconScalable extends ImageIcon {
    private int width = -1;
    private int height = -1;

    public ImageIconScalable() {
        super();
    }

    public ImageIconScalable(byte imageData[]) {
        super(imageData);
    }

    public ImageIconScalable(byte imageData[], String description) {
        super(imageData, description);
    }

    public ImageIconScalable(Image image) {
        super(image);
    }

    public ImageIconScalable(Image image, String description) {
        super(image, description);
    }

    public ImageIconScalable(String filename) {
        super(filename);
    }

    public ImageIconScalable(String filename, String description) {
        super(filename, description);
    }

    public ImageIconScalable(URL location) {
        super(location);
    }

    public ImageIconScalable(URL location, String description) {
        super(location, description);
    }

    @Override
    public int getIconHeight() {
        int returnValue;
        if (height == -1) {
            returnValue = super.getIconHeight();
        } else {
            returnValue = height;
        }
        return returnValue;
    }

    @Override
    public int getIconWidth() {
        int returnValue;
        if (width == -1) {
            returnValue = super.getIconWidth();
        } else {
            returnValue = width;
        }
        return returnValue;
    }

    @Override
    public void paintIcon(Component c, Graphics g, int x, int y) {
        if ((width == -1) && (height == -1)) {
            g.drawImage(getImage(), x, y, c);
        } else {
            g.drawImage(getImage(), x, y, width, height, c);
        }
    }

    public void setScaledSize(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public BufferedImage getRotatedImage(double theta) {
        double sin = Math.abs(Math.sin(theta));
        double cos = Math.abs(Math.cos(theta));
        int w = this.getIconWidth();
        int h = this.getIconHeight();
        int newW = (int) (w * cos + h * sin);
        int newH = (int) (w * sin + h * cos);
        BufferedImage result = new BufferedImage(newW, newH, BufferedImage.TYPE_INT_RGB);
        Graphics2D g2d = result.createGraphics();
        g2d.setPaint(UIManager.getColor("Panel.background"));
        g2d.fillRect(0, 0, newW, newH);
        AffineTransform at = AffineTransform.getRotateInstance(theta, newW / 2, newH / 2);
        at.translate((newW - w) / 2, (newH - h) / 2);
        g2d.drawRenderedImage((BufferedImage) this.getImage(), at);
        g2d.dispose();
        return result;
    }

    public BufferedImage getRotatedImage2(double angle, Color background) {
        double sin = Math.abs(Math.sin(angle));
        double cos = Math.abs(Math.cos(angle));
        int w = this.getIconWidth();
        int h = this.getIconHeight();
        int newW = (int) Math.floor(w * cos + h * sin);
        int newH = (int) Math.floor(h * cos + w * sin);
        GraphicsConfiguration gc = getDefaultConfiguration();
        BufferedImage result = gc.createCompatibleImage(newW, newH);
        Graphics2D g2d = result.createGraphics();
        g2d.setColor(background);
        g2d.fillRect(0, 0, newW, newH);
        g2d.translate((newW - w) / 2, (newH - h) / 2);
        g2d.rotate(angle, w / 2, h / 2);
        g2d.drawRenderedImage((BufferedImage) this.getImage(), null);
        g2d.dispose();
        return result;
    }

    private GraphicsConfiguration getDefaultConfiguration() {
        GraphicsEnvironment ge = GraphicsEnvironment.getLocalGraphicsEnvironment();
        GraphicsDevice gd = ge.getDefaultScreenDevice();
        return gd.getDefaultConfiguration();
    }
}