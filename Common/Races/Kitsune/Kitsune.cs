using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MrPlagueRaces;
using MrPlagueRaces.Common.Races;
using ShaggyAddonRaces.Content.Buffs;
using ShaggyAddonRaces.Content.Items;

namespace ShaggyAddonRaces.Common.Races.Kitsune
{
	public class Kitsune : Race
	{
		public override string RaceDisplayName => "Kitsune";
		public override bool UsesCustomHurtSound => true;
		public override bool UsesCustomDeathSound => false;
		public override bool HasFemaleHurtSound => false;

		public override string RaceEnvironmentIcon => "MrPlagueRaces/Common/UI/RaceDisplay/Environment/Environment_Forest";
		public override string RaceEnvironmentOverlay1Icon => "MrPlagueRaces/Common/UI/RaceDisplay/BlankDisplay";

		public override string RaceSelectIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/KitsuneSelect";
		public override string RaceDisplayMaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/KitsuneDisplayMale";
		public override string RaceDisplayFemaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/KitsuneDisplayFemale";
		public override string RaceLore1 => "Kitsunes are capable of controlling magic, with a multitude of tails to show their strength.";
		public override string RaceLore2 => "Legends say that Kitsune are born as regular foxes, and fused with magic while young.";
		public override string RaceAbilityName => "Magic Tails";
		public override string RaceAbilityDescription => $"Everytime you defeat one of these bosses, you gain a tail, a small mana regen boost and more mana."
													   + $"\nEye of Cthulu, Skeletron, Queen Bee, Wall of Flesh, Any Mech Boss, Plantera, Lunatic Cultist and the Moon Lord.";
		public override bool DarkenEnvironment => false;

		public override string RaceGoodBiomesDisplayText => "Forest / Tundra";
		public override string RaceBadBiomesDisplayText => "None";
		public override string RaceHealthDisplayText => "[c/FF4F64:-25]";
		public override string RaceManaDisplayText => "[c/34EB93:+20%]";
		public override string RaceDefenseDisplayText => "[c/FF4F64:-2]";
		public override string RaceMeleeDamageDisplayText => "[c/FF4F64:-10%]";
		public override string RaceMagicDamageDisplayText => "[c/34EB93:+15%]";
		public override string RaceRunAccelerationDisplayText => "[c/34EB93:+20%]";
		public static bool RacialSTATSSAR;

		private static int tailCount = 1;
		private static int oldTailCount = 1;
		private Mod ShaggyAddonRaces = ModLoader.GetMod("ShaggyAddonRaces");
		private MrPlagueRaces.MrPlagueRacesPlayer modPlayer = null;
		private static Texture2D texture_Color;
		private static Texture2D texture_Tail;

		private void Init(Player player)
		{
			if (modPlayer == null)
			{
				modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>(); // grab the modPlayer object associated with it.
				texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");
				texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
				Item familiarshirt = new Item();
				Item familiarpants = new Item();
				familiarshirt.SetDefaults(ItemID.FamiliarShirt);
				familiarpants.SetDefaults(ItemID.FamiliarPants);
			}
		}

		public override bool PreHurt(Player player, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			return true;
		}
		public override void Load(Player player)
		{
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			if (modPlayer.RaceStats)
			{
				player.statLife += 0;
			}
		}
		public override void ResetEffects(Player player)
		{
			Init(player);
			if (modPlayer.RaceStats)
			{
				tailCount = 1;
				player.statLifeMax2 -= 25;
				player.statManaMax2 += player.statManaMax2 / 5;
				player.statDefense -= 2;
				player.manaRegen += 20 / 100;
				player.meleeDamage -= player.meleeDamage / 10;
				player.magicDamage += player.magicDamage / 15;
				player.runAcceleration += player.runAcceleration / 5;
				if (NPC.downedBoss1)
				{
					player.statManaMax2 += 20;
					player.manaRegenBonus += 5;
					tailCount++;
				}
				if (NPC.downedBoss3)
				{
					player.statManaMax2 += 10;
					player.manaRegenBonus += 3;
					tailCount++;
				}
				if (NPC.downedQueenBee)
				{
					player.statManaMax2 += 10;
					player.manaRegenBonus += 3;
					tailCount++;
				}
				if (Main.hardMode)
				{
					player.statManaMax2 += 10;
					player.manaRegenBonus += 3;
					tailCount++;
				}
				if (NPC.downedMechBossAny)
				{
					player.statManaMax2 += 10;
					player.manaRegenBonus += 3;
					tailCount++;
				}
				if (NPC.downedPlantBoss)
				{
					player.statManaMax2 += 10;
					player.manaRegenBonus += 2;
					tailCount++;
				}
				if (NPC.downedAncientCultist)
				{
					player.statManaMax2 += 10;
					player.manaRegenBonus += 2;
					tailCount++;
				}
				if (NPC.downedMoonlord)
				{
					player.statManaMax2 += 10;
					player.manaRegenBonus += 10;
					tailCount++;
				}
				if (tailCount != oldTailCount)
				{
					Main.NewText("Kitsune Tail Count: " + tailCount);
					oldTailCount = tailCount;
					texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");
					texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
				}
			}
		}

		public override void ModifyDrawInfo(Player player, Mod mod, ref PlayerDrawInfo drawInfo)
		{
			modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>(); // Have to fetch the modplayer again for character screen shenanigans
			if (modPlayer.resetDefaultColors && Main.gameMenu)
			{
				modPlayer.resetDefaultColors = false;
				player.hairColor = new Color(232, 101, 54);
				player.skinColor = new Color(255, 255, 255);
				player.eyeColor = new Color(50, 50, 50);
				player.skinVariant = 0;
			}
		}

		public override void ModifyDrawLayers(Player player, List<PlayerLayer> layers)
		{
			Init(player);
			if (modPlayer != null)
			{
				int tailLayer = layers.IndexOf(PlayerLayer.Legs) - 1;
				layers.Insert(tailLayer, KitsuneTail);

				layers.Insert(tailLayer + 1, KitsuneTail_Color);
				base.ModifyDrawLayers(player, layers);

				bool hideChestplate = modPlayer.hideChestplate;
				bool hideLeggings = modPlayer.hideLeggings;

				modPlayer.updatePlayerSprites("MrPlagueRaces/Content/RaceTextures/", "ShaggyAddonRaces/Content/RaceTextures/Kitsune/", hideChestplate, hideLeggings, 4, 0, "Kitsune", false, false, false);
			}
		}

		// Original tail code provided by Kazun (thanks!). Refactored by AxeBane to remove some jank and somehow introduce some more.

		public readonly PlayerLayer KitsuneTail = new PlayerLayer("Kitsune", "KitsuneTail", PlayerLayer.Hair, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			int drawX = (int)(drawPlayer.position.X - 28);
			int drawY = (int)(drawPlayer.position.Y + 4 + drawPlayer.gfxOffY);

			if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
			{
				drawY = (int)(drawPlayer.position.Y + 2 + drawPlayer.gfxOffY);
			}

			SpriteEffects flip = SpriteEffects.None;

			if (drawPlayer.direction == -1)
			{
				if (drawPlayer.gravDir == -1)
				{
					flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

					drawY = (int)(drawPlayer.position.Y + 30 + drawPlayer.gfxOffY);
					drawX = (int)(drawPlayer.position.X + 30 + drawPlayer.gfxOffY);

					if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
					{
						drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
					}
				}
				else
				{
					flip = SpriteEffects.FlipHorizontally;
					drawX = (int)(drawPlayer.position.X + 34);
				}
			}

			if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
			{
				flip = SpriteEffects.FlipVertically;

				drawY = (int)(drawPlayer.position.Y + 30 + drawPlayer.gfxOffY);

				if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
				{
					drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
				}
			}

			Main.playerDrawData.Add(new DrawData(texture_Tail, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.skinColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0));
		});

		public readonly PlayerLayer KitsuneTail_Color = new PlayerLayer("Kitsune", "KitsuneTail_Color", PlayerLayer.Hair, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;

			int drawX = (int)(drawPlayer.position.X - 28);
			int drawY = (int)(drawPlayer.position.Y + 4 + drawPlayer.gfxOffY);

			if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
			{
				drawY = (int)(drawPlayer.position.Y + 2 + drawPlayer.gfxOffY);
			}

			SpriteEffects flip = SpriteEffects.None;

			if (drawPlayer.direction == -1)
			{
				if (drawPlayer.gravDir == -1)
				{
					flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

					drawY = (int)(drawPlayer.position.Y + 30 + drawPlayer.gfxOffY);
					drawX = (int)(drawPlayer.position.X + 30 + drawPlayer.gfxOffY);

					if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
					{
						drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
					}
				}
				else
				{
					flip = SpriteEffects.FlipHorizontally;
					drawX = (int)(drawPlayer.position.X + 34);
				}
			}

			if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
			{
				flip = SpriteEffects.FlipVertically;

				drawY = (int)(drawPlayer.position.Y + 30 + drawPlayer.gfxOffY);

				if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
				{
					drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
				}
			}

			DrawData data = new DrawData(texture_Color, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.hairColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

			Main.playerDrawData.Add(data);
		});
	}
}