using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.ProjectileHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierHitRadiusProjectileEntityComponent : HitRadiusProjectileEntityComponent {
		private class BarrierHitRadiusProjectileEntityComponentFactory : CustomEntityComponentFactory<BarrierHitRadiusProjectileEntityComponent> {
			public int HitsFriendly;	// .friendly = Hurts enemies
			public int HitsHostile;	// .hostile = Hurts player and friends


			public BarrierHitRadiusProjectileEntityComponentFactory( int hitsFriendly, int hitsHostile ) {
				this.HitsFriendly = hitsFriendly;
				this.HitsHostile = hitsHostile;
			}

			protected override void InitializeComponent( BarrierHitRadiusProjectileEntityComponent data ) {
				data.HitsFriendly = this.HitsFriendly;
				data.HitsHostile = this.HitsHostile;
			}
		}



		////////////////

		public static BarrierHitRadiusProjectileEntityComponent CreateBarrierHitRadiusProjectileEntityComponent( int hitsFriendly, int hitsHostile ) {
			var factory = new BarrierHitRadiusProjectileEntityComponentFactory( hitsFriendly, hitsHostile );
			return factory.Create();
		}



		////////////////
		
		public int HitsFriendly;
		public int HitsHostile;



		////////////////

		protected BarrierHitRadiusProjectileEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, Projectile proj, ref int dmg ) {
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			bool hitsFriendly = this.HitsFriendly > 0 ?
				proj.friendly :
				this.HitsFriendly < 0 ?
					!proj.friendly :
					false;
			bool hitsHostile = this.HitsHostile > 0 ?
				proj.hostile :
				this.HitsHostile < 0 ?
					!proj.hostile :
					false;

			return (hitsFriendly || hitsHostile) && behavComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, Projectile proj, int dmg ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			if( !behavComp.HitByProjectile( ent, proj, dmg ) ) {
				return;
			}

			int defDamage = Math.Max( 0, dmg - behavComp.Defense );

			if( defDamage > (mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount * behavComp.ShrinkResistScale) ) {
				ProjectileHelpers.Hit( proj );
			}

			proj.velocity = Vector2.Normalize( proj.position - ent.Core.position ) * proj.velocity.Length();

			myent.EmitImpactFx( proj.Center, proj.width, proj.height, defDamage );
		}
	}
}
