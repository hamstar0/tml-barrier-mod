using System;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierDrawInGameEntityComponent : DrawsInGameEntityComponent {
		public Color BarrierBodyColor;
		public Color BarrierEdgeColor;



		////////////////

		private BarrierDrawInGameEntityComponent() : base( "Barriers", "Entities/Barrier/Barrier128", 1 ) { }
		public BarrierDrawInGameEntityComponent( Color bodyColor, Color edgeColor ) : this() {
			this.BarrierBodyColor = bodyColor;
			this.BarrierEdgeColor = edgeColor;
		}


		////////////////

		public override Effect GetFx( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			var behavComp = myent.GetComponentByType<BarrierStatsEntityComponent>();
			float radius = behavComp.Radius;
			float hpPercent = behavComp.Hp / behavComp.MaxHp;
			float shrinkResistScale = myent.GetShrinkResistScale();

			if( radius == 0 || behavComp.Hp == 0 ) {
				return null;
			}

			Effect barrierFx = BarriersMod.Instance.BarrierFx;
			barrierFx.Parameters["ScreenPos"].SetValue( Main.screenPosition );
			barrierFx.Parameters["ScreenDim"].SetValue( new Vector2( Main.screenWidth, Main.screenHeight ) );
			barrierFx.Parameters["EntCenter"].SetValue( ent.Core.Center );
			barrierFx.Parameters["EdgeColor"].SetValue( this.BarrierEdgeColor.ToVector4() );
			barrierFx.Parameters["BodyColor"].SetValue( this.BarrierBodyColor.ToVector4() );
			barrierFx.Parameters["Radius"].SetValue( radius );
			barrierFx.Parameters["HpPercent"].SetValue( hpPercent );
			barrierFx.Parameters["ShrinkResistScale"].SetValue( shrinkResistScale );

			return barrierFx;
		}


		////////////////

		public override void DrawUnder( SpriteBatch sb, CustomEntity ent ) {
			sb.Draw( Main.magicPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Transparent );
		}
		
		/*public override void Draw( SpriteBatch sb, CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			var behavComp = myent.GetComponentByType<BarrierStatsEntityComponent>();
			float radius = behavComp.Radius;
			float hpPercent = behavComp.Hp / behavComp.MaxHp;
			float shrinkResistScale = myent.GetShrinkResistScale();
			Vector2 mid = ent.Core.Center;
			float dist, y;
			Vector2 pos;

			if( radius == 0 || behavComp.Hp == 0 ) {
				return;
			}

			for( int i = 0; i < Main.screenWidth; i++ ) {
				int j = 0, prevJ = 0;
				pos = new Vector2( i + Main.screenPosition.X, Main.screenPosition.Y );

				do {
					y = j + Main.screenPosition.Y;
					dist = Vector2.Distance( pos, mid );

					if( y < mid.Y && dist > radius ) { // above and outside barrier
						prevJ = j;
						j += (Main.screenHeight - j) / 2; // lower pos; moves closer
					} else {
						j = prevJ + ( (j - prevJ) / 2 ); // raise pos; moves closer
					}
					
					pos.Y = Main.screenPosition.Y + j;
				} while( j < Main.screenHeight && j != prevJ );

				for( j++; j < Main.screenHeight; j++ ) {
					pos.Y = Main.screenPosition.Y + j;

					dist = Vector2.Distance( pos, ent.Core.Center );
					if( dist > radius ) {
						break;
					}
					
					this.DrawAt( sb, i, j, dist, radius, hpPercent, shrinkResistScale );
				}
			}
		}


		private void DrawAt( SpriteBatch sb, int screenX, int screenY, float distFromCenter, float radius,
				float hpPercent, float shrinkResistScale ) {
			float percentToRadius = distFromCenter / radius;
			float distToEdge = radius - distFromCenter;
			float stability = 1f - (Main.rand.NextFloat() * (1f - hpPercent));
			float softness = BarriersMod.Instance.Config.BarrierDrawSoftness + 1f;
			float rand = Main.rand.NextFloat() * softness;

			if( distToEdge < ( shrinkResistScale * 24 ) ) {
				if( rand <= 1f ) {
					Color color = this.BarrierEdgeColor * stability;
					
					sb.Draw( Main.magicPixel, new Rectangle( screenX, screenY, 1, 1 ), color );
				}
			} else {
				float intensity = 1f - (float)Math.Sqrt( 1f - (percentToRadius * percentToRadius) );

				if( rand <= intensity ) {
					Color color = Color.Lerp( Color.Transparent, this.BarrierBodyColor, (0.15f + (intensity * 0.65f)) * stability );

					sb.Draw( Main.magicPixel, new Rectangle(screenX, screenY, 1, 1), color );
				}
			}
		}*/
	}
}
