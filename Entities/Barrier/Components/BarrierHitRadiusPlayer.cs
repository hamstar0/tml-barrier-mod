﻿using HamstarHelpers.Components.CustomEntity;
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
		private class BarrierHitRadiusPlayerEntityComponentFactory : CustomEntityComponentFactory<BarrierHitRadiusPlayerEntityComponent> {
			protected override void InitializeComponent( BarrierHitRadiusPlayerEntityComponent data ) { }
		}



		////////////////

		public static BarrierHitRadiusPlayerEntityComponent CreateBarrierHitRadiusPlayerEntityComponent() {
			var factory = new BarrierHitRadiusPlayerEntityComponentFactory();
			return factory.Create();
		}



		////////////////

		protected BarrierHitRadiusPlayerEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void OnInitialize() { }


		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, Player plr, ref int dmg ) {
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			dmg = (int)Math.Min( plr.statLife, behavComp.Hp );

			return behavComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, Player plr, int dmg ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();
			
			float oldHp = behavComp.Hp;
			if( !behavComp.HitByPlayer( ent, plr, ref dmg ) ) {
				return;
			}

			int plrDmg = dmg + behavComp.Defense;
			plrDmg = Math.Min( plrDmg, plr.statLife );
			
			if( plrDmg > 0 ) {
				PlayerHelpers.RawHurt( plr, PlayerDeathReason.ByCustomReason(plr.name+" forgot to knock first"), (int)plrDmg, 0 );

				plr.velocity += Vector2.Normalize( plr.position - ent.Core.position ) * (plrDmg / 40);

				myent.EmitImpactFx( plr.Center, plr.width, plr.height, plrDmg );
			}

			if( mymod.Config.DebugModeStatsInfo ) {
				DebugHelpers.Print( "barrier hurts " + plr.name + " (" + plr.whoAmI + ")", "dmg:" + dmg + ", -hp:" + (oldHp - behavComp.Hp) + ", plrDmg:" + plrDmg, 20 );
			}
		}
	}
}
