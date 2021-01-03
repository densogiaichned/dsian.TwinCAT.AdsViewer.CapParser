using dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.Cap.AdsCommands
{
    internal static class AdsCommandFactory
    {

        /// <summary>
        /// Max printed Data in ToString() method
        /// </summary>
        internal const int MAX_DATA_PRNT = 32;

        /// <summary>
        /// Lookup table for all implemented ADS commands.<br/>
        /// see <a href="https://infosys.beckhoff.com/english.php?content=../content/1033/tcadsamsspec/html/tcadsamsspec_adscmds.htm">TwinCAT ADS/AMS - Specification</a>
        /// </summary>
        internal static readonly IReadOnlyDictionary<ushort, Type> AdsCommandLookUp = new Dictionary<ushort, Type>
        {
            {BuildRequestOfCommand(AmsCommandId.ADS_Read_Device_Info), typeof(AdsReadDeviceInfoRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Read_Device_Info), typeof(AdsReadDeviceInfoResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Read), typeof(AdsReadRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Read), typeof(AdsReadResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Write), typeof(AdsWriteRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Write), typeof(AdsWriteResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Read_State), typeof(AdsReadStateRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Read_State), typeof(AdsReadStateResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Write_Control), typeof(AdsWriteControlRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Write_Control), typeof(AdsWriteControlResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Add_Device_Notification), typeof(AdsAddDeviceNotificationRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Add_Device_Notification), typeof(AdsAddDeviceNotificationResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Delete_Device_Notification), typeof(AdsDeleteDeviceNotificationRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Delete_Device_Notification), typeof(AdsDeleteDeviceNotificationResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Read_Write), typeof(AdsReadWriteRequest) },
            {BuildResponseOfCommand(AmsCommandId.ADS_Read_Write), typeof(AdsReadWriteResponse) },
            {BuildRequestOfCommand(AmsCommandId.ADS_Device_Notification), typeof(AdsDeviceNotificationRequest) },
        };


        internal static IPayload GetAdsCommand(AmsHeader amsHeader, BinaryReader binaryReader)
        {
            UInt16 cId = BuildReqResFromAmsHeader(amsHeader);
            if (AdsCommandLookUp.TryGetValue(cId, out var adsCommandType))
            {
                var constructorInfo = adsCommandType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new Type[] { typeof(AmsHeader), typeof(BinaryReader) }, null);
                if (constructorInfo is not null)
                    try
                    {
                        return (IPayload)(constructorInfo.Invoke(new object[] { amsHeader, binaryReader }));
                    }
                    catch (Exception ex)
                    {
                        throw ex.InnerException ?? ex;
                    }

            }
            throw new Exception("Fatal Error, no valid constructor found.");
        }

        private static ushort BuildRequestOfCommand(AmsCommandId amsCommandId)
        {
            return (ushort)amsCommandId;    // request is only the commandId reflected...
        }
        private static ushort BuildResponseOfCommand(AmsCommandId amsCommandId)
        {
            return (ushort)((ushort)amsCommandId | (1 << 15));
        }

        private static ushort BuildReqResFromAmsHeader(AmsHeader amsHeader)
        {
            return amsHeader.IsResponse ? BuildResponseOfCommand(amsHeader.AmsCommandId) : BuildRequestOfCommand(amsHeader.AmsCommandId);
        }
    }
}
