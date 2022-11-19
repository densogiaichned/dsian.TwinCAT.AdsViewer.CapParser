using dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
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
        public static async Task<NetMonFile?> ParseNetMonFileAsync(string pathToCap, CancellationToken cancellationToken, ILogger logger)
        {
            if (string.IsNullOrEmpty(pathToCap)) throw new ArgumentNullException(nameof(pathToCap));
            return await ParseNetMonFileAsync(new FileInfo(pathToCap), cancellationToken, logger);
        }

        /// <inheritdoc cref="ParseNetMonFileAsync(string , CancellationToken , ILogger? )"/>
        public static async Task<NetMonFile?> ParseNetMonFileAsync(FileInfo pathToCap, CancellationToken cancellationToken, ILogger? logger)
        {
            var fi = pathToCap ?? throw new ArgumentNullException(nameof(pathToCap));
            if (!fi.Exists) throw new FileNotFoundException("Could not find file.", fi.FullName);
            return await Task.Run(() => ParseCapFile(fi, cancellationToken, logger));
        }

        /// <summary>
        /// Tries to parse a NetMon file, e.g. *.cap and returns instance of <see cref="NetMonFile"/> asynchronously
        /// </summary>
        /// <param name="pathToCap"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="logger"></param>
        /// <returns>true if successful</returns>
        public static async Task<Tuple<bool, NetMonFile?>> TryParseNetMonFileAsync(string pathToCap, CancellationToken cancellationToken, ILogger? logger)
        {
            if (string.IsNullOrEmpty(pathToCap))
            {
                logger?.LoggingError(new ArgumentNullException(nameof(pathToCap)), "Argument is null.", args: nameof(pathToCap));
                return await Task.FromResult(new Tuple<bool, NetMonFile?>(false, default));
            }
            return await TryParseNetMonFileAsync(new FileInfo(pathToCap), cancellationToken, logger);
        }

        /// <inheritdoc cref="TryParseNetMonFileAsync(string , CancellationToken , ILogger? )"/>
        public static async Task<Tuple<bool, NetMonFile?>> TryParseNetMonFileAsync(FileInfo fi, CancellationToken cancellationToken, ILogger? logger)
        {

            if (fi == null)
            {
                logger?.LoggingError(new ArgumentNullException(nameof(fi)), "Argument is null.", args: nameof(fi));
                return new Tuple<bool, NetMonFile?>(false, default);
            }
            if (!fi.Exists)
            {
                logger?.LoggingError(new FileNotFoundException("Could not find file.", fi.FullName), "File not found. \"{FileName}\"", args: fi.Name);
                return new Tuple<bool, NetMonFile?>(false, default);
            }
            var nmf = await Task.Run(() => ParseCapFile(fi, cancellationToken, logger));
            return new Tuple<bool, NetMonFile?>(nmf is not null, nmf);
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
                logger?.LoggingError(ex, $"Error while parsing file \"{{FileName}}\".", args: fi.Name);
                return default;
            }
        }

        /// <summary>
        /// ILogger extension for ILogger.LogError
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        private static void LoggingError(this ILogger logger, Exception ex, string message, [System.Runtime.CompilerServices.CallerMemberName] string funcName = "", params object[] args)
        {
            var argsList = args.ToList();
            argsList.Reverse();
            argsList.Add(funcName);
            argsList.Reverse();
            using (logger?.BeginScope(nameof(NetMonFileFactory)))
            {
                logger?.LogError(ex, $"[{{FunctionName}}] { message }", argsList.ToArray());
            }
        }

    }
}
