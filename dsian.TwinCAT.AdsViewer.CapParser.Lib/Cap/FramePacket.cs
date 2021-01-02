using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    public sealed record FramePacket(FrameHeader Header, PacketData Data)
    {
        public int Index { get; init; }
    }
}
