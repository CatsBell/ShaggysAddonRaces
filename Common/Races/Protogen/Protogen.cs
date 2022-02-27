using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MrPlagueRaces.Common.Races;
using ShaggyAddonRaces.Content.Buffs;

namespace ShaggyAddonRaces.Common.Races.Protogen
{
	public class Protogen : Race
	{
		public override string RaceDisplayName => "Protogen";
		public override bool UsesCustomHurtSound => true;
		public override bool UsesCustomDeathSound => false;
		public override bool HasFemaleHurtSound => false;

		public override string RaceEnvironmentIcon => "MrPlagueRaces/Common/UI/RaceDisplay/Environment/Environment_Sky";
		public override string RaceEnvironmentOverlay1Icon => "MrPlagueRaces/Common/UI/RaceDisplay/BlankDisplay";

		public override string RaceSelectIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/ProtogenSelect";
		public override string RaceDisplayMaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/ProtogenDisplayMale";
		public override string RaceDisplayFemaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/ProtogenDisplayFemale";
		public override string RaceLore1 => $"Protogen are cyborg" + "\n-like wolves, being made of gears and flesh.";
		public override string RaceLore2 => "They were sent to Terraria, fromspace, to study life.";
		public override string RaceAbilityName => "Mechanical Genius";
        public override string RaceAbilityDescription => "Can see wires natrually.";

        public override bool DarkenEnvironment => false;

        public override string RaceHealthDisplayText => "[c/34EB93:+20%]";
        public override string RaceRegenerationDisplayText => "";
        public override string RaceManaDisplayText => "[c/FF4F64:-20%]";
        public override string RaceManaRegenerationDisplayText => "";
        public override string RaceDefenseDisplayText => "[c/34EB93:+4]";
        public override string RaceDamageReductionDisplayText => "";
        public override string RaceThornsDisplayText => "";
        public override string RaceLavaResistanceDisplayText => "";
        public override string RaceMeleeDamageDisplayText => "";
        public override string RaceRangedDamageDisplayText => "[c/34EB93:+20%]";
        public override string RaceMagicDamageDisplayText => "[c/FF4F64:-20%]";
        public override string RaceSummonDamageDisplayText => "";
        public override string RaceMeleeSpeedDisplayText => "";
        public override string RaceArmorPenetrationDisplayText => "";
        public override string RaceBulletDamageDisplayText => "";
        public override string RaceRocketDamageDisplayText => "";
        public override string RaceManaCostDisplayText => "[c/FF4F64:+20%]";
        public override string RaceMinionKnockbackDisplayText => "";
        public override string RaceMinionsDisplayText => "";
        public override string RaceSentriesDisplayText => "";
        public override string RaceMeleeCritChanceDisplayText => "";
        public override string RaceRangedCritChanceDisplayText => "[c/34EB93:+10%]";
        public override string RaceMagicCritChanceDisplayText => "";
        public override string RaceMiningSpeedDisplayText => "";
        public override string RaceBuildingSpeedDisplayText => "";
        public override string RaceBuildingWallSpeedDisplayText => "";
        public override string RaceBuildingRangeDisplayText => "";
        public override string RaceArrowDamageDisplayText => "";
        public override string RaceMovementSpeedDisplayText => "";
        public override string RaceJumpSpeedDisplayText => "";
        public override string RaceFallDamageResistanceDisplayText => "";
        public override string RaceAllDamageDisplayText => "";
        public override string RaceFishingSkillDisplayText => "";
        public override string RaceAggroDisplayText => "";
        public override string RaceRunSpeedDisplayText => "";
        public override string RaceRunAccelerationDisplayText => "";

        public override string RaceGoodBiomesDisplayText => "Sky / Space";
		public override string RaceBadBiomesDisplayText => "None";

		public override bool PreHurt(Player player, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			return true;
		}

		public override void Load(Player player)
		{
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			if (modPlayer.RaceStats)
			{
				player.statLife += player.statLifeMax2 / 5;
			}
		}

		public override void ResetEffects(Player player)
		{
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            if (modPlayer.RaceStats)
            {
                player.rangedDamage += player.rangedDamage / 5;
                player.rangedCrit += player.rangedCrit / 10;
                player.statDefense += 4;
                player.statLifeMax2 += player.statLifeMax2 / 5;
                player.magicDamage -= player.magicDamage / 5;
                player.statManaMax2 -= player.statManaMax2 / 5;
                player.manaCost += player.manaCost / 5;
                player.InfoAccMechShowWires = true;
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
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, ShaggyAddonRaces.GetSoundSlot(SoundType.Custom, "Sounds/Protogen_Hurt"));
                }
                else if (Main.rand.Next(3) == 2)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, ShaggyAddonRaces.GetSoundSlot(SoundType.Custom, "Sounds/Protogen_Hurt2"));
                }
                else
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, ShaggyAddonRaces.GetSoundSlot(SoundType.Custom, "Sounds/Protogen_Hurt3"));
                }
            }
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
                player.hairColor = new Color(230, 207, 193);
                player.skinColor = new Color(230, 207, 193);
                player.eyeColor = new Color(11, 213, 201);
                player.skinVariant = 0;
			}
		}

        public override void ModifyDrawLayers(Player player, List<PlayerLayer> layers)
        {
            ProtoEar.visible = true;
            layers.Insert(layers.IndexOf(PlayerLayer.Hair) + 2, ProtoEar);
            base.ModifyDrawLayers(player, layers);

            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();

            bool hideChestplate = modPlayer.hideChestplate;
            bool hideLeggings = modPlayer.hideLeggings;

            modPlayer.updatePlayerSprites("MrPlagueRaces/Content/RaceTextures/", "ShaggyAddonRaces/Content/RaceTextures/Protogen/", hideChestplate, hideLeggings, 0, 0, "Protogen", false);

            for (int i = 0; i < 133; i++)
            {
                Main.playerHairTexture[i] = ModContent.GetTexture($"MrPlagueRaces/Content/RaceTextures/Human/Hair/Human_Hair_16");
                Main.playerHairAltTexture[i] = ModContent.GetTexture($"MrPlagueRaces/Content/RaceTextures/Human/Hair/Human_HairAlt_16");
            }
        }

        public static readonly PlayerLayer ProtoEar = new PlayerLayer("Protogen", "ProtoEar", PlayerLayer.Hair, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

			Texture2D texture = mod.GetTexture("Content/RaceTextures/Protogen/ProtoEar");

			int drawX = (int)(drawPlayer.position.X + 20);
			int drawY = (int)(drawPlayer.position.Y + 30 + drawPlayer.gfxOffY);

			if ((drawPlayer.headFrame.Y >= (7 * 56) && drawPlayer.headFrame.Y <= (9 * 56)) || (drawPlayer.headFrame.Y >= (14 * 56) && drawPlayer.headFrame.Y <= (16 * 56)))
			{
				drawY = (int)(drawPlayer.position.Y + 28 + drawPlayer.gfxOffY);
			}

			SpriteEffects flip = SpriteEffects.None;

			if (drawPlayer.direction == -1)
			{
				flip = SpriteEffects.FlipHorizontally;
				drawX = (int)(drawPlayer.position.X + 30);
			}

			DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(230, 207, 193)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

			Main.playerDrawData.Add(data);
		});
	}
}