using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;
using System.Text;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsReadWriteResponse : IPayload
    {
        /// <summary>
        /// ADS Read - Request<br/>
        /// With ADS Read Write data will be written to an ADS device. Additionally, data can be read from the ADS device.<br/>
        /// The data which can be read are addressed by the Index Group and the Index Offset.
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsReadWriteResponse(AmsHeader amsHeader, BinaryReader binaryReader)
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
        public ReadOnlyMemory<byte> Data => _PacketData.AsMemory()[DATA_OFFSET..];

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
                    return $"{nameof(AdsReadWriteResponse)}: Res={Result}, Len={Length}, Data={BitConverter.ToString(_PacketData, DATA_OFFSET, max)}{dotdotdot}";
                }
                else
                    return $"{nameof(AdsReadWriteResponse)}: Res={Result}, Len={Length}";
            }
            else
                return $"{nameof(AdsReadWriteResponse)}: Res={Result}";
        }
    }
}
