using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.NPCHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	public class BarrierHitRadiusNpcEntityComponent : HitRadiusNpcEntityComponent {
		public bool HitsFriendly;



		////////////////

		private BarrierHitRadiusNpcEntityComponent() { }
		public BarrierHitRadiusNpcEntityComponent( bool hitsFriendly ) : this() {
			this.HitsFriendly = hitsFriendly;
		}

		////

		protected override void OnClone() { }


		////////////////

		public override float GetRadius( CustomEntity ent ) {
			return ent.GetComponentByType<BarrierStatsEntityComponent>().Radius;
		}


		////////////////

		public override bool PreHurt( CustomEntity ent, NPC npc, ref int dmg ) {
			var myent = (BarrierEntity)ent;
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();

			dmg = (int)Math.Min( npc.life, statsComp.Hp );
			
			if( this.HitsFriendly != npc.friendly ) {
				return false;
			}

			return statsComp.Hp > 0;
		}

		public override void PostHurt( CustomEntity ent, NPC npc, int dmg ) {
			var mymod = BarriersMod.Instance;
			var myent = (BarrierEntity)ent;
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();

			//float oldHp = behavComp.Hp;
			if( !statsComp.HitByNpc( ent, npc, ref dmg ) ) {
				return;
			}

			float npcDamage = dmg + statsComp.Defense;

			if( npcDamage > 0 ) {
				NPCHelpers.RawHurt( npc, (int)npcDamage );

				npc.velocity += Vector2.Normalize( npc.position - ent.Core.position ) * ( npcDamage / 20 );

				myent.EmitImpactFx( npc.Center, npc.width, npc.height, npcDamage );
			}

			//if( mymod.Config.DebugModeInfo ) {
			//	DebugHelpers.Print( "barrier hurts "+npc.TypeName+" ("+npc.whoAmI+")", "dmg:"+damage+", -hp:"+(oldHp-behavComp.Hp)+", -rad:"+radDamage+", npc hit:"+npcDamage, 20 );
			//}
		}
	}
}
