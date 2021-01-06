using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsReadRequest : IPayload
    {
        /// <summary>
        /// ADS Read - Request<br/>
        /// With ADS Read data can be read from an ADS device.  The data are addressed by the Index Group and the Index Offset
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsReadRequest(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length != EXPECTED_DATA_LEN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }


        public const uint EXPECTED_DATA_LEN = 12;

        private byte[] _PacketData = new byte[EXPECTED_DATA_LEN];
        public ReadOnlyMemory<byte> PacketData => _PacketData;


        public UInt32 IndexGroup { get; private set; }
        public UInt32 IndexOffset { get; private set; }
        public UInt32 Length { get; private set; }

        public override string ToString()
        {
            return $"{nameof(AdsReadRequest)}: IG=0x{IndexGroup:X8}, IO=0x{IndexOffset:X8}, Len={Length}";
        }
        private void ParsePacketData()
        {
            IndexGroup = BitConverter.ToUInt32(_PacketData, 0);
            IndexOffset = BitConverter.ToUInt32(_PacketData, 4);
            Length= BitConverter.ToUInt32(_PacketData, 8);
        }
    }
}
