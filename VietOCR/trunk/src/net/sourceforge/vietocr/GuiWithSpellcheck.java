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

import java.awt.Font;
import java.awt.Point;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;
import javax.swing.JMenuItem;
import javax.swing.text.BadLocationException;

public class GuiWithSpellcheck extends GuiWithSettings {

    private int start, end;
    private SpellChecker sp;

    @Override
    void populatePopupMenuWithSuggestions(Point pointClicked) {
        try {
            int offset = jTextArea1.viewToModel(pointClicked);
            start = javax.swing.text.Utilities.getWordStart(jTextArea1, offset);
            end = javax.swing.text.Utilities.getWordEnd(jTextArea1, offset);
            String curWord = jTextArea1.getDocument().getText(start, end - start);
            getSuggestions(curWord);
        } catch (BadLocationException e) {
        }
    }

    void getSuggestions(final String curWord) {
        popup.removeAll();
        
        if (sp == null || curWord == null || curWord.trim().length() == 0) {
            repopulatePopupMenu();
            return;
        }

        List<String> suggests = sp.suggest(curWord);
        if (suggests == null || suggests.isEmpty()) {
            repopulatePopupMenu();
            return;
        }

        ActionListener correctLst = new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent ae) {
                String word = ae.getActionCommand();
                if (word.equals("ignore.word")) {
                    sp.ignoreWord(curWord);
                } else if (word.equals("add.word")) {
                    sp.addWord(curWord);
                } else {
                    jTextArea1.select(start, end);
                    jTextArea1.replaceSelection(word);
                }
                sp.spellCheck();
            }
        };

        for (String word : suggests) {
            JMenuItem item = new JMenuItem(word);
            item.setFont(item.getFont().deriveFont(Font.BOLD));
            item.addActionListener(correctLst);
            popup.add(item);
        }

        popup.addSeparator();
        JMenuItem item = new JMenuItem(bundle.getString("Ignore_All"));
        item.setActionCommand("ignore.word");
        item.addActionListener(correctLst);
        popup.add(item);
        item = new JMenuItem(bundle.getString("Add_to_Dictionary"));
        item.setActionCommand("add.word");
        item.addActionListener(correctLst);
        popup.add(item);
        popup.addSeparator();

        // load standard menu items
        repopulatePopupMenu();
    }

    @Override
    void spellCheckActionPerformed() {
        sp = new SpellChecker(this.jTextArea1, curLangCode);
        if (this.jToggleButtonSpellCheck.isSelected()) {
            sp.enableSpellCheck();
        } else {
            sp.disableSpellCheck();
        }
        this.jTextArea1.repaint();
    }
}
