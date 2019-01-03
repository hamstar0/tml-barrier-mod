using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier.NpcBarrier.Components {
	public partial class NpcBarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class NpcBarrierBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : NpcBarrierBehaviorEntityComponent {
			public float Hp;
			public float Radius;
			public int Defense;
			public float ShrinkResistScale;
			public float RegenRate;

			
			public NpcBarrierBehaviorEntityComponentFactory( float hp, float radius, int defense, float shrinkResistScale, float regenRate ) {
				this.Hp = hp;
				this.Radius = radius;
				this.Defense = defense;
				this.ShrinkResistScale = shrinkResistScale;
				this.RegenRate = regenRate;
			}

			protected override void InitializeComponent( T data ) {
				data.Hp = this.Hp;
				data.Radius = this.Radius;
				data.Defense = this.Defense;
				data.ShrinkResistScale = this.ShrinkResistScale;
				data.RegenRate = this.RegenRate;
			}
		}



		////////////////

		public static NpcBarrierBehaviorEntityComponent CreateNpcBarrierBehaviorEntityComponent( NPC npc, float hp, float radius, int defense, float shrinkResistScale, float regenRate ) {
			var factory = new NpcBarrierBehaviorEntityComponentFactory<NpcBarrierBehaviorEntityComponent>( hp, radius, defense, shrinkResistScale, regenRate );
			NpcBarrierBehaviorEntityComponent comp = factory.Create();

			comp.Hp = factory.Hp;
			comp.Radius = factory.Defense;
			comp.Defense = factory.Defense;
			comp.ShrinkResistScale = factory.ShrinkResistScale;
			comp.RegenRate = factory.RegenRate;

			return comp;
		}
	}
}
