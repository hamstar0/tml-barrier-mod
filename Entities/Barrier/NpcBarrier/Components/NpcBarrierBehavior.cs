using System;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using Newtonsoft.Json;
using Terraria;


namespace Barriers.Entities.Barrier.NpcBarrier.Components {
	public partial class NpcBarrierBehaviorEntityComponent : CustomEntityComponent {
		[JsonIgnore]
		[PacketProtocolIgnore]
		public NPC Npc;



		////////////////

		private NpcBarrierBehaviorEntityComponent() { }
		public NpcBarrierBehaviorEntityComponent( NPC npc ) {
			this.Npc = npc;
		}

		////

		protected override void OnClone() { }


		////////////////

		public override void UpdateSingle( CustomEntity ent ) {
			//this.UpdateLocal( ent );
			this.UpdateAny( ent );
		}

		public override void UpdateClient( CustomEntity ent ) {
			//this.UpdateLocal( ent );
			this.UpdateAny( ent );
		}

		public override void UpdateServer( CustomEntity ent ) {
			this.UpdateAny( ent );
		}


		////////////////

		/*private void UpdateLocal( CustomEntity ent ) {
			var plr = Main.LocalPlayer;
			float dist = Vector2.Distance( plr.Center, myent.Core.Center );

			if( dist < myent.Core.width ) {
				var myplayer = plr.GetModPlayer<BarriersPlayer>();

				myplayer.NoBuilding = true;
			}
		}*/
		
		private void UpdateAny( CustomEntity ent ) { }
	}
}
