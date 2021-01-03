using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    internal class AdsWriteResponse : IPayload
    {
        /// <summary>
        /// ADS Write - Response<br/>
        /// With ADS Write data can be written to an ADS device. The data are addressed by the Index Group and the Index Offset 
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsWriteResponse(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length != EXPECTED_DATA_LEN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }


        public const int EXPECTED_DATA_LEN = 4;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;


        /// <summary>
        /// ADS error number.
        /// </summary>
        public AdsErrorCode Result { get; private set; }

        public override string ToString()
        {
            return $"{nameof(AdsWriteResponse)}: Res={Result}";
        }
        private void ParsePacketData()
        {
            Result = (AdsErrorCode)BitConverter.ToUInt32(_PacketData, 0);
        }
    }
}
