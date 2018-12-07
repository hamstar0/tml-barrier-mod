using Barriers.Entities.Barrier;
using HamstarHelpers.Helpers.DotNetHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;


namespace Barriers.UI {
	partial class BarrierUI {
		public void DrawUI( SpriteBatch sb, BarrierEntity ent ) {
			int x = Main.screenWidth / 2;
			int y = Main.screenHeight / 2;

			var rect = new Rectangle( x, y, 160, 160 );
			float rot = (float)( 45d * DotNetHelpers.RadDeg );
			var origin = new Vector2( 0.5f, 512f );

			sb.Draw( Main.magicPixel, rect, null, Color.LightPink * 0.05f, rot, origin, SpriteEffects.None, 1f );
			
			int whichSpan = this.FindRadialTickHovered( BarrierUI.SpanAngleRange );

			this.DrawRadialMarks( sb, BarrierUI.SpanAngleRange, whichSpan );
			this.DrawIcons( sb );

			this.Interact( whichSpan, BarrierUI.SpanAngleRange, ent );

			this.DrawSelectedRadialMarks( sb, ent, BarrierUI.SpanAngleRange );
		}


		public void DrawIcons( SpriteBatch sb ) {
			int dist = 80;
			var mid = new Vector2( Main.screenWidth / 2, Main.screenHeight / 2 );
			var top = mid + new Vector2( -21, -21 - dist );
			var right = mid + new Vector2( -21 + dist, -21 );
			var bottom = mid + new Vector2( -21, -21 + dist );
			var left = mid + new Vector2( -21 - dist, -21 );

			sb.Draw( BarrierUI.BarrierSizeTex, top, Color.White * (0.1f + (this.SizeScale * 0.9f)) );
			sb.Draw( BarrierUI.BarrierStrengthTex, right, Color.White * (0.1f + (this.StrengthScale * 0.9f)) );
			sb.Draw( BarrierUI.BarrierHardnessTex, bottom, Color.White * (0.1f + (this.HardScale * 0.9f)) );
			sb.Draw( BarrierUI.BarrierRegenTex, left, Color.White * (0.1f + (this.RegenScale * 0.9f)) );
		}


		////////////////

		public void DrawSelectedRadialMarks( SpriteBatch sb, BarrierEntity ent, double spanAngleRange ) {
			this.DrawRadialMark( sb, ent.UiRadialPosition1, spanAngleRange, 120, Color.Yellow );
			this.DrawRadialMark( sb, ent.UiRadialPosition2, spanAngleRange, 116, Color.Red );
		}


		////////////////

		public void DrawRadialMarks( SpriteBatch sb, double spanAngleRange, int highlight ) {
			for( int i = 0; i < 45; i++ ) {
				if( i == highlight ) {
					this.DrawRadialMark( sb, i, spanAngleRange, 128, new Color( 192, 192, 192 ) );
				} else {
					this.DrawRadialMark( sb, i, spanAngleRange, 128, new Color( 128, 128, 128 ) );
				}
			}
		}


		private Vector2? _TickDim = null;

		public void DrawRadialMark( SpriteBatch sb, double whichSpan, double spanAngleRange, double radius, Color color ) {
			int x = ( Main.screenWidth / 2 ) + (int)( radius * Math.Cos( whichSpan * spanAngleRange * DotNetHelpers.RadDeg ) );
			int y = ( Main.screenHeight / 2 ) + (int)( radius * Math.Sin( whichSpan * spanAngleRange * DotNetHelpers.RadDeg ) );

			if( this._TickDim == null ) {
				this._TickDim = Main.fontItemStack.MeasureString( "+" ) * 0.5f;
			}
			Vector2 pos = new Vector2( x, y ) - (Vector2)this._TickDim;

			sb.DrawString( Main.fontItemStack, "+", pos, color );
		}
	}
}
