using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.Components {
	public class BarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class BarrierBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : BarrierBehaviorEntityComponent {
			public float Radius;
			public float RadiusRegenRate;
			public int Defense;
			public float ShrinkResist;


			public BarrierBehaviorEntityComponentFactory( float radius, float regenRate, int defense, float shrinkResist ) {
				this.Radius = radius;
				this.RadiusRegenRate = regenRate;
				this.Defense = defense;
				this.ShrinkResist = shrinkResist;
			}

			protected override void InitializeComponent( T data ) {
				data.Radius = this.Radius;
				data.MaxRadius = this.Radius;
				data.RadiusRegenRate = this.RadiusRegenRate;
				data.Defense = this.Defense;
				data.ShrinkResist = this.ShrinkResist;
			}
		}



		////////////////

		public static BarrierBehaviorEntityComponent CreateBarrierEntityComponent( float radius, float regenRate, int defense, float shrinkResist ) {
			var factory = new BarrierBehaviorEntityComponentFactory<BarrierBehaviorEntityComponent>( radius, regenRate, defense, shrinkResist );
			return factory.Create();
		}



		////////////////
		
		public float MaxRadius;
		public float Radius;
		public float RadiusRegenRate;
		public int Defense;
		public float ShrinkResist;



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
