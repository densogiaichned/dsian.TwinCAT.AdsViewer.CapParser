using dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Helper
{
    internal static class Helper
    {

        /// <summary>
        /// Deserializes a struct from a binary file/stream<br/>
        /// <a href="https://www.codeproject.com/Articles/10750/Fast-Binary-File-Reading-with-C">kudos to Anthony Baraff</a>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="br"></param>
        /// <returns></returns>
        internal static T? DeserializeStruct<T>(BinaryReader br)
        {
            //Read byte array
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(T)));
            //Make sure that the Garbage Collector doesn't move our buffer 
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            //Marshal the bytes
            var ret = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();//Give control of the buffer back to the GC 
            if (ret is null) return default;
            return (T)ret;
        }


        internal static IEnumerable<UInt32> GetFrameTable(NetMonHeader netMonHeader, BinaryReader br)
        {
            int uiLength = sizeof(UInt32);

            var offset = netMonHeader.Header.frametableoffset;
            if (offset <= NetMonHeader.HEADER_SIZE) throw new FormatException($"Invalid FrameTable offset \"{ offset }\".");

            var length = netMonHeader.Header.frametablelength;
            if (length < uiLength) throw new FormatException($"Invalid FrameTable length \"{ length }\".");

            if (length % uiLength != 0) throw new FormatException($"Invalid FrameTable length { length }, must be a multiple of {uiLength}.");

            var indices = length / uiLength;

            br.BaseStream.Position = offset; // set stream to start of FrameTable
            var buffer = br.ReadBytes((int)length);

            var table = new UInt32[indices];
            for (int i = 0; i < indices; i++)
            {
                table[i] = BitConverter.ToUInt32(buffer, i * uiLength);
            }
            return table;
        }

        internal static IEnumerable<FramePacket> DeserializeFramePackets(IEnumerable<UInt32> frameTable, BinaryReader binaryReader, CancellationToken? cancellationToken = null)
        {

            var framePackets = new List<FramePacket>(frameTable.Count());
            foreach (var ft in frameTable)
            {
                if (cancellationToken != null && cancellationToken.Value.IsCancellationRequested)
                    break;
                binaryReader.BaseStream.Position = ft;  // Frametable is offset
                var frHeader = FrameHeader.DeserializeHeader(binaryReader);
                framePackets.Add(new FramePacket(frHeader, new PacketData(frHeader, binaryReader)));
            }
            return framePackets;
        }

    }
}
