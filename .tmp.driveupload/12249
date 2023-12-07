using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace GET.Printers.Vi1200
{
    public class Vi1200PrinterEthernetAPI_Raw
    {
        const string dllName = "PrinterEthCom.dll";
        [DllImport(dllName, EntryPoint = "SearchNetworkPrinters", CallingConvention = CallingConvention.Cdecl)]
        internal static extern API_RESULT SearchNetworkPrinters(ref int count, ref IntPtr printerList);

        [DllImport(dllName, EntryPoint = "OpenPrinterConnection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OpenPrinterConnection(string IP);

        [DllImport(dllName, EntryPoint = "ClosePrinterConnection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ClosePrinterConnection();

        [DllImport(dllName, EntryPoint = "SendToPrinter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SendToPrinter(IntPtr data, int len);

        [DllImport(dllName, EntryPoint = "RecvFromPrinter", CallingConvention = CallingConvention.Cdecl)]

        internal static extern IntPtr RecvFromPrinter(ref int len);

        [DllImport(dllName, EntryPoint = "AlignPrintHead", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool AlignPrintHead(int printerNo);//1.1.4.1

        [DllImport(dllName, EntryPoint = "capPrintHead", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CapPrinterHead(int PrinterNo);//1.1.4.2

             [DllImport(dllName, EntryPoint = "ConfirmErrorWarning", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ConfirmErrorWarning(int printerNo);//1.1.4.4

        [DllImport(dllName, EntryPoint = "getAutoCapTimeout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetAutoCapTimeout(int PrinterNo, ref int seconds);//1.1.4.5

        [DllImport(dllName, EntryPoint = "getCarriagePrintSpeedLimit", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetCarriagePrintSpeedLimit(int PrinterNo, ref int carriagePrintSpeedLimit_ips);//1.1.4.6

        [DllImport(dllName, EntryPoint = "getDryTime", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetDryTime(int PrinterNo, ref int seconds);//1.1.4.7


        [DllImport(dllName, EntryPoint = "GetErrorWarningList", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetErrorWarningList(int printerNo, ref IntPtr ErrorList);//1.1.4.8


        
        [DllImport(dllName, EntryPoint = "GetInkLevels", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetInkLevels(int printerNo, ref InkLevel_t inkLevel);//1.1.4.12



        [DllImport(dllName, EntryPoint = "getMachineStateString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetMachineStateString(MACHINE_STATE machineState);//1.1.4.14

        [DllImport(dllName, EntryPoint = "getManualLineFeedCalibrationValues", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetManualLineFeedCalibrationValues(int PrinterNo, ref int vertical, ref int amplitude, ref int angle);//1.1.4.15

        [DllImport(dllName, EntryPoint = "getPageCount", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetPageCount(int PrinterNo, ref int pageCount);//1.1.4.16

        [DllImport(dllName, EntryPoint = "getPaperPrintSpeedLimit", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetPaperPrintSpeedLimit(int PrinterNo, ref int inches_sec);//1.1.4.17

        [DllImport(dllName, EntryPoint = "getPpsLevel", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetPpsLevel(int PrinterNo, ref PPS_LEVEL pPSLevel);//1.1.4.18

        [DllImport(dllName, EntryPoint = "GetMachineState", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MACHINE_STATE GetPrinterMachineState(IntPtr printerStatus);//1.1.4.19
        

        [DllImport(dllName, EntryPoint = "GetPrinterName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetPrinterName(int PrinterNo);//1.1.4.20


        [DllImport(dllName, EntryPoint = "getPrinterStatusValues", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetPrinterStatusValues(int printerNo, ref IntPtr status);//1.1.4.21

        [DllImport(dllName, EntryPoint = "getWasteInkContainerCapacity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetWasteInkContainerCapacity(int PrinterNo, ref int capacity_ml);//1.1.4.22


        [DllImport(dllName, EntryPoint = "InitPrinterList")]
        internal static extern int InitPrinterList();//1.1.4.23

        [DllImport(dllName, EntryPoint = "PrinterGetLastError")]
        internal static extern ERROR_CODE PrinterGetLastError();//1.1.4.24


        [DllImport(dllName, EntryPoint = "resetWasteInkContainerCurrentLiquidLevel", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ResetWasteInkContainerCurrentLiquidLevel(int PrinterNo);//1.1.4.25


        [DllImport(dllName, EntryPoint = "setAutoCapTimeout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool setAutoCapTimeout(int PrinterNo, int seconds);//1.1.4.26

        [DllImport(dllName, EntryPoint = "setCarriagePrintSpeedLimit", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetCarriagePrintSpeedLimit(int PrinterNo, int carriagePrintSpeedLimit_ips);//1.1.4.27

        [DllImport(dllName, EntryPoint = "setDryTime", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetDryTime(int PrinterNo, int seconds);//1.1.4.28

        [DllImport(dllName, EntryPoint = "setManualLineFeedCalibrationValues", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetManualLineFeedCalibrationValues(int PrinterNo, int vertical, int amplitude, int angle);//1.1.4.29


        [DllImport(dllName, EntryPoint = "setPaperPrintSpeedLimit", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetPaperPrintSpeedLimit(int PrinterNo, int inches_sec);//1.1.4.30

        [DllImport(dllName, EntryPoint = "setPpsLevel", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetPpsLevel(int PrinterNo, PPS_LEVEL pPSLevel);//1.1.4.31


        [DllImport(dllName, EntryPoint = "setWasteInkContainerCapacity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetWasteInkContainerCapacity(int PrinterNo, int capacity_ml);//1.1.4.32

        [DllImport(dllName, EntryPoint = "uncapPrintHead", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool UncapPrintHead(int PrinterNo);//1.1.4.32

        [DllImport(dllName, EntryPoint = "checkMediaReadyForCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CheckMediaReadyForCamera(int PrinterNo, CAMERA_POSITION cameraPosition, ref bool cameraReady);//1.2.3.1

        [DllImport(dllName, EntryPoint = "ejectMediaWithoutPrint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool EjectMediaWithoutPrint(int PrinterNo);//1.2.3.2//TODO://

        [DllImport(dllName, EntryPoint = "GetEngineState", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ENGINE_STATE GetEngineState(IntPtr printerStatus);//1.2.3.3

        [DllImport(dllName, EntryPoint = "GetEngineStateString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetEngineStateString(ENGINE_STATE engineState);//1.2.3.4
                                                                                      //getManufactureAndModelNumber

        [DllImport(dllName, EntryPoint = "getManufactureAndModelNumber", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetManufactureAndModelNumber(int printerNo, ref int mfgMdlNr);//1.2.3.5

        [DllImport(dllName, EntryPoint = "getMediaEdgeCorrection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetMediaEdgeCorrection(int printerNo, bool left, ref int value);//1.2.3.6

        [DllImport(dllName, EntryPoint = "GetMediaLoadStatus", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MEDIA_LOAD_STATUS GetMediaLoadStatus(IntPtr printerStatus);//1.2.3.7

        [DllImport(dllName, EntryPoint = "GetMediaLoadStatusString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetMediaLoadStatusString(MEDIA_LOAD_STATUS mediaLoadStatus);//1.2.3.8

        [DllImport(dllName, EntryPoint = "getPickRollerDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string GetPickRollerDepth(int printerNo, ref int depth);//1.2.3.9

        [DllImport(dllName, EntryPoint = "getPickRollerReverseMove", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string GetPickRollerReverseMove(int printerNo, ref bool status);//1.2.3.10

        [DllImport(dllName, EntryPoint = "getPrinterInkLevel", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string GetPrinterInkLevel(int printerNo, InkLevel_t level);//1.2.3.11

        [DllImport(dllName, EntryPoint = "getVerticalEdgeDetectionCarriagePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string getVerticalEdgeDetectionCarriagePosition(int printerNo, ref int pos_inch);//1.2.3.12

        [DllImport(dllName, EntryPoint = "getWaitForFrontMediaLoop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern string getWaitForFrontMediaLoop(int printerNo, ref bool wait);//1.2.3.13

        [DllImport(dllName, EntryPoint = "getWasteInkLevel", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetWasteInkLevel(IntPtr printerStatus);//1.2.3.14

        [DllImport(dllName, EntryPoint = "isAuthorized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsAuthorized(IntPtr printerStatus);//1.2.3.15


        [DllImport(dllName, EntryPoint = "isAux1MotorStalled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsAux1MotorStalled(IntPtr printerStatus);//1.2.3.16


        [DllImport(dllName, EntryPoint = "isAux2MotorStalled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsAux2MotorStalled(IntPtr printerStatus);//1.2.3.17


        [DllImport(dllName, EntryPoint = "isCameraLedOn", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool isCameraLedOn(IntPtr printerStatus);//1.2.3.18


        [DllImport(dllName, EntryPoint = "isFrontCamConnected", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool isFrontCamConnected(IntPtr printerStatus);//1.2.3.19

        [DllImport(dllName, EntryPoint = "isFrontCamReady", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsFrontCamReady(IntPtr printerStatus);//1.2.3.20

        [DllImport(dllName, EntryPoint = "isNonApproved_C", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsNonApproved_C(IntPtr printerStatus);//1.2.3.21


        [DllImport(dllName, EntryPoint = "isNonApproved_K", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsNonApproved_K(IntPtr printerStatus);//1.2.3.22


        [DllImport(dllName, EntryPoint = "isNonApproved_M", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsNonApproved_M(IntPtr printerStatus);//1.2.3.23


        [DllImport(dllName, EntryPoint = "isNonApproved_Y", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsNonApproved_Y(IntPtr printerStatus);//1.2.3.24


        [DllImport(dllName, EntryPoint = "isNonApprovedInkUsed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsNonApprovedInkUsed(IntPtr printerStatus);//1.2.3.25

        [DllImport(dllName, EntryPoint = "isPaperJammed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsPaperJammed(IntPtr printerStatus);//1.2.3.26

        [DllImport(dllName, EntryPoint = "isPrintheadCapped", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsPrintheadCapped(IntPtr printerStatus);//1.2.3.27

        [DllImport(dllName, EntryPoint = "isRearCamConnected", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRearCamConnected(IntPtr printerStatus);//1.2.3.28


        [DllImport(dllName, EntryPoint = "isRearCamReady", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRearCamReady(IntPtr printerStatus);//1.2.3.29


        [DllImport(dllName, EntryPoint = "isRearDoorOpen", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRearDoorOpen(IntPtr printerStatus);//1.2.3.30

        [DllImport(dllName, EntryPoint = "isRefilled_C", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRefilled_C(IntPtr printerStatus);//1.2.3.31

        [DllImport(dllName, EntryPoint = "isRefilled_K", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRefilled_K(IntPtr printerStatus);//1.2.3.32

        [DllImport(dllName, EntryPoint = "isRefilled_M", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRefilled_M(IntPtr printerStatus);//1.2.3.33

        [DllImport(dllName, EntryPoint = "isRefilled_Y", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRefilled_Y(IntPtr printerStatus);//1.2.3.34

        [DllImport(dllName, EntryPoint = "isRefilledInkUsed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsRefilledInkUsed(IntPtr printerStatus);//1.2.3.35

        [DllImport(dllName, EntryPoint = "isWaitingForFrontMediaInsert", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsWaitingForFrontMediaInsert(IntPtr printerStatus);//1.2.3.36


        [DllImport(dllName, EntryPoint = "isWasteInkContainerMissing", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsWasteInkContainerMissing(IntPtr printerStatus);//1.2.3.37


        [DllImport(dllName, EntryPoint = "isWasteInkFullWarning", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsWasteInkFullWarning(IntPtr printerStatus);//1.2.3.38


        [DllImport(dllName, EntryPoint = "loadMediaToFrontCameraPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool LoadMediaToFrontCameraPosition(int printerNo);//1.2.3.39


        [DllImport(dllName, EntryPoint = "loadMediaToPrintPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool LoadMediaToPrintPosition(int printerNo);//1.2.3.40


        [DllImport(dllName, EntryPoint = "paperLoadTest", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool PaperLoadTest(int printerNo, LOAD_POSITION loadPosition);//1.2.3.41

        [DllImport(dllName, EntryPoint = "setExternalFactoryDefaults", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetExternalFactoryDefaults(int printerNo);//1.2.3.42


        [DllImport(dllName, EntryPoint = "setMediaEdgeCorrection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetMediaEdgeCorrection(int printerNo, bool left, int value);//1.2.3.43

        [DllImport(dllName, EntryPoint = "setPickRollerDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool setPickRollerDepth(int printerNo, int depth);//1.2.3.44

        [DllImport(dllName, EntryPoint = "setPickRollerReverseMove", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetPickRollerReverseMove(int printerNo, bool status);//1.2.3.45

        [DllImport(dllName, EntryPoint = "setVerticalEdgeDetectionCarriagePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SetVerticalEdgeDetectionCarriagePosition(int PrinterNo, int pos_inch);//1.2.3.46

        [DllImport(dllName, EntryPoint = "setWaitForFrontMediaLoop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool setWaitForFrontMediaLoop(int printerNo, bool wait);//1.2.3.47

        [DllImport(dllName, EntryPoint = "SwitchCameraLED", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SwitchCameraLED(int printerNo, bool ledOn);//1.2.3.48


    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterEthComMessage
    {
        //[FieldOffset(0)]
        internal NET_SERVICE_HEADER Header;
        //[FieldOffset(12)]
        internal ETHERNET_FUNCTION_ID FunctionID;
        //[FieldOffset(16)]
        internal OP_TYPE Optype;
        //[FieldOffset(20)]
        // public ERRCODE ErrCode; //RESUKT FAILED

    //[MarshalAs(UnmanagedType.ByValArray,SizeConst =4,ArraySubType = UnmanagedType.I1)]
       //// [FieldOffset(20)] 
      public IntPtr Data;    //RESULT OKAY 
    }

    public enum ETHERNET_ERROR_CODE
    {
        API_VERSION_MISMATCH = 0,
        NOT_IMPLEMENTED, //e.g FunctionID is not Implemented
        UNSUPPORTED, //e.g. you try to SET the MODEL while only GET is supported.
        BUSSY, //you try to do something which is currently not possible for any reason. You should try to do the same later.
        SIZE_ERROR,
    };
    public enum OP_TYPE:int
    {
        GET,
        SET,
        RUN,
        RESULT_OKAY_GET,
        RESULT_OKAY_SET,
        RESULT_OKAY_RUN,
        RESULT_FAILED_GET,
        RESULT_FAILED_SET,
        RESULT_FAILED_RUN
    }
    public enum MediaLoadStatus_t
    {
        NO_MEDIA_LOADED,
        FRONT_MEDIA_AT_CAMERA,
        MEDIA_AT_PRINT,
        GRABBED_AT_FRONT,
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct NET_SERVICE_HEADER
    {
        public int Major;
        public int Minor;
        public int Length;
    }
    public enum API_RESULT:int
    {
        RESULT_OK,     // 0 = OKAY, everything else should be treated as error
        UNKNOWN_ERROR,
        ERROR_PRINTER_COMMUNICATION,
        ERROR_CONNECTION_NOT_ESTABLISHED,
        ERROR_VERSION,
    }

    internal enum ETHERNET_FUNCTION_ID:int
    {
        API_VERSION = 0,
        FIRMWARE_BASE_VERSION,
        FIRMWARE_VERSION,
        MANUFACTURER,
        MODEL_NAME,
        INK_LEVEL,
        PRINTER_STATUS,
        PAGE_COUNT,

        IMAGE = 2000,
        LOAD_MEDIA_TO_FRONT_CAMERA_POS,
        LOAD_MEDIA_TO_PRINT_POS,
        EJECT_MEDIA_WITHOUT_PRINT,
        CAMERA_LED,
        FEED_TEST,
        INIT_MEDIA_PATH,
    }

}
