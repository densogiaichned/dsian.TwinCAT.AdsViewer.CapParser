using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds
{
    /// <summary>
    /// ADS Transmission Mode for ADS Notifications.
    /// </summary>
    /// <remarks>
    /// The <see cref="T:TwinCAT.Ads.AdsTransMode" /> configures the registration of the <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> at the
    /// server system and how the parameters of the
    /// <see cref="M:TwinCAT.Ads.TcAdsClient.AddDeviceNotification(System.String,TwinCAT.Ads.AdsStream,System.Int32,System.Int32,TwinCAT.Ads.AdsTransMode,System.Int32,System.Int32,System.Object)" />
    /// are interpreted.
    /// The following general scenarios are addressed:
    /// <list type="bullet"><item>Cyclic notifications.</item><item>Notifications on value change.</item><item>Server side and Client side notifications.</item><item>Binding of notifications to specific tasks.</item></list>
    /// <para>
    /// <para>In the default case the <see cref="F:TwinCAT.Ads.AdsTransMode.OnChange" /> or the <see cref="F:TwinCAT.Ads.AdsTransMode.Cyclic" /> (Server cycle) should be used.
    /// All other modes are side cases for special purposes.</para>
    /// More about the AdsNotifications: <a href="0fb21396-9448-45f0-9970-115c333627c5.htm">ADS Notification concept</a>.
    /// </para>
    /// </remarks>
    /// <seealso cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" />
    /// <seealso cref="E:TwinCAT.Ads.TcAdsClient.AdsNotificationEx" />
    /// <seealso cref="O:TwinCAT.Ads.TcAdsClient.AddDeviceNotification" />
    /// <seealso cref="O:TwinCAT.Ads.TcAdsClient.AddDeviceNotificationEx" />
    // Token: 0x0200007D RID: 125
    public enum AdsTransMode
    {
        /// <summary>
        /// None / Uninitalized transport mode.
        /// No <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event is fired.
        /// </summary>
        // Token: 0x040000AB RID: 171
        None,
        /// <summary>
        /// Client triggered cyclic <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event.
        /// The <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event is fired cyclically triggered from the client side.
        /// Polling is used from the User Application to read values, before they are fired as Notifications.
        /// <para>Client side triggering has the following consequences:
        /// <list type="bullet"><item>The realtime environment on the server side will be less stressed (expecially the mailbox queue).</item><item>Value requests are serialized one after another and are handled slower (synchronouly, not asynchronously)</item><item>Implicit synchronization of the events into the UI Thread.</item></list></para>
        /// </summary>
        // Token: 0x040000AC RID: 172
        ClientCycle,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event is fired when data changes triggered by the client.
        /// The <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event is fired on-change triggered from the client side.
        /// Polling is used from the User Application to read values, before they are fired as Notifications.
        /// Client side triggering has the following consequences:
        /// <list type="bullet">
        /// <item><description>The realtime environment on the server side will be less stressed (expecially the mailbox queue).</description></item>
        /// <item><description>Value requests are serialized one after another and are handled slower (synchronouly, not asynchronously)</description>.</item>
        /// <item><description>Implicit synchronization of the events into the UI Thread.</description></item>
        /// </list>
        /// </summary>
        // Token: 0x040000AD RID: 173
        ClientOnChange,
        /// <summary>
        ///   <para>
        /// The <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event is fired cyclically.</para>
        ///   <para>
        /// The Notification will be registered on the ADS Server side for a cyclical trigger (dependant on time parameter) and is bound
        /// to the 'default' task of the addressed target. In case of the PLC target (e.g. Port 851) the default
        /// task is the first configured task.</para>
        /// <para>
        /// Each time the 'default' task has finished its cycle the realtime system will check for the expired cycle time
        /// and sends the <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> message on expiry.</para>
        /// <para>The used ContextMask for the 'default' task is 0.
        /// </para>
        /// <para>
        /// <strong>Please be aware, that server side 'Change' notifications stress the realtime system and should be handled with care.
        /// Therefore, dependent of the cycle time of the task and the capabilities of the system only a limited set of Cyclic Notifications should be used!
        /// </strong>
        /// </para>
        /// <para>A system limit for server side notification registrations is 1024.</para>
        /// </summary>
        // Token: 0x040000AE RID: 174
        Cyclic,
        /// <summary>
        /// <para>
        /// On-Change <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event.</para>
        /// <para>The Notification will be registered on the ADS Server side for an on-change and optional cyclical trigger (dependant on parameters) and is bound
        /// to the 'default' task of the addressed target. In case of the PLC target (e.g. Port 851) the default
        /// task is the first configured task.
        /// </para>
        /// <para>Each time this task has finished its cycle the realtime system will check for the changed value and an optional expired cycle time
        /// and sends the <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> message on change or expiry.</para>
        /// <para>The used ContextMask for the 'default' task is 0.</para>
        /// <para>
        /// <strong>Please be aware, that server side 'OnChange' notifications stress the realtime system / the default task with value comparisons.
        /// Therefore, dependent of the cycle time of the task and the capabilities of the system a higher amount of notification registrations should be handled with care !
        /// </strong>
        /// </para>
        /// <para>A system limit for server side notification registrations is 1024.</para>
        /// </summary>
        // Token: 0x040000AF RID: 175
        OnChange,
        /// <summary>
        ///   <para>
        /// The <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event is fired cyclically within the given task context.</para>
        ///   <para>A Value of parameter is interpreted as task context number <see cref="P:TwinCAT.Ads.ITcAdsSymbol2.ContextMask" />. This can be important, if
        /// the notifications have to be synchron with specific tasks, but should not be used in the default case.</para>
        ///   <para>The Notification will be registered on the ADS Server side for a cyclical trigger (dependant on time parameter) and is bound
        /// to the task specified by the ContextMask of the addressed target. In case of the PLC target (e.g. Port 851)
        /// the ContextMask is the Index of the global TASKINFOARRAY - 1.
        /// </para>
        ///   <para>Each time this task has finished its cycle the realtime system will check for the expired cycle time
        /// and sends the <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> message on expiry.
        /// </para>
        /// </summary>
        // Token: 0x040000B0 RID: 176
        CyclicInContext,
        /// <summary>
        ///   <para>
        /// The <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> event is fired when the data changes within the given task context.</para>
        ///   <para>A
        /// Value of parameter is interpreted as task context number <see cref="P:TwinCAT.Ads.ITcAdsSymbol2.ContextMask" />. This can be important, if
        /// the notifications have to be synchron with specific tasks, but should not be used in the default case.</para>
        ///   <para>The Notification will be registered on the ADS Server side for an on-change and optional cyclical trigger (dependant on parameters) and is bound
        /// to the task specified by the ContextMask of the addressed target. In case of the PLC target (e.g. Port 851)
        /// the ContextMask is the Index of the global TASKINFOARRAY - 1.
        /// Each time this task has finished its cycle the realtime system will check for the changed value and an optional expired cycle time
        /// and sends the <see cref="E:TwinCAT.Ads.TcAdsClient.AdsNotification" /> message on change or expiry.</para>
        ///   <para>
        ///     <b>
        ///       <strong>Please be aware, that server side 'OnChange' notifications stress the realtime system / the default task with value comparisons.
        /// Therefore, dependent of the cycle time of the task and the capabilities of the system only a limited set of OnChange Notifications should be used!
        /// </strong>
        ///     </b>
        ///   </para>
        /// </summary>
        // Token: 0x040000B1 RID: 177
        OnChangeInContext,
        /// <summary>
        /// The client1 req 
        /// </summary>
        /// <exclude />
        // Token: 0x040000B2 RID: 178
        [EditorBrowsable(EditorBrowsableState.Never)]
        Client1Req = 10,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically. Same as 'Cyclic'.
        /// </summary>
        /// <exclude />
        // Token: 0x040000B3 RID: 179
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC0 = 16,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000B4 RID: 180
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC1,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000B5 RID: 181
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC2,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000B6 RID: 182
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC3,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000B7 RID: 183
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC4,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000B8 RID: 184
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC5,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000B9 RID: 185
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC6,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000BA RID: 186
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC7,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000BB RID: 187
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC8,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000BC RID: 188
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC9,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000BD RID: 189
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC10,
        /// <summary>
        /// The A<see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000BE RID: 190
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC11,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000BF RID: 191
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC12,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000C0 RID: 192
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC13,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000C1 RID: 193
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC14,
        /// <summary>
        /// The <see cref="E:TwinCAT.Ads.IAdsNotifications.AdsNotification" /> event is fired cyclically.
        /// </summary>
        /// <exclude />
        // Token: 0x040000C2 RID: 194
        [EditorBrowsable(EditorBrowsableState.Never)]
        CyclicC15
    }
}
