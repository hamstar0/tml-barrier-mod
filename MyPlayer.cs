using Barriers.Items;
using Barriers.NetProtocols;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.PlayerHelpers;
using HamstarHelpers.Services.Promises;
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

		private void OnConnectHost() {
			Promises.AddValidatedPromise( SaveableEntityComponent.LoadAllValidator, () => {
				return false;
			} );
		}

		////////////////

		private void OnConnectSingle() {
			this.OnConnectHost();
		}

		private void OnConnectClient() {
			PacketProtocolRequestToServer.QuickRequest<ModSettingsProtocol>();
		}

		private void OnConnectServer() {
			this.OnConnectHost();
		}


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			Item heldItem = this.player.HeldItem;

			if( heldItem != null && !heldItem.IsAir ) {
				var paling = heldItem.modItem as PalingItem;

				if( paling != null ) {
					this.player.mouseInterface = true;
				}
			}
		}

		public override void PostUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			var mymod = (BarriersMod)this.mod;
			int palingType = mymod.ItemType<PalingItem>();
			int barrierPower = 0;

			for( int i=PlayerItemHelpers.VanillaAccessorySlotFirst; PlayerItemHelpers.IsAccessorySlot(this.player, i); i++ ) {
				Item acc = this.player.armor[i];
				if( acc == null || !acc.active || acc.type != palingType ) { continue; }

				barrierPower = 256;	//TODO
				break;
			}

			mymod.BarrierManager.UpdatePalingForPlayer( this.player, barrierPower );
		}
	}
}
