using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsAddDeviceNotificationRequest : IPayload
    {
        /// <summary>
        /// ADS Write - Request<br/>
        /// With ADS Write data can be written to an ADS device. The data are addressed by the Index Group and the Index Offset 
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsAddDeviceNotificationRequest(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length != EXPECTED_DATA_LEN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }


        public const int EXPECTED_DATA_LEN = 40;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;

        /// <summary>
        /// Index Group of the data, which should be sent per notification.
        /// </summary>
        public UInt32 IndexGroup { get; private set; }
        /// <summary>
        /// Index Offset of the data, which should be sent per notification.
        /// </summary>
        public UInt32 IndexOffset { get; private set; }
        /// <summary>
        /// Length of data in bytes, which should be sent per notification.
        /// </summary>
        public int Length { get; private set; }
        /// <summary>
        /// See description of the structure ADSTRANSMODE at the ADS-DLL.
        /// </summary>
        public AdsTransMode TransmissonMode { get; private set; }
        /// <summary>
        /// At the latest after this time, the ADS Device Notification is called. The unit is 1ms.
        /// </summary>
        public int MaxDelay { get; private set; }
        /// <summary>
        /// The ADS server checks if the value changes in this time slice. The unit is 1ms
        /// </summary>
        public int CycleTime { get; private set; }
        /// <summary>
        /// Must be set to 0 
        /// </summary>
        public ReadOnlyMemory<byte> reserved => _PacketData.AsMemory()[24..];

        public override string ToString()
        {
            return $"{nameof(AdsAddDeviceNotificationRequest)}: IG=0x{IndexGroup:X8}, IO=0x{IndexOffset:X8}, Len={Length}, TransmissionMode={TransmissonMode}, MaxDelay={MaxDelay}, CycleTime={CycleTime}";
        }
        private void ParsePacketData()
        {
            IndexGroup = BitConverter.ToUInt32(_PacketData, 0);
            IndexOffset = BitConverter.ToUInt32(_PacketData, 4);
            Length = BitConverter.ToInt32(_PacketData, 8);
            TransmissonMode = (AdsTransMode)BitConverter.ToInt32(_PacketData, 12);
            MaxDelay = BitConverter.ToInt32(_PacketData, 16);
            CycleTime = BitConverter.ToInt32(_PacketData, 20);
        }
    }
}