using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier {
	public abstract partial class BarrierEntity : CustomEntity {
		protected interface IBarrierEntityFactory {
			float Hp { get; }
			float Radius { get; }
			int Defense { get; }
			float ShrinkResistScale { get; }
			float RegenRate { get; }
			Vector2 Center { get; }
		}



		protected abstract class BarrierEntityFactory<T> : CustomEntityFactory<T>, IBarrierEntityFactory where T : BarrierEntity {
			public abstract float Hp { get; }
			public abstract float Radius { get; }
			public abstract int Defense { get; }
			public abstract float ShrinkResistScale { get; }
			public abstract float RegenRate { get; }
			public abstract Vector2 Center { get; }


			protected BarrierEntityFactory( Player ownerPlr ) : base( ownerPlr ) { }
		}
	}
}
