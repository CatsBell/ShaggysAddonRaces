using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShaggyAddonRaces.Content.Buffs;

namespace ShaggyAddonRaces.Content.Items
{
	public class EclipsePot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eclipse potion");
			Tooltip.SetDefault("Negates debuffs from the sun.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 32;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.useAnimation = 15;
			item.useTime = 17;
			item.useTurn = true;
			item.UseSound = SoundID.Item3;
			item.maxStack = 30;
			item.consumable = true;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 2);
			item.buffType = ModContent.BuffType<ImmuneToSun>();
			item.buffTime = 28800;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater, 1);
			recipe.AddIngredient(ItemID.Moonglow, 1);
			recipe.AddIngredient(ItemID.Lens, 2);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe.AddIngredient(ItemID.BottledWater, 1);
			recipe.AddIngredient(ItemID.Moonglow, 1);
			recipe.AddIngredient(ItemID.Lens, 2);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}