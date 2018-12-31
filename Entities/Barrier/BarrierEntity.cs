using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		protected BarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		////

		protected override CustomEntityCore CreateCore<T>( CustomEntityFactory<T> factory ) {
			//var myfactory = factory as BarrierEntityFactory<BarrierEntity>;
			var myfactory = factory as IBarrierEntityFactory;

			float rad = myfactory?.Radius * 2f ?? 64f;

			return new CustomEntityCore( "Barrier", (int)rad, (int)rad, ( myfactory?.Center ?? default( Vector2 ) ), 1 );
		}

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			//var myfactory = factory as BarrierEntityFactory<BarrierEntity>;
			var myfactory = factory as IBarrierEntityFactory;
			var bodyColor = new Color( 128, 128, 128 );
			var edgeColor = new Color( 160, 160, 160 );

			if( myfactory != null ) {
				bodyColor = myfactory.BarrierBodyColor;
				edgeColor = myfactory.BarrierEdgeColor;
			}

			var behavComp = this.CreateBehaviorComponent( myfactory );

			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				if( myfactory != null ) {
					LogHelpers.Log( "New barrier stats = hp:" + myfactory.Hp
						+ ", rad:" + myfactory.Radius
						+ ", def:" + myfactory.Defense
						+ ", regen:" + myfactory.RegenRate
						+ ", hard:" + myfactory.ShrinkResistScale );
				} else {
					LogHelpers.LogOnce( "New template barrier (probably sync) " + this.ToString() );
				}
			}

			var comps = new List<CustomEntityComponent> {
				behavComp,
				BarrierDrawInGameEntityComponent.CreateBarrierDrawInGameEntityComponent( bodyColor, edgeColor ),
				BarrierDrawOnMapEntityComponent.CreateBarrierDrawOnMapEntityComponent(),
				BarrierPeriodicSyncEntityComponent.CreateBarrierPeriodicSyncEntityComponent()
			};

			return comps;
		}


		public override CustomEntityCore CreateCoreTemplate() {
			return this.CreateCore<BarrierEntity>( null );
		}

		public override IList<CustomEntityComponent> CreateComponentsTemplate() {
			return this.CreateComponents<BarrierEntity>( null );
		}


		////

		protected virtual BarrierBehaviorEntityComponent CreateBehaviorComponent( IBarrierEntityFactory myfactory ) {
			float hp = 64f;
			float radius = 64f;
			int defense = 0;
			float regenRate = BarriersMod.Instance.Config.BarrierDefenseBaseAmount;
			float shrinkResist = 0f;
			int regenRegenDurationHighest = 120;

			if( myfactory != null ) {
				hp = myfactory.Hp;
				radius = myfactory.Radius;
				defense = myfactory.Defense;
				regenRate = myfactory.RegenRate;
				shrinkResist = myfactory.ShrinkResistScale;
				regenRegenDurationHighest = myfactory.RegenRegenDurationHighest;
			}

			return BarrierBehaviorEntityComponent.CreateBarrierEntityComponent( hp, radius, regenRate, defense, shrinkResist,
				regenRegenDurationHighest );
		}
	}
}
