using System;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		public override Tuple<bool, bool> SyncFromClientServer => Tuple.Create( false, true );



		////////////////

		protected NpcBarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }
	}
}
