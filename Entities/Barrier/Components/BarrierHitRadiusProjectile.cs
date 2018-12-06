﻿using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierHitRadiusProjectileEntityComponent : HitRadiusProjectileEntityComponent {
		private class BarrierHitRadiusProjectileEntityComponentFactory : CustomEntityComponentFactory<BarrierHitRadiusProjectileEntityComponent> {
			protected override void InitializeComponent( BarrierHitRadiusProjectileEntityComponent data ) { }
		}



		////////////////

		public static BarrierHitRadiusProjectileEntityComponent CreateBarrierHitRadiusProjectileEntityComponent() {
			var factory = new BarrierHitRadiusProjectileEntityComponentFactory();
			return factory.Create();
		}



		////////////////
		
		protected BarrierHitRadiusProjectileEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierBehaviorEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, Projectile projectile, ref int damage ) {
			return projectile.hostile || !projectile.friendly;
		}

		public override void PostHurt( CustomEntity ent, Projectile projectile, int damage ) {
			var behavComp = ent.GetComponentByType<BarrierBehaviorEntityComponent>();

			behavComp.Radius -= damage;
			if( behavComp.Radius < 0 ) { behavComp.Radius = 0; }
		}
	}
}
