﻿using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.NPCHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierHitRadiusNpcEntityComponent : HitRadiusNpcEntityComponent {
		private class BarrierHitRadiusNpcEntityComponentFactory : CustomEntityComponentFactory<BarrierHitRadiusNpcEntityComponent> {
			public bool HitsFriendly;


			public BarrierHitRadiusNpcEntityComponentFactory( bool hitsFriendly ) {
				this.HitsFriendly = hitsFriendly;
			}
			
			protected override void InitializeComponent( BarrierHitRadiusNpcEntityComponent data ) {
				data.HitsFriendly = this.HitsFriendly;
			}
		}



		////////////////

		public static BarrierHitRadiusNpcEntityComponent CreateBarrierHitRadiusNpcEntityComponent( bool hitsFriendly ) {
			var factory = new BarrierHitRadiusNpcEntityComponentFactory( hitsFriendly );
			return factory.Create();
		}



		////////////////

		public bool HitsFriendly;



		////////////////

		protected BarrierHitRadiusNpcEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		
		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierBehaviorEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, NPC npc, ref int damage ) {
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierBehaviorEntityComponent>();

			return this.HitsFriendly == npc.friendly && behavComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, NPC npc, int damage ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierBehaviorEntityComponent>();

			float oldHp = behavComp.Hp;
			int defDamage = Math.Max( 0, damage - behavComp.Defense );
			float radDamage = defDamage * ( 1f - behavComp.ShrinkResistScale );

			if( defDamage > ( mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount * behavComp.ShrinkResistScale ) ) {
				behavComp.Hp = behavComp.Hp > defDamage ? behavComp.Hp - defDamage : 0;
				behavComp.Radius = behavComp.Radius > radDamage ? behavComp.Radius - radDamage : 0;
			}

			float npcDamage = damage;
			npcDamage += damage * behavComp.ShrinkResistScale * mymod.Config.BarrierHardnessDamageReflectionMultiplierAmount;
			
			if( npcDamage > 0 ) {
				NPCHelpers.Hurt( npc, (int)npcDamage );

				npc.velocity += Vector2.Normalize( npc.position - ent.Core.position ) * (npcDamage / 20);

				myent.EmitImpactFx( npc.Center, npc.width, npc.height, npcDamage );
			}

			//if( mymod.Config.DebugModeInfo ) {
			//	DebugHelpers.Print( "barrier hurts "+npc.TypeName+" ("+npc.whoAmI+")", "dmg:"+damage+", -hp:"+(oldHp-behavComp.Hp)+", -rad:"+radDamage+", npc hit:"+npcDamage, 20 );
			//}
		}
	}
}
