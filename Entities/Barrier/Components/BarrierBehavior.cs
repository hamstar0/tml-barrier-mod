using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier.Components {
	public class BarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class BarrierBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : BarrierBehaviorEntityComponent {
			public BarrierTypes[] BarrierTypes;
			public float Radius;
			public float RadiusRegenRate;


			public BarrierBehaviorEntityComponentFactory( BarrierTypes[] barrierTypes, float radius, float regenRate ) {
				this.BarrierTypes = barrierTypes;
				this.Radius = radius;
				this.RadiusRegenRate = regenRate;
			}

			protected override void InitializeComponent( T data ) {
				data.BarrierLayers = this.BarrierTypes;
				data.Radius = this.Radius;
				data.MaxRadius = this.Radius;
				data.RadiusRegenRate = this.RadiusRegenRate;
			}
		}



		////////////////

		public static BarrierBehaviorEntityComponent CreateBarrierEntityComponent( BarrierTypes[] barrierTypes, float radius, float regenRate ) {
			var factory = new BarrierBehaviorEntityComponentFactory<BarrierBehaviorEntityComponent>( barrierTypes, radius, regenRate );
			return factory.Create();
		}



		////////////////

		public BarrierTypes[] BarrierLayers;

		public float MaxRadius;
		public float Radius;
		public float RadiusRegenRate;



		////////////////

		protected BarrierBehaviorEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override void UpdateSingle( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.UpdateLocal( myent );
			this.UpdateAny( myent );
		}

		public override void UpdateClient( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.UpdateLocal( myent );
			this.UpdateAny( myent );
		}

		public override void UpdateServer( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.UpdateAny( myent );
		}


		////////////////

		private void UpdateLocal( BarrierEntity myent ) {
			/*var plr = Main.LocalPlayer;
			float dist = Vector2.Distance( plr.Center, myent.Core.Center );

			if( dist < myent.Core.width ) {
				var myplayer = plr.GetModPlayer<BarriersPlayer>();

				myplayer.NoBuilding = true;
			}*/
		}

		private void UpdateAny( BarrierEntity myent ) {
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();

			if( behavComp.Radius < behavComp.MaxRadius ) {
				behavComp.Radius += behavComp.RadiusRegenRate;
			}
			if( behavComp.Radius > behavComp.MaxRadius ) {
				behavComp.Radius = behavComp.MaxRadius;
			}
		}
	}
}
