using System;
using System.Collections.Generic;
using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.NpcBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		public override Tuple<bool, bool> SyncFromClientServer => Tuple.Create( false, true );



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
			NpcBarrierBehaviorEntityComponent behavComp;

			if( myfactory != null ) {
				behavComp = NpcBarrierBehaviorEntityComponent.CreateBarrierEntityComponent(
					myfactory.Npc,
					myfactory.Hp,
					myfactory.Radius,
					myfactory.Defense,
					myfactory.ShrinkResistScale,
					myfactory.RegenRate
				);
			} else {
				behavComp = NpcBarrierBehaviorEntityComponent.CreateBarrierEntityComponent(
					null,
					mymod.Config.NpcBarrierHpBaseAmount,
					mymod.Config.NpcBarrierHpBaseAmount,
					mymod.Config.BarrierDefenseBaseAmount,
					mymod.Config.BarrierHardnessDamageDeflectionMaximumAmount,
					mymod.Config.BarrierRegenBaseAmount
				);
			}

			return behavComp;
		}
	}
}
