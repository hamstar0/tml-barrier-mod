using Barriers.Entities.Barrier.PlayerBarrier;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier {
	public partial class BarrierManager {
		private IDictionary<int, PlayerBarrierEntity> PlayerBarriers = new Dictionary<int, PlayerBarrierEntity>();



		////////////////

		public PlayerBarrierEntity GetForPlayer( Player player ) {
			if( !this.PlayerBarriers.ContainsKey(player.whoAmI) ) {
				this.PlayerBarriers[ player.whoAmI ] = PlayerBarrierEntity.CreateDefaultPlayerBarrierEntity( player );
			}

			var barrier = this.PlayerBarriers[player.whoAmI];

			if( !CustomEntityManager.IsInWorld(barrier) ) {
LogHelpers.Log( "Adding barrier to world: "+barrier.ToString() );
				CustomEntityManager.AddToWorld( barrier );
			}

			return barrier;
		}


		public void Clear() {
			this.PlayerBarriers.Clear();
		}


		////////////////

		public void UpdateBarrierForPlayer( Player player, int power ) {
			var ent = this.GetForPlayer( player );
			ent.Core.Center = player.Center;

			if( (Main.netMode == 0 || Main.netMode == 1) && !Main.dedServ ) {
				ent.AdjustBarrierPower( power );
			}
		}
	}
}
