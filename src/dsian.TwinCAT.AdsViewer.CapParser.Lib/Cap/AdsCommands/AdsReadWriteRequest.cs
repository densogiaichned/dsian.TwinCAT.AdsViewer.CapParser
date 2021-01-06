using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsReadWriteRequest : IPayload
    {
        /// <summary>
        /// ADS Read Write - Request<br/>
        /// With ADS Read Write data will be written to an ADS device. Additionally, data can be read from the ADS device.<br/>
        /// The data which can be read are addressed by the Index Group and the Index Offset.
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsReadWriteRequest(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length < EXPECTED_DATA_LEN_MIN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }


        public const int EXPECTED_DATA_LEN_MIN = 16;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;

        /// <summary>
        /// Index Group, in which the data should be written.
        /// </summary>
        public UInt32 IndexGroup { get; private set; }
        /// <summary>
        /// Index Offset, in which the data should be written
        /// </summary>
        public UInt32 IndexOffset { get; private set; }
        /// <summary>
        /// Length of data in bytes, which should be read.
        /// </summary>
        public UInt32 ReadLength { get; private set; }
        /// <summary>
        /// Length of data in bytes, which should be written
        /// </summary>
        public UInt32 WriteLength { get; private set; }
        /// <summary>
        /// Data which are written in the ADS device.
        /// </summary>
        public ReadOnlyMemory<byte> Data => _PacketData[EXPECTED_DATA_LEN_MIN..];

        public override string ToString()
        {

            if (WriteLength > 0)
            {
                var max = (int)Math.Min(WriteLength, AdsCommandFactory.MAX_DATA_PRNT);
                var dotdotdot = WriteLength > AdsCommandFactory.MAX_DATA_PRNT ? "..." : string.Empty;
                return $"{nameof(AdsReadWriteRequest)}: IG=0x{IndexGroup:X8}, IO=0x{IndexOffset:X8}, RdLen={ReadLength}, WrLen={WriteLength}, Data={BitConverter.ToString(_PacketData, EXPECTED_DATA_LEN_MIN, max)}{dotdotdot}";
            }
            else
                return $"{nameof(AdsReadWriteRequest)}: IG=0x{IndexGroup:X8}, IO=0x{IndexOffset:X8}, RdLen={ReadLength}, WrLen={WriteLength}";
        }
        private void ParsePacketData()
        {
            IndexGroup = BitConverter.ToUInt32(_PacketData, 0);
            IndexOffset = BitConverter.ToUInt32(_PacketData, 4);
            ReadLength = BitConverter.ToUInt32(_PacketData, 8);
            WriteLength = BitConverter.ToUInt32(_PacketData, 12);
        }
    }
}
