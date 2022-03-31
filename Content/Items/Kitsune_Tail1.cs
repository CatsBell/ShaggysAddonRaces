using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShaggyAddonRaces.Common.Races.Kitsune;
using MrPlagueRaces.Common.Races;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShaggyAddonRaces.Content.Items
{
	public class Kitsune_Tail1 : ModItem
	{

		private Mod ShaggyAddonRaces = ModLoader.GetMod("ShaggyAddonRaces");
		private Texture2D texture_Tail;

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
			item.rare = ItemRarityID.Pink;
			item.value = Item.sellPrice(copper: 1);

			texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail1");
			//item.position.X = 10;

			//Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(0, 0));

			//item.buffType = ModContent.BuffType<ImmuneToSun>();
			//item.buffTime = 28800;
		}

		//public override string Texture => "ShaggyAddonRaces/Content/Items/Kitsune_Tail1_Texture";

		

		public override void UpdateAccessory(Player player, bool hideVisual)
        {

			item.position.X = player.Center.X - 20 * player.direction;
			item.position.Y = player.Center.Y;
			Main.NewText("Doing stuff. " + item.position.ToString());
			//DrawData data = new DrawData(texture_Tail, new Vector2(player.Center.X, player.Center.Y), null, drawPlayer);
			//DrawData data = new DrawData(texture_Tail, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.skinColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);
			//Main.playerDrawData.Add(data);
		}
    }
}