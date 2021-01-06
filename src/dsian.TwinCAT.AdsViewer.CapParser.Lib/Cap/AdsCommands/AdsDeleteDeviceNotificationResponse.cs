using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsDeleteDeviceNotificationResponse : IPayload
    {
        /// <summary>
        /// ADS Write Control - Response<br/>
        /// Changes the ADS status and the device status of an ADS device. Additionally it is possible to send data to the ADS device to transfer further information.<br/>
        /// These data were not analysed from the current ADS devices (PLC, NC, ...)
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsDeleteDeviceNotificationResponse(AmsHeader amsHeader, BinaryReader binaryReader)
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
            return $"{nameof(AdsDeleteDeviceNotificationResponse)}: Res={Result}";
        }
        private void ParsePacketData()
        {
            Result = (AdsErrorCode)BitConverter.ToUInt32(_PacketData, 0);
        }
    }
}
