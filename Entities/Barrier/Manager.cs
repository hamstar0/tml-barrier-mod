using Barriers.Entities.Barrier.PlayerBarrier;
using Barriers.Entities.Barrier.PlayerBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Services.Promises;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier {
	public partial class BarrierManager {
		private IDictionary<int, PlayerBarrierEntity> PlayerBarriers = new Dictionary<int, PlayerBarrierEntity>();



		////////////////

		public PlayerBarrierEntity GetForPlayer( Player player ) {
			if( !this.PlayerBarriers.ContainsKey(player.whoAmI) ) {
				foreach( var ent in CustomEntityManager.GetEntitiesByComponent<PlayerBarrierBehaviorEntityComponent>() ) {
					if( ent.MyOwnerPlayerWho != player.whoAmI ) { continue; }
					this.PlayerBarriers[ player.whoAmI ] = (PlayerBarrierEntity)ent;
				}

				if( Main.netMode == 2 ) {
					return null;
				} else if( !this.PlayerBarriers.ContainsKey( player.whoAmI ) ) {
					this.PlayerBarriers[player.whoAmI] = PlayerBarrierEntity.CreateDefaultPlayerBarrierEntity( player );
				}
			}

			var barrier = this.PlayerBarriers[ player.whoAmI ];

			if( !CustomEntityManager.IsInWorld(barrier) ) {
				CustomEntityManager.AddToWorld( barrier );
			}

			return barrier;
		}


		public void Clear() {
			this.PlayerBarriers.Clear();
		}


		////////////////

		public void UpdateBarrierForPlayer( Player barrierOwnerPlayer, int power ) {
			if( !Promises.IsPromiseValidated( SaveableEntityComponent.LoadAllValidator ) ) {
				return;
			}

			var ent = this.GetForPlayer( barrierOwnerPlayer );
			if( ent == null ) {
				if( Main.netMode != 2 ) {
					throw new HamstarException( "Missing player "+barrierOwnerPlayer.name+"'s barrier entity." );
				}
				return;
			}

			ent.Core.Center = barrierOwnerPlayer.Center;
			ent.SetBarrierPower( power, Main.dedServ || (barrierOwnerPlayer.whoAmI != Main.myPlayer) );
		}
	}
}
