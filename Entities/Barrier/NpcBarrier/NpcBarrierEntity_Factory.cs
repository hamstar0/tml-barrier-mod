using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.NpcBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		private class NpcBarrierEntityFactory : BarrierEntityFactory<NpcBarrierEntity> {
			public NPC Npc { get; }
			public override float Hp { get; }
			public override float Radius { get; }
			public override int Defense { get; }
			public override float ShrinkResistScale { get; }
			public override float RegenRate { get; }
			public override int RegenRegenDurationHighest { get; }
			public override Color BarrierBodyColor { get; }
			public override Color BarrierEdgeColor { get; }
			public override Vector2 Center { get; }


			////////////////

			public NpcBarrierEntityFactory( NPC npc, Vector2 center, float hp, float radius, int defense, float shrinkResistScale,
					float regenRate, int regenRegenDurationHighest, Color? bodyColor=null, Color? edgeColor=null ) : base( null ) {
				this.Npc = npc;
				this.Center = center;
				this.Hp = hp;
				this.Radius = radius;
				this.Defense = defense;
				this.ShrinkResistScale = shrinkResistScale;
				this.RegenRate = regenRate;
				this.RegenRegenDurationHighest = regenRegenDurationHighest;
				this.BarrierBodyColor = bodyColor ?? new Color( 128, 128, 0 );
				this.BarrierEdgeColor = edgeColor ?? new Color( 160, 160, 32 );
			}
		}



		////////////////

		public static NpcBarrierEntity CreateNpcBarrierEntity( NPC npc, Vector2 center, float hp, float radius, int defense,
				float shrinkResistScale, float regenRate, int regenRegenDurationHighest,
				Color? bodyColor = null, Color? edgeColor = null ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + center );
			}

			var factory = new NpcBarrierEntityFactory( npc, center, hp, radius, defense, shrinkResistScale, regenRate, regenRegenDurationHighest, bodyColor, edgeColor );
			NpcBarrierEntity myent = factory.Create();

			return myent;
		}

		internal static NpcBarrierEntity CreateDefaultNpcBarrierEntity( NPC npc, Vector2 center ) {
			var mymod = BarriersMod.Instance;
			int defaultHp = mymod.Config.NpcBarrierHpBaseAmount;
			float defaultRegen = mymod.Config.BarrierRegenBaseAmount;

			return NpcBarrierEntity.CreateNpcBarrierEntity( npc, center, defaultHp, defaultHp, 0, 0f, defaultRegen,
				mymod.Config.PlayerBarrierDefaultRegenRegenDurationHighest );
		}



		////////////////
		
		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var mymod = BarriersMod.Instance;
			var myfactory = factory as NpcBarrierEntityFactory;
			IList<CustomEntityComponent> comps = base.CreateComponents<T>( factory );

			if( myfactory != null ) {
				comps.Insert( 0, NpcBarrierBehaviorEntityComponent.CreateBarrierEntityComponent(
					myfactory.Npc,
					myfactory.Hp,
					myfactory.Radius,
					myfactory.Defense,
					myfactory.ShrinkResistScale,
					myfactory.RegenRate )
				);
			} else {
				comps.Insert( 0, NpcBarrierBehaviorEntityComponent.CreateBarrierEntityComponent(
					null,
					mymod.Config.NpcBarrierHpBaseAmount,
					mymod.Config.NpcBarrierHpBaseAmount,
					mymod.Config.BarrierDefenseBaseAmount,
					mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount,
					mymod.Config.BarrierRegenBaseAmount
				) );
			}

			comps.Add( BarrierHitRadiusProjectileEntityComponent.CreateBarrierHitRadiusProjectileEntityComponent(1, 1) );
			comps.Add( BarrierHitRadiusPlayerEntityComponent.CreateBarrierHitRadiusPlayerEntityComponent() );

			return comps;
		}
	}
}
