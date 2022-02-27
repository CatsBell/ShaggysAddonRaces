using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MrPlagueRaces.Common.Races;
using ShaggyAddonRaces.Content.Buffs;
using ShaggyAddonRaces.Content.Items;

namespace ShaggyAddonRaces.Common.Races.Baphoma
{
	//This is probably the first time I'm putting a comment down. Later down the road when I finish this race it's gonna be like... put in a history monument.
	public class Baphoma : Race 
	{
		public override string RaceDisplayName => "Baphoma";
		public override bool UsesCustomHurtSound => true;
		public override bool UsesCustomDeathSound => false;
		public override bool HasFemaleHurtSound => false;

		public bool SpawnWoF = false;

		public override string RaceEnvironmentIcon => "MrPlagueRaces/Common/UI/RaceDisplay/Environment/Environment_BloodMoon";
		public override string RaceEnvironmentOverlay1Icon => "MrPlagueRaces/Common/UI/RaceDisplay/BlankDisplay";

		public override string RaceSelectIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/BaphomaSelect";
		public override string RaceDisplayMaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/BaphomaDisplayMale";
		public override string RaceDisplayFemaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/BaphomaDisplayFemale";
		public override string RaceLore1 => "Baphoma are demon, goat-like creatures.";
		public override string RaceLore2 => "Some assume that they wandered from the Underworld, but no proof is solidof that.";
		public override string RaceAbilityName => "Ram";

		public override string RaceAbilityDescription => "You are able to ram by double tapping the left or right key.";

		public override bool DarkenEnvironment => false;

		public override string RaceDefenseDisplayText => "[c/FF4F64:-2]";
		public override string RaceLavaResistanceDisplayText => "[c/34EB93:+5 sec]";
		public override string RaceMeleeDamageDisplayText => "[c/FF4F64:-20%]";
		public override string RaceRangedDamageDisplayText => "[c/FF4F64:-10%]";
		public override string RaceMagicDamageDisplayText => "[c/34EB93:+25%]";
		public override string RaceMovementSpeedDisplayText => "[c/34EB93:+10%]";
		public override string RaceAggroDisplayText => "[c/34EB93:+30%]";
		public override string RaceJumpSpeedDisplayText => "[c/34EB93:+10%]";

		public override string RaceGoodBiomesDisplayText => "Underworld";
		public override string RaceBadBiomesDisplayText => "Hallow";

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
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            if (modPlayer.RaceStats)
			{

				player.statDefense -= 2;
                player.lavaMax += 300;
                player.meleeDamage -= 0.2f;
                player.rangedDamage -= 0.1f;
                player.magicDamage += 0.25f;
                player.jumpSpeedBoost += 0.1f;
                player.aggro += 30;
                player.moveSpeed += 0.1f;
                player.dash = 2;
                //Code by Kazune.
                if (Main.player[Main.myPlayer].ZoneHoly)
                {
                    bool hasUnholyCrossEquipped = false;
                    for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
                    {
                        if (player.armor[i].type == ModContent.ItemType<UnholyCrossNecklace>())
                        {
                            hasUnholyCrossEquipped = true;
                        }
                    }
                    if (!hasUnholyCrossEquipped)
                    {
                        player.AddBuff(ModContent.BuffType<BaphomaHell>(), 2);
                    }
                }
				//
            }
        }

		public override void PreUpdate(Player player, Mod mod)
		{
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			var _MrPlagueRaces = ModLoader.GetMod("MrPlagueRaces");
			var ShaggyAddonRaces = ModLoader.GetMod("ShaggyAddonRaces");
            if (player.HasBuff(_MrPlagueRaces.BuffType("DetectHurt")) && (player.statLife != player.statLifeMax2))
            {
                if (Main.rand.Next(3) == 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, ShaggyAddonRaces.GetSoundSlot(SoundType.Custom, "Sounds/Baphoma_Hurt"));
                }
                else if (Main.rand.Next(3) == 2)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, ShaggyAddonRaces.GetSoundSlot(SoundType.Custom, "Sounds/Baphoma_Hurt2"));
                }
                else
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, ShaggyAddonRaces.GetSoundSlot(SoundType.Custom, "Sounds/Baphoma_Hurt3"));
                }
            }
			return;
		}

		public override void ModifyDrawInfo(Player player, Mod mod, ref PlayerDrawInfo drawInfo)
		{
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			Item familiarshirt = new Item();
			familiarshirt.SetDefaults(ItemID.FamiliarShirt);
			Item familiarpants = new Item();
			familiarpants.SetDefaults(ItemID.FamiliarPants);
			if (modPlayer.resetDefaultColors && Main.gameMenu)
			{
				modPlayer.resetDefaultColors = false;
				player.hairColor = new Color(209, 169, 90);
				player.skinColor = new Color(208, 199, 181);
				player.eyeColor = new Color(209, 21, 25);
				player.shirtColor = new Color(219, 70, 44);
				player.underShirtColor = new Color(170, 75, 191);
				player.pantsColor = new Color(108, 99, 110);
				player.skinVariant = 8;
				if (player.armor[1].type < ItemID.IronPickaxe && player.armor[2].type < ItemID.IronPickaxe)
				{
					player.armor[1] = familiarshirt;
					player.armor[2] = familiarpants;
				}
			}
		}

		public override void ModifyDrawLayers(Player player, List<PlayerLayer> layers)
		{
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();

			bool hideChestplate = modPlayer.hideChestplate;
			bool hideLeggings = modPlayer.hideLeggings;

			Main.playerTextures[0, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head");
			Main.playerTextures[0, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2");
			Main.playerTextures[0, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes");
			Main.playerTextures[0, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_1");
			}
			else
			{
				Main.playerTextures[0, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[0, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_1");
			}
			else
			{
				Main.playerTextures[0, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[0, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1");
			}
			else
			{
				Main.playerTextures[0, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[0, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand");
			Main.playerTextures[0, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[0, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1");
			}
			else
			{
				Main.playerTextures[0, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1_2");
			}
			else
			{
				Main.playerTextures[0, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[0, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1_2");
			}
			else
			{
				Main.playerTextures[0, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[1, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head");
			Main.playerTextures[1, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2");
			Main.playerTextures[1, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes");
			Main.playerTextures[1, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_2");
			}
			else
			{
				Main.playerTextures[1, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[1, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_2");
			}
			else
			{
				Main.playerTextures[1, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[1, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2");
			}
			else
			{
				Main.playerTextures[1, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[1, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand");
			Main.playerTextures[1, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[1, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2");
			}
			else
			{
				Main.playerTextures[1, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2_2");
			}
			else
			{
				Main.playerTextures[1, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[1, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2_2");
			}
			else
			{
				Main.playerTextures[1, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[2, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head");
			Main.playerTextures[2, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2");
			Main.playerTextures[2, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes");
			Main.playerTextures[2, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_3");
			}
			else
			{
				Main.playerTextures[2, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[2, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_3");
			}
			else
			{
				Main.playerTextures[2, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[2, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3");
			}
			else
			{
				Main.playerTextures[2, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[2, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand");
			Main.playerTextures[2, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[2, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3");
			}
			else
			{
				Main.playerTextures[2, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3_2");
			}
			else
			{
				Main.playerTextures[2, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[2, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3_2");
			}
			else
			{
				Main.playerTextures[2, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[3, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head");
			Main.playerTextures[3, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2");
			Main.playerTextures[3, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes");
			Main.playerTextures[3, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_4");
			}
			else
			{
				Main.playerTextures[3, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[3, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_4");
			}
			else
			{
				Main.playerTextures[3, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[3, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4");
			}
			else
			{
				Main.playerTextures[3, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[3, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand");
			Main.playerTextures[3, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[3, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4");
			}
			else
			{
				Main.playerTextures[3, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4_2");
			}
			else
			{
				Main.playerTextures[3, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[3, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4_2");
			}
			else
			{
				Main.playerTextures[3, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[8, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head");
			Main.playerTextures[8, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2");
			Main.playerTextures[8, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes");
			Main.playerTextures[8, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_9");
			}
			else
			{
				Main.playerTextures[8, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[8, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_9");
			}
			else
			{
				Main.playerTextures[8, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[8, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9");
			}
			else
			{
				Main.playerTextures[8, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[8, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand");
			Main.playerTextures[8, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[8, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9");
			}
			else
			{
				Main.playerTextures[8, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9_2");
			}
			else
			{
				Main.playerTextures[8, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[8, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9_2");
			}
			else
			{
				Main.playerTextures[8, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[4, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head_Female");
			Main.playerTextures[4, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2_Female");
			Main.playerTextures[4, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_Female");
			Main.playerTextures[4, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_5");
			}
			else
			{
				Main.playerTextures[4, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[4, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_5");
			}
			Main.playerTextures[4, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5");
			}
			else
			{
				Main.playerTextures[4, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[4, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand_Female");
			Main.playerTextures[4, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs_Female");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[4, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5");
			}
			else
			{
				Main.playerTextures[4, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5_2");
			}
			else
			{
				Main.playerTextures[4, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[4, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5_2");
			}
			else
			{
				Main.playerTextures[4, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[5, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head_Female");
			Main.playerTextures[5, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2_Female");
			Main.playerTextures[5, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_Female");
			Main.playerTextures[5, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_6");
			}
			else
			{
				Main.playerTextures[5, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[5, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_6");
			}
			Main.playerTextures[5, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6");
			}
			else
			{
				Main.playerTextures[5, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[5, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand_Female");
			Main.playerTextures[5, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs_Female");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[5, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6");
			}
			else
			{
				Main.playerTextures[5, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6_2");
			}
			else
			{
				Main.playerTextures[5, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[5, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6_2");
			}
			else
			{
				Main.playerTextures[5, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[6, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head_Female");
			Main.playerTextures[6, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2_Female");
			Main.playerTextures[6, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_Female");
			Main.playerTextures[6, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_7");
			}
			else
			{
				Main.playerTextures[6, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[6, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_7");
			}
			Main.playerTextures[6, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7");
			}
			else
			{
				Main.playerTextures[6, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[6, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand_Female");
			Main.playerTextures[6, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs_Female");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[6, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7");
			}
			else
			{
				Main.playerTextures[6, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7_2");
			}
			else
			{
				Main.playerTextures[6, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[6, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7_2");
			}
			else
			{
				Main.playerTextures[6, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[7, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head_Female");
			Main.playerTextures[7, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2_Female");
			Main.playerTextures[7, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_Female");
			Main.playerTextures[7, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_8");
			}
			else
			{
				Main.playerTextures[7, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[7, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_8");
			}
			Main.playerTextures[7, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8");
			}
			else
			{
				Main.playerTextures[7, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[7, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand_Female");
			Main.playerTextures[7, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs_Female");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[7, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8");
			}
			else
			{
				Main.playerTextures[7, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8_2");
			}
			else
			{
				Main.playerTextures[7, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[7, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8_2");
			}
			else
			{
				Main.playerTextures[7, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[9, 0] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Head_Female");
			Main.playerTextures[9, 1] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_2_Female");
			Main.playerTextures[9, 2] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Eyes_Female");
			Main.playerTextures[9, 3] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Torso_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_10");
			}
			else
			{
				Main.playerTextures[9, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[9, 5] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hands_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_10");
			}
			Main.playerTextures[9, 7] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Arm_Female");
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10");
			}
			else
			{
				Main.playerTextures[9, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			Main.playerTextures[9, 9] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Hand_Female");
			Main.playerTextures[9, 10] = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Legs_Female");
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[9, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10");
			}
			else
			{
				Main.playerTextures[9, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10_2");
			}
			else
			{
				Main.playerTextures[9, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[9, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10_2");
			}
			else
			{
				Main.playerTextures[9, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			for (int i = 0; i < 133; i++)
			{
				Main.playerHairTexture[i] = ModContent.GetTexture($"ShaggyAddonRaces/Content/RaceTextures/Baphoma/Hair/Examplian_Hair_{i + 1}");
				Main.playerHairAltTexture[i] = ModContent.GetTexture($"ShaggyAddonRaces/Content/RaceTextures/Baphoma/Hair/Examplian_HairAlt_{i + 1}");
			}
			Main.ghostTexture = ModContent.GetTexture("ShaggyAddonRaces/Content/RaceTextures/Baphoma/Baphoma_Ghost");
		}
	}
}