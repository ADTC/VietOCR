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

import com.stibocatalog.hunspell.Hunspell;
import java.awt.Color;
//import java.util.logging.*;
import java.io.File;
import java.text.BreakIterator;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.*;
import javax.swing.JOptionPane;
import javax.swing.JTextArea;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import javax.swing.text.*;

public class SpellChecker {

    JTextArea ta;
    SpellcheckDocumentListener docLisener = new SpellcheckDocumentListener();
    // define the highlighter
    Highlighter.HighlightPainter myPainter = new WavyLineHighlighter(Color.red);
    String locale;

    public SpellChecker(JTextArea ta, String locale) {
        this.ta = ta;
        this.locale = locale;
    }

    public void enableSpellCheck() {
        this.ta.getDocument().addDocumentListener(docLisener);
        spellCheck();
    }

    void spellCheck() {
        List<String> words = parseText(ta.getText());
        List<String> misspelledWords = spellCheck(words); // results of a spellchecker to be implemented
        if (misspelledWords.isEmpty()) {
            return; // perfect writer!
        }

        StringBuilder sb = new StringBuilder();
        for (String word : misspelledWords) {
            sb.append(word).append("|");
        }
        sb.setLength(sb.length() - 1); //remove last |

        // build regex
        String patternStr = "\\b(" + sb.toString() + ")\\b";

        Pattern pattern = Pattern.compile(patternStr, Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(ta.getText());

        Highlighter hi = ta.getHighlighter();
        hi.removeAllHighlights();

        while (matcher.find()) {
            try {
                hi.addHighlight(matcher.start(), matcher.end(), myPainter);
            } catch (BadLocationException ex) {
//                Logger.getLogger(Gui.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    List<String> spellCheck(List<String> words) {
        List<String> misspelled = new ArrayList<String>();
        File baseDir = Utilities.getBaseDir(SpellChecker.this);
        try {
            Hunspell.Dictionary spellDict = Hunspell.getInstance().getDictionary(baseDir.getPath() + "/dict/" + locale);

            for (String word : words) {
                if (spellDict.misspelled(word)) {
                    misspelled.add(word);
                }
            }
        } catch (Exception e) {
            System.err.println(e.getMessage());
            JOptionPane.showMessageDialog(null, "The appropriate dictionary files were not found in " + new File(baseDir, "dict").getPath() + " directory.", Gui.APP_NAME, JOptionPane.ERROR_MESSAGE);
        }
        return misspelled;
    }

    List<String> parseText(String text) {
        List<String> words = new ArrayList<String>();
        BreakIterator boundary = BreakIterator.getWordInstance();
        boundary.setText(text);
        int start = boundary.first();
        for (int end = boundary.next(); end != BreakIterator.DONE; start = end, end = boundary.next()) {
            if (!Character.isLetter(text.charAt(start))) {
                continue;
            }
            words.add(text.substring(start, end));
        }

        return words;
    }

    public void disableSpellCheck() {
        this.ta.getDocument().removeDocumentListener(docLisener);
        this.ta.getHighlighter().removeAllHighlights();
    }

    class SpellcheckDocumentListener implements DocumentListener {

        @Override
        public void removeUpdate(DocumentEvent e) {
            spellCheck();
        }

        @Override
        public void insertUpdate(DocumentEvent e) {
            spellCheck();
        }

        @Override
        public void changedUpdate(DocumentEvent e) {
        }
    }
}
