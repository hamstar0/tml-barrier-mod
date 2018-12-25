﻿using Barriers.Entities.Barrier.Components;
using Barriers.Entities.Barrier.PlayerBarrier.Components;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
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

			////

			public override float Hp => PlayerBarrierEntity.ComputeBarrierMaxHp( this.Power, this.HpScale );
			public override float Radius => PlayerBarrierEntity.ComputeBarrierMaxRadius( this.Power, this.RadiusScale );
			public override int Defense => PlayerBarrierEntity.ComputeBarrierDefense( this.Power, this.DefenseScale );
			public override float ShrinkResistScale => PlayerBarrierEntity.ComputeBarrierShrinkResist( this.Power, this.ShrinkResist );
			public override float RegenRate => PlayerBarrierEntity.ComputeBarrierRegen( this.Power, this.RegenScale );
			public override Vector2 Center { get; }

			////////////////

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
		}



		////////////////

		public static PlayerBarrierEntity CreatePlayerBarrierEntity( Player ownerPlr, int power, float hpScale, float radiusScale, float defenseScale, float shrinkResist, float regenScale, Vector2 center ) {
			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				LogHelpers.Log( "Creating new barrier at " + center );
			}

			var factory = new PlayerBarrierEntityFactory( ownerPlr, power, hpScale, radiusScale, defenseScale, shrinkResist, regenScale, center );
			PlayerBarrierEntity myent = factory.Create();

			return myent;
		}

		internal static PlayerBarrierEntity CreateDefaultPlayerBarrierEntity( Player ownerPlr ) {
			var mymod = BarriersMod.Instance;
			int defaultPow = mymod.Config.PlayerBarrierDefaultShieldPower;
			float defaultRegen = mymod.Config.BarrierRegenBaseAmount;

			return PlayerBarrierEntity.CreatePlayerBarrierEntity( ownerPlr, defaultPow, 1f, 1f, 0f, 0f, defaultRegen, Main.LocalPlayer.Center );
		}

		
		////////////////
		
		protected override IList<CustomEntityComponent> CreateComponents<T>( CustomEntityFactory<T> factory ) {
			var mymod = BarriersMod.Instance;
			var myfactory = factory as PlayerBarrierEntityFactory;
			IList<CustomEntityComponent> comps = base.CreateComponents<T>( factory );

			if( myfactory != null ) {
				comps.Insert( 0, PlayerBarrierBehaviorEntityComponent.CreateBarrierEntityComponent(
					myfactory.Power,
					myfactory.HpScale,
					myfactory.RadiusScale,
					myfactory.DefenseScale,
					myfactory.RegenScale
				) );
			} else {
				int defaultPow = mymod.Config.PlayerBarrierDefaultShieldPower;
				float defaultRegen = mymod.Config.BarrierRegenBaseAmount;

				comps.Insert( 0, PlayerBarrierBehaviorEntityComponent.CreateBarrierEntityComponent( defaultPow, 1f, 1f, 0f, defaultRegen ) );
			}

			comps.Add( BarrierHitRadiusProjectileEntityComponent.CreateBarrierHitRadiusProjectileEntityComponent( -1, 1 ) );
			comps.Add( BarrierHitRadiusNpcEntityComponent.CreateBarrierHitRadiusNpcEntityComponent( false ) );

			return comps;
		}
	}
}
