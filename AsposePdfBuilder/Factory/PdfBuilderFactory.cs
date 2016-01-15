using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Builder.Builders;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Builder.Extensions;

namespace Aspose.Pdf.Builder.Factory
{
    public static class PdfBuilderFactory
    {
        /// <summary>
        /// Returns a new <see cref="PdfBuilder"/> for use.
        /// </summary>
        /// <returns></returns>
        public static PdfBuilder CreateAsposePdfBuilder()
        {
            // Create aspose pdf builder (responsible for formatting & layout operations)
            return new PdfBuilder();
        }

        /// <summary>
        /// Takes a IList{byte[]} array of pdfs as bytes and concatenates them together.
        /// The resulting byte array can then be outputted as a single pdf of all the pdfs contained in the list.
        /// </summary>
        /// <remarks>
        /// Usage example is as follows:
        ///     var generatedPdfs = new List{byte[]} { partApdfBytes, partBpdfBytes, partCpdfBytes };
        ///     var concatenatedPdfs = PdfBuilderFactory.ConcatenatePdfs(generatedPdfs);
        /// </remarks>
        /// <param name="serialisedArrayPdfList">serialisedArrayPdfList</param>
        /// <param name="licensePath">licensePath must be supplied to avoid watermarking on the concatenated pdfs</param>
        /// <returns>concatenated pdfs</returns>
        public static byte[] ConcatenatePdfs(byte[] serialisedArrayPdfList, string licensePath)
        {
            // must set license in order to concatenate many files.
            SetAsposeLicense(licensePath);

            // deserialize the array into a more meaningful context for us to work with.
            var pdfBytesCollection = serialisedArrayPdfList.DeserializeObject<IList<byte[]>>();
            // if this conversion fails, return null.
            if (pdfBytesCollection == null || pdfBytesCollection.Count <= 0)
            {
                return null;
            }

            // we must construct a list of Streams and 
            // use them to merge the pdfs together.
            var streamList = pdfBytesCollection
                .Select(pdf => new MemoryStream(pdf))
                    .Cast<Stream>()
                    .ToArray();

            // return merged pdfs.
            return ReturnStreamListAsByteArray(streamList);
        }

        /// <summary>
        /// Aspose requires that a valid license file be read before generating any sort of Aspose related content.
        /// A valid license will remove any 'Evaluation' cosmetics and watermarks from the resultant pdfs generated.
        /// NOTE: Depending on your circumstances, setting license.Embedded = true; may not be necessary. 
        /// E.g. If you are making calls to a wcf service that uses this builder, there is no need for the licence to be embedded.
        /// Just call the method before processing (as part of an internal operation) for every call. 
        /// It is recommended you add the Aspose.Pdf.lic to your project in App_Data and have Build Action set to 'Content'.
        /// </summary>
        public static void SetAsposeLicense(string licensePath)
        {
            var license = new License();
            if (!File.Exists(licensePath)) return;
            using (var licenseStream = new FileStream(licensePath, FileMode.Open, FileAccess.Read))
            {
                license.SetLicense(licenseStream);
            }
        }

        /// <summary>
        /// Takes an array of <see cref="Stream"/>s and returns a <see cref="byte"/>[] array.
        /// </summary>
        /// <param name="streamList">streamList</param>
        /// <returns>merged pdfs</returns>
        private static byte[] ReturnStreamListAsByteArray(Stream[] streamList)
        {
            using (var memoryStream = new MemoryStream())
            {
                var pfe = new PdfFileEditor();
                pfe.Concatenate(streamList, memoryStream);
                // Now manually dispose memory streams (we're done using them).
                // We need to do this otherwise the list of streams will be
                // unreadable which will cause an exception.
                foreach (var doc in streamList)
                {
                    doc.Dispose();
                }
                return memoryStream.ToArray();
            }
        }
    }
}
