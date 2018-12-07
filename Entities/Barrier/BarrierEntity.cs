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
	partial class BarrierEntity : CustomEntity {
		private class BarrierEntityFactory<T> : CustomEntityFactory<T> where T : BarrierEntity {
			public float Radius;
			public float RadiusRegenRate;
			public int Defense;
			public float ShrinkResist;
			public Vector2 Center;


			public BarrierEntityFactory( float radius, float regenRate, int defense, float shrinkResist, Vector2 center ) : base( null ) {
				this.Radius = radius;
				this.RadiusRegenRate = regenRate;
				this.Defense = defense;
				this.ShrinkResist = shrinkResist;
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

		public static BarrierEntity CreateBarrierEntity( float radius, float regenRate, int defense, float shrinkResist, Vector2 center ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + center );
			}

			var factory = new BarrierEntityFactory<BarrierEntity>( radius, regenRate, defense, shrinkResist, center );
			return factory.Create();
		}

		internal static BarrierEntity CreateDefaultBarrierEntity() {
			return BarrierEntity.CreateBarrierEntity( 64, 1f/60f, 0, 0, Main.LocalPlayer.Center );
		}



		////////////////

		public int TotalPower;

		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition1;
		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition2;


		////////////////

		protected BarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		////

		protected override CustomEntityCore CreateCore<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as BarrierEntityFactory<BarrierEntity>;

			float rad = myfactory?.Radius ?? 0f;

			return new CustomEntityCore( "Evil Barrier", (int)( rad * 2 ), (int)( rad * 2 ), ( myfactory?.Center ?? default( Vector2 ) ), 1 );
		}

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as BarrierEntityFactory<BarrierEntity>;
			float radius = myfactory?.Radius ?? 64;
			float regenRate = myfactory?.RadiusRegenRate ?? 1f / 60f;
			int defense = myfactory?.Defense ?? 0;
			float shrinkResist = myfactory?.ShrinkResist ?? 0f;

			return new List<CustomEntityComponent> {
				BarrierBehaviorEntityComponent.CreateBarrierEntityComponent( radius, regenRate, defense, shrinkResist ),
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


		////////////////

		public void UpdateForPlayer( Player player ) {
			this.Core.Center = player.Center;
		}


		////////////////

		public void AdjustBarrierSize( float radiusScale ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			behavComp.MaxRadius = (float)this.TotalPower * radiusScale;
		}

		public void AdjustBarrierDefense( float defenseScale ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			behavComp.Defense = (int)((float)this.TotalPower * defenseScale);
		}
		
		public void AdjustBarrierShrinkResist( float resistScale ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			behavComp.ShrinkResist = resistScale;
		}

		public void AdjustBarrierRegen( float regenScale ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			behavComp.RadiusRegenRate = (regenScale * this.TotalPower) / 60f;
		}
	}
}
