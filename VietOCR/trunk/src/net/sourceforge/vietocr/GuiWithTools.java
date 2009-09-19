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

import java.awt.Cursor;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Locale;
import javax.swing.JFileChooser;
import javax.swing.filechooser.FileFilter;
import javax.swing.JOptionPane;
import javax.swing.SwingWorker;
import net.sourceforge.vietpad.SimpleFilter;

public class GuiWithTools extends GuiWithSettings {

    File imageFolder;
    FileFilter selectedFilter;

    public GuiWithTools() {
        imageFolder = new File(prefs.get("ImageFolder", System.getProperty("user.home")));
    }

    @Override
    void mergeTiffs() {
        JFileChooser jf = new JFileChooser();
        jf.setDialogTitle(bundle.getString("Select") + " Input Images");
        jf.setCurrentDirectory(imageFolder);
        jf.setMultiSelectionEnabled(true);
        FileFilter tiffFilter = new SimpleFilter("tif;tiff", "TIFF");
        FileFilter jpegFilter = new SimpleFilter("jpg;jpeg", "JPEG");
        FileFilter gifFilter = new SimpleFilter("gif", "GIF");
        FileFilter pngFilter = new SimpleFilter("png", "PNG");
        FileFilter bmpFilter = new SimpleFilter("bmp", "Bitmap");
        FileFilter allImageFilter = new SimpleFilter("tif;tiff;jpg;jpeg;gif;png;bmp", "All Image Files");

        jf.addChoosableFileFilter(tiffFilter);
        jf.addChoosableFileFilter(jpegFilter);
        jf.addChoosableFileFilter(gifFilter);
        jf.addChoosableFileFilter(pngFilter);
        jf.addChoosableFileFilter(bmpFilter);
        jf.addChoosableFileFilter(allImageFilter);

        if (selectedFilter != null) {
            jf.setFileFilter(selectedFilter);
        }

        jf.setAcceptAllFileFilterUsed(false);
        if (jf.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
            selectedFilter = jf.getFileFilter();
            File[] inputs = jf.getSelectedFiles();
            imageFolder = jf.getCurrentDirectory();

            jf = new JFileChooser();
            jf.setDialogTitle(bundle.getString("Save") + " Multi-page TIFF Image");
            jf.setCurrentDirectory(imageFolder);
            jf.setFileFilter(tiffFilter);
            jf.setAcceptAllFileFilterUsed(false);
            if (jf.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
                File outputTiff = jf.getSelectedFile();
                if (!(outputTiff.getName().endsWith(".tif") || outputTiff.getName().endsWith(".tiff"))) {
                    outputTiff = new File(outputTiff.getParent(), outputTiff.getName() + ".tif");
                }

                if (outputTiff.exists()) {
                    outputTiff.delete();
                }

                try {
                    ImageIOHelper.mergeTiff(inputs, outputTiff);
                    JOptionPane.showMessageDialog(this, bundle.getString("Mergecompleted") + outputTiff.getName() + bundle.getString("created"), APP_NAME, JOptionPane.INFORMATION_MESSAGE);
                } catch (Exception e) {
                    JOptionPane.showMessageDialog(this, e.getMessage(), APP_NAME, JOptionPane.ERROR_MESSAGE);
                } catch (OutOfMemoryError oome) {
                    JOptionPane.showMessageDialog(this, oome.getMessage(), bundle.getString("OutOfMemoryError"), JOptionPane.ERROR_MESSAGE);
                }
            }
        }
    }

    @Override
    void splitPdf() {
        SplitPdfDialog dialog = new SplitPdfDialog(this, true);
        if (dialog.showDialog() == JOptionPane.OK_OPTION) {
            final SplitPdfArgs args = dialog.getArgs();

            jLabelStatus.setText(bundle.getString("SplitPDF_running..."));
            jProgressBar1.setIndeterminate(true);
            jProgressBar1.setString(bundle.getString("SplitPDF_running..."));
            jProgressBar1.setVisible(true);
            getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
            getGlassPane().setVisible(true);

            SwingWorker worker = new SwingWorker<String, Void>() {

                @Override
                protected String doInBackground() throws Exception {
                    String inputFilename = args.getInputFilename();
                    String outputFilename = args.getOutputFilename();

                    if (args.isPages()) {
                        Utilities.splitPdf(inputFilename, outputFilename, args.getFromPage(), args.getToPage());
                    } else {
                        if (outputFilename.endsWith(".pdf")) {
                            outputFilename = outputFilename.substring(0, outputFilename.lastIndexOf(".pdf"));
                        }

                        int pageCount = Utilities.getPdfPageCount(inputFilename);
                        if (pageCount == 0) {
                            throw new RuntimeException("Split PDF failed.");
                        }

                        int pageRange = Integer.parseInt(args.getNumOfPages());
                        int startPage = 1;

                        while (startPage <= pageCount) {
                            int endPage = startPage + pageRange - 1;
                            String outputFileName = outputFilename + startPage + ".pdf";
                            Utilities.splitPdf(inputFilename, outputFileName, String.valueOf(startPage), String.valueOf(endPage));
                            startPage = endPage + 1;
                        }
                    }

                    return outputFilename;
                }

                @Override
                protected void done() {
                    try {
                        String result = get();
                        JOptionPane.showMessageDialog(GuiWithTools.this, "Split PDF completed.\nPlease check output file(s) in " + new File(result).getParent());
                    } catch (InterruptedException ignore) {
                        ignore.printStackTrace();
                    } catch (java.util.concurrent.ExecutionException e) {
                        String why = null;
                        Throwable cause = e.getCause();
                        if (cause != null) {
                            if (cause instanceof OutOfMemoryError) {
                                why = bundle.getString("_has_run_out_of_memory.\nPlease_restart_");
                            } else {
                                why = cause.getMessage();
                            }
                        } else {
                            why = e.getMessage();
                        }
                        e.printStackTrace();
                        JOptionPane.showMessageDialog(GuiWithTools.this, why, APP_NAME, JOptionPane.ERROR_MESSAGE);
                    } finally {
                        jLabelStatus.setText(bundle.getString("SplitPDF_completed."));
                        jProgressBar1.setIndeterminate(false);
                        jProgressBar1.setString(bundle.getString("SplitPDF_completed."));
                        getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
                        getGlassPane().setVisible(false);
                    }
                }
            };

            worker.execute();
        }
    }

    @Override
    void quit() {
        super.quit();

        prefs.put("ImageFolder", imageFolder.getPath());

        System.exit(0);
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        selectedUILang = prefs.get("UILanguage", "en");
        Locale.setDefault(selectedUILang.equals("vi") ? VIETNAM : Locale.US);

        java.awt.EventQueue.invokeLater(new Runnable() {

            @Override
            public void run() {
                new GuiWithTools().setVisible(true);
            }
        });
    }
}
