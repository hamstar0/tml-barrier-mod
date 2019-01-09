using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsEntityComponent : CustomEntityComponent {
		public bool HitByNpc( CustomEntity ent, NPC npc, ref int dmg ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;

			int defDmg = Math.Max( 0, dmg - this.Defense );
			float radDmg = defDmg * ( 1f - this.ShrinkResistScale );

			if( !this.OnPreHitByNpc( ent, npc, ref dmg, ref defDmg, ref radDmg ) ) {
				return false;
			}

			if( defDmg > 0 ) {
				this.Hp = this.Hp > defDmg ? this.Hp - defDmg : 0;
				this.Radius = this.Radius > radDmg ? this.Radius - radDmg : 0;
			}

			this.OnPostHitByNpc( ent, npc, dmg, defDmg, radDmg );

			return true;
		}


		////////////////

		public virtual bool OnPreHitByNpc( CustomEntity ent, NPC npc, ref int dmg, ref int defDmg, ref float radiusDmg ) {
			return true;
		}

		public virtual void OnPostHitByNpc( CustomEntity ent, NPC npc, int dmg, int defDmg, float radiusDmg ) { }
	}
}
