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
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.io.*;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.util.Arrays;
import java.util.Enumeration;
import java.util.Properties;
import javax.swing.*;
import net.sourceforge.vietocr.utilities.FileExtractor;
import net.sourceforge.vietocr.utilities.Utilities;

public class DownloadDialog extends javax.swing.JDialog {

    final String urlAddress = "http://tesseract-ocr.googlecode.com/files/tesseract-2.00.%1$s.tar.gz";
    private Properties availableCodes;
    private String[] installedCodes;

    /** Creates new form DownloadDialog */
    public DownloadDialog(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        initComponents();

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
        jProgressBar1 = new javax.swing.JProgressBar();
        this.jProgressBar1.setVisible(false);
        jPanel2 = new javax.swing.JPanel();
        jButtonDownload = new javax.swing.JButton();
        jButtonCancel = new javax.swing.JButton();
        jButtonClose = new javax.swing.JButton();
        jPanel3 = new javax.swing.JPanel();
        jScrollPane1 = new javax.swing.JScrollPane();
        jList1 = new javax.swing.JList();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Download Language Data");
        setResizable(false);
        addWindowListener(new java.awt.event.WindowAdapter() {
            public void windowActivated(java.awt.event.WindowEvent evt) {
                formWindowActivated(evt);
            }
            public void windowOpened(java.awt.event.WindowEvent evt) {
                formWindowOpened(evt);
            }
        });

        jPanel1.setLayout(new java.awt.FlowLayout(java.awt.FlowLayout.LEFT));
        jPanel1.add(jProgressBar1);

        getContentPane().add(jPanel1, java.awt.BorderLayout.SOUTH);

        jPanel2.setBorder(javax.swing.BorderFactory.createEmptyBorder(1, 1, 1, 20));
        jPanel2.setLayout(new java.awt.GridBagLayout());

        jButtonDownload.setText("Download");
        jButtonDownload.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonDownloadActionPerformed(evt);
            }
        });
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.insets = new java.awt.Insets(0, 0, 5, 0);
        jPanel2.add(jButtonDownload, gridBagConstraints);

        jButtonCancel.setText("Cancel");
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

        jButtonClose.setText("Close");
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

        jScrollPane1.setBorder(javax.swing.BorderFactory.createTitledBorder("Available Languages"));

        jScrollPane1.setViewportView(jList1);

        jPanel3.add(jScrollPane1, java.awt.BorderLayout.CENTER);

        getContentPane().add(jPanel3, java.awt.BorderLayout.CENTER);

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButtonDownloadActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonDownloadActionPerformed
        if (this.jList1.getSelectedIndex() == -1) {
            return;
        }

        this.jProgressBar1.setVisible(true);
        try {
            String key = FindKey(availableCodes, this.jList1.getSelectedValue().toString());
            URL url = new URL(String.format(urlAddress, key));
            File out = loadFile(url);
            File baseDir = Utilities.getBaseDir(DownloadDialog.this);
            FileExtractor.extractCompressedFile(out.getPath(), baseDir.getPath() + "/tesseract");
        } catch (Exception e) {
            JOptionPane.showMessageDialog(null, "Resource does not exist:\n" + e.getMessage(), this.getTitle(), JOptionPane.ERROR_MESSAGE);
        }
    }//GEN-LAST:event_jButtonDownloadActionPerformed

    public String FindKey(Properties lookup, String value) {
        for (Enumeration e = lookup.keys(); e.hasMoreElements();) {
            String key = (String) e.nextElement();
            if (lookup.get(key).equals(value)) {
                return key;
            }
        }
        return null;
    }

    public File loadFile(URL remoteFile) throws Exception {
        URLConnection connection = remoteFile.openConnection();
        connection.setReadTimeout(15000);
        connection.connect();
        int length = connection.getContentLength(); // filesize
        InputStream inputStream = connection.getInputStream();

        int current = 0;

//        jProgressBar1.setMaximum(length);
//        jProgressBar1.setValue(0);

        String tmpdir = System.getProperty("java.io.tmpdir");

        File file = new File(tmpdir, new File(remoteFile.getFile()).getName());
        FileOutputStream fos = new FileOutputStream(file);
        BufferedOutputStream bout = new BufferedOutputStream(fos); //create our output steam to build the file here

        byte[] buffer = new byte[1024];
        int bytesRead = 0;
        while ((bytesRead = inputStream.read(buffer, 0, 1024)) > -1) {
            bout.write(buffer, 0, bytesRead);
            //            current += bytesRead; //we've progressed a little so update current
//            jProgressBar1.setValue(current); //tell progress how far we are
        }

        bout.close();
        inputStream.close();
        return file;
    }

    private void jButtonCloseActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonCloseActionPerformed
        this.setVisible(false);
        this.jProgressBar1.setVisible(false);
    }//GEN-LAST:event_jButtonCloseActionPerformed

    private void formWindowOpened(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowOpened
        Object[] available = availableCodes.values().toArray();
        Arrays.sort(available);
        this.jList1.removeAll();
        DefaultListModel model = new DefaultListModel();
        for (Object str : available) {
            model.addElement(str);
        }

        this.jList1.setModel(model);
        this.jList1.setCellRenderer(new CustomCellRenderer(installedCodes));
    }//GEN-LAST:event_formWindowOpened

    private void formWindowActivated(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowActivated
        // TODO add your handling code here:
    }//GEN-LAST:event_formWindowActivated

    private void jButtonCancelActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonCancelActionPerformed
        // TODO add your handling code here:
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
    private javax.swing.JList jList1;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel jPanel3;
    private javax.swing.JProgressBar jProgressBar1;
    private javax.swing.JScrollPane jScrollPane1;
    // End of variables declaration//GEN-END:variables

    /**
     * @param availableCodes the availableCodes to set
     */
    public void setAvailableCodes(Properties availableCodes) {
        this.availableCodes = availableCodes;
    }

    /**
     * @param installedCodes the installedCodes to set
     */
    public void setInstalledCodes(String[] installedCodes) {
        this.installedCodes = installedCodes;
    }

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
