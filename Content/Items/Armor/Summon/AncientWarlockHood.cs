using Consolaria.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Consolaria.Content.Items.Armor.Summon {
    [AutoloadEquip(EquipType.Head)]
    public class AncientWarlockHood : ModItem {
        public override void SetStaticDefaults () {
            DisplayName.SetDefault("Ancient Warlock Hood");
            Tooltip.SetDefault("Increases your max number of minions by 1" + "\n20% increased minion damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId [Type] = 1;
        }

        public override void SetDefaults () {
            int width = 30; int height = 26;
            Item.Size = new Vector2(width, height);

            Item.value = Item.sellPrice(gold: 6, silver: 40);
            Item.rare = ItemRarityID.Lime;

            Item.defense = 6;
        }

        public override void UpdateEquip (Player player) {
            player.maxMinions += 1;
            player.GetDamage(DamageClass.Summon) += 0.2f;
        }

        public override bool IsArmorSet (Item head, Item body, Item legs)
            => (body.type == ModContent.ItemType<WarlockRobe>() || body.type == ModContent.ItemType<AncientWarlockRobe>())
            && (legs.type == ModContent.ItemType<WarlockLeggings>() || legs.type == ModContent.ItemType<AncientWarlockLeggings>());

        public override void ArmorSetShadows (Player player)
            => player.armorEffectDrawShadow = true;

        public override void UpdateArmorSet (Player player) {
            player.setBonus = "Enemies killed by minions heal the player";
            player.GetModPlayer<WarlockPlayer>().necroHealing = true;
        }

        public override void AddRecipes () {
            CreateRecipe()
                .AddIngredient(ItemID.AncientHallowedHood)
                .AddRecipeGroup(RecipeGroups.Titanium, 10)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient<SoulofBlight>(10)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}