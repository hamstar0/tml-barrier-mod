using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;


namespace Barriers.Entities.Barrier.Components {
	class BarrierDrawOnMapEntityComponent : DrawsOnMapEntityComponent {
		private class BarrierDrawOnMapEntityComponentFactory : DrawsOnMapEntityComponentFactory<BarrierDrawOnMapEntityComponent> {
			public BarrierDrawOnMapEntityComponentFactory() : base( "Barriers", "Entities/Barrier/BarrierIcon", 1, 0.5f, true ) { }
		}



		////////////////

		public static BarrierDrawOnMapEntityComponent CreateBarrierDrawOnMapEntityComponent() {
			var factory = new BarrierDrawOnMapEntityComponentFactory();
			return factory.Create();
		}



		////////////////

		protected BarrierDrawOnMapEntityComponent( PacketProtocolDataConstructorLock ctor_lock ) : base( ctor_lock ) { }


		////////////////

		public override Color GetColor( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			return myent.GetBarrierColor();
		}
	}
}
