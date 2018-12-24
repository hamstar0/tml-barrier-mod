using System.Collections.Generic;
using Barriers.Entities.Barrier.Components;
using HamstarHelpers.Items;
using Microsoft.Xna.Framework;
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
			this.Tooltip.SetDefault( "Projects a protective barrier."
				+ '\n' + "Select on hotbar to adjust barrier stats." );
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


		////////////////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			var mymod = (BarriersMod)this.mod;
			var barrer = mymod.BarrierManager.GetForPlayer( Main.LocalPlayer );
			var behavComp = barrer.GetComponentByType<BarrierBehaviorEntityComponent>();

			var hpStat = new TooltipLine( this.mod, "stat_hp", "  Hp: " + (int)behavComp.Hp + "/" + behavComp.MaxHp );
			var radStat = new TooltipLine( this.mod, "stat_rad", "  Radius: " + (int)behavComp.Radius + "/" + behavComp.MaxRadius );
			var defStat = new TooltipLine( this.mod, "stat_def", "  Defense: " + behavComp.Defense );
			var regStat = new TooltipLine( this.mod, "stat_reg", "  Regen.: " + (behavComp.RegenRate*60f).ToString("N2")+" per second" );
			var hardStat = new TooltipLine( this.mod, "stat_hard", "  Hardness: " + (behavComp.ShrinkResistScale*100f).ToString("N0")+"%" );

			hpStat.overrideColor = Color.Gray;
			radStat.overrideColor = Color.Gray;
			defStat.overrideColor = Color.Gray;
			regStat.overrideColor = Color.Gray;
			hardStat.overrideColor = Color.Gray;

			tooltips.Add( hpStat );
			tooltips.Add( radStat );
			tooltips.Add( defStat );
			tooltips.Add( regStat );
			tooltips.Add( hardStat );
		}


		////////////////

		public int GetPower() {
			var mymod = (BarriersMod)this.mod;
			return mymod.Config.PlayerBarrierDefaultShieldPower;    //TODO
		}
	}
}
