﻿using Barriers.Items;
using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Helpers.HudHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;


namespace Barriers.UI {
	partial class BarrierUI {
		public void DrawUI( SpriteBatch sb, IPalingItemType paling ) {
			int x = Main.screenWidth / 2;
			int y = Main.screenHeight / 2;

			var rect = new Rectangle( x, y, 160, 160 );
			float rot = (float)( 45d * DotNetHelpers.RadDeg );
			var origin = new Vector2( 0.5f, 512f );

			sb.Draw( Main.magicPixel, rect, null, Color.LightPink * 0.3f, rot, origin, SpriteEffects.None, 1f );
			HudHelpers.DrawBorderedRect( sb, Color.DarkOliveGreen * 0.5f, Color.OliveDrab * 0.5f, new Rectangle( x - 48, y - 48, 96, 96 ), 4 );

			double spanAngleRange = 8;

			this.DrawRadialMarks( sb, spanAngleRange );

			if( Main.mouseLeft ) {
				if( !this.IsInteractingWithUI ) {
					this.IsInteractingWithUI = true;

					int whichSpan = this.FindRadialInteractions( spanAngleRange );
					if( whichSpan != -1 ) {
						this.RadialInteraction( whichSpan, spanAngleRange, paling );
					}
				}
			} else {
				this.IsInteractingWithUI = false;
			}

			this.DrawLayers( sb, paling, spanAngleRange );
		}


		////////////////

		public void DrawLayers( SpriteBatch sb, IPalingItemType paling, double spanAngleRange ) {
			for( int i = 0; i < paling.Layers.Length; i++ ) {
				if( paling.Layers[i] == -1 ) {
					break;
				}

				this.DrawRadialMark( sb, paling.Layers[i], spanAngleRange, 120 - (i*4), Color.Yellow );
			}
		}


		////////////////

		public void DrawRadialMarks( SpriteBatch sb, double spanAngleRange ) {
			for( int i = 0; i < 45; i++ ) {
				this.DrawRadialMark( sb, i, spanAngleRange, 128, new Color( 128, 128, 128 ) );
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