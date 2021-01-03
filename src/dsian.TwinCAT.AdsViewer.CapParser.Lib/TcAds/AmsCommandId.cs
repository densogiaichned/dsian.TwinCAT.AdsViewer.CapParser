using System;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds
{
    /// <summary>
    /// ADS command id table
    /// </summary>
    public enum AmsCommandId : UInt16
    {
        Invalid = 0,
        ADS_Read_Device_Info = 0x0001,
        ADS_Read = 0x0002,
        ADS_Write = 0x0003,
        ADS_Read_State = 0x0004,
        ADS_Write_Control = 0x0005,
        ADS_Add_Device_Notification = 0x0006,
        ADS_Delete_Device_Notification = 0x0007,
        ADS_Device_Notification = 0x0008,
        ADS_Read_Write = 0x0009,
    }
}
