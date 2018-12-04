using Barriers.Entities.Barrier;
using HamstarHelpers.Components.Config;
using System;
using Terraria.ModLoader;


namespace Barriers {
	partial class BarriersMod : Mod {
		public static BarriersMod Instance { get; private set; }



		////////////////

		public JsonConfig<BarriersConfigData> ConfigJson { get; private set; }
		public BarriersConfigData Config { get { return this.ConfigJson.Data; } }

		public BarrierManager Manager = new BarrierManager();



		////////////////

		public BarriersMod() {
			this.ConfigJson = new JsonConfig<BarriersConfigData>(
				BarriersConfigData.ConfigFileName,
				ConfigurationDataBase.RelativePath,
				new BarriersConfigData()
			);
		}

		////////////////

		public override void Load() {
			BarriersMod.Instance = this;

			this.LoadConfig();
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.Config.SetDefaults();
				this.ConfigJson.SaveFile();
				ErrorLogger.Log( "Evil Barriers config " + this.Version.ToString() + " created." );
			}

			if( this.Config.UpdateToLatestVersion() ) {
				ErrorLogger.Log( "Evil Barriers updated to " + this.Version.ToString() );
				this.ConfigJson.SaveFile();
			}
		}

		public override void Unload() {
			BarriersMod.Instance = null;
		}


		////////////////
		
		public override object Call( params object[] args ) {
			if( args.Length == 0 ) { throw new Exception( "Undefined call type." ); }

			string callType = args[0] as string;
			if( args == null ) { throw new Exception( "Invalid call type." ); }

			var newArgs = new object[args.Length - 1];
			Array.Copy( args, 1, newArgs, 0, args.Length - 1 );

			return BarriersAPI.Call( callType, newArgs );
		}
	}
}
