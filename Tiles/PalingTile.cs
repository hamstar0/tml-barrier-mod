using Barriers.Entities.Barrier;
using Barriers.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace Barriers.Tiles {
	class PalingTile : ModTile {
		public override void SetDefaults() {
			Main.tileFrameImportant[ this.Type ] = true;
			Main.tileNoAttach[ this.Type ] = true;
			Main.tileLavaDeath[ this.Type ] = false;
			TileObjectData.newTile.CopyFrom( TileObjectData.Style3x2 );
			TileObjectData.newTile.Origin = new Point16( 1, 1 );
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
			//TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook( BarrierEntity.CreateForTile, -1, 0, false );
			TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData( AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0 );
			TileObjectData.addTile( this.Type );

			ModTranslation name = this.CreateMapEntryName();
			name.SetDefault( "Protective Paling" );

			this.AddMapEntry( new Color( 200, 200, 200 ), name );
			this.dustType = 1;
			this.disableSmartCursor = true;
			this.adjTiles = new int[] { this.Type };

			this.animationFrameHeight = 36;
		}


		public override void AnimateTile( ref int frame, ref int frameCounter ) {
			if( ++Main.tileFrameCounter[ this.Type ] >= 2 ) {
				Main.tileFrameCounter[ this.Type ] = 0;

				if( ++Main.tileFrame[ this.Type ] >= 2 ) {
					Main.tileFrame[ this.Type ] = 0;
				}
			}
		}


		public override void NumDust( int i, int j, bool fail, ref int num ) {
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile( int i, int j, int frameX, int frameY ) {
			Item.NewItem( i * 16, j * 16, 32, 16, this.mod.ItemType<PalingItem>() );
		}
	}
}
