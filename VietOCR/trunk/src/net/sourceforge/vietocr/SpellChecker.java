/**
 * Copyright @ 2010 Quan Nguyen
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

import java.awt.Color;
//import java.util.logging.*;
import java.util.regex.*;
import javax.swing.JTextArea;
import javax.swing.text.*;

public class SpellChecker {

    JTextArea ta;

    SpellChecker(JTextArea ta) {
        this.ta = ta;
    }

    void spellCheck() {
        Highlighter hi = ta.getHighlighter();
        hi.removeAllHighlights();

        String[] incorrectWords = {"tunn", "doh", "emm"}; // results of a spellchecker to be implemented

        StringBuilder sb = new StringBuilder();
        for (String word : incorrectWords) {
            sb.append(word).append("|");
        }
        sb.setLength(sb.length() - 1); //remove last |
        String patternStr = "\\b(" + sb.toString() + ")\\b";

        Pattern pattern = Pattern.compile(patternStr, Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(ta.getText());

        // define the highlighter
        Highlighter.HighlightPainter myPainter = new DefaultHighlighter.DefaultHighlightPainter(Color.decode("#f0e0d0"));

        while (matcher.find()) {
            try {
                hi.addHighlight(matcher.start(), matcher.end(), myPainter);
            } catch (BadLocationException ex) {
//                Logger.getLogger(Gui.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
}
