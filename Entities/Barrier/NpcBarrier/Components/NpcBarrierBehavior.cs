using System;
using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Components.Network.Data;
using HamstarHelpers.Helpers.DebugHelpers;
using Newtonsoft.Json;
using Terraria;


namespace Barriers.Entities.Barrier.NpcBarrier.Components {
	public partial class NpcBarrierBehaviorEntityComponent : CustomEntityComponent {
		protected class NpcBarrierBehaviorEntityComponentFactory {
			public NPC Npc;
			public float Hp;
			public float Radius;
			public int Defense;
			public float ShrinkResistScale;
			public float RegenRate;
			
			public NpcBarrierBehaviorEntityComponentFactory( NPC npc, float hp, float radius, int defense, float shrinkResistScale, float regenRate ) {
				this.Npc = npc;
				this.Hp = hp;
				this.Radius = radius;
				this.Defense = defense;
				this.ShrinkResistScale = shrinkResistScale;
				this.RegenRate = regenRate;
			}
		}


		////////////////

		public static NpcBarrierBehaviorEntityComponent CreateNpcBarrierHitRadiusProjectileEntityComponent(
				NPC npc, float hp, float radius, int defense, float shrinkResistScale, float regenRate ) {
			var factory = new NpcBarrierBehaviorEntityComponentFactory( npc, hp, radius, defense, shrinkResistScale, regenRate );
			return NpcBarrierBehaviorEntityComponent.CreateDefault<NpcBarrierBehaviorEntityComponent>( factory );
		}



		////////////////

		[JsonIgnore]
		[PacketProtocolIgnore]
		public NPC Npc;

		public float Hp;
		public float Radius;
		public int Defense;
		public float ShrinkResistScale;
		public float RegenRate;
		


		////////////////

		protected NpcBarrierBehaviorEntityComponent( PacketProtocolDataConstructorLock ctorLock ) : base( ctorLock ) { }

		protected override void OnInitialize() { }

		////

		protected override Type GetMyFactoryType() {
			return typeof( NpcBarrierBehaviorEntityComponentFactory );
		}


		////////////////

		public override void UpdateSingle( CustomEntity ent ) {
			//this.UpdateLocal( ent );
			this.UpdateAny( ent );
		}

		public override void UpdateClient( CustomEntity ent ) {
			//this.UpdateLocal( ent );
			this.UpdateAny( ent );
		}

		public override void UpdateServer( CustomEntity ent ) {
			this.UpdateAny( ent );
		}


		////////////////

		/*private void UpdateLocal( CustomEntity ent ) {
			var plr = Main.LocalPlayer;
			float dist = Vector2.Distance( plr.Center, myent.Core.Center );

			if( dist < myent.Core.width ) {
				var myplayer = plr.GetModPlayer<BarriersPlayer>();

				myplayer.NoBuilding = true;
			}
		}*/
		
		private void UpdateAny( CustomEntity ent ) { }
	}
}
