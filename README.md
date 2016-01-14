# Aspose.Pdf.Builder
Aspose.Pdf.Builder is a toolkit designed to make developing with Aspose.Pdf a breeze. It contains a variety of functions which allows a user to develop complex and flexible page components without having to dig in too deeply into the intricacies of Aspose.Pdf.

Version 1.0.0.3 created

Examples are available from the unit test solution provided here. 
Look at the examples to understand how to use this simple toolkit. 

I've removed the packages associated with the solutions as it made the repo unnecessarily large. Use nuGet Restore on the projects if you want to build them successfully.

1. <b>Creating a Builder to Build Content With</b>
<br/>Invoke the following:
<br/>```var pdfBuilder = AsposePdfBuilder.Factory.PdfBuilderFactory.CreateAsposePdfBuilder();```
<br/>The *pdfBuilder* contains a large number of methods that will allow you to create content with Aspose.Pdf.

2. <b>Setting your own Aspose License</b>
<br/>```AsposePdfBuilder.Factory.PdfBuilderFactory.SetAsposeLicense(licensePath);```
<br/>SetAsposeLicense requires a path to the file that may be located on your machine or on your web server. 
<br/>You will need to provide the correct path in order for this to work correctly.
<br/>e.g.
<br/>```var licensePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Aspose.Pdf.lic");``` <b>or</b>,
<br/>```var licensePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "App_Data", "Aspose.Pdf.lic");```
<br/>
<br/> I would recommend placing your licence into App_Data or similar area, and setting it as 'Content -> Copy Always', so it is included in your build for your deployments.


More to follow...
