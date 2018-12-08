using Barriers.Entities.Barrier;
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
		public const int SpanAngleRange = 9;


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
			if( !Main.dedServ && BarrierUI.BarrierSizeTex == null ) {
				BarrierUI.BarrierSizeTex = mymod.GetTexture( "UI/BarrierSize" );
				BarrierUI.BarrierStrengthTex = mymod.GetTexture( "UI/BarrierStrength" );
				BarrierUI.BarrierHardnessTex = mymod.GetTexture( "UI/BarrierHardness" );
				BarrierUI.BarrierRegenTex = mymod.GetTexture( "UI/BarrierRegen" );

				Promises.AddModUnloadPromise( () => {
					BarrierUI.BarrierSizeTex = null;
					BarrierUI.BarrierStrengthTex = null;
					BarrierUI.BarrierHardnessTex = null;
					BarrierUI.BarrierRegenTex = null;
				} );
			}
		}



		////////////////

		private bool IsLeftClickingUI = false;
		private bool IsRightClickingUI = false;
		
		private float SizeScale = 0f;
		private float HardScale = 0f;
		private float StrengthScale = 0f;
		private float RegenScale = 0f;



		////////////////

		private void Interact( int whichSpan, double spanAngleRange, BarrierEntity ent ) {
			if( !this.IsLeftClickingUI ) {
				if( Main.mouseLeft ) {
					this.IsLeftClickingUI = true;
				}
			} else {
				if( !Main.mouseLeft ) {
					this.IsLeftClickingUI = false;
					if( whichSpan != -1 ) {
						this.RadialInteraction( true, whichSpan, ent );
					}
				}
			}

			if( !this.IsRightClickingUI ) {
				if( Main.mouseRight ) {
					this.IsRightClickingUI = true;
				}
			} else {
				if( !Main.mouseRight ) {
					this.IsRightClickingUI = false;
					if( whichSpan != -1 ) {
						this.RadialInteraction( false, whichSpan, ent );
					}
				}
			}

			float str1, hard1, regen1, size1;
			float str2, hard2, regen2, size2;

			this.InteractRadialPosition( ent.UiRadialPosition1, out str1, out hard1, out regen1, out size1 );
			this.InteractRadialPosition( ent.UiRadialPosition2, out str2, out hard2, out regen2, out size2 );
			this.StrengthScale = ( str1 + str2 ) * 0.5f;
			this.HardScale = ( hard1 + hard2 ) * 0.5f;
			this.RegenScale = ( regen1 + regen2 ) * 0.5f;
			this.SizeScale = ( size1 + size2 ) * 0.5f;

			bool hasChanged = false;
			ent.AdjustBarrierRadiusScale( this.SizeScale );
			ent.AdjustBarrierDefenseScale( this.StrengthScale );
			ent.AdjustBarrierShrinkResistScale( this.HardScale );
			ent.AdjustBarrierRegenScale( this.RegenScale );

			if( hasChanged ) {
				ent.SyncToAll();
			}
		}

		private void InteractRadialPosition( int radialPos, out float strScale, out float hardScale, out float regenScale, out float sizeScale ) {
			if( radialPos == -1 ) {
				strScale = 0;
				hardScale = 0;
				regenScale = 0;
				sizeScale = 0;
				return;
			}

			float fRadialPos = (float)radialPos;

			strScale =		1f - ( fRadialPos / 10f );
			hardScale =		1f - ( Math.Abs( fRadialPos - 10f ) / 10f );
			regenScale =	1f - ( Math.Abs( fRadialPos - 20f ) / 10f );
			sizeScale =		1f - ( Math.Abs( fRadialPos - 30f ) / 10f );

			strScale = strScale >= 0 ? strScale :
							1f - ( Math.Abs( fRadialPos - 40f ) / 10f );

			strScale = strScale < 0 ? 0 : strScale;
			hardScale = hardScale < 0 ? 0 : hardScale;
			regenScale = regenScale < 0 ? 0 : regenScale;
			sizeScale = sizeScale < 0 ? 0 : sizeScale;
		}


		////////////////

		private int FindRadialTickHovered( double spanAngleRange ) {
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

		public void RadialInteraction( bool isPosition1, int newPosition, BarrierEntity ent ) {
			if( isPosition1 ) {
				ent.UiRadialPosition1 = newPosition;
			} else {
				ent.UiRadialPosition2 = newPosition;
			}

			Main.PlaySound( SoundID.MenuTick );
		}
	}
}
