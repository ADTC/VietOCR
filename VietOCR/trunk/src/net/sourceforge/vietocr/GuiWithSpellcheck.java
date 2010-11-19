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
import java.util.List;
import javax.swing.JMenuItem;
import javax.swing.text.BadLocationException;

public class GuiWithSpellcheck extends GuiWithSettings {

    private int start, end;
    private SpellChecker sp;

    @Override
    void populatePopupMenu() {
        popup.removeAll();

        try {
            if (pointClicked != null) {
                int offset = jTextArea1.viewToModel(pointClicked);
                start = javax.swing.text.Utilities.getWordStart(jTextArea1, offset);
                end = javax.swing.text.Utilities.getWordEnd(jTextArea1, offset);
                String curMisspelled = jTextArea1.getDocument().getText(start, end - start);
                getSuggestions(curMisspelled);
            }
        } catch (BadLocationException e) {
        }

        super.populatePopupMenu();
    }

    void getSuggestions(final String misspelled) {
        if (sp == null || misspelled == null || misspelled.trim().length() == 0) {
            return;
        }

        List<String> suggests = sp.suggest(misspelled);
        if (suggests == null || suggests.isEmpty()) {
            return;
        }

        ActionListener correctLst = new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent ae) {
                String word = ae.getActionCommand();
                if (word.equals("ignore")) {
                    sp.ignoreWord(misspelled);
                } else if (word.equals("add")) {
                    sp.addWord(misspelled);
                } else {
                    jTextArea1.select(start, end);
                    jTextArea1.replaceSelection(word);
                }
                sp.spellCheck();
            }
        };

        for (String word : suggests) {
            JMenuItem item = new JMenuItem(word);
            item.setActionCommand(word);
            item.addActionListener(correctLst);
            popup.add(item);
        }
        popup.addSeparator();
        JMenuItem item = new JMenuItem(bundle.getString("Ignore_All"));
        item.setActionCommand("ignore");
        item.addActionListener(correctLst);
        popup.add(item);
        item = new JMenuItem(bundle.getString("Add_to_Dictionary"));
        item.setActionCommand("add");
        item.addActionListener(correctLst);
        popup.add(item);
        popup.addSeparator();
    }

    @Override
    void SpellCheckActionPerformed() {
        sp = new SpellChecker(this.jTextArea1, curLangCode);
        if (this.jToggleButtonSpellCheck.isSelected()) {
            sp.enableSpellCheck();
        } else {
            sp.disableSpellCheck();
        }
        this.jTextArea1.repaint();
    }
}
