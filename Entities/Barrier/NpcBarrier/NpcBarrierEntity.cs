using System;
using System.Collections.Generic;
using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.NpcBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		public override bool SyncFromClient => false;
		public override bool SyncFromServer => true;



		////////////////

		private NpcBarrierEntity() : base( null ) { }
		protected NpcBarrierEntity( NpcBarrierEntityConstructor ctor ) : base( ctor ) { }



		////////////////

		protected override IList<CustomEntityComponent> CreateComponents( CustomEntityConstructor ctor ) {
			var mymod = BarriersMod.Instance;
			var myctor = ctor as INpcBarrierEntityFactory;    //NpcBarrierEntityFactory
			IList<CustomEntityComponent> comps = base.CreateComponents( ctor );

			comps.Insert( 0, this.CreateNpcBehaviorComponent( myctor ) );
			comps.Add( new BarrierHitRadiusProjectileEntityComponent( 1, 1 ) );
			comps.Add( new BarrierHitRadiusPlayerEntityComponent() );

			return comps;
		}


		protected override BarrierStatsEntityComponent CreateStatsComponent( IBarrierEntityFactory myfactory ) {
			var mymod = BarriersMod.Instance;

			return new BarrierStatsEntityComponent(
				myfactory?.Hp ?? mymod.Config.NpcBarrierDefaultHpAmount,
				myfactory?.Radius ?? mymod.Config.NpcBarrierDefaultHpAmount,
				myfactory?.Defense ?? mymod.Config.BarrierDefaultDefenseAmount,
				myfactory?.ShrinkResistScale ?? mymod.Config.BarrierDefaultHardnessScaleBaseAmount,
				myfactory?.RegenRate ?? mymod.Config.BarrierDefaultRegenPerTick,
				myfactory?.RecoverDurationHighest ?? mymod.Config.NpcBarrierDefaultRecoverDurationMax
			);
		}

		protected virtual NpcBarrierBehaviorEntityComponent CreateNpcBehaviorComponent( INpcBarrierEntityFactory myfactory ) {
			var mymod = BarriersMod.Instance;
			
			return new NpcBarrierBehaviorEntityComponent( myfactory?.Npc ?? null );
		}
	}
}
