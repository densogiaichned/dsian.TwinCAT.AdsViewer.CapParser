using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    internal class AdsDeleteDeviceNotificationRequest : IPayload
    {
        /// <summary>
        /// ADS Write - Request<br/>
        /// With ADS Write data can be written to an ADS device. The data are addressed by the Index Group and the Index Offset 
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsDeleteDeviceNotificationRequest(AmsHeader amsHeader, BinaryReader binaryReader)
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
        /// Handle of notification. The handle is created by the ADS command Add Device Notification  
        /// </summary>
        public UInt32 NotificationHandle { get; private set; }


        public override string ToString()
        {
            return $"{nameof(AdsDeleteDeviceNotificationRequest)}: NotificationHandle={NotificationHandle}";
        }
        private void ParsePacketData()
        {
            NotificationHandle = BitConverter.ToUInt32(_PacketData, 0);
        }
    }
}
