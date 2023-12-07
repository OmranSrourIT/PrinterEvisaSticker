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
        /*******************GET HEader*************************************************************************************************///
        /// <summary>
        //checkMediaReadyForCamera checks if media is in the right position for the camera

        /// </summary>
        /// <param name="PrinterNo"></param>
        /// <param name="cameraPosition">1=front 2=back</param>
        /// <param name="cameraReady">reference to save the result</param>
        /// <returns></returns>

         const CallingConvention convention =  CallingConvention.Cdecl;
          [DllImport(DLL, EntryPoint = "checkMediaReadyForCamera", CallingConvention = convention)]
        internal static extern bool CheckMediaReadyForCamera(int PrinterNo, CAMERA_POSITION cameraPosition, ref bool cameraReady);//1.5.4.1

        /// <summary>
        /// @brief ejectMediaWithoutPrint ejects media without print

        /// </summary>
        /// <param name="PrinterNo">Printer No</param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "ejectMediaWithoutPrint", CallingConvention = convention)]
        public static extern bool EjectMediaWithoutPrint(int PrinterNo);//1.5.4.2

        /// <summary>
        ///@brief The GetEngineState function is a helper function which looks up the bits for engine state inside the PrinterStatus_t struct.
        /// </summary>
        /// <param name="printerStatus">printerStatus pass in the pointer to the printerStatus struct which you did read out before.</param>
        /// <returns>EngineState_t enumeration</returns>

        [DllImport(DLL, EntryPoint = "GetEngineState", CallingConvention = convention)]
        internal static extern ENGINE_STATE GetEngineState(ref PrinterStatus_t t_printerStatus);////1.5.4.3

        /// <summary>
        /// @brief GetEngineStateString converts the enumeration EngineState_t(number) to text representation
        /// </summary>
        /// <param name="engineState">engineState pass in the enum value</param>
        /// <returns>the text representation of the given enum value. type is (const char*) pointer to the first character. (zero terminated)</returns>

        [DllImport(DLL, EntryPoint = "GetEngineStateString", CallingConvention = convention)]
        internal static extern IntPtr GetEngineStateString(ENGINE_STATE engineState);//1.5.4.5

        /// <summary>
        /// getManufactureAndModelNumber get manufacture and modelnumber
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="mfgMdlNr"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "getManufactureAndModelNumber", CallingConvention = convention)]
        internal static extern bool GetManufactureAndModelNumber(int printerNo, ref int mfgMdlNr);//1.5.4.6

        /// <summary>
        /// The helper function GetMediaLoadStatus will look up the bits for the Media-Load-Status inside the PrinterStatus_t struct.
        /// </summary>
        /// <param name="printerStatus">pass in a pointer to the PrinterStatus_t which you need to read before calling this function.</param>
        /// <returns>the MediaLoadStatus_t</returns>
       
        
        [DllImport(DLL, EntryPoint = "GetMediaLoadStatus", CallingConvention = convention)]
         internal static extern MEDIA_LOAD_STATUS GetMediaLoadStatus(ref PrinterStatus_t t_printerStatus);//1.5.4.7

        /// <summary>
        /// GetMediaLoadStatusString converts the enum MediaLoadStatus_t numerical to text representation.
        /// </summary>
        /// <param name="mediaLoadStatus">pass in the enum value.</param>
        /// <returns>returns a zero terminated string (text representation of the of the value.)</returns>

        [DllImport(DLL, EntryPoint = "GetMediaLoadStatusString", CallingConvention = convention)]
        internal static extern IntPtr GetMediaLoadStatusString(MEDIA_LOAD_STATUS mediaLoadStatus);//1.5.4.9

        /// <summary>
        /// Get the current sheetfeeder roller depth
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="depth"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "getPickRollerDepth", CallingConvention = convention)] 
        internal static extern string GetPickRollerDepth(int printerNo, ref int depth);//1.5.4.10



        [DllImport(DLL, EntryPoint = "getPickRollerReverseMove", CallingConvention = convention)] //not in the header
        internal static extern string GetPickRollerReverseMove(int printerNo, ref bool status);//1.5.4.11


        /// <summary>
        /// Get the ink Level of C,K,M,
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "getPrinterInkLevel", CallingConvention = convention)]
        internal static extern bool GetPrinterInkLevel(int printerNo, ref InkLevel_t level);//1.5.4.12


        [DllImport(DLL, EntryPoint = "getPrintSpitCountK", CallingConvention = convention)]
        internal static extern bool GetPrintSpitCountK(int PrinterNo, ref int printDots, ref int spitDots);//1.5.4.13
        [DllImport(DLL, EntryPoint = "getPrintSpitCountC", CallingConvention = convention)]
        internal static extern bool GetPrintSpitCountC(int PrinterNo, ref int printDots, ref int spitDots);//1.5.4.14
        [DllImport(DLL, EntryPoint = "getPrintSpitCountM", CallingConvention = convention)]
        internal static extern bool GetPrintSpitCountM(int PrinterNo, ref int printDots, ref int spitDots);//1.5.4.15
        [DllImport(DLL, EntryPoint = "getPrintSpitCountY", CallingConvention = convention)]
        internal static extern bool GetPrintSpitCountY(int PrinterNo, ref int printDots, ref int spitDots);//1.5.4.16

        /// <summary>
        /// additionalTorque valid range[0..31]
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="additionalTorque"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "getServiceStationCapAdditionalTorque", CallingConvention = convention)]
        internal static extern string GetServiceStationCapAdditionalTorque(int printerNo, ref int additionalTorque);//1.5.4.17



        /// <summary>
        /// Get position of the carriage to detect media top edge with orange LED
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="pos_inch"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "getVerticalEdgeDetectionCarriagePosition", CallingConvention = convention)]
        internal static extern string getVerticalEdgeDetectionCarriagePosition(int printerNo, ref int pos_inch);//1.5.4.18


        /// <summary>
        /// get the current activated option to wait for media to be inserted manually
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="wait"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "getWaitForFrontMediaLoop", CallingConvention = convention)]
        internal static extern bool GetWaitForFrontMediaLoop(int printerNo, ref bool wait);//1.5.4.19


        /// <summary>
        ///  Gets the amount of waste ink inside the waste ink container. It is an estimated value and might not be that exact.
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns>a number between 0 and 100 which represents the waste ink containers current liquid level in percen</returns>
        [DllImport(DLL, EntryPoint = "getWasteInkLevel", CallingConvention = convention)]

        internal static extern int GetWasteInkLevel(ref PrinterStatus_t t_printerStatus);//1.5.4.20

        

        [DllImport(DLL, EntryPoint = "isAuthorized", CallingConvention = convention)]
        internal static extern bool IsAuthorized(ref PrinterStatus_t printerStatus);//1.5.4.21

        /// <summary>
        /// isAux1MotorStalled tells you if there is a motor stall for auxillary motor 1
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "isAux1MotorStalled", CallingConvention = convention)]
        internal static extern bool IsAux1MotorStalled(ref PrinterStatus_t t_printerStatus);//1.5.4.22
        /// <summary>
        /// tells you if there is a motor stall for auxillary motor 2
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isAux2MotorStalled", CallingConvention = convention)]
        internal static extern bool IsAux2MotorStalled(ref PrinterStatus_t t_printerStatus);//1.5.4.23


        /// <summary>
        /// tells you wether the LED light of the camera is on or not. You should switch it off if not needed to extend the life time of the LED.
                /// </summary>
                /// <param name="printerStatus"></param>
                /// <returns></returns>
        [DllImport(DLL, EntryPoint = "isCameraLedOn", CallingConvention = convention)]
        internal static extern bool IsCameraLedOn(ref PrinterStatus_t t_printerStatus);//1.5.4.24

        /// <summary>
        /// tells you if a Camera is connected at the front camera connector
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "isFrontCamConnected", CallingConvention = convention)]
        internal static extern bool IsFrontCamConnected(ref PrinterStatus_t t_printerStatus);//1.5.4.25

        /// <summary>
        /// tells you if the front camera is ready
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isFrontCamReady", CallingConvention = convention)]
        internal static extern bool IsFrontCamReady(ref PrinterStatus_t t_printerStatus);//1.5.4.26

        [DllImport(DLL, EntryPoint = "isLowInkWarning", CallingConvention = convention)]
        internal static extern bool IsLowInkWarning(OEMValues_t oemValues);//1.5.4.27

        [DllImport(DLL, EntryPoint = "isLowInkWarning_C", CallingConvention = convention)]
        internal static extern bool IsLowInkWarning_C(OEMValues_t oemValues);//1.5.4.28

        [DllImport(DLL, EntryPoint = "isLowInkWarning_K", CallingConvention = convention)]
        internal static extern bool IsLowInkWarning_K(OEMValues_t oemValues);//1.5.4.29
        [DllImport(DLL, EntryPoint = "isLowInkWarning_M", CallingConvention = convention)]
        internal static extern bool IsLowInkWarning_M(OEMValues_t oemValues);//1.5.4.30
        
        [DllImport(DLL, EntryPoint = "isLowInkWarning_Y", CallingConvention = convention)]
        internal static extern bool IsLowInkWarning_Y(OEMValues_t oemValues);//1.5.4.31




        [DllImport(DLL, EntryPoint = "isNonApproved_C", CallingConvention = convention)]
        internal static extern bool IsNonApproved_C(ref PrinterStatus_t t_printerStatus);//1.5.4.32


        [DllImport(DLL, EntryPoint = "isNonApproved_K", CallingConvention = convention)]
        internal static extern bool IsNonApproved_K(ref PrinterStatus_t t_printerStatus);//1.5.4.33


        [DllImport(DLL, EntryPoint = "isNonApproved_M", CallingConvention = convention)]
        internal static extern bool IsNonApproved_M(ref PrinterStatus_t t_printerStatus);//1.5.4.34


        [DllImport(DLL, EntryPoint = "isNonApproved_Y", CallingConvention = convention)]
        internal static extern bool IsNonApproved_Y(ref PrinterStatus_t t_printerStatus);//1.5.4.35

        /// <summary>
        /// will let you know if one of the ink cartridges is a non approved ink
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "isNonApprovedInkUsed", CallingConvention = convention)]
        internal static extern bool IsNonApprovedInkUsed(ref PrinterStatus_t t_printerStatus);//1.5.4.36

        /// <summary>
        /// the helper function isPaperJammed tells you if there is a paperJam inside the printer. if the printer tries to move the paper and the paper does not reach the expected sensor by moving the paper rollers it thinks that the paper
        ///got jammed inside the printer.it could also happen that the paper slipps and therefore does not reach the expected
        //position.The user needs to open up the printer and check for the real condition.
                /// </summary>
                /// <param name="printerStatus"></param>
                /// <returns></returns>
        [DllImport(DLL, EntryPoint = "isPaperJammed", CallingConvention = convention)]
        internal static extern bool IsPaperJammed(ref PrinterStatus_t t_printerStatus);//1.5.4.37


        /// <summary>
        /// the helper function isPrintheadCapped tells you wether the printhead is capped (parked inside the service station) or not.
                /// </summary>
                /// <param name="printerStatus"></param>
                /// <returns></returns>
        [DllImport(DLL, EntryPoint = "isPrintheadCapped", CallingConvention = convention)]
        internal static extern bool IsPrintheadCapped(ref PrinterStatus_t t_printerStatus);//1.5.4.38

        /// <summary>
        /// tells you if a Camera is connected at the rear camera connector
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isRearCamConnected", CallingConvention = convention)]
        internal static extern bool IsRearCamConnected(ref PrinterStatus_t t_printerStatus);//1.5.4.39

        /// <summary>
        /// tells you if the rear camera is read
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "isRearCamReady", CallingConvention = convention)]
        internal static extern bool IsRearCamReady(ref PrinterStatus_t t_printerStatus);//1.5.4.40
        /// <summary>
        /// tells you if the rear cover is open or not. (has nothing to do with the status of the lock-mechanism)
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isRearDoorOpen", CallingConvention = convention)]
        internal static extern bool IsRearDoorOpen(ref PrinterStatus_t t_printerStatus);//1.5.4.41
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isRefilled_C", CallingConvention = convention)]
        internal static extern bool IsRefilled_C(ref PrinterStatus_t t_printerStatus);//1.5.4.42

        [DllImport(DLL, EntryPoint = "isRefilled_K", CallingConvention = convention)]
        internal static extern bool IsRefilled_K(ref PrinterStatus_t t_printerStatus);//1.5.4.43

        [DllImport(DLL, EntryPoint = "isRefilled_M", CallingConvention = convention)]
        internal static extern bool IsRefilled_M(ref PrinterStatus_t t_printerStatus);//1.5.4.44

        [DllImport(DLL, EntryPoint = "isRefilled_Y", CallingConvention = convention)]
        internal static extern bool IsRefilled_Y(ref PrinterStatus_t t_printerStatus);//1.5.4.45

        [DllImport(DLL, EntryPoint = "isRefilledInkUsed", CallingConvention = convention)]
        internal static extern bool IsRefilledInkUsed(ref PrinterStatus_t t_printerStatus);//1.5.4.46
        /// <summary>
        /// tells you if the printer is in the waiting for Front media loop. While the printer is waiting in that loop you should not call a printer command.Querrying for status updates is still fine in that situation.
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isWaitingForFrontMediaInsert", CallingConvention = convention)]
        internal static extern bool IsWaitingForFrontMediaInsert(ref PrinterStatus_t t_printerStatus);//1.5.4.47

        /// <summary>
        /// isWasteInkContainerMissing lets you know if the wast ink container is inserted properly
        /// </summary>
        /// <param name="printerStatus"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isWasteInkContainerMissing", CallingConvention = convention)]
        internal static extern bool IsWasteInkContainerMissing(ref PrinterStatus_t t_printerStatus);//1.5.4.48

        /// <summary>
        /// the helper function isWasteInkFullWarning let's you know if the waste ink container reached a level where the printer thinks you should exchange the waste ink pad.
                /// </summary>
                /// <param name="printerStatus"></param>
                /// <returns></returns>

        [DllImport(DLL, EntryPoint = "isWasteInkFullWarning", CallingConvention = convention)]
        internal static extern bool IsWasteInkFullWarning(ref PrinterStatus_t t_printerStatus);//1.5.4.49

        /// <summary>
        /// loadMediaToFrontCameraPosition loads the media to front camera.
        /// </summary>
        /// <param name="printerNo"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "loadMediaToFrontCameraPosition", CallingConvention = convention)]
        public static extern bool LoadMediaToFrontCameraPosition(int printerNo);//1.5.4.50

        /// <summary>
        /// loadMediaToPrintPosition
        /// </summary>
        /// <param name="printerNo"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "loadMediaToPrintPosition", CallingConvention = convention)]
        internal static extern bool LoadMediaToPrintPosition(int printerNo);//1.5.4.51

        /// <summary>
        /// paperLoadTest test the paper loading
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="loadPosition">1=rear 4=front same numbers as pcl tray number</param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "paperLoadTest", CallingConvention = convention)]
        internal static extern bool PaperLoadTest(int printerNo, LOAD_POSITION loadPosition);//1.5.4.52

        /// <summary>
        /// setExternalFactoryDefaults sets the factory defaults settings
        /// </summary>
        /// <param name="printerNo"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "setExternalFactoryDefaults", CallingConvention = convention)]
        internal static extern bool SetExternalFactoryDefaults(int printerNo);//1.5.4.53

        //[DllImport(DLL, EntryPoint = "setMediaEdgeCorrection", CallingConvention = convention)]
        //internal static extern bool SetMediaEdgeCorrection(int printerNo, bool left, int value);//1.5.4.43


        /// <summary>
        ///  set the sheetfeeder roller depth for different paper types
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        [DllImport(DLL, EntryPoint = "setPickRollerDepth", CallingConvention = convention)]
        internal static extern bool SetPickRollerDepth(int printerNo, int depth);//1.5.4.54
      

        [DllImport(DLL, EntryPoint = "setPickRollerReverseMove", CallingConvention = convention)]
        internal static extern bool SetPickRollerReverseMove(int printerNo, bool status);//1.5.4.55


        [DllImport(DLL, EntryPoint = "setServiceStationCapAdditionalTorque", CallingConvention = convention)]
        internal static extern bool SetServiceStationCapAdditionalTorque(int PrinterNo, int additionalTorque);//1.5.4.56


        /// <summary>
        /// set position of the carriage to detect media top edge with orange LED
        /// </summary>
        /// <param name="PrinterNo"></param>
        /// <param name="pos_inch"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "setVerticalEdgeDetectionCarriagePosition", CallingConvention = convention)]
        internal static extern bool SetVerticalEdgeDetectionCarriagePosition(int PrinterNo, int pos_inch);//1.5.4.57


        /// <summary>
        /// enables/disables option to wait for media to be inserted manually
        /// </summary>
        /// <param name="printerNo"></param>
        /// <param name="wait"></param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "setWaitForFrontMediaLoop", CallingConvention = convention)]
        internal static extern bool SetWaitForFrontMediaLoop(int printerNo, bool wait);//1.5.4.58



        /// <summary>

        //switches the Camera LED on/off for the printer given by PrinterNo.

        /// </summary>
        /// <param name="printerNo">the printer for which you want to switch on/off the camera's LED. [0..noOfPrinters-1] where noOfPrinters is the returnvalue of InitPrinterList.</param>
        /// <param name="ledOn">true if you want to switch the LED on; otherwise false.</param>
        /// <returns></returns>

        [DllImport(DLL, EntryPoint = "SwitchCameraLED", CallingConvention = convention)]
        internal static extern bool SwitchCameraLED(int printerNo, bool ledOn);//1.5.4.48
    }

}
