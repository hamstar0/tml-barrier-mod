using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.PlayerHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;


namespace Barriers.Entities.Barrier.Components {
	class BarrierHitRadiusPlayerEntityComponent : HitRadiusPlayerEntityComponent {
		protected BarrierHitRadiusPlayerEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void OnInitialize() { }

		////

		protected override Type GetMyFactoryType() {
			return null;
		}


		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierStatsEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, Player plr, ref int plrDmg ) {
			var myent = (BarrierEntity)ent;
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();

			plrDmg = (int)Math.Min( plr.statLife, statsComp.Hp );

			return statsComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, Player plr, int dmg ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();
			
			float oldHp = statsComp.Hp;
			if( !statsComp.HitByPlayer( ent, plr, ref dmg ) ) {
				return;
			}
			
			if( dmg > 0 ) {
				PlayerHelpers.RawHurt( plr, PlayerDeathReason.ByCustomReason(plr.name+" forgot to knock first"), (int)dmg, 0 );

				plr.velocity += Vector2.Normalize( plr.position - ent.Core.position ) * (dmg / 40);

				myent.EmitImpactFx( plr.Center, plr.width, plr.height, dmg );
			}

			if( mymod.Config.DebugModeStatsInfo ) {
				DebugHelpers.Print( "barrier hurts " + plr.name + " (" + plr.whoAmI + ")", "dmg:" + dmg + ", -hp:" + (oldHp - statsComp.Hp) + ", plrDmg:" + dmg, 20 );
			}
		}
	}
}
