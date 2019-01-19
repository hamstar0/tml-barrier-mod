using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.PlayerBarrier.Components {
	public partial class PlayerBarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class PlayerBarrierBehaviorEntityComponentFactory {
			public int Power;
			public float HpScale;
			public float RadiusScale;
			public float DefenseScale;
			public float RegenScale;

			public PlayerBarrierBehaviorEntityComponentFactory( int power, float hpScale, float radiusScale, float defenseScale, float regenScale ) {
				this.Power = power;
				this.HpScale = hpScale;
				this.RadiusScale = radiusScale;
				this.DefenseScale = defenseScale;
				this.RegenScale = regenScale;
			}
		}


		////////////////
		
		public static PlayerBarrierBehaviorEntityComponent CreatePlayerBarrierBehaviorEntityComponent(
				int power, float hpScale, float radiusScale, float defenseScale, float regenScale ) {
			var factory = new PlayerBarrierBehaviorEntityComponentFactory( power, hpScale, radiusScale, defenseScale, regenScale );
			return PlayerBarrierBehaviorEntityComponent.CreateDefault<PlayerBarrierBehaviorEntityComponent>( factory );
		}



		////////////////

		public int Power;
		public float HpScale;
		public float RadiusScale;
		public float DefenseScale;
		public float RegenScale;



		////////////////

		protected PlayerBarrierBehaviorEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void OnInitialize() { }

		////

		protected override Type GetMyFactoryType() {
			return typeof( PlayerBarrierBehaviorEntityComponentFactory );
		}


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
