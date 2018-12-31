﻿using System;
using System.Collections.Generic;
using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.PlayerBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Newtonsoft.Json;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition1 = 0;
		[JsonIgnore]
		[PacketProtocolIgnore]
		internal int UiRadialPosition2 = 0;

		////

		public override Tuple<bool, bool> SyncFromClientServer => Tuple.Create( true, false );



		////////////////

		protected PlayerBarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var mymod = BarriersMod.Instance;
			var myfactory = factory as IPlayerBarrierEntityFactory; //PlayerBarrierEntityFactory;
			IList<CustomEntityComponent> comps = base.CreateComponents<T>( factory );

			comps.Insert( 0, this.CreatePlayerBehaviorComponent( myfactory ) );
			comps.Add( BarrierHitRadiusProjectileEntityComponent.CreateBarrierHitRadiusProjectileEntityComponent( -1, 1 ) );
			comps.Add( BarrierHitRadiusNpcEntityComponent.CreateBarrierHitRadiusNpcEntityComponent( false ) );

			return comps;
		}


		protected virtual PlayerBarrierBehaviorEntityComponent CreatePlayerBehaviorComponent( IPlayerBarrierEntityFactory myfactory ) {
			var mymod = BarriersMod.Instance;
			PlayerBarrierBehaviorEntityComponent behavComp;

			if( myfactory != null ) {
				behavComp = PlayerBarrierBehaviorEntityComponent.CreateBarrierEntityComponent(
					myfactory.Power,
					myfactory.HpScale,
					myfactory.RadiusScale,
					myfactory.DefenseScale,
					myfactory.RegenScale
				);
			} else {
				int defaultPow = mymod.Config.PlayerBarrierDefaultShieldPower;
				float defaultRegen = mymod.Config.BarrierRegenBaseAmount;

				behavComp = PlayerBarrierBehaviorEntityComponent.CreateBarrierEntityComponent( defaultPow, 1f, 1f, 0f, defaultRegen );
			}
			return behavComp;
		}
	}
}
