namespace gxViewer.GxDebugger.Helpers
{
    using FastColoredTextBoxNS;
    using System.Drawing;

    public static class CodeLineHighlighter
    {
        private static Style _highlightStyle = new TextStyle(Brushes.White, Brushes.DarkBlue, FontStyle.Bold);
        private static Style _defaultStyle = new TextStyle(Brushes.Black, null, FontStyle.Regular);

        public static void ResaltarLinea(FastColoredTextBox box, int numeroLinea)
        {
            box.ClearStylesBuffer();
            box.Range.ClearStyle(_highlightStyle);
            if (numeroLinea >= 0 && numeroLinea < box.LinesCount)
            {
                var linea = box.Lines[numeroLinea];
                box.GetLine(numeroLinea).SetStyle(_highlightStyle);
                box.Invalidate();
            }
        }
    }
}
