using Barriers.Entities.Barrier;
using Barriers.NetProtocols;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Services.Promises;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Barriers {
	class BarriersPlayer : ModPlayer {
		public bool NoBuilding = false;
		public bool HasBarrier = false;

		////////////////

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

		public override void PostUpdate() {
			if( this.HasBarrier ) {
				this.HasBarrier = false;

				BarrierEntity ent = BarrierEntity.ApplyToPlayer( this.player );
				ent.Core.Center = this.player.Center;
			}
		}

		public override void PreUpdateBuffs() {
			if( this.NoBuilding ) {
				this.NoBuilding = false;

				this.player.AddBuff( BuffID.NoBuilding, 3, true );
				this.player.noBuilding = true;
			}
		}
	}
}
