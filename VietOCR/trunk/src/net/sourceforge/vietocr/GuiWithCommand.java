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

import java.awt.*;
import java.awt.image.*;
import java.io.*;
import java.util.*;
import java.util.List;
import javax.imageio.IIOImage;
import javax.swing.*;
import net.sourceforge.vietocr.postprocessing.Processor;

public class GuiWithCommand extends Gui {

    private OcrWorker ocrWorker;

    @Override
    void OCRActionPerformed() {
        if (jImageLabel.getIcon() == null) {
            JOptionPane.showMessageDialog(this, bundle.getString("Please_load_an_image."), APP_NAME, JOptionPane.INFORMATION_MESSAGE);
            return;
        }

        Rectangle rect = ((JImageLabel) jImageLabel).getRect();

        if (rect != null) {
            try {
                ImageIcon ii = (ImageIcon) this.jImageLabel.getIcon();
                int offsetX = 0;
                int offsetY = 0;
                if (ii.getIconWidth() < this.jScrollPane2.getWidth()) {
                    offsetX = (this.jScrollPane2.getViewport().getWidth() - ii.getIconWidth()) / 2;
                }
                if (ii.getIconHeight() < this.jScrollPane2.getHeight()) {
                    offsetY = (this.jScrollPane2.getViewport().getHeight() - ii.getIconHeight()) / 2;
                }
                BufferedImage bi = ((BufferedImage) ii.getImage()).getSubimage((int) ((rect.x - offsetX) * scaleX), (int) ((rect.y - offsetY) * scaleY), (int) (rect.width * scaleX), (int) (rect.height * scaleY));
                IIOImage iioImage = new IIOImage(bi, null, null);
                ArrayList<IIOImage> tempList = new ArrayList<IIOImage>();
                tempList.add(iioImage);
                performOCR(tempList, 0, rect);
            } catch (RasterFormatException rfe) {
                JOptionPane.showMessageDialog(this, rfe.getMessage(), APP_NAME, JOptionPane.ERROR_MESSAGE);
//                rfe.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else {
            performOCR(iioImageList, imageIndex, null);
        }
    }

    @Override
    void OCRAllActionPerformed() {
        if (this.jImageLabel.getIcon() == null) {
            JOptionPane.showMessageDialog(this, bundle.getString("Please_load_an_image."), APP_NAME, JOptionPane.INFORMATION_MESSAGE);
            return;
        }

        this.jButtonOCR.setVisible(false);
        this.jButtonCancelOCR.setVisible(true);
        this.jButtonCancelOCR.setEnabled(true);
        performOCR(iioImageList, -1, null);
    }

    @Override
    void PostProcessActionPerformed() {
        if (curLangCode == null) {
            return;
        }

        jLabelStatus.setText(bundle.getString("Correcting_errors..."));
        jProgressBar1.setIndeterminate(true);
        jProgressBar1.setString(bundle.getString("Correcting_errors..."));
        jProgressBar1.setVisible(true);
        getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
        getGlassPane().setVisible(true);
        this.jMenuItemPostProcess.setEnabled(false);

        SwingWorker<String, Void> correctWorker = new SwingWorker<String, Void>() {

            String selectedText;

            @Override
            public String doInBackground() throws Exception {
                selectedText = jTextArea1.getSelectedText();
                return Processor.postProcess((selectedText != null) ? selectedText : jTextArea1.getText(), curLangCode, dangAmbigsPath, dangAmbigsOn);
            }

            @Override
            public void done() {
                jProgressBar1.setIndeterminate(false);

                try {
                    String result = get();

                    if (selectedText != null) {
                        int start = jTextArea1.getSelectionStart();
                        jTextArea1.replaceSelection(result);
                        jTextArea1.select(start, start + result.length());
                    } else {
                        jTextArea1.setText(result);
                    }
                    jLabelStatus.setText(bundle.getString("Correction_completed"));
                    jProgressBar1.setString(bundle.getString("Correction_completed"));
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (java.util.concurrent.ExecutionException e) {
                    String why = null;
                    Throwable cause = e.getCause();
                    if (cause != null) {
                        if (cause instanceof UnsupportedOperationException) {
                            why = String.format("Post-processing not supported for %1$s language.\nYou can provide one via a \"%2$s.DangAmbigs.txt\" file.", jComboBoxLang.getSelectedItem(), curLangCode);
                        } else if (cause instanceof RuntimeException) {
                            why = cause.getMessage();
                        } else {
                            why = cause.getMessage();
                        }
                    } else {
                        why = e.getMessage();
                    }
                    e.printStackTrace();
                    JOptionPane.showMessageDialog(null, why, APP_NAME, JOptionPane.ERROR_MESSAGE);
                    jProgressBar1.setVisible(false);
                } finally {
                    getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
                    getGlassPane().setVisible(false);
                    jMenuItemPostProcess.setEnabled(true);
                }
            }
        };
        correctWorker.execute();
    }

    /**
     * Perform OCR on images represented by IIOImage.
     *
     * @param list List of IIOImage
     * @param index Index of page to be OCRed: -1 for all pages
     */
    void performOCR(final List<IIOImage> iioImageList, final int index, Rectangle rect) {
        if (this.jComboBoxLang.getSelectedIndex() == -1) {
            JOptionPane.showMessageDialog(this, bundle.getString("Please_select_a_language."), APP_NAME, JOptionPane.INFORMATION_MESSAGE);
            return;
        }

        jLabelStatus.setText(bundle.getString("OCR_running..."));
        jProgressBar1.setIndeterminate(true);
        jProgressBar1.setString(bundle.getString("OCR_running..."));
        jProgressBar1.setVisible(true);
        getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
        getGlassPane().setVisible(true);
        this.jButtonOCR.setEnabled(false);
        this.jMenuItemOCR.setEnabled(false);
        this.jMenuItemOCRAll.setEnabled(false);

        OCRImageEntity entity = new OCRImageEntity(iioImageList, index, rect, curLangCode);
//        entity.setScreenshotMode(true);

        // instantiate SwingWorker for OCR
        ocrWorker = new OcrWorker(entity);
        ocrWorker.execute();
    }

    @Override
    void CancelOCRActionPerformed() {
        if (ocrWorker != null && !ocrWorker.isDone()) {
            // Cancel current OCR op to begin a new one. You want only one OCR op at a time.
            ocrWorker.cancel(true);
            ocrWorker = null;
        }

        this.jButtonCancelOCR.setEnabled(false);
    }

    class OcrWorker extends SwingWorker<Void, String> {

        OCRImageEntity entity;
        List<File> workingFiles;

        OcrWorker(OCRImageEntity entity) {
            this.entity = entity;
        }

        @Override
        protected Void doInBackground() throws Exception {
            OCR ocrEngine = new OCR(tessPath);
            workingFiles = entity.getClonedImageFiles();

            for (int i = 0; i < workingFiles.size(); i++) {
                if (!isCancelled()) {
                    String result = ocrEngine.recognizeText(workingFiles.subList(i, i + 1), entity.getLanguage());
                    publish(result); // interim result
                }
            }

            return null;
        }

        @Override
        protected void process(List<String> results) {
            for (String str : results) {
                jTextArea1.append(str);
                jTextArea1.setCaretPosition(jTextArea1.getDocument().getLength());
            }
        }

        @Override
        protected void done() {
            jProgressBar1.setIndeterminate(false);

            try {
                get(); // dummy method
                jLabelStatus.setText(bundle.getString("OCR_completed."));
                jProgressBar1.setString(bundle.getString("OCR_completed."));
            } catch (InterruptedException ignore) {
                ignore.printStackTrace();
            } catch (java.util.concurrent.ExecutionException e) {
                String why = null;
                Throwable cause = e.getCause();
                if (cause != null) {
                    if (cause instanceof IOException) {
                        why = bundle.getString("Cannot_find_Tesseract._Please_set_its_path.");
                    } else if (cause instanceof FileNotFoundException) {
                        why = bundle.getString("An_exception_occurred_in_Tesseract_engine_while_recognizing_this_image.");
                    } else if (cause instanceof OutOfMemoryError) {
                        why = bundle.getString("_has_run_out_of_memory.\nPlease_restart_");
                    } else {
                        why = cause.getMessage();
                    }
                } else {
                    why = e.getMessage();
                }
                e.printStackTrace();
//                    System.err.println(why);
                jLabelStatus.setText(null);
                jProgressBar1.setString(null);
                JOptionPane.showMessageDialog(null, why, "OCR Operation", JOptionPane.ERROR_MESSAGE);
            } catch (java.util.concurrent.CancellationException e) {
                jLabelStatus.setText("OCR " + bundle.getString("canceled"));
                jProgressBar1.setString("OCR " + bundle.getString("canceled"));
            } finally {
                getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
                getGlassPane().setVisible(false);
                jButtonOCR.setVisible(true);
                jButtonOCR.setEnabled(true);
                jMenuItemOCR.setEnabled(true);
                jMenuItemOCRAll.setEnabled(true);
                jButtonCancelOCR.setVisible(false);

                // clean up temporary image files
                for (File tempImageFile : workingFiles) {
                    tempImageFile.delete();
                }
            }
        }
    }
}
