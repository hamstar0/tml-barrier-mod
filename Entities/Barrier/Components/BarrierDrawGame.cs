using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierDrawInGameEntityComponent : DrawsInGameEntityComponent {
		private class BarrierDrawOnMapEntityComponentFactory : DrawsInGameEntityComponentFactory<BarrierDrawInGameEntityComponent> {
			public Color BarrierBodyColor;
			public Color BarrierEdgeColor;


			public BarrierDrawOnMapEntityComponentFactory( Color bodyColor, Color edgeColor )
					: base( "Barriers", "Entities/Barrier/Barrier128", 1 ) {
				this.BarrierBodyColor = bodyColor;
				this.BarrierEdgeColor = edgeColor;
			}

			protected override void InitializeDerivedComponent( BarrierDrawInGameEntityComponent data ) {
				data.BarrierBodyColor = this.BarrierBodyColor;
				data.BarrierEdgeColor = this.BarrierEdgeColor;
			}
		}



		////////////////

		public static BarrierDrawInGameEntityComponent CreateBarrierDrawInGameEntityComponent( Color bodyColor, Color edgeColor ) {
			var factory = new BarrierDrawOnMapEntityComponentFactory( bodyColor, edgeColor );
			return factory.Create();
		}



		////////////////

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

		protected BarrierDrawInGameEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void PostPostInitialize() {
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
			var behavComp = myent.GetComponentByType<BarrierStatsBehaviorEntityComponent>();
			float radius = behavComp.Radius;

			if( radius == 0 ) {
				return;
			}

			float stability = 1f - (Main.rand.NextFloat() * (1f - (behavComp.Hp / behavComp.MaxHp)));
			Color bodyColor = myent.GetBarrierColor() * stability;
			Color edgeColor = myent.GetEdgeColor() * stability;
			
			if( radius <= 64 ) {
				float scale = radius / 64f;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Body128, 1, bodyColor, scale );   //, scale * Vector2.One * 64 );
			} else if( radius <= 256 ) {
				float scale = radius / 256f;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Body512, 1, bodyColor, scale );    //, scale * Vector2.One * 256 );
			} else {    //if( radius <= 1024 )
				float scale = radius / 1024;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Body2048, 1, bodyColor, scale );   //, scale * Vector2.One * 1024 );
			}
				
			if( radius <= 64 ) {
				float scale = radius / 64f;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Edge128, 1, edgeColor, scale );   //, scale * Vector2.One * 64 );
			} else if( radius <= 256 ) {
				float scale = radius / 256f;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Edge512, 1, edgeColor, scale );    //, scale * Vector2.One * 256 );
			} else {    //if( radius <= 1024 )
				float scale = radius / 1024;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Edge2048, 1, edgeColor, scale );   //, scale * Vector2.One * 1024 );
			}

			//sb.End();
			//sb.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, GameShaders.Misc["ForceField"].Shader, Main.Transform );
			//sb.End();
			//sb.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform );


			/*
			sb.End();
			sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			float num143 = 0f;
			if (nPC.ai[3] > 0f && nPC.ai[3] <= 30f)
			{
				num143 = 1f - nPC.ai[3] / 30f;
			}
			Filters.Scene[key].GetShader().UseIntensity(1f + num143).UseProgress(0f);
			DrawData value12 = new DrawData(TextureManager.Load("Images/Misc/Perlin"), value11 + new Vector2(300f, 300f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 600, 600)), Microsoft.Xna.Framework.Color.White * (num142 * 0.8f + 0.2f), nPC.rotation, new Vector2(300f, 300f), nPC.scale * (1f + num143 * 0.05f), spriteEffects, 0);
			GameShaders.Misc["ForceField"].UseColor(new Vector3(1f + num143 * 0.5f));
			GameShaders.Misc["ForceField"].Apply(new DrawData?(value12));
			value12.Draw( sb );

			sb.End();
			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, this.Rasterizer, null, Main.Transform);
			*/
		}
	}
}
