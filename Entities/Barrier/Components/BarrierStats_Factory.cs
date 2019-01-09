using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsEntityComponent : CustomEntityComponent {
		protected class BarrierStatsEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : BarrierStatsEntityComponent {
			public float Hp;
			public float Radius;
			public float RadiusRegenRate;
			public int Defense;
			public float ShrinkResist;
			public int RegenRegenDurationHighest;


			public BarrierStatsEntityComponentFactory( float hp, float radius, int defense, float regenRate, float shrinkResist, int regenRegenDurationHighest ) {
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
				this.InitializeBarrierStatsComponent( data );
			}

			protected virtual void InitializeBarrierStatsComponent( T data ) { }
		}



		////////////////

		public static BarrierStatsEntityComponent CreateBarrierStatsEntityComponent( float hp, float radius, int defense, float regenRate, float shrinkResist, int regenRegenDurationHighest ) {
			var factory = new BarrierStatsEntityComponentFactory<BarrierStatsEntityComponent>( hp, radius, defense, regenRate, shrinkResist, regenRegenDurationHighest );
			return factory.Create();
		}
	}
}
