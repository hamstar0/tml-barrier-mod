using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		private class PlayerBarrierEntityFactory : BarrierEntityFactory<PlayerBarrierEntity> {
			public int Power;
			public float HpScale;
			public float RadiusScale;
			public float DefenseScale;
			public float ShrinkResist;
			public float RegenScale;

			public override float Hp => PlayerBarrierEntity.ComputeBarrierMaxHp( this.Power, this.HpScale );
			public override float Radius => PlayerBarrierEntity.ComputeBarrierMaxRadius( this.Power, this.RadiusScale );
			public override int Defense => PlayerBarrierEntity.ComputeBarrierDefense( this.Power, this.DefenseScale );
			public override float ShrinkResistScale => PlayerBarrierEntity.ComputeBarrierShrinkResist( this.Power, this.RegenScale );
			public override float RegenRate => PlayerBarrierEntity.ComputeBarrierRegen( this.Power, this.RegenScale );
			public override Vector2 Center { get; protected set; }


			public PlayerBarrierEntityFactory( Player ownerPlr, int power, float hpScale, float radiusScale, float defenseScale, float shrinkResist, float regenScale, Vector2 center )
					: base( ownerPlr ) {
				this.Power = power;
				this.HpScale = hpScale;
				this.RadiusScale = radiusScale;
				this.DefenseScale = defenseScale;
				this.ShrinkResist = shrinkResist;
				this.RegenScale = regenScale;
				this.Center = center;
			}

			////

			protected override void InitializeEntity( PlayerBarrierEntity ent ) {
				if( Main.netMode == 2 ) {
					ent.SyncToAll();
				}
			}
		}



		////////////////

		public static PlayerBarrierEntity CreatePlayerBarrierEntity( Player ownerPlr, int power, float hpScale, float radiusScale, float defenseScale, float shrinkResist, float regenScale, Vector2 center ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + center );
			}

			var factory = new PlayerBarrierEntityFactory( ownerPlr, power, hpScale, radiusScale, defenseScale, shrinkResist, regenScale, center );
			return factory.Create();
		}

		internal static PlayerBarrierEntity CreateDefaultPlayerBarrierEntity( Player ownerPlr ) {
			return PlayerBarrierEntity.CreatePlayerBarrierEntity( ownerPlr, 128, 1f, 1f, 0f, 0f, ( 1f / 60f ), Main.LocalPlayer.Center );
		}
	}
}
