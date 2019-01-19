using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsEntityComponent : CustomEntityComponent {
		protected class BarrierStatsEntityComponentFactory {
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
		}
	}
}
