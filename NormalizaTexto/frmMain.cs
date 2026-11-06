/* ãáçêí */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NormalizaTexto
{
    public partial class frmMain : Form
    {
        private static readonly Regex WordRegex = new Regex(@"[\p{L}\p{N}]+", RegexOptions.Compiled);

        private readonly Dictionary<RichTextBox, int> lastHighlightedLine = new Dictionary<RichTextBox, int>();
        private readonly ToolTip splitterToolTip = new ToolTip();

        private TextCharPositions highlightedLine;
        private int splitterPosition;
        private bool updatingSelection;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            splitterPosition = splitContainer1.SplitterDistance;

            splitterToolTip.IsBalloon = true;
            splitterToolTip.SetToolTip(
                splitContainer1,
                "Arraste para redimensionar os painéis.\nDuplo clique para restaurar.");

            txtOrig.SelectAll();
            UpdateCaretInfo();
        }

        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            try
            {
                string normalizedString = text.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder(normalizedString.Length);

                foreach (char c in normalizedString)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        stringBuilder.Append(c);
                }

                return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return text;
            }
        }

        private void txtOrig_TextChanged(object sender, EventArgs e)
        {
            txtDest.Text = RemoveDiacritics(txtOrig.Text);
            UpdateStatistics();
            UpdateCaretInfo();
        }

        private void UpdateStatistics()
        {
            TotLinhas.Text = txtOrig.Lines.Length.ToString();
            NumPalavras.Text = WordRegex.Matches(txtOrig.Text).Count.ToString();
            NumCaracs.Text = txtOrig.TextLength.ToString();
        }

        private void txtOrig_Click(object sender, EventArgs e)
        {
            HighlightAndUpdate();
            SyncDestinationCaret();
        }

        private void txtOrig_SelectionChanged(object sender, EventArgs e)
        {
            if (updatingSelection)
                return;

            UpdateCaretInfo();
        }

        private void txtOrig_KeyUp(object sender, KeyEventArgs e)
        {
            HighlightAndUpdate();
        }

        private void HighlightAndUpdate()
        {
            HighlightCurrentLine(txtOrig, Color.PaleTurquoise);

            if (highlightedLine.end > highlightedLine.start)
            {
                HighlightCurrentLine(
                    txtDest,
                    highlightedLine.start,
                    highlightedLine.end,
                    Color.PaleTurquoise);
            }

            UpdateCaretInfo();
        }

        private void SyncDestinationCaret()
        {
            int position = Math.Min(txtOrig.SelectionStart, txtDest.TextLength);

            txtDest.SelectionStart = position;
            txtDest.SelectionLength = 0;
            txtDest.ScrollToCaret();
            txtOrig.ScrollToCaret();
        }

        private void UpdateCaretInfo()
        {
            int position = Math.Min(txtOrig.SelectionStart, txtOrig.TextLength);
            int line = txtOrig.GetLineFromCharIndex(position);
            int firstChar = Math.Max(txtOrig.GetFirstCharIndexFromLine(line), 0);

            linha.Text = (line + 1).ToString();
            coluna.Text = (position - firstChar + 1).ToString();
        }

        private struct TextCharPositions
        {
            public readonly int start;
            public readonly int end;

            public TextCharPositions(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }

        private void HighlightCurrentLine(RichTextBox rtb, Color color)
        {
            int firstCharPosition = rtb.GetFirstCharIndexOfCurrentLine();
            int lineNumber = rtb.GetLineFromCharIndex(firstCharPosition);
            int lastCharPosition = rtb.GetFirstCharIndexFromLine(lineNumber + 1);

            if (lastCharPosition < 0)
                lastCharPosition = rtb.TextLength;

            highlightedLine = new TextCharPositions(firstCharPosition, lastCharPosition);
            HighlightRange(rtb, firstCharPosition, lastCharPosition, lineNumber, color);
        }

        private void HighlightCurrentLine(RichTextBox rtb, int firstCharPosition, int lastCharPosition, Color color)
        {
            int lineNumber = rtb.GetLineFromCharIndex(Math.Min(firstCharPosition, rtb.TextLength));
            HighlightRange(rtb, firstCharPosition, lastCharPosition, lineNumber, color);
        }

        private void HighlightRange(
            RichTextBox rtb,
            int firstCharPosition,
            int lastCharPosition,
            int lineNumber,
            Color color)
        {
            firstCharPosition = Math.Max(0, Math.Min(firstCharPosition, rtb.TextLength));
            lastCharPosition = Math.Max(firstCharPosition, Math.Min(lastCharPosition, rtb.TextLength));

            int selectionStart = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;
            bool wasUpdatingSelection = updatingSelection;
            updatingSelection = true;

            try
            {
                int previousLine;
                if (lastHighlightedLine.TryGetValue(rtb, out previousLine)
                    && previousLine != lineNumber
                    && previousLine >= 0)
                {
                    int previousFirstChar = rtb.GetFirstCharIndexFromLine(previousLine);
                    if (previousFirstChar >= 0)
                    {
                        int previousLastChar = rtb.GetFirstCharIndexFromLine(previousLine + 1);
                        if (previousLastChar < 0)
                            previousLastChar = rtb.TextLength;

                        rtb.SelectionStart = previousFirstChar;
                        rtb.SelectionLength = Math.Max(0, previousLastChar - previousFirstChar);
                        rtb.SelectionBackColor = SystemColors.Window;
                    }
                }

                lastHighlightedLine[rtb] = lineNumber;

                if (lastCharPosition > firstCharPosition)
                {
                    rtb.SelectionStart = firstCharPosition;
                    rtb.SelectionLength = lastCharPosition - firstCharPosition;
                    rtb.SelectionBackColor = color;
                }

                rtb.SelectionStart = selectionStart;
                rtb.SelectionLength = selectionLength;
            }
            finally
            {
                updatingSelection = wasUpdatingSelection;
            }
        }

        private void txtOrig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                try
                {
                    if (Clipboard.ContainsText())
                        txtOrig.SelectedText = Clipboard.GetText();
                }
                catch (ExternalException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void splitContainer1_DoubleClick(object sender, EventArgs e)
        {
            RestoreSplitterPosition();
        }

        private void RestoreSplitterPosition()
        {
            int minimum = splitContainer1.Panel1MinSize;
            int maximum = splitContainer1.Width - splitContainer1.Panel2MinSize - splitContainer1.SplitterWidth;

            if (maximum < minimum)
                return;

            splitContainer1.SplitterDistance = Math.Max(minimum, Math.Min(splitterPosition, maximum));
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            splitterToolTip.Dispose();
            base.OnFormClosed(e);
        }
    }
}
