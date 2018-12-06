using Barriers.Items;
using HamstarHelpers.Helpers.DotNetHelpers;
using HamstarHelpers.Services.Promises;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;


namespace Barriers.UI {
	partial class BarrierUI {
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

		private int FindRadialInteractions( double spanAngleRange ) {
			for( int i = 0; i < 45; i++ ) {
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

			return	Math.Abs( angle - myangle ) <= ( spanAngleRange * 0.5d ) ||
					Math.Abs( ( 360 + angle ) - myangle ) <= ( spanAngleRange * 0.5d );
		}


		////////////////

		public void RadialInteraction( int whichSpan, double spanAngleRange, IPalingItemType paling ) {
			bool found = false;

			for( int i=0; i<paling.Layers.Length; i++ ) {
				if( paling.Layers[i] == -1 ) {
					paling.Layers[i] = whichSpan;
					found = true;
					break;
				}

				if( paling.Layers[i] == whichSpan ) {
					paling.Layers[i] = -1;
					found = true;
					break;
				}
			}

			if( !found ) {
				paling.Layers[ paling.Layers.Length - 1 ] = whichSpan;
			}

			Main.PlaySound( SoundID.MenuTick );
		}
	}
}
