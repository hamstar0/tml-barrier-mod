using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		public virtual Color GetBarrierColor() {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			var drawComp = this.GetComponentByType<BarrierDrawInGameEntityComponent>();

			float opacity = Math.Min( (float)behavComp.Defense / 64f, 1f );
			opacity = 0.25f + ( opacity * 0.6f );

			return drawComp.BarrierBodyColor * opacity;
		}


		public virtual Color GetEdgeColor() {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();
			var drawComp = this.GetComponentByType<BarrierDrawInGameEntityComponent>();

			return drawComp.BarrierEdgeColor * behavComp.ShrinkResistScale;
		}


		////////////////

		public void EmitImpactFx( Vector2 center, int width, int height, float damage ) {
			var drawComp = this.GetComponentByType<BarrierDrawInGameEntityComponent>();
			int particles = Math.Min( (int)damage / 3, 8 );

			for( int i = 0; i < particles; i++ ) {
				Vector2 position = Main.LocalPlayer.Center;
				Dust.NewDust( center, width, height, 264, 0f, 0f, 0, drawComp.BarrierBodyColor, 0.66f );
			}
		}
	}
}
