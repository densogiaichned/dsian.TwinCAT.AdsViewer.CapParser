using dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib
{
    public static class NetMonFileFactory
    {

        /// <summary>
        /// Parses a NetMon file, e.g. *.cap and returns instance of <see cref="NetMonFile"/> asynchronously
        /// </summary>
        /// <param name="pathToCap"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FileNotFoundException" />
        public static async Task<NetMonFile?> ParseNetMonFileAsync(string pathToCap, CancellationToken cancellationToken, ILogger? logger)
        {
            if (string.IsNullOrEmpty(pathToCap)) throw new ArgumentNullException(nameof(pathToCap));
            return await ParseNetMonFileAsync(new FileInfo(pathToCap), cancellationToken, logger);
        }

        /// <inheritdoc cref="ParseNetMonFileAsync(string)"/>
        public static async Task<NetMonFile?> ParseNetMonFileAsync(FileInfo pathToCap, CancellationToken cancellationToken, ILogger? logger)
        {
            var fi = pathToCap ?? throw new ArgumentNullException(nameof(pathToCap));
            if (!fi.Exists) throw new FileNotFoundException("File not found.", fi.FullName);
            return await Task.Run(() => ParseCapFile(fi, cancellationToken, logger));
        }

        /// <summary>
        /// parses a *.cap file
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static NetMonFile? ParseCapFile(FileInfo fi, CancellationToken cancellationToken, ILogger? logger)
        {
            try
            {
                using (FileStream fs = new FileStream(fi.FullName, FileMode.Open))
                {
                    fs.Position = 0;
                    using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8))
                    {
                        var fileHeader = NetMonHeader.DeserializeHeader(br);
                        var frameTable = Helper.Helper.GetFrameTable(fileHeader, br);
                        var framePackets = Helper.Helper.DeserializeFramePackets(frameTable, br, cancellationToken);
                        return new NetMonFile(fileHeader, framePackets, frameTable);
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Error while parsing file \"{fi.Name}\".");
                return default;
            }
        }
    }
}
