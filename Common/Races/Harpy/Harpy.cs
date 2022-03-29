using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MrPlagueRaces;
using MrPlagueRaces.Common.Races;

namespace ShaggyAddonRaces.Common.Races.Harpy
{
    public class Harpy : Race
    {
        public override string RaceDisplayName => "Harpy";
        public override string RaceSelectIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/HarpySelect";
        public override string RaceDisplayMaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/HarpyDisplayMale";
        public override string RaceDisplayFemaleIcon => "ShaggyAddonRaces/Common/UI/RaceDisplay/HarpyDisplayFemale";

        public override string RaceLore1 => "Some say the Harpy were made as an amalgamation of Humans and birds.";
        public override string RaceLore2 => "Cast away by the primitive Harpys for aiding in the war of Mushfolk, these Harpy search for a new way of life on the surface.";

        public override string RaceEnvironmentIcon => "MrPlagueRaces/Common/UI/RaceDisplay/Environment/Environment_Forest";
        public override string RaceEnvironmentOverlay1Icon => "MrPlagueRaces/Common/UI/RaceDisplay/BlankDisplay";

        public override string RaceAbilityName => "Natrual wings";

        public override string RaceAbilityDescription => $"You cannot fly, but can glide" +
                                                         $"\nGlide by holding the [c/34EB93:jump] button while falling.";
        public override string RaceAdditionalNotesDescription => $"[c/34EB93:2+ seconds] of flight time." +
                                                                 $"\nGains horizontal speed while gliding." +
                                                                 $"\nGains +8% critical strike chance when gliding.";
        public override bool UsesCustomHurtSound => false;
        public override bool UsesCustomDeathSound => false;
        public static bool Female;

        /*
        public static bool BarEmpty = false;
        public static bool BarIsFull = false;
        */

        public override string RaceHealthDisplayText => "[c/FF4F64:-10]";
        public override string RaceDamageReductionDisplayText => "[c/FF4F64:-10%]";

        public override string RaceGoodBiomesDisplayText => "Sky";
        public override string RaceBadBiomesDisplayText => "Cavern";

        public int HarpyWingAnim;
        public int HarpyWingFrame;

        /*
        public int HarpyFallDamagePrevention;
        public int HarpyFeatherBarTime = 0;
        public int HarpyFeatherBarDrain = 20;
        */
        
        public override void ResetEffects(Player player)
        {
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            if (modPlayer.RaceStats)
            {
                player.noFallDmg = true;
                player.statLifeMax2 -= 10;
                player.endurance -= 0.1f;
                player.wingTimeMax += 120;
                if (!player.Male)
                {
                    Female = true;
                }
            }
        }

        public override void ProcessTriggers(Player player, Mod mod)
        {
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            if (modPlayer.RaceStats)
            {
                if (MrPlagueRaces.MrPlagueRaces.RacialAbilityHotKey.Current)
                {
                    /*
                    if (Full.visible && BarIsFull)
                    {
                        Main.NewText("Test");
                        Projectile.NewProjectile(player.position.X, player.position.Y, 15, 0, mod.ProjectileType("Feather"), 15, 4, player.whoAmI, 0, 0);
                        BarIsFull = false;
                        HarpyFeatherBarTime = 0;
                    }
                    */
                }
            }
        }

        /*
        public override void Load(Player player)
        {
            FadeIn.visible = false;
            Clear.visible = false;
            Bar1.visible = false;
            Bar2.visible = false;
            Bar3.visible = false;
            Full.visible = false;
            FadeOut.visible = false;
        }
        */

        public override void PreUpdate(Player player, Mod mod)
        {
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            if (modPlayer.RaceStats)
            {
                if (player.wingsLogic == 0 && player.velocity.Y != 0)
                {
                    if (player.controlJump)
                    {
                        if (player.velocity.Y != 0)
                        {
                            if (!(player.velocity.Y < 4))
                            {
                                player.velocity.Y -= 7 / 2;
                            }
                            if (player.velocity.Y > 0)
                            {
                                player.maxRunSpeed += 1f;
                            }
                            player.magicCrit += 8;
                            player.meleeCrit += 8;
                            player.rangedCrit += 8;
                            player.thrownCrit += 8;
                        }
                    }
                }
            }
        }

        public override void ModifyDrawInfo(Player player, Mod mod, ref PlayerDrawInfo drawInfo)
        {
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();

            if (modPlayer.resetDefaultColors)
            {
                modPlayer.resetDefaultColors = false;
                player.hairColor = new Color(59, 140, 173);
                player.skinColor = new Color(255, 217, 196);
                player.eyeColor = new Color(220, 13, 77);
                player.skinVariant = 0;
            }

            if (modPlayer.RaceStats)
            {
                if (player.wingsLogic == 0 && !player.mount.Active)
                {
                    player.wings = 0;
                    if (player.controlJump)
                    {
                        if (player.velocity.Y > 0)
                        {
                            HarpyWingAnim += 1;
                            if (HarpyWingAnim >= 0 && HarpyWingAnim < 6)
                            {
                                if (!(player.itemAnimation > 0))
                                {
                                    player.bodyFrame.Y = player.bodyFrame.Height * 6;
                                }
                                HarpyWingFrame = 1;
                            }
                            if (HarpyWingAnim >= 6 && HarpyWingAnim < 12)
                            {
                                if (!(player.itemAnimation > 0))
                                {
                                    player.bodyFrame.Y = player.bodyFrame.Height * 0;
                                }
                                HarpyWingFrame = 2;
                            }
                            if (HarpyWingAnim >= 12 && HarpyWingAnim < 18)
                            {
                                if (!(player.itemAnimation > 0))
                                {
                                    player.bodyFrame.Y = player.bodyFrame.Height * 6;
                                }
                                HarpyWingFrame = 3;
                            }
                            if (HarpyWingAnim >= 18 && HarpyWingAnim < 24)
                            {
                                if (!(player.itemAnimation > 0))
                                {
                                    player.bodyFrame.Y = player.bodyFrame.Height * 5;
                                }
                                HarpyWingFrame = 4;
                                HarpyWingAnim = 0;
                                if (!player.dead)
                                {
                                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Wing_Flap"));
                                }
                            }
                        }
                        else
                        {
                            HarpyWingAnim = 0;
                            HarpyWingFrame = -1;
                        }
                    }
                    else
                    {
                        HarpyWingAnim = 0;
                        HarpyWingFrame = -1;
                    }
                }
                /*
                if (player.velocity.Y != 0 && !player.mount.Active && !player.controlHook)
                {
                    HarpyFeatherBarTime += 1;

                    if (HarpyFeatherBarTime >= 0 && HarpyFeatherBarTime < 9 && BarIsFull == false && BarEmpty == true)
                    {
                        Invis.visible = false;
                        FadeIn.visible = true;
                    }
                    else if (HarpyFeatherBarTime >= 10 && HarpyFeatherBarTime < 29 && BarIsFull == false)
                    {
                        FadeIn.visible = false;
                        Clear.visible = true;
                    }
                    else if (HarpyFeatherBarTime >= 30 && HarpyFeatherBarTime < 44 && BarIsFull == false)
                    {
                        Clear.visible = false;
                        Bar1.visible = true;
                    }
                    else if (HarpyFeatherBarTime >= 45 && HarpyFeatherBarTime < 59 && BarIsFull == false)
                    {
                        Bar1.visible = false;
                        Bar2.visible = true;
                    }
                    else if (HarpyFeatherBarTime >= 60 && HarpyFeatherBarTime < 74 && BarIsFull == false)
                    {
                        Bar2.visible = false;
                        Bar3.visible = true;
                    }
                    else if (HarpyFeatherBarTime >= 75 && HarpyFeatherBarTime < 89 && BarIsFull == false)
                    {
                        Bar3.visible = false;
                        Full.visible = true;
                        BarIsFull = true;
                    }
                    else if (HarpyFeatherBarTime > 90)
                    {
                        HarpyFeatherBarTime = 75;
                    }
                }
                else if (player.velocity.Y == 0)
                {
                    Invis.visible = false;
                    FadeIn.visible = false;
                    Clear.visible = false;
                }
                */
            }
        }

        public override void ModifyDrawLayers(Player player, List<PlayerLayer> layers)
        {
            layers.Insert(layers.IndexOf(PlayerLayer.Legs), Harpy_Skin_Legs);
            if (!Female)
            {
                layers.Insert(layers.IndexOf(PlayerLayer.Body), Harpy_ChestFeathers);
            }
            else if (Female)
            {
                layers.Insert(layers.IndexOf(PlayerLayer.Body), Harpy_ChestFeathers_Female);
            }
            layers.Insert(layers.IndexOf(PlayerLayer.Arms) + 1, Harpy_wings);
            layers.Insert(layers.IndexOf(PlayerLayer.Arms) + 1, Harpy_Skin_Arm);
            /*
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), FadeIn);
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), Clear);
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), Bar1);
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), Bar2);
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), Bar3);
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), Full);
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), FadeOut);
            layers.Insert(layers.IndexOf(PlayerLayer.FaceAcc), Invis);
            */

            base.ModifyDrawLayers(player, layers);

            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();

            bool hideChestplate = modPlayer.hideChestplate;
            bool hideLeggings = modPlayer.hideLeggings;

            modPlayer.updatePlayerSprites("MrPlagueRaces/Content/RaceTextures/", "ShaggyAddonRaces/Content/RaceTextures/Harpy/", hideChestplate, hideLeggings, 0, 0, "Harpy", false);

            for (int i = 0; i < 133; i++)
            {
                Main.playerHairTexture[0] = ModContent.GetTexture($"MrPlagueRaces/Content/RaceTextures/Human/Hair/Human_Hair_55");
                Main.playerHairAltTexture[0] = ModContent.GetTexture($"MrPlagueRaces/Content/RaceTextures/Human/Hair/Human_HairAlt_55");
                Main.playerHairTexture[i] = ModContent.GetTexture($"MrPlagueRaces/Content/RaceTextures/Human/Hair/Human_Hair_{i + 1}");
                Main.playerHairAltTexture[i] = ModContent.GetTexture($"MrPlagueRaces/Content/RaceTextures/Human/Hair/Human_HairAlt_{i + 1}");
            }
        }
        /*
        public static readonly PlayerLayer Invis = new PlayerLayer("Harpy", "Invis", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/Invis");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer FadeIn = new PlayerLayer("Harpy", "FadeIn", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/FadeIn");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Clear = new PlayerLayer("Harpy", "Clear", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/Clear");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Bar1 = new PlayerLayer("Harpy", "Bar1", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/Bar1");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Bar2 = new PlayerLayer("Harpy", "Bar2", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/Bar2");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Bar3 = new PlayerLayer("Harpy", "Bar3", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/Bar3");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Full = new PlayerLayer("Harpy", "Full", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/Full");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer FadeOut = new PlayerLayer("Harpy", "FadeOut", PlayerLayer.Face, delegate (PlayerDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Bar_Simp/FadeOut");

            int drawX = (int)(drawPlayer.position.X + 18);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 20 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 20);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), null, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, new Color(255, 255, 255)), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });
        */

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        public static readonly PlayerLayer Harpy_Skin_Arm = new PlayerLayer("Harpy", "Harpy_Skin_Arm", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
        {
            Harpy_Skin_Arm.visible = true;
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Harpy_Skin_Arm");

            int drawX = (int)(drawPlayer.position.X + 10);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 12 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 12);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            Rectangle sourceRectangle = drawPlayer.bodyFrame;

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), sourceRectangle, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.eyeColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Harpy_wings = new PlayerLayer("Harpy", "Harpy_wings", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            Harpy_wings.visible = true;
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Harpy_wings");

            int drawX = (int)(drawPlayer.position.X + 10);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 12 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 12);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            Rectangle sourceRectangle = drawPlayer.bodyFrame;

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), sourceRectangle, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.hairColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Harpy_Skin_Legs = new PlayerLayer("Harpy", "Harpy_Skin_Legs", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
        {
            Harpy_Skin_Legs.visible = true;
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Harpy_Skin_Legs");

            int drawX = (int)(drawPlayer.position.X + 10);
            int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

            if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
            {
                drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
            }

            //Initialize the sprite effect I'll be using to flip the sprite
            SpriteEffects flip = SpriteEffects.None;

            //if the player is facing left, flip horiz
            if (drawPlayer.direction == -1)
            {
                //if the player is upside down, flip vertically as well.
                if (drawPlayer.gravDir == -1)
                {
                    flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                    drawX = (int)(drawPlayer.position.X + 12 + drawPlayer.gfxOffY);
                }
                else
                {
                    flip = SpriteEffects.FlipHorizontally;
                    drawX = (int)(drawPlayer.position.X + 12);
                }
            }

            //If the player is upside down (e.g. gravity potion), flip vert
            if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
            {
                flip = SpriteEffects.FlipVertically;

                drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
            }

            Rectangle sourceRectangle = drawPlayer.legFrame;

            DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), sourceRectangle, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.eyeColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

            Main.playerDrawData.Add(data);
        });

        public static readonly PlayerLayer Harpy_ChestFeathers = new PlayerLayer("Harpy", "Harpy_ChestFeathers", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            if (!Female)
            {
                Harpy_ChestFeathers.visible = true;
                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

                Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Harpy_ChestFeathers");

                int drawX = (int)(drawPlayer.position.X + 10);
                int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

                if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
                {
                    drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
                }

                //Initialize the sprite effect I'll be using to flip the sprite
                SpriteEffects flip = SpriteEffects.None;

                //if the player is facing left, flip horiz
                if (drawPlayer.direction == -1)
                {
                    //if the player is upside down, flip vertically as well.
                    if (drawPlayer.gravDir == -1)
                    {
                        flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                        drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                        drawX = (int)(drawPlayer.position.X + 12 + drawPlayer.gfxOffY);
                    }
                    else
                    {
                        flip = SpriteEffects.FlipHorizontally;
                        drawX = (int)(drawPlayer.position.X + 12);
                    }
                }

                //If the player is upside down (e.g. gravity potion), flip vert
                if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
                {
                    flip = SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                }

                Rectangle sourceRectangle = drawPlayer.bodyFrame;

                DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), sourceRectangle, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.hairColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

                Main.playerDrawData.Add(data);
            }
        });

        public static readonly PlayerLayer Harpy_ChestFeathers_Female = new PlayerLayer("Harpy", "Harpy_ChestFeathers_Female", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            if (Female)
            {
                Harpy_ChestFeathers_Female.visible = true;
                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

                Texture2D texture = mod.GetTexture("Content/RaceTextures/Harpy/Harpy_ChestFeathers_Female");

                int drawX = (int)(drawPlayer.position.X + 10);
                int drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);

                if ((drawPlayer.bodyFrame.Y >= (7 * 56) && drawPlayer.bodyFrame.Y <= (9 * 56)) || (drawPlayer.bodyFrame.Y >= (14 * 56) && drawPlayer.bodyFrame.Y <= (16 * 56)))
                {
                    drawY = (int)(drawPlayer.position.Y + 18 + drawPlayer.gfxOffY);
                }

                //Initialize the sprite effect I'll be using to flip the sprite
                SpriteEffects flip = SpriteEffects.None;

                //if the player is facing left, flip horiz
                if (drawPlayer.direction == -1)
                {
                    //if the player is upside down, flip vertically as well.
                    if (drawPlayer.gravDir == -1)
                    {
                        flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

                        drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                        drawX = (int)(drawPlayer.position.X + 12 + drawPlayer.gfxOffY);
                    }
                    else
                    {
                        flip = SpriteEffects.FlipHorizontally;
                        drawX = (int)(drawPlayer.position.X + 12);
                    }
                }

                //If the player is upside down (e.g. gravity potion), flip vert
                if (drawPlayer.gravDir == -1 && drawPlayer.direction != -1)
                {
                    flip = SpriteEffects.FlipVertically;

                    drawY = (int)(drawPlayer.position.Y + 32 + drawPlayer.gfxOffY);
                }

                Rectangle sourceRectangle = drawPlayer.bodyFrame;

                DrawData data = new DrawData(texture, (new Vector2(drawX, drawY) - Main.screenPosition), sourceRectangle, drawPlayer.GetImmuneAlphaPure(Lighting.GetColor(drawX / 16, drawY / 16, drawPlayer.hairColor), drawInfo.shadow), 0f, new Vector2(20, 28), 1f, flip, 0);

                Main.playerDrawData.Add(data);
            }
        });
    }
}