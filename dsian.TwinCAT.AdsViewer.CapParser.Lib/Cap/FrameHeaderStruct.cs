using System;
using System.Runtime.InteropServices;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    [StructLayout(LayoutKind.Explicit, Size=FrameHeader.HEADER_SIZE)]
    public struct FrameHeaderStruct
    {
        /// <summary>
        /// time stamp - usecs since start of capture
        /// </summary>
        [FieldOffset(0)] public Int64 ts_delta;
        /// <summary>
        /// actual length of packet
        /// </summary> 
        [FieldOffset(8)] public Int32 orig_len;
        /// <summary>
        /// number of octets captured in file 
        /// </summary>
        [FieldOffset(12)] public Int32 incl_len;
    }
}
