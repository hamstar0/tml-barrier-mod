using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier {
	public partial class BarrierEntity : CustomEntity {
		public static float ComputeBarrierMaxHp( int power, float hpScale ) {
			return (float)power * hpScale;
		}

		public static float ComputeBarrierMaxRadius( int power, float radiusScale ) {
			return (float)power * radiusScale;
		}

		public static int ComputeBarrierDefense( int power, float defenseScale ) {
			return (int)( (float)power * defenseScale * 0.125f );
		}

		public static float ComputeBarrierShrinkResist( int power, float resistScale ) {
			return resistScale;
		}

		public static float ComputeBarrierRegen( int power, float regenScale ) {
			return ( regenScale * power * 0.125f ) / 60f;
		}



		////////////////

		public bool AdjustBarrierPower( int power ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			bool isChanged = this.Power != power;

			this.Power = power;
			isChanged = isChanged || this.AdjustBarrierHpScale( this.HpScale, true );
			isChanged = isChanged || this.AdjustBarrierRadiusScale( this.RadiusScale, true );
			isChanged = isChanged || this.AdjustBarrierDefenseScale( this.DefenseScale, true );
			isChanged = isChanged || this.AdjustBarrierShrinkResistScale( behavComp.ShrinkResistScale, true );
			isChanged = isChanged || this.AdjustBarrierRegenScale( this.RegenScale, true );

			if( isChanged ) {
				if( Main.netMode == 1 ) {
					this.SyncToAll();
				}
			}

			return isChanged;
		}

		////

		internal bool AdjustBarrierHpScale( float hpScale, bool skipSync = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			float maxHp = BarrierEntity.ComputeBarrierMaxHp( this.Power, hpScale );
			float hp = behavComp.Hp > maxHp ? maxHp : behavComp.Hp;
			bool isChanged = this.HpScale != hpScale
				|| behavComp.MaxHp != maxHp
				|| behavComp.Hp != hp;

			this.HpScale = hpScale;
			behavComp.MaxHp = maxHp;
			behavComp.Hp = hp;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					this.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool AdjustBarrierRadiusScale( float radiusScale, bool skipSync = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			float maxRadius = BarrierEntity.ComputeBarrierMaxRadius( this.Power, radiusScale );
			float radius = behavComp.Radius > maxRadius ? maxRadius : behavComp.Radius;
			bool isChanged = this.RadiusScale != radiusScale
				|| behavComp.MaxRadius != maxRadius
				|| behavComp.Radius != radius;

			this.RadiusScale = radiusScale;
			behavComp.MaxRadius = maxRadius;
			behavComp.Radius = radius;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					this.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool AdjustBarrierDefenseScale( float defenseScale, bool skipSync = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			int defense = BarrierEntity.ComputeBarrierDefense( this.Power, defenseScale );
			bool isChanged = this.DefenseScale != defenseScale
				|| behavComp.Defense != defense;

			this.DefenseScale = defenseScale;
			behavComp.Defense = defense;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					this.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool AdjustBarrierShrinkResistScale( float resistScale, bool skipSync = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			float resistScaleNew = BarrierEntity.ComputeBarrierShrinkResist( this.Power, resistScale );
			bool isChanged = behavComp.ShrinkResistScale != resistScaleNew;
			
			behavComp.ShrinkResistScale = resistScaleNew;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					this.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool AdjustBarrierRegenScale( float regenScale, bool skipSync = false ) {
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			float regenRate = BarrierEntity.ComputeBarrierRegen( this.Power, regenScale );
			bool isChanged = this.RegenScale != regenScale
				|| behavComp.RegenRate != regenRate;

			this.RegenScale = regenScale;

			behavComp.RegenRate = regenRate;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					this.SyncToAll();
				}
			}

			return isChanged;
		}
	}
}
