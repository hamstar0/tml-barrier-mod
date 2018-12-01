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

		////////////////

		public override bool CloneNewInstances => false;



		////////////////
		
		public override void SyncPlayer( int to_who, int from_who, bool new_player ) {
			if( Main.netMode == 2 ) {
				if( to_who == -1 && from_who == this.player.whoAmI ) {
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

		public override void PreUpdateBuffs() {
			if( this.NoBuilding ) {
				this.NoBuilding = false;

				this.player.AddBuff( BuffID.NoBuilding, 3, true );
				this.player.noBuilding = true;
			}
		}
	}
}
