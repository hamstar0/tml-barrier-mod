using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierBehaviorEntityComponent : CustomEntityComponent {
		private void ApplyRegen( BarrierEntity myent ) {
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();

			if( behavComp.Radius < behavComp.MaxRadius ) {
				behavComp.Radius += behavComp.RegenRate;
			}
			if( behavComp.Radius > behavComp.MaxRadius ) {
				behavComp.Radius = behavComp.MaxRadius;
			}

			if( behavComp.Hp < behavComp.MaxHp ) {
				behavComp.Hp += behavComp.RegenRate;
			}
			if( behavComp.Hp > behavComp.MaxHp ) {
				behavComp.Hp = behavComp.MaxHp;
			}
		}
	}
}
