using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier {
	public partial class BarrierEntity : CustomEntity {
		private class BarrierEntityFactory<T> : CustomEntityFactory<T> where T : BarrierEntity {
			public int Power;
			public float HpScale;
			public float RadiusScale;
			public float DefenseScale;
			public float ShrinkResist;
			public float RegenScale;
			public Vector2 Center;


			public BarrierEntityFactory( int power, float hpScale, float radiusScale, float defenseScale, float shrinkResist, float regenScale, Vector2 center ) : base( null ) {
				this.Power = power;
				this.HpScale = hpScale;
				this.RadiusScale = radiusScale;
				this.DefenseScale = defenseScale;
				this.ShrinkResist = shrinkResist;
				this.RegenScale = regenScale;
				this.Center = center;
			}

			////

			protected override void InitializeEntity( T ent ) {
				if( Main.netMode == 2 ) {
					ent.SyncToAll();
				}
			}
		}



		////////////////

		public static BarrierEntity CreateBarrierEntity( int power, float hpScale, float radiusScale, float defenseScale, float shrinkResist, float regenScale, Vector2 center ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + center );
			}

			var factory = new BarrierEntityFactory<BarrierEntity>( power, hpScale, radiusScale, defenseScale, shrinkResist, regenScale, center );
			return factory.Create();
		}

		internal static BarrierEntity CreateDefaultBarrierEntity() {
			return BarrierEntity.CreateBarrierEntity( 128, 1f, 1f, 0f, 0f, (1f / 60f), Main.LocalPlayer.Center );
		}



		////////////////
		
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

		protected BarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		////

		protected override CustomEntityCore CreateCore<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as BarrierEntityFactory<BarrierEntity>;

			float rad = myfactory?.Power * 0.5f ?? 0f;

			return new CustomEntityCore( "Barrier", (int)rad, (int)rad, (myfactory?.Center ?? default(Vector2)), 1 );
		}

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as BarrierEntityFactory<BarrierEntity>;
			float hp = 64f;
			float radius = 64f;
			float regenRate = 1f / 60f;
			int defense = 0;
			float shrinkResist = 0f;

			if( myfactory != null ) {
				hp = BarrierEntity.ComputeBarrierMaxHp( this.Power, myfactory.HpScale );
				radius = BarrierEntity.ComputeBarrierMaxRadius( this.Power, myfactory.RadiusScale );
				regenRate = BarrierEntity.ComputeBarrierRegen( this.Power, myfactory.RegenScale );
				defense = BarrierEntity.ComputeBarrierDefense( this.Power, myfactory.DefenseScale );
				shrinkResist = BarrierEntity.ComputeBarrierShrinkResist( this.Power, myfactory.ShrinkResist );
			}

			return new List<CustomEntityComponent> {
				BarrierBehaviorEntityComponent.CreateBarrierEntityComponent( hp, radius, regenRate, defense, shrinkResist ),
				BarrierDrawInGameEntityComponent.CreateBarrierDrawInGameEntityComponent(),
				BarrierDrawOnMapEntityComponent.CreateBarrierDrawOnMapEntityComponent(),
				BarrierPeriodicSyncEntityComponent.CreateBarrierPeriodicSyncEntityComponent(),
				BarrierHitRadiusProjectileEntityComponent.CreateBarrierHitRadiusProjectileEntityComponent(),
				BarrierHitRadiusNpcEntityComponent.CreateBarrierHitRadiusNpcEntityComponent()
			};
		}

		public override CustomEntityCore CreateCoreTemplate() {
			return this.CreateCore<BarrierEntity>( null );
		}

		public override IList<CustomEntityComponent> CreateComponentsTemplate() {
			return this.CreateComponents<BarrierEntity>( null );
		}



		////////////////

		public virtual Color GetBarrierColor() {
			/*var behav = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			int layers = behav.BarrierLayers.Length * 2;
			int r = 0, g = 0, b = 0;

			foreach( var hue in behav.BarrierLayers ) {
				switch( hue ) {
				case BarrierTypes.Red:
					r += 255;
					break;
				case BarrierTypes.Green:
					g += 255;
					break;
				case BarrierTypes.Blue:
					b += 255;
					break;
				}
			}

			return new Color( r / layers, g / layers, b / layers, 128 );*/
			return new Color( 0, 128, 0 );
		}
	}
}
