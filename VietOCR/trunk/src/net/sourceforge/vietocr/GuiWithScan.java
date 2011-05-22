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

import java.awt.Cursor;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import javax.imageio.ImageIO;
import javax.swing.JOptionPane;
import javax.swing.SwingUtilities;
import net.sourceforge.vietocr.wia.*;
import uk.co.mmscomputing.device.scanner.*;
import uk.co.mmscomputing.device.sane.*;

public class GuiWithScan extends Gui implements ScannerListener {

    BufferedImage scannedImage;

    public GuiWithScan() {
//        // Hide Scan buttons for non-Windows OS because WIA Automation is Windows only
//        if (!WINDOWS) {
//            this.jButtonScan.setVisible(false);
//            this.jMenuItemScan.setVisible(false);
//        }
    }

    /**
     * Access scanner and scan documents via Windows WIA or Linux Sane.
     *
     */
    @Override
    void jMenuItemScanActionPerformed(java.awt.event.ActionEvent evt) {
        scaleX = scaleY = 1f;

        jLabelStatus.setText(bundle.getString("Scanning..."));
        jProgressBar1.setIndeterminate(true);
        jProgressBar1.setString(bundle.getString("Scanning..."));
        jProgressBar1.setVisible(true);
        getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
        getGlassPane().setVisible(true);
        jMenuItemScan.setEnabled(false);
        jButtonScan.setEnabled(false);

        SwingUtilities.invokeLater(new Runnable() {

            @Override
            public void run() {
                try {
                    File tempImageFile = File.createTempFile("tmp", WINDOWS ? ".bmp" : ".png");

                    if (tempImageFile.exists()) {
                        tempImageFile.delete();
                    }
                    if (WINDOWS) {
                        WiaScannerAdapter adapter = new WiaScannerAdapter(); // with MS WIA
                        // The reason for not using PNG format is that jai-imageio library would throw an "I/O error reading PNG header" error.
                        tempImageFile = adapter.ScanImage(FormatID.wiaFormatBMP, tempImageFile.getCanonicalPath());
                    } else { // Linux
                        Scanner scanner = Scanner.getDevice();
                        scanner.addListener(GuiWithScan.this);
                        System.out.println("Device Name: " + scanner.getSelectedDeviceName());
                        scanner.acquire();
                        if (scannedImage != null) {
                            ImageIO.write(scannedImage, "png", tempImageFile);
                        }
                    }
                    openFile(tempImageFile);
                    tempImageFile.deleteOnExit();
                } catch (ScannerIOException se) {
//                    se.printStackTrace();
                    JOptionPane.showMessageDialog(null, se.getMessage(), "Error Scanning Image", JOptionPane.ERROR_MESSAGE);
                } catch (IOException ioe) {
                    JOptionPane.showMessageDialog(null, ioe.getMessage(), "I/O Error", JOptionPane.ERROR_MESSAGE);
                } catch (WiaOperationException woe) {
                    JOptionPane.showMessageDialog(null, woe.getWIAMessage(), woe.getMessage(), JOptionPane.WARNING_MESSAGE);
                } catch (Exception e) {
                    String msg = e.getMessage();
                    if (msg == null || msg.equals("")) {
                        msg = "Scanner Operation Error.";
                    }
                    JOptionPane.showMessageDialog(null, msg, "Scanner Operation Error", JOptionPane.ERROR_MESSAGE);
                } finally {
                    jLabelStatus.setText(bundle.getString("Scanning_completed"));
                    jProgressBar1.setIndeterminate(false);
                    jProgressBar1.setString(bundle.getString("Scanning_completed"));
                    getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
                    getGlassPane().setVisible(false);
                    jMenuItemScan.setEnabled(true);
                    jButtonScan.setEnabled(true);
                }
            }
        });
    }

    /**
     * Sane scanning.
     * 
     * @param type
     * @param metadata 
     */
    @Override
    public void update(ScannerIOMetadata.Type type, ScannerIOMetadata metadata) {
        if (type.equals(ScannerIOMetadata.ACQUIRED)) {
            scannedImage = metadata.getImage();
        } else if (type.equals(ScannerIOMetadata.NEGOTIATE)) {
            SaneDevice device = (SaneDevice) metadata.getDevice();
            try {
                device.setResolution(300);
                device.setOption("mode", "True Gray");
                device.setOption("source", "FlatBed");
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (type.equals(ScannerIOMetadata.EXCEPTION)) {
            metadata.getException().printStackTrace();
        }
    }
}
