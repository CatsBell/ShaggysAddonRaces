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

		private int tailCount = 1;
		private int oldTailCount = 1;
		private Mod ShaggyAddonRaces = ModLoader.GetMod("ShaggyAddonRaces");
		private MrPlagueRaces.MrPlagueRacesPlayer modPlayer = null;
		private Player globalPlayer = null;
		private static Texture2D texture_Color;
		private static Texture2D texture_Tail;

		public Item[] tails = new Item[9];

		private const bool DEBUG = false;

		// Debugging log function.
		private void Log(object message)
		{
			if (DEBUG)
			{
				#pragma warning disable CS0162 // Unreachable code detected
				ShaggyAddonRaces.Logger.Debug(message);
				#pragma warning restore CS0162 // Unreachable code detected
			}
		}

		private void Init(Player player)
		{
			// Redundant initialize every frame.
			if (modPlayer == null)
			{
				ShaggyAddonRaces.Logger.Info("Modplayer Null, defining...");
				globalPlayer = player;
				modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
				Item familiarshirt = new Item();
				Item familiarpants = new Item();
				familiarshirt.SetDefaults(ItemID.FamiliarShirt);
				familiarpants.SetDefaults(ItemID.FamiliarPants);
			}

			if (texture_Color == null || texture_Tail == null)
			{
				texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
				texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");
			}
		}

		// Proper initialization function that defines everything. In theory.
		public override void Initialize(Player player) {
			ShaggyAddonRaces.Logger.Info("Initializing...");
			globalPlayer = player;
			ShaggyAddonRaces.Logger.Info("Defining modplayer...");
			modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			Item familiarshirt = new Item();
			Item familiarpants = new Item();
			familiarshirt.SetDefaults(ItemID.FamiliarShirt);
			familiarpants.SetDefaults(ItemID.FamiliarPants);
			ShaggyAddonRaces.Logger.Info("Defining tails...");
			texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
			texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");

			tails.SetValue(new Kitsune_Tail1(), 0);
			Log(tails[0]);
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
					// Update the tail sprites!
					oldTailCount = tailCount;
					texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");
					texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
				}
			}
		}

		public override void ModifyDrawInfo(Player player, Mod mod, ref PlayerDrawInfo drawInfo)
		{
			if (player == null)
			{
				player = globalPlayer;
				ShaggyAddonRaces.Logger.Warn("Player undefined, assigned globalPlayer value.");
			}
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

		public void UpdateTailTexture()
		{
			texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
			texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");
		}
		
		public override void ModifyDrawLayers(Player player, List<PlayerLayer> layers)
		{
			try
			{
				if (player == null)
				{
					player = globalPlayer;
					ShaggyAddonRaces.Logger.Warn("Player undefined, assigned globalPlayer value.");
				}
				var mP = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
				if (layers == null)
				{
					ShaggyAddonRaces.Logger.Warn("LAYERS IS NULL");
				}
				if (texture_Color == null || texture_Tail == null)
				{
					ShaggyAddonRaces.Logger.Info("Tails are null... Acquiring.");
					texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
					texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");
				}
				if (texture_Color.IsDisposed || texture_Tail.IsDisposed)
				{
					// catch to make absolutely sure stuff isn't broken
					ShaggyAddonRaces.Logger.Info("Tails are disposed... re-acquiring.");
					texture_Tail = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount);
					texture_Color = ShaggyAddonRaces.GetTexture("Content/RaceTextures/Kitsune/Tail/Kitsune_Tail" + tailCount + "_Color");
				}
				if (modPlayer != null && player != null && layers != null)
				{
					int tailLayer = layers.IndexOf(PlayerLayer.Legs) - 1;
					layers.Insert(tailLayer, KitsuneTail);
					//layers.Insert(tailLayer + 1, KitsuneTail_Color);
					base.ModifyDrawLayers(player, layers);
					bool hideChestplate = mP.hideChestplate;
					bool hideLeggings = mP.hideLeggings;
					mP.updatePlayerSprites("ShaggyAddonRaces/Content/RaceTextures/", "ShaggyAddonRaces/Content/RaceTextures/Kitsune/", hideChestplate, hideLeggings, 4, 0, "Kitsune", false, false, false);
				}
			} catch (System.Exception e) {
				// Literally just pipe all errors to the log file rather than crash immediately and hope for the best.
				ShaggyAddonRaces.Logger.Error("Error in ModifyDraw Layers! Now printing error to log...");
				ShaggyAddonRaces.Logger.Error(e);
			}
		}

		//Original tail code provided by Kazun (thanks!). Refactored by AxeBane.
		public static readonly PlayerLayer KitsuneTail = new PlayerLayer("Kitsune", "KitsuneTail", PlayerLayer.Hair, delegate (PlayerDrawInfo drawInfo)
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

			try
			{
				DrawData data = new DrawData(texture_Tail, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.skinColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);
				Main.playerDrawData.Add(data);
			}
			catch (System.Exception)
			{
				// Fail silently.
			}
		});

		public static readonly PlayerLayer KitsuneTail_Color = new PlayerLayer("Kitsune", "KitsuneTail_Color", PlayerLayer.Hair, delegate (PlayerDrawInfo drawInfo)
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


			try
			{
				DrawData data = new DrawData(texture_Color, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.hairColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);
				Main.playerDrawData.Add(data);
			}
			catch
			(System.Exception) {
				// Fail silently.
			}
		});
	}
}