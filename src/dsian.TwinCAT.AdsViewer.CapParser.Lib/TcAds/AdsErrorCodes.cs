using System;
using System.ComponentModel;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds
{
    /// <summary>
    /// Describes the ADS error that occured.
    /// </summary>
    // Token: 0x02000083 RID: 131
    public enum AdsErrorCode : UInt32
    {
        /// <summary>
        /// No Error.
        /// <para>Error code: 0(0x000).</para>
        /// </summary>
        // Token: 0x0400014D RID: 333
        NoError,
        /// <summary>
        /// Internal Error.
        /// <para>Error code: 1(0x001).</para>
        /// </summary>
        // Token: 0x0400014E RID: 334
        InternalError,
        /// <summary>
        /// No Rtime.
        /// <para>Error code: 2(0x002).</para>
        /// </summary>
        // Token: 0x0400014F RID: 335
        NoRTime,
        /// <summary>
        /// Allocation locked memory error.
        /// <para>Error code: 3(0x003).</para>
        /// </summary>
        // Token: 0x04000150 RID: 336
        LockedMemoryError,
        /// <summary>
        /// Insert mailbox error.
        /// <para>Error code: 4(0x004).</para>
        /// </summary>
        // Token: 0x04000151 RID: 337
        MailBoxError,
        /// <summary>
        /// Wrong receive HMSG.
        /// <para>Error code: 5(0x005).</para>
        /// </summary>
        // Token: 0x04000152 RID: 338
        WrongHMsg,
        /// <summary>
        /// Target port not found.
        /// <para>Error code: 6(0x006).</para>
        /// </summary>
        // Token: 0x04000153 RID: 339
        TargetPortNotFound,
        /// <summary>
        /// Target machine not found.
        /// <para>Error code: 7(0x007).</para>
        /// </summary>
        // Token: 0x04000154 RID: 340
        TargetMachineNotFound,
        /// <summary>
        /// Unknown command ID.
        /// <para>Error code: 8(0x008).</para>
        /// </summary>
        // Token: 0x04000155 RID: 341
        UnknownCommandID,
        /// <summary>
        /// Bad task ID.
        /// <para>Error code: 9(0x009).</para>
        /// </summary>
        // Token: 0x04000156 RID: 342
        BadTaskID,
        /// <summary>
        /// No IO.
        /// <para>Error code: 10(0x00A).</para>
        /// </summary>
        // Token: 0x04000157 RID: 343
        NoIO,
        /// <summary>
        /// Unknown AMS command.
        /// <para>Error code: 11(0x00B).</para>
        /// </summary>
        // Token: 0x04000158 RID: 344
        UnknwonAmsCommand,
        /// <summary>
        /// Win 32 error.
        /// <para>Error code: 12(0x00C).</para>
        /// </summary>
        // Token: 0x04000159 RID: 345
        Win32Error,
        /// <summary>
        /// Port is not connected.
        /// <para>Error code: 13(0x00D).</para>
        /// </summary>
        // Token: 0x0400015A RID: 346
        PortNotConnected,
        /// <summary>
        /// Invalid AMS length.
        /// <para>Error code: 14(0x00E).</para>
        /// </summary>
        // Token: 0x0400015B RID: 347
        InvalidAmsLength,
        /// <summary>
        /// Invalid AMS Net ID.
        /// <para>Error code: 15(0x00F).</para>
        /// </summary>
        // Token: 0x0400015C RID: 348
        InvalidAmsNetID,
        /// <summary>
        /// Low Installation level.
        /// <para>Error code: 16(0x010).</para>
        /// </summary>
        // Token: 0x0400015D RID: 349
        LowInstallLevel,
        /// <summary>
        /// No debug available.
        /// <para>Error code: 17(0x011).</para>
        /// </summary>
        // Token: 0x0400015E RID: 350
        NoDebug,
        /// <summary>
        /// Port disabled.
        /// <para>Error code: 18(0x012).</para>
        /// </summary>
        // Token: 0x0400015F RID: 351
        PortDisabled,
        /// <summary>
        /// Port is already connected.
        /// <para>Error code: 19(0x013).</para>
        /// </summary>
        // Token: 0x04000160 RID: 352
        PortConnected,
        /// <summary>
        /// AMS Sync Win32 error.
        /// <para>Error code: 20(0x014).</para>
        /// </summary>
        // Token: 0x04000161 RID: 353
        AmsSyncWin32Error,
        /// <summary>
        /// AMS Sync timeout.
        /// <para>Error code: 21(0x015).</para>
        /// </summary>
        // Token: 0x04000162 RID: 354
        SyncTimeOut,
        /// <summary>
        /// AMS Sync AMS error
        /// <para>Error code: 22(0x016).</para>
        /// </summary>
        // Token: 0x04000163 RID: 355
        AmsSyncAmsError,
        /// <summary>
        /// AMS Sync no index map.
        /// <para>Error code: 23(0x017).</para>
        /// </summary>
        // Token: 0x04000164 RID: 356
        AmsSyncNoIndexMap,
        /// <summary>
        /// Invalid AMS port.
        /// <para>Error code: 24(0x018).</para>
        /// </summary>
        // Token: 0x04000165 RID: 357
        InvalidAmsPort,
        /// <summary>
        /// No memory.
        /// <para>Error code: 25(0x019).</para>
        /// </summary>
        // Token: 0x04000166 RID: 358
        NoMemory,
        /// <summary>
        /// TCP send error.
        /// <para>Error code: 26(0x01A).</para>
        /// </summary>
        // Token: 0x04000167 RID: 359
        TCPSendError,
        /// <summary>
        /// Host unreachable.
        /// <para>Error code: 27(0x1B).</para>
        /// </summary>
        // Token: 0x04000168 RID: 360
        HostUnreachable,
        /// <summary>
        /// Invalid AMS fragment.
        /// <para>Error code: 28(0x1C).</para>
        /// </summary>
        // Token: 0x04000169 RID: 361
        AmsInvalidFragment,
        /// <summary>
        /// Router: no locked memory.
        /// <para>Error code: 1280(0x500).</para>
        /// </summary>
        // Token: 0x0400016A RID: 362
        NoLockedMemory = 1280,
        /// <summary>
        /// Router: The size of the  router memory could not be changed.
        /// <para>Error code: 1281(0x501).</para>
        /// </summary>
        // Token: 0x0400016B RID: 363
        ResizeMemory,
        /// <summary>
        /// Router: mailbox full.
        /// <para>Error code: 1282(0x502).</para>
        /// </summary>
        // Token: 0x0400016C RID: 364
        MailboxFull,
        /// <summary>
        /// Router: The mailbox has reached the maximum number of possible messages.
        /// <para>Error code: 1283(0x503).</para>
        /// </summary>
        // Token: 0x0400016D RID: 365
        DebugBoxFull,
        /// <summary>
        /// Router: Unknown Port Type
        /// <para>Error code: 1284(0x504).</para>
        /// </summary>
        // Token: 0x0400016E RID: 366
        UnknownPortType,
        /// <summary>
        /// Router: Router is not initialized.
        /// <para>Error code: 1285(0x505).</para>
        /// </summary>
        // Token: 0x0400016F RID: 367
        RouterNotInitialized,
        /// <summary>
        /// Router: The desired port number is already assigned.
        /// <para>Error code: 1286(0x506).</para>
        /// </summary>
        // Token: 0x04000170 RID: 368
        PortAlreadyInUse,
        /// <summary>
        /// Router: Port not registered.
        /// <para>Error code: 1287(0x507).</para>
        /// </summary>
        // Token: 0x04000171 RID: 369
        PortNotRegistered,
        /// <summary>
        /// Router: The maximum number of Ports reached.
        /// <para>Error code: 1288(0x508).</para>
        /// </summary>
        // Token: 0x04000172 RID: 370
        NoMoreQueues,
        /// <summary>
        /// Router: The port is invalid.
        /// <para>Error code: 1289(0x509).</para>
        /// </summary>
        // Token: 0x04000173 RID: 371
        InvalidPort,
        /// <summary>
        /// Router:  TwinCAT Router not active.
        /// <para>Error code: 1290(0x50A).</para>
        /// </summary>
        // Token: 0x04000174 RID: 372
        RouterNotActive,
        /// <summary>
        /// error class &lt;device error"&gt;
        /// <para>Error code: 1792(0x700).</para>
        /// </summary>
        // Token: 0x04000175 RID: 373
        DeviceError = 1792,
        /// <summary>
        /// Service is not supported by server.
        /// <para>Error code: 1793(0x701).</para>
        /// </summary>
        // Token: 0x04000176 RID: 374
        DeviceServiceNotSupported,
        /// <summary>
        /// Invalid index group.
        /// <para>Error code: 1794(0x702).</para>
        /// </summary>
        // Token: 0x04000177 RID: 375
        DeviceInvalidGroup,
        /// <summary>
        /// Invalid index offset.
        /// <para>Error code: 1795(0x703).</para>
        /// </summary>
        // Token: 0x04000178 RID: 376
        DeviceInvalidOffset,
        /// <summary>
        /// Reading/writing not permitted.
        /// <para>Error code: 1796(0x704).</para>
        /// </summary>
        // Token: 0x04000179 RID: 377
        DeviceInvalidAccess,
        /// <summary>
        /// Parameter size not correct.
        /// <para>Error code: 1797(0x705).</para>
        /// </summary>
        // Token: 0x0400017A RID: 378
        DeviceInvalidSize,
        /// <summary>
        /// Invalid parameter value(s).
        /// <para>Error code: 1798(0x706).</para>
        /// </summary>
        // Token: 0x0400017B RID: 379
        DeviceInvalidData,
        /// <summary>
        /// Device is not in a ready state.
        /// <para>Error code: 1799(0x707).</para>
        /// </summary>
        // Token: 0x0400017C RID: 380
        DeviceNotReady,
        /// <summary>
        /// Device is busy.
        /// <para>Error code: 1800(0x708).</para>
        /// </summary>
        // Token: 0x0400017D RID: 381
        DeviceBusy,
        /// <summary>
        /// Invalid context (must be in Windows).
        /// <para>Error code: 1801(0x709).</para>
        /// </summary>
        // Token: 0x0400017E RID: 382
        DeviceInvalidContext,
        /// <summary>
        /// Out of memory.
        /// <para>Error code: 1802(0x70a).</para>
        /// </summary>
        // Token: 0x0400017F RID: 383
        DeviceNoMemory,
        /// <summary>
        /// Invalid parameter value(s).
        /// <para>Error code: 1803(0x70b).</para>
        /// </summary>
        // Token: 0x04000180 RID: 384
        DeviceInvalidParam,
        /// <summary>
        /// Obsolete
        /// <para>Error code: 1803(0x70b).</para>
        /// </summary>
        /// <exclude />
        // Token: 0x04000181 RID: 385
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use 'DeviceInvalidParam instead!")]
        DeviceInavlidParam = 1803,
        /// <summary>
        /// Not found(files, ...).
        /// <para>Error code: 1804(0x70c).</para>
        /// </summary>
        // Token: 0x04000182 RID: 386
        DeviceNotFound,
        /// <summary>
        /// Syntax error in command or file.
        /// <para>Error code: 1805(0x70d).</para>
        /// </summary>
        // Token: 0x04000183 RID: 387
        DeviceSyntaxError,
        /// <summary>
        /// Objects do not match.
        /// <para>Error code: 1806(0x70e).</para>
        /// </summary>
        // Token: 0x04000184 RID: 388
        DeviceIncompatible,
        /// <summary>
        /// Object already exists.
        /// <para>Error code: 1807(0x70f).</para>
        /// </summary>
        // Token: 0x04000185 RID: 389
        DeviceExists,
        /// <summary>
        /// Symbol not found.
        /// <para>Error code: 1808(0x7010).</para>
        /// </summary>
        // Token: 0x04000186 RID: 390
        DeviceSymbolNotFound,
        /// <summary>
        /// Symbol version is invalid.
        /// <para>Error code: 1809(0x711).</para>
        /// </summary>
        // Token: 0x04000187 RID: 391
        DeviceSymbolVersionInvalid,
        /// <summary>
        /// Server is not i a valid state.
        /// <para>Error code: 1810(0x712).</para>
        /// </summary>
        // Token: 0x04000188 RID: 392
        DeviceInvalidState,
        /// <summary>
        /// ADS transmode is not supported.
        /// <para>Error code: 1811(0x713).</para>
        /// </summary>
        // Token: 0x04000189 RID: 393
        DeviceTransModeNotSupported,
        /// <summary>
        /// Notification handle is invalid.
        /// <para>Error code: 1812(0x714).</para>
        /// </summary>
        // Token: 0x0400018A RID: 394
        DeviceNotifyHandleInvalid,
        /// <summary>
        /// Notification vlient not registered.
        /// <para>Error code: 1813(0x715).</para>
        /// </summary>
        // Token: 0x0400018B RID: 395
        DeviceClientUnknown,
        /// <summary>
        /// No more notification handles.
        /// <para>Error code: 1814(0x716).</para>
        /// </summary>
        // Token: 0x0400018C RID: 396
        DeviceNoMoreHandles,
        /// <summary>
        /// Size for watch to big.
        /// <para>Error code: 1815(0x717).</para>
        /// </summary>
        // Token: 0x0400018D RID: 397
        DeviceInvalidWatchsize,
        /// <summary>
        /// Device is not initialized.
        /// <para>Errocr code: 1818(0x718).</para>
        /// </summary>
        // Token: 0x0400018E RID: 398
        DeviceNotInitialized,
        /// <summary>
        /// Devicee has a timeout.
        /// <para>Error code: 1817(0x719).</para>
        /// </summary>
        // Token: 0x0400018F RID: 399
        DeviceTimeOut,
        /// <summary>
        /// Query interface has failed.
        /// <para>Error code: 1818(0x71A).</para>
        /// </summary>
        // Token: 0x04000190 RID: 400
        DeviceNoInterface,
        /// <summary>
        /// Wrong interface required.
        /// <para>Error code: 1819(0x71B).</para>
        /// </summary>
        // Token: 0x04000191 RID: 401
        DeviceInvalidInterface,
        /// <summary>
        /// Class ID is invalid.
        /// <para>Error code: 1820(0x71C).</para>
        /// </summary>
        // Token: 0x04000192 RID: 402
        DeviceInvalidCLSID,
        /// <summary>
        /// Object ID is invalid.
        /// <para>Error code: 1821(0x71D).</para>
        /// </summary>
        // Token: 0x04000193 RID: 403
        DeviceInvalidObjectID,
        /// <summary>
        /// Device: Request is Pending.
        /// <para>Error code: 1822(0x71E).</para>
        /// </summary>
        // Token: 0x04000194 RID: 404
        DeviceRequestIsPending,
        /// <summary>
        /// Device: Request is Aborted.
        /// <para>Error code: 1823(0x71F).</para>
        /// </summary>
        // Token: 0x04000195 RID: 405
        DeviceRequestIsAborted,
        /// <summary>
        /// Device: Signal warning.
        /// <para>Error code: 1824(0x720).</para>
        /// </summary>
        // Token: 0x04000196 RID: 406
        DeviceSignalWarning,
        /// <summary>
        /// Device: Invalid Array Index (ADSERR_DEVICE_INVALIDARRAYIDX)
        /// <para>Error code: 1825(0x721).</para>
        /// </summary>
        // Token: 0x04000197 RID: 407
        DeviceInvalidArrayIndex,
        /// <summary>
        /// Device: Symbol not Active
        /// <para>Error code: 1826(0x722).</para>
        /// </summary>
        // Token: 0x04000198 RID: 408
        DeviceSymbolNotActive,
        /// <summary>
        /// Device: Access denied.
        /// <para>Error code: 1827(0x723).</para>
        /// </summary>
        // Token: 0x04000199 RID: 409
        DeviceAccessDenied,
        /// <summary>
        /// Device: Missing license
        /// <para>Error code: 1828(0x724).</para>
        /// </summary>
        // Token: 0x0400019A RID: 410
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use 'DeviceLicenseNotFound instead!", false)]
        DeviceMissingLicense,
        /// <summary>
        /// Device: license not found
        /// <para>Error code: 1828(0x724).</para>
        /// </summary>
        // Token: 0x0400019B RID: 411
        DeviceLicenseNotFound = 1828,
        /// <summary>
        /// Device: license expired
        /// <para>Error code: 1829(0x725).</para>
        /// </summary>
        // Token: 0x0400019C RID: 412
        DeviceLicenseExpired,
        /// <summary>
        /// Device: license exceeded
        /// <para>Error code: 1830(0x726).</para>
        /// </summary>
        // Token: 0x0400019D RID: 413
        DeviceLicenseExceeded,
        /// <summary>
        /// Device: license invalid 
        /// <para>Error code: 1831(0x727).</para>
        /// </summary>
        // Token: 0x0400019E RID: 414
        DeviceLicenseInvalid,
        /// <summary>
        /// Device: license invalid system id
        /// <para>Error code: 1832(0x728).</para>
        /// </summary>
        // Token: 0x0400019F RID: 415
        DeviceLicenseSystemId,
        /// <summary>
        /// Device: license not time limited
        /// <para>Error code: 1833(0x729).</para>
        /// </summary>
        // Token: 0x040001A0 RID: 416
        DeviceLicenseNoTimeLimit,
        /// <summary>
        /// Device: license issue time in the future
        /// <para>Error code: 1834(0x72A).</para>
        /// </summary>
        // Token: 0x040001A1 RID: 417
        DeviceLicenseFutureIssue,
        /// <summary>
        /// Device: license time period to long
        /// <para>Error code: 1835(0x72B).</para>
        /// </summary>
        // Token: 0x040001A2 RID: 418
        DeviceLicenseTimeToLong,
        /// <summary>
        /// Device: Exception occured during system start.
        /// <para>Error code: 1836(0x72C).</para>
        /// </summary>
        // Token: 0x040001A3 RID: 419
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use 'DeviceException instead!", false)]
        DeviceExceptionDuringStartup,
        /// <summary>
        /// Device: Exception in device specific code
        /// <para>Error code: 1836(0x72C).</para>
        /// </summary>
        // Token: 0x040001A4 RID: 420
        DeviceException = 1836,
        /// <summary>
        /// Device: license file read twice
        /// <para>Error code: 1837(0x72D).</para>
        /// </summary>
        // Token: 0x040001A5 RID: 421
        DeviceLicenseDuplicated,
        /// <summary>
        /// Device:  invalid signature
        /// <para>Error code: 1838(0x72E).</para>
        /// </summary>
        // Token: 0x040001A6 RID: 422
        DeviceSignatureInvalid,
        /// <summary>
        /// Device: public key certificate
        /// <para>Error code: 1839(0x72F).</para>
        /// </summary>
        // Token: 0x040001A7 RID: 423
        DeviceCertificateInvalid,
        /// <summary>
        /// Device: public key of OEM unknown
        /// <para>Error code: 1840(0x730).</para>
        /// </summary>
        // Token: 0x040001A8 RID: 424
        DeviceLicenseOemNotFound,
        /// <summary>
        /// Device: license not valid for this system id type
        /// <para>Error code: 1841(0x731).</para>
        /// </summary>
        // Token: 0x040001A9 RID: 425
        DeviceLicenseRestricted,
        /// <summary>
        /// Device: trial license denied
        /// <para>Error code: 1842(0x732).</para>
        /// </summary>
        // Token: 0x040001AA RID: 426
        DeviceLicenseDemoDenied,
        /// <summary>
        /// Device: function id is invalid
        /// <para>Error code: 1843(0x733).</para>
        /// </summary>
        // Token: 0x040001AB RID: 427
        DeviceInvalidFncId,
        /// <summary>
        /// Device:  a parameter, an index, an iterator, ... is out of range
        /// <para>Error code: 1844(0x734).</para>
        /// </summary>
        // Token: 0x040001AC RID: 428
        DeviceOutOfRange,
        /// <summary>
        /// Device: invalid alignment
        /// <para>Error code: 1845(0x735).</para>
        /// </summary>
        // Token: 0x040001AD RID: 429
        DeviceInvalidAlignment,
        /// <summary>
        /// Device: license invalid platform level
        /// <para>Error code: 1846(0x736).</para>
        /// </summary>
        // Token: 0x040001AE RID: 430
        DeviceLicensePlatform,
        /// <summary>
        /// Error class &lt;client error&gt;
        /// <para>Error code: 1856(0x740).</para>
        /// </summary>
        // Token: 0x040001AF RID: 431
        ClientError = 1856,
        /// <summary>
        /// Parameter at service is invalid.
        /// <para></para>Error code: 1857(0x741).
        /// </summary>
        // Token: 0x040001B0 RID: 432
        ClientInvalidParameter,
        /// <summary>
        /// Polling list is empty.
        /// <para>Error code: 1858(0x742).</para>
        /// </summary>
        // Token: 0x040001B1 RID: 433
        ClientListEmpty,
        /// <summary>
        /// Obsolete
        /// </summary>
        /// <exclude />
        // Token: 0x040001B2 RID: 434
        [Obsolete("Use ClientVariableInUse instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        ClientVaraiableInUse,
        /// <summary>
        /// Variable connection is already in use.
        /// <para>Error code: 1859(0x743).</para>
        /// </summary>
        // Token: 0x040001B3 RID: 435
        ClientVariableInUse = 1859,
        /// <summary>
        /// Invoke ID already in use.
        /// <para>Error code: 1860(0x744).</para>
        /// </summary>
        // Token: 0x040001B4 RID: 436
        ClientDuplicateInvokeID,
        /// <summary>
        /// Timeout has elapsed.
        /// <para>Error code: 1861(x745).</para>
        /// </summary>
        // Token: 0x040001B5 RID: 437
        ClientSyncTimeOut,
        /// <summary>
        /// Error in win32 subsystem.
        /// <para>Error code: 1862(0x746).</para>
        /// </summary>
        // Token: 0x040001B6 RID: 438
        ClientW32OR,
        /// <summary>
        /// Timeout value is invalid.
        /// <para>Error code: 1863(0x747).</para>
        /// </summary>
        // Token: 0x040001B7 RID: 439
        ClientTimeoutInvalid,
        /// <summary>
        /// ADS port is not opened.
        /// <para>Error code: 1864(0x748).</para>
        /// </summary>
        // Token: 0x040001B8 RID: 440
        ClientPortNotOpen,
        /// <summary>
        /// No AMS Address.
        /// <para>Error code: 1865(0x749).</para>
        /// </summary>
        // Token: 0x040001B9 RID: 441
        ClientNoAmsAddr,
        /// <summary>
        /// An internal in ADS sync has occurred.
        /// <para>Error code: 1872(0x750).</para>
        /// </summary>
        // Token: 0x040001BA RID: 442
        ClientSyncInternal = 1872,
        /// <summary>
        /// Hash table overflow.
        /// <para>Error code: 1873(0x751).</para>
        /// </summary>
        // Token: 0x040001BB RID: 443
        ClientAddHash,
        /// <summary>
        /// There are no more symbols in the hash table.
        /// <para>Error code: 1874(0x752).</para>
        /// </summary>
        // Token: 0x040001BC RID: 444
        ClientRemoveHash,
        /// <summary>
        /// There are no more symbols in cache.
        /// <para>Error code: 1875(0x753).</para>
        /// </summary>
        // Token: 0x040001BD RID: 445
        ClientNoMoreSymbols,
        /// <summary>
        /// An invalid response has been received.
        /// <para>Error code: 1876(0x754).</para>
        /// </summary>
        // Token: 0x040001BE RID: 446
        ClientSyncResInvalid,
        /// <summary>
        /// Sync port is locked.
        /// <para>Error code: 1877(0x755).</para>
        /// </summary>
        // Token: 0x040001BF RID: 447
        ClientSyncPortLocked,
        /// <summary>
        /// Client queue is full
        /// </summary>
        // Token: 0x040001C0 RID: 448
        ClientQueueFull = 32768,
        /// <summary>
        /// Windows sockets connection refused (0x274d, 10061)
        /// </summary>
        /// <remarks>
        /// No connection could be made because the target computer actively refused it.
        /// This usually results from trying to connect to a service that is inactive on
        /// the foreign host—that is, one with no server application running.
        /// </remarks>
        // Token: 0x040001C1 RID: 449
        WSA_ConnRefused = 10061
    }
}

