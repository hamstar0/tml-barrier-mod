using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
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
			var behavComp = ent.GetComponentByType<BarrierBehaviorEntityComponent>();

			behavComp.Radius -= damage;
			if( behavComp.Radius < 0 ) { behavComp.Radius = 0; }
		}
	}
}
