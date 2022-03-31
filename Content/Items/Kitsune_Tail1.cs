using Terraria;
using Terraria.ID;
using CustomSlot;
using Terraria.ModLoader;
using ShaggyAddonRaces.Content.Buffs;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace ShaggyAddonRaces.Content.Items
{
	[AutoloadEquip(EquipType.Wings)]
	public class Kitsune_Tail1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kitsune Tail (1)");
			Tooltip.SetDefault("You shouldn't be able to read this.");
		}

		public override void SetDefaults()
		{
			item.width = 56;
			item.height = 64;
			item.maxStack = 1;
			item.consumable = false;
			item.accessory = true;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 1);
			
			//Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(0, 0));
			
			//item.buffType = ModContent.BuffType<ImmuneToSun>();
			//item.buffTime = 28800;
		}
	}
}