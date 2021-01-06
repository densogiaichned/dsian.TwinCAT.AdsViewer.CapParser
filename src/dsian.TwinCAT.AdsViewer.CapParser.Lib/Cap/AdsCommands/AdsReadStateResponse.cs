using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.IO;
using System.Text;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public class AdsReadStateResponse : IPayload
    {
        /// <summary>
        /// ADS Read - Request<br/>
        /// With ADS Read data can be read from an ADS device.  The data are addressed by the Index Group and the Index Offset
        /// </summary>
        /// <param name="amsHeader"></param>
        /// <param name="binaryReader"></param>
        /// <exception cref="FormatException"/>
        internal AdsReadStateResponse(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            if (!amsHeader.IsValid) throw new FormatException("Parsed AmsHeader is not valid!");

            if (amsHeader.Data_Length != EXPECTED_DATA_LEN) throw new FormatException($"Parsed AmsHeader is not valid, Data_Length={amsHeader.Data_Length} not allowed.");
            _PacketData = binaryReader.ReadBytes((int)amsHeader.Data_Length);

            ParsePacketData();
        }


        public const int EXPECTED_DATA_LEN = 8;

        private byte[] _PacketData;
        public ReadOnlyMemory<byte> PacketData => _PacketData;


        /// <summary>
        /// ADS error number.
        /// </summary>
        public AdsErrorCode Result { get; private set; }

        /// <summary>
        /// ADS status (see data type ADSSTATE of the ADS-DLL).
        /// </summary>
        public AdsState ADS_State { get; private set; }
        /// <summary>
        /// Device status
        /// </summary>
        public ushort Device_State { get; private set; }

        public override string ToString()
        {

            if(Result == AdsErrorCode.NoError)
                return $"{nameof(AdsReadStateResponse)}: Res={Result}, ADS State={ADS_State}, Device State={Device_State}";
            else
                return $"{nameof(AdsReadStateResponse)}: Res={Result}";
        }
        private void ParsePacketData()
        {
            Result = (AdsErrorCode)BitConverter.ToUInt32(_PacketData, 0);
            ADS_State = (AdsState)BitConverter.ToInt16(_PacketData, 4);
            Device_State = BitConverter.ToUInt16(_PacketData, 6);
        }
    }
}
