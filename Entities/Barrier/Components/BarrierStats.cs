using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Newtonsoft.Json;
using System;


namespace Barriers.Entities.Barrier.Components {
	public partial class BarrierStatsEntityComponent : CustomEntityComponent {
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

		protected BarrierStatsEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void OnInitialize() { }

		////

		protected override Type GetMyFactoryType() {
			return typeof( BarrierStatsEntityComponentFactory );
		}


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

			if( BarriersMod.Instance.Config.DebugModeStatsInfo ) {
				DebugHelpers.Print( "Barrier " + ent.Core.WhoAmI + "'s Behavior", this.GetStatsInfo(ent), 20 );
			}
		}


		////////////////

		public string GetStatsInfo( CustomEntity ent ) {
			return "Barrier " + ent.Core.WhoAmI + "'s stats:"
					+ "hp:" + this.Hp.ToString( "N0" ) + "/" + this.MaxHp.ToString( "N0" )
					+ ", radius:" + this.Radius.ToString( "N0" ) + "/" + this.MaxRadius.ToString( "N0" )
					+ ", defense:" + this.Defense
					+ ", Regen:" + this.RegenRate.ToString( "N0" );
		}
	}
}
