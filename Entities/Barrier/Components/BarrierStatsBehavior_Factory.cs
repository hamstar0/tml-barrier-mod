using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsBehaviorEntityComponent : CustomEntityComponent {
		protected class BarrierStatsBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : BarrierStatsBehaviorEntityComponent {
			public float Hp;
			public float Radius;
			public float RadiusRegenRate;
			public int Defense;
			public float ShrinkResist;
			public int RegenRegenDurationHighest;


			public BarrierStatsBehaviorEntityComponentFactory( float hp, float radius, int defense, float regenRate, float shrinkResist, int regenRegenDurationHighest ) {
				this.Hp = hp;
				this.Radius = radius;
				this.RadiusRegenRate = regenRate;
				this.Defense = defense;
				this.ShrinkResist = shrinkResist;
				this.RegenRegenDurationHighest = regenRegenDurationHighest;
			}

			protected sealed override void InitializeComponent( T data ) {
				data.Hp = this.Hp;
				data.MaxHp = this.Hp;
				data.Radius = this.Radius;
				data.MaxRadius = this.Radius;
				data.RegenRate = this.RadiusRegenRate;
				data.Defense = this.Defense;
				data.ShrinkResistScale = this.ShrinkResist;
				data.RegenRegenDurationHighest = this.RegenRegenDurationHighest;
				this.InitializeBarrierStatsBehaviorComponent( data );
			}

			protected virtual void InitializeBarrierStatsBehaviorComponent( T data ) { }
		}



		////////////////

		public static BarrierStatsBehaviorEntityComponent CreateBarrierStatsBehaviorEntityComponent( float hp, float radius, int defense, float regenRate, float shrinkResist, int regenRegenDurationHighest ) {
			var factory = new BarrierStatsBehaviorEntityComponentFactory<BarrierStatsBehaviorEntityComponent>( hp, radius, defense, regenRate, shrinkResist, regenRegenDurationHighest );
			return factory.Create();
		}
	}
}
