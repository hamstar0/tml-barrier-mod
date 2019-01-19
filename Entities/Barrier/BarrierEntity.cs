﻿using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		private BarrierEntity() : base( null ) { }

		////

		protected override CustomEntityCore CreateCore( CustomEntityFactory factory ) {
			//var myfactory = factory as BarrierEntityFactory<BarrierEntity>;
			var myfactory = factory as IBarrierEntityFactory;

			float rad = myfactory?.Radius ?? 64f;
			float dim = rad * 2f;
			Vector2 pos = (myfactory?.Center ?? default(Vector2)) - new Vector2(rad, rad);

			return new CustomEntityCore( "Barrier", (int)dim, (int)dim, pos, 1 );
		}

		protected override IList<CustomEntityComponent> CreateComponents( CustomEntityFactory factory ) {
			//var myfactory = factory as BarrierEntityFactory<BarrierEntity>;
			var myfactory = factory as IBarrierEntityFactory;
			var bodyColor = new Color( 128, 128, 128 );
			var edgeColor = new Color( 160, 160, 160 );

			if( myfactory != null ) {
				bodyColor = myfactory.BarrierBodyColor;
				edgeColor = myfactory.BarrierEdgeColor;
			}

			var statsComp = this.CreateStatsComponent( myfactory );

			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				if( myfactory != null ) {
					LogHelpers.Log( "Initializing barrier components - Stats = hp:" + myfactory.Hp
						+ ", rad:" + myfactory.Radius
						+ ", def:" + myfactory.Defense
						+ ", regen:" + myfactory.RegenRate
						+ ", hard:" + myfactory.ShrinkResistScale
					);
				} else {
					LogHelpers.LogOnce( "Initializing template barrier components (probably sync) - " + this.ToString() );
				}
			}

			var comps = new List<CustomEntityComponent> {
				statsComp,
				BarrierDrawInGameEntityComponent.CreateBarrierDrawInGameEntityComponent( bodyColor, edgeColor ),
				PacketProtocolData.CreateDefault<BarrierDrawOnMapEntityComponent>(),
				PacketProtocolData.CreateDefault<BarrierPeriodicSyncEntityComponent>()
			};

			return comps;
		}


		public override CustomEntityCore CreateCoreTemplate() {
			return this.CreateCore( null );
		}

		public override IList<CustomEntityComponent> CreateComponentsTemplate() {
			return this.CreateComponents( null );
		}


		////

		protected virtual BarrierStatsEntityComponent CreateStatsComponent( IBarrierEntityFactory myfactory ) {
			return BarrierStatsEntityComponent.CreateBarrierStatsEntityComponent(
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
