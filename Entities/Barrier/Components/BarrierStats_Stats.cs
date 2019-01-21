using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsEntityComponent : CustomEntityComponent {
		private void ApplyRegen( CustomEntity ent ) {
			if( this.Hp == 0 ) {
				int recoverMaxOffset = (int)( 60f * this.RegenRate );
				int regenRegenThresh = Math.Max( 30, this.MaxRecoverDuration - recoverMaxOffset );

				if( this.RecoverDuration++ >= regenRegenThresh ) {
					this.OnKnockoutRecover( ent );

					this.RecoverDuration = 0;
					this.Hp = 1;
				} else {
					return;
				}
			} else {
				this.RecoverDuration = 0;
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
				this.OnFullRecover( ent );

				this.Hp = this.MaxHp;
			}
		}


		////////////////

		public virtual void OnKnockoutRecover( CustomEntity ent ) { }

		public virtual void OnFullRecover( CustomEntity ent ) { }
	}
}
