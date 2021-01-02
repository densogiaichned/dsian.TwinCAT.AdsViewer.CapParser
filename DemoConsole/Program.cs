using dsian.TwinCAT.AdsViewer.CapParser.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            using (var serviceProvider = new ServiceCollection()
                    .AddLogging(cfg => cfg.AddConsole())
                    .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug)
                    .BuildServiceProvider())
            {
                var logger = serviceProvider.GetService<ILogger<Program>>();

                try
                {
                    var capFilePath = @".\DemoFiles\Demo.cap";
                    var netMon = await NetMonFileFactory.ParseNetMonFileAsync(capFilePath, CancellationToken.None, logger);
                    int i = 0;
                    netMon.FramePackets.ToList().ForEach(frame => Console.WriteLine($"{++i} {frame.Header} {frame.Data}"));
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "Ooops ...");
                }

            }
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
