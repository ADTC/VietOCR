package net.sourceforge.vietocr;

import javax.swing.JOptionPane;

public class GuiWithImage extends GuiWithCommand {

    @Override
    void readImageMetadata() {
        if (iioImageList == null) {
            JOptionPane.showMessageDialog(this, "Please load an image.");
            return;
        }
        
        ImageInfoDialog dialog = new ImageInfoDialog(this, true);
        dialog.setImage(iioImageList.get(imageIndex));
        dialog.setVisible(true);
    }
}
