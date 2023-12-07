using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET.Printers.Vi1200
{
    public class ViInkLevel
    {
        public int Cayan { get; internal set; }
        public int Black { get; internal set; }
        public int Magenta { get; internal set; }
        public int Yellow { get; internal set; }
    }
    public class Vi1200Status
    {
        public bool IsAuthorized { get; internal set; }
        public ENGINE_STATE EngineState { get; internal set; }
        public MACHINE_STATE MachineState { get; internal set; }
        public bool IsWasteInkContainerMissing { get; internal set; }
        public bool IsWasteInkFullWarning { get; internal set; }
        public bool IsCameraLedOn { get; internal set; }
        public bool IsFrontCameraConnected { get; internal set; }
        public bool IsRearCameraConnected { get; internal set; }
        public bool IsFrontCameraReady { get; internal set; }
        public bool IsRearCameraReady { get; internal set; }
        public bool IsWaitingForFrontMediaInsert { get; internal set; }
       public bool IsAux1MotorStalled { get; internal set; }
        public bool IsAux2MotorStalled { get; internal set; }
        public bool IsPaperJammed { get; set; }
        public bool IsPrintheadCapped { get; set; }
        public MEDIA_LOAD_STATUS MediaLoadStatus { get; set; }
        public bool IsNonApprovedInkUsed { get; set; }
        public bool IsNonApproved_C { get; set; }
        public bool IsNonApproved_K { get; set; }
        public bool IsNonApproved_M { get; set; }
        public bool IsNonApproved_Y { get; set; }
        public bool IsRefilledInkUsed { get; set; }
        public bool IsRefilled_C { get; set; }
        public bool IsRefilled_K { get; set; }
        public bool IsRefilled_M { get; internal set; }
        public bool IsRefilled_Y { get; internal set; }
        public int WasteInkLevel { get; internal set; }
        public bool IsMediaIn { get; internal set; }

        public ViInkLevel InkLevels { get; internal set; } = new ViInkLevel();

        public bool IsLowInkWarning { get; internal set; }
        public bool IsLowInkWarning_C { get; internal set; }
        public bool IsLowInkWarning_K { get; internal set; }
        public bool IsLowInkWarning_M { get; internal set; }
        public bool IsLowInkWarning_Y { get; internal set; }

        public int TotalPrintCount { get; internal set; }
    }
}
