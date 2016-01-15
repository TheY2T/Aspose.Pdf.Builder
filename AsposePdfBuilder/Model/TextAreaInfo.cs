﻿namespace Aspose.Pdf.Builder.Model
{
    /// <summary>
    /// TextAreaInfo encapsulates the common properties necessary to 
    /// create a 'TextArea' (similar to HTML TextArea) in Aspose.Pdf.
    /// </summary>
    public class TextAreaInfo
    {
        public string ColumnWidths { get; set; }
        public bool WithBorder { get; set; }
        public bool WithRoundedCorners { get; set; }
        public Generator.MarginInfo Padding { get; set; }
    }
}
