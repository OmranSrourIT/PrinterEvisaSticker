using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET.Printers.Vi1200
{
    public enum CONNECTION_TYPE
    {
        ANY,
        USB,
        NETWORK,
    }
    public enum MACHINE_STATE
    {
        UNKNOWN_INVALID,
        INITIALIZING,
        OFF,
        MFG_OFF,
        GOING_ON,
        IDS_STARTUP,
        IDLE,
        PRINTING,
        REPORTS_PRINTING,
        CANCELLING_PRINTING,
        SI_1300_STALL,
        DRY_TIME_WAIT,
        PEN_CHANGE,
        OUT_OF_PAPER,
        BANNER_MISMATCH,
        PHOTO_MISMATCH,
        DUPLEX_MISMATCH,
        MEDIA_TOO_NARROW,
        MEDIA_UPSIDE_DOWN,
        MEDIA_JAM,
        CARRIAGE_MOTOR_STALL,
        PAPER_MOTOR_STALL,
        SERVICE_MOTOR_STALL,
        PICK_MOTOR_STALL,
        PUMP_MOTOR_STALL,
        MOTOR_STALL,
        PEN_FAILURE,
        INK_SUPPLY_FAILURE,
        HARD_ERROR,
        IDS_HW_FAILURE,
        POWERING_DOWN,
        FP_TEST,
        OUTPUT_TRAY_CLOSED,
        DUPLEXER_MISSING,
        DUPLEXER_INVALID,
        OUT_OF_INK,
        MEDIA_SIZE_MISMATCH,
        MEDIA_TOO_WIDE,
        MEDIA_WRONG,
        MEDIA_TYPE_WRONG,
        DOOR_OPEN,
        PEN_NOT_LATCHED,
        INK_SUPPLY_CHANGE,
        GENERIC_ERROR,
        IDS_STARTUP_BLOCKED_LOI,
        VERY_LOW_ON_INK,
        ATTENTION_NEEDED,
    }

    public enum ERROR_CODE
    {
        NO_ERROR = 0,
        NUMBER,
    }
    public enum CAMERA_POSITION
    {
        UNKNOWN,
        NO_CAM,
        FRONT,
        REAR,
    }

    public enum LOAD_POSITION
    {
        REAR = 1,
        FRONT = 4
    }
    public enum MEDIA_LOAD_STATUS
    {
        NO_MEDIA_LOADED,
        FRONT_MEDIA_AT_CAMERA,
        MEDIA_AT_PRINT,
        GRABBED_AT_FRONT,
    }
    public enum ENGINE_STATE
    {
        READY,
        AUTHORIZATION_FAILED,
        WASTE_INK_CONTAINER_MISSING,
        NON_APPROVED_INK,
        REFILLED_INK,
        MEDIA_PRE_LOADED,
        WAITING_FOR_FRONT_MEDIA,
        UNAUTHORIZED_PRINTER_PCB,
        REAR_DOOR_OPEN,
    }

    public enum PPS_LEVEL
    {
        AUTO = 0,
        LOW = 1,
        MEDIUM = 2,
        HIGH = 3,
    }
    public enum ORDER_STAGE
    {
        INITIALIZING,
        CONFIGURED,
        ACCEPTED,
        CANCELLED,
        SUCCEEDED,
        FAILED,
        REJECTED,

        //CARD_REVERSING,
        //CARD_REVERSED,
        //PRINTING_FRONT,
        //FRONT_PRINTED,
        //PRINTING_BACK,
        //BACK_PRINTED,
        //MOVING_TO_CONTACTLESS,
        //MOVING_TO_CONTACT,
        //MOVING_TO_CAMERA,
        //CONTACTLESS_REACHED,
        //CONTACT_REACHED,
        //CAMERA_REACHED,
        //EJECTING,
        //EJECTED,
    }

    public enum PAPER_POSITION
    {
        NONE,
        CAMERA,
        PRINT,
      
       
    }

    public enum SOURCE_TRAY
    {
        PRINTER_DRIVER_SELECT,
        REAR_SHEET_FEEDER,
        FRONT_MANUAL_TRAY,
    }
}
