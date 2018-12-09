using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class BarrierBehaviorEntityComponentFactory<T> : CustomEntityComponentFactory<T> where T : BarrierBehaviorEntityComponent {
			public float Hp;
			public float Radius;
			public float RadiusRegenRate;
			public int Defense;
			public float ShrinkResist;


			public BarrierBehaviorEntityComponentFactory( float hp, float radius, float regenRate, int defense, float shrinkResist ) {
				this.Hp = hp;
				this.Radius = radius;
				this.RadiusRegenRate = regenRate;
				this.Defense = defense;
				this.ShrinkResist = shrinkResist;
			}

			protected override void InitializeComponent( T data ) {
				data.Hp = this.Hp;
				data.MaxHp = this.Hp;
				data.Radius = this.Radius;
				data.MaxRadius = this.Radius;
				data.RegenRate = this.RadiusRegenRate;
				data.Defense = this.Defense;
				data.ShrinkResistScale = this.ShrinkResist;
			}
		}



		////////////////

		public static BarrierBehaviorEntityComponent CreateBarrierEntityComponent( float hp, float radius, float regenRate, int defense, float shrinkResist ) {
			var factory = new BarrierBehaviorEntityComponentFactory<BarrierBehaviorEntityComponent>( hp, radius, regenRate, defense, shrinkResist );
			return factory.Create();
		}



		////////////////

		public float Hp;
		public float MaxHp;
		public float Radius;
		public float MaxRadius;
		public float RegenRate;
		public int Defense;
		public float ShrinkResistScale;



		////////////////

		protected BarrierBehaviorEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }


		////////////////

		public override void UpdateSingle( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.UpdateLocal( myent );
			this.UpdateAny( myent );
		}

		public override void UpdateClient( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.UpdateLocal( myent );
			this.UpdateAny( myent );
		}

		public override void UpdateServer( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.UpdateAny( myent );
		}


		////////////////

		private void UpdateLocal( BarrierEntity myent ) {
			/*var plr = Main.LocalPlayer;
			float dist = Vector2.Distance( plr.Center, myent.Core.Center );

			if( dist < myent.Core.width ) {
				var myplayer = plr.GetModPlayer<BarriersPlayer>();

				myplayer.NoBuilding = true;
			}*/
		}

		private void UpdateAny( BarrierEntity myent ) {
			this.ApplyRegen( myent );

			var center = myent.Core.Center;

			myent.Core.Width = myent.Core.Height = (int)Math.Max( this.Radius * 2, 1 );
			myent.Core.Center = center;

			if( BarriersMod.Instance.Config.DebugModeInfo ) {
				DebugHelpers.Print( "Barrier " + myent.Core.WhoAmI + "'s Behavior",
					"hp:" + this.Hp.ToString( "N0" ) + "/" + this.MaxHp.ToString( "N0" )
					+ ", radius:" + this.Radius.ToString( "N0" ) + "/" + this.MaxRadius.ToString("N0")
					+ ", defense:" + this.Defense
					+ ", Regen:" + this.RegenRate.ToString( "N0" ),
					20 );
			}
		}
	}
}
