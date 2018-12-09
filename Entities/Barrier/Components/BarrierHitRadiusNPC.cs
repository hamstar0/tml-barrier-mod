using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.NPCHelpers;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierHitRadiusNpcEntityComponent : HitRadiusNPCEntityComponent {
		private class BarrierHitRadiusNpcEntityComponentFactory : CustomEntityComponentFactory<BarrierHitRadiusNpcEntityComponent> {
			protected override void InitializeComponent( BarrierHitRadiusNpcEntityComponent data ) { }
		}



		////////////////

		public static BarrierHitRadiusNpcEntityComponent CreateBarrierHitRadiusNpcEntityComponent() {
			var factory = new BarrierHitRadiusNpcEntityComponentFactory();
			return factory.Create();
		}



		////////////////
		
		protected BarrierHitRadiusNpcEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		
		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierBehaviorEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, NPC npc, ref int damage ) {
			return !npc.friendly;
		}

		public override void PostHurt( CustomEntity ent, NPC npc, int damage ) {
			var mymod = BarriersMod.Instance;
			var behavComp = ent.GetComponentByType<BarrierBehaviorEntityComponent>();

			int defDamage = Math.Max( 0, damage - behavComp.Defense );
			float radDamage = defDamage * ( 1f - behavComp.ShrinkResistScale );

			if( defDamage > ( mymod.Config.HardnessDeflectionMaximumAmount * behavComp.ShrinkResistScale ) ) {
				behavComp.Hp -= defDamage;
				if( behavComp.Hp < 0 ) { behavComp.Hp = 0; }

				behavComp.Radius -= radDamage;
				if( behavComp.Radius < 0 ) { behavComp.Radius = 0; }
			}
			
			if( defDamage > 0 ) {
				NPCHelpers.Hurt( npc, defDamage );
			}
		}
	}
}
