/**
 * Copyright @ 2008 Quan Nguyen
 * 
 * Licensed under the Apache License, Version 2.0 (the &quot;License&quot;);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an &quot;AS IS&quot; BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using System.Globalization;
using System.Threading;
using System.Drawing.Imaging;

using net.sourceforge.vietocr.postprocessing;
using Vietpad.NET.Controls;
using VietOCR.NET.Controls;
using Net.SourceForge.Vietpad.InputMethod;
using VietOCR.NET.WIA;

namespace VietOCR.NET
{
    public partial class GUI : Form
    {
        //private Bitmap image;
        protected const string strProgName = "VietOCR.NET";

        string curLangCode;
        string[] langCodes;
        string[] langs;

        private int imageIndex;
        private int imageTotal;
        private IList<Image> imageList;
        private Image currentImage;
        private FileInfo imageFile;

        private Rectangle rect = Rectangle.Empty;
        private Rectangle box = Rectangle.Empty;

        private float scaleX, scaleY;

        protected string selectedUILanguage;
        protected const string strUILang = "UILanguage";
        protected string strRegKey = "Software\\VietUnicode\\";
        private bool IsFitForZoomIn = false;
        private bool toggle = false;

        private bool isOcrTask; // true : OCR; false : Scan

        System.ComponentModel.ComponentResourceManager resources;

        public GUI()
        {
            // Access registry to determine which UI Language to be loaded.
            // The desired locale must be known before initializing visual components
            // with language text. Waiting until OnLoad would be too late.
            strRegKey += strProgName;

            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(strRegKey);

            if (regkey == null)
                regkey = Registry.CurrentUser.CreateSubKey(strRegKey);

            selectedUILanguage = (string)regkey.GetValue(strUILang, "en-US");
            regkey.Close();

            // Sets the UI culture to the selected language.
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedUILanguage);

            resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));

            InitializeComponent();

            //rectNormal = DesktopBounds;

            LoadLang();
            this.toolStripCbLang.Items.AddRange(langs);
        }

        void LoadLang()
        {
            XmlDocument doc = new XmlDocument();
            String path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            doc.Load(Path.Combine(path, "Data/ISO639-3.xml"));
            //doc.Load(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET.Data.ISO639-3.xml"));

            XmlNodeList list = doc.GetElementsByTagName("entry");
            Dictionary<string, string> ht = new Dictionary<string, string>();
            foreach (XmlNode node in list)
            {
                ht.Add(node.Attributes[0].Value, node.InnerText);
            }

            langCodes = Directory.GetFiles("tessdata", "*.inttemp");

            if (langCodes == null)
            {
                langs = new String[0];
            }
            else
            {
                langs = new String[langCodes.Length];
            }

            for (int i = 0; i < langs.Length; i++)
            {
                langCodes[i] = langCodes[i].Replace(".inttemp", "").Replace("tessdata\\", "");
                // translate ISO codes to full English names for user-friendliness
                langs[i] = ht[langCodes[i]];
            }
        }

        private void oCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                MessageBox.Show(this, resources.GetString("loadImage"), strProgName);
                return;
            }

            Rectangle rect = ((ScrollablePictureBox)this.pictureBox1).getRect();

            if (rect != Rectangle.Empty && this.pictureBox1.Image != null)
            {
                try
                {
                    //Image croppedImage = ImageIOHelper.Crop(this.pictureBox1.Image, rect);
                    //IList<Image> list = new List<Image>();
                    //list.Add(croppedImage);
                    rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                    performOCR(imageList, imageIndex, rect);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.StackTrace);
                }
            }
            else
            {
                performOCR(imageList, imageIndex, Rectangle.Empty);
            }
        }

        private void oCRAllPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                MessageBox.Show(this, resources.GetString("loadImage"), strProgName);
                return;
            }

            performOCR(imageList, -1, Rectangle.Empty);
        }

        void performOCR(IList<Image> imageList, int index, Rectangle rect)
        {
            try
            {
                if (this.toolStripCbLang.SelectedIndex == -1)
                {
                    MessageBox.Show(this, resources.GetString("selectLanguage"), strProgName);
                    return;
                }
                //if (this.pictureBox1.Image == null)
                //{
                //    MessageBox.Show(this, "Please load an image.", strProgName);
                //    return;
                //}

                this.toolStripStatusLabel1.Text = resources.GetString("OCRrunning");
                this.Cursor = Cursors.WaitCursor;
                this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;

                isOcrTask = true;

                OCRImageEntity entity = new OCRImageEntity(ImageIOHelper.GetImageList(imageFile), index, rect, curLangCode);
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void postprocessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curLangCode == null) return;

            try
            {
                string selectedText = this.textBox1.SelectedText;
                if (!String.IsNullOrEmpty(selectedText))
                {
                    selectedText = Processor.PostProcess(selectedText, curLangCode);
                    this.textBox1.SelectedText = selectedText;
                }
                else
                {
                    this.textBox1.Text = Processor.PostProcess(this.textBox1.Text, curLangCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(this, string.Format("Post-processing not supported for {0} language.", ex.Message), strProgName);
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked ^= true;
            this.textBox1.WordWrap = mi.Checked;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontdlg = new FontDialog();

            fontdlg.ShowColor = true;
            fontdlg.Font = this.textBox1.Font;
            fontdlg.Color = this.textBox1.ForeColor;

            if (fontdlg.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Font = fontdlg.Font;
                this.textBox1.ForeColor = fontdlg.Color;
            }

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OwnedForms.Length > 0)
                return;

            HtmlHelpForm helpForm = new HtmlHelpForm(resources.GetString("readme"), strProgName + resources.GetString("_Help"));
            helpForm.Owner = this;
            helpForm.Show();
        }

        private void aboutToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string releaseDate = System.Configuration.ConfigurationManager.AppSettings["ReleaseDate"];
            string version = System.Configuration.ConfigurationManager.AppSettings["Version"];

            MessageBox.Show(this, strProgName + " " + version + " © 2008\n" +
                ".NET GUI Frontend for Tesseract OCR\n" +
                DateTime.Parse(releaseDate).ToString("D", System.Threading.Thread.CurrentThread.CurrentUICulture).Normalize() + "\n" +
                "http://vietocr.sourceforge.net",
                resources.GetString("About_") + strProgName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Title = resources.GetString("OpenImageFile");
            openFileDialog1.Filter = "Image files (*.tif)|*.tif|Image files (*.bmp)|*.bmp|Image files (*.jpg)|*.jpg|Image files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFile(openFileDialog1.FileName);
                scaleX = 1f;
                scaleY = 1f;
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, new System.Text.UTF8Encoding());
                    sw.Write(this.textBox1.Text);
                    sw.Close();
                    this.textBox1.Modified = false;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void toolStripCbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            curLangCode = langCodes[this.toolStripCbLang.SelectedIndex];
            VietKeyHandler.VietModeEnabled = curLangCode == "vie";
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ToolStripSeparator || e.ClickedItem is ToolStripLabel) return;

            ToolStripButton tsb = (ToolStripButton)e.ClickedItem;
            ToolStripMenuItem mi = (ToolStripMenuItem)tsb.Tag;
            if (mi != null)
                mi.PerformClick();
        }

        private void toolStripBtnClear_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = null;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void settingsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.wordWrapToolStripMenuItem.Checked = this.textBox1.WordWrap;
        }

        private void toolStripBtnPrev_Click(object sender, EventArgs e)
        {
            imageIndex--;
            if (imageIndex < 0)
            {
                imageIndex = 0;
            }
            else
            {
                this.toolStripStatusLabel1.Text = null;
                displayImage();
            }
            setButton();
            this.pictureBox1.deselect();
        }

        private void toolStripBtnNext_Click(object sender, EventArgs e)
        {
            imageIndex++;
            if (imageIndex > imageTotal - 1)
            {
                imageIndex = imageTotal - 1;
            }
            else
            {
                this.toolStripStatusLabel1.Text = null;
                displayImage();
            }
            setButton();
            this.pictureBox1.deselect();
        }

        void setButton()
        {
            if (imageIndex == 0)
            {
                this.toolStripBtnPrev.Enabled = false;
            }
            else
            {
                this.toolStripBtnPrev.Enabled = true;
            }

            if (imageIndex == imageList.Count - 1)
            {
                this.toolStripBtnNext.Enabled = false;
            }
            else
            {
                this.toolStripBtnNext.Enabled = true;
            }
        }


        private void toolStripBtnFitImage_Click(object sender, EventArgs e)
        {
            if (toggle)
            {
                this.pictureBox1.Dock = DockStyle.None;
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                this.pictureBox1.deselect();
                scaleX = scaleY = 1f;
            }
            else
            {
                this.pictureBox1.Dock = DockStyle.Fill;
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pictureBox1.deselect();
                scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
                scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
            }
            toggle ^= true;
        }

        /// <summary>
        /// Opens image file.
        /// </summary>
        /// <param name="selectedImageFile"></param>
        public void openFile(string selectedImageFile)
        {
            imageFile = new FileInfo(selectedImageFile);
            this.Text = imageFile.Name + " - " + strProgName;
            loadImage(imageFile);
            displayImage();
            if (imageList == null)
            {
                return;
            }

            this.toolStripStatusLabel1.Text = null;
            this.pictureBox1.deselect();

            this.toolStripBtnFitImage.Enabled = true;
            this.toolStripBtnZoomIn.Enabled = true;
            this.toolStripBtnZoomOut.Enabled = true;
            this.toolStripBtnRL.Enabled = true;
            this.toolStripBtnRR.Enabled = true;

            if (imageList.Count == 1)
            {
                this.toolStripBtnNext.Enabled = false;
                this.toolStripBtnPrev.Enabled = false;
            }
            else
            {
                this.toolStripBtnNext.Enabled = true;
                this.toolStripBtnPrev.Enabled = true;
            }

            setButton();
        }

        void loadImage(FileInfo imageFile)
        {
            try
            {
                imageList = ImageIOHelper.GetImageList(imageFile);
                imageTotal = imageList.Count;
                imageIndex = 0;
            }
            catch (Exception ncde)
            {
                Console.Write(ncde.Message);
                MessageBox.Show(resources.GetString("Cannotloadimage"));
            }
        }

        void displayImage()
        {
            if (imageList != null)
            {
                this.lblCurIndex.Text = resources.GetString("Page_") + (imageIndex + 1) + resources.GetString("_of_") + imageTotal;
                currentImage = imageList[imageIndex];
                this.pictureBox1.Image = currentImage;
                this.pictureBox1.Size = this.pictureBox1.Image.Size;
                this.pictureBox1.Invalidate();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(e.Error.Message, isOcrTask ? resources.GetString("OCROperation") : resources.GetString("ScanningOperation"));
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = resources.GetString("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                if (isOcrTask)
                {
                    this.toolStripStatusLabel1.Text = resources.GetString("OCRcompleted");
                    this.textBox1.AppendText(e.Result.ToString());
                }
                else 
                {
                    this.toolStripStatusLabel1.Text = resources.GetString("Scancompleted");
                    openFile(e.Result.ToString());
                }
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.UseWaitCursor = false;
            this.textBox1.Cursor = Cursors.Default;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            OCRImageEntity entity = (OCRImageEntity)e.Argument;
            OCR ocrEngine = new OCR();

            // Assign the result of the computation to the Result property of the DoWorkEventArgs
            // object. This is will be available to the RunWorkerCompleted eventhandler.
            e.Result = ocrEngine.RecognizeText(entity.Images, entity.Index, entity.Lang, entity.Rect, worker, e);
        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.toolStripStatusLabel1.Text = resources.GetString("Scanning");
                this.Cursor = Cursors.WaitCursor;
                this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;

                isOcrTask = false;

                // Start the asynchronous operation.
                backgroundWorker2.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            using (WiaScannerAdapter adapter = new WiaScannerAdapter())
            {
                try
                {
                    string tempFileName = Path.GetTempFileName();
                    FileInfo imageFile = new FileInfo(tempFileName);
                    if (imageFile.Exists)
                    {
                        imageFile.Delete();
                    }
                    adapter.ScanImage(ImageFormat.Bmp, imageFile.FullName);
                    e.Result = tempFileName;
                }
                catch (WiaOperationException ex)
                {
                    throw new Exception(System.Text.RegularExpressions.Regex.Replace(ex.ErrorCode.ToString(), "(?=\\p{Lu}+)", " ").Trim() + ".");
                }
            }
        }
        private void splitContainer2_Panel2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if ((e.AllowedEffect & DragDropEffects.Move) != 0)
                    e.Effect = DragDropEffects.Move;

                if (((e.AllowedEffect & DragDropEffects.Copy) != 0) &&
                    ((e.KeyState & 0x08) != 0))    // Ctrl key
                    e.Effect = DragDropEffects.Copy;
            }
            //else if (e.Data.GetDataPresent(DataFormats.Bitmap))
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
        }

        private void splitContainer2_Panel2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] astr = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (System.IO.File.Exists(astr[0]))
                {
                    openFile(astr[0]);
                }
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!this.textBox1.Focused)
            {
                this.textBox1.Focus();
            }
        }

        private void toolStripBtnRL_Click(object sender, EventArgs e)
        {
            // Rotating 270 degrees is equivalent to rotating -90 degrees.
            this.pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.pictureBox1.Refresh();
        }

        private void toolStripBtnRR_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.pictureBox1.Refresh();
        }

        private void toolStripBtnZoomIn_Click(object sender, EventArgs e)
        {
            IsFitForZoomIn = true;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            // Zoom works best if you first fit the image according to its true aspect ratio.
            Fit();
            // Make the PictureBox dimensions larger by 25% to effect the Zoom.
            this.pictureBox1.Width = Convert.ToInt32(this.pictureBox1.Width * 1.25);
            this.pictureBox1.Height = Convert.ToInt32(this.pictureBox1.Height * 1.25);
            scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
            scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;

        }

        private void toolStripBtnZoomOut_Click(object sender, EventArgs e)
        {
            // Zoom works best if you first fit the image according to its true aspect ratio.
            Fit();
            // StretchImage SizeMode works best for zooming.
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            // Make the PictureBox dimensions smaller by 25% to effect the Zoom.
            this.pictureBox1.Width = Convert.ToInt32(this.pictureBox1.Width / 1.25);
            this.pictureBox1.Height = Convert.ToInt32(this.pictureBox1.Height / 1.25);
            scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
            scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
        }
        // This method makes the image fit properly in the PictureBox. You might think 
        // that the AutoSize SizeMode enum would make the image appear in the PictureBox 
        // according to its true aspect ratio within the fixed bounds of the PictureBox.
        // However, it merely expands or shrinks the PictureBox.
        private void Fit()
        {
            // if Fit was called by the Zoom In button, then center the image. This is
            // only needed when working with images that are smaller than the PictureBox.
            // Feel free to uncomment the line that sets the SizeMode and then see how
            // it causes Zoom In for small images to show unexpected behavior.

            if ((this.pictureBox1.Image.Width < this.pictureBox1.Width) &&
                (this.pictureBox1.Image.Height < this.pictureBox1.Height))
            {
                if (!IsFitForZoomIn)
                {
                    this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                }
            }
            CalculateAspectRatioAndSetDimensions();
        }

        // Calculates and returns the image's aspect ratio, and sets 
        // its proper dimensions. This is used for Fit() and for saving thumbnails
        // of images.
        private double CalculateAspectRatioAndSetDimensions()
        {
            // Calculate the proper aspect ratio and set the image's dimensions.
            double ratio;

            if (this.pictureBox1.Image.Width > this.pictureBox1.Image.Height)
            {
                ratio = this.pictureBox1.Image.Width / this.pictureBox1.Image.Height;
                this.pictureBox1.Height = Convert.ToInt32(Convert.ToDouble(this.pictureBox1.Width) / ratio);
            }
            else
            {
                ratio = this.pictureBox1.Image.Height / this.pictureBox1.Image.Width;
                this.pictureBox1.Width = Convert.ToInt32(Convert.ToDouble(this.pictureBox1.Height) / ratio);
            }
            return ratio;
        }
    }
}