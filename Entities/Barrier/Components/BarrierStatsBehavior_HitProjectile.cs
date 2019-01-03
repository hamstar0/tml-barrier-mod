﻿using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsBehaviorEntityComponent : CustomEntityComponent {
		public bool HitByProjectile( CustomEntity ent, Projectile proj, int dmg ) {
			var mymod = BarriersMod.Instance;

			int defDmg = Math.Max( 0, dmg - this.Defense );
			float radDmg = defDmg * ( 1f - this.ShrinkResistScale );

			if( !this.OnPreHitByProjectile( ent, proj, ref defDmg, ref radDmg ) ) {
				return false;
			}

			if( defDmg > ( mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount * this.ShrinkResistScale ) ) {
				this.Hp -= defDmg;
				if( this.Hp < 0 ) { this.Hp = 0; }

				this.Radius -= radDmg;
				if( this.Radius < 0 ) {
					this.Radius = 0;
				}
			}

			this.OnPostHitByProjectile( ent, proj, defDmg, radDmg );

			return true;
		}


		////////////////

		public virtual bool OnPreHitByProjectile( CustomEntity ent, Projectile proj, ref int dmg, ref float radiusDmg ) {
			return true;
		}

		public virtual void OnPostHitByProjectile( CustomEntity ent, Projectile proj, int dmg, float radiusDmg ) { }
	}
}
