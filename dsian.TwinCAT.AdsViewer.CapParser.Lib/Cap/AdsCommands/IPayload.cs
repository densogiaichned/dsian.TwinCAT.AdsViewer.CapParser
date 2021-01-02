using System;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    public interface IPayload
    {
        /// <summary>
        /// The raw, unparsed packet data
        /// </summary>
        ReadOnlyMemory<byte> PacketData { get; }
    }
}
