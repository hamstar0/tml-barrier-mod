﻿using Barriers.Items;
using Barriers.NetProtocols;
using HamstarHelpers.Components.Config;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Helpers.PlayerHelpers;
using Terraria;
using Terraria.ModLoader;


namespace Barriers {
	class BarriersPlayer : ModPlayer {
		public override bool CloneNewInstances => false;



		////////////////
		
		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					this.OnConnectServer();
				}
			}
		}

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			if( Main.netMode == 0 ) {
				this.OnConnectSingle();
			} else if( Main.netMode == 1 ) {
				this.OnConnectClient();
			}
		}


		////////////////

		private void OnConnectAny() {	// TODO: Add Mod Helpers API for config options
			var helperMod = ModLoader.GetMod( "HamstarHelpers" );
			ConfigurationDataBase config;

			if( ReflectionHelpers.GetProperty<ConfigurationDataBase>( helperMod, "Config", out config ) ) {
				ReflectionHelpers.SetField( config, "MagiTechScrapMechBossDropsEnabled", true );
			}
		}

		private void OnConnectHost() {
			/*Promises.AddValidatedPromise( SaveableEntityComponent.LoadAllValidator, () => {
				return false;
			} );*/
		}

		////////////////

		private void OnConnectSingle() {
			this.OnConnectHost();
			this.OnConnectAny();
		}

		private void OnConnectClient() {
			PacketProtocolRequestToServer.QuickRequest<ModSettingsProtocol>();
			this.OnConnectAny();
		}

		private void OnConnectServer() {
			this.OnConnectHost();
			this.OnConnectAny();
		}


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			Item heldItem = this.player.HeldItem;

			if( heldItem != null && !heldItem.IsAir ) {
				var paling = heldItem.modItem as PalingItem;

				if( paling != null && Main.mouseItem.type != heldItem.type ) {
					this.player.mouseInterface = true;
				}
			}
		}

		public override void PostUpdate() {
			bool isMe = this.player.whoAmI == Main.myPlayer;

			var mymod = (BarriersMod)this.mod;
			int palingType = mymod.ItemType<PalingItem>();
			int barrierPower = 0;

			for( int i=PlayerItemHelpers.VanillaAccessorySlotFirst; PlayerItemHelpers.IsAccessorySlot(this.player, i); i++ ) {
				Item acc = this.player.armor[i];
				if( acc == null || !acc.active || acc.type != palingType ) { continue; }

				var myBarrier = (PalingItem)acc.modItem;

				barrierPower = myBarrier.GetPower();
				break;
			}
			
			mymod.BarrierManager.UpdateBarrierForPlayer( this.player, barrierPower );

			if( isMe && mymod.Config.DebugModeStatsInfo ) {
				DebugHelpers.Print( "Power for " + this.player.name, "" + barrierPower, 20 );
			}
		}
	}
}
