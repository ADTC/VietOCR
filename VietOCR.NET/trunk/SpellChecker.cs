using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NHunspell;
using System.Text.RegularExpressions;

namespace VietOCR.NET
{
    class SpellChecker
    {
        TextBoxBase textbox;
        string localeId;
        String workingDir;

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
            spellCheck();
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

            List<CharacterRange> spellingErrorRanges = new List<CharacterRange>(); 

            // build regex
            String patternStr = "\\b(" + sb.ToString() + ")\\b";       
            Regex regex = new Regex(patternStr, RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(textbox.Text);
            
            // Loop through  the match collection to retrieve all 
            // matches and positions.
            for (int i = 0; i < mc.Count; i++)
            {
                spellingErrorRanges.Add(new CharacterRange(mc[i].Index, mc[i].Length));
            }
            new CustomPaintTextBox(textbox, spellingErrorRanges.ToArray());
        }

        List<String> spellCheck(List<String> words)
        {
            List<String> misspelled = new List<String>();
            try
            {
                string dictPath = workingDir + "/dict/" + localeId;
                Hunspell spellDict = new Hunspell(dictPath + ".aff", dictPath + ".dic");

                foreach (String word in words)
                {
                    if (!spellDict.Spell(word))
                    {
                        misspelled.Add(word);
                    }
                }
            }
            catch (Exception e)
            {
                 MessageBox.Show(e.Message);
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
            //this.textComp.getDocument().removeDocumentListener(lstList.remove(0));
            //this.textComp.getHighlighter().removeAllHighlights();
        }
    }
}
