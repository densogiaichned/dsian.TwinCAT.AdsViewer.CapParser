using System;
using System.Runtime.InteropServices;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    /// <summary>
    /// EtherCAT Header<br/>
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct EtherCATHeader
    {
        [FieldOffset(0)] private UInt16 EcFrameHeader;

        public int Length() => EcFrameHeader & 0x7FF;
        public bool IsADS => (EcFrameHeader & 0xF000) == 0x2000;
    }
}
