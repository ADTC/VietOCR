package net.sourceforge.vietocr;

import java.awt.event.*;
import java.util.Map;
import javax.imageio.IIOImage;
import javax.swing.*;

public class ImageInfoDialog extends javax.swing.JDialog {

    IIOImage oimage;
    boolean isProgrammatic;

    /** Creates new form ImageInfoDialog */
    public ImageInfoDialog(java.awt.Frame parent, boolean modal) {
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
        jLabelWidth = new javax.swing.JLabel();
        jLabelHeight = new javax.swing.JLabel();
        jLabelXRes = new javax.swing.JLabel();
        jLabelYRes = new javax.swing.JLabel();
        jTextFieldWidth = new javax.swing.JTextField();
        jTextFieldHeight = new javax.swing.JTextField();
        jTextFieldXRes = new javax.swing.JTextField();
        jTextFieldYRes = new javax.swing.JTextField();
        jLabel7 = new javax.swing.JLabel();
        jLabel8 = new javax.swing.JLabel();
        jComboBox1 = new javax.swing.JComboBox();
        jComboBox2 = new javax.swing.JComboBox();
        jPanel2 = new javax.swing.JPanel();
        jButtonOK = new javax.swing.JButton();
        jButtonCancel = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        java.util.ResourceBundle bundle = java.util.ResourceBundle.getBundle("net/sourceforge/vietocr/ImageInfoDialog"); // NOI18N
        setTitle(bundle.getString("this.Title")); // NOI18N
        setResizable(false);

        jPanel1.setBorder(javax.swing.BorderFactory.createEmptyBorder(20, 30, 10, 30));
        jPanel1.setLayout(new java.awt.GridBagLayout());

        jLabelWidth.setText(bundle.getString("jLabelWidth.Text")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        gridBagConstraints.insets = new java.awt.Insets(0, 0, 2, 0);
        jPanel1.add(jLabelWidth, gridBagConstraints);

        jLabelHeight.setText(bundle.getString("jLabelHeight.Text")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 0;
        gridBagConstraints.gridy = 1;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        gridBagConstraints.insets = new java.awt.Insets(0, 0, 2, 0);
        jPanel1.add(jLabelHeight, gridBagConstraints);

        jLabelXRes.setText(bundle.getString("jLabelXRes.Text")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 0;
        gridBagConstraints.gridy = 2;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        gridBagConstraints.insets = new java.awt.Insets(0, 0, 2, 0);
        jPanel1.add(jLabelXRes, gridBagConstraints);

        jLabelYRes.setText(bundle.getString("jLabelYRes.Text")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 0;
        gridBagConstraints.gridy = 3;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        jPanel1.add(jLabelYRes, gridBagConstraints);

        jTextFieldWidth.setEditable(false);
        jTextFieldWidth.setPreferredSize(new java.awt.Dimension(40, 20));
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        gridBagConstraints.insets = new java.awt.Insets(0, 5, 2, 5);
        jPanel1.add(jTextFieldWidth, gridBagConstraints);

        jTextFieldHeight.setEditable(false);
        jTextFieldHeight.setPreferredSize(new java.awt.Dimension(40, 20));
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridy = 1;
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        gridBagConstraints.insets = new java.awt.Insets(0, 5, 2, 5);
        jPanel1.add(jTextFieldHeight, gridBagConstraints);

        jTextFieldXRes.setEditable(false);
        jTextFieldXRes.setPreferredSize(new java.awt.Dimension(40, 20));
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridy = 2;
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        gridBagConstraints.insets = new java.awt.Insets(0, 5, 2, 5);
        jPanel1.add(jTextFieldXRes, gridBagConstraints);

        jTextFieldYRes.setEditable(false);
        jTextFieldYRes.setPreferredSize(new java.awt.Dimension(40, 20));
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridy = 3;
        gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
        gridBagConstraints.insets = new java.awt.Insets(0, 5, 0, 5);
        jPanel1.add(jTextFieldYRes, gridBagConstraints);

        jLabel7.setText(bundle.getString("DPI")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 2;
        gridBagConstraints.gridy = 2;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        gridBagConstraints.insets = new java.awt.Insets(0, 0, 2, 0);
        jPanel1.add(jLabel7, gridBagConstraints);

        jLabel8.setText(bundle.getString("DPI")); // NOI18N
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 2;
        gridBagConstraints.gridy = 3;
        gridBagConstraints.anchor = java.awt.GridBagConstraints.WEST;
        jPanel1.add(jLabel8, gridBagConstraints);

        jComboBox1.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "pixels", "inches", "cm" }));
        jComboBox1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jComboBox1ActionPerformed(evt);
            }
        });
        jPanel1.add(jComboBox1, new java.awt.GridBagConstraints());

        jComboBox2.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "pixels", "inches", "cm" }));
        jComboBox2.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jComboBox2ActionPerformed(evt);
            }
        });
        gridBagConstraints = new java.awt.GridBagConstraints();
        gridBagConstraints.gridx = 2;
        gridBagConstraints.gridy = 1;
        jPanel1.add(jComboBox2, gridBagConstraints);

        getContentPane().add(jPanel1, java.awt.BorderLayout.CENTER);

        jPanel2.setBorder(javax.swing.BorderFactory.createEmptyBorder(1, 1, 10, 1));

        jButtonOK.setText(bundle.getString("jButtonOK.Text")); // NOI18N
        jButtonOK.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonOKActionPerformed(evt);
            }
        });
        jPanel2.add(jButtonOK);

        jButtonCancel.setText(bundle.getString("jButtonCancel.Text")); // NOI18N
        jButtonCancel.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButtonCancelActionPerformed(evt);
            }
        });
        jPanel2.add(jButtonCancel);

        getContentPane().add(jPanel2, java.awt.BorderLayout.SOUTH);

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButtonOKActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonOKActionPerformed
        this.setVisible(false);
    }//GEN-LAST:event_jButtonOKActionPerformed

    private void jButtonCancelActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButtonCancelActionPerformed
        this.setVisible(false);
    }//GEN-LAST:event_jButtonCancelActionPerformed

    private void jComboBox1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jComboBox1ActionPerformed
        if (!isProgrammatic) {
            isProgrammatic = true;
            this.jComboBox2.setSelectedItem(this.jComboBox1.getSelectedItem());
            convertUnits(this.jComboBox1.getSelectedIndex());
            isProgrammatic = false;
        }
    }//GEN-LAST:event_jComboBox1ActionPerformed

    private void jComboBox2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jComboBox2ActionPerformed
        if (!isProgrammatic) {
            isProgrammatic = true;
            this.jComboBox1.setSelectedItem(this.jComboBox2.getSelectedItem());
            convertUnits(this.jComboBox2.getSelectedIndex());
            isProgrammatic = false;
        }
    }//GEN-LAST:event_jComboBox2ActionPerformed

    private void convertUnits(int unit) {
        int width = oimage.getRenderedImage().getWidth();
        int height = oimage.getRenderedImage().getHeight();

        switch (unit) {
            case 1: // "inches"
                this.jTextFieldWidth.setText(String.valueOf(round(width / Float.parseFloat(this.jTextFieldXRes.getText()), 1)));
                this.jTextFieldHeight.setText(String.valueOf(round(height / Float.parseFloat(this.jTextFieldYRes.getText()), 1)));
                break;

            case 2: // "cm"
                this.jTextFieldWidth.setText(String.valueOf(round(width / Float.parseFloat(this.jTextFieldXRes.getText()) * 2.54, 2)));
                this.jTextFieldHeight.setText(String.valueOf(round(height / Float.parseFloat(this.jTextFieldYRes.getText()) * 2.54, 2)));
                break;

            default: // "pixel"
                this.jTextFieldWidth.setText(String.valueOf(width));
                this.jTextFieldHeight.setText(String.valueOf(height));
                break;
        }
    }

    public void setImage(IIOImage oimage) {
        this.oimage = oimage;
        readImageData();
    }

    void readImageData() {
        this.jTextFieldWidth.setText(String.valueOf(oimage.getRenderedImage().getWidth()));
        this.jTextFieldHeight.setText(String.valueOf(oimage.getRenderedImage().getHeight()));
        Map<String, String> metadata = ImageIOHelper.readImageData(oimage);
        this.jTextFieldXRes.setText(metadata.get("dpiX"));
        this.jTextFieldYRes.setText(metadata.get("dpiY"));
    }

    public static double round(double d, int decimalPlace) {
//        BigDecimal bd = new BigDecimal(Double.toString(d));
//        bd = bd.setScale(decimalPlace, BigDecimal.ROUND_HALF_UP);
//        return bd.doubleValue();
        int temp = (int) (d * Math.pow(10, decimalPlace));
        return ((double) temp) / Math.pow(10, decimalPlace);
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        java.awt.EventQueue.invokeLater(new Runnable() {

            @Override
            public void run() {
                ImageInfoDialog dialog = new ImageInfoDialog(new javax.swing.JFrame(), true);
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
    private javax.swing.JButton jButtonOK;
    private javax.swing.JComboBox jComboBox1;
    private javax.swing.JComboBox jComboBox2;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JLabel jLabelHeight;
    private javax.swing.JLabel jLabelWidth;
    private javax.swing.JLabel jLabelXRes;
    private javax.swing.JLabel jLabelYRes;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JTextField jTextFieldHeight;
    private javax.swing.JTextField jTextFieldWidth;
    private javax.swing.JTextField jTextFieldXRes;
    private javax.swing.JTextField jTextFieldYRes;
    // End of variables declaration//GEN-END:variables
}
