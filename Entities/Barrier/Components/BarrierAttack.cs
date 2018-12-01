using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Components.Network.Data;
using System;


namespace Barriers.Entities.Barrier.Components {
	class BarrierAttackableEntityComponent : AttackableEntityComponent {
		protected BarrierAttackableEntityComponent( PacketProtocolDataConstructorLock ctor_lock ) : base( ctor_lock ) { }

		public override void PostHurt( CustomEntity ent, int damage ) {
			throw new NotImplementedException();
		}
	}
}
