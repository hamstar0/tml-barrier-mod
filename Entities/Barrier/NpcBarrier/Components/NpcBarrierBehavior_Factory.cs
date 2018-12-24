using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.NpcBarrier.Components {
	public partial class NpcBarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class NpcBarrierBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : NpcBarrierBehaviorEntityComponent {
			public float Hp;
			public float Radius;
			public int Defense;
			public float ShrinkResistScale;
			public float RegenScale;

			
			public NpcBarrierBehaviorEntityComponentFactory( float hp, float radius, int defense, float shrinkResistScale, float regenScale ) {
				this.Hp = hp;
				this.Radius = radius;
				this.Defense = defense;
				this.ShrinkResistScale = shrinkResistScale;
				this.RegenScale = regenScale;
			}

			protected override void InitializeComponent( T data ) {
				data.Hp = this.Hp;
				data.Radius = this.Radius;
				data.Defense = this.Defense;
				data.ShrinkResistScale = this.ShrinkResistScale;
				data.RegenScale = this.RegenScale;
			}
		}



		////////////////

		public static NpcBarrierBehaviorEntityComponent CreateBarrierEntityComponent( float hp, float radius, int defense, float shrinkResistScale, float regenScale ) {
			var factory = new NpcBarrierBehaviorEntityComponentFactory<NpcBarrierBehaviorEntityComponent>( hp, radius, defense, shrinkResistScale, regenScale );
			NpcBarrierBehaviorEntityComponent comp = factory.Create();

			comp.Hp = factory.Hp;
			comp.Radius = factory.Defense;
			comp.Defense = factory.Defense;
			comp.ShrinkResistScale = factory.ShrinkResistScale;
			comp.RegenScale = factory.RegenScale;

			return comp;
		}
	}
}
