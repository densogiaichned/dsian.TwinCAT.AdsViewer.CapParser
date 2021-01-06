# dsian.TwinCAT.AdsViewer.CapParser
[![build](https://github.com/densogiaichned/dsian.TwinCAT.AdsViewer.CapParser/workflows/build/badge.svg)](https://www.nuget.org/packages/dsian.TwinCAT.AdsViewer.CapParser.Lib/)

Parses a *.cap logfile from Beckhoff TwinCAT AmsAdsViewer.  
Although the logfiles basically are "Network Monitor 2.x" files, it only supports ADS frames.


### Example
```csharp
using dsian.TwinCAT.AdsViewer.CapParser.Lib;
//...
var capFilePath = @".\DemoFiles\Demo.cap";
var netMon = await NetMonFileFactory.ParseNetMonFileAsync(capFilePath, CancellationToken.None, logger);
netMon.FramePackets.ToList().ForEach(frame => Console.WriteLine($"{frame.Index} {frame.Header}, {frame.Data}"));
// ...
```

or see Project "DemoConsole"


* TC3 ADS Monitor:
    Download: [TF6010 | TC3 ADS Monitor](https://www.beckhoff.com/en-en/products/automation/twincat/tfxxxx-twincat-3-functions/tf6xxx-tc3-connectivity/tf6010.html)  
    Documentation: [infosys.beckhoff.com](https://infosys.beckhoff.com/index.php?content=../content/1033/tcadsmonitor/html/tcadsmonitor_viewer_overview.htm)  
    Minimum requirement for TC3 ADS Monitor: [TC1000 | TC3 ADS](https://www.beckhoff.com/en-en/products/automation/twincat/tc1xxx-twincat-3-base/tc1000.html)
