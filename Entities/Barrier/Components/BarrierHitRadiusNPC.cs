using HamstarHelpers.Components.CustomEntity;
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
			
			protected sealed override void InitializeComponent( BarrierHitRadiusNpcEntityComponent data ) {
				data.HitsFriendly = this.HitsFriendly;
				this.InitializeBarrierHitRadiusNpcComponent( data );
			}

			protected virtual void InitializeBarrierHitRadiusNpcComponent( BarrierHitRadiusNpcEntityComponent data ) { }
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
			return ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, NPC npc, ref int dmg ) {
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			dmg = (int)Math.Min( npc.life, behavComp.Hp );
			
			if( this.HitsFriendly != npc.friendly ) {
				return false;
			}

			return behavComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, NPC npc, int dmg ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			//float oldHp = behavComp.Hp;
			if( !behavComp.HitByNpc( ent, npc, ref dmg ) ) {
				return;
			}

			float npcDamage = dmg + behavComp.Defense;

			if( npcDamage > 0 ) {
				NPCHelpers.HurtRaw( npc, (int)npcDamage );

				npc.velocity += Vector2.Normalize( npc.position - ent.Core.position ) * ( npcDamage / 20 );

				myent.EmitImpactFx( npc.Center, npc.width, npc.height, npcDamage );
			}

			//if( mymod.Config.DebugModeInfo ) {
			//	DebugHelpers.Print( "barrier hurts "+npc.TypeName+" ("+npc.whoAmI+")", "dmg:"+damage+", -hp:"+(oldHp-behavComp.Hp)+", -rad:"+radDamage+", npc hit:"+npcDamage, 20 );
			//}
		}
	}
}
