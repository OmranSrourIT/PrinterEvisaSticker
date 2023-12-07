using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;

namespace PrinterEvisaSticker.WebApi
{
    class PdfGenerator
    {
        public static void GeneratePdf(string TepmlatefromPath , string TemplateFilledform, PdfFont FontMRZ, PdfFont FontARA, PDFTemplateFields ObjectData)
        {
             
            var Base64IMage = ObjectData.P_PersonlImage;
              
            // Create a PdfWriter object to write to the filled PDF
            using (PdfWriter writer = new PdfWriter(TemplateFilledform))
            {
                // Create a PdfReader object to read the existing PDF form
                using (PdfReader reader = new PdfReader(TepmlatefromPath))
                {
                    // Create a PdfDocument object
                    using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
                    {
                        // Create a PdfAcroForm object to access the form fields
                        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                       
                        var document = new Document(pdfDoc);

                         
                        //Byte[] bytes = File.ReadAllBytes("path");
                        //String file = Convert.ToBase64String(bytes);

                        if (ObjectData.P_VisaTypeSticker == "AA")
                        {

                            // Set the font for the field
                             

                            form.GetField("P_VisaType").SetFont(FontARA);
                            form.GetField("P_NoOfEntries").SetFont(FontARA);
                            form.GetField("P_IssuingAthority").SetFont(FontMRZ);
                            form.GetField("P_FullName").SetFont(FontMRZ);
                            form.GetField("P_Nationalty").SetFont(FontMRZ);
                            form.GetField("P_Requester").SetFont(FontMRZ);
                            form.GetField("P_DurationOfStay").SetFont(FontMRZ);
                            form.GetField("P_DateofIssue").SetFont(FontMRZ);
                            form.GetField("P_DateofExpiry").SetFont(FontMRZ);
                            form.GetField("P_MRZ").SetFont(FontMRZ);
                            form.GetField("P_MRZ").SetFontSize(11);

                            char[] stringArray = ObjectData.P_VisaType.ToCharArray();
                            Array.Reverse(stringArray);
                            string reversedStr = new string(stringArray);
                            

                            // Set the field values
                            form.GetField("P_VisaType").SetValue(reversedStr);
                            form.GetField("P_NoOfEntries").SetValue(ObjectData.P_NoOfEntries.ToUpper());
                            form.GetField("P_IssuingAthority").SetValue(ObjectData.P_IssuingAuthority_ARA.ToUpper());
                            form.GetField("P_FullName").SetValue(ObjectData.P_FullName.ToUpper());
                            form.GetField("P_Nationalty").SetValue(ObjectData.P_Nationality.ToUpper());
                            form.GetField("P_Requester").SetValue(ObjectData.P_Requester_ARA.ToUpper());
                            form.GetField("P_DurationOfStay").SetValue(ObjectData.P_DurationOfStay.ToUpper());
                            form.GetField("P_DateofIssue").SetValue(ObjectData.P_DateOfIssue.ToUpper());
                            form.GetField("P_DateofExpiry").SetValue(ObjectData.P_DateOfExpiry.ToUpper());
                            form.GetField("P_MRZ").SetValue(ObjectData.P_MRZ.ToUpper());
                          //  Adding Image To PDFTemplate
                            AddImageToPdf(document, Base64IMage);


                        }
                        else
                        {
                            // Set the font for the field
                            // form.GetField("P_VisaType").SetFont(arabicFont);
                            form.GetField("P_VisaType").SetFont(FontMRZ);
                            form.GetField("P_DepaturePeriod").SetFont(FontMRZ);
                            form.GetField("P_IssuingAthority").SetFont(FontMRZ);
                            form.GetField("P_FullName").SetFont(FontMRZ);
                            form.GetField("P_Nationalty").SetFont(FontMRZ);
                            form.GetField("P_Requester").SetFont(FontMRZ);
                            form.GetField("P_DateofIssue").SetFont(FontMRZ);
                            form.GetField("P_DateofExpiry").SetFont(FontMRZ);
                            form.GetField("P_MRZ").SetFont(FontMRZ);
                            form.GetField("P_MRZ").SetFontSize(11);

                            // Set the field values
                            form.GetField("P_VisaType").SetValue(ObjectData.P_VisaType.ToUpper());
                            form.GetField("P_DepaturePeriod").SetValue(ObjectData.P_DepaturePeriod_DA.ToUpper());
                            form.GetField("P_IssuingAthority").SetValue(ObjectData.P_IssuingAuthority_ARA.ToUpper());
                            form.GetField("P_FullName").SetValue(ObjectData.P_FullName.ToUpper());
                            form.GetField("P_Nationalty").SetValue(ObjectData.P_Nationality.ToUpper());
                            form.GetField("P_Requester").SetValue(ObjectData.P_Requester_ARA.ToUpper());
                            form.GetField("P_DateofIssue").SetValue(ObjectData.P_DateOfIssue.ToUpper());
                            form.GetField("P_DateofExpiry").SetValue(ObjectData.P_DateOfExpiry.ToUpper());
                            form.GetField("P_MRZ").SetValue(ObjectData.P_MRZ.ToUpper());

                            //Adding Image To PDFTemplate
                            AddImageToPdf(document, Base64IMage);
                             

                        }
                         
                        // Close the PdfDocument
                        pdfDoc.Close();
                    }
                }
            }

        }

        static void AddImageToPdf(Document document, string base64Image)
        {
            // Replace these coordinates and scale as needed
            float x = 32;
            float y = 671;

            // Create an image element
            ImageData imageData = ImageDataFactory.Create(Convert.FromBase64String(base64Image));
            Image image = new Image(imageData)
                .SetAutoScale(false)
                .SetFixedPosition(x, y)
                .SetHeight(94) // Set the height of the image
                .SetWidth(77); // Set the width of the image

            // Add the image to the document
            document.Add(image);
        }


        private void Print(PrintDocument document, string printerName)
        {
            var printerSettings = new PrinterSettings { PrinterName = printerName };

            var pageSettings = new PageSettings(printerSettings) { };
            document.PrinterSettings = printerSettings;
            //document.DefaultPageSettings.PaperSize = new PaperSize() { Width = 827, Height = 1169 };//A4 Size

            document.DefaultPageSettings = pageSettings;
            document.PrintController = new StandardPrintController();

            document.DefaultPageSettings.Landscape = false;
            document.Print();
        }




    }
}
