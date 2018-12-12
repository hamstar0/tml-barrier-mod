using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		protected interface IBarrierEntityFactory {
			float HpGet { get; }
			float RadiusGet { get; }
			int DefenseGet { get; }
			float ShrinkResistScaleGet { get; }
			float RegenRateGet { get; }
			Vector2 CenterGetSet { get; }
		}



		protected abstract class BarrierEntityFactory<T> : CustomEntityFactory<T>, IBarrierEntityFactory where T : BarrierEntity {
			public abstract float HpGet { get; }
			public abstract float RadiusGet { get; }
			public abstract int DefenseGet { get; }
			public abstract float ShrinkResistScaleGet { get; }
			public abstract float RegenRateGet { get; }
			public abstract Vector2 CenterGetSet { get; protected set; }


			protected BarrierEntityFactory( Player ownerPlr ) : base( ownerPlr ) { }


			protected override void InitializeEntity( T ent ) {
				if( Main.netMode == 2 ) {
					ent.SyncToAll();
				}
			}
		}
	}
}
