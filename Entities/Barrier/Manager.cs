using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.PlayerBarrier;
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
				foreach( var ent in CustomEntityManager.GetEntitiesByComponent<BarrierBehaviorEntityComponent>() ) {
					if( !(ent is PlayerBarrierEntity) && ent.MyOwnerPlayerWho != player.whoAmI ) {
						continue;
					}

					this.PlayerBarriers[ player.whoAmI ] = (PlayerBarrierEntity)ent;
				}

				if( Main.netMode == 2 ) {
					return null;
				} else if( !this.PlayerBarriers.ContainsKey( player.whoAmI ) ) {
					this.PlayerBarriers[player.whoAmI] = PlayerBarrierEntity.CreateDefaultPlayerBarrierEntity( player );
				}
			}

			var barrier = this.PlayerBarriers[player.whoAmI];

			if( !CustomEntityManager.IsInWorld(barrier) ) {
				CustomEntityManager.AddToWorld( barrier );
			}

			return barrier;
		}


		public void Clear() {
			this.PlayerBarriers.Clear();
		}


		////////////////

		public void UpdateBarrierForPlayer( Player player, int power ) {
			if( !Promises.IsPromiseValidated( SaveableEntityComponent.LoadAllValidator ) ) {
				return;
			}

			var ent = this.GetForPlayer( player );
			if( ent == null ) {
				if( Main.netMode != 2 ) {
					throw new HamstarException( "Missing player "+player.name+"'s barrier entity." );
				}
				return;
			}

			ent.Core.Center = player.Center;

			if( (Main.netMode == 0 || Main.netMode == 1) && !Main.dedServ ) {
				ent.AdjustBarrierPower( power );
			}
		}
	}
}
