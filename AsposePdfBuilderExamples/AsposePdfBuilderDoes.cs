using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf.Generator;
using AsposePdfBuilder.Builders;
using AsposePdfBuilder.Extensions;
using AsposePdfBuilder.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsposePdfBuilderExamples
{
    [TestClass]
    public class AsposePdfBuilderDoes
    {
        private const string ASPOSE_PDF_BUILDER_CATEGORY = "Aspose.Pdf.Builder";

        #region Example 001 - Display a document with example tables and summaries

        [TestMethod]
        [TestCategory(ASPOSE_PDF_BUILDER_CATEGORY)]
        public void AsposePdfBuilderShouldGenerateExample001()
        {
            // Arrange
            var exampleHeaderText = "Example Header Text Title";
            var exampleFooterText = "All Rights Reserved Footer Example © 2016";
            var pdfBuilder = AsposePdfBuilder.Factory.PdfBuilderFactory.CreateAsposePdfBuilder();
            
            // set license, if available - e.g.
            var licensePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "App_Data", "Aspose.Pdf.lic");
            AsposePdfBuilder.Factory.PdfBuilderFactory.SetAsposeLicense(licensePath);
            
            // this is optional padding to be applied to all elements
            var parentPadding = new MarginInfo
            {
                Top = 2,
                Bottom = 2,
                Left = 2
            };

            // create sections of pdf
            CreateSectionOneExample001(pdfBuilder, parentPadding);
            CreateSectionTwoExample001(pdfBuilder, parentPadding);
            CreateSectionThreeExample001(pdfBuilder, parentPadding);
            CreateSectionFourExample001(pdfBuilder, parentPadding);
            CreateSectionFiveExample001(pdfBuilder, parentPadding);

            // create a header and footer for each section (page) in the pdf - also creates 'Page 1 of x' numbering.
            pdfBuilder.CreateHeaderAndFooterForPdf(exampleHeaderText, exampleFooterText);

            // Act - generate the pdf
            var generatedPdf = pdfBuilder.SaveFileStream();
            
            // Assert
            Assert.IsNotNull(generatedPdf);
            Assert.IsTrue(generatedPdf.Length > 0);

            // write file output so we can view it.
            var filename = GetTemporaryFileName("Example_001");
            File.WriteAllBytes(filename, generatedPdf);
        }

        #region Section 1 of Example 001
        private static void CreateSectionOneExample001(PdfBuilder pdfBuilder, MarginInfo parentPadding)
        {
            const string detailsColumnWidths = "140 310";
            const bool boolValue = true;

            // Create Outer Table
            var exampleOuterTable = pdfBuilder.CreateOuterTable(
                detailsColumnWidths,
                AsposePdfBuilder.Properties.Resources.ArialFont,
                PdfBuilder.DefaultHeadingTwoFontSize,
                parentPadding,
                keepContentTogether: true);

            // Create outer table heading
            pdfBuilder.CreateOuterTableRowHeading(
                exampleOuterTable,
                "Example Heading");

            // insert blank row
            pdfBuilder.BlankRow(exampleOuterTable);

            // Create details
            var exampleLabelTextBox01 = exampleOuterTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleLabelTextBox01,
                "Example 01 (label-textbox)",
                "Description of example 01",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var exampleLabelTextBox02 = exampleOuterTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleLabelTextBox02,
                "Example 02 (label-textbox)",
                "Description of example 02",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var exampleLabelTextBox03 = exampleOuterTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleLabelTextBox03,
                "Example 03 (label-textbox)",
                "Description of example 03",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var exampleLabelTextBox04 = exampleOuterTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleLabelTextBox04,
                "Example 04 (label-textbox)",
                "Description of example 04",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var exampleLabelTextBox05 = exampleOuterTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleLabelTextBox05,
                "Example 05 (label-textbox)",
                "Description of example 05",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var capitalOnlyRow = exampleOuterTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                capitalOnlyRow,
                "Example Name 06 (from bool as yes/no value)",
                pdfBuilder.CreateYesNoValue(boolValue),
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            // project description
            pdfBuilder.CreateTextAreaCell(
                exampleOuterTable.ColumnWidths,
                "Example 07 (textarea)",
                "Description of example 07",
                AsposePdfBuilder.Properties.Resources.TextAreaColumnWidths);
            pdfBuilder.BlankRow();
        }
        #endregion

        #region Section 2 of Example 001
        private static void CreateSectionTwoExample001(PdfBuilder pdfBuilder, MarginInfo parentPadding)
        {
            const VerticalAlignmentType parentVerticalAlignment = VerticalAlignmentType.Center;
            const bool keepContentTogether = false;
            const bool isKeptTogether = false;
            const bool isKeptWithNext = false;
            const string exampleDocWidths = "100 250 100";
            var exampleDocPadding = new MarginInfo
            {
                Left = 2,
                Bottom = 2,
                Top = 2
            };
            var exampleDocs = new List<string[]>
            {
                new[]{ "item 1 type", "item 1 desc", "item 1 filename" }, 
                new[]{ "item 2 type", "item 2 desc", "item 2 filename" }, 
                new[]{ "item 3 type", "item 3 desc", "item 3 filename" }, 
                new[]{ "item 4 type", "item 4 desc", "item 4 filename" }, 
            };
            if (exampleDocs.IsAny())
            {
                // Create an inner table
                var exampleDocumentTable = pdfBuilder.CreateInnerTable(
                    exampleDocWidths,
                    isKeptTogether: isKeptTogether,
                    isKeptWithNext: isKeptWithNext,
                    keepContentTogether: keepContentTogether);

                // Create inner table row
                var docRow = pdfBuilder.CreateInnerTableRow(exampleDocumentTable, exampleDocPadding);

                // Create inner table cells
                pdfBuilder.CreateInnerTableHeaderCells(docRow, new[]
                {
                    new HeaderCell { Content = "Example 001 doc type" },
                    new HeaderCell { Content = "Example 001 doc desc" },
                    new HeaderCell { Content = "Example 001 doc filename" }
                });

                // format rows for each document
                foreach (var document in exampleDocs)
                {
                    var itemRow = exampleDocumentTable.Rows.Add();
                    pdfBuilder.CreateInnerTableItemCells(
                        itemRow,
                        exampleDocPadding,
                        verticalAlignment: parentVerticalAlignment,
                        cellContent: new[]
                        {
                            new DisplayCell { Content = document[0].Trim() }, 
                            new DisplayCell { Content = document[1].Trim() }, 
                            new DisplayCell { Content = document[2].Trim() }
                        });
                }
            }
            pdfBuilder.BlankRow();
        }
        #endregion

        #region Section 3 of Example 001
        private static void CreateSectionThreeExample001(PdfBuilder pdfBuilder, MarginInfo parentPadding)
        {
            const bool keepContentTogether = true;
            const bool isKeptTogether = false;
            const bool isKeptWithNext = false;
            var title = "Simple list example 01";
            string[] descriptions = { "example desc 01", "example desc 02", "example desc 03", "example desc 04" };
            if (descriptions.IsAny())
            {
                // Create an inner table
                var simpleThreeColumnInnerTable = pdfBuilder.CreateInnerTable(
                    AsposePdfBuilder.Properties.Resources.ColumnSpanThreeWidth,
                    isKeptTogether: isKeptTogether,
                    isKeptWithNext: isKeptWithNext,
                    keepContentTogether: keepContentTogether);

                // Create inner table row
                var simpleThreeColumnInnerTableRow = pdfBuilder.CreateInnerTableRow(simpleThreeColumnInnerTable, parentPadding);

                // Create Header Cell Title
                pdfBuilder.CreateInnerTableItemCells(
                        simpleThreeColumnInnerTableRow,
                        parentPadding,
                        cellContent: new DisplayCell { Content = title, IsHeader = true });

                // create a row for each description
                foreach (var description in descriptions)
                {
                    pdfBuilder.CreateInnerTableItemCells(
                        simpleThreeColumnInnerTable.Rows.Add(),
                        parentPadding,
                        cellContent: new DisplayCell { Content = description });
                }
            }
            pdfBuilder.BlankRow();

            title = "Simple list example 02";
            descriptions = new[]{ "example desc 05", "example desc 06", "example desc 07", "example desc 08" };
            if (descriptions.IsAny())
            {
                // Create an inner table
                var simpleThreeColumnInnerTable = pdfBuilder.CreateInnerTable(
                    AsposePdfBuilder.Properties.Resources.ColumnSpanThreeWidth,
                    isKeptTogether: isKeptTogether,
                    isKeptWithNext: isKeptWithNext,
                    keepContentTogether: keepContentTogether);

                // Create inner table row
                var simpleThreeColumnInnerTableRow = pdfBuilder.CreateInnerTableRow(simpleThreeColumnInnerTable, parentPadding);

                // Create Header Cell Title
                pdfBuilder.CreateInnerTableItemCells(
                        simpleThreeColumnInnerTableRow,
                        parentPadding,
                        cellContent: new DisplayCell { Content = title, IsHeader = true });

                // create a row for each description
                foreach (var description in descriptions)
                {
                    pdfBuilder.CreateInnerTableItemCells(
                        simpleThreeColumnInnerTable.Rows.Add(),
                        parentPadding,
                        cellContent: new DisplayCell { Content = description });
                }
            }
            pdfBuilder.BlankRow();
        }
        #endregion

        #region Section 4 of Example 001
        private static void CreateSectionFourExample001(PdfBuilder pdfBuilder, MarginInfo parentPadding)
        {
            const string columnWidths = "350 100";
            var descriptions = new List<string[]>{ new [] { "[ex code 01]", "ex foo", "cm" }, new [] { "[ex code 02]", "ex foo", "mm" } };
            if (descriptions.IsAny())
            {
                // Create an inner table
                var sectionFourExampleTable = pdfBuilder.CreateInnerTable(
                    columnWidths,
                    keepContentTogether: true);

                // Create inner table row
                var projectFundedServicesRow = pdfBuilder.CreateInnerTableRow(sectionFourExampleTable, parentPadding);

                // Create Header Cell Titles
                pdfBuilder.CreateInnerTableItemCells(
                        projectFundedServicesRow,
                        cellContent: new[] {
                            new DisplayCell { Content = "Example Code", IsHeader = true }, 
                            new DisplayCell { Content = "Example Measure", IsHeader = true }
                        });

                // create a row for each description
                foreach (var item in descriptions)
                {
                    pdfBuilder.CreateInnerTableItemCells(
                        sectionFourExampleTable.Rows.Add(),
                        cellContent: new[] {
                            new DisplayCell { Content = item[0] + " - " + item[1] }, 
                            new DisplayCell { Content = item[2] }
                        });
                }
            }
            pdfBuilder.BlankRow();
        }
        #endregion

        #region Section 5 of Example 001
        private static void CreateSectionFiveExample001(PdfBuilder pdfBuilder, MarginInfo parentPadding)
        {
            var secionFiveExampleTable = pdfBuilder.CreateOuterTable(
                    "140 310",
                    AsposePdfBuilder.Properties.Resources.ArialFont,
                    PdfBuilder.DefaultHeadingTwoFontSize,
                    parentPadding,
                    keepContentTogether: true);

            // create table row heading
            pdfBuilder.CreateOuterTableRowHeading(
                secionFiveExampleTable,
                "Example Section 5 Heading");

            pdfBuilder.BlankRow(secionFiveExampleTable);

            // Create dates
            var exampleItem01Row = secionFiveExampleTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleItem01Row,
                "Example item 01",
                DateTime.Now.ToShortDateString(),
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var exampleItem02Row = secionFiveExampleTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleItem02Row,
                "Example item 02",
                "example info",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var exampleItem03Row = secionFiveExampleTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleItem03Row,
                "Example item 03",
                "example info",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            var exampleItem04Row = secionFiveExampleTable.Rows.Add();
            pdfBuilder.CreateOuterTableRowCells(
                exampleItem04Row,
                "Example item 04",
                "example info",
                padding: PdfBuilder.DefaultSubHeadingBottomPadding);

            pdfBuilder.BlankRow(secionFiveExampleTable);

            // create an overly complex arrangement of data and values to demonstrate the flexibility of the aspose pdf builder.
            var orderedNumbers = new[] { "0001", "0002", "0003" };
            var examplePairsAsGroup = new List<KeyValuePair<string, decimal>>
            {
                new KeyValuePair<string, decimal>(orderedNumbers[0], 10000), 
                new KeyValuePair<string, decimal>(orderedNumbers[0], 10000), 
                new KeyValuePair<string, decimal>(orderedNumbers[0], 10000), 
                new KeyValuePair<string, decimal>(orderedNumbers[1], 20000), 
                new KeyValuePair<string, decimal>(orderedNumbers[1], 20000), 
                new KeyValuePair<string, decimal>(orderedNumbers[1], 20000), 
                new KeyValuePair<string, decimal>(orderedNumbers[2], 30000),
                new KeyValuePair<string, decimal>(orderedNumbers[2], 30000),
                new KeyValuePair<string, decimal>(orderedNumbers[2], 30000)
            }.OrderBy(x => x.Key)
            .GroupBy(x => x.Key)
            .ToArray();

            // calculate totals - terrible example...
            var orderedNumbers0001 = examplePairsAsGroup.SelectMany(x => x).Where(x => x.Key == orderedNumbers[0]).ToArray();
            var orderedNumbers0002 = examplePairsAsGroup.SelectMany(x => x).Where(x => x.Key == orderedNumbers[1]).ToArray();
            var orderedNumbers0003 = examplePairsAsGroup.SelectMany(x => x).Where(x => x.Key == orderedNumbers[2]).ToArray();
            var exampleTotalsAsGroup = new List<KeyValuePair<string, decimal>>
            {
                new KeyValuePair<string, decimal>(orderedNumbers[0], orderedNumbers0001[0].Value + orderedNumbers0002[0].Value + orderedNumbers0003[0].Value), 
                new KeyValuePair<string, decimal>(orderedNumbers[1], orderedNumbers0001[1].Value + orderedNumbers0002[1].Value + orderedNumbers0003[1].Value), 
                new KeyValuePair<string, decimal>(orderedNumbers[2], orderedNumbers0001[2].Value + orderedNumbers0002[2].Value + orderedNumbers0003[2].Value)
            }.OrderBy(x => x.Key)
            .GroupBy(x => x.Key)
            .ToArray();

            var useWidths = "53 80 79 79 80 79"; // remember - a user can specify less or more columns and pass them into CreateInnerTable.
            // Create an inner table
            var exampleInnerTable = pdfBuilder.CreateInnerTable(useWidths, secionFiveExampleTable);

            // Create Header Inner Table row
            var exampleInnerHeaderRow = pdfBuilder.CreateInnerTableRow(exampleInnerTable, parentPadding);

            // Create Header Cell Titles
            var titles = new List<HeaderCell> { new HeaderCell { Content = "Side Content Name" } };
            var additionalNames = new [] { "example 01", "example 02", "example 03" }.GroupBy(x => x).ToArray();
            titles.AddRange(additionalNames.Select(name => new HeaderCell { Content = "ex name " + name.Key }));
            titles.Add(new HeaderCell { Content = "Total" });
            pdfBuilder.CreateInnerTableHeaderCells(exampleInnerHeaderRow, titles.ToArray());

            // Display example pairs by their ordered numbers
            foreach (var epag in examplePairsAsGroup)
            {
                var cellContentByFy = new List<DisplayCell> { new DisplayCell { Content = epag.Key, IsDate = true } };
                cellContentByFy.AddRange(epag.Select(x => new DisplayCell { Content = x.Value.ToString("C"), IsCurrency = true }).ToArray());
                cellContentByFy.Add(new DisplayCell { Content = epag.Sum(x => x.Value).ToString("C"), IsCurrency = true });

                var itemRow = pdfBuilder.CreateInnerTableRow(exampleInnerTable, parentPadding);
                pdfBuilder.CreateInnerTableItemCells(
                    itemRow,
                    cellContent: cellContentByFy.ToArray());
            }

            // Display Totals
            var cellContentTotals = new List<DisplayCell> { new DisplayCell { Content = "Totals", IsHeader = true } };
            cellContentTotals.AddRange(exampleTotalsAsGroup.Select(x => new DisplayCell { Content = x.Sum(sum => sum.Value).ToString("C"), IsTotal = true, IsCurrency = true }).ToArray());
            cellContentTotals.Add(new DisplayCell { Content = exampleTotalsAsGroup.Sum(sum => sum.Sum(x => x.Value)).ToString("C"), IsTotal = true, IsCurrency = true });
            var projectCostCentreTotalRow = pdfBuilder.CreateInnerTableRow(exampleInnerTable, PdfBuilder.DefaultOuterPadding);
            pdfBuilder.CreateInnerTableItemCells(
                    projectCostCentreTotalRow,
                    cellContent: cellContentTotals.ToArray());
            
            pdfBuilder.BlankRow();
        }
        #endregion

        #endregion

        #region Helper Methods

        private const string PDF_TEMP_FOLDER_PATH = @"C:\AsposePdfBuilderTemp\";

        private void DetectFolderExists()
        {
            var file = new FileInfo(PDF_TEMP_FOLDER_PATH);
            if (file.Directory != null) file.Directory.Create();
        }

        private string GetTemporaryFileName(string prefix)
        {
            DetectFolderExists();
            var info = new DirectoryInfo(PDF_TEMP_FOLDER_PATH);
            var uniqueKey = info.LastWriteTime.Ticks + 1L;
            var filename = string.Format("{0}_{1}.pdf", prefix, uniqueKey);

            return PDF_TEMP_FOLDER_PATH + filename;
        }

        #endregion
    }
}
