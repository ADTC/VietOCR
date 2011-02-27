package net.sourceforge.vietocr;

import com.apple.eawt.*;
import com.apple.eawt.AppEvent.*;

import java.awt.Color;
import java.awt.Rectangle;
import java.io.File;

import javax.swing.BorderFactory;
import javax.swing.JScrollPane;

/**
 *  Mac OS X functionality for VietOCR.
 *
 *@author     Quan Nguyen
 *@modified   February 27, 2011
 */
class MacOSXApplication {

    private final static int ZOOM_LIMIT = 60;
    // http://www.mactech.com/articles/develop/issue_17/Yu_final.html

    Application app = null;

    /**
     *  Constructor for the MacOSXApplication object.
     *
     *@param  vietPad  calling instance of VietPad
     */
    public MacOSXApplication(final GuiWithTools vietOCR) {
        app = Application.getApplication();

//        vietOCR.setMaximizedBounds(new Rectangle(
//                Math.max(vietOCR.getWidth(), vietOCR.m_font.getSize() * ZOOM_LIMIT),
//                Integer.MAX_VALUE));
//        vietOCR.scrollPane.setHorizontalScrollBarPolicy(JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
//        vietOCR.scrollPane.setBorder(null); // line up scrollbars with grow box
//        vietOCR.m_toolBar.setBorder(BorderFactory.createCompoundBorder(
//                BorderFactory.createMatteBorder(0, 0, 1, 0, new Color(0.5765F, 0.5765F, 0.5765F)),
//                vietOCR.m_toolBar.getBorder()));
        
//        app.setDefaultMenuBar(vietPad.getJMenuBar());
        app.setAboutHandler(new AboutHandler() {

            @Override
            public void handleAbout(AboutEvent ae) {
//                vietOCR.about();
            }
        });

        app.setOpenFileHandler(new OpenFilesHandler() {

            @Override
            public void openFiles(OpenFilesEvent ofe) {
                File droppedFile = ofe.getFiles().get(0);
                if (droppedFile.isFile() && vietOCR.promptToSave()) {
//                    vietOCR.openDocument(droppedFile);
                }
            }
        });

        app.setPreferencesHandler(new PreferencesHandler() {

            @Override
            public void handlePreferences(PreferencesEvent pe) {
//                vietOCR.preferences();
            }
        });

        app.setQuitHandler(new QuitHandler() {

            @Override
            public void handleQuitRequestWith(QuitEvent qe, QuitResponse qr) {
                vietOCR.quit();
            }
        });
    }
}
