using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace Barriers.Entities.Barrier {
	public partial class BarrierEntity : CustomEntity {
		public virtual Color GetBarrierColor( bool baseOnly = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			Color baseColor = new Color( 0, 128, 0 );
			if( baseOnly ) {
				return baseColor;
			}

			float opacity = Math.Min( (float)behavComp.Defense / 64f, 1f );
			opacity = 0.25f + ( opacity * 0.6f );

			return baseColor * opacity;
		}


		public virtual Color GetEdgeColor( bool baseOnly = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			Color baseColor = new Color( 32, 160, 32 );
			if( baseOnly ) {
				return baseColor;
			}

			return baseColor * behavComp.ShrinkResistScale;
		}


		////////////////

		public void EmitImpactFx( Vector2 center, int width, int height, float damage ) {
			int particles = Math.Min( (int)damage / 3, 8 );

			for( int i = 0; i < particles; i++ ) {
				Vector2 position = Main.LocalPlayer.Center;
				Dust.NewDust( center, width, height, 264, 0f, 0f, 0, this.GetBarrierColor( true ), 0.66f );
			}
		}
	}
}
