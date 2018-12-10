using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition1 = 0;
		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition2 = 0;

		[JsonIgnore]
		[PacketProtocolIgnore]
		private int Power;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float HpScale;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float RadiusScale;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float DefenseScale;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float RegenScale;


		////////////////

		protected PlayerBarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		////

		protected override CustomEntityCore CreateCore<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as PlayerBarrierEntityFactory;

			float rad = myfactory?.Power * 0.5f ?? 0f;

			return new CustomEntityCore( "Barrier", (int)rad, (int)rad, ( myfactory?.Center ?? default( Vector2 ) ), 1 );
		}

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as PlayerBarrierEntityFactory;
			float hp = 64f;
			float radius = 64f;
			float regenRate = 1f / 60f;
			int defense = 0;
			float shrinkResist = 0f;

			if( myfactory != null ) {
				this.Power = myfactory.Power;
				this.HpScale = myfactory.HpScale;
				this.RadiusScale = myfactory.RadiusScale;
				this.RegenScale = myfactory.RegenScale;
				this.DefenseScale = myfactory.DefenseScale;
				//this.ShrinkResist = myfactory.ShrinkResist;

				hp = PlayerBarrierEntity.ComputeBarrierMaxHp( myfactory.Power, myfactory.HpScale );
				radius = PlayerBarrierEntity.ComputeBarrierMaxRadius( myfactory.Power, myfactory.RadiusScale );
				regenRate = PlayerBarrierEntity.ComputeBarrierRegen( myfactory.Power, myfactory.RegenScale );
				defense = PlayerBarrierEntity.ComputeBarrierDefense( myfactory.Power, myfactory.DefenseScale );
				shrinkResist = PlayerBarrierEntity.ComputeBarrierShrinkResist( myfactory.Power, myfactory.ShrinkResist );
			}

			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				if( myfactory != null ) {
					LogHelpers.Log( "New barrier scales = hp%:" + myfactory.HpScale
						+ ", rad%:" + myfactory.RadiusScale
						+ ", def%:" + myfactory.DefenseScale
						+ ", reg%:" + myfactory.RegenScale
						+ ", hard%:" + myfactory.ShrinkResist );
				} else {
					LogHelpers.Log( "New template barrier" );
				}
			}

			var comps = new List<CustomEntityComponent> {
				BarrierBehaviorEntityComponent.CreateBarrierEntityComponent( hp, radius, regenRate, defense, shrinkResist ),
				BarrierDrawInGameEntityComponent.CreateBarrierDrawInGameEntityComponent(),
				BarrierDrawOnMapEntityComponent.CreateBarrierDrawOnMapEntityComponent(),
				BarrierPeriodicSyncEntityComponent.CreateBarrierPeriodicSyncEntityComponent(),
				BarrierHitRadiusProjectileEntityComponent.CreateBarrierHitRadiusProjectileEntityComponent(),
				BarrierHitRadiusNpcEntityComponent.CreateBarrierHitRadiusNpcEntityComponent()
			};

			return comps;
		}

		public override CustomEntityCore CreateCoreTemplate() {
			return this.CreateCore<PlayerBarrierEntity>( null );
		}

		public override IList<CustomEntityComponent> CreateComponentsTemplate() {
			return this.CreateComponents<PlayerBarrierEntity>( null );
		}
	}
}
