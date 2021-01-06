using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    [StructLayout(LayoutKind.Sequential, Size = Size, Pack = 1)]
    public struct AmsNetId
    {
        public const int Size = 6;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Size, ArraySubType = UnmanagedType.U1)]
        public byte[] AmsAddress;

        public static bool operator ==(AmsNetId lhs, AmsNetId rhs)
        {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(AmsNetId lhs, AmsNetId rhs)
        {
            return !lhs.Equals(rhs);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj is null or not AmsNetId) return false;
            return AmsAddress.SequenceEqual(((AmsNetId)obj).AmsAddress);
        }
        public override string ToString()
        {
            if (AmsAddress is null || AmsAddress.Length != Size)
                return string.Empty;
            return string.Join('.', AmsAddress);

        }
    }
}
