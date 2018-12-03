using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Barriers.Entities.Barrier.Components {
	class BarrierDrawOnMapEntityComponent : DrawsOnMapEntityComponent {
		private class BarrierDrawOnMapEntityComponentFactory : DrawsOnMapEntityComponentFactory<BarrierDrawOnMapEntityComponent> {
			public BarrierDrawOnMapEntityComponentFactory() : base( "Barriers", "Entities/Barrier/Barrier128", 1, 0.5f, true ) { }
		}



		////////////////

		public static BarrierDrawOnMapEntityComponent CreateBarrierDrawOnMapEntityComponent() {
			var factory = new BarrierDrawOnMapEntityComponentFactory();
			return factory.Create();
		}



		////////////////

		protected BarrierDrawOnMapEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override Color GetColor( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			return myent.GetBarrierColor();
		}


		////////////////

		private void UpdateScale( CustomEntity ent ) {
			var behavComp = ent.GetComponentByType<BarrierBehaviorEntityComponent>();
			float radius = behavComp.Radius;

			this.Scale = radius / (128f * 8f);
		}

		public override bool PreDrawFullscreenMap( SpriteBatch sb, CustomEntity ent ) {
			this.UpdateScale( ent );
			return true;
		}

		public override bool PreDrawMiniMap( SpriteBatch sb, CustomEntity ent ) {
			this.UpdateScale( ent );
			return true;
		}

		public override bool PreDrawOverlayMap( SpriteBatch sb, CustomEntity ent ) {
			this.UpdateScale( ent );
			return true;
		}
	}
}
