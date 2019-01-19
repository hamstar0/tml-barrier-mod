using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsEntityComponent : CustomEntityComponent {
		public bool HitByPlayer( CustomEntity ent, Player plr, ref int dmg ) {
			var mymod = BarriersMod.Instance;
			float radDmg = dmg * ( 1f - this.ShrinkResistScale );

			if( !this.OnPreHitByPlayer( ent, plr, ref dmg, ref radDmg ) ) {
				return false;
			}

			float defDmg = Math.Max( dmg - this.Defense, 0 );

			if( dmg > 0 ) {
				this.Hp = Math.Max( this.Hp - defDmg, 0 );
				this.Radius = Math.Max( this.Radius - radDmg, 0 );
			}

			this.OnPostHitByPlayer( ent, plr, dmg, radDmg );

			return true;
		}


		////////////////

		public virtual bool OnPreHitByPlayer( CustomEntity ent, Player plr, ref int dmg, ref float radiusDmg  ) {
			return true;
		}

		public virtual void OnPostHitByPlayer( CustomEntity ent, Player plr, int dmg, float radiusDmg ) { }
	}
}
