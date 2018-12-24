using Barriers.Entities.Barrier.NpcBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		private class NpcBarrierEntityFactory : BarrierEntityFactory<NpcBarrierEntity> {
			public Vector2 Center;

			////
			
			public override float HpGet { get; }
			public override float RadiusGet { get; }
			public override int DefenseGet { get; }
			public override float ShrinkResistScaleGet { get; }
			public override float RegenRateGet { get; }
			public override Vector2 CenterGetSet { get; protected set; }
			

			////////////////

			public NpcBarrierEntityFactory( NPC npc, float hp, float radius, int defense, float shrinkResistScale, float regenScale ) : base( null ) {
				this.Center = npc.Center;
			}
		}



		////////////////

		public static NpcBarrierEntity CreateNpcBarrierEntity( NPC npc,
				float hp, float radius, int defense, float shrinkResistScale, float regenScale ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + npc.Center );
			}

			var factory = new NpcBarrierEntityFactory( npc, hp, radius, defense, shrinkResistScale, regenScale );
			NpcBarrierEntity myent = factory.Create();

			return myent;
		}

		internal static NpcBarrierEntity CreateDefaultNpcBarrierEntity( NPC npc ) {
			var mymod = BarriersMod.Instance;
			int defaultHp = mymod.Config.NpcBarrierHpBaseAmount;
			float defaultRegen = mymod.Config.BarrierRegenBaseAmount;

			return NpcBarrierEntity.CreateNpcBarrierEntity( npc, defaultHp, defaultHp, 0, 0f, defaultRegen );
		}



		////////////////
		
		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var mymod = BarriersMod.Instance;
			var myfactory = factory as NpcBarrierEntityFactory;
			IList<CustomEntityComponent> comps = base.CreateComponents<T>( factory );

			if( myfactory != null ) {
				comps.Insert( 0, NpcBarrierBehaviorEntityComponent.CreateBarrierEntityComponent(
					myfactory.HpGet,
					myfactory.RadiusGet,
					myfactory.DefenseGet,
					myfactory.ShrinkResistScaleGet,
					myfactory.RegenRateGet )
				);
			} else {
				comps.Insert( 0, NpcBarrierBehaviorEntityComponent.CreateBarrierEntityComponent( 
					mymod.Config.NpcBarrierHpBaseAmount,
					mymod.Config.NpcBarrierHpBaseAmount,
					mymod.Config.BarrierDefenseBaseAmount,
					mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount,
					mymod.Config.BarrierRegenBaseAmount
				) );
			}

			return comps;
		}
	}
}
