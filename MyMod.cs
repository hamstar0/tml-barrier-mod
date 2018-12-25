using Barriers.Entities.Barrier;
using Barriers.Items;
using Barriers.UI;
using HamstarHelpers.Components.Config;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Services.Promises;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace Barriers {
	partial class BarriersMod : Mod {
		public static BarriersMod Instance { get; private set; }



		////////////////

		internal BarrierManager BarrierManager = new BarrierManager();
		internal BarrierUI BarrierUI = new BarrierUI();

		////

		public JsonConfig<BarriersConfigData> ConfigJson { get; private set; }
		public BarriersConfigData Config { get { return this.ConfigJson.Data; } }



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

			Promises.AddWorldUnloadEachPromise( () => {
				this.BarrierManager.Clear();
			} );
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.Config.SetDefaults();
				this.ConfigJson.SaveFile();
				ErrorLogger.Log( "Barriers config " + this.Version.ToString() + " created." );
			}

			if( this.Config.UpdateToLatestVersion() ) {
				ErrorLogger.Log( "Barriers updated to " + this.Version.ToString() );
				this.ConfigJson.SaveFile();
			}
		}

		public override void Unload() {
			BarriersMod.Instance = null;
		}


		public override void PostSetupContent() {
			BarrierUI.InitializeStatic( this );
		}


		////////////////

		public override object Call( params object[] args ) {
			if( args == null || args.Length == 0 ) { throw new HamstarException( "Undefined call type." ); }

			string callType = args[0] as string;
			if( callType == null ) { throw new HamstarException( "Invalid call type." ); }

			var methodInfo = typeof(BarriersAPI).GetMethod( callType );
			if( methodInfo == null ) { throw new HamstarException( "Invalid call type " + callType ); }

			var newArgs = new object[args.Length - 1];
			Array.Copy( args, 1, newArgs, 0, args.Length - 1 );

			try {
				return ReflectionHelpers.SafeCall( methodInfo, null, newArgs );
			} catch( Exception e ) {
				throw new HamstarException( "Barriers.BarrierMod.Call - Bad API call.", e );
			}
		}


		////////////////

		private int _NoInteractTimer = 0;

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( idx == -1 ) { return; }
			
			GameInterfaceDrawMethod func = delegate {
				Player player = Main.LocalPlayer;
				Item heldItem = player.HeldItem;

				if( heldItem == null || heldItem.IsAir ) {
					this._NoInteractTimer = 0;
					return true;
				}

				var paling = heldItem.modItem as PalingItem;
				if( paling != null && Main.mouseItem.type != heldItem.type ) {
					bool noInteract = !player.mouseInterface || this._NoInteractTimer > 30;

					this.BarrierUI.DrawUI( Main.spriteBatch, this.BarrierManager.GetForPlayer(player), noInteract );
					this._NoInteractTimer++;
				} else {
					this._NoInteractTimer = 0;
				}
				
				return true;
			};

			var interfaceLayer = new LegacyGameInterfaceLayer( "BetterPaint: Paint Blaster UI", func, InterfaceScaleType.UI );

			layers.Insert( idx, interfaceLayer );
		}
	}
}
