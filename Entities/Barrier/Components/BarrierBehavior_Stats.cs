using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierBehaviorEntityComponent : CustomEntityComponent {
		private void ApplyRegen( BarrierEntity myent ) {
			if( this.Radius < this.MaxRadius ) {
				this.Radius += this.RegenRate;
			}
			if( this.Radius > this.MaxRadius ) {
				this.Radius = this.MaxRadius;
			}

			if( this.Hp < this.MaxHp ) {
				this.Hp += this.RegenRate;
			}
			if( this.Hp > this.MaxHp ) {
				this.Hp = this.MaxHp;
			}
		}
	}
}
