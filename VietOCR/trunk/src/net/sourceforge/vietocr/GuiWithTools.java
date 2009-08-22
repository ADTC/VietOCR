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

import java.io.File;
import java.io.IOException;
import java.util.Locale;
import javax.swing.JFileChooser;
import javax.swing.JOptionPane;
import net.sourceforge.vietpad.SimpleFilter;

public class GuiWithTools extends GuiWithSettings {

    File imageFolder;

    public GuiWithTools() {
        imageFolder = new File(prefs.get("ImageFolder", System.getProperty("user.home")));
    }

    @Override
    void mergeTiffs() {
        JFileChooser jf = new JFileChooser();
        jf.setDialogTitle("Select Input TIFF Images");
        jf.setMultiSelectionEnabled(true);
        jf.setCurrentDirectory(imageFolder);
        javax.swing.filechooser.FileFilter tiffFilter = new SimpleFilter("tif;tiff", "TIFF");
        jf.setFileFilter(tiffFilter);
        jf.setAcceptAllFileFilterUsed(false);
        if (jf.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
            File[] inputs = jf.getSelectedFiles();
            imageFolder = jf.getCurrentDirectory();

            jf = new JFileChooser();
            jf.setDialogTitle("Save Output TIFF Image");
            jf.setFileFilter(tiffFilter);
            jf.setAcceptAllFileFilterUsed(false);
            if (jf.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
                File output = jf.getSelectedFile();
                if (!(output.getName().endsWith(".tif") || output.getName().endsWith(".tiff"))) {
                    output = new File(output.getParent(), output.getName() + ".tif");
                }

                try {
                    ImageIOHelper.mergeTiff(inputs, output);
                    JOptionPane.showMessageDialog(this, "Merge completed.", APP_NAME, JOptionPane.INFORMATION_MESSAGE);
                } catch (IOException ioe) {
                    System.err.println(ioe.getMessage());
                }

            }
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
