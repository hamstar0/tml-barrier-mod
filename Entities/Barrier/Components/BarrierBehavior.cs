using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	class BarrierBehaviorEntityComponent : CustomEntityComponent {
		private class BarrierBehaviorEntityComponentFactory : CustomEntityComponentFactory<BarrierBehaviorEntityComponent> {
			public BarrierTypes[] BarrierTypes;
			public float Radius;


			public BarrierBehaviorEntityComponentFactory( BarrierTypes[] barrierTypes, float radius ) {
				this.BarrierTypes = barrierTypes;
				this.Radius = radius;
			}

			protected override void InitializeComponent( BarrierBehaviorEntityComponent data ) {
				data.BarrierLayers = this.BarrierTypes;
				data.Radius = this.Radius;
			}
		}



		////////////////

		public static BarrierBehaviorEntityComponent CreateBarrierEntityComponent( BarrierTypes[] barrierTypes, float radius ) {
			var factory = new BarrierBehaviorEntityComponentFactory( barrierTypes, radius );
			return factory.Create();
		}



		////////////////
		
		public BarrierTypes[] BarrierLayers;
		public float Radius;



		////////////////

		protected BarrierBehaviorEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override void UpdateSingle( CustomEntity myent ) {
			this.UpdateLocal( myent );
		}

		public override void UpdateClient( CustomEntity myent ) {
			this.UpdateLocal( myent );
		}

		public override void UpdateServer( CustomEntity myent ) {
		}


		////////////////

		private void UpdateLocal( CustomEntity myent ) {
			var plr = Main.LocalPlayer;
			float dist = Vector2.Distance( plr.Center, myent.Core.Center );
			
			if( dist < myent.Core.width ) {
				var myplayer = plr.GetModPlayer<BarriersPlayer>();

				myplayer.NoBuilding = true;
			}
		}
	}
}
