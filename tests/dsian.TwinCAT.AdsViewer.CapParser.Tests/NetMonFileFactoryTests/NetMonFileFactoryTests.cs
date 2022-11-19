using dsian.TwinCAT.AdsViewer.CapParser.Lib;
using dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap;
using dsian.TwinCAT.AdsViewer.CapParser.Tests.MoqExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Tests.NetMonFileFactoryTests
{
    [TestClass]
    public class NetMonFileFactoryTests
    {

        [TestMethod]
        public async Task Should_throw_ArgumentNullException()
        {
            string nullString = null;
            FileInfo fi = null;
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NetMonFileFactory.ParseNetMonFileAsync(nullString, CancellationToken.None, null));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NetMonFileFactory.ParseNetMonFileAsync(string.Empty, CancellationToken.None, null));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, null));
        }

        [TestMethod]
        public async Task Should_throw_FileNotFoundException()
        {
            var testFile = @"./TestData/invalidFilePath.cap";
            FileInfo fi = new FileInfo(testFile);
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null));
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, null));
        }

        [TestMethod]
        public async Task Should_parse_two_FramePackets_ReadRequest_ReadResponse_from_string()
        {
            var testFile = @"./TestData/Valid_ReadRequest.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.IsTrue(netMonFile.FramePackets.Count() == 2);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsRequest);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsResponse);
        }

        [TestMethod]
        public async Task Should_TryParse_two_FramePackets_ReadRequest_ReadResponse_from_string()
        {
            var testFile = @"./TestData/Valid_ReadRequest.cap";
            var (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(testFile, CancellationToken.None, null);
            Assert.IsTrue(ok);
            Assert.IsTrue(netMonFile.FramePackets.Count() == 2);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsRequest);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsResponse);
        }

        [TestMethod]
        public async Task Should_parse_two_FramePackets_ReadRequest_ReadResponse_from_FileInfo()
        {
            FileInfo fi = new FileInfo(@"./TestData/Valid_ReadRequest.cap");
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, null);

            Assert.IsTrue(netMonFile.FramePackets.Count() == 2);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsRequest);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsResponse);
        }
        [TestMethod]
        public async Task Should_TryParse_two_FramePackets_ReadRequest_ReadResponse_from_FileInfo()
        {
            FileInfo fi = new FileInfo(@"./TestData/Valid_ReadRequest.cap");
            var (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(fi, CancellationToken.None, null);
            Assert.IsTrue(ok);
            Assert.IsTrue(netMonFile.FramePackets.Count() == 2);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(0).Data.AmsHeader.IsRequest);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsValid);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read);
            Assert.IsTrue(netMonFile.FramePackets.ElementAt(1).Data.AmsHeader.IsResponse);
        }

        [TestMethod]
        public async Task Should_fail_TryParse_two_FramePackets_ReadRequest_ReadResponse_from_string()
        {
            var testFile = @"./TestData/Invalid_ReadRequest.cap";
            var (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(testFile, CancellationToken.None, null);
            Assert.IsFalse(ok);
            Assert.IsNull(netMonFile);
            testFile = @"./TestData/FileDoesNotExist.cap";
            (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(testFile, CancellationToken.None, null);
            Assert.IsFalse(ok);
            Assert.IsNull(netMonFile);
        }

        [TestMethod]
        public async Task Should_fail_TryParse_two_FramePackets_ReadRequest_ReadResponse_from_FileInfo()
        {
            FileInfo fi = new FileInfo(@"./TestData/Invalid_ReadRequest.cap");
            var (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(fi, CancellationToken.None, null);
            Assert.IsFalse(ok);
            Assert.IsNull(netMonFile);
            fi = new FileInfo(@"./TestData/FileDoesNotExist.cap");
            (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(fi, CancellationToken.None, null);
            Assert.IsFalse(ok);
            Assert.IsNull(netMonFile);
        }

        [TestMethod]
        public async Task Should_fail_TryParse_two_FramePackets_with_invalid_arguments()
        {
            var (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(pathToCap: null, CancellationToken.None, null);
            Assert.IsFalse(ok);
            Assert.IsNull(netMonFile);
            (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(string.Empty, CancellationToken.None, null);
            Assert.IsFalse(ok);
            Assert.IsNull(netMonFile);
            (ok, netMonFile) = await NetMonFileFactory.TryParseNetMonFileAsync(fi: null, CancellationToken.None, null);
            Assert.IsFalse(ok);
            Assert.IsNull(netMonFile);
        }


        [TestMethod]
        public async Task Should_log_invalid_Header_GMBU_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@"./TestData/Invalid_Header_GMBU.cap");
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);

            var expectedException = new FormatException($"Is not a valid NetMon 2.x file format.");

            Assert.IsNull(netMonFile);
            loggerMock.VerifyLoggingException($"[ParseCapFile] Error while parsing file \"{fi.Name}\".", expectedException);
        }

        [TestMethod]
        public async Task Should_log_invalid_Header_version_3_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@"./TestData/Invalid_Header_Version.cap");
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);
            var expectedException = new FormatException($"NetMon file version \"3.x\" is not supported.");

            Assert.IsNull(netMonFile);
            loggerMock.VerifyLoggingException($"[ParseCapFile] Error while parsing file \"{fi.Name}\".", expectedException);
        }

        [TestMethod]
        public async Task Should_log_invalid_Ams_Header_AmsCommandId_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@"./TestData/Invalid_Ams_Header_AmsCommandId.cap");
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);
            var expectedException = new Exception("Fatal Error, no valid constructor found.");

            Assert.IsNull(netMonFile);
            loggerMock.VerifyLoggingException($"[ParseCapFile] Error while parsing file \"{fi.Name}\".", expectedException);
        }


        [TestMethod]
        public async Task Should_log_invalid_Ams_Header_version_3_in_Cap_File()
        {
            var loggerMock = new Mock<ILogger<NetMonFile>>();

            FileInfo fi = new FileInfo(@"./TestData/Invalid_Ams_Header_DataLength.cap");
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(fi, CancellationToken.None, loggerMock.Object);
            var expectedException = new FormatException($"Parsed AmsHeader is not valid, Data_Length=255 not allowed.");

            Assert.IsNull(netMonFile);
            loggerMock.VerifyLoggingException($"[ParseCapFile] Error while parsing file \"{fi.Name}\".", expectedException);
        }


        [TestMethod]
        public async Task Should_parse_all_710_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Count(), 710);
            netMonFile.FramePackets.ToList().ForEach(packet => Assert.IsTrue(packet.Data.AmsHeader.IsValid));
        }

        [TestMethod]
        public async Task Should_parse_250_ADS_Read_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet=>packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read).Count(), 250);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read).Count(), 250);
        }

        [TestMethod]
        public async Task Should_parse_7_ADS_Write_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Write).Count(), 7);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Write).Count(), 7);
        }

        [TestMethod]
        public async Task Should_parse_0_ADS_AddDeviceNotification_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Add_Device_Notification).Count(), 0);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Add_Device_Notification).Count(), 0);
        }

        [TestMethod]
        public async Task Should_parse_0_ADS_DeleteDeviceNotification_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Delete_Device_Notification).Count(), 0);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Delete_Device_Notification).Count(), 0);
        }

        [TestMethod]
        public async Task Should_parse_52_ADS_Device_Notification_RequestFramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Device_Notification).Count(), 52);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Device_Notification).Count(), 0); // there is no response!
        }

        [TestMethod]
        public async Task Should_parse_0_ADS_Read_Device_Info_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read_Device_Info).Count(), 0);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read_Device_Info).Count(), 0);
        }

        [TestMethod]
        public async Task Should_parse_0_ADS_Read_State_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read_State).Count(), 0);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read_State).Count(), 0);
        }

        [TestMethod]
        public async Task Should_parse_72_ADS_Read_Write_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read_Write).Count(), 72);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Read_Write).Count(), 72);
        }

        [TestMethod]
        public async Task Should_parse_0_ADS_Write_Control_Request_Response_FramePackets()
        {
            var testFile = @"./TestData/Valid_Test_Data.cap";
            var netMonFile = await NetMonFileFactory.ParseNetMonFileAsync(testFile, CancellationToken.None, null);

            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsRequest && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Write_Control).Count(), 0);
            Assert.AreEqual(netMonFile.FramePackets.Where(packet => packet.Data.AmsHeader.IsResponse && packet.Data.AmsHeader.AmsCommandId == Lib.TcAds.AmsCommandId.ADS_Write_Control).Count(), 0);
        }
    }
}
