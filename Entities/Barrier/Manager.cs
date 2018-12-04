using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier {
	partial class BarrierManager {
		private IDictionary<int, BarrierEntity> PlayerBarriers = new Dictionary<int, BarrierEntity>();



		////////////////

		public BarrierEntity ApplyToPlayer( Player player ) {
			if( !this.PlayerBarriers.ContainsKey(player.whoAmI) ) {
				this.PlayerBarriers[player.whoAmI] = BarrierEntity.CreateBarrierEntity( new BarrierTypes[] { BarrierTypes.Green }, 64, player.Center );

				CustomEntityManager.AddToWorld( this.PlayerBarriers[ player.whoAmI ] );
			}
			return this.PlayerBarriers[ player.whoAmI ];
		}
	}
}
