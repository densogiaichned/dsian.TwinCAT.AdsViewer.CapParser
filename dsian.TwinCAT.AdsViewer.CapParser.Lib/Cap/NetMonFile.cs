using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    /// <summary>
    /// Represents a NetMon V2.x file (*.cap)
    /// </summary>
    public sealed record NetMonFile(NetMonHeader NetMonHeader, IEnumerable<FramePacket> FramePackets, IEnumerable<UInt32> FrameTable);
}
