﻿using dsian.TwinCAT.AdsViewer.CapParser.Lib;
using dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap;
using dsian.TwinCAT.AdsViewer.CapParser.Tests.MoqExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Tests.NetMonFileFactoryTests
{
    [TestClass]
    public class NetMonFileFactoryTests
    {

        [TestMethod]
        public async Task Should_Throw_ArgumentNullException()
        {
            string nullString = null;
            FileInfo fi = null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NetMonFileFactory.ParseNetMonFileAsync(nullString, CancellationToken.None, null));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NetMonFileFactory.ParseNetMonFileAsync(string.Empty, CancellationToken.None, null));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, null));
        }

        [TestMethod]
        public async Task Should_Throw_FileNotFoundException()
        {
            var testFile = @".\TestData\invalidFilePath.cap";
            FileInfo fi = new FileInfo(testFile);
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null));
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, null));
        }

        [TestMethod]
        public async Task Should_parse_two_FramePackets_ReadRequest_ReadResponse_from_string()
        {
            var testFile = @".\TestData\Valid_ReadRequest.cap";
            var framePackets = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.IsTrue(framePackets.FramePackets.Count() == 2);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(0).Data.AmsHeader.IsValid);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(0).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(0).Data.AmsHeader.IsRequest);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(1).Data.AmsHeader.IsValid);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(1).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(1).Data.AmsHeader.IsResponse);
        }

        [TestMethod]
        public async Task Should_parse_two_FramePackets_ReadRequest_ReadResponse_from_FileInfo()
        {
            FileInfo fi = new FileInfo(@".\TestData\Valid_ReadRequest.cap");
            var framePackets = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, null);

            Assert.IsTrue(framePackets.FramePackets.Count() == 2);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(0).Data.AmsHeader.IsValid);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(0).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(0).Data.AmsHeader.IsRequest);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(1).Data.AmsHeader.IsValid);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(1).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(framePackets.FramePackets.ElementAt(1).Data.AmsHeader.IsResponse);
        }

        [TestMethod]
        public async Task Should_log_invalid_Header_GMBU_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@".\TestData\Invalid_Header_GMBU.cap");
            var framePackets = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);

            var expectedException = new FormatException($"Is not a valid NetMon 2.x file format.");

            Assert.IsNull(framePackets);
            loggerMock.VerifyLoggingException($"Error while parsing file \"{fi.Name}\".", expectedException);
        }

        [TestMethod]
        public async Task Should_log_invalid_Header_version_3_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@".\TestData\Invalid_Header_Version.cap");
            var framePackets = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);
            var expectedException = new FormatException($"NetMon file version \"3.x\" is not supported.");

            Assert.IsNull(framePackets);
            loggerMock.VerifyLoggingException($"Error while parsing file \"{fi.Name}\".", expectedException);
        }

        [TestMethod]
        public async Task Should_log_invalid_Ams_Header_AmsCommandId_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@".\TestData\Invalid_Ams_Header_AmsCommandId.cap");
            var framePackets = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);
            var expectedException = new Exception("Fatal Error, no valid constructor found.");

            Assert.IsNull(framePackets);
            loggerMock.VerifyLoggingException($"Error while parsing file \"{fi.Name}\".", expectedException);
        }
       

        [TestMethod]
        public async Task Should_log_invalid_Ams_Header_version_3_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@".\TestData\Invalid_Ams_Header_DataLength.cap");
            var framePackets = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);
            var expectedException = new FormatException($"Parsed AmsHeader is not valid, Data_Length=255 not allowed.");

            Assert.IsNull(framePackets);
            loggerMock.VerifyLoggingException($"Error while parsing file \"{fi.Name}\".", expectedException);
        }
    }
}
