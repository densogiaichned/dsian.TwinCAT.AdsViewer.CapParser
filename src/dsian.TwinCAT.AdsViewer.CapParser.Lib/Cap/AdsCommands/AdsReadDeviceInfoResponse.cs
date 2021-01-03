using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;
using System.Text;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsReadDeviceInfoResponse : IPayload
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsReadDeviceInfoResponse(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length != EXPECTED_DATA_LEN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }

        public const uint EXPECTED_DATA_LEN = 24;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;

        /// <summary>
        /// ADS error number.
        /// </summary>
        public AdsErrorCode Result { get; private set; }
        /// <summary>
        /// Major version number
        /// </summary>
        public byte Major_Version { get; private set; }
        /// <summary>
        /// Minor version number
        /// </summary>
        public byte Minor_Version { get; private set; }
        /// <summary>
        /// Build number
        /// </summary>
        public UInt16 Version_Build { get; private set; }
        /// <summary>
        /// Name of ADS device
        /// </summary>
        public string Device_Name { get; private set; } = string.Empty;


        private void ParsePacketData()
        {
            Result = (AdsErrorCode)BitConverter.ToUInt32(_PacketData, 0);
            Major_Version = _PacketData[4];
            Minor_Version = _PacketData[5];
            Version_Build = BitConverter.ToUInt16(_PacketData, 6);
            Device_Name = Encoding.UTF8.GetString(_PacketData, 8, 16);
        }
        public override string ToString()
        {
            if (Result == AdsErrorCode.NoError)
                return $"{nameof(AdsReadDeviceInfoResponse)}: Res={Result}, Major={Major_Version}, Minor={Minor_Version}, Build={Version_Build}, DeviceName={Device_Name}";
            else
                return $"{nameof(AdsReadDeviceInfoResponse)}: Res={Result}";
        }
    }
}
