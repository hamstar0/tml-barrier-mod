using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		public override Color GetBarrierColor( bool baseOnly = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			Color baseColor = new Color( 0, 128, 0 );
			if( baseOnly ) {
				return baseColor;
			}

			float opacity = Math.Min( (float)behavComp.Defense / 64f, 1f );
			opacity = 0.25f + ( opacity * 0.6f );

			return baseColor * opacity;
		}


		public override Color GetEdgeColor( bool baseOnly = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			Color baseColor = new Color( 32, 160, 32 );
			if( baseOnly ) {
				return baseColor;
			}

			return baseColor * behavComp.ShrinkResistScale;
		}
	}
}
