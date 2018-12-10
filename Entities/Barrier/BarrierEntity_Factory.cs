using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		protected abstract class BarrierEntityFactory<T> : CustomEntityFactory<T> where T : BarrierEntity {
			public abstract float Hp { get; }
			public abstract float Radius { get; }
			public abstract int Defense { get; }
			public abstract float ShrinkResistScale { get; }
			public abstract float RegenRate { get; }
			public abstract Vector2 Center { get; protected set; }


			protected BarrierEntityFactory( Player ownerPlr ) : base( ownerPlr ) { }


			protected override void InitializeEntity( T ent ) {
				if( Main.netMode == 2 ) {
					ent.SyncToAll();
				}
			}
		}
	}
}
