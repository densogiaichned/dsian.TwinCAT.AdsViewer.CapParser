using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    /// <summary>
    /// AMS Header without Data section<br/>
    /// see also <a href="https://infosys.beckhoff.com/index.php?content=../content/1033/tcadsamsspec/html/tcadsamsspec_amstcppackage.htm">TwinCAT ADS/AMS - Specification</a>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 1)]
    public struct AmsHeader
    { 
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.U1)]
        public AmsNetId AMSNetId_Target;        
        public UInt16 AMSPort_Target;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.U1)]
        public AmsNetId AMSNetId_Source;
        public UInt16 AMSPort_Source;
        [MarshalAs(UnmanagedType.U2, SizeConst = 2)]
        public AmsCommandId AmsCommandId;
        [MarshalAs(UnmanagedType.U2, SizeConst = 2)]
        public AmsStateFlags AmsStateFlags;
        public UInt32 Data_Length;
        public UInt32 Error_Code;
        public UInt32 Invoke_Id;

        public bool IsValid => AmsCommandId > 0 && AmsCommandId <= AmsCommandId.ADS_Read_Write;
        public bool IsResponse => (AmsStateFlags & AmsStateFlags.Response) != 0;
        public bool IsRequest => (AmsStateFlags & AmsStateFlags.Response) == 0;
    }
}
