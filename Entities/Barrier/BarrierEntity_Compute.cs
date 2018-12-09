using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;


namespace Barriers.Entities.Barrier {
	public partial class BarrierEntity : CustomEntity {
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
			float minRegen = 3f / 60f;

			return minRegen + (regenScale * (float)power * (minRegen/8f));
		}
	}
}
