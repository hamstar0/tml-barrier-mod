using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;


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
			this.ApplyRegen( myent );
		}

		public override void UpdateClient( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.UpdateLocal( myent );
			this.ApplyRegen( myent );
		}

		public override void UpdateServer( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			this.ApplyRegen( myent );
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
	}
}
