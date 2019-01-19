using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.Components {
	public class BarrierPeriodicSyncEntityComponent : PeriodicSyncEntityComponent {
		private BarrierPeriodicSyncEntityComponent() : base() { }
		public BarrierPeriodicSyncEntityComponent( object _ = null ) : this() { }


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
