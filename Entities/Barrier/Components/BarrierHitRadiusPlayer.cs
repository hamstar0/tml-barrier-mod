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

		
		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, Player player, ref int damage ) {
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			damage = (int)Math.Min( behavComp.Hp, player.statLife );

			return behavComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, Player player, int damage ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			float oldHp = behavComp.Hp;
			int defDamage = Math.Max( 0, damage - behavComp.Defense );
			float radDamage = defDamage * ( 1f - behavComp.ShrinkResistScale );

			if( defDamage > ( mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount * behavComp.ShrinkResistScale ) ) {
				behavComp.Hp = Math.Max( behavComp.Hp - defDamage, 0 );
				behavComp.Radius = Math.Max( behavComp.Radius - radDamage, 0 );
			}

			float plrDamage = damage;
			plrDamage += damage * behavComp.ShrinkResistScale * mymod.Config.BarrierHardnessDamageReflectionMultiplierAmount;
			plrDamage = Math.Min( plrDamage, player.statLife );
			
			if( plrDamage > 0 ) {
				PlayerHelpers.RawHurt( player, PlayerDeathReason.ByCustomReason(" forgot to knock first"), (int)plrDamage, 0 );

				player.velocity += Vector2.Normalize( player.position - ent.Core.position ) * (plrDamage / 40);

				myent.EmitImpactFx( player.Center, player.width, player.height, plrDamage );
			}

			//if( mymod.Config.DebugModeInfo ) {
			//	DebugHelpers.Print( "barrier hurts "+npc.TypeName+" ("+npc.whoAmI+")", "dmg:"+damage+", -hp:"+(oldHp-behavComp.Hp)+", -rad:"+radDamage+", npc hit:"+npcDamage, 20 );
			//}
		}
	}
}
