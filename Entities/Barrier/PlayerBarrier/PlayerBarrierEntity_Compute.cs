using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier.PlayerBarrier {
	public partial class PlayerBarrierEntity : BarrierEntity {
		public static float ComputeBarrierMaxHp( int power, float hpScale ) {
			return PlayerBarrierEntity.ComputeBarrierMaxRadius( power, hpScale );
		}


		public static float ComputeBarrierMaxRadius( int power, float radiusScale ) {
			if( power <= 0 ) {
				return 0f;
			}

			float minRadius = 32f;
			float minScale = minRadius / (float)power;
			float diff = 1f - minScale;
			radiusScale = minScale + ( diff * radiusScale );

			return (float)power * radiusScale;
		}


		public static int ComputeBarrierDefense( int power, float defenseScale ) {
			float defPerPowerUnit = 0.25f;
			int def = (int)( (float)power * defenseScale * defPerPowerUnit );
			return def + BarriersMod.Instance.Config.BarrierDefenseBaseAmount;
		}


		public static float ComputeBarrierShrinkResist( int power, float resistScale ) {
			return resistScale;
		}


		public static float ComputeBarrierRegen( int power, float regenScale ) {
			var mymod = BarriersMod.Instance;
			float regen = mymod.Config.BarrierRegenBaseAmount;
			float regenPerPowerUnit = regen / 24f;
			regen += regenScale * (float)power * regenPerPowerUnit;

			return regen * mymod.Config.BarrierRegenMultiplier;
		}
	}
}
