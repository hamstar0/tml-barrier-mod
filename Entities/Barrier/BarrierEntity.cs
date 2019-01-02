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

			var behavComp = this.CreateStatsBehaviorComponent( myfactory );

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

		protected virtual BarrierStatsBehaviorEntityComponent CreateStatsBehaviorComponent( IBarrierEntityFactory myfactory ) {
			return BarrierStatsBehaviorEntityComponent.CreateBarrierEntityComponent(
				myfactory?.Hp ?? 64f,
				myfactory?.Radius ?? 64f,
				myfactory?.Defense ?? 0,
				myfactory?.RegenRate ?? BarriersMod.Instance.Config.BarrierDefenseBaseAmount,
				myfactory?.ShrinkResistScale ?? 0f,
				myfactory?.RegenRegenDurationHighest ?? 120
			);
		}
	}
}
