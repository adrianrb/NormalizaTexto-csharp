/* ãáçêí */
using System;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using LibComponentesCustomizados;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace NormalizaTexto
{
    public partial class frmMain : Form
    {
        int lastLine = 0;
        int lineNumber;
        int SplitterPos;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SplitterPos = splitContainer1.SplitterDistance;

            ToolTip toolTip1 = new ToolTip();
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(splitContainer1, "Arraste para redimencionar os painéis.\nDuplo clique para restaurar.");
            
            txtOrig.SelectAll();
        }
        
        string RemoveDiacritics(string text)
        {
            string ret = "";

            try
            {
                var normalizedString = text.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();

                foreach (var c in normalizedString)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c);
                    }
                }

                ret = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ret;
        }

        private void txtOrig_TextChanged(object sender, EventArgs e)
        {
            txtOrig.SuspendLayout();
            txtDest.SuspendLayout();

            txtOrig.SelectAll();
            txtDest.Text = RemoveDiacritics(txtOrig.Text);
            txtOrig.SelectionStart = 0;
            txtOrig.SelectionLength = 0;

            txtOrig.SelectionStart = 0;
            txtOrig.SelectionLength = 0;
            txtOrig.ScrollToCaret();
            
            txtDest.SelectionStart = 0;
            txtDest.SelectionLength = 0;
            txtDest.ScrollToCaret();

            txtOrig.ResumeLayout();
            txtDest.ResumeLayout();

            TotLinhas.Text = (txtOrig.Lines.Length + 1).ToString();

            MatchCollection wordColl = Regex.Matches(txtOrig.Text, @"[\W]+");
            NumPalavras.Text = wordColl.Count.ToString();

            NumCaracs.Text = txtOrig.TextLength.ToString();
        }

        private void txtOrig_Click(object sender, EventArgs e)
        {
            txtOrig.SuspendLayout();
            txtDest.SuspendLayout();

            HighlightCurrentLine(txtOrig, Color.PaleTurquoise);
            HighlightCurrentLine(txtDest, tcp.start, tcp.end, Color.PaleTurquoise);
            
            txtDest.SelectionStart = txtOrig.SelectionStart;
            txtDest.ScrollToCaret();
            txtOrig.ScrollToCaret();

            txtOrig.ResumeLayout();
            txtDest.ResumeLayout();

            // Linha e coluna do cursor
            linha.Text = lineNumber.ToString();

            int firstChar = txtOrig.GetFirstCharIndexFromLine(lineNumber);
            int column = txtOrig.SelectionStart - firstChar;
            coluna.Text = column.ToString();
        }

        public struct TextCharPositions
        {
            public int start, end;

            public TextCharPositions(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }

        TextCharPositions tcp;

        private void HighlightCurrentLine(RichTextBox rtb, Color c)
        {
            if (c == null)
                c = Color.PaleTurquoise;

            // Save current selection
            int selectionStart = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;

            // Get character positions for the current line
            int firstCharPosition = rtb.GetFirstCharIndexOfCurrentLine();
            lineNumber = rtb.GetLineFromCharIndex(firstCharPosition);
            int lastCharPosition = rtb.GetFirstCharIndexFromLine(lineNumber + 1);
            if (lastCharPosition == -1)
                lastCharPosition = rtb.TextLength;

            tcp = new TextCharPositions(firstCharPosition, lastCharPosition);

            // Clear any previous color
            if (lineNumber != lastLine)
            {
                int previousFirstCharPosition = rtb.GetFirstCharIndexFromLine(lastLine);
                int previousLastCharPosition = rtb.GetFirstCharIndexFromLine(lastLine + 1);
                if (previousLastCharPosition == -1)
                    previousLastCharPosition = rtb.TextLength;

                rtb.SelectionStart = previousFirstCharPosition;
                rtb.SelectionLength = previousLastCharPosition - previousFirstCharPosition;
                rtb.SelectionBackColor = SystemColors.Window;
                lastLine = lineNumber;
            }

            // Set new color
            rtb.SelectionStart = firstCharPosition;
            rtb.SelectionLength = lastCharPosition - firstCharPosition;
            if (rtb.SelectionLength > 0)
                rtb.SelectionBackColor = c;

            // Reset selection
            rtb.SelectionStart = selectionStart;
            rtb.SelectionLength = selectionLength;
        }

        private void HighlightCurrentLine(RichTextBox rtb, int firstCharPosition, int lastCharPosition, Color c)
        {
            if (c == null)
                c = Color.PaleTurquoise;

            // Save current selection
            int selectionStart = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;

            // Get character positions for the current line
            int lineNumber = rtb.GetLineFromCharIndex(firstCharPosition);
            if (lastCharPosition == -1)
                lastCharPosition = rtb.TextLength;

            // Clear any previous color
            if (lineNumber != lastLine)
            {
                int previousFirstCharPosition = rtb.GetFirstCharIndexFromLine(lastLine);
                int previousLastCharPosition = rtb.GetFirstCharIndexFromLine(lastLine + 1);
                if (previousLastCharPosition == -1)
                    previousLastCharPosition = rtb.TextLength;

                rtb.SelectionStart = previousFirstCharPosition;
                rtb.SelectionLength = previousLastCharPosition - previousFirstCharPosition;
                rtb.SelectionBackColor = SystemColors.Window;
                lastLine = lineNumber;
            }

            // Set new color
            rtb.SelectionStart = firstCharPosition;
            rtb.SelectionLength = lastCharPosition - firstCharPosition;
            if (rtb.SelectionLength > 0)
                rtb.SelectionBackColor = c;

            // Reset selection
            rtb.SelectionStart = selectionStart;
            rtb.SelectionLength = selectionLength;
        }

        private void txtOrig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                txtOrig.Text = (string)Clipboard.GetData("Text");

                e.Handled = true;
            }
        }

        private void splitContainer1_DoubleClick(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = SplitterPos;
        }
    }
}
