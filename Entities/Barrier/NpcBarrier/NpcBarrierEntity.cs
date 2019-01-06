using System.Collections.Generic;
using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.NpcBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		public override bool SyncFromClient => false;
		public override bool SyncFromServer => true;



		////////////////

		protected NpcBarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }



		////////////////

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var mymod = BarriersMod.Instance;
			var myfactory = factory as INpcBarrierEntityFactory;    //NpcBarrierEntityFactory
			IList<CustomEntityComponent> comps = base.CreateComponents<T>( factory );

			comps.Insert( 0, this.CreateNpcBehaviorComponent( myfactory ) );
			comps.Add( BarrierHitRadiusProjectileEntityComponent.CreateBarrierHitRadiusProjectileEntityComponent( 1, 1 ) );
			comps.Add( BarrierHitRadiusPlayerEntityComponent.CreateBarrierHitRadiusPlayerEntityComponent() );

			return comps;
		}


		protected virtual NpcBarrierBehaviorEntityComponent CreateNpcBehaviorComponent( INpcBarrierEntityFactory myfactory ) {
			var mymod = BarriersMod.Instance;

			return NpcBarrierBehaviorEntityComponent.CreateNpcBarrierBehaviorEntityComponent(
				myfactory?.Npc ?? null,
				myfactory?.Hp ?? mymod.Config.NpcBarrierHpBaseAmount,
				myfactory?.Radius ?? mymod.Config.NpcBarrierHpBaseAmount,
				myfactory?.Defense ?? mymod.Config.BarrierDefenseBaseAmount,
				myfactory?.ShrinkResistScale ?? mymod.Config.BarrierHardnessScaleBaseAmount,
				myfactory?.RegenRate ?? mymod.Config.BarrierRegenBaseAmount
			);
		}
	}
}
