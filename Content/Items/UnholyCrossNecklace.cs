using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShaggyAddonRaces.Content.Items
{
	[AutoloadEquip(EquipType.Neck)]
	public class UnholyCrossNecklace : ModItem
	{
		public string RaceDisplayName;
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Unholy Cross Necklace");
			if (RaceDisplayName == "Baphoma")
            {
				Tooltip.SetDefault("Takes half your invincibility frames for extra regen in your world evil and in the Underworld." + "\nWill protect you from the Hallow.");
			}
			else
            {
				Tooltip.SetDefault("Takes half your invincibility frames for extra regen in your world evil and in the Underworld.");
			}
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 32;
			item.accessory = true;
			item.rare = ItemRarityID.LightRed;
			item.value = Item.sellPrice(gold: 2);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.immuneTime -= (int)(player.immuneTime * 0.5f);
			if ((Main.player[Main.myPlayer].ZoneCrimson) || (Main.player[Main.myPlayer].ZoneCorrupt))
            {
                player.lifeRegen += (player.statLifeMax2 / 200);
                player.manaRegenBonus += (player.statLifeMax2 / 200);
            }
			if (Main.player[Main.myPlayer].ZoneUnderworldHeight)
			{
				player.lifeRegen += (player.statLifeMax2 / 50);
				player.manaRegenBonus += (player.statLifeMax2 / 50);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrossNecklace, 1);
            recipe.AddIngredient(ItemID.DarkShard, 3);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
