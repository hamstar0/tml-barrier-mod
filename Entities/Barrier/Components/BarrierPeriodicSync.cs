using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using System;

namespace Barriers.Entities.Barrier.Components {
	class BarrierPeriodicSyncEntityComponent : PeriodicSyncEntityComponent {
		protected BarrierPeriodicSyncEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		////

		protected override Type GetMyFactoryType() {
			return null;
		}


		////////////////

		protected override bool UpdateMe( CustomEntity ent ) {
			bool isUpdated = base.UpdateMe( ent );

			if( isUpdated ) {
				if( BarriersMod.Instance.Config.DebugModeInfo ) {
					LogHelpers.Alert( "Sync occurred" );
				}
			}

			return isUpdated;
		}
	}
}
