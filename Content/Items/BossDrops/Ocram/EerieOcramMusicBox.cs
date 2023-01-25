using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace Consolaria.Content.Items.BossDrops.Ocram {
	public class EerieOcramMusicBox : ModItem {
		public override void SetStaticDefaults () {
			DisplayName.SetDefault("Music Box (Eerie Ocram)");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId [Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/EerieOcram"), ModContent.ItemType<EerieOcramMusicBox>(), ModContent.TileType<Tiles.EerieOcramMusicBox>());
		}

		public override void SetDefaults () {
			int width = 24; int height = width;
			Item.Size = new Vector2(width, height);

			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;

			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(gold: 1);
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.EerieOcramMusicBox>());
		}
	}
}