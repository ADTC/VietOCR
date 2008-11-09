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

import java.awt.event.*;
import javax.swing.*;
import java.awt.*;
import java.awt.geom.AffineTransform;
import java.awt.image.BufferedImage;
import java.util.*;

/**
 *
 * @author Quan Nguyen (nguyenq@users.sf.net)
 * @note: Part of this class is from http://forum.java.sun.com/thread.jspa?threadID=634438&messageID=3691163
 */
public class JImageLabel extends JLabel implements MouseMotionListener, MouseListener {

    Point startPoint;
    int preX, preY;
    Rectangle rect;
    BasicStroke bs;
    boolean pressOut = false;
    boolean moving;
    protected int frameWidth = 5;
    protected int minSize = 5;
    protected int startDragX,  startDragY;
    protected boolean resizeLeft,  resizeTop,  resizeRight,  resizeBottom,  move;
    int selX, selY, selW, selH;
    int count;

    /** Creates a new instance of JImageLabel */
    public JImageLabel() {
        addMouseListener(this);
        addMouseMotionListener(this);

        final BasicStroke bs1 = new BasicStroke(1, BasicStroke.CAP_ROUND, BasicStroke.CAP_ROUND, 0, new float[]{6, 6}, 0);
        final BasicStroke bs2 = new BasicStroke(1, BasicStroke.CAP_ROUND, BasicStroke.CAP_ROUND, 0, new float[]{6, 6}, 3);
        final BasicStroke bs3 = new BasicStroke(1, BasicStroke.CAP_ROUND, BasicStroke.CAP_ROUND, 0, new float[]{6, 6}, 6);
        final BasicStroke bs4 = new BasicStroke(1, BasicStroke.CAP_ROUND, BasicStroke.CAP_ROUND, 0, new float[]{6, 6}, 9);

        int delay = 500; //milliseconds
        final int modulus = 4;

        bs = bs1;

        ActionListener taskPerformer = new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent evt) {
                if (count % modulus == 0) {
                    bs = bs1;
                } else if (count % modulus == 1) {
                    bs = bs2;
                } else if (count % modulus == 2) {
                    bs = bs3;
                } else if (count % modulus == 3) {
                    bs = bs4;
                    count = 0;
                }
                count++;

                repaint();
            }
        };
        new javax.swing.Timer(delay, taskPerformer).start();
    }

    public Rectangle getRect() {
        return rect;
    }

    public void deselect() {
        startPoint = null;
        rect = null;
    }

    @Override
    public void paintComponent(Graphics g) {
        // automatically called when repaint
        super.paintComponent(g);

        if (rect != null) {
            Graphics2D g2d = (Graphics2D) g;
            Rectangle[] squares = createSquares(rect);
            for (Rectangle square : squares) {
                g2d.draw(square);
            }

            g2d.setStroke(bs);
            //g2d.setPaint(gp);
            g2d.draw(rect);

        }
    }

    /**
     * Creates grip squares.
     *
     */
    Rectangle[] createSquares(Rectangle rect) {
        if (moving) {
            return new Rectangle[]{};
        }

        int wh = 6;

        int x = rect.x - wh / 2;
        int y = rect.y - wh / 2;
        int w = rect.width;
        int h = rect.height;

        ArrayList<Rectangle> ar = new ArrayList<Rectangle>();
        ar.add(new Rectangle(x, y, wh, wh));
        ar.add(new Rectangle(x + w / 2, y, wh, wh));
        ar.add(new Rectangle(x + w, y, wh, wh));
        ar.add(new Rectangle(x + w, y + h / 2, wh, wh));
        ar.add(new Rectangle(x + w, y + h, wh, wh));
        ar.add(new Rectangle(x + w / 2, y + h, wh, wh));
        ar.add(new Rectangle(x, y + h, wh, wh));
        ar.add(new Rectangle(x, y + h / 2, wh, wh));

        return ar.toArray(new Rectangle[ar.size()]);
    }

    public void rotate(int rotateAngle) {
//        rotateImage(rotateAngle);
        rotateImage2(rotateAngle);
//        rotateEm(rotateAngle);
//        this.rotateAngle = rotateAngle;
//
//        Rectangle bound = this.getBounds();
//
//        int m_imageWidth = this.getIcon().getIconWidth();
//        int m_imageHeight = this.getIcon().getIconHeight();
//
//        m_affineTransform = new AffineTransform();
//        //set the translation to the mid of the component
//        m_affineTransform.setToTranslation((bound.width - m_imageWidth) / 2, (bound.height - m_imageHeight) / 2);
//
//        //rotate with the rotation point as the mid of the image
//        m_affineTransform.rotate(Math.toRadians(rotateAngle), m_imageWidth / 2, m_imageHeight / 2);
//        this.deselect();
//
//        repaint();
    }

    public void rotateImage(int angle) {
        int w = this.getIcon().getIconWidth();
        int h = this.getIcon().getIconHeight();

        BufferedImage bi = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);
        Graphics bg = bi.createGraphics();
        Graphics2D g2d = (Graphics2D) bg;
        g2d.rotate(Math.toRadians(angle), w / 2, h / 2);
        // move the graphics center point to the center of the icon.
//        g2d.translate(w / 2, h / 2);

        g2d.drawImage(((ImageIcon) this.getIcon()).getImage(), 0, 0, w, h, 0, 0, w, h, null);
        g2d.dispose(); //cleans up resources
        this.setIcon(new ImageIcon(bi));
        this.setPreferredSize(new Dimension(h, w));
    }

    public void rotateImage1(int angle) {
        // Sets the Shape.
        Rectangle r = this.getBounds();
        int w = this.getIcon().getIconWidth();
        int h = this.getIcon().getIconHeight();

        AffineTransform at = new AffineTransform();
        at.rotate(Math.toRadians(angle));

        BufferedImage bi = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);
        Graphics bg = bi.createGraphics();
        Graphics2D g2d = (Graphics2D) bg;

        // Sets the selected Shape to the center of the Canvas.
        AffineTransform saveXform = g2d.getTransform();
        AffineTransform toCenterAt = new AffineTransform();
        toCenterAt.concatenate(at);
        toCenterAt.translate(-(r.width / 2), -(r.height / 2));

        g2d.transform(toCenterAt);
        g2d.drawImage(((ImageIcon) this.getIcon()).getImage(), toCenterAt, null);

        g2d.setTransform(saveXform);
    }

    public void rotateImage2(int angle) {
        int h = this.getIcon().getIconHeight();
        int w = this.getIcon().getIconWidth();

        BufferedImage bim = new BufferedImage(h, w, BufferedImage.TYPE_INT_RGB);
        Graphics2D g2d = (Graphics2D) (bim.createGraphics());
        g2d.translate(h, 0);
        g2d.rotate(Math.toRadians(angle)); //apprx 90 degree
        g2d.drawImage(((ImageIcon) this.getIcon()).getImage(), 0, 0, w, h, this);
        this.setIcon(new ImageIcon(bim));
    }

    @Override
    public void mousePressed(MouseEvent e) {
        if (SwingUtilities.isLeftMouseButton(e)) {
            if (rect == null) {
                startPoint = e.getPoint();
                pressOut = true;
            } else {
                selX = rect.x;
                selY = rect.y;
                selW = rect.width;
                selH = rect.height;

                Rectangle leftFrame = new Rectangle(selX, selY, frameWidth, selH);
                Rectangle topFrame = new Rectangle(selX, selY, selW, frameWidth);
                Rectangle rightFrame = new Rectangle(selX + selW - frameWidth, selY, frameWidth, selH);
                Rectangle bottomFrame = new Rectangle(selX, selY + selH - frameWidth, selW, frameWidth);

                Point p = e.getPoint();

                boolean isInside = rect.contains(p);
                boolean isLeft = leftFrame.contains(p);
                boolean isTop = topFrame.contains(p);
                boolean isRight = rightFrame.contains(p);
                boolean isBottom = bottomFrame.contains(p);

                if (isLeft && isTop) {
                    resizeLeft = true;
                    resizeTop = true;
                    resizeRight = false;
                    resizeBottom = false;
                    move = false;
                } else if (isTop && isRight) {
                    resizeLeft = false;
                    resizeTop = true;
                    resizeRight = true;
                    resizeBottom = false;
                    move = false;
                } else if (isRight && isBottom) {
                    resizeLeft = false;
                    resizeTop = false;
                    resizeRight = true;
                    resizeBottom = true;
                    move = false;
                } else if (isBottom && isLeft) {
                    resizeLeft = true;
                    resizeTop = false;
                    resizeRight = false;
                    resizeBottom = true;
                    move = false;
                } else if (isLeft) {
                    resizeLeft = true;
                    resizeTop = false;
                    resizeRight = false;
                    resizeBottom = false;
                    move = false;
                } else if (isTop) {
                    resizeLeft = false;
                    resizeTop = true;
                    resizeRight = false;
                    resizeBottom = false;
                    move = false;
                } else if (isRight) {
                    resizeLeft = false;
                    resizeTop = false;
                    resizeRight = true;
                    resizeBottom = false;
                    move = false;
                } else if (isBottom) {
                    resizeLeft = false;
                    resizeTop = false;
                    resizeRight = false;
                    resizeBottom = true;
                    move = false;
                } else if (isInside) {
                    resizeLeft = false;
                    resizeTop = false;
                    resizeRight = false;
                    resizeBottom = false;
                    move = true;
                } else {
                    resizeLeft = false;
                    resizeTop = false;
                    resizeRight = false;
                    resizeBottom = false;
                    move = false;
                }

                int x = e.getX();
                int y = e.getY();

                startDragX = x;
                startDragY = y;

                preX = rect.x - startDragX;
                preY = rect.y - startDragY;

                if (!rect.contains(p)) {
                    startPoint = p;
                    pressOut = true;
                }

            }
        }
    }

    @Override
    public void mouseDragged(MouseEvent e) {
        if (SwingUtilities.isLeftMouseButton(e)) {
            if (rect == null) {
                rect = new Rectangle();
            } else {
                int x = e.getX();
                int y = e.getY();

                if (pressOut) {
                    rect.x = Math.min(startPoint.x, x);
                    rect.y = Math.min(startPoint.y, y);
                    rect.width = Math.abs(x - startPoint.x);
                    rect.height = Math.abs(y - startPoint.y);
                    moving = true;
                    repaint();
                } else {


                    int diffX = startDragX - x;
                    int diffY = startDragY - y;

                    if (resizeLeft) {
                        rect.x = selX - diffX;
                        rect.width = selW + diffX;
                    }

                    if (resizeTop) {
                        rect.y = selY - diffY;
                        rect.height = selH + diffY;
                    }

                    if (resizeRight) {
                        rect.width = selW - diffX;
                    }

                    if (resizeBottom) {
                        rect.height = selH - diffY;
                    }

                    if (move) {
                        moving = true;
                        rect.setLocation(preX + x, preY + y);
                    }

                    if (rect.width > minSize && rect.height > minSize) {
                        repaint();
                    }
                }
            }
        }
    }

    @Override
    public void mouseClicked(MouseEvent e) {
        if (SwingUtilities.isLeftMouseButton(e)) {
            if (rect != null) {
                if (!rect.contains(e.getPoint())) {
                    deselect();
                    repaint();
                }
            }
        }
    }

    /**
     * Invoked when the mouse cursor has been moved onto a component
     * but no buttons have been pushed.
     */
    @Override
    public void mouseMoved(MouseEvent e) {

        if (rect != null) {
            int rectX = rect.x;
            int rectY = rect.y;
            int rectW = rect.width;
            int rectH = rect.height;

            Rectangle leftFrame = new Rectangle(rectX, rectY, frameWidth, rectH);
            Rectangle topFrame = new Rectangle(rectX, rectY, rectW, frameWidth);
            Rectangle rightFrame = new Rectangle(rectX + rectW - frameWidth, rectY, frameWidth, rectH);
            Rectangle bottomFrame = new Rectangle(rectX, rectY + rectH - frameWidth, rectW, frameWidth);

            Point p = e.getPoint();

            boolean isInside = rect.contains(p);
            boolean isLeft = leftFrame.contains(p);
            boolean isTop = topFrame.contains(p);
            boolean isRight = rightFrame.contains(p);
            boolean isBottom = bottomFrame.contains(p);

            if (isLeft && isTop) {
                setCursor(Cursor.getPredefinedCursor(Cursor.NW_RESIZE_CURSOR));
            } else if (isTop && isRight) {
                setCursor(Cursor.getPredefinedCursor(Cursor.NE_RESIZE_CURSOR));
            } else if (isRight && isBottom) {
                setCursor(Cursor.getPredefinedCursor(Cursor.SE_RESIZE_CURSOR));
            } else if (isBottom && isLeft) {
                setCursor(Cursor.getPredefinedCursor(Cursor.SW_RESIZE_CURSOR));
            } else if (isLeft) {
                setCursor(Cursor.getPredefinedCursor(Cursor.W_RESIZE_CURSOR));
            } else if (isTop) {
                setCursor(Cursor.getPredefinedCursor(Cursor.N_RESIZE_CURSOR));
            } else if (isRight) {
                setCursor(Cursor.getPredefinedCursor(Cursor.E_RESIZE_CURSOR));
            } else if (isBottom) {
                setCursor(Cursor.getPredefinedCursor(Cursor.S_RESIZE_CURSOR));
            } else if (isInside) {
                setCursor(Cursor.getPredefinedCursor(Cursor.MOVE_CURSOR));
            } else {
                setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
            }
        } else {
            setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
        }
    }

    @Override
    public void mouseReleased(MouseEvent e) {
        if (SwingUtilities.isLeftMouseButton(e)) {
            if (rect != null) {
                moving = false;
                pressOut = false;
                repaint();
            }
        }
    }

    @Override
    public void mouseEntered(MouseEvent e) {
    }

    @Override
    public void mouseExited(MouseEvent e) {
    }
}