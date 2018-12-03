using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier {
	partial class BarrierEntity : CustomEntity {
		public static BarrierEntity ApplyToPlayer( Player player ) {
			if( !BarrierEntity.PlayerBarriers.ContainsKey(player.whoAmI) ) {
				var factory = new BarrierEntityFactory<BarrierEntity>( new BarrierTypes[] { BarrierTypes.Green }, 64, player.Center );
				BarrierEntity.PlayerBarriers[player.whoAmI] = factory.Create();

				CustomEntityManager.AddToWorld( BarrierEntity.PlayerBarriers[ player.whoAmI ] );
			}
			return BarrierEntity.PlayerBarriers[ player.whoAmI ];
		}
	}
}
