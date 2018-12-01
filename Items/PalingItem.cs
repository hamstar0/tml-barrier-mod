using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Barriers.Items {
	public class PalingItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Protective Paling" );
			this.Tooltip.SetDefault( "Projects a protective barrier." +
				'\n' + "May be placed freely." );
		}


		public override void SetDefaults() {
			this.item.width = 32;
			this.item.height = 16;
			this.item.value = Item.buyPrice(0, 15, 0, 0);
			this.item.rare = 4;
			this.item.maxStack = 1;
			this.item.accessory = true;
			this.item.createTile = this.mod.TileType<PalingTile>();
		}


		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe( mod );
			recipe.AddIngredient( ItemID.DirtBlock, 10 );
			recipe.AddTile( TileID.WorkBenches );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}
