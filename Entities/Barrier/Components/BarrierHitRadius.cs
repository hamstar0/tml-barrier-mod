using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierHitRadiusProjectileEntityComponent : HitRadiusProjectileEntityComponent {
		protected class BarrierHitRadiusProjectileEntityComponentFactory : HitRadiusProjectileEntityComponentFactory<BarrierHitRadiusProjectileEntityComponent> {
			public BarrierHitRadiusProjectileEntityComponentFactory( float radius ) : base( radius ) { }

			protected override void InitializeComponent( BarrierHitRadiusProjectileEntityComponent data ) { }
		}



		////////////////

		public static BarrierHitRadiusProjectileEntityComponent CreateBarrierHitRadiusProjectileEntityComponent( float radius ) {
			var factory = new BarrierHitRadiusProjectileEntityComponentFactory( radius );
			return factory.Create();
		}


		////////////////

		protected BarrierHitRadiusProjectileEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		public override bool PreHurt( CustomEntity ent, Projectile projectile, ref int damage ) {
Main.NewText( projectile.hostile+" "+projectile.friendly );
			return projectile.hostile && !projectile.friendly;
		}

		public override void PostHurt( CustomEntity ent, Projectile projectile, int damage ) {
			var behavComp = ent.GetComponentByType<BarrierBehaviorEntityComponent>();
		}
	}
}
