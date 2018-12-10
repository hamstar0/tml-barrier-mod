﻿using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier {
	public partial class BarrierEntity : CustomEntity {
		public const float BaseRegen = 3f / 60f;



		////////////////

		public static float ComputeBarrierMaxHp( int power, float hpScale ) {
			return BarrierEntity.ComputeBarrierMaxRadius( power, hpScale );
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
			return (int)( ((float)power * defenseScale) / 8f );
		}


		public static float ComputeBarrierShrinkResist( int power, float resistScale ) {
			return resistScale;
		}


		public static float ComputeBarrierRegen( int power, float regenScale ) {
			float regenPerPowerUnit = BarrierEntity.BaseRegen / 12f;
			float regen = BarrierEntity.BaseRegen;
			regen += regenScale * (float)power * regenPerPowerUnit;

			return regen * BarriersMod.Instance.Config.BarrierRegenMultiplier;
		}
	}
}