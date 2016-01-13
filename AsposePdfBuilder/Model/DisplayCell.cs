using Aspose.Pdf.Generator;

namespace AsposePdfBuilder.Model
{
    public class DisplayCell
    {
        public int ColumnSpan { get; set; }
        public int RowSpan { get; set; }
        public string Content { get; set; }
        public bool IsDate { get; set; }
        public bool IsHeader { get; set; }
        public bool IsTotal { get; set; }
        public bool IsCurrency { get; set; }
        public bool IsEmpty { get; set; }
        public bool NeedCellBorder { get; set; }
        public bool IsCentralized { get; set; }
        public TextAreaInfo TextAreaInfo { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public Table TableInfo { get; set; }
        public BorderSide Border { get; set; }
        public bool IsCheckBox { get; set; }
        public bool IsChecked { get; set; }
    }
}
