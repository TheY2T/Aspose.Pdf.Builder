using Aspose.Pdf.Generator;
using AsposePdfBuilder.Builders;

namespace AsposePdfBuilder.Extensions
{
    public static class PdfBuilderExtensions
    {
        /// <summary>
        /// Goes through each <see cref="Section"/> in the <see cref="Pdf"/> and adds a Header and Footer component.
        /// </summary>
        /// <param name="pdfBuilder">pdfBuilder</param>
        /// <param name="headerText">headerText</param>
        /// <param name="footerText">footerText</param>
        public static void CreateHeaderAndFooterForPdf(this PdfBuilder pdfBuilder, string headerText, string footerText)
        {
            foreach (Section section in pdfBuilder.AsposePdf.Sections)
            {
                pdfBuilder.CreateHeaderAndFooterForPdfForSection(section, headerText, footerText);
            }
        }
    }
}
