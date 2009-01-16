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
import java.io.*;
import java.util.*;
import javax.imageio.IIOImage;
import javax.swing.*;
import javax.swing.Timer;
import net.sourceforge.vietocr.postprocessing.Processor;

/**
 *
 * @author Quan Nguyen
 */
public class GuiWithWatch extends Gui {
    private String watchFolder = System.getProperty("user.home");
    private String outputFolder = System.getProperty("user.home");
    private boolean watchEnabled;

    private StatusFrame statusPanel;
    private WatchDialog dialog;

    public GuiWithWatch() {
        statusPanel = new StatusFrame();

        // watch for new image files
        final Queue<File> queue = new LinkedList<File>();
        Thread t = new Thread(new Watcher(queue, new File(watchFolder)));
        t.start();

        // autoOCR if there are files in the queue
        Action autoOcrAction = new AbstractAction() {

            @Override
            public void actionPerformed(ActionEvent e) {
                final File imageFile = queue.poll();
                if (imageFile != null) {
                    final ArrayList<IIOImage> iioImageList = ImageIOHelper.getIIOImageList(imageFile);
                    if (!statusPanel.isVisible()) {
                        statusPanel.setVisible(true);
                    }
                    statusPanel.getTextArea().append(imageFile.getPath() + "\n");

                    SwingUtilities.invokeLater(new Runnable() {

                        @Override
                        public void run() {
                            try {
                                OCR ocrEngine = new OCR(tessPath);
                                String result = ocrEngine.recognizeText(iioImageList, -1, curLangCode);

                                // postprocess to correct common OCR errors
                                result = Processor.postProcess(result, curLangCode);

                                BufferedWriter out = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(new File(outputFolder, imageFile.getName() + ".txt")), UTF8));
                                out.write(result);
                                out.close();
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
                        }
                    });
                }
            }
        };

        new Timer(5000, autoOcrAction).start();
    }

    @Override
    protected void openWatchDialog() {
        if (dialog == null) {
            dialog = new WatchDialog(this, true);
        }

        dialog.setWatchFolder(watchFolder);
        dialog.setOutputFolder(outputFolder);
        dialog.setWatchEnabled(watchEnabled);

        dialog.setVisible(true);

//            if (dialog.ShowDialog() == DialogResult.OK)
//            {
//                watchFolder = dialog.getWatchFolder();
//                outputFolder = dialog.getOutputFolder();
//                watchEnabled = dialog.isWatchEnabled();
//            }
    }


    /**
     *  Updates UI component if changes in LAF
     *
     *@param  laf  the look and feel class name
     */
    @Override
    protected void updateLaF(String laf) {
        super.updateLaF(laf);

        SwingUtilities.updateComponentTreeUI(this.statusPanel);
        if (dialog != null) {
            SwingUtilities.updateComponentTreeUI(this.dialog);
        }
    }
    
    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        selectedUILang = prefs.get("UILanguage", "en");
        Locale.setDefault(selectedUILang.equals("vi") ? VIETNAM : Locale.US);

        java.awt.EventQueue.invokeLater(new Runnable() {

            @Override
            public void run() {
                new GuiWithWatch().setVisible(true);
            }
        });
    }
}
