﻿using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		protected interface INpcBarrierEntityFactory : IBarrierEntityFactory {
			NPC Npc { get; }
		}



		protected class NpcBarrierEntityConstructor : BarrierEntityConstructor, INpcBarrierEntityFactory {
			public NPC Npc { get; }
			public override float Hp { get; }
			public override float Radius { get; }
			public override int Defense { get; }
			public override float ShrinkResistScale { get; }
			public override float RegenRate { get; }
			public override int RecoverDurationHighest { get; }
			public override Color BarrierBodyColor { get; }
			public override Color BarrierEdgeColor { get; }
			public override Vector2 Center { get; }


			////////////////

			public NpcBarrierEntityConstructor( NPC npc, Vector2 center, float hp, float radius, int defense, float shrinkResistScale,
					float regenRate, int recoverDurationHighest, Color? bodyColor = null, Color? edgeColor = null ) : base( null ) {
				this.Npc = npc;
				this.Center = center;
				this.Hp = hp;
				this.Radius = radius;
				this.Defense = defense;
				this.ShrinkResistScale = shrinkResistScale;
				this.RegenRate = regenRate;
				this.RecoverDurationHighest = recoverDurationHighest;
				this.BarrierBodyColor = bodyColor ?? new Color( 128, 128, 0 );
				this.BarrierEdgeColor = edgeColor ?? new Color( 160, 160, 32 );
			}
		}



		////////////////

		public static NpcBarrierEntity CreateNpcBarrierEntity( NPC npc, Vector2 center, float hp, float radius, int defense,
				float shrinkResistScale, float regenRate, int recoverDurationHighest,
				Color? bodyColor = null, Color? edgeColor = null ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + center );
			}

			var factory = new NpcBarrierEntityConstructor( npc, center, hp, radius, defense, shrinkResistScale, regenRate, recoverDurationHighest, bodyColor, edgeColor );
			return new NpcBarrierEntity( factory );
		}

		internal static NpcBarrierEntity CreateDefaultNpcBarrierEntity( NPC npc, Vector2 center ) {
			var mymod = BarriersMod.Instance;
			int defaultHp = mymod.Config.NpcBarrierDefaultHpAmount;
			float defaultRegen = mymod.Config.BarrierDefaultRegenPerTick;

			return NpcBarrierEntity.CreateNpcBarrierEntity( npc, center, defaultHp, defaultHp, 0, 0f, defaultRegen,
				mymod.Config.PlayerBarrierDefaultRecoverDurationMax );
		}
	}
}
