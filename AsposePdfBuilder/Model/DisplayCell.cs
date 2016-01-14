using Aspose.Pdf.Generator;

namespace AsposePdfBuilder.Model
{
    /// <summary>
    /// DisplayCell encapsulates the common properties necessary to create a cell with content 
    /// in Aspose.Pdf. A cell can also contain <see cref="TextAreaInfo"/> to be rendered as well.
    /// </summary>
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
