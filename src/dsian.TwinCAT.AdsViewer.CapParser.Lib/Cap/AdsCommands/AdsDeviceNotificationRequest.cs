using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    /// <summary>
    /// ADS Device Notification - Request<br/>
    /// </summary>
    /// <remarks>
    /// The data which are transfered at the Device Notification  are multiple nested into one another.<br/>
    /// The Notification Stream contains an array with elements of type AdsStampHeader.<br/>
    /// This array again contains elements of type AdsStampHeader.
    /// </remarks>
    public class AdsDeviceNotificationRequest : IPayload
    {

        /// <inheritdoc cref="AdsDeviceNotificationRequest"/>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        internal AdsDeviceNotificationRequest(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length < EXPECTED_DATA_LEN_MIN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }

        public const int EXPECTED_DATA_LEN_MIN = 8;

        /// <summary>
        /// Total size in Bytes of <see cref="AdsStampHeader"/>
        /// </summary>
        private int _TotalSizeStampHeaders = 0;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;

        /// <summary>
        /// Size of data in byte.
        /// </summary>
        public int Length { get; private set; }
        /// <summary>
        /// Number of elements of type <see cref="AdsStampHeader"/>.
        /// </summary>
        public int Stamps { get; private set; }

        public IEnumerable<AdsStampHeader>? AdsStampHeaders { get; private set; }



        public override string ToString()
        {
            return $"{nameof(AdsStampHeader)}: Length={Length}, Stamps={Stamps}";
        }

        private void ParsePacketData()
        {
            Length = BitConverter.ToInt32(_PacketData, 0);
            Stamps = BitConverter.ToInt32(_PacketData, 4);

            var tmpList = new List<AdsStampHeader>(Stamps);

            for (int i = 0; i < Stamps; i++)
            {
                var offset = EXPECTED_DATA_LEN_MIN + _TotalSizeStampHeaders;
                tmpList.Add(new AdsStampHeader(PacketData[offset..]));
                _TotalSizeStampHeaders += tmpList.Last().TotalSizeSamples + AdsNotificationSample.EXPECTED_DATA_LEN_MIN;
            }
            AdsStampHeaders = tmpList;
        }
    }
}
