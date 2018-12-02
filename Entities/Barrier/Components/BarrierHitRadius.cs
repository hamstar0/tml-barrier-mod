using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierHitRadiusProjectileEntityComponent : HitRadiusProjectileEntityComponent {
		protected class BarrierHitRadiusProjectileEntityComponentFactory : HitRadiusProjectileEntityComponentFactory<BarrierHitRadiusProjectileEntityComponent> {
			public BarrierHitRadiusProjectileEntityComponentFactory( float radius ) : base( radius ) { }

			protected override void InitializeComponent( BarrierHitRadiusProjectileEntityComponent data ) { }
		}



		////////////////

		public static HitRadiusProjectileEntityComponent CreateBarrierHitRadiusProjectileEntityComponent( float radius ) {
			var factory = new HitRadiusProjectileEntityComponentFactory<HitRadiusProjectileEntityComponent>( radius );
			return factory.Create();
		}


		////////////////

		protected BarrierHitRadiusProjectileEntityComponent( PacketProtocolDataConstructorLock ctor_lock ) : base( ctor_lock ) { }

		public override void PostHurt( CustomEntity ent, Projectile projectile, int damage ) {
Main.NewText("? "+damage);
		}
	}
}
