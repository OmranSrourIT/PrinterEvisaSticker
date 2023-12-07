using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GET.Printers.Vi1200
{
    public class Vi1200PrinterEthernet : Vi1200Printer
    {
        public Vi1200PrinterEthernet(int printerNumber, string printerName)
        {
            this.ConnectionType = CONNECTION_TYPE.NETWORK;
            this.PrinterName = printerName;
            this.PrinterNumber = printerNumber;

        }

        internal static API_RESULT GetPrintersList(out List<Vi1200Printer> printers)
        {
            int count = 0;
             printers = new List<Vi1200Printer>();

            IntPtr ptr = new IntPtr();
            var result = Vi1200PrinterEthernetAPI_Raw.SearchNetworkPrinters(ref count, ref ptr);
            for (int i = 0; i < count; i++)
            {
                var p1 = Marshal.ReadInt32(ptr);
                if (p1 == 0)
                {
                    break;
                }
                var name = Marshal.PtrToStringAnsi(new IntPtr(p1));
                printers.Add(new Vi1200PrinterEthernet(i, name));

                ptr += 4;
            }
            // Marshal.FreeHGlobal(ptr);
            return result;
        }
        void SendRequestToPrinter(ETHERNET_FUNCTION_ID function, OP_TYPE optype, int data = 0)
        {

            var msg = new PrinterEthComMessage();
            var size = Marshal.SizeOf(msg);
            var ptr = Marshal.AllocHGlobal(size);
            msg.Header.Major = 0;
            msg.Header.Minor = 1;
            msg.Header.Length = size;
            msg.FunctionID = function;
            msg.Optype = optype;
            msg.Data = new IntPtr(data);
            //msg.Data[0] =(byte) data;

            Marshal.StructureToPtr(msg, ptr, false);
            var result = Vi1200PrinterEthernetAPI_Raw.SendToPrinter(ptr, size);
        }
        bool ReadResponse(out IntPtr ptrData, out PrinterEthComMessage ethComMessage)
        {
            int responseLength = 0;
            ethComMessage = new PrinterEthComMessage();
            ptrData = new IntPtr(0);
            var ptr = Vi1200PrinterEthernetAPI_Raw.RecvFromPrinter(ref responseLength);
            if (responseLength > 0)
            {
                ptrData = new IntPtr(ptr.ToInt32() + Marshal.SizeOf(typeof(PrinterEthComMessage)) - 4);
                ethComMessage = (PrinterEthComMessage)Marshal.PtrToStructure(ptr, typeof(PrinterEthComMessage));
                return true;
            }
            return false;
        }

        protected override bool GetPrinterStatus(out Vi1200Status printerStatus)
        {
            IntPtr ptrData;
            printerStatus = new Vi1200Status();
           

            SendRequestToPrinter(ETHERNET_FUNCTION_ID.PRINTER_STATUS, OP_TYPE.GET);

            var result = ReadResponse(out ptrData, out PrinterEthComMessage responseMessage);
            if (result == true)
            {
                //var bytes = new byte[responseMessage.Header.Length];
                //Marshal.Copy(ptrData, bytes, 0, responseMessage.Header.Length);

                //IntPtr ptr = Marshal.AllocHGlobal(bytes.Length);

                //Marshal.Copy(bytes, 0, ptr, bytes.Length);


                //  var x = (PrinterStatus_t)Marshal.PtrToStructure(ptrData, typeof(PrinterStatus_t));


                if (responseMessage.Optype == OP_TYPE.RESULT_OKAY_GET)
                {
                    result = true;
                    printerStatus.MachineState = Vi1200PrinterEthernetAPI_Raw.GetPrinterMachineState(ptrData);

                    printerStatus.IsAuthorized = Vi1200PrinterEthernetAPI_Raw.IsAuthorized(ptrData);//( t_printerStatus);
                    printerStatus.EngineState = Vi1200PrinterEthernetAPI_Raw.GetEngineState(ptrData);
                    printerStatus.IsWasteInkContainerMissing = Vi1200PrinterEthernetAPI_Raw.IsWasteInkContainerMissing(ptrData);
                    printerStatus.IsWasteInkFullWarning = Vi1200PrinterEthernetAPI_Raw.IsWasteInkFullWarning(ptrData);
                    printerStatus.IsCameraLedOn = Vi1200PrinterEthernetAPI_Raw.isCameraLedOn(ptrData);
                    printerStatus.IsFrontCameraConnected = Vi1200PrinterEthernetAPI_Raw.isFrontCamConnected(ptrData);
                    printerStatus.IsRearCameraConnected = Vi1200PrinterEthernetAPI_Raw.IsRearCamConnected(ptrData);
                    printerStatus.IsFrontCameraReady = Vi1200PrinterEthernetAPI_Raw.IsFrontCamReady(ptrData);
                    printerStatus.IsRearCameraReady = Vi1200PrinterEthernetAPI_Raw.IsRearCamReady(ptrData);
                    printerStatus.IsWaitingForFrontMediaInsert = Vi1200PrinterEthernetAPI_Raw.IsWaitingForFrontMediaInsert(ptrData);
                    //printerStatus.IsRearDoorOpen = Vi1200PrinterEthernetAPI_Raw.IsRearDoorOpen(ptrData);
                    printerStatus.IsAux1MotorStalled = Vi1200PrinterEthernetAPI_Raw.IsAux1MotorStalled(ptrData);
                    printerStatus.IsAux2MotorStalled = Vi1200PrinterEthernetAPI_Raw.IsAux2MotorStalled(ptrData);
                    printerStatus.IsPaperJammed = Vi1200PrinterEthernetAPI_Raw.IsPaperJammed(ptrData);
                    printerStatus.IsPrintheadCapped = Vi1200PrinterEthernetAPI_Raw.IsPrintheadCapped(ptrData);
                    printerStatus.WasteInkLevel = Vi1200PrinterEthernetAPI_Raw.GetWasteInkLevel(ptrData);
                    printerStatus.MediaLoadStatus = Vi1200PrinterEthernetAPI_Raw.GetMediaLoadStatus(ptrData);
                    printerStatus.IsNonApprovedInkUsed = Vi1200PrinterEthernetAPI_Raw.IsNonApprovedInkUsed(ptrData);
                    printerStatus.IsNonApproved_C = Vi1200PrinterEthernetAPI_Raw.IsNonApproved_C(ptrData);
                    printerStatus.IsNonApproved_K = Vi1200PrinterEthernetAPI_Raw.IsNonApproved_K(ptrData);
                    printerStatus.IsNonApproved_M = Vi1200PrinterEthernetAPI_Raw.IsNonApproved_M(ptrData);
                    printerStatus.IsNonApproved_Y = Vi1200PrinterEthernetAPI_Raw.IsNonApproved_Y(ptrData);
                    printerStatus.IsRefilledInkUsed = Vi1200PrinterEthernetAPI_Raw.IsRefilledInkUsed(ptrData);
                    printerStatus.IsRefilled_C = Vi1200PrinterEthernetAPI_Raw.IsRefilled_C(ptrData);
                    printerStatus.IsRefilled_K = Vi1200PrinterEthernetAPI_Raw.IsRefilled_K(ptrData);
                    printerStatus.IsRefilled_M = Vi1200PrinterEthernetAPI_Raw.IsRefilled_M(ptrData);
                    printerStatus.IsRefilled_Y = Vi1200PrinterEthernetAPI_Raw.IsRefilled_Y(ptrData);
                    

                   var isInkReturned= GetInkLevels(out ViInkLevel inkLevel);
                    if(isInkReturned)
                    {
                        printerStatus.InkLevels = inkLevel;
                    }


                  
                }
            }
            return result;
        }

        //public override bool MoveToPrintCommand()
        //{
        //    SendRequestToPrinter(ETHERNET_FUNCTION_ID.LOAD_MEDIA_TO_PRINT_POS, OP_TYPE.RUN);
        //    var result = ReadResponse(out IntPtr ptrData, out PrinterEthComMessage responseMessage);

        //    if (responseMessage.Optype == OP_TYPE.RESULT_FAILED_RUN ||
        //        responseMessage.Optype == OP_TYPE.RESULT_FAILED_SET)
        //    {

        //        var output = new byte[responseMessage.Header.Length];
        //        Marshal.Copy(ptrData, output, 0, responseMessage.Header.Length);
        //        var error = (ETHERNET_ERROR_CODE)output[0];
        //    }
        //    return true;
        //}

        //public override bool MoveToCameraCommand()
        //{
        //    SendRequestToPrinter(ETHERNET_FUNCTION_ID.LOAD_MEDIA_TO_FRONT_CAMERA_POS, OP_TYPE.RUN);
        //    var result = ReadResponse(out IntPtr ptrData, out PrinterEthComMessage responseMessage);
        //    if (responseMessage.Optype == OP_TYPE.RESULT_FAILED_RUN || responseMessage.Optype == OP_TYPE.RESULT_FAILED_SET)
        //    {

        //        var output = new byte[responseMessage.Header.Length];
        //        Marshal.Copy(ptrData, output, 0, responseMessage.Header.Length);
        //        var error = (ETHERNET_ERROR_CODE)output[0];
        //    }

        //    return true;
        //}

        //public override bool CaptureImage(out byte[] image)
        //{

        //    SendRequestToPrinter(ETHERNET_FUNCTION_ID.IMAGE, OP_TYPE.GET);
        //    var result = ReadResponse(out IntPtr ptrData, out PrinterEthComMessage responseMessage);
        //    image = null;
        //    if (responseMessage.Optype == OP_TYPE.RESULT_OKAY_GET)
        //    {
        //        image = new byte[responseMessage.Header.Length];
        //        Marshal.Copy(ptrData, image, 0, responseMessage.Header.Length);
        //        return true;
        //    }
        //    else return false;
        //}
        bool GetInkLevel(out InkLevel_t inkLevels)
        {
            inkLevels = new InkLevel_t();
            SendRequestToPrinter(ETHERNET_FUNCTION_ID.INK_LEVEL, OP_TYPE.GET);
            var result = ReadResponse(out IntPtr ptrData, out PrinterEthComMessage responseMessage);
            byte[] output = null;
            if (responseMessage.Optype == OP_TYPE.RESULT_OKAY_GET)
            {
                output = new byte[responseMessage.Header.Length];
                Marshal.Copy(ptrData, output, 0, responseMessage.Header.Length);
                inkLevels.K = Convert.ToChar(output[0]);
                inkLevels.C = Convert.ToChar(output[1]);
                inkLevels.M = Convert.ToChar(output[2]);
                inkLevels.Y = Convert.ToChar(output[3]);
            }
            return result;
        }

        //public override bool OpenConnection()
        //{
        //    CloseConnection();
        //    var isConnectionOpened = Vi1200PrinterEthernetAPI_Raw.OpenPrinterConnection(this.PrinterName);
        //    return isConnectionOpened;

        //}

        //public override bool CloseConnection()
        //{
        //    Vi1200PrinterEthernetAPI_Raw.ClosePrinterConnection();
        //    return true;
        //}

        //public override bool LedOnOff(bool isLedOn)
        //{
        //    int data = isLedOn ? 1 : 0;
        //    SendRequestToPrinter(ETHERNET_FUNCTION_ID.CAMERA_LED, OP_TYPE.SET, data);
        //    var result = ReadResponse(out IntPtr ptrData, out PrinterEthComMessage responseMessage);
        //    if (responseMessage.Optype == OP_TYPE.RESULT_FAILED_RUN || responseMessage.Optype == OP_TYPE.RESULT_FAILED_SET)
        //    {

        //        var output = new byte[responseMessage.Header.Length];
        //        Marshal.Copy(ptrData, output, 0, responseMessage.Header.Length);
        //        var error = (ETHERNET_ERROR_CODE)output[0];
        //        return false;
        //    }
        //    return true;

        //}



        //public override bool PaperLoadTest(LOAD_POSITION position)
        //{
        //    SendRequestToPrinter(ETHERNET_FUNCTION_ID.FEED_TEST, OP_TYPE.SET);
        //    var result = ReadResponse(out IntPtr ptrData, out PrinterEthComMessage responseMessage);
        //    if (responseMessage.Optype == OP_TYPE.RESULT_FAILED_RUN || responseMessage.Optype == OP_TYPE.RESULT_FAILED_SET)
        //    {

        //        var output = new byte[responseMessage.Header.Length];
        //        Marshal.Copy(ptrData, output, 0, responseMessage.Header.Length);
        //        var error = (ETHERNET_ERROR_CODE)output[0];
        //        return false;
        //    }
        //    return true;
        //}

        //public override List<string> GetWarningErrors()
        //{
        //    throw new NotImplementedException();
        //}



         bool GetInkLevels(out ViInkLevel inkLevel)
        {
            var result = GetInkLevel(out InkLevel_t _inklevel);

            inkLevel = new ViInkLevel();
            if (result == true)
            {
                inkLevel.Yellow = _inklevel.Y;
                inkLevel.Cayan = _inklevel.C;
                inkLevel.Black = _inklevel.K;
                inkLevel.Magenta = _inklevel.M;
            }
            return result;

        }


    }
}
