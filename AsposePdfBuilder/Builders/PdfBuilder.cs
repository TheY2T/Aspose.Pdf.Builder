using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Generator;
using AsposePdfBuilder.Extensions;
using AsposePdfBuilder.Model;
using AsposePdfBuilder.Properties;

namespace AsposePdfBuilder.Builders
{
    public class PdfBuilder
    {
        #region Properties

        /// <summary>
        /// <see cref="Pdf"/> Used for all formatting and layout.
        /// </summary>
        internal Pdf AsposePdf { get; set; }

        /// <summary>
        /// Currently used <see cref="Section"/> for formatting and layout.
        /// </summary>
        private Section Section { get; set; }

        /// <summary>
        /// Create a typical <see cref="DisplayCell.IsEmpty"/> = true row.
        /// </summary>
        public static DisplayCell EmptyRow { get { return new DisplayCell { IsEmpty = true }; } }

        /// <summary>
        /// Default value for 'style' depth (level).
        /// </summary>
        public const int DefaultStyleLevel = 1;

        /// <summary>
        /// Default value for the distance from the edge of an A4 page.
        /// </summary>
        public const float A4PageDistanceFromEdge = 1.27f;

        /// <summary>
        /// Default value for the size of a typical border.
        /// </summary>
        public const float DefaultBorderSize = 0.1f;

        /// <summary>
        /// Default Page Padding (Top, Left, Right, Bottom) for an A4 page.
        /// </summary>
        public const float DefaultPageMarginInCm = 2.54f;

        /// <summary>
        /// Default Corner Radius for borders.
        /// </summary>
        public static readonly float DefaultCornerRadius = 10F;

        /// <summary>
        /// Standard Header column span (typically used in Outer Table Header Labels).
        /// </summary>
        public const int DefaultHeaderColumnSpan = 2;

        /// <summary>
        /// Standard column span (typically used in Inner Table Cells).
        /// </summary>
        public const int DefaultColumnSpan = 1;

        /// <summary>
        /// Standard row span (typically used in Inner Table Cells).
        /// </summary>
        public const int DefaultRowSpan = 1;

        /// <summary>
        /// Row span = '2' (typically used in Inner Table Cells).
        /// </summary>
        public const int RowSpanTwo = 2;

        /// <summary>
        /// Standard nested column span (typically used in Inner Table Cells).
        /// </summary>
        public const int DefaultNestedColumnSpan = 2;

        /// <summary>
        /// Default Line Spacing for Cells.
        /// </summary>
        public const int DefaultLineSpacing = 3;

        /// <summary>
        /// Default Line Spacing for TextArea Cells.
        /// </summary>
        public const int TextAreaLineSpacing = 1;

        /// <summary>
        /// Default Offset for bullet points.
        /// </summary>
        public const float DefaultBulletPointOffset = 5.0f;

        /// <summary>
        /// Default Offset for Checkboxes.
        /// </summary>
        public const float DefaultCheckBoxOffset = -2.0f;

        /// <summary>
        /// Default row height for inner table rows.
        /// </summary>
        public const float InnerTableRowHeight = 40.0f;

        /// <summary>
        /// Default margin size for any (outer) control.
        /// </summary>
        public static readonly MarginInfo DefaultOuterPadding =
            new MarginInfo { Top = 2f, Left = 0f, Right = 2f, Bottom = 2f };

        /// <summary>
        /// Default margin size for (inner) control.
        /// </summary>
        public static readonly MarginInfo DefaultInnerPadding =
            new MarginInfo { Left = 3.5f, Right = 3.5f, Bottom = 3.5f, Top = 3.5f };

        /// <summary>
        /// Default margin size for Sub Heading text row.
        /// </summary>
        public static readonly MarginInfo DefaultSubHeadingPadding =
            new MarginInfo { Left = 0f, Right = 4f, Bottom = 0f, Top = 0f };

        /// <summary>
        /// Default margin size for Sub Heading text bottom padding row.
        /// </summary>
        public static readonly MarginInfo DefaultSubHeadingBottomPadding =
            new MarginInfo { Bottom = 3f };

        /// <summary>
        /// Extra margin size for Outer text bottom padding row.
        /// </summary>
        public static readonly MarginInfo DefaultOuterBottomPadding =
            new MarginInfo { Bottom = 8f };

        /// <summary>
        /// Default margin size for Sub Heading text top & bottom padding row.
        /// </summary>
        public static readonly MarginInfo DefaultSubHeadingTopBottomPadding =
            new MarginInfo { Bottom = 3f, Top = 5f };

        /// <summary>
        /// Default margin size for TextArea Label padding.
        /// </summary>
        public static readonly MarginInfo DefaultTextAreaLabelPadding =
            new MarginInfo { Left = 0f, Right = 0f, Bottom = 4f, Top = 0f };

        /// <summary>
        /// Default margin size for TextArea Description padding.
        /// </summary>
        public static readonly MarginInfo DefaultTextAreaDescriptionPadding =
            new MarginInfo { Left = 4f, Right = 4f, Bottom = 4f, Top = 4f };

        /// <summary>
        /// Default margin size for TextArea Paragraph padding.
        /// </summary>
        public static readonly MarginInfo DefaultTextAreaParagraphPadding =
            new MarginInfo { Left = 0f, Right = 0f, Bottom = 4f, Top = 4f };

        /// <summary>
        /// Margin size for zero padding.
        /// </summary>
        public static readonly MarginInfo ZeroPadding =
            new MarginInfo { Left = 0f, Right = 0f, Bottom = 0f, Top = 0f };

        /// <summary>
        /// Margin size for top padding.
        /// </summary>
        public static readonly MarginInfo TopPadding = new MarginInfo { Top = 10f };

        /// <summary>
        /// Margin size for a Heading with no obvious sub heading. Typically used in
        /// cases where a row or table is immediately shown without a subheading prior.
        /// </summary>
        public static readonly MarginInfo HeadingWithNoSubHeadingPadding = new MarginInfo { Bottom = 12f };

        /// <summary>
        /// Margin size for a table that needs a 'blank row'. May be used in cases where it is not possible 
        /// to use <see cref="BlankRow(Aspose.Pdf.Generator.BorderSide,Aspose.Pdf.Generator.Table,bool)"/>.
        /// </summary>
        public static readonly MarginInfo InternalBlankRowPadding = new MarginInfo { Bottom = 20f };

        /// <summary>
        /// Compiler ready default for <see cref="Resources.HeadingOneFontSize"/>.
        /// </summary>
        public static readonly float DefaultHeadingOneFontSize = float.Parse(Resources.HeadingOneFontSize);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.HeadingTwoFontSize"/>.
        /// </summary>
        public static readonly float DefaultHeadingTwoFontSize = float.Parse(Resources.HeadingTwoFontSize);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.HeadingThreeFontSize"/>.
        /// </summary>
        public static readonly float DefaultHeadingThreeFontSize = float.Parse(Resources.HeadingThreeFontSize);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.HeadingFourFontSize"/>.
        /// </summary>
        public static readonly float DefaultHeadingFourFontSize = float.Parse(Resources.HeadingFourFontSize);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.BodyFontSize"/>.
        /// </summary>
        public static readonly float DefaultInnerBodyFontSize = float.Parse(Resources.BodyFontSize);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.BulletPointFontSize"/>.
        /// </summary>
        public static readonly float DefaultBulletPointFontSize = float.Parse(Resources.BulletPointFontSize);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.ColumnSpanThreeWidth"/>.
        /// </summary>
        public const string DefaultColumnSpanThreeWidth = "350";

        /// <summary>
        /// Compiler ready default for <see cref="Resources.TextAreaColumnWidths"/>.
        /// </summary>
        public const string DefaultTextAreaColumnWidth = "450";

        /// <summary>
        /// Compiler ready default for <see cref="Resources.DefaultColumnSpanOneWidth"/>.
        /// </summary>
        public const string DefaultColumnSpanOneWidth = "105";

        /// <summary>
        /// Compiler ready default for <see cref="Resources.TextAreaRowHeight"/>.
        /// </summary>
        public static readonly float DefaultTextAreaRowHeight = float.Parse(Resources.TextAreaRowHeight);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.BlankRowHeight"/>.
        /// </summary>
        public static readonly float DefaultBlankRowHeight = float.Parse(Resources.BlankRowHeight);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.BlankRowContactsHeight"/>.
        /// </summary>
        public static readonly float DefaultBlankRowContactsHeight = float.Parse(Resources.BlankRowContactsHeight);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.ShortBlankRowHeight"/>.
        /// </summary>
        public static readonly float ShortBlankRowHeight = float.Parse(Resources.ShortBlankRowHeight);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.BlankRowTextAreaHeight"/>.
        /// </summary>
        public static readonly float DefaultBlankRowTextAreaHeight = float.Parse(Resources.BlankRowTextAreaHeight);

        /// <summary>
        /// Compiler ready default for <see cref="Resources.TableBlankRowHeight"/>.
        /// </summary>
        public static readonly float DefaultTableBlankRowHeight = float.Parse(Resources.TableBlankRowHeight);

        #endregion

        /// <summary>
        /// Creates a default A4 pdf and section for use.
        /// </summary>
        public PdfBuilder()
        {
            AsposePdf = CreateDefaultA4PdfAndPageLayout();
            Section = AsposePdf.Sections.Add();
        }

        /// <summary>
        /// User can specify <paramref name="pageMargin"/>, <paramref name="height"/> and <paramref name="width"/>
        /// to create a PDF fit for their purposes. 
        /// Example usage: 
        ///     new PdfBuilder(2.54, PageSize.A0Height, PageSize.A0Width);
        ///     new PdfBuilder(0.5, PageSize.LedgerHeight, PageSize.LedgerWidth);
        ///     new PdfBuilder(1.00, PageSize.B5Height, PageSize.B5Width); etc.
        /// </summary>
        /// <param name="pageMargin">pageMargin (interpreted in cm)</param>
        /// <param name="height">height (interpreted in cm)</param>
        /// <param name="width">width (interpreted in cm)</param>
        public PdfBuilder(float pageMargin, float height, float width)
        {
            AsposePdf = CreatePdfAndPageLayoutWithUserDefinedValues(pageMargin, height, width);
            Section = AsposePdf.Sections.Add();
        }

        #region Create & Save Methods

        /// <summary>
        /// Creates an A4 pdf with respective default margins of 2.54cm.
        /// </summary>
        /// <returns>pdf</returns>
        private static Pdf CreateDefaultA4PdfAndPageLayout()
        {
            // default margin is 2.54cm - standard for A4 documents.
            var defaultMargin = float.Parse(Resources.OneCm) * DefaultPageMarginInCm;
            return new Pdf
            {
                PageSetup =
                {
                    PageHeight = PageSize.A4Height,
                    PageWidth = PageSize.A4Width,
                    Margin = new MarginInfo
                    {
                        Bottom = defaultMargin,
                        Left = defaultMargin,
                        Right = defaultMargin,
                        Top = defaultMargin
                    }
                }
            };
        }

        /// <summary>
        /// Creates a pdf with user specified margins and page width / height.
        /// NOTE: Please use <see cref="PageSize"/> to get desired the page layout height and width.
        /// </summary>
        /// <returns>pdf</returns>
        private static Pdf CreatePdfAndPageLayoutWithUserDefinedValues(float pageMargin, float height, float width)
        {
            return new Pdf
            {
                PageSetup =
                {
                    PageHeight = height,
                    PageWidth = width,
                    Margin = new MarginInfo
                    {
                        Bottom = pageMargin,
                        Left = pageMargin,
                        Right = pageMargin,
                        Top = pageMargin
                    }
                }
            };
        }

        /// <summary>
        /// Saves (renders) the <see cref="Pdf"/>PdfBuilder and returns the result as a byte[] representation.
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] SaveFileStream()
        {
            using (var mStream = new MemoryStream())
            {
                AsposePdf.Save(mStream);
                return mStream.ToArray();
            }
        }

        #endregion

        #region Header & Footer Methods

        /// <summary>
        /// Creates a Header and Footer <see cref="HeaderFooter"/> for the specified section at hand.
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="headerText">headerText</param>
        /// <param name="footerText">footerText</param>
        public void CreateHeaderAndFooterForPdfForSection(Section section, string headerText, string footerText)
        {
            // Create a Header Section of the PDF file
            var header = new HeaderFooter(section)
            {
                // Add Distance From Edge Property to 1.27cm
                DistanceFromEdge = float.Parse(Resources.OneCm) * A4PageDistanceFromEdge,
            };

            // set Odd and Even Headers
            section.OddHeader = header;
            section.EvenHeader = header;

            // Set with column widths & default cell border of the table
            var headerTable = new Table
            {
                DefaultCellBorder = new BorderInfo((int)BorderSide.None, DefaultBorderSize),
                ColumnWidths = section.IsLandscape
                    ? Resources.HeaderRowLandscapeColumnWidths
                    : Resources.HeaderRowPortraitColumnWidths

            };
            header.Paragraphs.Add(headerTable);

            // Create header row and cell content
            var headerRow = headerTable.Rows.Add();
            headerRow.DefaultCellTextInfo.FontName = Resources.HelveticaFont;
            headerRow.DefaultCellTextInfo.FontSize = int.Parse(Resources.HeaderFontSize);

            // Set the vertical alignment of the cell as center aligned
            var headerCell = headerRow.Cells.Add(headerText);
            headerCell.VerticalAlignment = VerticalAlignmentType.Center;
            headerCell.Alignment = AlignmentType.Center;

            //=====================================================//
            //	Footer to show Page Number
            //=====================================================//

            // Create Footer Section
            var footer = new HeaderFooter(section)
            {
                //Add Distance From Edge Property to 1.27cm
                DistanceFromEdge = float.Parse(Resources.OneCm) * A4PageDistanceFromEdge
            };

            // set Odd and Even Headers
            section.OddFooter = footer;
            section.EvenFooter = footer;

            // Add footer table
            var footerTable = new Table
            {
                DefaultCellBorder = new BorderInfo((int)BorderSide.None, DefaultBorderSize),
                ColumnWidths = section.IsLandscape
                    ? Resources.FooterRowLandscapeColumnWidths
                    : Resources.FooterRowPortraitColumnWidths
            };

            // Set with column widths of the table
            footer.Paragraphs.Add(footerTable);

            // Add footer row
            var footerRow = footerTable.Rows.Add();
            footerRow.DefaultCellTextInfo.FontName = Resources.HelveticaFont;
            footerRow.DefaultCellTextInfo.FontSize = int.Parse(Resources.FooterRowFontSize);

            // Add foot text
            var footerCell = footerRow.Cells.Add(footerText);
            footerCell.Alignment = AlignmentType.Left;

            // Add page number
            var pageFooterCell = footerRow.Cells.Add(string.Format("Page {0} of {1}", "$p", "$P"));
            pageFooterCell.VerticalAlignment = VerticalAlignmentType.Center;
            pageFooterCell.Alignment = AlignmentType.Right;
        }

        #endregion

        #region Section Methods

        /// <summary>
        /// Returns a new <see cref="Section"/>.
        /// </summary>
        /// <returns><see cref="Section"/></returns>
        public void CreatePortraitSection()
        {
            // Create a section for the pdf object
            var section = AsposePdf.Sections.Add();
            section.IsLandscape = false;
            Section = section;
        }

        /// <summary>
        /// Returns a new <see cref="Section"/> with 
        /// <see cref="Aspose.Pdf.Generator.Section.IsLandscape"/> set to true.
        /// </summary>
        /// <returns><see cref="Section"/></returns>
        public void CreateLandscapeSection()
        {
            // Create a section for the pdf object
            var section = AsposePdf.Sections.Add();
            section.IsLandscape = true;
            Section = section;
        }

        /// <summary>
        /// Retrieves the current section in use. 
        /// NOTE: It is recommended that you only use this in specific cases. The PdfBuilder manipulates the <see cref="Section"/> 
        /// through its available public methods. Use those methods over this if possible, in order to avoid any unexpected behaviour.
        /// </summary>
        /// <returns>the current Section in use</returns>
        public Section GetSection()
        {
            return Section;
        }

        #endregion

        #region Row (Blank/SubHeading) Methods

        /// <summary>
        /// Adds a blank row for formatting in a given <see cref="Section"/>.
        /// NOTE: <see cref="PdfBuilder.Section"/> is maintained in this 
        /// class internally.
        /// </summary>
        /// <param name="border">border</param>
        /// <param name="table">table</param>
        /// <param name="isShortRow">isShortRow</param>
        public void BlankRow(
            BorderSide border = BorderSide.None,
            Table table = null,
            bool isShortRow = false)
        {
            if (table == null)
            {
                table = new Table { IsFixedRowHeightContentClip = true };
                Section.Paragraphs.Add(table);
                var row = table.Rows.Add();
                row.FixedRowHeight = !isShortRow
                    ? DefaultBlankRowHeight
                    : ShortBlankRowHeight;
                row.Border = new BorderInfo((int)border, DefaultBorderSize);
            }
            else
            {
                // add blank row to existing table
                var row = table.Rows.Add();
                var cell = row.Cells.Add();
                var blankRowTable = new Table(cell) { IsFixedRowHeightContentClip = true };
                cell.Paragraphs.Add(blankRowTable);
                row = blankRowTable.Rows.Add();
                row.FixedRowHeight = !isShortRow
                    ? DefaultBlankRowHeight
                    : ShortBlankRowHeight;
                row.Border = new BorderInfo((int)border, DefaultBorderSize);
            }
        }

        /// <summary>
        /// Adds a blank row for formatting in a given <see cref="parentCell"/>.
        /// Specify <see cref="fixedRowHeight"/> for precise formatting.
        /// </summary>
        /// <param name="parentCell">parentCell</param>
        /// <param name="fixedRowHeight">fixedRowHeight</param>
        /// <param name="isShortRow">isShortRow</param>
        /// <param name="border">border</param>
        public void BlankRow(
            Cell parentCell,
            float fixedRowHeight = 0.0f,
            bool isShortRow = false,
            BorderSide border = BorderSide.None)
        {
            var table = new Table { IsFixedRowHeightContentClip = true };
            if (fixedRowHeight > 0.0f)
            {
                var row = table.Rows.Add();
                row.FixedRowHeight = !isShortRow
                    ? DefaultBlankRowTextAreaHeight
                    : ShortBlankRowHeight;
                row.Border = new BorderInfo((int)border, DefaultBorderSize);
            }
            parentCell.Paragraphs.Add(table);
        }

        /// <summary>
        /// Adds a blank row for formatting in a given <see cref="table"/>.
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="isShortRow">isShortRow</param>
        public void BlankRow(Table table, bool isShortRow = false)
        {
            if (table == null)
            {
                BlankRow(isShortRow: isShortRow);
            }
            else
            {
                var row = table.Rows.Add();
                if (!row.IsInNewPage)
                {
                    row.FixedRowHeight = isShortRow ? ShortBlankRowHeight : DefaultTableBlankRowHeight;
                }
            }
        }

        /// <summary>
        /// Create a sub heading row. User must specify <see cref="subHeadingText"/>
        /// and a <see cref="Table"/> to add the row to.
        /// If the user specifies <see cref="isFullPageText"/> the sub heading will be rendered
        /// across the entire page and not to the default of 'three' columns (four is total).
        /// </summary>
        /// <param name="subHeadingText">subHeadingText</param>
        /// <param name="table">table</param>
        /// <param name="margins">margins</param>
        /// <param name="isFullPageText">isFullPageText</param>
        public void CreateSubHeadingRow(
            string subHeadingText,
            Table table,
            MarginInfo margins = null,
            bool isFullPageText = false)
        {
            var subHeadingTable = !isFullPageText
                ? table
                : new Table { ColumnWidths = Resources.DefaultFullWidth };
            var row = subHeadingTable.Rows.Add();
            var cell = row.Cells.Add(string.Empty);
            cell.ColumnsSpan = DefaultHeaderColumnSpan;
            cell.Padding = DefaultSubHeadingPadding;
            var paragraph = new Text(subHeadingText)
            {
                TextInfo =
                {
                    FontName = Resources.ArialFont,
                    FontSize = int.Parse(Resources.HeadingTwoFontSize),
                    Color = new Color(Resources.BlackColor),
                    IsTrueTypeFontBold = true
                }
            };
            if (margins != null)
                paragraph.Margin = margins;
            cell.Paragraphs.Add(paragraph);
        }

        #endregion

        #region Nested Table Methods

        /// <summary>
        /// Creates and adds an 'Outer Table' into <see cref="parentTable"/> so the user
        /// is able to add tables within a parent table.
        /// </summary>
        /// <param name="parentTable">parentTable</param>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="font">font</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="defaultPadding">defaultPadding</param>
        /// <param name="keepContentTogether">keepContentTogether</param>
        /// <param name="isKeptTogether">isKeptTogether</param>
        /// <param name="isKeptWithNext">isKeptWithNext</param>
        /// <param name="columnSpan">columnSpan</param>
        /// <returns>inserted table</returns>
        public Table AddNestedTable(
            Table parentTable,
            string columnWidths,
            string font,
            float fontSize,
            MarginInfo defaultPadding = null,
            bool keepContentTogether = false,
            bool isKeptTogether = false,
            bool isKeptWithNext = false,
            int columnSpan = DefaultNestedColumnSpan)
        {
            var row = parentTable.Rows.Add();
            var cell = row.Cells.Add();

            cell.ColumnsSpan = columnSpan;
            var table = new Table(cell)
            {
                ColumnWidths = columnWidths,
                DefaultCellPadding = defaultPadding ?? DefaultOuterPadding,
                DefaultCellTextInfo = { FontName = font, FontSize = fontSize },
                IsBroken = !keepContentTogether,
                IsKeptTogether = isKeptTogether,
                IsKeptWithNext = isKeptWithNext
            };
            cell.Paragraphs.Add(table);
            return table;
        }

        /// <summary>
        /// Create a standard <see cref="Table"/> from a given <see cref="parentTable"/>.
        /// This Table will be stored in a new row and harbouring cell from
        /// <see cref="parentTable"/>, the user must specify <see cref="columnWidths"/> 
        /// and the new table column widths will be adjusted to these values.
        /// </summary>
        /// <param name="parentTable">parentTable</param>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="font">font</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="parentPadding">parentPadding</param>
        /// <param name="isKeptTogether">isKeptTogether</param>
        /// <param name="isKeptWithNext">isKeptWithNext</param>
        /// <param name="keepContentTogether">keepContentTogether</param>
        /// <returns>row</returns>
        public Table AddNestedTableFromTable(
            Table parentTable,
            string columnWidths,
            string font,
            float fontSize,
            MarginInfo parentPadding = null,
            bool isKeptTogether = false,
            bool isKeptWithNext = false,
            bool keepContentTogether = false)
        {
            var row = parentTable.Rows.Add();
            var cell = row.Cells.Add();
            cell.IsNoBorder = true;
            cell.ColumnsSpan = GetColumnSpan(parentTable);
            // Set the default cell padding to parentPadding definition
            row.DefaultRowCellPadding = parentPadding ?? DefaultInnerPadding;
            var table = new Table(cell)
            {
                ColumnWidths = columnWidths,
                DefaultCellPadding = parentPadding ?? DefaultOuterPadding,
                DefaultCellTextInfo = { FontName = font, FontSize = fontSize },
                IsBroken = !keepContentTogether,
                IsKeptTogether = isKeptTogether,
                IsKeptWithNext = isKeptWithNext
            };
            cell.Paragraphs.Add(table);
            return table;
        }

        /// <summary>
        /// Create a standard <see cref="Table"/> from a given <see cref="row"/>.
        /// This Table will be stored in a new row and harbouring cell from
        /// <see cref="row"/>, the user must specify <see cref="columnWidths"/> 
        /// and the new table column widths will be adjusted to these values.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="font">font</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="parentPadding">parentPadding</param>
        /// <param name="isKeptTogether">isKeptTogether</param>
        /// <param name="isKeptWithNext">isKeptWithNext</param>
        /// <param name="keepContentTogether">keepContentTogether</param>
        /// <returns>row</returns>
        public Table AddNestedTableFromRow(
            Row row,
            string columnWidths,
            string font,
            float fontSize,
            MarginInfo parentPadding = null,
            bool isKeptTogether = false,
            bool isKeptWithNext = false,
            bool keepContentTogether = false)
        {
            var cell = row.Cells.Add();
            cell.IsNoBorder = true;
            // Set the default cell padding to parentPadding definition
            row.DefaultRowCellPadding = parentPadding ?? DefaultInnerPadding;
            var table = new Table(cell)
            {
                ColumnWidths = columnWidths,
                DefaultCellPadding = parentPadding ?? DefaultOuterPadding,
                DefaultCellTextInfo = { FontName = font, FontSize = fontSize },
                IsBroken = !keepContentTogether,
                IsKeptTogether = isKeptTogether,
                IsKeptWithNext = isKeptWithNext
            };
            cell.Paragraphs.Add(table);
            return table;
        }

        #endregion

        #region Bullet Point, Numbered Style, Checkbox Methods

        /// <summary>
        /// Creates a dot point '•', representation along with the <see cref="description"/> provided.
        /// </summary>
        /// <param name="description">description</param>
        /// <param name="bulletStyle">bulletStyle</param>
        /// <param name="level">level</param>
        /// <param name="parentTable">parentTable</param>
        public void CreateBulletPointStyle(
            string description,
            string bulletStyle = "",
            int level = DefaultStyleLevel,
            Table parentTable = null)
        {
            var heading = new Heading(AsposePdf, new Section(), DefaultStyleLevel)
            {
                BulletOffset = DefaultBulletPointOffset,
                UserLabel = !string.IsNullOrEmpty(bulletStyle)
                    ? bulletStyle
                    : Resources.DefaultBulletStyle,
                LabelFontSize = DefaultBulletPointFontSize,
                TextInfo =
                {
                    FontName = Resources.ArialFont,
                    FontSize = DefaultHeadingTwoFontSize
                }
            };

            var segment = new Segment(heading)
            {
                Content = description
            };
            heading.Segments.Add(segment);

            if (parentTable != null)
            {
                var row = parentTable.Rows.Add();
                var cell = row.Cells.Add();
                cell.Paragraphs.Add(heading);
            }
        }

        /// <summary>
        /// Creates a numbered style '1.', representation along with the <see cref="description"/> provided.
        /// </summary>
        /// <param name="description">description</param>
        /// <param name="level">level</param>
        /// <param name="parentTable">parentTable</param>
        public void CreateNumberedStyle(
            string description,
            Table parentTable,
            int level = DefaultStyleLevel)
        {
            // create heading object and add set its level
            var heading = new Heading(AsposePdf, new Section(), level)
            {
                IsAutoSequence = true,
                IsInList = true,
                TextInfo =
                {
                    FontName = Resources.ArialFont,
                    FontSize = DefaultHeadingTwoFontSize
                },
                LabelPattern = ".",
                LabelFontName = Resources.ArialFont,
                LabelFontSize = DefaultHeadingTwoFontSize,
                HeadingType = HeadingType.Arab,
            };
            var segment = new Segment(heading);
            heading.Segments.Add(segment);
            segment.Content = description;

            var headingTable = new Table
            {
                ColumnWidths = Resources.ColumnSpanFourWidth
            };
            var headingTableRow = headingTable.Rows.Add();
            var headingTableRowCell = headingTableRow.Cells.Add();
            headingTableRowCell.Paragraphs.Add(heading);

            var row = parentTable.Rows.Add();
            var cell = row.Cells.Add();
            cell.ColumnsSpan = 2;
            cell.Paragraphs.Add(headingTable);
        }

        /// <summary>
        /// Creates a checkbox.
        /// </summary>
        private Heading CreateCheckBox()
        {
            var checkbox = new Heading(AsposePdf, new Section(), DefaultStyleLevel)
            {
                BulletOffset = DefaultCheckBoxOffset,
                UserLabel = Resources.DefaultBulletFiveStyle,
                TextInfo =
                {
                    FontName = Resources.ArialFont,
                    FontSize = DefaultHeadingTwoFontSize
                }
            };
            var segment = new Segment(checkbox)
            {
                Content = string.Empty
            };
            checkbox.Segments.Add(segment);
            return checkbox;
        }

        /// <summary>
        /// Creates a checkbox with a label attached.
        /// </summary>
        /// <param name="label">label</param>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="parentTable">parentTable</param>
        public void CreateLabelWithCheckBox(
            string label,
            string columnWidths = "",
            Table parentTable = null)
        {
            var checkbox = CreateCheckBox();
            if (parentTable != null)
            {
                var table = new Table
                {
                    ColumnWidths = !string.IsNullOrEmpty(columnWidths)
                        ? columnWidths
                        : Resources.CheckBoxColumnWidths
                };
                var row = table.Rows.Add();
                var cellOne = row.Cells.Add(label);
                cellOne.Padding = DefaultInnerPadding;
                cellOne.DefaultCellTextInfo.IsTrueTypeFontBold = true;
                cellOne.DefaultCellTextInfo.FontName = Resources.ArialFont;
                cellOne.DefaultCellTextInfo.FontSize = DefaultHeadingTwoFontSize;
                var cellTwo = row.Cells.Add();
                cellTwo.Padding = DefaultInnerPadding;
                cellTwo.DefaultCellTextInfo.IsTrueTypeFontBold = true;
                cellTwo.DefaultCellTextInfo.FontName = Resources.ArialFont;
                cellTwo.DefaultCellTextInfo.FontSize = DefaultHeadingTwoFontSize;
                cellTwo.Paragraphs.Add(checkbox);

                var parentRow = parentTable.Rows.Add();
                var parentCell = parentRow.Cells.Add();
                parentCell.Paragraphs.Add(table);
            }
        }

        #endregion

        #region Outer Table Methods

        /// <summary>
        /// Creates a standard Outer Table with the option of 'keeping all content together'
        /// by setting <see cref="keepContentTogether"/> to true, it will ensure the entire
        /// table rows at hand is not broken up across pages and will be treated as a single 
        /// entity.
        /// </summary>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="font">font</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="defaultPadding">defaultPadding</param>
        /// <param name="keepContentTogether">keepContentTogether</param>
        /// <param name="isNewPage">isNewPage</param>
        /// <param name="isKeptWithNext">isKeptWithNext</param>
        /// <param name="isKeptTogether">isKeptTogether</param>
        /// <param name="parentTable">parentTable</param>
        /// <returns>Outer Table</returns>
        public Table CreateOuterTable(
            string columnWidths,
            string font,
            float fontSize,
            MarginInfo defaultPadding = null,
            bool keepContentTogether = false,
            bool isNewPage = false,
            bool isKeptWithNext = false,
            bool isKeptTogether = false,
            Table parentTable = null)
        {
            // attach to parent table
            if (parentTable != null)
            {
                var row = parentTable.Rows.Add();
                var table = new Table
                {
                    ColumnWidths = columnWidths,
                    DefaultCellBorder = new BorderInfo((int)BorderSide.None, DefaultBorderSize),
                    Border = new BorderInfo((int)BorderSide.None, DefaultBorderSize),
                    DefaultCellPadding = defaultPadding ?? DefaultOuterPadding,
                    DefaultCellTextInfo = { FontName = font, FontSize = fontSize },
                    IsBroken = !keepContentTogether,
                    IsFirstParagraph = isNewPage,
                    IsKeptWithNext = isKeptWithNext
                };
                if (defaultPadding != null)
                {
                    table.DefaultCellPadding = defaultPadding;
                }
                var cell = row.Cells.Add();
                cell.Paragraphs.Add(table);
                return table;
            }
            else
            {
                var table = new Table
                {
                    ColumnWidths = columnWidths,
                    DefaultCellBorder = new BorderInfo((int)BorderSide.None, DefaultBorderSize),
                    Border = new BorderInfo((int)BorderSide.None, DefaultBorderSize),
                    DefaultCellTextInfo = { FontName = font, FontSize = fontSize },
                    IsBroken = !keepContentTogether,
                    IsFirstParagraph = isNewPage,
                    IsKeptWithNext = isKeptWithNext
                };
                if (defaultPadding != null)
                {
                    table.DefaultCellPadding = defaultPadding;
                }
                Section.Paragraphs.Add(table);
                return table;
            }
        }

        /// <summary>
        /// Creates an 'Outer Table Page Heading' <see cref="title"/>.
        /// </summary>
        /// <param name="parentTable">parentTable</param>
        /// <param name="title">title</param>
        /// <param name="columnSpan">columnSpan</param>
        /// <param name="padding">padding</param>
        /// <param name="useSpacing">useSpacing</param>
        public void CreateOuterTableRowHeading(
            Table parentTable,
            string title,
            int columnSpan = DefaultHeaderColumnSpan,
            MarginInfo padding = null,
            bool useSpacing = false)
        {
            var row = parentTable.Rows.Add();
            var cell = row.Cells.Add(title);
            cell.ColumnsSpan = columnSpan;
            cell.DefaultCellTextInfo.FontName = Resources.ArialFont;
            cell.DefaultCellTextInfo.FontSize = int.Parse(Resources.HeadingOneFontSize);
            cell.DefaultCellTextInfo.Color = new Color(Resources.HeaderTableBackgroundColor);
            cell.DefaultCellTextInfo.IsTrueTypeFontBold = true;
            if (padding != null)
            {
                cell.Padding = padding;
            }
            if (useSpacing)
            {
                cell.DefaultCellTextInfo.LineSpacing = DefaultLineSpacing;
            }
        }

        /// <summary>
        /// Creates an 'Outer Table Row'. You can optionally specify a <see cref="columnSpan"/>.
        /// Everything else must be supplied.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="title">title</param>
        /// <param name="fullDescription">fullDescription</param>
        /// <param name="columnSpan">columnSpan</param>
        /// <param name="padding">padding</param>
        /// <param name="descriptionAlignment">descriptionAlignment</param>
        /// <param name="useSpacing">useSpacing</param>
        public void CreateOuterTableRowCells(
            Row row,
            string title,
            string fullDescription,
            int columnSpan = DefaultHeaderColumnSpan,
            MarginInfo padding = null,
            AlignmentType descriptionAlignment = AlignmentType.Left,
            bool useSpacing = false)
        {
            var labelCell = row.Cells.Add(title);
            var descriptionColor = new Color(Resources.GreyColor);
            labelCell.DefaultCellTextInfo.Color = descriptionColor;
            labelCell.DefaultCellTextInfo.FontSize = DefaultHeadingTwoFontSize;
            labelCell.DefaultCellTextInfo.IsTrueTypeFontBold = true;

            var descriptionCell = row.Cells.Add(fullDescription);
            descriptionCell.DefaultCellTextInfo.FontName = Resources.ArialFont;
            descriptionCell.DefaultCellTextInfo.FontSize = DefaultHeadingTwoFontSize;
            descriptionCell.DefaultCellTextInfo.Color = descriptionColor;
            descriptionCell.Alignment = descriptionAlignment;

            if (useSpacing)
            {
                labelCell.DefaultCellTextInfo.LineSpacing = DefaultLineSpacing;
                descriptionCell.DefaultCellTextInfo.LineSpacing = DefaultLineSpacing;
            }
            if (padding != null)
            {
                labelCell.Padding = padding;
                descriptionCell.Padding = padding;
            }
        }

        #endregion

        #region Inner Table Methods

        /// <summary>
        /// Creates a standard 'Inner Table' with respective borders.
        /// By setting <see cref="keepContentTogether"/> to true, it will ensure the entire
        /// table rows at hand is not broken up across pages and will be treated as a single 
        /// entity.
        /// The inner table that is created can be nested in another table if you specify 
        /// <see cref="parentTable"/>.
        /// </summary>
        /// <param name="resourceColumnWidths">resourceColumnWidths</param>
        /// <param name="parentTable">parentTable</param>
        /// <param name="borderSide">borderSide</param>
        /// <param name="fontName">fontName</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="alignment">alignment</param>
        /// <param name="padding">padding</param>
        /// <param name="keepContentTogether">keepContentTogether</param>
        /// <param name="isKeptWithNext">isKeptWithNext</param>
        /// <param name="isKeptTogether">isKeptTogether</param>
        /// <returns>Inner table</returns>
        public Table CreateInnerTable(
            string resourceColumnWidths,
            Table parentTable = null,
            BorderSide borderSide = BorderSide.All,
            string fontName = "",
            float fontSize = 0f,
            AlignmentType alignment = AlignmentType.Left,
            MarginInfo padding = null,
            bool keepContentTogether = false,
            bool isKeptWithNext = false,
            bool isKeptTogether = false)
        {
            Table table;
            if (parentTable != null)
            {
                var row = parentTable.Rows.Add();
                if (padding != null)
                {
                    row.DefaultRowCellPadding = padding;
                }
                var cell = row.Cells.Add();
                table = new Table(cell)
                {
                    ColumnWidths = resourceColumnWidths,
                    DefaultCellBorder = borderSide != BorderSide.None
                        ? new BorderInfo((int)borderSide, DefaultBorderSize)
                        : new BorderInfo((int)BorderSide.None),
                    DefaultCellTextInfo =
                    {
                        FontSize = fontSize > 0
                            ? fontSize
                            : DefaultInnerBodyFontSize,
                        FontName = !string.IsNullOrEmpty(fontName)
                            ? fontName
                            : Resources.ArialFont
                    },
                    Alignment = alignment,
                    IsBroken = !keepContentTogether,
                    IsFirstRowRepeated = true,
                    IsKeptWithNext = isKeptWithNext,
                    IsKeptTogether = isKeptTogether
                };
                cell.IsNoBorder = true;
                cell.Paragraphs.Add(table);
            }
            else
            {
                table = new Table
                {
                    ColumnWidths = resourceColumnWidths,
                    DefaultCellBorder = borderSide != BorderSide.None
                        ? new BorderInfo((int)borderSide, DefaultBorderSize)
                        : new BorderInfo((int)BorderSide.None, DefaultBorderSize),
                    DefaultCellTextInfo =
                    {
                        FontSize = fontSize > 0
                            ? fontSize
                            : DefaultInnerBodyFontSize,
                        FontName = !string.IsNullOrEmpty(fontName)
                            ? fontName
                            : Resources.ArialFont
                    },
                    Alignment = alignment,
                    IsBroken = !keepContentTogether,
                    IsFirstRowRepeated = true,
                    IsKeptWithNext = isKeptWithNext
                };
                if (padding != null)
                {
                    table.DefaultCellPadding = padding;
                }
                Section.Paragraphs.Add(table);
            }

            return table;
        }

        /// <summary>
        /// Creates and formats a 'Header' styled <see cref="Cell"/> for a 
        /// given <see cref="row"/> and <see cref="headerCells"/> provided.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="headerCells">headerCells</param>
        public void CreateInnerTableHeaderCells(
            Row row,
            HeaderCell[] headerCells)
        {
            foreach (var headerCell in headerCells)
            {
                CreateInnerTableHeaderCell(row, headerCell);
            }
        }

        /// <summary>
        /// Creates and formats a 'Header' styled <see cref="Cell"/> 
        /// for a given <see cref="row"/> and <see cref="headerCell"/>.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="headerCell">headerCell</param>
        public void CreateInnerTableHeaderCell(
            Row row,
            HeaderCell headerCell)
        {
            row.IsBroken = false;
            var cell = row.Cells.Add(headerCell.Content);
            cell.Padding = DefaultInnerPadding;
            cell.BackgroundColor = !string.IsNullOrEmpty(headerCell.BackgroundColor)
                ? new Color(headerCell.BackgroundColor)
                : new Color(Resources.HeaderTableBackgroundColor);
            cell.DefaultCellTextInfo.Color = new Color(Resources.WhiteColor);
            cell.DefaultCellTextInfo.FontName = Resources.ArialFont;
            cell.DefaultCellTextInfo.FontSize = DefaultInnerBodyFontSize;
            cell.DefaultCellTextInfo.IsTrueTypeFontBold = true;
            cell.DefaultCellTextInfo.IsUnicode = true;
            cell.IsNoBorder = headerCell.IsNoBorder;

            if (headerCell.IsCentralized)
            {
                cell.Alignment = AlignmentType.Center;
            }
            if (headerCell.Border != BorderSide.None)
            {
                cell.Border = new BorderInfo((int)headerCell.Border, DefaultBorderSize);
            }
            if (headerCell.RowSpan > 0)
            {
                cell.RowSpan = headerCell.RowSpan;
            }
        }

        /// <summary>
        /// Creates & formats a nested inner table <see cref="Cell"/> to vertically align content within it.
        /// E.g. Catchment Areas (header) --> item 01
        ///                               --> item 02
        ///                               --> item 03 etc.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="descriptions">descriptions</param>
        /// <param name="fontName">fontName</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="parentPadding">parentPadding</param>
        public void CreateNestedInnerTableCell(
            Row row,
            IEnumerable<string> descriptions,
            string fontName = "",
            int fontSize = 0,
            MarginInfo parentPadding = null)
        {
            var table = new Table
            {
                DefaultCellTextInfo =
                {
                    FontName = !string.IsNullOrEmpty(fontName)
                    ? fontName
                    : Resources.ArialFont,
                    FontSize = fontSize > 0
                        ? fontSize
                        : DefaultInnerBodyFontSize
                }
            };

            // add descriptions
            foreach (var description in descriptions)
            {
                var tableRow = table.Rows.Add();
                tableRow.Cells.Add(description);
            }

            // add content back into parent cell
            var parentCell = row.Cells.Add();
            parentCell.Border = new BorderInfo((int)BorderSide.All, DefaultBorderSize);
            parentCell.Padding = parentPadding ?? DefaultInnerPadding;
            parentCell.BackgroundColor = new Color(Resources.InnerTableColor);
            parentCell.Paragraphs.Add(table);
        }

        /// <summary>
        /// Create a standard 'Inner Table Row' for a given <see cref="table"/>.
        /// NOTE: Remember to never put padding on a <see cref="Table"/> object, 
        /// it messes with the content layout inside <see cref="Row"/> objects etc.
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="parentPadding">parentPadding</param>
        /// <param name="verticalAlignment">verticalAlignment</param>
        /// <param name="backgroundColor">backgroundColor</param>
        /// <param name="withBorder">withBorder</param>
        /// <returns>row</returns>
        public Row CreateInnerTableRow(
            Table table,
            MarginInfo parentPadding = null,
            VerticalAlignmentType verticalAlignment = VerticalAlignmentType.Top,
            string backgroundColor = "",
            bool withBorder = false)
        {
            var row = table.Rows.Add();
            // Set the default cell padding to parentPadding definition
            row.DefaultRowCellPadding = parentPadding ?? DefaultInnerPadding;
            row.VerticalAlignment = verticalAlignment;
            row.IsBroken = false;
            row.BackgroundColor = new Color(
                !string.IsNullOrEmpty(backgroundColor)
                    ? backgroundColor
                    : Resources.InnerTableColor);
            if (withBorder)
            {
                row.DefaultCellBorder = new BorderInfo((int)BorderSide.All, DefaultBorderSize);
            }
            return row;
        }

        /// <summary>
        /// Create an 'Inner Table', but ensure that it is not bound to <see cref="Section"/>.
        /// This is handy if you want to nest inner tables successively and allows the user
        /// to manipulate the table along with any other cell they wish to inject it in.
        /// </summary>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="fontName">fontName</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="borderSide">borderSide</param>
        /// <param name="alignment">alignment</param>
        /// <param name="padding">padding</param>
        /// <param name="keepContentTogether">keepContentTogether</param>
        /// <param name="isKeptWithNext">isKeptWithNext</param>
        public Table CreateSimpleInnerTable(
            string columnWidths, 
            string fontName = "", 
            float fontSize = 0,
            BorderSide borderSide = BorderSide.None, 
            AlignmentType alignment = AlignmentType.Left, 
            MarginInfo padding = null, 
            bool keepContentTogether = false, 
            bool isKeptWithNext = false)
        {
            var table = new Table
            {
                ColumnWidths = columnWidths,
                DefaultCellTextInfo =
                {
                    FontSize = fontSize > 0
                        ? fontSize
                        : DefaultInnerBodyFontSize,
                    FontName = !string.IsNullOrEmpty(fontName)
                        ? fontName
                        : Resources.ArialFont
                },
                Alignment = alignment,
                IsBroken = !keepContentTogether,
                IsFirstRowRepeated = true,
                IsKeptWithNext = isKeptWithNext
            };
            if (borderSide != BorderSide.None)
            {
                table.Border = new BorderInfo((int) borderSide, DefaultBorderSize);
            }
            if (padding != null)
            {
                table.DefaultCellPadding = padding;
            }
            return table;
        }

        /// <summary>
        /// Creates Inner Table Item Cell(s). Must provide at least ONE <see cref="cellContent"/> 
        /// and an applicable <see cref="row"/>.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="defaultPadding">defaultPadding</param>
        /// <param name="backgroundColor">backgroundColor</param>
        /// <param name="textColor">textColor</param>
        /// <param name="fontName">fontName</param>
        /// <param name="fontSize">fontSize</param>
        /// <param name="verticalAlignment">verticalAlignment</param>
        /// <param name="alignment">alignment</param>
        /// <param name="fixedRowHeight">fixedRowHeight</param>
        /// <param name="cellContent">cellContent</param>
        public void CreateInnerTableItemCells(
            Row row,
            MarginInfo defaultPadding = null,
            string backgroundColor = "",
            string textColor = "",
            string fontName = "",
            int fontSize = 0,
            VerticalAlignmentType verticalAlignment = VerticalAlignmentType.Bottom,
            AlignmentType alignment = AlignmentType.Left,
            float fixedRowHeight = 0,
            params DisplayCell[] cellContent)
        {
            row.IsBroken = false;
            row.BackgroundColor = new Color(
                !string.IsNullOrEmpty(backgroundColor)
                    ? backgroundColor
                    : Resources.InnerTableColor);
            row.DefaultCellTextInfo.Color = new Color(
                !string.IsNullOrEmpty(textColor)
                    ? textColor
                    : Resources.GreyColor);
            row.DefaultCellTextInfo.FontName = !string.IsNullOrEmpty(fontName)
                ? fontName
                : Resources.ArialFont;
            row.DefaultCellTextInfo.FontSize = fontSize == 0
                ? DefaultInnerBodyFontSize
                : fontSize;
            row.DefaultCellTextInfo.Alignment = alignment;
            row.VerticalAlignment = verticalAlignment;
            row.DefaultRowCellPadding = defaultPadding ?? DefaultInnerPadding;
            if (fixedRowHeight > 0)
            {
                row.FixedRowHeight = fixedRowHeight;
            }

            // Add content
            if (cellContent.IsAny())
            {
                foreach (var item in cellContent)
                {
                    Cell cell;
                    if (item.TextAreaInfo != null)
                    {
                        cell = CreateTextAreaCell(row, item.Content, item.TextAreaInfo.Padding,
                            verticalAlignment, alignment, item.TextAreaInfo.WithBorder,
                            item.TextAreaInfo.WithRoundedCorners);
                    }
                    else
                    {
                        cell = row.Cells.Add();
                        if (item.TableInfo != null)
                        {
                            cell.Paragraphs.Add(item.TableInfo);
                        }
                        else if (item.IsCheckBox)
                        {
                            if (item.IsChecked)
                            {
                                var checkbox = CreateCheckBox();
                                cell.Paragraphs.Add(checkbox);
                            }
                            else
                            {
                                CreateCellText(row, cell, Resources.CrossLabel, Resources.ArialUnicodeFont); 
                            }
                        }
                        else
                        {
                            // this is a safer way to add in content into a cell without it causing exceptions.
                            CreateCellText(row, cell, item.Content);
                        }
                    }

                    if (item.Border != BorderSide.None)
                    {
                        cell.Border = new BorderInfo((int)item.Border, DefaultBorderSize);
                    }
                    if (!string.IsNullOrEmpty(item.Color))
                    {
                        cell.DefaultCellTextInfo.Color = new Color(item.Color);
                    }
                    if (item.NeedCellBorder)
                    {
                        cell.Border = new BorderInfo((int)BorderSide.All, DefaultBorderSize);
                    }
                    if (item.IsDate || item.IsCentralized)
                    {
                        cell.Alignment = AlignmentType.Center;
                    }
                    if (item.IsTotal)
                    {
                        // Bold font
                        cell.DefaultCellTextInfo.FontName = Resources.ArialFont;
                        row.DefaultCellTextInfo.IsTrueTypeFontBold = true;
                    }
                    if (item.IsCurrency)
                    {
                        cell.Alignment = AlignmentType.Right;
                        cell.VerticalAlignment = VerticalAlignmentType.Center;
                    }
                    if (!string.IsNullOrEmpty(item.BackgroundColor))
                    {
                        cell.BackgroundColor = new Color(item.BackgroundColor);
                    }
                    if (item.IsHeader)
                    {
                        cell.BackgroundColor = new Color(Resources.HeaderTableBackgroundColor);
                        cell.DefaultCellTextInfo.Color = new Color(Resources.WhiteColor);
                        cell.DefaultCellTextInfo.FontName = Resources.ArialFont;
                        cell.DefaultCellTextInfo.FontSize = DefaultHeadingThreeFontSize;
                        cell.DefaultCellTextInfo.IsTrueTypeFontBold = true;
                    }
                    if (item.IsEmpty)
                    {
                        cell.BackgroundColor = new Color(Resources.LightGrayColor);
                    }

                    cell.ColumnsSpan = item.ColumnSpan > 0
                        ? item.ColumnSpan
                        : DefaultColumnSpan;

                    cell.RowSpan = item.RowSpan > 0
                        ? item.RowSpan
                        : DefaultRowSpan;
                }
            }
        }

        /// <summary>
        /// Create <see cref="Text"/> and inject it into a <see cref="Cell"/>.
        /// <see cref="Row"/> provides additional information necessary to format
        /// the text.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="cell">cell</param>
        /// <param name="content">content</param>
        /// <param name="fontName">fontName</param>
        private static void CreateCellText(Row row, Cell cell, string content, string fontName = "")
        {
            var cellText = new Text(content)
            {
                TextInfo = new TextInfo
                    {
                        FontName = !string.IsNullOrEmpty(fontName) 
                            ? fontName 
                            : row.DefaultCellTextInfo.FontName, 
                        FontSize = row.DefaultCellTextInfo.FontSize, 
                        IsUnicode = true
                    }
            };
            cell.Paragraphs.Add(cellText);
        }

        /// <summary>
        /// Creates a simple 'three column' spanning inner table (with blue title background) and 
        /// populates it with the <see cref="descriptions"/> provided. User can specify alternative 
        /// column widths using  <see cref="columnWidths"/> and can provide an accompanying title. 
        /// By setting <see cref="keepContentTogether"/> to true, it will ensure the entire table 
        /// rows at hand is not  broken up across pages and will be treated as a single entity.
        /// The inner table that is created can be nested in another table if you specify 
        /// <see cref="parentTable"/>.
        /// </summary>
        /// <param name="descriptions">descriptions</param>
        /// <param name="parentPadding">parentPadding</param>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="title">title</param>
        /// <param name="parentTable">parentTable</param>
        /// <param name="keepContentTogether">keepContentTogether</param>
        /// <param name="isKeptTogether">isKeptTogether</param>
        /// <param name="isKeptWithNext"></param>
        public void CreateSimpleThreeColumnInnerTableFromReferenceData(
            string[] descriptions,
            string columnWidths = DefaultColumnSpanThreeWidth,
            string title = "",
            Table parentTable = null,
            MarginInfo parentPadding = null,
            bool keepContentTogether = false,
            bool isKeptTogether = false,
            bool isKeptWithNext = false)
        {
            if (descriptions.IsAny())
            {
                // Create an inner table
                var simpleThreeColumnInnerTable = CreateInnerTable(
                    columnWidths, 
                    parentTable, 
                    isKeptTogether: isKeptTogether, 
                    isKeptWithNext: isKeptWithNext,
                    keepContentTogether: keepContentTogether);

                // Create inner table row
                var simpleThreeColumnInnerTableRow = CreateInnerTableRow(simpleThreeColumnInnerTable, parentPadding);

                // Create Header Cell Title
                CreateInnerTableItemCells(
                        simpleThreeColumnInnerTableRow,
                        parentPadding,
                        cellContent: new DisplayCell { Content = title, IsHeader = true });

                // create a row for each description
                foreach (var description in descriptions)
                {
                    CreateInnerTableItemCells(
                        simpleThreeColumnInnerTable.Rows.Add(),
                        parentPadding,
                        cellContent: new DisplayCell { Content = description });
                }
            }
        }

        /// <summary>
        /// Creates an 'Inner Table' with no header. Instead Labels and their content are displayed side by side.
        /// Labels are in 'Bold' and descriptions are in normal form.
        ///      |                    ||                |
        /// E.g. | My Label (in bold) || My Description |
        ///      |                    ||                |
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="label">label</param>
        /// <param name="columnWidths">columnWidths</param>
        /// <param name="description">description</param>
        /// <param name="firstSectionText">firstSectionText</param>
        /// <param name="underlineText">underlineText</param>
        /// <param name="secondSectionText">secondSectionText</param>
        /// <param name="padding">padding</param>
        public void CreateInnerTableLabelValueRow(
            Table table,
            string columnWidths,
            string description,
            string label = "",
            string firstSectionText = "",
            string underlineText = "",
            string secondSectionText = "",
            MarginInfo padding = null)
        {
            var nestedTable = new Table
            {
                ColumnWidths = columnWidths,
                DefaultCellTextInfo =
                {
                    FontName = Resources.ArialFont,
                    FontSize = DefaultInnerBodyFontSize
                },
                DefaultCellBorder = new BorderInfo((int)BorderSide.All, DefaultBorderSize),
                DefaultCellPadding = DefaultInnerPadding,
                BackgroundColor = new Color(Resources.InnerTableColor)
            };

            // add descriptions
            var tableRow = nestedTable.Rows.Add();
            if (!string.IsNullOrEmpty(underlineText) &&
                !string.IsNullOrEmpty(firstSectionText) &&
                !string.IsNullOrEmpty(secondSectionText))
            {
                var labelSectionText = new Text(Section);
                var labelSegmentOne = new Segment(labelSectionText)
                {
                    Content = firstSectionText,
                    TextInfo =
                    {
                        FontName = Resources.ArialFont,
                        FontSize = DefaultInnerBodyFontSize
                    }
                };
                labelSegmentOne.TextInfo.IsTrueTypeFontBold = true;
                labelSectionText.Segments.Add(labelSegmentOne);

                var labelSegmentTwo = new Segment(labelSectionText)
                {
                    Content = underlineText,
                    TextInfo =
                    {
                        FontName = Resources.ArialFont,
                        FontSize = DefaultInnerBodyFontSize,
                        IsUnderline = true,
                        IsTrueTypeFontBold = true,
                    }
                };
                labelSectionText.Segments.Add(labelSegmentTwo);

                var labelSegmentThree = new Segment(labelSectionText)
                {
                    Content = secondSectionText,
                    TextInfo =
                    {
                        FontName = Resources.ArialFont,
                        FontSize = DefaultInnerBodyFontSize,
                        IsTrueTypeFontBold = true
                    }
                };
                labelSectionText.Segments.Add(labelSegmentThree);

                var tableRowCell = tableRow.Cells.Add();
                tableRowCell.Paragraphs.Add(labelSectionText);
            }
            else
            {
                var labelCell = tableRow.Cells.Add(label);
                labelCell.DefaultCellTextInfo.IsTrueTypeFontBold = true;
            }
            var descriptionCell = tableRow.Cells.Add(description);
            descriptionCell.VerticalAlignment = VerticalAlignmentType.Center;

            // add content back into parent cell
            var parentRow = table.Rows.Add();
            var parentCell = parentRow.Cells.Add();
            parentCell.ColumnsSpan = 2;
            if (padding != null)
            {
                parentCell.Padding = padding;
            }
            parentCell.Paragraphs.Add(nestedTable);
        }

        #endregion

        #region RadioButton Methods

        /// <summary>
        /// Creates a 'Yes' or 'No' value depending on the <see cref="value"/> passed in.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Yes or No value</returns>
        public string CreateYesNoValue(bool value)
        {
            return value ? Resources.YesValue : Resources.NoValue;
        }

        #endregion

        #region TextArea Methods

        /// <summary>
        /// Creates a 'TextArea' to be displayed. You can specify 
        /// <see cref="withRoundedCorners"/> to make the text area border rounded.
        /// </summary>
        /// <param name="parentColumnWidths">parentColumnWidths</param>
        /// <param name="title">title</param>
        /// <param name="textAreaText">textAreaText</param>
        /// <param name="defaultColumnWidth">defaultColumnWidth</param>
        /// <param name="textAreaPadding">textAreaPadding</param>
        /// <param name="withInternalBlankRow">withInternalBlankRow</param>
        /// <param name="withRoundedCorners">withRoundedCorners</param>
        public void CreateTextAreaCell(
            string parentColumnWidths,
            string title,
            string textAreaText,
            string defaultColumnWidth = DefaultTextAreaColumnWidth,
            MarginInfo textAreaPadding = null,
            bool withInternalBlankRow = false,
            bool withRoundedCorners = false)
        {
            // parent housing table
            var textAreaTable = CreateOuterTable(
                parentColumnWidths,
                Resources.ArialFont,
                DefaultHeadingTwoFontSize,
                DefaultOuterPadding,
                keepContentTogether: true);
            
            // nested table to correlate label & text area.
            var nestedTable = AddNestedTable(
                textAreaTable, 
                defaultColumnWidth, 
                Resources.ArialFont,
                DefaultHeadingTwoFontSize, 
                isKeptTogether: true, 
                isKeptWithNext: true);

            // Label Area formatting
            var parentTableLabelRow = nestedTable.Rows.Add();
            parentTableLabelRow.IsBroken = false;
            var inputColor = new Color(Resources.GreyColor);
            parentTableLabelRow.DefaultCellTextInfo.IsTrueTypeFontBold = true;
            parentTableLabelRow.DefaultCellTextInfo.Color = inputColor;
            parentTableLabelRow.DefaultCellTextInfo.LineSpacing = TextAreaLineSpacing;
            var labelCell = parentTableLabelRow.Cells.Add(title);
            labelCell.Padding = textAreaPadding ?? DefaultTextAreaLabelPadding;
            
            // Text Area formatting
            var textAreaInnerTable = CreateInnerTable(
                Resources.DefaultFullWidth,
                fontSize: DefaultHeadingTwoFontSize,
                parentTable: textAreaTable,
                keepContentTogether: true,
                // in some circumstances, a blank row has to be created like this, in order to separate elements.
                padding: withInternalBlankRow ? HeadingWithNoSubHeadingPadding : null);
            textAreaInnerTable.BackgroundColor = new Color(Resources.InnerTableColor);
            var textAreaInnerTableRow = textAreaInnerTable.Rows.Add();
            var textAreaCell = textAreaInnerTableRow.Cells.Add(textAreaText);
            textAreaCell.Padding = textAreaPadding ?? DefaultTextAreaDescriptionPadding;
            CreateStandardOrRoundedCornersBorder(withRoundedCorners, textAreaInnerTable);
        }

        /// <summary>
        /// Creates a 'TextArea' to be displayed. You can specify 
        /// <see cref="withRoundedCorners"/> to make the text area border rounded.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="textAreaText">textAreaText</param>
        /// <param name="textAreaPadding">textAreaPadding</param>
        /// <param name="verticalAlignment">verticalAlignment</param>
        /// <param name="alignment">alignment</param>
        /// <param name="withRoundedCorners">withRoundedCorners</param>
        /// <param name="withBorder">withBorder</param>
        public Cell CreateTextAreaCell(
            Row row,
            string textAreaText,
            MarginInfo textAreaPadding = null,
            VerticalAlignmentType verticalAlignment = VerticalAlignmentType.Top,
            AlignmentType alignment = AlignmentType.Left,
            bool withRoundedCorners = false,
            bool withBorder = false)
        {
            row.IsBroken = false;
            var cell = row.Cells.Add(textAreaText);
            // Text Area formatting
            cell.Padding = textAreaPadding ?? DefaultTextAreaDescriptionPadding;
            cell.DefaultCellTextInfo.FontName = Resources.ArialFont;
            cell.DefaultCellTextInfo.FontSize = DefaultInnerBodyFontSize;
            cell.DefaultCellTextInfo.Color = new Color(Resources.GreyColor);
            cell.VerticalAlignment = verticalAlignment;
            cell.Alignment = alignment;
            return cell;
        }

        /// <summary>
        /// If <see cref="withRoundedCorners"/> is true, we format the border with rounded corners.
        /// Else, return a standard rectangle border.
        /// </summary>
        /// <param name="withRoundedCorners">withRoundedCorners</param>
        /// <param name="table">table</param>
        private static void CreateStandardOrRoundedCornersBorder(bool withRoundedCorners, Table table)
        {
            table.Border = new BorderInfo((int)BorderSide.All, DefaultBorderSize, new Color(Resources.BlackColor));
            if (!withRoundedCorners) return;
            table.Border = new BorderInfo
            {
                Round = new GraphInfo
                {
                    CornerRadius = DefaultCornerRadius,
                    Color = new Color(Resources.BlackColor)
                }
            };
            table.CornerStyle = BorderCornerStyle.Round;
        }

        #endregion

        #region Miscellaneous Methods

        /// <summary>
        /// Retrieves the column span for a <see cref="Table"/> by checking 
        /// the <see cref="Table.ColumnWidths"/> specified.
        /// Will return '1' if <see cref="Table.ColumnWidths"/> isn't set,
        /// or does not contain more than one column.
        /// </summary>
        /// <param name="table">table</param>
        /// <returns>column span</returns>
        public static int GetColumnSpan(Table table)
        {
            return !string.IsNullOrEmpty(table.ColumnWidths) && table.ColumnWidths.Contains(" ")
                ? table.ColumnWidths.Split(' ').Length
                : 1;
        }

        #endregion
    }
}
