using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.PlayerBarrier.Components {
	public partial class PlayerBarrierBehaviorEntityComponent : CustomEntityComponent {
		public int Power;
		public float HpScale;
		public float RadiusScale;
		public float DefenseScale;
		public float RegenScale;



		////////////////

		protected PlayerBarrierBehaviorEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void OnInitialize() { }


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

		private void UpdateAny( CustomEntity ent ) {
		}
	}
}
