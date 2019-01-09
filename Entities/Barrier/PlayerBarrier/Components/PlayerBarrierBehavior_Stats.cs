using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;


namespace Barriers.Entities.Barrier.PlayerBarrier.Components {
	public partial class PlayerBarrierBehaviorEntityComponent : CustomEntityComponent {
		public bool SetBarrierPower( PlayerBarrierEntity myent, int power, bool skipSync = false ) {
			int pwrChange = power - this.Power;
			
			var statsComp = myent.GetComponentByType<BarrierStatsEntityComponent>();
			
			this.Power = power;
			bool isHpChanged = this.SetBarrierHpScale( myent, this.HpScale, true );
			bool isRadiusChanged = this.SetBarrierRadiusScale( myent, this.RadiusScale, true );
			bool isDefenseChanged = this.SetBarrierDefenseScale( myent, this.DefenseScale, true );
			bool isShrinkResistChanged = this.SetBarrierShrinkResistScale( myent, statsComp.ShrinkResistScale, true );
			bool isRegenChanged = this.SetBarrierRegenScale( myent, this.RegenScale, true );
			
			if( BarriersMod.Instance.Config.DebugModeStatsInfo ) {
				string pow = "pow:" + this.Power + ( pwrChange!=0 ? "*" : "" );
				string hp = "hp%:" + this.HpScale + ( isHpChanged ? "*" : "" );
				string rad = "rad%:" + this.RadiusScale + ( isRadiusChanged ? "*" : "" );
				string def = "def%:" + this.DefenseScale + ( isDefenseChanged ? "*" : "" );
				string res = "resist%:" + statsComp.ShrinkResistScale + ( isShrinkResistChanged ? "*" : "" );
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
			var statsComp = myent.GetComponentByType<BarrierStatsEntityComponent>();
			
			float maxHp = PlayerBarrierEntity.ComputeBarrierMaxHp( this.Power, hpScale );
			float hp = statsComp.Hp > maxHp ? maxHp : statsComp.Hp;
			bool isChanged = this.HpScale != hpScale
				|| statsComp.MaxHp != maxHp
				|| statsComp.Hp != hp;
			
			this.HpScale = hpScale;
			statsComp.MaxHp = maxHp;
			statsComp.Hp = hp;
			
			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}
			
			return isChanged;
		}

		internal bool SetBarrierRadiusScale( PlayerBarrierEntity myent, float radiusScale, bool skipSync = false ) {
			var statsComp = myent.GetComponentByType<BarrierStatsEntityComponent>();

			float maxRadius = PlayerBarrierEntity.ComputeBarrierMaxRadius( this.Power, radiusScale );
			float radius = statsComp.Radius > maxRadius ? maxRadius : statsComp.Radius;
			bool isChanged = this.RadiusScale != radiusScale
				|| statsComp.MaxRadius != maxRadius
				|| statsComp.Radius != radius;

			this.RadiusScale = radiusScale;
			statsComp.MaxRadius = maxRadius;
			statsComp.Radius = radius;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool SetBarrierDefenseScale( PlayerBarrierEntity myent, float defenseScale, bool skipSync = false ) {
			var statsComp = myent.GetComponentByType<BarrierStatsEntityComponent>();

			int defense = PlayerBarrierEntity.ComputeBarrierDefense( this.Power, defenseScale );
			bool isChanged = this.DefenseScale != defenseScale
				|| statsComp.Defense != defense;

			this.DefenseScale = defenseScale;
			statsComp.Defense = defense;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool SetBarrierShrinkResistScale( PlayerBarrierEntity myent, float resistScale, bool skipSync = false ) {
			var statsComp = myent.GetComponentByType<BarrierStatsEntityComponent>();

			float resistScaleNew = PlayerBarrierEntity.ComputeBarrierShrinkResist( this.Power, resistScale );
			bool isChanged = statsComp.ShrinkResistScale != resistScaleNew;
			
			statsComp.ShrinkResistScale = resistScaleNew;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}

		internal bool SetBarrierRegenScale( PlayerBarrierEntity myent, float regenScale, bool skipSync = false ) {
			var statsComp = myent.GetComponentByType<BarrierStatsEntityComponent>();

			float regenRate = PlayerBarrierEntity.ComputeBarrierRegen( this.Power, regenScale );
			bool isChanged = this.RegenScale != regenScale
				|| statsComp.RegenRate != regenRate;

			this.RegenScale = regenScale;

			statsComp.RegenRate = regenRate;

			if( isChanged ) {
				if( !skipSync && Main.netMode == 1 ) {
					myent.SyncToAll();
				}
			}

			return isChanged;
		}
	}
}
