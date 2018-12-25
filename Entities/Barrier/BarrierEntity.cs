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

			float rad = myfactory?.RadiusGet * 2f ?? 64f;

			return new CustomEntityCore( "Barrier", (int)rad, (int)rad, ( myfactory?.CenterGetSet ?? default( Vector2 ) ), 1 );
		}

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			//var myfactory = factory as BarrierEntityFactory<BarrierEntity>;
			var myfactory = factory as IBarrierEntityFactory;
			float hp = 64f;
			float radius = 64f;
			int defense = 0;
			float regenRate = BarriersMod.Instance.Config.BarrierDefenseBaseAmount;
			float shrinkResist = 0f;

			if( myfactory != null ) {
				hp = myfactory.HpGet;
				radius = myfactory.RadiusGet;
				defense = myfactory.DefenseGet;
				regenRate = myfactory.RegenRateGet;
				shrinkResist = myfactory.ShrinkResistScaleGet;
			}

			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				if( myfactory != null ) {
					LogHelpers.Log( "New barrier stats = hp:" + hp
						+ ", rad:" + radius
						+ ", def:" + defense
						+ ", regen:" + regenRate
						+ ", hard:" + shrinkResist );
				} else {
					LogHelpers.LogOnce( "New template barrier (probably sync) "+this.ToString() );
				}
			}

			var comps = new List<CustomEntityComponent> {
				BarrierBehaviorEntityComponent.CreateBarrierEntityComponent( hp, radius, regenRate, defense, shrinkResist ),
				BarrierDrawInGameEntityComponent.CreateBarrierDrawInGameEntityComponent(),
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
	}
}
