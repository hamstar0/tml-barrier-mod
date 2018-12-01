using HamstarHelpers.Components.Config;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace Barriers {
	partial class BarriersMod : Mod {
		public static string GithubUserName { get { return "hamstar0"; } }
		public static string GithubProjectName { get { return "tml-barriers-mod"; } }

		public static string ConfigFileRelativePath {
			get { return ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + BarriersConfigData.ConfigFileName; }
		}
		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( !BarriersMod.Instance.ConfigJson.LoadFile() ) {
				BarriersMod.Instance.ConfigJson.SaveFile();
			}
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var config_data = new BarriersConfigData();
			config_data.SetDefaults();

			BarriersMod.Instance.ConfigJson.SetData( config_data );
			BarriersMod.Instance.ConfigJson.SaveFile();
		}
	}
}
