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

import java.awt.Rectangle;
import java.awt.image.BufferedImage;
import java.awt.image.RasterFormatException;
import java.util.ArrayList;
import javax.imageio.IIOImage;
import javax.swing.ImageIcon;
import javax.swing.JOptionPane;

public class GuiWithCommand extends Gui {

    void jMenuItemOCRActionPerformed(java.awt.event.ActionEvent evt) {
        if (jImageLabel.getIcon() == null) {
            JOptionPane.showMessageDialog(this, bundle.getString("Please_load_an_image."), APP_NAME, JOptionPane.INFORMATION_MESSAGE);
            return;
        }

        Rectangle rect = ((JImageLabel) jImageLabel).getRect();

        if (rect != null) {
            try {
                ImageIcon ii = (ImageIcon) this.jImageLabel.getIcon();
                BufferedImage bi = ((BufferedImage) ii.getImage()).getSubimage((int) (rect.x * scaleX), (int) (rect.y * scaleY), (int) (rect.width * scaleX), (int) (rect.height * scaleY));
                IIOImage iioImage = new IIOImage(bi, null, null);
                ArrayList<IIOImage> tempList = new ArrayList<IIOImage>();
                tempList.add(iioImage);
                performOCR(tempList, 0);
            } catch (RasterFormatException rfe) {
                JOptionPane.showMessageDialog(this, rfe.getMessage(), APP_NAME, JOptionPane.ERROR_MESSAGE);
//                rfe.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else {
            performOCR(iioImageList, imageIndex);
        }
    }
}
