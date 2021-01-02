using System;
using System.IO;
using System.Text;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsReadDeviceInfoRequest : IPayload
    {

        /// <summary>
        /// ADS Read Device Info - Request<br/>
        /// No additional data required - no payload
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsReadDeviceInfoRequest(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if(!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length != EXPECTED_DATA_LEN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);
        }

        public const uint EXPECTED_DATA_LEN = 0;

        private byte[] _PacketData = new byte[EXPECTED_DATA_LEN];
        public ReadOnlyMemory<byte> PacketData => _PacketData;
        public override string ToString()
        {
            return $"{nameof(AdsReadDeviceInfoRequest)} has no payload.";
        }
    }
}
