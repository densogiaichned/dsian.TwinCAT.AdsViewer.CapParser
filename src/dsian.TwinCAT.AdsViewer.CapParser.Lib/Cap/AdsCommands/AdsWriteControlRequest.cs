using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsWriteControlRequest : IPayload
    {
        /// <summary>
        /// ADS Write Control - Request<br/>
        /// Changes the ADS status and the device status of an ADS device. Additionally it is possible to send data to the ADS device to transfer further information.<br/>
        /// These data were not analysed from the current ADS devices (PLC, NC, ...)
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsWriteControlRequest(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length < EXPECTED_DATA_LEN_MIN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }


        public const int EXPECTED_DATA_LEN_MIN = 8;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;

        /// <summary>
        /// ADS status (see data type ADSSTATE of the ADS-DLL).
        /// </summary>
        public AdsState ADS_State { get; private set; }
        /// <summary>
        /// Device status
        /// </summary>
        public ushort Device_State { get; private set; }
        /// <summary>
        /// Length of data in byte.
        /// </summary>
        public UInt32 Length { get; private set; }
        /// <summary>
        /// Data which are written in the ADS device.
        /// </summary>
        public ReadOnlyMemory<byte> Data => _PacketData.AsMemory()[EXPECTED_DATA_LEN_MIN..];

        public override string ToString()
        {
            if (Length > 0)
            {
                var max = (int)Math.Min(Length, AdsCommandFactory.MAX_DATA_PRNT);
                var dotdotdot = Length > AdsCommandFactory.MAX_DATA_PRNT ? "..." : string.Empty;
                return $"{nameof(AdsWriteControlRequest)}: ADS State={ADS_State}, Device State={Device_State}, Len={Length}, Data={BitConverter.ToString(_PacketData, EXPECTED_DATA_LEN_MIN, max)}{dotdotdot}";
            }
            else
                return $"{nameof(AdsWriteControlRequest)}: ADS State={ADS_State}, Device State={Device_State}, Len={Length}";
        }
        private void ParsePacketData()
        {
            ADS_State = (AdsState)BitConverter.ToInt16(_PacketData, 0);
            Device_State = BitConverter.ToUInt16(_PacketData, 2);
            Length = BitConverter.ToUInt32(_PacketData, 4);
        }
    }
}
