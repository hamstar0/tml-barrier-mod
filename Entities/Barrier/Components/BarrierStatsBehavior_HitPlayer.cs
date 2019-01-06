﻿using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsBehaviorEntityComponent : CustomEntityComponent {
		public bool HitByPlayer( CustomEntity ent, Player plr, ref int dmg ) {
			var mymod = BarriersMod.Instance;
			int defDmg = Math.Max( 0, dmg - this.Defense );
			float radDmg = defDmg * ( 1f - this.ShrinkResistScale );

			if( !this.OnPreHitByPlayer( ent, plr, ref dmg, ref defDmg, ref radDmg ) ) {
				return false;
			}

			if( defDmg > 0 ) {
				this.Hp = Math.Max( this.Hp - defDmg, 0 );
				this.Radius = Math.Max( this.Radius - radDmg, 0 );
			}

			this.OnPostHitByPlayer( ent, plr, dmg, defDmg, radDmg );

			return true;
		}


		////////////////

		public virtual bool OnPreHitByPlayer( CustomEntity ent, Player plr, ref int dmg, ref int defDmg, ref float radiusDmg  ) {
			return true;
		}

		public virtual void OnPostHitByPlayer( CustomEntity ent, Player plr, int dmg, int defDmg, float radiusDmg ) { }
	}
}
