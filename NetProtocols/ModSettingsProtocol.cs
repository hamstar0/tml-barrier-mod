using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;


namespace Barriers.NetProtocols {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public BarriersConfigData ModSettings;



		////////////////

		protected ModSettingsProtocol( PacketProtocolDataConstructorLock ctor_lock ) : base( ctor_lock ) { }

		////////////////

		protected override void SetServerDefaults( int who ) {
			this.ModSettings = BarriersMod.Instance.Config;
		}

		////////////////
		
		protected override void ReceiveReply() {
			BarriersMod.Instance.ConfigJson.SetData( this.ModSettings );
		}
	}
}
