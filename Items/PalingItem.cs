using HamstarHelpers.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Barriers.Items {
	[AutoloadEquip( EquipType.Back )]
	public class PalingItem : ModItem {
		public bool IsUsingUI { get; private set; }
		public override bool CloneNewInstances => true;



		////////////////

		public PalingItem() {
			this.IsUsingUI = false;
		}


		////

		public override void SetStaticDefaults() {
			var mymod = (BarriersMod)this.mod;

			this.DisplayName.SetDefault( "Protective Paling" );
			this.Tooltip.SetDefault( "Projects a protective barrier." );
				//+ '\n' + "May be placed freely." );
		}


		public override void SetDefaults() {
			this.item.width = 32;
			this.item.height = 16;
			this.item.value = Item.buyPrice( 0, 15, 0, 0 );
			this.item.maxStack = 1;
			this.item.rare = 4;

			this.item.accessory = true;

			this.item.useStyle = 1;
			this.item.useTurn = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.autoReuse = true;

			//this.item.consumable = true;
			//this.item.createTile = this.mod.TileType<PalingTile>();

			//this.item.material = true;
		}


		public override void AddRecipes() {
			var helperMod = ModLoader.GetMod( "HamstarHelpers" );

			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( helperMod.ItemType<MagiTechScrapItem>(), 10 );
			recipe.AddIngredient( ItemID.CrystalShard, 10 );
			recipe.AddIngredient( ItemID.Cog, 50 );
			recipe.AddTile( TileID.SteampunkBoiler );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}
