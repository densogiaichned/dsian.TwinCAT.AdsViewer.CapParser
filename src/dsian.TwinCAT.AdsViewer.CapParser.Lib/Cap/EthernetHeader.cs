using System;
using System.Runtime.InteropServices;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{


    [StructLayout(LayoutKind.Explicit, Size = 14)]
    public struct EthernetHeader
    {
        [FieldOffset(0)]
        public UInt32 destination_addr;   /* IP4 Destination*/
        [FieldOffset(4)]
        public UInt16 destination_port;
        [FieldOffset(6)]
        public UInt32 source;        /* IP4 Source */
        [FieldOffset(10)]
        public UInt16 source_port;
        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(12)]
        public EthernetFrameTypes type;
    }
}
