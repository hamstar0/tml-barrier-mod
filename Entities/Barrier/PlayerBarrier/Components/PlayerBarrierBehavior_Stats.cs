using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier.PlayerBarrier.Components {
	public partial class PlayerBarrierBehaviorEntityComponent : CustomEntityComponent {
		public bool SetBarrierPower( PlayerBarrierEntity myent, int power, bool skipSync = false ) {
			int pwrChange = power - this.Power;
			
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();
			
			this.Power = power;
			bool isHpChanged = this.SetBarrierHpScale( myent, this.HpScale, true );
			bool isRadiusChanged = this.SetBarrierRadiusScale( myent, this.RadiusScale, true );
			bool isDefenseChanged = this.SetBarrierDefenseScale( myent, this.DefenseScale, true );
			bool isShrinkResistChanged = this.SetBarrierShrinkResistScale( myent, behavComp.ShrinkResistScale, true );
			bool isRegenChanged = this.SetBarrierRegenScale( myent, this.RegenScale, true );
			
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				string pow = "pow:" + this.Power + ( pwrChange!=0 ? "*" : "" );
				string hp = "hp%:" + this.HpScale + ( isHpChanged ? "*" : "" );
				string rad = "rad%:" + this.RadiusScale + ( isRadiusChanged ? "*" : "" );
				string def = "def%:" + this.DefenseScale + ( isDefenseChanged ? "*" : "" );
				string res = "resist%:" + behavComp.ShrinkResistScale + ( isShrinkResistChanged ? "*" : "" );
				string reg = "regen%:" + this.RegenScale + ( isRegenChanged ? "*" : "" );
				
				DebugHelpers.Print( "Barrier "+ myent.Core.WhoAmI+" scales", pow+", "+hp+", "+rad+", "+def+", "+res+", "+reg, 20 );
			}

			if( pwrChange != 0 || isHpChanged || isRadiusChanged || isDefenseChanged || isShrinkResistChanged || isRegenChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
				return true;
			}
			return false;
		}

		////

		internal bool SetBarrierHpScale( PlayerBarrierEntity myent, float hpScale, bool skipSync = false ) {
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();

			float maxHp = PlayerBarrierEntity.ComputeBarrierMaxHp( this.Power, hpScale );
			float hp = behavComp.Hp > maxHp ? maxHp : behavComp.Hp;
			bool isChanged = this.HpScale != hpScale
				|| behavComp.MaxHp != maxHp
				|| behavComp.Hp != hp;

			this.HpScale = hpScale;
			behavComp.MaxHp = maxHp;
			behavComp.Hp = hp;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool SetBarrierRadiusScale( PlayerBarrierEntity myent, float radiusScale, bool skipSync = false ) {
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();

			float maxRadius = PlayerBarrierEntity.ComputeBarrierMaxRadius( this.Power, radiusScale );
			float radius = behavComp.Radius > maxRadius ? maxRadius : behavComp.Radius;
			bool isChanged = this.RadiusScale != radiusScale
				|| behavComp.MaxRadius != maxRadius
				|| behavComp.Radius != radius;

			this.RadiusScale = radiusScale;
			behavComp.MaxRadius = maxRadius;
			behavComp.Radius = radius;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool SetBarrierDefenseScale( PlayerBarrierEntity myent, float defenseScale, bool skipSync = false ) {
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();

			int defense = PlayerBarrierEntity.ComputeBarrierDefense( this.Power, defenseScale );
			bool isChanged = this.DefenseScale != defenseScale
				|| behavComp.Defense != defense;

			this.DefenseScale = defenseScale;
			behavComp.Defense = defense;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool SetBarrierShrinkResistScale( PlayerBarrierEntity myent, float resistScale, bool skipSync = false ) {
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();

			float resistScaleNew = PlayerBarrierEntity.ComputeBarrierShrinkResist( this.Power, resistScale );
			bool isChanged = behavComp.ShrinkResistScale != resistScaleNew;
			
			behavComp.ShrinkResistScale = resistScaleNew;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool SetBarrierRegenScale( PlayerBarrierEntity myent, float regenScale, bool skipSync = false ) {
			var behavComp = myent.GetComponentByType<BarrierBehaviorEntityComponent>();

			float regenRate = PlayerBarrierEntity.ComputeBarrierRegen( this.Power, regenScale );
			bool isChanged = this.RegenScale != regenScale
				|| behavComp.RegenRate != regenRate;

			this.RegenScale = regenScale;

			behavComp.RegenRate = regenRate;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}
	}
}
