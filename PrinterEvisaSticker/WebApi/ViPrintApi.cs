using GET.Printers.Vi1200;
using iText.IO.Font;
using iText.Kernel.Font;
using OpenCvSharp;
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
using System.Runtime.InteropServices;
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
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetOrderStatus()
        { 

            ApiResult oApi = new ApiResult();
             
            try
            {


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
                 
                if (Program.ObjectConfig.PrinterName == null)
                {
                    oApi.ErrorMessage = "تأكد من أن الطابعة قيد الشتغيل  و متصلة بجهاز الكمبوتر ";
                    oApi.ErrorCode = "01";
                    oApi.IsError = false;
                    return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);

                }

                if (lblIsAuthorized == "False")
                {
                    oApi.ErrorMessage = Convert.ToString("لا توجد صلاحية للدخول");
                    oApi.ErrorCode = "01";
                    oApi.IsError = false;
                    return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
                }
                if (lblMachineState == "OUT_OF_PAPER")
                {
                    oApi.ErrorMessage = Convert.ToString("لقد نفذ الورق");
                    oApi.ErrorCode = "01";
                    oApi.IsError = false;
                    return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
                }
                if (lblEngineState == "REAR_DOOR_OPEN")
                {
                    oApi.ErrorMessage = "يرجى اغلاق الباب الخلفي للطابعة";
                    oApi.ErrorCode = "01";
                    oApi.IsError = false;
                    return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
                }
                if (lblMachineState != "IDLE")
                {
                    oApi.ErrorMessage = Convert.ToString(Program.lblMachineState);
                    oApi.ErrorCode = "01";
                    oApi.IsError = false;
                    return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
                }
                 

            }
            catch (Exception ex)
            {
                oApi.IsError = true;
                oApi.ErrorMessage = ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }

            oApi.Data = lblOrderStatus;
            oApi.ErrorCode = "00";

            return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);


        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PreviewBFprinting(PDFTemplateFields objDataForm)
        {
            ApiResult oApi = new ApiResult();

            try
            {

                int width = 2716;
                int height = 2267;
                var Bmb = new Bitmap(width, height);
              

                Bmb = pd_PrintPreiveiwPage(objDataForm);

                string base64String = ConvertBitmapToBase64(Bmb);
                oApi.Data = base64String;

            }
            catch(Exception ex)
            {
                oApi.ErrorMessage = ex.Message;
                oApi.IsError = true;
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);

            }
           
            return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
             
        }




        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.HttpPost]
        public  HttpResponseMessage PrintSticker(PDFTemplateFields objDataForm)
        {
              
            PaperSource pkSource;
            PaperSize pkSize;//this for size
            var NewSerialNoPrinter = string.Empty;

            ApiResult oApi = new ApiResult();
          

            var SerialNoDevice = GetSerialNoDevice();
            var SerialNoPrinter = Program.ObjectConfig.PrinterNumber;
            if(SerialNoPrinter != null)
            {
                NewSerialNoPrinter = SerialNoPrinter.Trim(new Char[] { '[', ']' });
            }else
            {
                oApi.ErrorMessage = "الطابعة غير مهيئه يرجى اعادة تشغيلها";
                oApi.ErrorCode = "01";
                oApi.IsError = false;
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }
           
            if(NewSerialNoPrinter.ToUpper() != objDataForm.P_SerialNoPrinter.ToUpper())
            {
                oApi.ErrorMessage = "هذه الطابعة غير فعالة لهذا المستخدم";
                oApi.ErrorCode = "01";
                oApi.IsError = false;
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }

            if (SerialNoDevice.ToUpper() != objDataForm.P_SerialNoDevice.ToUpper())
            {
                oApi.ErrorMessage = "هذا الجهاز غير فعال للطباعة لهذا المستخدم";
                oApi.ErrorCode = "01";
                oApi.IsError = false;
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }

            
            try
            {

                var Bmb = pd_PrintPageNewVersion(objDataForm);

                Program.PrintingOrders.Add(Bmb);


                //var IPD = new PrintDocument();
                //IPD.PrinterSettings.PrinterName = Program.ObjectConfig.PrinterName;
                //IPD.PrintPage += (sender, e) => pd_PrintPageNewVersion_Copy(objDataForm, e);

                ////Set the Paper size A4 , A5 .....bla bla
                //for (int i = 0; i < IPD.PrinterSettings.PaperSizes.Count; i++)
                //{
                //    pkSize = IPD.PrinterSettings.PaperSizes[i];
                //    IPD.DefaultPageSettings.PaperSize = pkSize;
                //    break;
                //}

                ////Set the Paper source 

                //for (int i = 0; i < IPD.PrinterSettings.PaperSources.Count; i++)
                //{
                //    pkSource = IPD.PrinterSettings.PaperSources[i];
                //    IPD.DefaultPageSettings.PaperSource = pkSource;

                //    break;
                //}

                ////Printer Paper as Landscape

                //switch ("1")
                //{
                //    case "0":
                //        IPD.DefaultPageSettings.Landscape = true;
                //        break;
                //    case "1":
                //        IPD.DefaultPageSettings.Landscape = false;
                //        break;
                //    default:
                //        IPD.DefaultPageSettings.Landscape = true;
                //        break;
                //}

                //IPD.Print();

            }
            catch (Exception ex)
            {
                oApi.ErrorMessage = ex.Message;
                oApi.IsError = true; 
                return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);
            }

            oApi.ErrorCode = "00";
            oApi.ErrorMessage = "تمت عملية الطباعة بنجاح";

            

            return Request.CreateResponse(HttpStatusCode.OK, oApi, Configuration.Formatters.JsonFormatter);

        }
         
        private void pd_PrintPageNewVersion_Copy(PDFTemplateFields obj, PrintPageEventArgs ev)
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



            //// Check if the input string has at least 44 characters
            //if (obj.P_MRZ.Length >= 44)
            //{
            //    // Split the string into two substrings of 44 characters each
            //    var substring1 = obj.P_MRZ.Substring(0, 44);
            //    var substring2 = obj.P_MRZ.Substring(44, 44);

             
            //}
            MRZ = obj.P_MRZ_FirstLine + Environment.NewLine + obj.P_MRZ_SecoundLine;

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
                if (obj.P_NationalityParam == "ARA")
                {
                    ev.Graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat);
                    ev.Graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);
                }
                else
                {
                    ev.Graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat);
                    ev.Graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);
                }
                ev.Graphics.DrawString(obj.P_Requester_ARA.ToUpper(), lFontOCRB, lBrush, 61.5f, 51.5f, drawFormat);
                ev.Graphics.DrawString(obj.P_DateOfIssue.ToUpper(), lFontOCRB, lBrush, 80f, 31f, drawFormat);
                ev.Graphics.DrawString(obj.P_DateOfExpiry.ToUpper(), lFontOCRB, lBrush, 80f, 38f, drawFormat);
                ev.Graphics.DrawString(MRZ, lFontOCRBMRZ, lBrush, 11f, 60.2f, drawFormat);

            }
            else
            {
                Image persoanlImage = ConvertBase64ToImage(obj.P_PersonlImage);
                //ImageSigneHusbend.RotateFlip(System.Drawing.RotateFlipType.ro);
                ev.Graphics.DrawImage(persoanlImage, 11f, 21f, 26f, 30f); //IMage SignHusbedn
                ev.Graphics.DrawString(obj.P_VisaType.ToUpper(), lFontOCRB, lBrush, 40f, 23f, drawFormat);
                ev.Graphics.DrawString(obj.P_VisaType_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 25.5f, drawFormat);
                ev.Graphics.DrawString(obj.P_NoOfEntries.ToUpper() + " / " + obj.P_NoOfEntries_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 31f, drawFormat);
                ev.Graphics.DrawString(obj.P_IssuingAuthority_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 38f, drawFormat);
                if (obj.P_NationalityParam == "ARA")
                {
                    ev.Graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat);
                    ev.Graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);
                }
                else
                {
                    ev.Graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 45.5f, drawFormat);
                    ev.Graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 51.5f, drawFormat);
                }
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


        private Bitmap pd_PrintPreiveiwPage(PDFTemplateFields obj)
        {

            var MRZ = "";

            int width = 2716;
            int height = 1800;
            Bitmap image = new Bitmap(width, height);
            image.SetResolution(600, 600);
            // Create a graphics object from the image
            using (var graphics = Graphics.FromImage(image))
            {

                graphics.Clear(Color.White);
                //  'Set color types
                var lBrush = new SolidBrush(Color.Black);
                var redBrush = new SolidBrush(Color.DarkRed);
                var NewPathFont = @"C:\Program Files (x86)\Default Company Name\PrinterVisaSetup\OCRB Regular.ttf"; 
                var privateFontCollection = new PrivateFontCollection();
                privateFontCollection.AddFontFile(NewPathFont);

                //  'Set font types 
                var lFontOCRB = new Font("OCR-B", 6, FontStyle.Bold);
                var lFontForLable = new Font("OCR-B",4);
                var lFontForHeaderAR = new Font("OCR-B", 6, FontStyle.Bold);
                var lFontForHeaderEV = new Font("OCR-B", 5);
                var lFontOCRBMRZ = new Font(privateFontCollection.Families[0], 10, FontStyle.Bold);


                //' Change the unit of measurement to millimetres
                graphics.PageUnit = GraphicsUnit.Millimeter;

                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Near;
                MRZ = obj.P_MRZ_FirstLine + Environment.NewLine + obj.P_MRZ_SecoundLine;

                if (obj.P_VisaTypeSticker == "DA")
                {

                    //Lable Visa Sticker for DA  8

                    //Header
                    graphics.DrawString("سمة مغادرة", lFontForHeaderAR, lBrush, 11f, 3f, drawFormat);
                    graphics.DrawString("ڤیزەی ڕۆشتن", lFontForHeaderAR, lBrush, 11f, 6f, drawFormat);
                    graphics.DrawString("Depature Visa".ToUpper(), lFontForHeaderEV, lBrush, 11f, 9f, drawFormat);

                    //DataPage
                    graphics.DrawString("Visa Type"+ " / " + "نوع السمة" + " / " + "جۆری ڤیزە", lFontForLable, redBrush, 40f, 17f, drawFormat);
                    graphics.DrawString("Depature Period" + " / " + "مدة المغادرة" + " / " + "ماوەی ڕۆشتن", lFontForLable, redBrush, 40f, 24.5f, drawFormat);
                    graphics.DrawString("Issuing Authority" + " / " + "جهة الأصدار" + " / " + "دەرهێنەر", lFontForLable, redBrush, 40f, 31f, drawFormat);
                    graphics.DrawString("Full Name" + " / " + "الأسم الكامل" + " / " + "ناوى تەواو", lFontForLable, redBrush, 40f, 38.5f, drawFormat);
                    graphics.DrawString("Nationality" + " / " + "الجنسية" + " / " + "ڕەگەزنامە", lFontForLable, redBrush, 40f, 45f, drawFormat);
                    graphics.DrawString("Requester" + " / " + "جهة الطلب" + " / " + "لایەنی داواکار", lFontForLable, redBrush, 61.5f, 45f, drawFormat);
                    graphics.DrawString("Date Of Issue" + " / " + "تاريخ الأصدار" + " / " + "بەرواری دەرچوون", lFontForLable, redBrush, 80f, 24.5f, drawFormat);
                    graphics.DrawString("Date Of Expiry" + " / " + "تاريخ النفاذ" + " / " + "بەرواری بەسەرچوون", lFontForLable, redBrush, 80f, 31f, drawFormat);

                      
 




                    //DrawSting IN English Languages
                    Image persoanlImage = ConvertBase64ToImage(ConverterImage(obj.P_PersonlImage));
                    //ImageSigneHusbend.RotateFlip(System.Drawing.RotateFlipType.ro);
                    graphics.DrawImage(persoanlImage, 11f, 15.5f, 26f, 32.5f); //IMage SignHusbedn
                    graphics.DrawString(obj.P_VisaType.ToUpper(), lFontOCRB, lBrush, 40f, 19f, drawFormat);
                    graphics.DrawString(obj.P_VisaType_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 21.5f, drawFormat);
                    graphics.DrawString(obj.P_DepaturePeriod_DA.ToUpper() + " / " + obj.P_DepaturePeriod_DA_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 27f, drawFormat);
                    graphics.DrawString(obj.P_IssuingAuthority_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 34f, drawFormat);
                    if (obj.P_NationalityParam == "ARA")
                    {
                        graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    else
                    {
                        graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    graphics.DrawString(obj.P_Requester_ARA.ToUpper(), lFontOCRB, lBrush, 61.5f, 47.5f, drawFormat);
                    graphics.DrawString(obj.P_DateOfIssue.ToUpper(), lFontOCRB, lBrush, 80f, 27f, drawFormat);
                    graphics.DrawString(obj.P_DateOfExpiry.ToUpper(), lFontOCRB, lBrush, 80f, 34f, drawFormat);
                    graphics.DrawString(MRZ, lFontOCRBMRZ, lBrush, 11f, 56.2f, drawFormat);

                }
                else
                {

                    //Lable Visa Sticker for AA 9 
                     
                    //Header
                    graphics.DrawString("سمة دخول", lFontForHeaderAR, lBrush, 11f, 3f, drawFormat);
                    graphics.DrawString("ڤیزە", lFontForHeaderAR, lBrush, 11f, 6f, drawFormat);
                    graphics.DrawString("VISA".ToUpper(), lFontForHeaderEV, lBrush, 11f, 9f, drawFormat);

                    //DataPage
                    graphics.DrawString("Duration Of Stay" + " / " + "مدة الأقامة" + " / " + "ماوەی مانەوە", lFontForLable, redBrush, 61.5f,17f, drawFormat);
                    graphics.DrawString("Visa Type" + " / " + "نوع السمة" + " / " + "جۆری ڤیزە", lFontForLable, redBrush, 40f, 17f, drawFormat);
                    graphics.DrawString("No Of Entries" + " / " + "عدد مرات الدخول" + " / " + "ژمارەی کاتەکانی چوونە ژوورەوە", lFontForLable, redBrush, 40f, 24.5f, drawFormat);
                    graphics.DrawString("Issuing Authority" + " / " + "جهة الأصدار" + " / " + "دەرهێنەر", lFontForLable, redBrush, 40f, 31f, drawFormat);
                    graphics.DrawString("Full Name" + " / " + "الأسم الكامل" + " / " + "ناوى تەواو", lFontForLable, redBrush, 40f, 38.5f, drawFormat);
                    graphics.DrawString("Nationality" + " / " + "الجنسية" + " / " + "ڕەگەزنامە", lFontForLable, redBrush, 40f, 45f, drawFormat);
                    graphics.DrawString("Requester" + " / " + "جهة الطلب" + " / " + "لایەنی داواکار", lFontForLable, redBrush, 61.5f, 45f, drawFormat);
                    graphics.DrawString("Date Of Issue" + " / " + "تاريخ الأصدار" + " / " + "بەرواری دەرچوون", lFontForLable, redBrush, 80f, 24.5f, drawFormat);
                    graphics.DrawString("Date Of Expiry" + " / " + "تاريخ النفاذ" + " / " + "بەرواری بەسەرچوون", lFontForLable, redBrush, 80f, 31f, drawFormat);



                    Image persoanlImage = ConvertBase64ToImage(ConverterImage(obj.P_PersonlImage));
                    //ImageSigneHusbend.RotateFlip(System.Drawing.RotateFlipType.ro);
                    graphics.DrawImage(persoanlImage, 11f, 15.5f, 26f, 32.5f); //IMage SignHusbedn
                    graphics.DrawString(obj.P_VisaType.ToUpper(), lFontOCRB, lBrush, 40f, 19f, drawFormat);
                    graphics.DrawString(obj.P_VisaType_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 21.5f, drawFormat);
                    graphics.DrawString(obj.P_NoOfEntries.ToUpper() + " / " + obj.P_NoOfEntries_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 27f, drawFormat);
                    graphics.DrawString(obj.P_IssuingAuthority_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 34f, drawFormat);
                    if (obj.P_NationalityParam == "ARA")
                    {
                        graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    else
                    {
                        graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    graphics.DrawString(obj.P_Requester_ARA.ToUpper(), lFontOCRB, lBrush, 61.5f, 47.5f, drawFormat);
                    graphics.DrawString(obj.P_DurationOfStay.ToUpper(), lFontOCRB, lBrush, 78f, 19f, drawFormat);
                    graphics.DrawString("يوم", lFontOCRB, lBrush, 74f, 18.5f, drawFormat);
                    graphics.DrawString("DAY" + " / ", lFontOCRB, lBrush, 68f, 19f, drawFormat);
                    graphics.DrawString(obj.P_DateOfIssue.ToUpper(), lFontOCRB, lBrush, 80f, 27f, drawFormat);
                    graphics.DrawString(obj.P_DateOfExpiry.ToUpper(), lFontOCRB, lBrush, 80f, 34f, drawFormat);
                    graphics.DrawString(MRZ, lFontOCRBMRZ, lBrush, 11f, 56.2f, drawFormat);
                }

                // graphics.HasMorePages = false;
            }

            return image;
        }

        private Bitmap pd_PrintPageNewVersion(PDFTemplateFields obj)
        {

            var MRZ = "";

            int width = 2716;
            int height = 2267;
            Bitmap image = new Bitmap(width, height);
            image.SetResolution(600, 600);
            // Create a graphics object from the image
            using (var graphics = Graphics.FromImage(image))
            {

                graphics.Clear(Color.White);
                //  'Set color types
                var lBrush = new SolidBrush(Color.Black);

                var NewPathFont = @"C:\Program Files (x86)\Default Company Name\PrinterVisaSetup\OCRB Regular.ttf";
                var privateFontCollection = new PrivateFontCollection();
                privateFontCollection.AddFontFile(NewPathFont);

                //  'Set font types 
                var lFontOCRB = new Font("OCR-B", 6, FontStyle.Bold);
                var lFontOCRBMRZ = new Font(privateFontCollection.Families[0], 10, FontStyle.Bold);


                //' Change the unit of measurement to millimetres
                graphics.PageUnit = GraphicsUnit.Millimeter;

                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Near;
                MRZ = obj.P_MRZ_FirstLine + Environment.NewLine + obj.P_MRZ_SecoundLine;
                if (obj.P_VisaTypeSticker == "DA")
                {

                    //DrawSting IN English Languages
                    Image persoanlImage =  ConvertBase64ToImage(ConverterImage(obj.P_PersonlImage));
                    //ImageSigneHusbend.RotateFlip(System.Drawing.RotateFlipType.ro);
                    graphics.DrawImage(persoanlImage, 11f, 15.5f, 26f, 32.5f); //IMage SignHusbedn
                    graphics.DrawString(obj.P_VisaType.ToUpper(), lFontOCRB, lBrush, 40f, 19f, drawFormat);
                    graphics.DrawString(obj.P_VisaType_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 21.5f, drawFormat);
                    graphics.DrawString(obj.P_DepaturePeriod_DA.ToUpper() + " / " + obj.P_DepaturePeriod_DA_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 27f, drawFormat);
                    graphics.DrawString(obj.P_IssuingAuthority_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 34f, drawFormat);
                    if (obj.P_NationalityParam == "ARA")
                    {
                        graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    else
                    {
                        graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    graphics.DrawString(obj.P_Requester_ARA.ToUpper(), lFontOCRB, lBrush, 61.5f, 47.5f, drawFormat);
                    graphics.DrawString(obj.P_DateOfIssue.ToUpper(), lFontOCRB, lBrush, 80f, 27f, drawFormat);
                    graphics.DrawString(obj.P_DateOfExpiry.ToUpper(), lFontOCRB, lBrush, 80f, 34f, drawFormat);
                    graphics.DrawString(MRZ, lFontOCRBMRZ, lBrush, 11f, 56.2f, drawFormat);

                }
                else
                {
                    Image persoanlImage = ConvertBase64ToImage(ConverterImage(obj.P_PersonlImage));
                    //ImageSigneHusbend.RotateFlip(System.Drawing.RotateFlipType.ro);
                    graphics.DrawImage(persoanlImage, 11f, 15.5f, 26f, 32.5f); //IMage SignHusbedn
                    graphics.DrawString(obj.P_VisaType.ToUpper(), lFontOCRB, lBrush, 40f, 19f, drawFormat);
                    graphics.DrawString(obj.P_VisaType_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 21.5f, drawFormat);
                    graphics.DrawString(obj.P_NoOfEntries.ToUpper() + " / " + obj.P_NoOfEntries_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 27f, drawFormat);
                    graphics.DrawString(obj.P_IssuingAuthority_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 34f, drawFormat);
                    if (obj.P_NationalityParam == "ARA")
                    {
                        graphics.DrawString(obj.P_FullName_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality_ARA.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    else
                    {
                        graphics.DrawString(obj.P_FullName.ToUpper(), lFontOCRB, lBrush, 40f, 41.5f, drawFormat);
                        graphics.DrawString(obj.P_Nationality.ToUpper(), lFontOCRB, lBrush, 40f, 47.5f, drawFormat);
                    }
                    graphics.DrawString(obj.P_Requester_ARA.ToUpper(), lFontOCRB, lBrush, 61.5f, 47.5f, drawFormat);
                    graphics.DrawString(obj.P_DurationOfStay.ToUpper(), lFontOCRB, lBrush, 78f, 19f, drawFormat);
                    graphics.DrawString("يوم", lFontOCRB, lBrush, 74f, 18.5f, drawFormat);
                    graphics.DrawString("DAY" + " / ", lFontOCRB, lBrush, 68f, 19f, drawFormat);
                    graphics.DrawString(obj.P_DateOfIssue.ToUpper(), lFontOCRB, lBrush, 80f, 27f, drawFormat);
                    graphics.DrawString(obj.P_DateOfExpiry.ToUpper(), lFontOCRB, lBrush, 80f, 34f, drawFormat);
                    graphics.DrawString(MRZ, lFontOCRBMRZ, lBrush, 11f, 56.2f, drawFormat);

                }

               // graphics.HasMorePages = false;
            }

            return image;
        }


        public Image ConvertBase64ToImage(string base64String)
        {

            Image image;

            image = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64String)));

            return image;

        }
        static string ConvertMatToBase64(Mat image)
        {
            // Convert the Mat image to a byte array
            byte[] imageBytes = image.ToBytes();

            // Convert the byte array to base64
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }


        public string GetSerialNoDevice()
        {
            var SerialNumber = string.Empty;
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

                SerialNumber = mo["SerialNumber"].ToString();

                
            }

            return SerialNumber;
        }

        static string ConvertBitmapToBase64(Bitmap bitmap)
        {
            // Convert Bitmap to MemoryStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the Bitmap to the MemoryStream in a specific format (e.g., PNG)
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                // Convert MemoryStream to byte array
                byte[] byteArr = memoryStream.ToArray();

                // Convert byte array to base64 string
                string base64String = Convert.ToBase64String(byteArr);

                return base64String;
            }
        }

        private Mat ConvertBase64ToMat(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return Mat.FromStream(stream, ImreadModes.Color);
            }
        }
         
        public string ConverterImage(string Base64Param)
        { 
            //string inputImagePath = @"C:\Users\admin\Desktop\input\download.png";

            //Mat originalImage = Cv2.ImRead(inputImagePath);

            Mat originalImage = ConvertBase64ToMat(Base64Param);

            Mat grayImage = new Mat();
            Cv2.CvtColor(originalImage, grayImage, ColorConversionCodes.BGR2GRAY);

            int threshold = 200;
            Mat mask = grayImage.Threshold(threshold, 255, ThresholdTypes.Binary);

            Mat invertedMask = new Mat();
            Cv2.BitwiseNot(mask, invertedMask);

            Mat whiteBackground = new Mat(originalImage.Size(), MatType.CV_8UC3, Scalar.White);

            originalImage.CopyTo(whiteBackground, invertedMask);

            var base64IMageReuslt = ConvertMatToBase64(whiteBackground);


            // Dispose of the Mats to free up resources
            originalImage.Dispose();
            grayImage.Dispose();
            mask.Dispose();
            invertedMask.Dispose();
            whiteBackground.Dispose();

            return base64IMageReuslt;
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
