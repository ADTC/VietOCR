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

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.ButtonGroup;
import javax.swing.JRadioButtonMenuItem;

public class GuiWithPSM extends GuiWithSettings {

    String psModes = "0 - Orientation and script detection (OSD) only;"
            + "1 - Automatic page segmentation with OSD;"
            + "2 - Automatic page segmentation, but no OSD, or OCR;"
            + "3 - Fully automatic page segmentation, but no OSD (default);"
            + "4 - Assume a single column of text of variable sizes;"
            + "5 - Assume a single uniform block of vertically aligned text;"
            + "6 - Assume a single uniform block of text;"
            + "7 - Treat the image as a single text line;"
            + "8 - Treat the image as a single word;"
            + "9 - Treat the image as a single word in a circle;"
            + "10 - Treat the image as a single character";

    public GuiWithPSM() {
        currentPSM = prefs.get("psm", "3");
        
        ButtonGroup groupPSM = new ButtonGroup();
        ActionListener psmLst = new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent ae) {
                currentPSM = ae.getActionCommand();
            }
        };

        // build PSM submenu

        for (String mode : psModes.split(";")) {
            JRadioButtonMenuItem radioItem = new JRadioButtonMenuItem(mode, mode.startsWith(currentPSM));
            radioItem.addActionListener(psmLst);
            groupPSM.add(radioItem);
            this.jMenuPSM.add(radioItem);
        }
    }
}
