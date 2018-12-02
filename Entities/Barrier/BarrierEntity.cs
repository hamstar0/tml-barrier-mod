using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier {
	public enum BarrierTypes {
		Red, Green, Blue, //Yellow, Cyan, Purple
	}




	partial class BarrierEntity : CustomEntity {
		protected class BarrierEntityFactory<T> : CustomEntityFactory<T> where T : BarrierEntity {
			public BarrierTypes[] BarrierTypes;
			public float Radius;
			public Vector2 Center;


			public BarrierEntityFactory( BarrierTypes[] barriers_types, float radius, Vector2 center ) : base( null ) {
				this.BarrierTypes = barriers_types;
				this.Radius = radius;
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

		private static IDictionary<int, BarrierEntity> PlayerBarriers = new Dictionary<int, BarrierEntity>();



		////////////////

		public static BarrierEntity CreateBarrierEntity( BarrierTypes[] barriers, float radius, Vector2 position ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + position );
			}

			var factory = new BarrierEntityFactory<BarrierEntity>( barriers, radius, position );
			return factory.Create();
		}



		////////////////

		protected BarrierEntity( PacketProtocolDataConstructorLock ctor_lock ) : base( ctor_lock ) { }

		////

		protected override CustomEntityCore CreateCore<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as BarrierEntityFactory<BarrierEntity>;

			float rad = myfactory?.Radius ?? 0f;

			return new CustomEntityCore( "Evil Barrier", (int)(rad * 2), (int)(rad * 2), (myfactory?.Center ?? default(Vector2)), 1 );
		}

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var myfactory = factory as BarrierEntityFactory<BarrierEntity>;

			return new List<CustomEntityComponent> {
				BarrierBehaviorEntityComponent.CreateBarrierEntityComponent( myfactory?.BarrierTypes ?? new BarrierTypes[0], 64 ),
				BarrierDrawInGameEntityComponent.CreateBarrierDrawInGameEntityComponent(),
				BarrierDrawOnMapEntityComponent.CreateBarrierDrawOnMapEntityComponent(),
				BarrierPeriodicSyncEntityComponent.CreateBarrierPeriodicSyncEntityComponent(),
				BarrierHitRadiusProjectileEntityComponent.CreateBarrierHitRadiusProjectileEntityComponent( 64 )
			};
		}

		public override CustomEntityCore CreateCoreTemplate() {
			return this.CreateCore<BarrierEntity>( null );
		}

		public override IList<CustomEntityComponent> CreateComponentsTemplate() {
			return this.CreateComponents<BarrierEntity>( null );
		}



		////////////////

		public Color GetBarrierColor() {
			var behav = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			int layers = behav.BarrierLayers.Length;
			int r=0, g=0, b=0;

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

			return new Color( r / layers, g / layers, b / layers, 192 );
		}
	}
}
