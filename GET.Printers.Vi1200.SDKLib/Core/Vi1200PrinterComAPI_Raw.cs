
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace GET.Printers.Vi1200
{
    public partial class Vi1200PrinterUsbAPI_Raw
    {
        const string DLL = "REST_USB";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printerNo"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "AlignPrintHead", CallingConvention = convention)]
        internal static extern bool AlignPrintHead(int printerNo);//1.4.3.1
        /// <summary>
        /// capPrintHead will cap the printhead.

        /// </summary>
        /// <param name="PrinterNo">the number of the Printer for which you want to cap the print head.</param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "capPrintHead", CallingConvention = convention)]
        internal static extern bool CapPrinterHead(int PrinterNo);//1.4.3.2
        /// <summary>
        /// Checks if a passbook is inserted or not. Attention: The value might be wrong if you are not in sensor position.
        /// </summary>
        /// <param name="PrinterNo">the number of the Printer for which you want to cap the print head.</param>
        /// <param name="mediaIn">[out] this variable will be filled with true or false depending on Media In detection</param>
        /// <returns>true on success false otherwise</returns>

        [DllImport(DLL, EntryPoint = "checkMediaIn", CallingConvention = convention)]
        internal static extern bool CheckMediaIn(int PrinterNo, ref int mediaIn);//1.4.3.3

        ///
        [DllImport(DLL, EntryPoint = "checkPrinter", CallingConvention = convention)]
        internal static extern int CheckPrinter(int PrinterNo);//1.4.3.4

        /// <summary>
        /// cleanPrinthead runs a Printhead service routine to get rid of cloged nozzles. please use level 2 only if really neccessary.(only when level 0 and 1 are not able to recover from cloged nozzels )
        /// </summary>
        /// <param name="PrinterNo"></param>
        /// <param name="lvl">defines the intensity of the cleaning. the valid range is from 0..2 where 2 is the strongest but consumes more ink</param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "cleanPrinthead", CallingConvention = convention)]
        internal static extern int CleanPrinthead(int PrinterNo, int lvl);//1.4.3.5

        /// <summary>
        /// ConfirmErrorWarning confirms errors like jamInPrinter and retries
        /// </summary>
        /// <param name="printerNo"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "ConfirmErrorWarning", CallingConvention = convention)]
        internal static extern bool ConfirmErrorWarning(int printerNo);//1.4.3.6


        [DllImport(DLL, EntryPoint = "getAutoCapTimeout", CallingConvention = convention)]
        internal static extern bool GetAutoCapTimeout(int PrinterNo, ref int seconds);//1.4.3.7


        [DllImport(DLL, EntryPoint = "getBaseFirmwareVersionString", CallingConvention = convention)]
        internal static extern IntPtr GetBaseFirmwareVersionString(int printerNo);//1.4.3.8



        [DllImport(DLL, EntryPoint = "getCarriagePrintSpeedLimit", CallingConvention = convention)]
        internal static extern bool GetCarriagePrintSpeedLimit(int PrinterNo, ref int carriagePrintSpeedLimit_ips);//1.4.3.9


        [DllImport(DLL, EntryPoint = "getDryTime", CallingConvention = convention)]
        internal static extern bool GetDryTime(int PrinterNo, ref int seconds);//1.4.3.10


        [DllImport(DLL, EntryPoint = "GetErrorWarningList", CallingConvention = convention)]
        internal static extern bool GetErrorWarningList(int printerNo, ref IntPtr ErrorList);//1.4.3.11


        [DllImport(DLL, EntryPoint = "getFirmwareVersionString", CallingConvention = convention)]
        internal static extern IntPtr GetFirmwareVersionString(int printerNo);//1.4.3.12


        [DllImport(DLL, EntryPoint = "GetInkLevelC", CallingConvention = convention)]
        internal static extern int GetInkLevelC(ref PrinterStatus_t printerStatus);//1.4.3.13

        [DllImport(DLL, EntryPoint = "GetInkLevelK", CallingConvention = convention)]
        internal static extern int GetInkLevelK(ref PrinterStatus_t printerStatus);//1.4.3.14

        [DllImport(DLL, EntryPoint = "GetInkLevelM", CallingConvention = convention)]
        internal static extern int GetInkLevelM(ref PrinterStatus_t printerStatus);//1.4.3.15


        [DllImport(DLL, EntryPoint = "GetInkLevels", CallingConvention = convention)]
        internal static extern bool GetInkLevels(int printerNo, ref InkLevel_t inkLevel);//1.4.3.16


        [DllImport(DLL, EntryPoint = "GetInkLevelY", CallingConvention = convention)]
        internal static extern int GetInkLevelY(ref PrinterStatus_t printerStatus);//1.4.3.17


        [DllImport(DLL, EntryPoint = "GetMachineState", CallingConvention = convention)]
        internal static extern MACHINE_STATE GetMachineState(ref PrinterStatus_t printerStatus);//1.4.3.18


        [DllImport(DLL, EntryPoint = "getMachineStateString", CallingConvention = convention)]
        internal static extern IntPtr GetMachineStateString(MACHINE_STATE machineState);//1.4.3.19


        [DllImport(DLL, EntryPoint = "getManualLineFeedCalibrationValues", CallingConvention = convention)]
        internal static extern bool GetManualLineFeedCalibrationValues(int PrinterNo, ref int vertical, ref int amplitude, ref int angle);//1.4.3.20


        [DllImport(DLL, EntryPoint = "getMediaEdgeCorrection", CallingConvention = convention)]
        internal static extern bool GetMediaEdgeCorrection(int printerNo, int left, ref int value);//1.4.3.21

        [DllImport(DLL, EntryPoint = "getOemValues", CallingConvention = convention)]
        internal static extern bool GetOemValues(int printerNo,ref OEMValues_t oEMValues_);//1.4.3.22

        


        [DllImport(DLL, EntryPoint = "getPageCount", CallingConvention = convention)]
        internal static extern bool GetPageCount(int PrinterNo, ref int pageCount);//1.4.3.23


        [DllImport(DLL, EntryPoint = "getPaperPrintSpeedLimit", CallingConvention = convention)]
        internal static extern bool GetPaperPrintSpeedLimit(int PrinterNo, ref int inches_sec);//1.4.3.24

        [DllImport(DLL, EntryPoint = "getPpsLevel", CallingConvention = convention)]
        internal static extern bool GetPpsLevel(int PrinterNo, ref PPS_LEVEL pPSLevel);//1.4.3.25



        [DllImport(DLL, EntryPoint = "getPreJobCloggRecoverySpitCycles", CallingConvention = convention)]
        internal static extern bool GetPreJobCloggRecoverySpitCycles(int PrinterNo, ref int spitCycles);//1.4.3.26


        [DllImport(DLL, EntryPoint = "GetPrinterMachineState", CallingConvention = convention)]
        internal static extern MACHINE_STATE GetPrinterMachineState(ref PrinterStatus_t printerStatus);//1.4.3.27


        [DllImport(DLL, EntryPoint = "GetPrinterName", CallingConvention = convention)]
        internal static extern IntPtr GetPrinterName(int PrinterNo);//1.4.3.28


        [DllImport(DLL, EntryPoint = "getWasteInkContainerCapacity", CallingConvention = convention)]
        internal static extern bool GetWasteInkContainerCapacity(int PrinterNo, ref int capacity_ml);//1.4.3.29


        [DllImport(DLL, EntryPoint = "InitPrinterList", CallingConvention = convention)]
        internal static extern int InitPrinterList();////1.4.3.31


        [DllImport(DLL, EntryPoint = "InkCodeErrorC", CallingConvention = convention)]
        internal static extern int InkCodeErrorC(ref PrinterStatus_t printerStatus);////1.4.3.32


        [DllImport(DLL, EntryPoint = "InkCodeErrorK", CallingConvention = convention)]
        internal static extern int InkCodeErrorK(ref PrinterStatus_t printerStatus);////1.4.3.33


        [DllImport(DLL, EntryPoint = "InkCodeErrorM", CallingConvention = convention)]
        internal static extern int InkCodeErrorM(ref PrinterStatus_t printerStatus);////1.4.3.34


        [DllImport(DLL, EntryPoint = "InkCodeErrorY", CallingConvention = convention)]
        internal static extern int InkCodeErrorY(ref PrinterStatus_t printerStatus);////1.4.3.35

        


        [DllImport(DLL, EntryPoint = "IsRefilledInkC", CallingConvention = convention)]
        internal static extern int IsRefilledInkC(ref PrinterStatus_t printerStatus);////1.4.3.36


        [DllImport(DLL, EntryPoint = "IsRefilledInkK", CallingConvention = convention)]
        internal static extern int IsRefilledInkK(ref PrinterStatus_t printerStatus);////1.4.3.37


        [DllImport(DLL, EntryPoint = "IsRefilledInkM", CallingConvention = convention)]
        internal static extern int IsRefilledInkM(ref PrinterStatus_t printerStatus);////1.4.3.38


        [DllImport(DLL, EntryPoint = "IsRefilledInkY", CallingConvention = convention)]
        internal static extern int IsRefilledInkY(ref PrinterStatus_t printerStatus);////1.4.3.39

        [DllImport(DLL , EntryPoint = "printCleaningVerificationPage", CallingConvention = convention)]

        internal static extern int PrintCleaningVerificationPage(ref PrinterStatus_t printerStatus);////1.4.3.41


        [DllImport(DLL, EntryPoint = "PrinterGetLastError", CallingConvention = convention)]
        internal static extern ERROR_CODE PrinterGetLastError();//1.4.3.42


        [DllImport(DLL, EntryPoint = "printFromBuffer", CallingConvention = convention)]

        public static extern int PrintFromBuffer(int PrinterNo, byte[] buffer, int size);//1.4.3.43

        [DllImport(DLL, EntryPoint = "printNetworkConfigurationPage", CallingConvention = convention)]

        public static extern int PrintNetworkConfigurationPage(int PrinterNo);//1.4.3.44

        [DllImport(DLL, EntryPoint = "resetWasteInkContainerCurrentLiquidLevel", CallingConvention = convention)]
        internal static extern bool ResetWasteInkContainerCurrentLiquidLevel(int PrinterNo);////1.4.3.45


        [DllImport(DLL, EntryPoint = "REST_USB_GetNetworkSettings", CallingConvention = convention)]
        public static extern int GetNetworkSettings(int printerNo, ref IntPtr HostName, ref IntPtr IPv4DomainName, ref IntPtr IPv6DomainName); //1.4.3.48




        [DllImport(DLL, EntryPoint = "REST_USB_getPrinterStatus", CallingConvention = convention)]
        internal static extern bool GetPrinterStatusValues(int printerNo, ref PrinterStatus_t status);//1.4.3.49


        [DllImport(DLL, EntryPoint = "REST_USB_getSourceTray", CallingConvention = convention)]

        internal static extern int GetSourceTray(int PrinterNo, ref SOURCE_TRAY sourceTray);//1.4.3.51


        [DllImport(DLL, EntryPoint = "REST_USB_printDiagnosticsPage", CallingConvention = convention)]

        public static extern int PrintDiagnosticsPage(int PrinterNo);//1.4.3.52


        [DllImport(DLL, EntryPoint = "REST_USB_setLeftMargin", CallingConvention = convention)]

        internal static extern int SetLeftMargin(int PrinterNo, int left);////1.4.3.54

        [DllImport(DLL, EntryPoint = "REST_USB_setLeftMargin", CallingConvention = convention)]

        internal static extern int GetProductUsage(int printerNo, ref ProductUsage_t productUsage_T );


        [DllImport(DLL, EntryPoint = "REST_USB_setMargins", CallingConvention = convention)]

        internal static extern int SetMargins(int PrinterNo, int left, int top);//1.4.3.55


        [DllImport(DLL, EntryPoint = "REST_USB_setSourceTray", CallingConvention = convention)]

        internal static extern int SetSourceTray(int PrinterNo, SOURCE_TRAY sourceTray);//1.4.3.57




        [DllImport(DLL, EntryPoint = "setAutoCapTimeout", CallingConvention = convention)]
        internal static extern bool SetAutoCapTimeout(int PrinterNo, int seconds);//1.4.3.59

        [DllImport(DLL, EntryPoint = "setCarriagePrintSpeedLimit", CallingConvention = convention)]
        internal static extern bool SetCarriagePrintSpeedLimit(int PrinterNo, int carriagePrintSpeedLimit_ips);//1.4.3.60

        [DllImport(DLL, EntryPoint = "setDryTime", CallingConvention = convention)]
        internal static extern bool SetDryTime(int PrinterNo, int seconds);//1.4.3.61

        [DllImport(DLL, EntryPoint = "setManualLineFeedCalibrationValues", CallingConvention = convention)]
        internal static extern bool SetManualLineFeedCalibrationValues(int PrinterNo, int vertical, int amplitude, int angle);//1.4.3.62


        [DllImport(DLL, EntryPoint = "setPaperPrintSpeedLimit", CallingConvention = convention)]
        internal static extern bool SetPaperPrintSpeedLimit(int PrinterNo, int inches_sec);//1.4.3.64

        [DllImport(DLL, EntryPoint = "setPpsLevel", CallingConvention = convention)]
        internal static extern bool SetPpsLevel(int PrinterNo, PPS_LEVEL pPSLevel);////1.4.3.65


        [DllImport(DLL, EntryPoint = "setWasteInkContainerCapacity", CallingConvention = convention)]
        internal static extern bool SetWasteInkContainerCapacity(int PrinterNo, int capacity_ml);////1.4.3.67

        [DllImport(DLL, EntryPoint = "uncapPrintHead", CallingConvention = convention)]
        internal static extern bool UncapPrintHead(int PrinterNo);////1.4.3.68









    }

  

    

}
