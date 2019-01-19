using HamstarHelpers.Components.CustomEntity;
using HamstarHelpers.Components.CustomEntity.Components;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Barriers.Entities.Barrier.Components {
	public class BarrierDrawOnMapEntityComponent : DrawsOnMapEntityComponent {
		private BarrierDrawOnMapEntityComponent() : base( "Barriers", "Entities/Barrier/Barrier128", 1, 0.5f, true ) { }
		public BarrierDrawOnMapEntityComponent( object _=null ) : this() { }


		////////////////

		public override Color GetColor( CustomEntity ent ) {
			var myent = (BarrierEntity)ent;
			return myent.GetBarrierColor();
		}


		////////////////

		private void UpdateScale( CustomEntity ent ) {
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();
			float radius = statsComp.Radius;

			this.Scale = radius / (128f * 8f);
		}

		public override bool PreDrawFullscreenMap( SpriteBatch sb, CustomEntity ent ) {
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();
			this.UpdateScale( ent );
			return statsComp.Hp > 0 && statsComp.Radius > 0;
		}

		public override bool PreDrawMiniMap( SpriteBatch sb, CustomEntity ent ) {
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();
			this.UpdateScale( ent );
			return statsComp.Hp > 0 && statsComp.Radius > 0;
		}

		public override bool PreDrawOverlayMap( SpriteBatch sb, CustomEntity ent ) {
			var statsComp = ent.GetComponentByType<BarrierStatsEntityComponent>();
			this.UpdateScale( ent );
			return statsComp.Hp > 0 && statsComp.Radius > 0;
		}
	}
}
