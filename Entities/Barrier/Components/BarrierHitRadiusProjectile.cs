﻿using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.ProjectileHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	public class BarrierHitRadiusProjectileEntityComponent : HitRadiusProjectileEntityComponent {
		public int HitsFriendly;
		public int HitsHostile;



		////////////////

		private BarrierHitRadiusProjectileEntityComponent() { }

		public BarrierHitRadiusProjectileEntityComponent( int hitsFriendly, int hitsHostile ) : this() {
			this.HitsFriendly = hitsFriendly;   // .friendly = Hurts enemies
			this.HitsHostile = hitsHostile;     // .hostile = Hurts player and friends
			this.Validate();
		}

		////

		protected override void OnClone() {
			this.Validate();
		}

		private void Validate() {
			if( this.HitsFriendly < -1 || this.HitsFriendly > 1 ) {
				throw new HamstarException( "Invalid HitsFriendly value." );
			}
			if( this.HitsHostile < -1 || this.HitsHostile > 1 ) {
				throw new HamstarException( "Invalid HitsHostile value." );
			}
		}


		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierStatsEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, Projectile proj, ref int dmg ) {
			var myent = (BarrierEntity)ent;
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();

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

			return (hitsFriendly || hitsHostile) && statsComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, Projectile proj, int dmg ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var behavComp = ent.GetComponentByType<BarrierStatsEntityComponent>();

			if( !behavComp.HitByProjectile( ent, proj, ref dmg ) ) {
				return;
			}

			int defDamage = Math.Max( 0, dmg - behavComp.Defense );

			//if( dmg > (behavComp.Defense * behavComp.ShrinkResistScale) ) {
			if( dmg > 0 ) {
				ProjectileHelpers.Hit( proj );
			}

			proj.velocity = Vector2.Normalize( proj.position - ent.Core.position ) * proj.velocity.Length();

			myent.EmitImpactFx( proj.Center, proj.width, proj.height, defDamage );
		}
	}
}
