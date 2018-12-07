using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier {
	partial class BarrierManager {
		private IDictionary<int, BarrierEntity> PlayerBarriers = new Dictionary<int, BarrierEntity>();



		////////////////

		public BarrierEntity GetForPlayer( Player player ) {
			if( !this.PlayerBarriers.ContainsKey(player.whoAmI) ) {
				this.PlayerBarriers[ player.whoAmI ] = BarrierEntity.CreateDefaultBarrierEntity();

				CustomEntityManager.AddToWorld( this.PlayerBarriers[ player.whoAmI ] );
			}
			return this.PlayerBarriers[ player.whoAmI ];
		}


		////////////////

		public void UpdatePalingForPlayer( Player player, int totalPower ) {
			if( !this.PlayerBarriers.ContainsKey( player.whoAmI ) ) {
				return;
			}

			var ent = this.GetForPlayer( player );
			ent.TotalPower = totalPower;

			ent.UpdateForPlayer( player );
		}
	}
}
