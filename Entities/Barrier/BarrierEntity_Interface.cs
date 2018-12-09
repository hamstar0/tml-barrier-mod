using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier {
	public partial class BarrierEntity : CustomEntity {
		public bool AdjustBarrierPower( int power ) {
			bool isChanged = this.Power != power;
			
			var behavComp = this.GetComponentByType<BarrierBehaviorEntityComponent>();

			this.Power = power;
			bool isHpChanged = this.AdjustBarrierHpScale( this.HpScale, true );
			bool isRadiusChanged = this.AdjustBarrierRadiusScale( this.RadiusScale, true );
			bool isDefenseChanged = this.AdjustBarrierDefenseScale( this.DefenseScale, true );
			bool isShrinkResistChanged = this.AdjustBarrierShrinkResistScale( behavComp.ShrinkResistScale, true );
			bool isRegenChanged = this.AdjustBarrierRegenScale( this.RegenScale, true );
			
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				string pow = "pow:" + this.Power + ( isChanged ? "*" : "" );
				string hp = "hp%:" + this.HpScale + ( isHpChanged ? "*" : "" );
				string rad = "rad%:" + this.RadiusScale + ( isRadiusChanged ? "*" : "" );
				string def = "def%:" + this.DefenseScale + ( isDefenseChanged ? "*" : "" );
				string res = "resist%:" + behavComp.ShrinkResistScale + ( isShrinkResistChanged ? "*" : "" );
				string reg = "regen%:" + this.RegenScale + ( isRegenChanged ? "*" : "" );

				DebugHelpers.Print( "Barrier "+this.Core.WhoAmI+" scales", pow+", "+hp + ", "+rad+", "+def+", "+res+", "+reg, 20 );
			}

			if( isChanged || isHpChanged || isRadiusChanged || isDefenseChanged || isShrinkResistChanged || isRegenChanged ) {
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
