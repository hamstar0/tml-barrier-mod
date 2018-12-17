using Barriers.Entities.Barrier.PlayerBarrier.Components;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		public bool SetBarrierPower( int power, bool skipSync = false ) {
			var plrBehavComp = this.GetComponentByType<PlayerBarrierBehaviorEntityComponent>();
			return plrBehavComp.SetBarrierPower( this, power, skipSync );
		}

		////

		internal bool SetBarrierHpScale( float hpScale, bool skipSync = false ) {
			var plrBehavComp = this.GetComponentByType<PlayerBarrierBehaviorEntityComponent>();
			return plrBehavComp.SetBarrierHpScale( this, hpScale, skipSync );
		}

		internal bool SetBarrierRadiusScale( float radiusScale, bool skipSync = false ) {
			var plrBehavComp = this.GetComponentByType<PlayerBarrierBehaviorEntityComponent>();
			return plrBehavComp.SetBarrierRadiusScale( this, radiusScale, skipSync );
		}

		internal bool SetBarrierDefenseScale( float defenseScale, bool skipSync = false ) {
			var plrBehavComp = this.GetComponentByType<PlayerBarrierBehaviorEntityComponent>();
			return plrBehavComp.SetBarrierDefenseScale( this, defenseScale, skipSync );
		}

		internal bool SetBarrierShrinkResistScale( float resistScale, bool skipSync = false ) {
			var plrBehavComp = this.GetComponentByType<PlayerBarrierBehaviorEntityComponent>();
			return plrBehavComp.SetBarrierShrinkResistScale( this, resistScale, skipSync );
		}

		internal bool SetBarrierRegenScale( float regenScale, bool skipSync = false ) {
			var plrBehavComp = this.GetComponentByType<PlayerBarrierBehaviorEntityComponent>();
			return plrBehavComp.SetBarrierRegenScale( this, regenScale, skipSync );
		}
	}
}
