using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		public float GetHp() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.Hp;
		}

		public float GetMaxHp() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.MaxHp;
		}

		public float GetRadius() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.Radius;
		}

		public float GetMaxRadius() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.MaxRadius;
		}

		public float GetRegenRate() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.RegenRate;
		}
		
		public int GetDefense() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.Defense;
		}

		public float GetShrinkResistScale() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.ShrinkResistScale;
		}

		public int GetRecoverDuration() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.RecoverDuration;
		}

		public int GetMaxRecoverDuration() {
			var statsComp = this.GetComponentByType<BarrierStatsEntityComponent>();
			return statsComp.MaxRecoverDuration;
		}
	}
}
