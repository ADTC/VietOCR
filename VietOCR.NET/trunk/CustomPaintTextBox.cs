//
// Reuse NHunspellTextBoxExtender
//

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace VietOCR.NET
{
/// <summary>
/// This is the class that handles painting the wavy red lines.
/// 
/// It utilizes the NativeWindow to find out when it needs to draw
/// </summary>
/// <remarks></remarks>
class CustomPaintTextBox : NativeWindow, IDisposable
{

	private TextBoxBase clientTextBox;
	private Bitmap myBitmap;
	private Graphics textBoxGraphics;
	private Graphics bufferGraphics;
	private SpellChecker mySpellChecker;
    CharacterRange[] spellingErrorRanges;

    //private NHunspellTextBoxExtender myParent;
	public event CustomPaintCompleteEventHandler CustomPaintComplete;
	public delegate void CustomPaintCompleteEventHandler(TextBoxBase sender, long Milliseconds);


	/// <summary>
	/// This is called when the textbox is being redrawn.
	/// When it is, for the textbox to get refreshed, call it's default
	/// paint method and then call our method
	/// </summary>
	/// <param name="m">The windows message</param>
	/// <remarks></remarks>
	protected override void WndProc(ref System.Windows.Forms.Message m)
	{
		switch (m.Msg) {
			case 15:
				//This is the WM_PAINT message
				//Invalidate the textBoxBase so that it gets refreshed properly
				clientTextBox.Invalidate();

				//call the default win32 Paint method for the TextBoxBase first
				base.WndProc(ref m);

				//now use our code to draw the extra stuff
				this.CustomPaint();

				break;
			default:
				base.WndProc(ref m);
				break;
		}
	}


    public CustomPaintTextBox(TextBoxBase CallingTextBox, CharacterRange[] spellingErrorRanges)
	{
		//Set up the CustomPaintTextBox
		this.clientTextBox = CallingTextBox;
        this.spellingErrorRanges = spellingErrorRanges;

		//Create a bitmap with the same dimensions as the textbox
		myBitmap = new Bitmap(clientTextBox.Width, clientTextBox.Height);

		//Create the graphics object from this bitmpa...this is where we will draw the lines to start with
		bufferGraphics = Graphics.FromImage(myBitmap);
		bufferGraphics.Clip = new Region(clientTextBox.ClientRectangle);

		//Get the graphics object for the textbox.  We use this to draw the bufferGraphics
		textBoxGraphics = Graphics.FromHwnd(clientTextBox.Handle);

		//Assign a handle for this class and set it to the handle for the textbox
		this.AssignHandle(clientTextBox.Handle);

		//We also need to make sure we update the handle if the handle for the textbox changes
		//This occurs if wordWrap is turned off for a RichTextBox
		clientTextBox.HandleCreated += TextBoxBase_HandleCreated;

		//We need to add a handler to change the clip rectangle if the textBox is resized
		clientTextBox.ClientSizeChanged += TextBoxBase_ClientSizeChanged;

		this.disposedValue = false;
	}


	/// <summary>
	/// Gets the ranges of chars that represent the spelling errors and then draw a wavy red line underneath
	/// them.
	/// </summary>
	/// <remarks></remarks>
	//ByVal sender As Object, ByVal e As DoWorkEventArgs)
	private void CustomPaint()
	{
		//Determine if we need to draw anything
        //if (!mySpellChecker.HasSpellingErrors)
        //    return;
        //if (!myParent.IsEnabled(parentTextBox))
        //    return;

		//Benchmarking
		DateTime startTime = DateTime.Now;

		RichTextBox tempRTB = null;

		if (clientTextBox is RichTextBox) {
			tempRTB = new RichTextBox();
			tempRTB.Rtf = ((RichTextBox)clientTextBox).Rtf;
		}

		//Clear the graphics buffer
		bufferGraphics.Clear(Color.Transparent);

		//Now, find out if any of the spelling errors are within the visible part of the textbox

		//First get the ranges of characters visible in the textbox
		Point startPoint = new Point(0, 0);
		Point endPoint = new Point(clientTextBox.ClientRectangle.Width, clientTextBox.ClientRectangle.Height);
		long startIndex = clientTextBox.GetCharIndexFromPosition(startPoint);
		long endIndex = clientTextBox.GetCharIndexFromPosition(endPoint);

        foreach (CharacterRange currentRange in spellingErrorRanges)
        {
			//Get the X, Y of the start and end characters
			startPoint = clientTextBox.GetPositionFromCharIndex(currentRange.First);
			endPoint = clientTextBox.GetPositionFromCharIndex(currentRange.First + currentRange.Length - 1);

			if (startPoint.Y != endPoint.Y) {
				//We have a word on multiple lines
				int curIndex = 0;
				int startingIndex = 0;
				curIndex = currentRange.First;
				startingIndex = curIndex;
				GetNextLine:

				//Determine the first line of waves to draw
				while ((clientTextBox.GetPositionFromCharIndex(curIndex).Y == startPoint.Y) & (curIndex <= (currentRange.First + currentRange.Length - 1))) {
					curIndex += 1;
				}

				//Go back to the previous character
				curIndex -= 1;

				endPoint = clientTextBox.GetPositionFromCharIndex(curIndex);
				Point offsets = GetOffsets(ref clientTextBox, startingIndex, curIndex, tempRTB);

				//Dim offsetsDiff As TimeSpan = Now.Subtract(startTime)
				//Debug.Print("Get Offsets: " & offsetsDiff.TotalMilliseconds & " Milliseconds")

				//If we're using a RichTextBox, we have to account for the zoom factor
				if (clientTextBox is RichTextBox)
					offsets.Y = (int)(offsets.Y * ((RichTextBox)clientTextBox).ZoomFactor);

				//Reset the starting and ending points to make sure we're underneath the word
				//(The measurestring adds some margin, so remove them)
				startPoint.Y += offsets.Y - 2;
				endPoint.Y += offsets.Y - 2;
				endPoint.X += offsets.X - 0;

				//Add a new wavy line using the starting and ending point
				DrawWave(startPoint, endPoint);

				//Dim drawWaveDiff As TimeSpan = Now.Subtract(startTime)
				//Debug.Print("DrawWave: " & drawWaveDiff.TotalMilliseconds & " Milliseconds")

				startingIndex = curIndex + 1;
				curIndex += 1;
				startPoint = clientTextBox.GetPositionFromCharIndex(curIndex);

				if (curIndex <= (currentRange.First + currentRange.Length - 1)) {
					goto GetNextLine;
				}
			} else {
				Point offsets = GetOffsets(ref clientTextBox, currentRange.First, (currentRange.First + currentRange.Length - 1), tempRTB);

				//Dim offsetsDiff As TimeSpan = Now.Subtract(startTime)
				//Debug.Print("Get Offsets: " & offsetsDiff.TotalMilliseconds & " Milliseconds")

				//If we're using a RichTextBox, we have to account for the zoom factor
				if (clientTextBox is RichTextBox)
					offsets.Y = (int) (offsets.Y * ((RichTextBox)clientTextBox).ZoomFactor);

				//Reset the starting and ending points to make sure we're underneath the word
				//(The measurestring adds some margin, so remove them)
				startPoint.Y += offsets.Y - 2;
				endPoint.Y += offsets.Y - 2;
				endPoint.X += offsets.X - 4;

				//Add a new wavy line using the starting and ending point
				//If e.Cancel Then Return
				DrawWave(startPoint, endPoint);

				//Dim drawWaveDiff As TimeSpan = Now.Subtract(startTime)
				//Debug.Print("DrawWave: " & drawWaveDiff.TotalMilliseconds & " Milliseconds")
			}
		}

		//Dim drawDiff As TimeSpan = Now.Subtract(drawStartTime)
		//Debug.Print("Draw: " & drawDiff.TotalMilliseconds & " Milliseconds")

		//We've drawn all of the wavy lines, so draw that image over the textbox
		textBoxGraphics.DrawImageUnscaled(myBitmap, 0, 0);

		//Dim dateDiff As TimeSpan = Now.Subtract(startTime)
		//Debug.Print("----TotalTime: " & dateDiff.Seconds & " Seconds, " & dateDiff.Milliseconds & " Milliseconds------------")

		if (CustomPaintComplete != null) {
			CustomPaintComplete(clientTextBox, (long)DateTime.Now.Subtract(startTime).TotalMilliseconds);
		}
	}


	/// <summary>
	/// Determines the X and Y offsets to use based on font height last letter width
	/// </summary>
	/// <param name="curTextBox"></param>
	/// <param name="startingIndex"></param>
	/// <param name="endingIndex"></param>
	/// <param name="tempRTB"></param>
	/// <returns></returns>
	/// <remarks></remarks>
	private Point GetOffsets(ref TextBoxBase curTextBox, int startingIndex, int endingIndex, RichTextBox tempRTB)
	{
		DateTime startTime = DateTime.Now;

		//We now have the top left point of the characters, now we need to add the offsets
		int offsetY = 0;
		Font fontToUse = curTextBox.Font;
		Point offsets = default(Point);

		fontToUse = new Font(fontToUse.FontFamily, 0.1f, fontToUse.Style, fontToUse.Unit, fontToUse.GdiCharSet, fontToUse.GdiVerticalFont);

		//If it's a RichTextBox, we have to do some extra things
		if (curTextBox is RichTextBox) {
			//We need to go through every character where we will draw the lines and get the tallest
			//font height

			//Benchmarking
			//Dim beforeCreateTextBoxDiff As TimeSpan = Now.Subtract(startTime)
			//Debug.Print("    Before Create TextBox: " & beforeCreateTextBoxDiff.TotalMilliseconds & " Milliseconds")

			//Create a temporary textbox for getting the RTF info so that we don't have to select and
			//de-select a lot of text and cause the screen to have to refresh
			if (tempRTB == null) {
				tempRTB = new RichTextBox();
				tempRTB.Rtf = ((RichTextBox)curTextBox).Rtf;
			}

			//Benchmarking
			//Dim createTextBoxDiff As TimeSpan = Now.Subtract(startTime)
			//Debug.Print("    Create TextBox: " & createTextBoxDiff.TotalMilliseconds & " Milliseconds")

			var _with2 = tempRTB;
			if (_with2.Text.Length > 0) {
				//Have to find the first visible character on that line
				int firstCharInLine = 0;
				int lastCharInLine = 0;
				int curCharLine = 0;
				curCharLine = _with2.GetLineFromCharIndex(startingIndex);
				firstCharInLine = _with2.GetFirstCharIndexFromLine(curCharLine);
				lastCharInLine = _with2.GetFirstCharIndexFromLine(curCharLine + 1);

				if (lastCharInLine == -1)
					lastCharInLine = curTextBox.TextLength;

				DateTime getFontHeightStart = DateTime.Now;

				//Now go through every character that is visible and get the biggest font height
				//Use the tempRTB for this
				for (int i = firstCharInLine + 1; i <= (lastCharInLine + 1); i++) {
					_with2.SelectionStart = i;
					_with2.SelectionLength = 1;
					if (_with2.SelectionFont.Height > fontToUse.Height) {
						//fontHeight = .SelectionFont.Height
						fontToUse = _with2.SelectionFont;
					}
				}

				//Benchmarking
				//Dim foundHeightdiff As TimeSpan = Now.Subtract(startTime)
				//Debug.Print("    Get Font Height: " & foundHeightdiff.TotalMilliseconds & " Milliseconds")

				offsetY = fontToUse.Height;
			}

		} else {
			//If we get here, it's just a standard textbox and we can just use the font height
			fontToUse = curTextBox.Font;

			offsetY = curTextBox.Font.Height;
		}

		//Now find out how wide the last character is
		int offsetX = 0;
		offsetX = (int) textBoxGraphics.MeasureString(curTextBox.Text[startingIndex + (endingIndex - startingIndex)].ToString(), fontToUse).Width;

		offsets = new Point(offsetX, offsetY);

		//Benchmarking
		//Dim timeDiff As TimeSpan = Now.Subtract(startTime)
		//Debug.Print("GetOffsets: " & timeDiff.TotalMilliseconds & " Milliseconds")

		return offsets;
	}


	/// <summary>
	/// The textbox is not redrawn much, so this will force the textbox to call the custom paint function.
	/// Otherwise, text can be entered and no wavy red lines will appear
	/// </summary>
	/// <remarks></remarks>
	public void ForcePaint()
	{
		clientTextBox.Invalidate();
	}


	/// <summary>
	/// Draws the wavy red line given a starting point and an ending point
	/// </summary>
	/// <param name="StartOfLine">A Point representing the starting point</param>
	/// <param name="EndOfLine">A Point representing the ending point</param>
	/// <remarks></remarks>
	private void DrawWave(Point StartOfLine, Point EndOfLine)
	{
		Pen newPen = Pens.Red;

		if ((EndOfLine.X - StartOfLine.X) > 4) {
			ArrayList pl = new ArrayList();
			for (int i = StartOfLine.X; i <= (EndOfLine.X - 2); i += 4) {
				pl.Add(new Point(i, StartOfLine.Y));
				pl.Add(new Point(i + 2, StartOfLine.Y + 2));
			}

			Point[] p = (Point[])pl.ToArray(typeof(Point));
			bufferGraphics.DrawLines(newPen, p);
		} else {
			bufferGraphics.DrawLine(newPen, StartOfLine, EndOfLine);
		}
	}


	/// <summary>
	/// Reassign this classes handle and the graphics object anytime the textbox's handle is changed
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	/// <remarks></remarks>
	private void TextBoxBase_HandleCreated(object sender, System.EventArgs e)
	{
		this.AssignHandle(clientTextBox.Handle);
		textBoxGraphics = Graphics.FromHwnd(clientTextBox.Handle);
	}


	/// <summary>
	/// When the TextBoxBase is resized, this will reset the objects that are used to draw
	/// the wavy, red line.  Without this, anything outside of the original bounds will not
	/// be drawn
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	/// <remarks></remarks>
	private void TextBoxBase_ClientSizeChanged(object sender, System.EventArgs e)
	{
		TextBoxBase tempTextBox = (TextBoxBase)sender;

		//Create a bitmap with the same dimensions as the textbox
		myBitmap = new Bitmap(tempTextBox.Width, tempTextBox.Height);

		//Create the graphics object from this bitmpa...this is where we will draw the lines to start with
		bufferGraphics = Graphics.FromImage(myBitmap);
		bufferGraphics.Clip = new Region(tempTextBox.ClientRectangle);

		//Get the graphics object for the textbox.  We use this to draw the bufferGraphics
		textBoxGraphics = Graphics.FromHwnd(tempTextBox.Handle);
	}


	#region "IDisposable Support"
		// To detect redundant calls
	private bool disposedValue;

	// IDisposable
	protected virtual void Dispose(bool disposing)
	{
		if (!this.disposedValue & disposing) {
			if (disposing) {
				// TODO: dispose managed state (managed objects).

				if (clientTextBox != null) {
					this.ReleaseHandle();

					clientTextBox.Invalidate();

					clientTextBox.HandleCreated -= TextBoxBase_HandleCreated;
					clientTextBox.ClientSizeChanged -= TextBoxBase_ClientSizeChanged;

					clientTextBox.Dispose();
					clientTextBox = null;
				}

				if (myBitmap != null) {
					myBitmap.Dispose();
					myBitmap = null;
				}

				if (textBoxGraphics != null) {
					textBoxGraphics.Dispose();
					textBoxGraphics = null;
				}

				if (bufferGraphics != null) {
					bufferGraphics.Dispose();
					bufferGraphics = null;
				}

				if (mySpellChecker != null) {
					mySpellChecker = null;
				}

                //if (myParent != null) {
                //    myParent = null;
                //}
			}

			// TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
			// TODO: set large fields to null.
		}
		this.disposedValue = true;
	}

	// TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
	//Protected Overrides Sub Finalize()
	//    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
	//    Dispose(False)
	//    MyBase.Finalize()
	//End Sub

	// This code added by Visual Basic to correctly implement the disposable pattern.
	public void Dispose()
	{
		// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	#endregion


}

}
