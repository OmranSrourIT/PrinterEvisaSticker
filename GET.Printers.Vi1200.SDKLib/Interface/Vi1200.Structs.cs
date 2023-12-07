using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GET.Printers.Vi1200
{
     struct InkLevel_t
    {
        public char C;
        public char M;
        public char Y;
        public char K;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterStatus_t
    {

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I4)]

        internal int[] Ro;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]

        internal int[] Rw;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]

        internal int[] Nvm;
        internal int machineState;
        internal int TopMargin;
        internal int LeftMargin;

        internal int MachineState;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]

        public char[] BaseFirmwareVersion;
    };

    [StructLayout(LayoutKind.Sequential)]

    internal struct OEMValues_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I4)]

        internal int[] Ro;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]

        internal int[] Rw;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]

        internal int[] Nvm;
    };
    [StructLayout(LayoutKind.Sequential)]

    internal struct ProductUsage_t
    {
        internal int PageCount;
        internal int DropCountCyanPrint;
        internal int DropCountCyanService;
        internal int DropCountMagentaPrint;
        internal int DropCountMagentaService;
        internal int DropCountYellowPrint;
        internal int DropCountYellowService;
        internal int DropCountBlackPrint;
        internal int DropCountBlackService;
    };
}
