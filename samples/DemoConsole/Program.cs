﻿using dsian.TwinCAT.AdsViewer.CapParser.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
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
                    var (ok, nmf) = await NetMonFileFactory.TryParseNetMonFileAsync(capFilePath, CancellationToken.None, logger);
                    if(ok)
                    {
                        nmf.FramePackets.ToList().ForEach(frame => Console.WriteLine($"{frame.Index} {frame.Header}, {frame.Data}"));
                    }
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
