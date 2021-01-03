using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds
{
    [Flags]
    public enum AmsStateFlags: UInt16
    {
        Request = 0x0,
        Response = 0x1,
        ADS_Command  =0x4,
        /// <summary>
        /// Bit number 7 marks, if it should be transfered with TCP=0 or UDP=1.
        /// </summary>
        UDP_Protocol = 0x0040,
    }
}
