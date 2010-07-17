package net.sourceforge.vietocr;

import javax.swing.JOptionPane;

public class GuiWithImage extends GuiWithCommand {

    @Override
    void readImageMetadata() {
        if (iioImageList == null) {
            JOptionPane.showMessageDialog(this, "Please select an image.");
            return;
        }
        ImageDialog dialog = new ImageDialog(this, true);
        dialog.setImage(iioImageList.get(imageIndex));
        dialog.setVisible(true);
    }
}
