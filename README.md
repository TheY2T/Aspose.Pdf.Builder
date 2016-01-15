# Aspose.Pdf.Builder
Aspose.Pdf.Builder is a toolkit designed to make developing with Aspose.Pdf a breeze. It contains a variety of functions which allows a user to develop complex and flexible page components without having to dig in too deeply into the intricacies of Aspose.Pdf.

<b>Latest Version:</b> 1.0.0.3

Examples are available from the unit test solution provided in the repository. 
Look at the examples to understand how to use this simple toolkit. 

I've removed the packages associated with the solutions as it made the repo unnecessarily large. Use nuGet Restore on the projects if you want to build them successfully.

1. <b>Creating a Builder</b>
<br/>Invoke the following:
<br/>```var pdfBuilder = Aspose.Pdf.Builder.Factory.PdfBuilderFactory.CreateAsposePdfBuilder();```
<br/>This will create a pdf defaulted to A4 portrait.
<br/>
<br/>If you require a different page setup, invoke the following:
<br/>```var pdfBuilder = Aspose.Pdf.Builder.Factory.PdfBuilderFactory.CreateAsposePdfBuilder(customMargin, height, width);```
<br/> where customMargin, height and width are all user-specified. 
<br/>
<br/>The *pdfBuilder* contains a number of methods that will allow you to create content with Aspose.Pdf.
<br/>
<br/> NOTE: It is recommended that you make use of PageSize class that is available in ***Aspose.Pdf.Generator.PageSize*** for correct height and width of pdf pages. 
<br/>customMargin is the margin to be set on each page in the PDF. Use ***Aspose.Pdf.Generator.MarginInfo*** to set the margins you want.

2. <b>Setting your own Aspose License</b>
<br/>```Aspose.Pdf.Builder.Factory.PdfBuilderFactory.SetAsposeLicense(licensePath);```
<br/>SetAsposeLicense requires a path to the file that may be located on your machine or on your web server. 
<br/>You will need to provide the correct path in order for this to work correctly.
<br/>e.g.
<br/>```var licensePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Aspose.Pdf.lic");``` <b>or</b>,
<br/>```var licensePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "App_Data", "Aspose.Pdf.lic");```
<br/>
<br/> I would recommend placing your licence into App_Data or similar area, and setting it as 'Content -> Copy Always', so it is included in your build for your deployments.


More to follow...
