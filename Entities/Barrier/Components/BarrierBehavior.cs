using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Newtonsoft.Json;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class BarrierBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : BarrierBehaviorEntityComponent {
			public float Hp;
			public float Radius;
			public float RadiusRegenRate;
			public int Defense;
			public float ShrinkResist;
			public int RegenRegenDurationHighest;


			public BarrierBehaviorEntityComponentFactory( float hp, float radius, float regenRate, int defense, float shrinkResist, int regenRegenDurationHighest ) {
				this.Hp = hp;
				this.Radius = radius;
				this.RadiusRegenRate = regenRate;
				this.Defense = defense;
				this.ShrinkResist = shrinkResist;
				this.RegenRegenDurationHighest = regenRegenDurationHighest;
			}

			protected override void InitializeComponent( T data ) {
				data.Hp = this.Hp;
				data.MaxHp = this.Hp;
				data.Radius = this.Radius;
				data.MaxRadius = this.Radius;
				data.RegenRate = this.RadiusRegenRate;
				data.Defense = this.Defense;
				data.ShrinkResistScale = this.ShrinkResist;
				data.RegenRegenDurationHighest = this.RegenRegenDurationHighest;
			}
		}



		////////////////

		public static BarrierBehaviorEntityComponent CreateBarrierEntityComponent( float hp, float radius, float regenRate, int defense, float shrinkResist, int regenRegenDurationHighest ) {
			var factory = new BarrierBehaviorEntityComponentFactory<BarrierBehaviorEntityComponent>( hp, radius, regenRate, defense, shrinkResist, regenRegenDurationHighest );
			return factory.Create();
		}



		////////////////

		[JsonIgnore]
		[PacketProtocolIgnore]
		private int RegenRegen = 0;

		public float Hp;
		public float MaxHp;
		public float Radius;
		public float MaxRadius;
		public float RegenRate;
		public int Defense;
		public float ShrinkResistScale;
		public int RegenRegenDurationHighest;



		////////////////

		protected BarrierBehaviorEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override void UpdateSingle( CustomEntity ent ) {
			this.UpdateLocal( ent );
			this.UpdateAny( ent );
		}

		public override void UpdateClient( CustomEntity ent ) {
			this.UpdateLocal( ent );
			this.UpdateAny( ent );
		}

		public override void UpdateServer( CustomEntity ent ) {
			this.UpdateAny( ent );
		}


		////////////////

		private void UpdateLocal( CustomEntity ent ) {
			/*var plr = Main.LocalPlayer;
			float dist = Vector2.Distance( plr.Center, myent.Core.Center );

			if( dist < myent.Core.width ) {
				var myplayer = plr.GetModPlayer<BarriersPlayer>();

				myplayer.NoBuilding = true;
			}*/
		}

		private void UpdateAny( CustomEntity ent ) {
			this.ApplyRegen( ent );

			var center = ent.Core.Center;

			ent.Core.Width = ent.Core.Height = (int)Math.Max( this.Radius * 2, 1 );
			ent.Core.Center = center;

			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				DebugHelpers.Print( "Barrier " + ent.Core.WhoAmI + "'s Behavior",
					"hp:" + this.Hp.ToString( "N0" ) + "/" + this.MaxHp.ToString( "N0" )
					+ ", radius:" + this.Radius.ToString( "N0" ) + "/" + this.MaxRadius.ToString("N0")
					+ ", defense:" + this.Defense
					+ ", Regen:" + this.RegenRate.ToString( "N0" ),
					20 );
			}
		}
	}
}
