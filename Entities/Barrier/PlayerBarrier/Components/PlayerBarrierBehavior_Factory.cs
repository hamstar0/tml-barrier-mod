using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.PlayerBarrier.Components {
	public partial class PlayerBarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class PlayerBarrierBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : PlayerBarrierBehaviorEntityComponent {
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

			protected override void InitializeComponent( T data ) {
				data.Power = this.Power;
				data.HpScale = this.HpScale;
				data.RadiusScale = this.RadiusScale;
				data.DefenseScale = this.DefenseScale;
				data.RegenScale = this.RegenScale;
			}
		}



		////////////////

		public static PlayerBarrierBehaviorEntityComponent CreateBarrierEntityComponent( int power, float hpScale, float radiusScale, float defenseScale, float regenScale ) {
			var factory = new PlayerBarrierBehaviorEntityComponentFactory<PlayerBarrierBehaviorEntityComponent>( power, hpScale, radiusScale, defenseScale, regenScale );
			PlayerBarrierBehaviorEntityComponent comp = factory.Create();
			
			comp.Power = factory.Power;
			comp.HpScale = factory.HpScale;
			comp.RadiusScale = factory.RadiusScale;
			comp.DefenseScale = factory.DefenseScale;
			comp.RegenScale = factory.RegenScale;

			return comp;
		}
	}
}
