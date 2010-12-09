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

import java.awt.Component;
import java.awt.Cursor;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.*;
import java.net.URL;
import java.net.URLConnection;
import java.text.Collator;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Enumeration;
import java.util.List;
import java.util.Properties;
import java.util.ResourceBundle;
import javax.swing.*;
import net.sourceforge.vietocr.utilities.FileExtractor;
import net.sourceforge.vietocr.utilities.Utilities;

public class DownloadDialog extends javax.swing.JDialog {

    final static int BUFFER_SIZE = 1024;
    final String tmpdir = System.getProperty("java.io.tmpdir");
    private Properties availableLanguageCodes;
    private Properties availableDictionaries;
    private Properties iso_3_1_Codes;
    private Properties lookupISO639;
    File baseDir;
    SwingWorker<File, Integer> downloadWorker;
    int length, byteCount, numberOfDownloads, numOfConcurrentTasks;
    ResourceBundle bundle;

    /** Creates new form DownloadDialog */
    public DownloadDialog(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        initComponents();
        bundle = ResourceBundle.getBundle("net/sourceforge/vietocr/DownloadDialog");

        baseDir = Utilities.getBaseDir(DownloadDialog.this);
        lookupISO639 = ((Gui) parent).getLookupISO639();
        iso_3_1_Codes= ((Gui) parent).getISO_3_1_Codes();
        availableLanguageCodes = new Properties();
        availableDictionaries = new Properties();

        try {
            File xmlFile = new File(baseDir, "data/Tess2DataURL.xml");
            availableLanguageCodes.loadFromXML(new FileInputStream(xmlFile));
            xmlFile = new File(baseDir, "data/OO-SpellDictionaries.xml");
            availableDictionaries.loadFromXML(new FileInputStream(xmlFile));
        } catch (Exception e) {
        }

        setLocationRelativeTo(getOwner());

        //  Handle escape key to hide the dialog
        KeyStroke escapeKeyStroke = KeyStroke.getKeyStroke(KeyEvent.VK_ESCAPE, 0, false);
        Action escapeAction =
                new AbstractAction() {

                    @Override
                    public void actionPerformed(ActionEvent e) {
                        setVisible(false);
                    }
                };
        getRootPane().getInputMap(JComponent.WHEN_IN_FOCUSED_WINDOW).put(escapeKeyStroke, "ESCAPE");
        getRootPane().getActionMap().put("ESCAPE", escapeAction);
    }

    /** This method is called from within the constructor to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {
        java.awt.GridBagConstraints gridBagConstraints;

        jPanel1 = new javax.swing.JPanel();
        jLabelStatus = new javax.swing.JLabel();
        jProgressBar1 = new javax.swing.JProgressBar();
        this.jProgressBar1.setVisible(false);
        jPanel2 = new javax.swing.JPanel();
        jButtonDownload = new javax.swing.JButton();
        jButtonCancel = new javax.swing.JButton();
        jButtonCancel.setEnabled(false);
        jButtonClose = new javax.swing.JButton();
        jPanel3 = new javax.swing.JPanel();
        jScrollPane1 = new javax.swing.JScrollPane();
        jList1 = new javax.swing.JList();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        java.util.ResourceBundle bundle = java.util.ResourceBundle.getBundle("net/sourceforge/vietocr/DownloadDialog"); // NOI18N
        setTitle(bundle.getString("Download_Language_Data")); // NOI18N
        setResizable(false);
        addWindowListener(new java.awt.event.WindowAdapter() {
            public void windowOpened(java.awt.event.WindowEvent evt) {
                formWindowOpened(evt);
            }
        });

        jPanel1.setLayout(new java.awt.FlowLayout(java.awt.FlowLayout.LEFT));
        jPanel1.add(jLabelStatus);
        jPanel1.add(jProgressBar1);

        getContentPane().add(jPanel1, java.awt.BorderLayout.SOUTH);

        jPanel2.setBorder(javax.swing.BorderFactory.createEmptyBorder(1, 1, 1, 20));
        jPanel2.setLayout(new java.awt.GridBagLayout());

        jButtonDownload.setText(bundle.getString("Download")); // NOI18N
        jButtonDownload.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonDownloadActionPerformed(evt);
            }
        });
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.insets = new java.awt.Insets(0, 0, 5, 0);
        jPanel2.add(jButtonDownload, gridBagConstraints);

        jButtonCancel.setText(bundle.getString("Cancel")); // NOI18N
        jButtonCancel.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonCancelActionPerformed(evt);
            }
        });
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 0;
        gridBagConstraints.gridy = 1;
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        gridBagConstraints.insets = new java.awt.Insets(0, 0, 25, 0);
        jPanel2.add(jButtonCancel, gridBagConstraints);

        jButtonClose.setText(bundle.getString("Close")); // NOI18N
        jButtonClose.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonCloseActionPerformed(evt);
            }
        });
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 0;
        gridBagConstraints.gridy = 2;
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        jPanel2.add(jButtonClose, gridBagConstraints);

        getContentPane().add(jPanel2, java.awt.BorderLayout.EAST);

        jPanel3.setBorder(javax.swing.BorderFactory.createEmptyBorder(10, 10, 10, 20));
        jPanel3.setPreferredSize(new java.awt.Dimension(200, 175));
        jPanel3.setLayout(new java.awt.BorderLayout());

        jScrollPane1.setBorder(javax.swing.BorderFactory.createTitledBorder(bundle.getString("Available_Languages"))); // NOI18N

        jScrollPane1.setViewportView(jList1);

        jPanel3.add(jScrollPane1, java.awt.BorderLayout.CENTER);

        getContentPane().add(jPanel3, java.awt.BorderLayout.CENTER);

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButtonDownloadActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonDownloadActionPerformed
        if (this.jList1.getSelectedIndex() == -1) {
            return;
        }

        this.jButtonDownload.setEnabled(false);
        this.jButtonCancel.setEnabled(true);
        this.jLabelStatus.setText(bundle.getString("Downloading..."));
        this.jProgressBar1.setMaximum(100);
        this.jProgressBar1.setValue(0);
        this.jProgressBar1.setVisible(true);
        getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
        getGlassPane().setVisible(true);

        length = byteCount = 0;
        numOfConcurrentTasks = this.jList1.getSelectedIndices().length;

        for (Object value : this.jList1.getSelectedValues()) {
            String key = FindKey(lookupISO639, value.toString()); // Vietnamese -> vie
            if (key != null) {
                try {
                    URL url = new URL(availableLanguageCodes.getProperty(key));
                    downloadDataFile(url, "tesseract"); // download language data pack
                    if (iso_3_1_Codes.containsKey(key)) {
                        String iso_3_1_Code = iso_3_1_Codes.getProperty(key); // vie -> vi_VN
                        url = new URL(availableDictionaries.getProperty(iso_3_1_Code));
                        if (url != null) {
                            ++numOfConcurrentTasks;
                            downloadDataFile(url, "dict"); // download dictionary
                        }
                    }
                } catch (Exception e) {
                }
            }
        }
    }//GEN-LAST:event_jButtonDownloadActionPerformed

    String FindKey(Properties lookup, String value) {
        for (Enumeration e = lookup.keys(); e.hasMoreElements();) {
            String key = (String) e.nextElement();
            if (lookup.get(key).equals(value)) {
                return key;
            }
        }
        return null;
    }

    void downloadDataFile(final URL remoteFile, final String destFolder) throws Exception {
        final URLConnection connection = remoteFile.openConnection();
        connection.setReadTimeout(15000);
        connection.connect();
        length += connection.getContentLength(); // filesize
        downloadWorker = new SwingWorker<File, Integer>() {

            @Override
            public File doInBackground() throws Exception {
                InputStream inputStream = connection.getInputStream();
                File outputFile = new File(tmpdir, new File(remoteFile.getFile()).getName());
                FileOutputStream fos = new FileOutputStream(outputFile);
                BufferedOutputStream bout = new BufferedOutputStream(fos);
                byte[] buffer = new byte[BUFFER_SIZE];
                int bytesRead = 0;

                while ((bytesRead = inputStream.read(buffer, 0, BUFFER_SIZE)) > -1) {
                    if (isCancelled()) {
                        break;
                    }
                    bout.write(buffer, 0, bytesRead);
                    byteCount += bytesRead;
                    setProgress(100 * byteCount / length);
                }

                bout.close();
                inputStream.close();
                return outputFile;
            }

            @Override
            public void done() {
                try {
                    File file = get();
                    FileExtractor.extractCompressedFile(file.getPath(), baseDir.getPath() + "/" + destFolder);
                    numberOfDownloads++;
                    if (--numOfConcurrentTasks <= 0) {
                        jLabelStatus.setText(bundle.getString("Download_completed"));
                        jProgressBar1.setVisible(false);
                    }
                } catch (InterruptedException e) {
                    e.printStackTrace();
                    numOfConcurrentTasks = 0;
                } catch (java.util.concurrent.ExecutionException e) {
                    String why = null;
                    Throwable cause = e.getCause();
                    if (cause != null) {
                        if (cause instanceof UnsupportedOperationException) {
                            why = cause.getMessage();
                        } else if (cause instanceof RuntimeException) {
                            why = cause.getMessage();
                        } else if (cause instanceof FileNotFoundException) {
                            why = bundle.getString("Resource_does_not_exist") + cause.getMessage();
                        } else {
                            why = cause.getMessage();
                        }
                    } else {
                        why = e.getMessage();
                    }
                    e.printStackTrace();
                    JOptionPane.showMessageDialog(null, why, Gui.APP_NAME, JOptionPane.ERROR_MESSAGE);
                    jProgressBar1.setVisible(false);
                    jLabelStatus.setText(null);
                    numOfConcurrentTasks = 0;
                } catch (java.util.concurrent.CancellationException e) {
                    jLabelStatus.setText(bundle.getString("Download_cancelled"));
//                    jProgressBar1.setVisible(false);
                    numOfConcurrentTasks = 0;
                } finally {
                    if (numOfConcurrentTasks <= 0) {
                        jButtonDownload.setEnabled(true);
                        jButtonCancel.setEnabled(false);
                        getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
                        getGlassPane().setVisible(false);
                    }
                }
            }
        };

        downloadWorker.addPropertyChangeListener(new PropertyChangeListener() {

            @Override
            public void propertyChange(PropertyChangeEvent evt) {
                if ("progress".equals(evt.getPropertyName())) {
                    jProgressBar1.setValue((Integer) evt.getNewValue());
                }
            }
        });
        downloadWorker.execute();
    }

    private void jButtonCloseActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonCloseActionPerformed
        this.setVisible(false);

        if (numberOfDownloads > 0) {
            JOptionPane.showMessageDialog(DownloadDialog.this, bundle.getString("Please_restart"), Gui.APP_NAME, JOptionPane.INFORMATION_MESSAGE);
        }
        this.dispose();
    }//GEN-LAST:event_jButtonCloseActionPerformed

    /**
     * Populates the list upon window opening.
     * @param evt
     */
    private void formWindowOpened(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowOpened
        String[] available = availableLanguageCodes.keySet().toArray(new String[0]);
        List<String> languageNames = new ArrayList<String>();
        for (String key : available) {
            languageNames.add(this.lookupISO639.getProperty(key, key));
        }
        Collections.sort(languageNames, Collator.getInstance());
        DefaultListModel model = new DefaultListModel();
        for (String name : languageNames) {
            model.addElement(name);
        }

        this.jList1.setModel(model);
        this.jList1.setCellRenderer(new CustomCellRenderer(((Gui) this.getParent()).getInstalledLanguages()));
    }//GEN-LAST:event_formWindowOpened

    private void jButtonCancelActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonCancelActionPerformed
        if (downloadWorker != null && !downloadWorker.isDone()) {
            // Cancel current OCR op to begin a new one. You want only one OCR op at a time.
            downloadWorker.cancel(true);
            downloadWorker = null;
        }

        this.jButtonCancel.setEnabled(false);
    }//GEN-LAST:event_jButtonCancelActionPerformed

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        java.awt.EventQueue.invokeLater(new Runnable() {

            @Override
            public void run() {
                DownloadDialog dialog = new DownloadDialog(new javax.swing.JFrame(), true);
                dialog.addWindowListener(new java.awt.event.WindowAdapter() {

                    @Override
                    public void windowClosing(java.awt.event.WindowEvent e) {
                        System.exit(0);
                    }
                });
                dialog.setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jButtonCancel;
    private javax.swing.JButton jButtonClose;
    private javax.swing.JButton jButtonDownload;
    private javax.swing.JLabel jLabelStatus;
    private javax.swing.JList jList1;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel jPanel3;
    private javax.swing.JProgressBar jProgressBar1;
    private javax.swing.JScrollPane jScrollPane1;
    // End of variables declaration//GEN-END:variables

    /**
     * A custom renderer which disables certain elements in list.
     */
    class CustomCellRenderer extends DefaultListCellRenderer {

        Object[] disabledElements;

        CustomCellRenderer(Object[] disabledElements) {
            this.disabledElements = disabledElements;
        }

        @Override
        public Component getListCellRendererComponent(JList list, Object value, int index, boolean isSelected, boolean cellHasFocus) {
            for (Object el : disabledElements) {
                if (value.equals(el)) {
                    Component c = super.getListCellRendererComponent(list, value, index, false, false);
                    c.setEnabled(false);
                    return c;
                }
            }

            return super.getListCellRendererComponent(list, value, index, isSelected, cellHasFocus);
        }
    }
}
