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
			public BarrierDrawOnMapEntityComponentFactory() : base( "Barriers", "Entities/Barrier/Barrier128", 1 ) { }

			protected override void InitializeDerivedComponent( BarrierDrawInGameEntityComponent data ) { }
		}



		////////////////

		public static BarrierDrawInGameEntityComponent CreateBarrierDrawInGameEntityComponent() {
			var factory = new BarrierDrawOnMapEntityComponentFactory();
			return factory.Create();
		}



		////////////////

		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Texture2048;
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Texture512;
		[PacketProtocolIgnore]
		[JsonIgnore]
		protected Texture2D Texture128;



		////////////////

		protected BarrierDrawInGameEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void PostPostInitialize() {
			var mymod = BarriersMod.Instance;

			if( !Main.dedServ ) {
				this.Texture128 = mymod.GetTexture( "Entities/Barrier/Barrier128" );
				this.Texture512 = mymod.GetTexture( "Entities/Barrier/Barrier512" );
				this.Texture2048 = mymod.GetTexture( "Entities/Barrier/Barrier2048" );
			}
		}


		////////////////
		
		public override void Draw( SpriteBatch sb, CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();
			float radius = behavComp.Radius;

			if( radius == 0 ) {
				return;
			}

			Color color = myent.GetBarrierColor();

			//sb.End();
			//sb.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, GameShaders.Misc["ForceField"].Shader, Main.Transform );

			if( radius <= 64 ) {
				float scale = radius / 64f;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Texture128, 1, color, scale );	//, scale * Vector2.One * 64 );
			} else if( radius <= 256 ) {
				float scale = radius / 256f;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Texture512, 1, color, scale );    //, scale * Vector2.One * 256 );
			} else {	//if( radius <= 1024 )
				float scale = radius / 1024;
				DrawsInGameEntityComponent.DrawTexture( sb, ent, this.Texture2048, 1, color, scale );   //, scale * Vector2.One * 1024 );
			}

			//sb.End();
			//sb.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform );
		}
	}
}
