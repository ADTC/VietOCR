/**
 * Copyright @ 2014 Quan Nguyen
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */
package net.sourceforge.vietocr;

import java.awt.Component;
import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.util.List;
import javax.swing.AbstractAction;
import static javax.swing.Action.LARGE_ICON_KEY;
import static javax.swing.Action.SHORT_DESCRIPTION;
import javax.swing.Box;
import javax.swing.ButtonGroup;
import javax.swing.Icon;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JToggleButton;
import javax.swing.SwingWorker;

import net.sourceforge.vietocr.components.JImageLabel;

/**
 * Adapted from Java Tutorial's IconDemoApp example.
 * 
 */
public class GuiWithThumbnail extends Gui {

    String page = "Page ";

    @Override
    void loadThumbnails() {
//        jPanelThumb.removeAll();
        loadImages.execute();
    }

    /**
     * SwingWorker class that loads the images a background thread and calls
     * publish when a new one is ready to be displayed.
     */
    private final SwingWorker<Void, ThumbnailAction> loadImages = new SwingWorker<Void, ThumbnailAction>() {

        /**
         * Creates thumbnail versions of the target image files.
         */
        @Override
        protected Void doInBackground() throws Exception {
            for (int i = 0; i < imageList.size(); i++) {
                ImageIcon thumbnailIcon = new ImageIcon(imageList.get(i).getScaledImage(52, 72));
                ThumbnailAction thumbAction = new ThumbnailAction(thumbnailIcon, i, page + i);
                publish(thumbAction);
            }
            return null;
        }

        /**
         * Process all loaded images.
         */
        @Override
        protected void process(List<ThumbnailAction> chunks) {
            ButtonGroup group = new ButtonGroup();
            for (ThumbnailAction thumbAction : chunks) {
                JToggleButton thumbButton = new JToggleButton(thumbAction);
                thumbButton.setAlignmentX(Component.CENTER_ALIGNMENT);
                // add the new button BEFORE the last glue
                // this centers the buttons in the toolbar
                jPanelThumb.add(Box.createRigidArea((new Dimension(0,5))));
                jPanelThumb.add(thumbButton);
                group.add(thumbButton);
                JLabel label = new JLabel(String.valueOf(thumbAction.getIndex() + 1));
                label.setAlignmentX(Component.CENTER_ALIGNMENT);
                jPanelThumb.add(label);
            }
            jPanelThumb.revalidate();
            jPanelThumb.repaint();
            jPanelThumb.validate();
        }
    };

    /**
     * Action class that shows the image specified.
     */
    private class ThumbnailAction extends AbstractAction {
        int index;

        /**
         * @param Icon - The thumbnail to show in the button.
         * @param String - The description of the icon.
         */
        public ThumbnailAction(Icon thumb, int index, String desc) {
            this.index = index;

            // The short description becomes the tooltip of a button.
            putValue(SHORT_DESCRIPTION, desc);

            // The LARGE_ICON_KEY is the key for setting the
            // icon when an Action is applied to a button.
            putValue(LARGE_ICON_KEY, thumb);
        }

        public int getIndex() {
            return index;
        }

        /**
         * Shows the full image in the image panel.
         */
        @Override
        public void actionPerformed(ActionEvent e) {
            if (imageIndex == index) {
                return;
            }
            imageIndex = index;
            ((JImageLabel) jImageLabel).deselect();
            jLabelStatus.setText(null);
            jProgressBar1.setString(null);
            jProgressBar1.setVisible(false);
            displayImage();
            clearStack();
            setButton();
        }
    }
}
