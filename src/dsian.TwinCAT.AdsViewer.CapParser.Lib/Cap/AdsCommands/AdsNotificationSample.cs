using System;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    /// <summary>
    /// ADS Device Notification - AdsNotificationSample
    /// </summary>
    /// <remarks>
    /// The data which are transfered at the Device Notification  are multiple nested into one another.<br/>
    /// The Notification Stream contains an array with elements of type AdsStampHeader.<br/>
    /// This array again contains elements of type AdsNotificationSample.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public class AdsNotificationSample
    {
        internal AdsNotificationSample(ReadOnlyMemory<byte> readOnlyMemory)
        {
            if (readOnlyMemory.Length < EXPECTED_DATA_LEN_MIN)
                throw new ArgumentOutOfRangeException($"Expected at least {nameof(readOnlyMemory.Length)} >= {EXPECTED_DATA_LEN_MIN}.");

            ParsePacketData(readOnlyMemory);

            _PacketData = readOnlyMemory.Slice(0, EXPECTED_DATA_LEN_MIN + SampleSize).ToArray();
        }
        public const int EXPECTED_DATA_LEN_MIN = 8;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;

        /// <summary>
        /// Handle of notification. 
        /// </summary>
        public UInt32 NotificationHandle { get; private set; }
        /// <summary>
        /// Size of data range in bytes. 
        /// </summary>
        public int SampleSize { get; private set; }
        /// <summary>
        /// Data which are written in the ADS device.
        /// </summary>
        public ReadOnlyMemory<byte> Data => _PacketData.AsMemory()[EXPECTED_DATA_LEN_MIN..];

        public override string ToString()
        {
            if (SampleSize > 0)
            {
                var max = (int)Math.Min(SampleSize, AdsCommandFactory.MAX_DATA_PRNT);
                var dotdotdot = SampleSize > AdsCommandFactory.MAX_DATA_PRNT ? "..." : string.Empty;
                return $"{nameof(AdsNotificationSample)}: NotificationHandle={NotificationHandle}, SampleSize={SampleSize}, Data={BitConverter.ToString(_PacketData, EXPECTED_DATA_LEN_MIN, max)}{dotdotdot}";
            }
            else
                return $"{nameof(AdsNotificationSample)}: NotificationHandle={NotificationHandle}, SampleSize={SampleSize}";
        }
        private void ParsePacketData(ReadOnlyMemory<byte> readOnlyMemory)
        {
#if NETSTANDARD2_0
            NotificationHandle = BitConverter.ToUInt32(readOnlyMemory[..4].ToArray(), 0);
            SampleSize = BitConverter.ToInt32(readOnlyMemory[4..8].ToArray(), 0);
#else
            NotificationHandle = BitConverter.ToUInt32(readOnlyMemory[..4].Span);
            SampleSize = BitConverter.ToInt32(readOnlyMemory[4..8].Span);
#endif
        }
    }
}
