using System;
using System.IO;
using System.Text;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{

    /// <summary>
    /// Represents header of a .cap file.
    /// </summary>
    /// <exception cref="FormatException"  
    /// <exception cref="ArgumentNullException" />
    public sealed class NetMonHeader
    {


        public const string NETMON_2_X_MAGIC = "GMBU";
        public static readonly UInt32 NETMON_2_X_MAGIC_UI32 = BitConverter.ToUInt32(Encoding.UTF8.GetBytes(NETMON_2_X_MAGIC));
        public const int MAGIC_SIZE = 4;
        public const int HEADER_SIZE = 128;
        public const byte VALID_MAJOR = 2;

        private NetMonHeader(HeaderStruct? hdr)
        {
            Header = hdr ?? throw new ArgumentNullException(nameof(hdr));
            SanityChecks();
        }

        public HeaderStruct Header { get; private set; }


        internal static NetMonHeader DeserializeHeader(BinaryReader binaryReader)
        {
            var header = Helper.Helper.DeserializeStruct<HeaderStruct>(binaryReader);
            return new(header);
        }


        private void SanityChecks()
        {
            // check for MagicNumber
            if (Header.magic != NETMON_2_X_MAGIC_UI32) throw new FormatException("Is not a valid NetMon 2.x file format.");
            // check version major
            if (Header.ver_major != VALID_MAJOR) throw new FormatException($"NetMon file version \"{Header.ver_major}.x\" is not supported.");
        }

    }
}
