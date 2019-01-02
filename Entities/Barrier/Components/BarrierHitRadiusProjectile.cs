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

		public override bool PreHurt( CustomEntity ent, Projectile projectile, ref int damage ) {
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			bool hitsFriendly = this.HitsFriendly > 0 ?
				projectile.friendly :
				this.HitsFriendly < 0 ?
					!projectile.friendly :
					false;
			bool hitsHostile = this.HitsHostile > 0 ?
				projectile.hostile :
				this.HitsHostile < 0 ?
					!projectile.hostile :
					false;

			return (hitsFriendly || hitsHostile) && behavComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, Projectile projectile, int damage ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();

			int defDamage = Math.Max( 0, damage - behavComp.Defense );
			float radDamage = defDamage * ( 1f - behavComp.ShrinkResistScale );

			if( defDamage > (mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount * behavComp.ShrinkResistScale) ) {
				behavComp.Hp -= defDamage;
				if( behavComp.Hp < 0 ) { behavComp.Hp = 0; }

				behavComp.Radius -= radDamage;
				if( behavComp.Radius < 0 ) {
					behavComp.Radius = 0;
				}

				ProjectileHelpers.Hit( projectile );
			}

			projectile.velocity = Vector2.Normalize( projectile.position - ent.Core.position ) * projectile.velocity.Length();

			myent.EmitImpactFx( projectile.Center, projectile.width, projectile.height, defDamage );
		}
	}
}
