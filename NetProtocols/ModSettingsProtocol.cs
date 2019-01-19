using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;


namespace Barriers.NetProtocols {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public BarriersConfigData ModSettings;



		////////////////

		private ModSettingsProtocol() { }

		////////////////

		protected override void InitializeServerSendData( int who ) {
			this.ModSettings = BarriersMod.Instance.Config;
		}

		////////////////
		
		protected override void ReceiveReply() {
			BarriersMod.Instance.ConfigJson.SetData( this.ModSettings );
		}
	}
}
