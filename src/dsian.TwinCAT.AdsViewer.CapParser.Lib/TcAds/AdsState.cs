using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Lib.TcAds
{
	/// <summary>
	/// Describes the AdsState.
	/// </summary>
	// Token: 0x02000080 RID: 128
	public enum AdsState : short
	{
		/// <summary>
		///             Invalid
		/// </summary>
		// Token: 0x04000110 RID: 272
		Invalid,
		/// <summary>
		/// Idle
		/// </summary>
		// Token: 0x04000111 RID: 273
		Idle,
		/// <summary>
		/// Reset
		/// </summary>
		// Token: 0x04000112 RID: 274
		Reset,
		/// <summary>
		/// Initialize
		/// </summary>
		// Token: 0x04000113 RID: 275
		Init,
		/// <summary>
		/// Start
		/// </summary>
		// Token: 0x04000114 RID: 276
		Start,
		/// <summary>
		/// Run
		/// </summary>
		// Token: 0x04000115 RID: 277
		Run,
		/// <summary>
		/// Stop
		/// </summary>
		// Token: 0x04000116 RID: 278
		Stop,
		/// <summary>
		/// Save Configuration
		/// </summary>
		// Token: 0x04000117 RID: 279
		SaveConfig,
		/// <summary>
		/// Load Configuration
		/// </summary>
		// Token: 0x04000118 RID: 280
		LoadConfig,
		/// <summary>
		/// Power failure
		/// </summary>
		// Token: 0x04000119 RID: 281
		PowerFailure,
		/// <summary>
		/// Power Good
		/// </summary>
		// Token: 0x0400011A RID: 282
		PowerGood,
		/// <summary>
		/// Error
		/// </summary>
		// Token: 0x0400011B RID: 283
		Error,
		/// <summary>
		/// Shutdown
		/// </summary>
		// Token: 0x0400011C RID: 284
		Shutdown,
		/// <summary>
		/// Suspend
		/// </summary>
		// Token: 0x0400011D RID: 285
		Suspend,
		/// <summary>
		/// Resume
		/// </summary>
		// Token: 0x0400011E RID: 286
		Resume,
		/// <summary>
		/// Config (System is in config mode)
		/// </summary>
		// Token: 0x0400011F RID: 287
		Config,
		/// <summary>
		/// Reconfig (System should restart in config mode)
		/// </summary>
		// Token: 0x04000120 RID: 288
		Reconfig,
		/// <summary>
		/// Stopping
		/// </summary>
		// Token: 0x04000121 RID: 289
		Stopping,
		/// <summary>
		/// Incompatible
		/// </summary>
		// Token: 0x04000122 RID: 290
		Incompatible,
		/// <summary>
		/// Exception
		/// </summary>
		// Token: 0x04000123 RID: 291
		Exception,
		/// <summary>
		/// Maxstates (no valid state)
		/// </summary>
		/// <exclude />
		// Token: 0x04000124 RID: 292
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Maxstates = 17
	}
}
