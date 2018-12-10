using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierBehaviorEntityComponent : CustomEntityComponent {
		private void ApplyRegen() {
			if( this.Hp == 0 ) {
				int regenRegenMax = Math.Max( 30, 120 - (int)(60f * this.RegenRate) );

				if( this.RegenRegen++ >= regenRegenMax ) {
					this.RegenRegen = 0;
					this.Hp = 1;
				} else {
					return;
				}
			} else {
				this.RegenRegen = 0;
			}
			
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
