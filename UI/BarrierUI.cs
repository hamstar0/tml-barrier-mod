using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Helpers.HudHelpers;
using HamstarHelpers.Services.Promises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;


namespace Barriers.UI {
	class BarrierUI {
		public const int LayersRingRadius = 72;


		////////////////

		public static Texture2D BarrierSizeTex { get; private set; }
		public static Texture2D BarrierStrengthTex { get; private set; }
		public static Texture2D BarrierHardnessTex { get; private set; }
		public static Texture2D BarrierRegenTex { get; private set; }


		////

		static BarrierUI() {
			BarrierUI.BarrierSizeTex = null;
			BarrierUI.BarrierStrengthTex = null;
			BarrierUI.BarrierHardnessTex = null;
			BarrierUI.BarrierRegenTex = null;
		}

		internal static void InitializeStatic( BarriersMod mymod ) {
			if( BarrierUI.BarrierSizeTex == null ) {
				/*BarrierUI.BarrierSizeTex = mymod.GetTexture( "UI/BarrierSizeTex" );
				BarrierUI.BarrierStrengthTex = mymod.GetTexture( "UI/BarrierStrengthTex" );
				BarrierUI.BarrierHardnessTex = mymod.GetTexture( "UI/BarrierHardnessTex" );
				BarrierUI.BarrierRegenTex = mymod.GetTexture( "UI/BarrierRegenTex" );*/

				Promises.AddModUnloadPromise( () => {
					BarrierUI.BarrierSizeTex = null;
					BarrierUI.BarrierStrengthTex = null;
					BarrierUI.BarrierHardnessTex = null;
					BarrierUI.BarrierRegenTex = null;
				} );
			}
		}



		////////////////
		
		private bool IsInteractingWithUI = false;



		////////////////

		public void DrawUI( BarriersMod mymod, SpriteBatch sb ) {
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
						this.DrawRadialMark( sb, whichSpan, spanAngleRange, Color.Yellow );
						Main.PlaySound( SoundID.MenuTick );
					}
				}
			} else {
				this.IsInteractingWithUI = false;
			}
		}

		////

		public void DrawRadialMarks( SpriteBatch sb, double spanAngleRange ) {
			for( int i = 0; i < 45; i++ ) {
				this.DrawRadialMark( sb, i, spanAngleRange, new Color( 128, 128, 128 ) );
			}
		}

		private Vector2? _TickDim = null;

		public void DrawRadialMark( SpriteBatch sb, double whichSpan, double spanAngleRange, Color color ) {
			int x = (Main.screenWidth / 2) + (int)( 128d * Math.Cos( whichSpan * spanAngleRange * DotNetHelpers.RadDeg ) );
			int y = (Main.screenHeight / 2) + (int)( 128d * Math.Sin( whichSpan * spanAngleRange * DotNetHelpers.RadDeg ) );

			if( this._TickDim == null ) {
				this._TickDim = Main.fontItemStack.MeasureString( "+" ) * 0.5f;
			}
			Vector2 pos = new Vector2( x, y ) - (Vector2)this._TickDim;

			sb.DrawString( Main.fontItemStack, "+", pos, color );
		}


		////////////////

		private int FindRadialInteractions( double spanAngleRange ) {
			for( int i=0; i<45; i++ ) {
				if( this.IsHoveringRadialMark( i, spanAngleRange ) ) {
					return i;
				}
			}
			return -1;
		}

		public bool IsHoveringRadialMark( double whichSpan, double spanAngleRange ) {
			var screenMid = new Vector2( Main.screenWidth / 2, Main.screenHeight / 2 );
			var mousePos = new Vector2( Main.mouseX, Main.mouseY );

			if( Vector2.Distance( screenMid, mousePos ) < ( BarrierUI.LayersRingRadius + 16 ) ) {
				return false;
			}

			double xOff = mousePos.X - screenMid.X;
			double yOff = mousePos.Y - screenMid.Y;
			double myangle = Math.Atan2( yOff, xOff ) * DotNetHelpers.DegRed;
			myangle = myangle < 0 ? 360 + myangle : myangle;

			double angle = whichSpan * spanAngleRange;

			return	Math.Abs( angle - myangle ) <= (spanAngleRange * 0.5d) ||
					Math.Abs( (360 + angle) - myangle ) <= (spanAngleRange * 0.5d);
		}
	}
}
