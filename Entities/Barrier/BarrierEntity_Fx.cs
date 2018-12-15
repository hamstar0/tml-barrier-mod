using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		public abstract Color GetBarrierColor( bool baseOnly = false );


		public abstract Color GetEdgeColor( bool baseOnly = false );


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
