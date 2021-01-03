using dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands;
using System.IO;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap
{
    public class PacketData
    {
        internal PacketData(FrameHeader frameHeader, BinaryReader binaryReader)
        {

            this.EthernetHeader = Helper.Helper.DeserializeStruct<EthernetHeader>(binaryReader);
            this.EtherCATHeader = Helper.Helper.DeserializeStruct<EtherCATHeader>(binaryReader);
            this.AmsHeader = Helper.Helper.DeserializeStruct<AmsHeader>(binaryReader);
            this.AdsCommand = AdsCommandFactory.GetAdsCommand(this.AmsHeader, binaryReader);
        }

        public EthernetHeader EthernetHeader { get; private set; }
        public EtherCATHeader EtherCATHeader { get; private set; }
        public AmsHeader AmsHeader { get; private set; }

        public IPayload AdsCommand { get; private set; }


        public override string ToString()
        {
            return $"{AdsCommand}";
        }

    }
}
