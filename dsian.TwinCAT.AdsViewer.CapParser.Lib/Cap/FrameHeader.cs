using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    /// <summary>
    /// Deserializes a FrameHeader
    /// </summary>
    /// <exception cref="Exception"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public sealed class FrameHeader
    {
        public const int HEADER_SIZE = 16;
        private FrameHeader(FrameHeaderStruct? frameHdr)
        {
            Header = frameHdr ?? throw new ArgumentNullException(nameof(frameHdr));
            TimeStamp = DateTime.FromFileTime(Header.ts_delta);
            Length = Header.orig_len;
        }

        /// <summary>
        /// A header of a frame, with timestamp and size
        /// </summary>
        public FrameHeaderStruct Header { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public int Length { get; private set; }

        public static FrameHeader DeserializeHeader(BinaryReader br)
        {
            var header = Helper.Helper.DeserializeStruct<FrameHeaderStruct>(br);
            return new(header);
        }
        public override string ToString()
        {
            return $"TimeStamp={TimeStamp:HH:mm:ss.fffff}, Len={Length}";
        }
    }
}
