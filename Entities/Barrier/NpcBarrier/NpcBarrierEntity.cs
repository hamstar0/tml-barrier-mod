using System;
using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;


namespace Barriers.Entities.Barrier.NpcBarrier {
	public partial class NpcBarrierEntity : BarrierEntity {
		public override Tuple<bool, bool> SyncFromClientServer => Tuple.Create( false, true );



		////////////////

		protected NpcBarrierEntity( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override Color GetBarrierColor( bool baseOnly = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			Color baseColor = new Color( 128, 128, 0 );
			if( baseOnly ) {
				return baseColor;
			}

			float opacity = Math.Min( (float)behavComp.Defense / 64f, 1f );
			opacity = 0.25f + ( opacity * 0.6f );

			return baseColor * opacity;
		}

		public override Color GetEdgeColor( bool baseOnly = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			Color baseColor = new Color( 160, 160, 32 );
			if( baseOnly ) {
				return baseColor;
			}

			return baseColor * behavComp.ShrinkResistScale;
		}
	}
}
