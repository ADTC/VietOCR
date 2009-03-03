/*
 * ChangeCaseDialog.java
 *
 * @author  Quan Nguyen
 * @created on May 30, 2003, 11:06 AM
 * @version 1.1, 16 October 03
 */

package net.sourceforge.vietpad;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.EmptyBorder;
import java.util.*;
import net.sourceforge.vietocr.*;

public class ChangeCaseDialog extends JDialog {
    
    // Variables declaration - do not modify
    private ButtonGroup buttonGroup;
    private JButton jButtonChangeCase;
    private JButton jButtonClose;
    private JPanel jPanel1;
    private JPanel jPanel2;
    private JRadioButton jRadioButton;
    private String selectedCase;
    private ResourceBundle myResources = ResourceBundle.getBundle("net.sourceforge.vietpad.Resources");

    public ChangeCaseDialog(Frame owner, boolean modal) {
        super(owner, modal);              
        setLocale(owner.getLocale());     
        setResizable(false);
        initComponents();
       
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
    
    /**
     * initialize components
     */
    private void initComponents() {
        jPanel1 = new JPanel();
        jPanel2 = new JPanel();
        jButtonChangeCase = new JButton(myResources.getString("Change"));
        jButtonClose = new JButton(myResources.getString("Close"));
        setTitle(myResources.getString("Change_Case"));
        addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(java.awt.event.WindowEvent evt) {
                setVisible(false);
            }
        });

        jPanel1.setLayout(new GridLayout(0, 1));

        ActionListener cslst =
            new ActionListener() {
            @Override
                public void actionPerformed(ActionEvent ae) {
                    selectedCase = ae.getActionCommand();
                }
            };
        jPanel1.setBorder(new EmptyBorder(new Insets(17, 17, 17, 17)));
        jPanel2.setBorder(new EmptyBorder(new Insets(17,  0, 17, 17)));
        
        buttonGroup = new ButtonGroup();
        String[] cases = {"Sentence_case", "lowercase", "UPPERCASE", "Title_Case"};
                                
        // add radiobuttons to panel and button group
        for (int i = 0; i < cases.length; i++) {
            jRadioButton = new JRadioButton(myResources.getString(cases[i]));
            jRadioButton.setActionCommand(cases[i]);            
            jRadioButton.addActionListener(cslst);
            buttonGroup.add(jRadioButton);
            jPanel1.add(jRadioButton);            
        }      
        
        getRootPane().setDefaultButton(jButtonChangeCase);
                
        getContentPane().add(jPanel1);
        jButtonChangeCase.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent evt) {
                final GuiWithFormat frame = (GuiWithFormat) getOwner();

                getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
                getGlassPane().setVisible(true);
                frame.getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
                frame.getGlassPane().setVisible(true);

                try {
                    frame.changeCase(selectedCase);
                } catch (OutOfMemoryError e) {
                    e.printStackTrace();
                    JOptionPane.showMessageDialog(frame, Gui.APP_NAME
                             + myResources.getString("_has_run_out_of_memory.\nPlease_restart_") + Gui.APP_NAME
                             + myResources.getString("_and_try_again."), myResources.getString("Out_of_Memory"), JOptionPane.ERROR_MESSAGE);
                } finally {
                    SwingUtilities.invokeLater(
                        new Runnable() {
                        @Override
                            public void run() {
                                frame.getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
                                frame.getGlassPane().setVisible(false);
                                getGlassPane().setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
                                getGlassPane().setVisible(false);
                                getRootPane().setDefaultButton(jButtonClose);
                            }
                        });
                }   
            }
        });

        jPanel2.setLayout(new BoxLayout(jPanel2, BoxLayout.Y_AXIS));
        Dimension size = jButtonChangeCase.getMaximumSize();
        size.width = Short.MAX_VALUE;
        jButtonChangeCase.setMaximumSize(size);         
        jPanel2.add(jButtonChangeCase);
        jPanel2.add(Box.createVerticalStrut(3));

        jButtonClose.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent evt) {
                setVisible(false);
            }
        });
        size = jButtonClose.getMaximumSize();
        size.width = Short.MAX_VALUE;
        jButtonClose.setMaximumSize(size); 
        jPanel2.add(jButtonClose);
        jPanel2.add(Box.createVerticalGlue());
        
        getContentPane().add(jPanel2, BorderLayout.EAST);

        pack();
        setLocationRelativeTo(getOwner());
    }

    /**
     *  Sets the selected case
     *
     *@param  String selectedCase
     */
    public void setSelectedCase(String selectedCase) {
        this.selectedCase = selectedCase;
        
        for (Enumeration e = buttonGroup.getElements(); e.hasMoreElements();) {
            JRadioButton bt = (JRadioButton) e.nextElement();
            if (bt.getActionCommand().equals(selectedCase)) {
                bt.setSelected(true);
                break;
            }
        }

    }

    /**
     *  Gets the selected case
     *
     *@return    String selectedCase
     */
    String getSelectedCase() {
        return selectedCase;
    }
    
    /**
     *  Shows and hides the dialog
     */
    @Override
    public void setVisible(final boolean flag) {
        if (flag) {
            super.setVisible(true);
            // switch default button twice to make it pulse in Mac OS X
            getRootPane().setDefaultButton(jButtonClose);
            getRootPane().setDefaultButton(jButtonChangeCase);
            // send to back and front to get focus in Metal and CDE/Motif
            toBack();
            toFront();
            jPanel1.requestFocus();
        } else {
            // switch default button to make it appear immediately upon re-opening
            getRootPane().setDefaultButton(jButtonChangeCase);
            jPanel2.paintImmediately(jPanel2.getBounds());
            super.setVisible(false);
        }
    }   
}
