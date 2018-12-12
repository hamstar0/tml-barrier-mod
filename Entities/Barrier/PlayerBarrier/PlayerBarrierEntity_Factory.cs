using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		protected interface IPlayerBarrierEntityFactory : IBarrierEntityFactory {
			int PowerGetSet { get; }
			float HpScaleGetSet { get; }
			float RadiusScaleGetSet { get; }
			float DefenseScaleGetSet { get; }
			float ShrinkResistGetSet { get; }
			float RegenScaleGetSet { get; }
		}



		private class PlayerBarrierEntityFactory : BarrierEntityFactory<PlayerBarrierEntity>, IPlayerBarrierEntityFactory {
			public int PowerGetSet { get; private set; }
			public float HpScaleGetSet { get; private set; }
			public float RadiusScaleGetSet { get; private set; }
			public float DefenseScaleGetSet { get; private set; }
			public float ShrinkResistGetSet { get; private set; }
			public float RegenScaleGetSet { get; private set; }

			public override float HpGet => PlayerBarrierEntity.ComputeBarrierMaxHp( this.PowerGetSet, this.HpScaleGetSet );
			public override float RadiusGet => PlayerBarrierEntity.ComputeBarrierMaxRadius( this.PowerGetSet, this.RadiusScaleGetSet );
			public override int DefenseGet => PlayerBarrierEntity.ComputeBarrierDefense( this.PowerGetSet, this.DefenseScaleGetSet );
			public override float ShrinkResistScaleGet => PlayerBarrierEntity.ComputeBarrierShrinkResist( this.PowerGetSet, this.ShrinkResistGetSet );
			public override float RegenRateGet => PlayerBarrierEntity.ComputeBarrierRegen( this.PowerGetSet, this.RegenScaleGetSet );
			public override Vector2 CenterGetSet { get; protected set; }


			public PlayerBarrierEntityFactory( Player ownerPlr, int power, float hpScale, float radiusScale, float defenseScale, float shrinkResist, float regenScale, Vector2 center )
					: base( ownerPlr ) {
				this.PowerGetSet = power;
				this.HpScaleGetSet = hpScale;
				this.RadiusScaleGetSet = radiusScale;
				this.DefenseScaleGetSet = defenseScale;
				this.ShrinkResistGetSet = shrinkResist;
				this.RegenScaleGetSet = regenScale;
				this.CenterGetSet = center;
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
			int defaultPow = BarriersMod.Instance.Config.DefaultShieldPower;

			return PlayerBarrierEntity.CreatePlayerBarrierEntity( ownerPlr, defaultPow, 1f, 1f, 0f, 0f, ( 1f / 60f ), Main.LocalPlayer.Center );
		}



		////////////////
		
		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var myfactory = (IPlayerBarrierEntityFactory)factory;

			if( myfactory != null ) {
				this.Power = myfactory.PowerGetSet;
				this.HpScale = myfactory.HpScaleGetSet;
				this.RadiusScale = myfactory.RadiusScaleGetSet;
				this.DefenseScale = myfactory.DefenseScaleGetSet;
				this.RegenScale = myfactory.RegenScaleGetSet;
			} else {
				this.Power = BarriersMod.Instance.Config.DefaultShieldPower;
			}

			return base.CreateComponents<T>( factory );
		}
	}
}
