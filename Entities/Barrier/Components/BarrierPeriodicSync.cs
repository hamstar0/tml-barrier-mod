using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.Components {
	class BarrierPeriodicSyncEntityComponent : PeriodicSyncEntityComponent {
		private class BarrierPeriodicSyncEntityComponentFactory : CustomEntityComponentFactory<BarrierPeriodicSyncEntityComponent> {
			public BarrierPeriodicSyncEntityComponentFactory() { }
			protected override void InitializeComponent( BarrierPeriodicSyncEntityComponent data ) { }
		}



		////////////////

		public static BarrierPeriodicSyncEntityComponent CreateBarrierPeriodicSyncEntityComponent() {
			var factory = new BarrierPeriodicSyncEntityComponentFactory();
			return factory.Create();
		}



		////////////////

		protected BarrierPeriodicSyncEntityComponent( PacketProtocolDataConstructorLock ctor_lock ) : base( ctor_lock ) { }

		////////////////

		public override void UpdateClient( CustomEntity ent ) {
			base.UpdateMe( ent );
		}

		public override void UpdateServer( CustomEntity ent ) { }
	}
}
