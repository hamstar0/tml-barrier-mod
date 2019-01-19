using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		protected interface IPlayerBarrierEntityFactory : IBarrierEntityFactory {
			int Power { get; }
			float HpScale { get; }
			float RadiusScale { get; }
			float DefenseScale { get; }
			float ShrinkResist { get; }
			float RegenScale { get; }
		}



		protected class PlayerBarrierEntityFactory : BarrierEntityFactory, IPlayerBarrierEntityFactory {
			public int Power { get; }
			public float HpScale { get; }
			public float RadiusScale { get; }
			public float DefenseScale { get; }
			public float ShrinkResist { get; }
			public float RegenScale { get; }

			////

			public override float Hp => PlayerBarrierEntity.ComputeBarrierMaxHp( this.Power, this.HpScale );
			public override float Radius => PlayerBarrierEntity.ComputeBarrierMaxRadius( this.Power, this.RadiusScale );
			public override int Defense => PlayerBarrierEntity.ComputeBarrierDefense( this.Power, this.DefenseScale );
			public override float ShrinkResistScale => PlayerBarrierEntity.ComputeBarrierShrinkResist( this.Power, this.ShrinkResist );
			public override float RegenRate => PlayerBarrierEntity.ComputeBarrierRegen( this.Power, this.RegenScale );
			public override int RegenRegenDurationHighest => BarriersMod.Instance.Config.PlayerBarrierDefaultRegenRegenDurationHighest;
			public override Color BarrierBodyColor { get; }
			public override Color BarrierEdgeColor { get; }
			public override Vector2 Center { get; }

			////////////////

			public PlayerBarrierEntityFactory( Player ownerPlr, int power, float hpScale, float radiusScale, float defenseScale,
					float shrinkResist, float regenScale, Vector2 center, Color? bodyColor = null, Color? edgeColor = null ) :
					base( ownerPlr ) {
				this.Power = power;
				this.HpScale = hpScale;
				this.RadiusScale = radiusScale;
				this.DefenseScale = defenseScale;
				this.ShrinkResist = shrinkResist;
				this.RegenScale = regenScale;
				this.BarrierBodyColor = bodyColor ?? new Color( 0, 128, 0 );
				this.BarrierEdgeColor = edgeColor ?? new Color( 32, 160, 32 );
				this.Center = center;
			}
		}



		////////////////

		public static PlayerBarrierEntity CreatePlayerBarrierEntity( Player ownerPlr, int power, float hpScale, float radiusScale,
				float defenseScale, float shrinkResist, float regenScale, Vector2 center,
				Color? bodyColor = null, Color? edgeColor = null ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + center );
			}

			var factory = new PlayerBarrierEntityFactory( ownerPlr, power, hpScale, radiusScale, defenseScale, shrinkResist, regenScale, center, bodyColor, edgeColor );
			return PlayerBarrierEntity.CreateDefault<PlayerBarrierEntity>( factory );
		}

		internal static PlayerBarrierEntity CreateDefaultPlayerBarrierEntity( Player ownerPlr ) {
			var mymod = BarriersMod.Instance;
			int defaultPow = mymod.Config.PlayerBarrierDefaultShieldPower;
			float defaultRegen = mymod.Config.BarrierRegenBaseAmount;

			return PlayerBarrierEntity.CreatePlayerBarrierEntity( ownerPlr, defaultPow, 1f, 1f, 0f, 0f, defaultRegen, Main.LocalPlayer.Center );
		}
	}
}
