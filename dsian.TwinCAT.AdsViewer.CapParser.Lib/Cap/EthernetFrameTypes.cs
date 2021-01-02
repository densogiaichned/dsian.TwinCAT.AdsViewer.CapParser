using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    public enum EthernetFrameTypes : UInt16
    {
        NotAllowed,
        EtherCAT = 0xA488,        
    }
}
