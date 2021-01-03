using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    internal class AdsReadResponse : IPayload
    {
        /// <summary>
        /// ADS Read - Request<br/>
        /// With ADS Read data can be read from an ADS device.  The data are addressed by the Index Group and the Index Offset
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsReadResponse(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length < EXPECTED_DATA_LEN_MIN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }


        public const uint EXPECTED_DATA_LEN_MIN = 8;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;


        /// <summary>
        /// ADS error number.
        /// </summary>
        public AdsErrorCode Result { get; private set; }

        /// <summary>
        /// Length of data which are supplied back.
        /// </summary>
        public UInt32 Length { get; private set; }

        /// <summary>
        /// Data which are supplied back.
        /// </summary>
        public ReadOnlyMemory<byte> Data => _PacketData[DATA_OFFSET..];

        private void ParsePacketData()
        {
            Result = (AdsErrorCode)BitConverter.ToUInt32(_PacketData, 0);
            Length = BitConverter.ToUInt32(_PacketData, 4);

        }

        private const int DATA_OFFSET = 8;
        public override string ToString()
        {
            if (Result == AdsErrorCode.NoError)
            {

                if (Length > 0)
                {
                    var max = (int)Math.Min(Length, AdsCommandFactory.MAX_DATA_PRNT);
                    var dotdotdot = Length > AdsCommandFactory.MAX_DATA_PRNT ? "..." : string.Empty;
                    return $"{nameof(AdsReadResponse)}: Res={Result}, Len={Length}, Data={BitConverter.ToString(_PacketData, DATA_OFFSET, max)}{dotdotdot}";
                }
                else
                    return $"{nameof(AdsReadResponse)}: Res={Result}, Len={Length}";
            }
            else
                return $"{nameof(AdsReadResponse)}: Res={Result}";
        }
    }
}
