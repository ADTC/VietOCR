/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/*
 * OptionsDialog.java
 *
 * Created on Jul 9, 2009, 9:51:19 PM
 */
package net.sourceforge.vietocr;

import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;
import java.io.File;
import java.util.Locale;
import java.util.ResourceBundle;
import javax.swing.AbstractAction;
import javax.swing.Action;
import javax.swing.JComponent;
import javax.swing.JFileChooser;
import javax.swing.JOptionPane;
import javax.swing.KeyStroke;
import javax.swing.SwingUtilities;

/**
 *
 * @author Quan
 */
public class OptionsDialog extends javax.swing.JDialog {

    private int actionSelected = -1;
    private String watchFolder;
    private String outputFolder;
    private String tessPath;
    private String dangAmbigsPath;
    private String curLangCode;
    private boolean watchEnabled;
    private boolean dangAmbigsOn;
    
    protected ResourceBundle bundle;

    /** Creates new form OptionsDialog */
    public OptionsDialog(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        initComponents();

        bundle = ResourceBundle.getBundle("net/sourceforge/vietocr/OptionsDialog");

        this.setLocationRelativeTo(parent);
        getRootPane().setDefaultButton(jButtonOK);

        //  Handle escape key to hide the dialog
        KeyStroke escapeKeyStroke = KeyStroke.getKeyStroke(KeyEvent.VK_ESCAPE, 0, false);
        Action escapeAction = new AbstractAction() {

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
        jButtonOK = new javax.swing.JButton();
        jButtonCancel = new javax.swing.JButton();
        jTabbedPane1 = new javax.swing.JTabbedPane();
        jPanelWatchFolder = new javax.swing.JPanel();
        jLabelWatch = new javax.swing.JLabel();
        jTextFieldWatch = new javax.swing.JTextField();
        jLabelOutput = new javax.swing.JLabel();
        jTextFieldOutput = new javax.swing.JTextField();
        jCheckBoxWatch = new javax.swing.JCheckBox();
        jButtonWatch = new javax.swing.JButton();
        jButtonOutput = new javax.swing.JButton();
        jPanelTessPath = new javax.swing.JPanel();
        jLabelTess = new javax.swing.JLabel();
        jTextFieldTess = new javax.swing.JTextField();
        jButtonTess = new javax.swing.JButton();
        jPanelDangAmbigsPath = new javax.swing.JPanel();
        jLabelDangAmbigs = new javax.swing.JLabel();
        jButtonDangAmbigs = new javax.swing.JButton();
        jTextFieldDangAmbigs = new javax.swing.JTextField();
        jCheckBoxDangAmbigs = new javax.swing.JCheckBox();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        java.util.ResourceBundle bundle = java.util.ResourceBundle.getBundle("net/sourceforge/vietocr/OptionsDialog"); // NOI18N
        setTitle(bundle.getString("this.Title")); // NOI18N
        setResizable(false);
        addWindowListener(new java.awt.event.WindowAdapter() {
            public void windowActivated(java.awt.event.WindowEvent evt) {
                formWindowActivated(evt);
            }
            public void windowDeactivated(java.awt.event.WindowEvent evt) {
                formWindowDeactivated(evt);
            }
        });

        jPanel1.setLayout(new java.awt.FlowLayout(java.awt.FlowLayout.RIGHT));

        jButtonOK.setText(bundle.getString("jButtonOK.Text")); // NOI18N
        jButtonOK.setToolTipText(bundle.getString("jButtonOK.ToolTipText")); // NOI18N
        jButtonOK.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonOKActionPerformed(evt);
            }
        });
        jPanel1.add(jButtonOK);

        jButtonCancel.setText(bundle.getString("jButtonCancel.Text")); // NOI18N
        jButtonCancel.setToolTipText(bundle.getString("jButtonCancel.ToolTipText")); // NOI18N
        jButtonCancel.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonCancelActionPerformed(evt);
            }
        });
        jPanel1.add(jButtonCancel);

        getContentPane().add(jPanel1, java.awt.BorderLayout.SOUTH);

        jPanelWatchFolder.setLayout(new java.awt.GridBagLayout());

        jLabelWatch.setText(bundle.getString("jLabelWatch.Text")); // NOI18N
        jPanelWatchFolder.add(jLabelWatch, new java.awt.GridBagConstraints());

        jTextFieldWatch.setEditable(false);
        jTextFieldWatch.setPreferredSize(new java.awt.Dimension(200, 20));
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 1;
        gridBagConstraints.gridy = 0;
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        gridBagConstraints.insets = new java.awt.Insets(0, 3, 0, 3);
        jPanelWatchFolder.add(jTextFieldWatch, gridBagConstraints);

        jLabelOutput.setText(bundle.getString("jLabelOutput.Text")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 0;
        gridBagConstraints.gridy = 1;
        jPanelWatchFolder.add(jLabelOutput, gridBagConstraints);

        jTextFieldOutput.setEditable(false);
        jTextFieldOutput.setPreferredSize(new java.awt.Dimension(200, 20));
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 1;
        gridBagConstraints.gridy = 1;
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        gridBagConstraints.insets = new java.awt.Insets(0, 3, 0, 3);
        jPanelWatchFolder.add(jTextFieldOutput, gridBagConstraints);

        jCheckBoxWatch.setText(bundle.getString("jCheckBoxWatch.Text")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 1;
        gridBagConstraints.gridy = 2;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        jPanelWatchFolder.add(jCheckBoxWatch, gridBagConstraints);

        jButtonWatch.setText("...");
        jButtonWatch.setToolTipText("Browse");
        jButtonWatch.setPreferredSize(new java.awt.Dimension(30, 23));
        jButtonWatch.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonWatchActionPerformed(evt);
            }
        });
        jPanelWatchFolder.add(jButtonWatch, new java.awt.GridBagConstraints());

        jButtonOutput.setText("...");
        jButtonOutput.setToolTipText("Browse");
        jButtonOutput.setPreferredSize(new java.awt.Dimension(30, 23));
        jButtonOutput.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonOutputActionPerformed(evt);
            }
        });
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 2;
        gridBagConstraints.gridy = 1;
        jPanelWatchFolder.add(jButtonOutput, gridBagConstraints);

        jTabbedPane1.addTab("Watch", jPanelWatchFolder);

        jLabelTess.setText(bundle.getString("jLabelTess.Text")); // NOI18N
        jPanelTessPath.add(jLabelTess);

        jTextFieldTess.setEditable(false);
        jTextFieldTess.setPreferredSize(new java.awt.Dimension(200, 20));
        jPanelTessPath.add(jTextFieldTess);

        jButtonTess.setText("...");
        jButtonTess.setToolTipText("Browse");
        jButtonTess.setPreferredSize(new java.awt.Dimension(30, 23));
        jButtonTess.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonTessActionPerformed(evt);
            }
        });
        jPanelTessPath.add(jButtonTess);

        jTabbedPane1.addTab("Tesseract", jPanelTessPath);

        jPanelDangAmbigsPath.setLayout(new java.awt.GridBagLayout());

        jLabelDangAmbigs.setText(bundle.getString("jLabelDangAmbigs.Text")); // NOI18N
        jPanelDangAmbigsPath.add(jLabelDangAmbigs, new java.awt.GridBagConstraints());

        jButtonDangAmbigs.setText("...");
        jButtonDangAmbigs.setToolTipText("Browse");
        jButtonDangAmbigs.setPreferredSize(new java.awt.Dimension(30, 23));
        jButtonDangAmbigs.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonDangAmbigsActionPerformed(evt);
            }
        });
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 2;
        gridBagConstraints.gridy = 0;
        jPanelDangAmbigsPath.add(jButtonDangAmbigs, gridBagConstraints);

        jTextFieldDangAmbigs.setEditable(false);
        jTextFieldDangAmbigs.setPreferredSize(new java.awt.Dimension(200, 20));
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 1;
        gridBagConstraints.gridy = 0;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        gridBagConstraints.insets = new java.awt.Insets(0, 3, 0, 3);
        jPanelDangAmbigsPath.add(jTextFieldDangAmbigs, gridBagConstraints);

        jCheckBoxDangAmbigs.setSelected(true);
        jCheckBoxDangAmbigs.setText(bundle.getString("jCheckBoxDangAmbigs.Text")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 1;
        gridBagConstraints.gridy = 1;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        jPanelDangAmbigsPath.add(jCheckBoxDangAmbigs, gridBagConstraints);

        jTabbedPane1.addTab("DangAmbigs.txt", jPanelDangAmbigsPath);

        getContentPane().add(jTabbedPane1, java.awt.BorderLayout.CENTER);
        jTabbedPane1.getAccessibleContext().setAccessibleName("Watch");

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButtonWatchActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonWatchActionPerformed
        // TODO add your handling code here:
        JFileChooser filechooser = new JFileChooser();
        filechooser.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        filechooser.setAcceptAllFileFilterUsed(false);
        filechooser.setApproveButtonText(bundle.getString("Set"));
        filechooser.setCurrentDirectory(new File(watchFolder));
        filechooser.setDialogTitle(bundle.getString("Set_Watch_Folder"));
        if (filechooser.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
            watchFolder = filechooser.getSelectedFile().getPath();
            this.jTextFieldWatch.setText(watchFolder);
        }
    }//GEN-LAST:event_jButtonWatchActionPerformed

    private void jButtonOutputActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonOutputActionPerformed
        // TODO add your handling code here:
        JFileChooser filechooser = new JFileChooser();
        filechooser.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        filechooser.setAcceptAllFileFilterUsed(false);
        filechooser.setApproveButtonText(bundle.getString("Set"));
        filechooser.setCurrentDirectory(new File(outputFolder));
        filechooser.setDialogTitle(bundle.getString("Set_Output_Folder"));
        if (filechooser.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
            outputFolder = filechooser.getSelectedFile().getPath();
            this.jTextFieldOutput.setText(outputFolder);
        }
    }//GEN-LAST:event_jButtonOutputActionPerformed

    private void jButtonTessActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonTessActionPerformed
        // TODO add your handling code here:
        JFileChooser pathchooser = new JFileChooser(tessPath);
        pathchooser.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        pathchooser.setAcceptAllFileFilterUsed(false);
        pathchooser.setApproveButtonText(bundle.getString("Set"));
        pathchooser.setDialogTitle(bundle.getString("Locate_Tesseract_Directory"));
        int returnVal = pathchooser.showOpenDialog(this);
        if (returnVal == JFileChooser.APPROVE_OPTION) {
            if (!tessPath.equals(pathchooser.getSelectedFile().getPath())) {
                setTessPath(pathchooser.getSelectedFile().getPath());
            }
        }
    }//GEN-LAST:event_jButtonTessActionPerformed

    private void jButtonDangAmbigsActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonDangAmbigsActionPerformed
        // TODO add your handling code here:
        JFileChooser pathchooser = new JFileChooser(dangAmbigsPath);
        pathchooser.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        pathchooser.setAcceptAllFileFilterUsed(false);
        pathchooser.setApproveButtonText(bundle.getString("Set"));
        pathchooser.setDialogTitle(bundle.getString("Path_to") + " " + curLangCode + ".DangAmbigs.txt");
        int returnVal = pathchooser.showOpenDialog(this);
        if (returnVal == JFileChooser.APPROVE_OPTION) {
            if (!dangAmbigsPath.equals(pathchooser.getSelectedFile().getPath())) {
                setDangAmbigsPath(pathchooser.getSelectedFile().getPath());
            }
        }
    }//GEN-LAST:event_jButtonDangAmbigsActionPerformed

    private void jButtonOKActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonOKActionPerformed
        actionSelected = JOptionPane.OK_OPTION;
        this.setVisible(false);
    }//GEN-LAST:event_jButtonOKActionPerformed

    private void jButtonCancelActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonCancelActionPerformed
        actionSelected = JOptionPane.CANCEL_OPTION;
        this.setVisible(false);
    }//GEN-LAST:event_jButtonCancelActionPerformed

    private void formWindowActivated(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowActivated
        // TODO add your handling code here:
        this.jTextFieldWatch.setText(watchFolder);
        this.jTextFieldOutput.setText(outputFolder);
        this.jCheckBoxWatch.setSelected(watchEnabled);
        this.jTextFieldTess.setText(tessPath);
        this.jTextFieldDangAmbigs.setText(dangAmbigsPath);
        this.jCheckBoxDangAmbigs.setSelected(dangAmbigsOn);
    }//GEN-LAST:event_formWindowActivated

    private void formWindowDeactivated(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowDeactivated
        // TODO add your handling code here:
        watchFolder = this.jTextFieldWatch.getText();
        outputFolder = this.jTextFieldOutput.getText();
        watchEnabled = this.jCheckBoxWatch.isSelected();
        tessPath = this.jTextFieldTess.getText();
        dangAmbigsPath = this.jTextFieldDangAmbigs.getText();
        dangAmbigsOn = this.jCheckBoxDangAmbigs.isSelected();
    }//GEN-LAST:event_formWindowDeactivated

    public int showDialog() {
        setVisible(true);
        return actionSelected;
    }

    void changeUILanguage(final Locale locale) {
        Locale.setDefault(locale);
        bundle = ResourceBundle.getBundle("net/sourceforge/vietocr/OptionsDialog");

        SwingUtilities.invokeLater(new Runnable() {

            @Override
            public void run() {
                FormLocalizer localizer = new FormLocalizer(OptionsDialog.this, OptionsDialog.class);
                localizer.ApplyCulture(bundle);
            }
        });
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        java.awt.EventQueue.invokeLater(new Runnable() {

            @Override
            public void run() {
                OptionsDialog dialog = new OptionsDialog(new javax.swing.JFrame(), true);
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
    private javax.swing.JButton jButtonDangAmbigs;
    private javax.swing.JButton jButtonOK;
    private javax.swing.JButton jButtonOutput;
    private javax.swing.JButton jButtonTess;
    private javax.swing.JButton jButtonWatch;
    private javax.swing.JCheckBox jCheckBoxDangAmbigs;
    private javax.swing.JCheckBox jCheckBoxWatch;
    private javax.swing.JLabel jLabelDangAmbigs;
    private javax.swing.JLabel jLabelOutput;
    private javax.swing.JLabel jLabelTess;
    private javax.swing.JLabel jLabelWatch;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanelDangAmbigsPath;
    private javax.swing.JPanel jPanelTessPath;
    private javax.swing.JPanel jPanelWatchFolder;
    private javax.swing.JTabbedPane jTabbedPane1;
    private javax.swing.JTextField jTextFieldDangAmbigs;
    private javax.swing.JTextField jTextFieldOutput;
    private javax.swing.JTextField jTextFieldTess;
    private javax.swing.JTextField jTextFieldWatch;
    // End of variables declaration//GEN-END:variables

    /**
     * @return the watchFolder
     */
    public String getWatchFolder() {
        return watchFolder;
    }

    /**
     * @param watchFolder the watchFolder to set
     */
    public void setWatchFolder(String watchFolder) {
        this.watchFolder = watchFolder;
    }

    /**
     * @return the outputFolder
     */
    public String getOutputFolder() {
        return outputFolder;
    }

    /**
     * @param outputFolder the outputFolder to set
     */
    public void setOutputFolder(String outputFolder) {
        this.outputFolder = outputFolder;
    }

    /**
     * @return the watchEnabled
     */
    public boolean isWatchEnabled() {
        watchEnabled = jCheckBoxWatch.isSelected();
        return watchEnabled;
    }

    /**
     * @param watchEnabled the watchEnabled to set
     */
    public void setWatchEnabled(boolean watchEnabled) {
        this.watchEnabled = watchEnabled;
    }

    /**
     * @return the tessPath
     */
    public String getTessPath() {
        return tessPath;
    }

    /**
     * @param tessPath the tessPath to set
     */
    public void setTessPath(String tessPath) {
        this.tessPath = tessPath;
    }

    /**
     * @return the dangAmbigsPath
     */
    public String getDangAmbigsPath() {
        return dangAmbigsPath;
    }

    /**
     * @param dangAmbigsPath the dangAmbigsPath to set
     */
    public void setDangAmbigsPath(String dangAmbigsPath) {
        this.dangAmbigsPath = dangAmbigsPath;
    }

    /**
     * @param curLangCode the curLangCode to set
     */
    public void setCurLangCode(String curLangCode) {
        this.curLangCode = curLangCode;
    }

    /**
     * @return the dangAmbigsOn
     */
    public boolean isDangAmbigsEnabled() {
        dangAmbigsOn = this.jCheckBoxDangAmbigs.isSelected();
        return dangAmbigsOn;
    }

    /**
     * @param dangAmbigsOn the dangAmbigsOn to set
     */
    public void setDangAmbigsEnabled(boolean dangAmbigsOn) {
        this.dangAmbigsOn = dangAmbigsOn;
    }
}
