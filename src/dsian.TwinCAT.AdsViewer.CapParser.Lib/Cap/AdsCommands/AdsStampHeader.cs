using System;
using System.Collections.Generic;
using System.Linq;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    /// <summary>
    /// ADS Device Notification - AdsStampHeader
    /// </summary>
    /// <remarks>
    /// The data which are transfered at the Device Notification  are multiple nested into one another.<br/>
    /// The Notification Stream contains an array with elements of type AdsStampHeader.<br/>
    /// This array again contains elements of type AdsStampHeader.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public class AdsStampHeader
    {
        internal AdsStampHeader(ReadOnlyMemory<byte> readOnlyMemory)
        {
            if (readOnlyMemory.Length < EXPECTED_DATA_LEN_MIN)
                throw new ArgumentOutOfRangeException($"Expected at least {nameof(readOnlyMemory.Length)} >= {EXPECTED_DATA_LEN_MIN}.");

            ParsePacketData(readOnlyMemory);

            _PacketData = readOnlyMemory.Slice(0, EXPECTED_DATA_LEN_MIN + _TotalSizeSamples).ToArray();

        }
        public const int EXPECTED_DATA_LEN_MIN = 12;

        /// <inheritdoc cref="TotalSizeSamples"/>
        private int _TotalSizeSamples = 0;
        /// <summary>
        /// Total size in Bytes of <see cref="AdsNotificationSamples"/>
        /// </summary>
        public int TotalSizeSamples => _TotalSizeSamples;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;


        /// <summary>
        /// The timestamp is coded after the Windos FILETIME format.
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Number of elements of type <see cref="AdsNotificationSample"/>. 
        /// </summary>
        public UInt32 Samples { get; private set; }


        /// <summary>
        /// Array with elements of type <see cref="AdsNotificationSample"/>.
        /// </summary>
        public IEnumerable<AdsNotificationSample>? AdsNotificationSamples { get; private set; }


        public override string ToString()
        {
            return $"{nameof(AdsStampHeader)}: TimeStamp={TimeStamp}, Samples={Samples}";
        }
        private void ParsePacketData(ReadOnlyMemory<byte> readOnlyMemory)
        {
#if NETSTANDARD2_0
            TimeStamp = DateTime.FromFileTimeUtc(BitConverter.ToInt64(readOnlyMemory[..8].ToArray(), 0));
            Samples = BitConverter.ToUInt32(readOnlyMemory[8..EXPECTED_DATA_LEN_MIN].ToArray(), 0);
#else
            TimeStamp = DateTime.FromFileTimeUtc(BitConverter.ToInt64(readOnlyMemory[..8].Span));
            Samples = BitConverter.ToUInt32(readOnlyMemory[8..EXPECTED_DATA_LEN_MIN].Span);
#endif
            var tmpList = new List<AdsNotificationSample>((int)Samples);


            for (int i = 0; i < Samples; i++)
            {
                var offset = _TotalSizeSamples + EXPECTED_DATA_LEN_MIN;
                tmpList.Add(new AdsNotificationSample(readOnlyMemory[offset..]));
                _TotalSizeSamples += tmpList.Last().SampleSize + AdsNotificationSample.EXPECTED_DATA_LEN_MIN;
            }
            AdsNotificationSamples = tmpList;
        }
    }
}
