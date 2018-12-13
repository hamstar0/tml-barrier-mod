using System;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Newtonsoft.Json;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition1 = 0;
		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition2 = 0;

		[JsonIgnore]
		[PacketProtocolIgnore]
		private int Power;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float HpScale;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float RadiusScale;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float DefenseScale;
		[JsonIgnore]
		[PacketProtocolIgnore]
		private float RegenScale;

		////

		public override Tuple<bool, bool> SyncClientServer => Tuple.Create( true, false );


		////////////////

		protected PlayerBarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }
	}
}
