using System;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierDrawInGameEntityComponent : DrawsInGameEntityComponent {
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Body2048;
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Body512;
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Body128;
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Edge2048;
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Edge512;
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Edge128;

		////

		public Color BarrierBodyColor;
		public Color BarrierEdgeColor;



		////////////////

		private BarrierDrawInGameEntityComponent() : base( "Barriers", "Entities/Barrier/Barrier128", 1 ) { }
		public BarrierDrawInGameEntityComponent( Color bodyColor, Color edgeColor ) : this() {
			this.BarrierBodyColor = bodyColor;
			this.BarrierEdgeColor = edgeColor;
		}

		////

		protected override void PostInitialize() {
			var mymod = BarriersMod.Instance;

			if( !Main.dedServ ) {
				this.Body128 = mymod.GetTexture( "Entities/Barrier/Barrier128" );
				this.Body512 = mymod.GetTexture( "Entities/Barrier/Barrier512" );
				this.Body2048 = mymod.GetTexture( "Entities/Barrier/Barrier2048" );
				this.Edge128 = mymod.GetTexture( "Entities/Barrier/BarrierRing128" );
				this.Edge512 = mymod.GetTexture( "Entities/Barrier/BarrierRing512" );
				this.Edge2048 = mymod.GetTexture( "Entities/Barrier/BarrierRing2048" );
			}
		}


		////////////////

		public override void Draw( SpriteBatch sb, CustomEntity ent ) {
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
		}
	}
}
