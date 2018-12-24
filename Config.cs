using HamstarHelpers.Components.Config;
using System;


namespace Barriers {
	public class BarriersConfigData : ConfigurationDataBase {
		public readonly static string ConfigFileName = "Barriers Config.json";



		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;
		
		public int BarrierDefenseBaseAmount = 8;
		public float BarrierHardnessDamageDeflectionMaximumAmount = 10f;
		public float BarrierHardnessDamageReflectionMultiplierAmount = 2f;
		public float BarrierRegenBaseAmount = 6f / 60f;
		public float BarrierRegenMultiplier = 1f;

		public int PlayerBarrierDefaultShieldPower = 128;

		public int NpcBarrierHpBaseAmount = 128;



		////////////////

		public void SetDefaults() { }
		
		
		////////////////

		public bool UpdateToLatestVersion() {
			var mymod = BarriersMod.Instance;
			var newConfig = new BarriersConfigData();
			newConfig.SetDefaults();

			var versSince = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( versSince >= mymod.Version ) {
				return false;
			}

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();

			return true;
		}
	}
}
