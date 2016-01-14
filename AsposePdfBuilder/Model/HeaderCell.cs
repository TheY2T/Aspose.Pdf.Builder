using Aspose.Pdf.Generator;

namespace AsposePdfBuilder.Model
{
    /// <summary>
    /// HeaderCell encapsulates the common properties necessary to create 
    /// a 'Header' in a table (similar to 'th' in HTML) in Aspose.Pdf.
    /// </summary>
    public class HeaderCell
    {
        public string Content { get; set; }
        public string BackgroundColor { get; set; }
        public BorderSide Border { get; set; }
        public int RowSpan { get; set; }
        public bool IsNoBorder { get; set; }
        public bool IsCentralized { get; set; }
    }
}
