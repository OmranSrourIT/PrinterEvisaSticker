using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GET.Printers.Vi1200
{
    public class Vi1200PrinterUsb : Vi1200Printer
    {
        public Vi1200PrinterUsb(int printerNumber, string printerName)
        {
            this.PrinterName = printerName;
            this.PrinterNumber = printerNumber;
            this.ConnectionType = CONNECTION_TYPE.USB;
            //FireNewOrderEvent();
           
        }

        public static API_RESULT GetPrinters(out List<Vi1200Printer> printers)
        {
            var result = API_RESULT.RESULT_OK;
            int count = Vi1200PrinterUsbAPI_Raw.InitPrinterList();
            printers = new List<Vi1200Printer>();
            for (int i = 0; i < count; i++)
            {
                var intptr = Vi1200PrinterUsbAPI_Raw.GetPrinterName(i);
                var printerName = Marshal.PtrToStringAnsi(intptr);
                var printer = new Vi1200PrinterUsb(i, printerName);

              
                printers.Add(printer);

            }
            return result;
        }

        int GetProductUsage(int printerNo,out ProductUsage_t productUsage)
        {
            productUsage = new ProductUsage_t();
            var result = Vi1200PrinterUsbAPI_Raw.GetProductUsage(printerNo, ref productUsage);

            return result;
        }
        bool  GetManfactureAndModelNumber(int printerNo,out int mfgMdlMr)
        {
            IntPtr ptrMfgMdlMr = new IntPtr(0);
            mfgMdlMr = 0;
           var result= Vi1200PrinterUsbAPI_Raw.GetManufactureAndModelNumber(printerNo, ref mfgMdlMr);
            

            return result;
        }
        string GetBaseFirmwareVersion(int printerNo)
        {
            var ptrVersion= Vi1200PrinterUsbAPI_Raw.GetBaseFirmwareVersionString(printerNo);
           var sVersion= Marshal.PtrToStringAnsi(ptrVersion);
            return sVersion;
        }
        string GetFirmwareVersion(int printerNo)
        {
            var ptrVersion = Vi1200PrinterUsbAPI_Raw.GetFirmwareVersionString(printerNo);
            var sVersion= Marshal.PtrToStringAnsi(ptrVersion);
            return sVersion;
        }
        int GetSourceTray(out SOURCE_TRAY sourceTray)
        {
            sourceTray = SOURCE_TRAY.REAR_SHEET_FEEDER;
            int result=Vi1200PrinterUsbAPI_Raw.GetSourceTray(this.PrinterNumber,ref sourceTray);
            return result;
        }
        int SetSourceTray( SOURCE_TRAY sourceTray)
        {
            int result = Vi1200PrinterUsbAPI_Raw.SetSourceTray(this.PrinterNumber,  sourceTray);
            return result;
        }
        //public override bool CaptureImage(out byte[] image)
        //{
        //    //Write your own code to capture Image through web caM
        //    image = new byte[0];
        //    return false;
        //}

     

        //public override bool PaperLoadTest(LOAD_POSITION loadPosition)
        //{
        //    return Vi1200PrinterUsbAPI_Raw.PaperLoadTest(this.PrinterNumber, loadPosition);
        //}

        protected override bool GetPrinterStatus(out Vi1200Status printerStatus)
        {
            var t_printerStatus = new PrinterStatus_t();
            printerStatus = new Vi1200Status();
            var result = Vi1200PrinterUsbAPI_Raw.GetPrinterStatusValues(this.PrinterNumber, ref t_printerStatus);
            if (result)
            {
                printerStatus.IsAuthorized = Vi1200PrinterUsbAPI_Raw.IsAuthorized(ref t_printerStatus);
                printerStatus.EngineState = Vi1200PrinterUsbAPI_Raw.GetEngineState(ref t_printerStatus);
                printerStatus.MachineState = Vi1200PrinterUsbAPI_Raw.GetMachineState(ref t_printerStatus);
                printerStatus.IsWasteInkContainerMissing = Vi1200PrinterUsbAPI_Raw.IsWasteInkContainerMissing(ref t_printerStatus);
                printerStatus.IsWasteInkFullWarning = Vi1200PrinterUsbAPI_Raw.IsWasteInkFullWarning(ref t_printerStatus);
                printerStatus.IsCameraLedOn = Vi1200PrinterUsbAPI_Raw.IsCameraLedOn(ref t_printerStatus);
                printerStatus.IsFrontCameraConnected = Vi1200PrinterUsbAPI_Raw.IsFrontCamConnected(ref t_printerStatus);
                printerStatus.IsRearCameraConnected = Vi1200PrinterUsbAPI_Raw.IsRearCamConnected(ref t_printerStatus);
                printerStatus.IsFrontCameraReady = Vi1200PrinterUsbAPI_Raw.IsFrontCamReady(ref t_printerStatus);
                printerStatus.IsRearCameraReady = Vi1200PrinterUsbAPI_Raw.IsRearCamReady(ref t_printerStatus);
                printerStatus.IsWaitingForFrontMediaInsert = Vi1200PrinterUsbAPI_Raw.IsWaitingForFrontMediaInsert(ref t_printerStatus);
                //printerStatus.IsRearDoorOpen = Vi1200PrinterUsbAPI_Raw.IsRearDoorOpen(ref t_printerStatus);
                printerStatus.IsAux1MotorStalled = Vi1200PrinterUsbAPI_Raw.IsAux1MotorStalled(ref t_printerStatus);
                printerStatus.IsAux2MotorStalled = Vi1200PrinterUsbAPI_Raw.IsAux2MotorStalled(ref t_printerStatus);
                printerStatus.IsPaperJammed = Vi1200PrinterUsbAPI_Raw.IsPaperJammed(ref t_printerStatus);
                printerStatus.IsPrintheadCapped = Vi1200PrinterUsbAPI_Raw.IsPrintheadCapped(ref t_printerStatus);
                printerStatus.WasteInkLevel = Vi1200PrinterUsbAPI_Raw.GetWasteInkLevel(ref t_printerStatus);
                printerStatus.MediaLoadStatus = Vi1200PrinterUsbAPI_Raw.GetMediaLoadStatus(ref t_printerStatus);
                printerStatus.IsNonApprovedInkUsed = Vi1200PrinterUsbAPI_Raw.IsNonApprovedInkUsed(ref t_printerStatus);
                printerStatus.IsNonApproved_C = Vi1200PrinterUsbAPI_Raw.IsNonApproved_C(ref t_printerStatus);
                printerStatus.IsNonApproved_K = Vi1200PrinterUsbAPI_Raw.IsNonApproved_K(ref t_printerStatus);
                printerStatus.IsNonApproved_M = Vi1200PrinterUsbAPI_Raw.IsNonApproved_M(ref t_printerStatus);
                printerStatus.IsNonApproved_Y = Vi1200PrinterUsbAPI_Raw.IsNonApproved_Y(ref t_printerStatus);
                printerStatus.IsRefilledInkUsed = Vi1200PrinterUsbAPI_Raw.IsRefilledInkUsed(ref t_printerStatus);
                printerStatus.IsRefilled_C = Vi1200PrinterUsbAPI_Raw.IsRefilled_C(ref t_printerStatus);
                printerStatus.IsRefilled_K = Vi1200PrinterUsbAPI_Raw.IsRefilled_K(ref t_printerStatus);
                printerStatus.IsRefilled_M = Vi1200PrinterUsbAPI_Raw.IsRefilled_M(ref t_printerStatus);
                printerStatus.IsRefilled_Y = Vi1200PrinterUsbAPI_Raw.IsRefilled_Y(ref t_printerStatus);
                printerStatus.InkLevels.Cayan= Vi1200PrinterUsbAPI_Raw.GetInkLevelC(ref t_printerStatus);
                printerStatus.InkLevels.Black = Vi1200PrinterUsbAPI_Raw.GetInkLevelK(ref t_printerStatus);
                printerStatus.InkLevels.Magenta = Vi1200PrinterUsbAPI_Raw.GetInkLevelM(ref t_printerStatus);
                printerStatus.InkLevels.Yellow= Vi1200PrinterUsbAPI_Raw.GetInkLevelY(ref t_printerStatus);


                var omValues = new OEMValues_t() { Nvm = t_printerStatus.Nvm, Ro = t_printerStatus.Ro, Rw = t_printerStatus.Rw };

                // printerStatus.IsLowInkWarning= Vi1200PrinterUsbAPI_Raw.IsLowInkWarning(omValues);
                //  printerStatus.IsLowInkWarning_C = Vi1200PrinterUsbAPI_Raw.IsLowInkWarning_C(omValues);
                //  printerStatus.IsLowInkWarning_K = Vi1200PrinterUsbAPI_Raw.IsLowInkWarning_K(omValues);
                //  printerStatus.IsLowInkWarning_M = Vi1200PrinterUsbAPI_Raw.IsLowInkWarning_M(omValues);
                //  printerStatus.IsLowInkWarning_Y = Vi1200PrinterUsbAPI_Raw.IsLowInkWarning_Y(omValues);
                 int pageCount = 0,wastIntk=0;
                if (Vi1200PrinterUsbAPI_Raw.GetPageCount(this.PrinterNumber, ref pageCount))
                {

                    printerStatus.TotalPrintCount = pageCount;
                }
               // int inches_sec = 0;PPS_LEVEL pPS_LEVEL= PPS_LEVEL.AUTO;
                //var r1=Vi1200PrinterUsbAPI_Raw.GetPaperPrintSpeedLimit(this.PrinterNumber, ref inches_sec);
                //var r2 = Vi1200PrinterUsbAPI_Raw.GetPpsLevel(this.PrinterNumber, ref pPS_LEVEL);
                //var r4 = Vi1200PrinterUsbAPI_Raw.GetWasteInkContainerCapacity(this.PrinterNumber, ref wastIntk);

               //var isInkCodeErrorC= Vi1200PrinterUsbAPI_Raw.InkCodeErrorC(ref t_printerStatus);
               // var isInkCodeErrorK = Vi1200PrinterUsbAPI_Raw.InkCodeErrorK(ref t_printerStatus);
               // var isInkCodeErrorM= Vi1200PrinterUsbAPI_Raw.InkCodeErrorM(ref t_printerStatus);
               // var isInkCodeErrorY = Vi1200PrinterUsbAPI_Raw.InkCodeErrorY(ref t_printerStatus);

               // var IsRefilledInkC = Vi1200PrinterUsbAPI_Raw.IsRefilledInkC(ref t_printerStatus);
               // var IsRefilledInkK = Vi1200PrinterUsbAPI_Raw.IsRefilledInkK(ref t_printerStatus);
               // var IsRefilledInkM = Vi1200PrinterUsbAPI_Raw.IsRefilledInkM(ref t_printerStatus);
               // var IsRefilledInkY = Vi1200PrinterUsbAPI_Raw.IsRefilledInkY(ref t_printerStatus);

            }
            return result;

        }

        //public override bool LedOnOff(bool isLedOn)
        //{
        //    return Vi1200PrinterUsbAPI_Raw.SwitchCameraLED(this.PrinterNumber, isLedOn);
        //}

        //public override List<string> GetWarningErrors()
        //{
        //    var ptrErrorList = new IntPtr();
        //    var errorList = new List<string>();

        //    var result = Vi1200PrinterUsbAPI_Raw.GetErrorWarningList(0, ref ptrErrorList);

        //    if (result)
        //    {
        //        while (true)
        //        {
        //            var p1 = Marshal.ReadInt32(ptrErrorList);
        //            if (p1 == 0)
        //            {
        //                break;
        //            }
        //            var error = Marshal.PtrToStringAnsi(new IntPtr(p1));
        //          if(!errorList.Contains(error))  errorList.Add(error);

        //            ptrErrorList += 4;

        //        }
        //    }
        //    return errorList;
        //}

       

        //public override bool MoveToCameraCommand()
        //{
        //    return Vi1200PrinterUsbAPI_Raw.LoadMediaToFrontCameraPosition(this.PrinterNumber);
        //}

        //public override bool MoveToPrintCommand()
        //{
        //    return Vi1200PrinterUsbAPI_Raw.LoadMediaToPrintPosition(this.PrinterNumber);
        //}

        //public override bool OpenConnection()
        //{
        //    return true;
        //}

        //public override bool GetInkLevels(out ViInkLevel inkLevel)
        //{
        //   InkLevel_t _inkLevel = new InkLevel_t();
        //    var inkLevelResult = Vi1200PrinterUsbAPI_Raw.GetInkLevels(this.PrinterNumber, ref _inkLevel);
         
        //    inkLevel = new ViInkLevel
        //    {
        //        Cayan = _inkLevel.C,
        //        Black = _inkLevel.K,
        //        Yellow = _inkLevel.Y,
        //        Magenta = _inkLevel.M
        //    };

        //    InkLevel_t _inkLevel2 = new InkLevel_t();

        //    inkLevelResult = Vi1200PrinterUsbAPI_Raw.GetPrinterInkLevel(this.PrinterNumber, ref _inkLevel2);

        //    inkLevel = new ViInkLevel
        //    {
        //        Cayan = _inkLevel.C,
        //        Black = _inkLevel.K,
        //        Yellow = _inkLevel.Y,
        //        Magenta = _inkLevel.M
        //    };
        //    return inkLevelResult;
        //}

        //public override bool CloseConnection()
        //{
        //    return true;
        //}

        //public override bool Start()
        //{
        //    base.Start();
        //    return true;
        //}
    }
}
