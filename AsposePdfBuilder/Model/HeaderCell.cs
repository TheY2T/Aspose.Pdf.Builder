using Aspose.Pdf.Generator;

namespace AsposePdfBuilder.Model
{
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
