using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NHunspell;
using System.Text.RegularExpressions;
using System.Collections;

namespace VietOCR.NET
{
    class SpellChecker
    {
        TextBoxBase textbox;
        string localeId;
        String workingDir;
        static ArrayList listeners = new ArrayList();
        List<CharacterRange> spellingErrorRanges = new List<CharacterRange>();
        Hunspell spellDict;

        public CharacterRange[] GetSpellingErrorRanges()
        {
            return spellingErrorRanges.ToArray();
        }


        public SpellChecker(TextBoxBase textbox, string langCode)
        {
            this.textbox = textbox;

            XmlDocument doc = new XmlDocument();

            workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            String xmlFilePath = Path.Combine(workingDir, "Data/ISO639-1.xml");
            Dictionary<string, string> ht = new Dictionary<string, string>();
            doc.Load(xmlFilePath);
            //doc.Load(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET.Data.ISO639-3.xml"));

            XmlNodeList list = doc.GetElementsByTagName("entry");
            foreach (XmlNode node in list)
            {
                ht.Add(node.Attributes[0].Value, node.InnerText);
            }

            localeId = ht[langCode];
        }

        public void enableSpellCheck()
        {
            if (localeId == null)
            {
                return;
            }
            try
            {
                string dictPath = workingDir + "/dict/" + localeId;
                spellDict = new Hunspell(dictPath + ".aff", dictPath + ".dic");

                listeners.Add(new System.EventHandler(this.textbox_TextChanged));

                this.textbox.TextChanged += (System.EventHandler)listeners[0];
                spellCheck();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void spellCheck()
        {
            List<String> words = parseText(textbox.Text);
            List<String> misspelledWords = spellCheck(words);
            if (misspelledWords.Count == 0)
            {
                return; // perfect writer!
            }

            StringBuilder sb = new StringBuilder();
            foreach (String word in misspelledWords)
            {
                sb.Append(word).Append("|");
            }
            sb.Length -= 1; //remove last |

            // build regex
            String patternStr = "\\b(" + sb.ToString() + ")\\b";
            Regex regex = new Regex(patternStr, RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(textbox.Text);

            // Loop through  the match collection to retrieve all 
            // matches and positions.
            for (int i = 0; i < mc.Count; i++)
            {
                spellingErrorRanges.Add(new CharacterRange(mc[i].Index, mc[i].Length));
                //textbox.Select(mc[i].Index, mc[i].Length);
            }
            //new CustomPaintTextBox(textbox, this);
        }

        List<String> spellCheck(List<String> words)
        {
            List<String> misspelled = new List<String>();
            
            foreach (String word in words)
            {
                if (!spellDict.Spell(word))
                {
                    misspelled.Add(word);
                }
            }

            return misspelled;
        }

        List<String> parseText(String text)
        {
            List<String> words = new List<String>();
            BreakIterator boundary = BreakIterator.GetWordInstance();
            boundary.Text = text;
            int start = boundary.First();
            for (int end = boundary.Next(); end != BreakIterator.DONE; start = end, end = boundary.Next())
            {
                if (!Char.IsLetter(text[start]))
                {
                    continue;
                }
                words.Add(text.Substring(start, end - start));
            }

            return words;
        }

        public void disableSpellCheck()
        {
            if (localeId == null)
            {
                return;
            }
            this.textbox.TextChanged -= (System.EventHandler)listeners[0];
            listeners.RemoveAt(0);
            //this.textComp.getHighlighter().removeAllHighlights();
        }
        
        private void textbox_TextChanged(object sender, EventArgs e)
        {
            spellCheck();
        }
    }
}
