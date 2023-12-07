using GET.Printers.Vi1200;
using iText.IO.Font;
using iText.Kernel.Font;
using Patagames.Pdf.Net;
using PrinterEvisaSticker.ConfigrationPDF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PrinterEvisaSticker.WebApi
{
    public class ViPrintApiController : ApiController
    {
        public string ServiceTypeP = string.Empty;

        public string lblMediaState;
        public string lblEngineState;
        public string lblMachineState;
        public string lblTotalCount;
        public string lblIsAuthorized;
        public string lblC;
        public string lblK;
        public string lblM;
        public string lblY;

        public string lblOrderStatus;
        public string lblCompleted;
        public string lblStarted;
        public string lblError;



        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PrintSticker(PDFTemplateFields objDataForm)
        {
              


            PaperSource pkSource;
            PaperSize pkSize;//this for size

            ApiResult oApi = new ApiResult();

            if (Program.ObjectConfig.PrinterName == null)
            {
                oApi.ErrorMessage = "تأكد من أن الطابعة قيد الشتغيل  و متصلة بجهاز الكمبوتر ";
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);

            }

            if (Program.lblIsAuthorized == "False")
            {
                oApi.ErrorMessage = Convert.ToString(Program.lblEngineState);
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }
            if(Program.lblMachineState == "OUT_OF_PAPER")
            {
                oApi.ErrorMessage = Convert.ToString("لقد نفذ الورق");
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }
            if (Program.lblEngineState == "REAR_DOOR_OPEN")
            {
                oApi.ErrorMessage = "يرجى اغلاق الباب الخلفي للطابعة";
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }
            if (Program.lblMachineState != "IDLE")
            {
                oApi.ErrorMessage = Convert.ToString(Program.lblMachineState);
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }




            try
            {
                var PrinterName = Program.ObjectConfig.PrinterName;
                var lblMediaState = Program.lblMediaState;
                var lblEngineState = Program.lblEngineState;
                var lblMachineState = Program.lblMachineState;
                var lblTotalCount = Program.lblTotalCount;
                var lblIsAuthorized = Program.lblIsAuthorized;
                var lblC = Program.lblC;
                var lblK = Program.lblK;
                var lblM = Program.lblM;
                var lblY = Program.lblY;
                var lblOrderStatus = Program.lblOrderStatus;
                var lblCompleted = Program.lblCompleted;
                var lblStarted = Program.lblStarted;
                var lblError = Program.lblError;



                var IPD = new PrintDocument();
                IPD.PrinterSettings.PrinterName = Program.ObjectConfig.PrinterName;
                IPD.PrintPage += (sender, e) => pd_PrintPageNewVersion(objDataForm, e);
                IPD.BeginPrint += new PrintEventHandler(pd_BeginPrint);


                //Set the Paper size A4 , A5 .....bla bla
                for (int i = 0; i < IPD.PrinterSettings.PaperSizes.Count; i++)
                {
                    pkSize = IPD.PrinterSettings.PaperSizes[i];
                    IPD.DefaultPageSettings.PaperSize = pkSize;
                    break;
                }

                //Set the Paper source 

                for (int i = 0; i < IPD.PrinterSettings.PaperSources.Count; i++)
                {
                    pkSource = IPD.PrinterSettings.PaperSources[i];
                    IPD.DefaultPageSettings.PaperSource = pkSource;

                    break;
                }

                //Printer Paper as Landscape

                switch ("1")
                {
                    case "0":
                        IPD.DefaultPageSettings.Landscape = true;
                        break;
                    case "1":
                        IPD.DefaultPageSettings.Landscape = false;
                        break;
                    default:
                        IPD.DefaultPageSettings.Landscape = true;
                        break;
                }

                IPD.Print();

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message, Configuration.Formatters.JsonFormatter);
            } 

            Thread.Sleep(2000);

            while (Program.lblMachineState == "PRINTING")
            {
                var xxx = "Printing";
            }

           
            return Request.CreateResponse(HttpStatusCode.OK, "تمت الطباعة بنجاح", Configuration.Formatters.JsonFormatter);

        }

        private void pd_BeginPrint(object beginPrintSender, PrintEventArgs beginPrintE)
        {
            try
            {
                var xxx = beginPrintE;
                //write your logic when printing ends  

            }
            catch (Exception e)
            {

            }
        }

        private void pd_PrintPageNewVersion(PDFTemplateFields obj, PrintPageEventArgs ev)
        {

            var MRZ = "";

            //  'Set color types
            var lBrush = new SolidBrush(Color.Black);

            var ocrbFontPath = @"C:\Users\admin\source\repos\PrinterEvisaSticker\PrinterEvisaSticker\fonts\OCRB Regular.ttf"; // Replace with the actual path to your font file
            var privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile(ocrbFontPath);

            //  'Set font types 
            var lFontOCRB = new Font("OCR-B", 6, FontStyle.Bold);
            var lFontOCRBMRZ = new Font(privateFontCollection.Families[0], 10, FontStyle.Bold);


            //' Change the unit of measurement to millimetres
            ev.Graphics.PageUnit = GraphicsUnit.Millimeter;

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Near;



            // Check if the input string has at least 44 characters
            if (obj.P_MRZ.Length >= 44)
            {
                // Split the string into two substrings of 44 characters each
                var substring1 = obj.P_MRZ.Substring(0, 44);
                var substring2 = obj.P_MRZ.Substring(44, 44);

                MRZ = substring1 + Environment.NewLine + substring2;
            }


            if (obj.P_VisaTypeSticker == "DA")
            {

                //DrawSting IN English Languages
                Image persoanlImage = ConvertBase64ToImage(obj.P_PersonlImage);
                //ImageSigneHusbend.RotateFlip(System.Drawing.RotateFlipType.ro);
                ev.Graphics.DrawImage(persoanlImage, 11f, 21f, 26f, 30f); //IMage SignHusbedn
                ev.Graphics.DrawString(obj.P_VisaType.ToUpper(), lFontOCRB, lBrush, 40f, 23f, drawFormat);
                ev.Graphics.DrawString(obj.P_VisaType_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 25.5f, drawFormat);
                ev.Graphics.DrawString(obj.P_DepaturePeriod_DA.ToUpper() + " / " + obj.P_DepaturePeriod_DA_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 31f, drawFormat);
                ev.Graphics.DrawString(obj.P_IssuingAuthority_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 38f, drawFormat);
                ev.Graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat);
                //  ev.Graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat); 
                ev.Graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);
                //  ev.Graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);  
                ev.Graphics.DrawString(obj.P_Requester_ARA.ToUpper(), lFontOCRB, lBrush, 61.5f, 51.5f, drawFormat); 
                ev.Graphics.DrawString(obj.P_DateOfIssue.ToUpper(), lFontOCRB, lBrush, 80f, 31f, drawFormat);
                ev.Graphics.DrawString(obj.P_DateOfExpiry.ToUpper(), lFontOCRB, lBrush, 80f, 38f, drawFormat);
                ev.Graphics.DrawString(MRZ, lFontOCRBMRZ, lBrush, 11f, 60.2f, drawFormat);

            }
            else
            {
                Image persoanlImage = ConvertBase64ToImage(obj.P_PersonlImage);
                //ImageSigneHusbend.RotateFlip(System.Drawing.RotateFlipType.ro);
                ev.Graphics.DrawImage(persoanlImage, 11f, 21f,26f, 30f); //IMage SignHusbedn
                ev.Graphics.DrawString(obj.P_VisaType.ToUpper(), lFontOCRB, lBrush, 40f, 23f, drawFormat);
                ev.Graphics.DrawString(obj.P_VisaType_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 25.5f, drawFormat);
                ev.Graphics.DrawString(obj.P_NoOfEntries.ToUpper() + " / " + obj.P_NoOfEntries_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 31f, drawFormat);
                ev.Graphics.DrawString(obj.P_IssuingAuthority_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 38f, drawFormat);
                ev.Graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat);
                //  ev.Graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat); 
                ev.Graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);
                //  ev.Graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);  
                ev.Graphics.DrawString(obj.P_Requester_ARA.ToUpper(), lFontOCRB, lBrush, 61.5f, 51.5f, drawFormat);
                ev.Graphics.DrawString(obj.P_DurationOfStay.ToUpper(), lFontOCRB, lBrush, 78f, 23f, drawFormat);
                ev.Graphics.DrawString("يوم", lFontOCRB, lBrush, 74f, 22.5f, drawFormat);
                ev.Graphics.DrawString("Day" + " / ", lFontOCRB, lBrush, 68f, 23f, drawFormat);
                ev.Graphics.DrawString(obj.P_DateOfIssue.ToUpper(), lFontOCRB, lBrush, 80f, 31f, drawFormat);
                ev.Graphics.DrawString(obj.P_DateOfExpiry.ToUpper(), lFontOCRB, lBrush, 80f, 38f, drawFormat);
                ev.Graphics.DrawString(MRZ, lFontOCRBMRZ, lBrush, 11f, 60.2f, drawFormat);

            }

            ev.HasMorePages = false;

        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TEST_PRINT()
        {

            //Get machine serial number

            SelectQuery Sq = new SelectQuery("Win32_BIOS");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(Sq);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            foreach (ManagementObject mo in osDetailsCollection)
            {

                string[] BIOSVersion = (string[])mo["BIOSVersion"];
                string s2 = null;
                foreach (string version in BIOSVersion)
                {
                    s2 += version;
                }

                var xxx = mo["SerialNumber"].ToString();
            }



            // Print PDF
            //  PdfPrinter.PrintPdf(filePath);

            return Request.CreateResponse(HttpStatusCode.OK, "Generated Sucess", Configuration.Formatters.JsonFormatter);
        }

        public Image ConvertBase64ToImage(string base64String)
        {

            Image image;

            image = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64String)));

            return image;

        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CheackServiceRuning()
        {
            //PdfDocument doc = new PdfDocument(@"C:/Users/admin/Desktop/Passport Printer/TemplatePassport/OriganlTrmplate/Template_MainPageRefugee.pdf");
            //doc.PrintSettings.PrinterName = "DILETTA 600i"; 
            //doc.Print();

            // Logger.WriteLog("ErrorMessage" +" "+"NO Error");

            return Request.CreateResponse(HttpStatusCode.OK, "Service Is Runing", Configuration.Formatters.JsonFormatter);
        }

    }
}
