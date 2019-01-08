using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.Components {
	class BarrierPeriodicSyncEntityComponent : PeriodicSyncEntityComponent {
		private class BarrierPeriodicSyncEntityComponentFactory : CustomEntityComponentFactory<BarrierPeriodicSyncEntityComponent> {
			protected override void InitializeComponent( BarrierPeriodicSyncEntityComponent data ) { }
		}



		////////////////

		public static BarrierPeriodicSyncEntityComponent CreateBarrierPeriodicSyncEntityComponent() {
			var factory = new BarrierPeriodicSyncEntityComponentFactory();
			return factory.Create();
		}



		////////////////

		protected BarrierPeriodicSyncEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }
		
		////

		protected override bool UpdateMe( CustomEntity ent ) {
			bool isUpdated = base.UpdateMe( ent );

			if( isUpdated ) {
				if( BarriersMod.Instance.Config.DebugModeInfo ) {
					LogHelpers.Log( "Barriers.BarrierPeriodicSyncEntityComponentFactory.UpdateMe - Sync occurred" );
				}
			}

			return isUpdated;
		}
	}
}
