using System;
using System.Runtime.InteropServices;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    [StructLayout(LayoutKind.Explicit, Size = NetMonHeader.HEADER_SIZE, CharSet = CharSet.Ansi)]
    public struct HeaderStruct
    {
        [FieldOffset(0)] public UInt32 magic;      /* Magic number in Network Monitor 2.x files. = GMBU*/
        [FieldOffset(4)] public byte ver_minor;   /* minor version number */
        [FieldOffset(5)] public byte ver_major;   /* major version number */
        [FieldOffset(6)] public UInt16 network;    /* network type */
        [FieldOffset(8)] public UInt16 ts_year;    /* year of capture start */
        [FieldOffset(10)] public UInt16 ts_month;   /* month of capture start (January = 1) */
        [FieldOffset(12)] public UInt16 ts_dow;     /* day of week of capture start (Sun = 0) */
        [FieldOffset(14)] public UInt16 ts_day;     /* day of month of capture start */
        [FieldOffset(16)] public UInt16 ts_hour;    /* hour of capture start */
        [FieldOffset(18)] public UInt16 ts_min;     /* minute of capture start */
        [FieldOffset(20)] public UInt16 ts_sec;     /* second of capture start */
        [FieldOffset(22)] public UInt16 ts_msec;    /* millisecond of capture start */
        [FieldOffset(24)] public UInt32 frametableoffset;   /* frame index table offset */
        [FieldOffset(28)] public UInt32 frametablelength;   /* frame index table size */
        [FieldOffset(32)] public UInt32 userdataoffset;     /* user data offset */
        [FieldOffset(36)] public UInt32 userdatalength;     /* user data size */
        [FieldOffset(40)] public UInt32 commentdataoffset;  /* comment data offset */
        [FieldOffset(44)] public UInt32 commentdatalength;  /* comment data size */
        [FieldOffset(48)] public UInt32 processinfooffset;  /* offset to process info structure */
        [FieldOffset(52)] public UInt32 processinfocount;   /* number of process info structures */
        [FieldOffset(56)] public UInt32 networkinfooffset;  /* offset to network info structure */
        [FieldOffset(60)] public UInt32 networkinfolength;  /* length of network info structure */
        [FieldOffset(64)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] reserve;
    }
}
