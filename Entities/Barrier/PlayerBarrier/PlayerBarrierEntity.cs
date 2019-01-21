using System;
using System.Collections.Generic;
using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.PlayerBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
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
		
		public override bool SyncFromClient => true;
		public override bool SyncFromServer => false;



		////////////////

		private PlayerBarrierEntity() : base( null ) { }
		protected PlayerBarrierEntity( PlayerBarrierEntityFactory ctor ) : base( ctor ) { }


		////////////////

		protected override IList<CustomEntityComponent> CreateComponents( CustomEntityConstructor factory ) {
			var mymod = BarriersMod.Instance;
			var myfactory = factory as IPlayerBarrierEntityFactory; //PlayerBarrierEntityFactory;
			IList<CustomEntityComponent> comps = base.CreateComponents( factory );

			comps.Insert( 0, this.CreatePlayerBehaviorComponent( myfactory ) );
			comps.Add( new BarrierHitRadiusProjectileEntityComponent( -1, 1 ) );
			comps.Add( new BarrierHitRadiusNpcEntityComponent( false ) );

			return comps;
		}


		protected virtual PlayerBarrierBehaviorEntityComponent CreatePlayerBehaviorComponent( IPlayerBarrierEntityFactory myfactory ) {
			var mymod = BarriersMod.Instance;

			return new PlayerBarrierBehaviorEntityComponent(
				myfactory?.Power ?? mymod.Config.PlayerBarrierDefaultShieldPower,
				myfactory?.HpScale ?? 1f,
				myfactory?.RadiusScale ?? 1f,
				myfactory?.DefenseScale ?? 0f,
				myfactory?.RegenScale ?? mymod.Config.BarrierDefaultRegenPerTick
			);
		}
	}
}
