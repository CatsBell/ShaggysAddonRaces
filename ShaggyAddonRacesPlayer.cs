using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.GameInput;
using MrPlagueRaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using System.Runtime;
using Terraria.ModLoader.IO;
using ShaggyAddonRaces.Content.Buffs;

namespace ShaggyAddonRaces
{
    public class ShaggyAddonRacesPlayer : ModPlayer
    {
        private int RacialKeySwitch = 0;
        private bool AutoPressing = false;
        private bool RacialToggle = true;
        private int spelunkerradius = 30;

        private ModHotKey RacialHotKey = MrPlagueRaces.MrPlagueRaces.RacialAbilityHotKey;
        private MrPlagueRaces.MrPlagueRaces _MrPlagueRaces = ModLoader.GetMod("MrPlagueRaces") as MrPlagueRaces.MrPlagueRaces;
        private MrPlagueRaces.MrPlagueRacesPlayer modPlayer;

        public override void OnEnterWorld(Player player)
        {
            modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            Main.NewText("Shaggy's Addon Races: ''Racial ability can toggle'' is set to true by default. (It's located in Mod Configuration if you want to change it!)");
        }

        public override void PostUpdate()
        {

            if (RacialHotKey == null)
            {
                RacialHotKey = MrPlagueRaces.MrPlagueRaces.RacialAbilityHotKey;
            }

            if (RacialToggle && RacialHotKey != null)
            {
                //Turns on the auto held racial key mechanic.
                if (RacialHotKey.JustPressed && !AutoPressing)
                {
                    AutoPressing = true;
                }

                //Turns off the auto held racial key mechanic.
                else if (RacialHotKey.JustPressed && AutoPressing)
                {
                    AutoPressing = false;
                }

                if (AutoPressing == true)
                {
                    if (modPlayer.RaceStats)
                    {
                        //Vampire
                        if (player.HasBuff(_MrPlagueRaces.BuffType("VampireBat")))
                        {
                            player.AddBuff(_MrPlagueRaces.BuffType("VampireBat"), 2);
                        }
                        /*
                        //Kobold
                        if (player.nightVision == true)
                        {
                            if (player.pickSpeed == (player.pickSpeed -= 0.6f))
                            {
                                if ((player.Center - Main.player[Main.myPlayer].Center).Length() < (float)(Main.screenWidth + spelunkerradius * 16))
                                {
                                    int playerX = (int)player.Center.X / 16;
                                    int playerY = (int)player.Center.Y / 16;
                                    int num3;
                                    for (int playerX2 = playerX - spelunkerradius; playerX2 <= playerX + spelunkerradius; playerX2 = num3 + 1)
                                    {
                                        for (int playerY2 = playerY - spelunkerradius; playerY2 <= playerY + spelunkerradius; playerY2 = num3 + 1)
                                        {
                                            if (Main.rand.Next(4) == 0)
                                            {
                                                Vector2 vector16 = new Vector2((float)(playerX - playerX2), (float)(playerY - playerY2));
                                                if (vector16.Length() < (float)spelunkerradius && playerX2 > 0 && playerX2 < Main.maxTilesX - 1 && playerY2 > 0 && playerY2 < Main.maxTilesY - 1 && Main.tile[playerX2, playerY2] != null && Main.tile[playerX2, playerY2].active())
                                                {
                                                    bool flag3 = false;
                                                    if (Main.tile[playerX2, playerY2].type == 185 && Main.tile[playerX2, playerY2].frameY == 18)
                                                    {
                                                        if (Main.tile[playerX2, playerY2].frameX >= 576 && Main.tile[playerX2, playerY2].frameX <= 882)
                                                        {
                                                            flag3 = true;
                                                        }
                                                    }
                                                    else if (Main.tile[playerX2, playerY2].type == 186 && Main.tile[playerX2, playerY2].frameX >= 864 && Main.tile[playerX2, playerY2].frameX <= 1170)
                                                    {
                                                        flag3 = true;
                                                    }
                                                    if (flag3 || Main.tileSpelunker[(int)Main.tile[playerX2, playerY2].type] || (Main.tileAlch[(int)Main.tile[playerX2, playerY2].type] && Main.tile[playerX2, playerY2].type != 82))
                                                    {
                                                        int spelunkerdust = Dust.NewDust(new Vector2((float)(playerX2 * 16), (float)(playerY2 * 16)), 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
                                                        Main.dust[spelunkerdust].fadeIn = 0.75f;
                                                        Dust dust = Main.dust[spelunkerdust];
                                                        dust.velocity *= 0.1f;
                                                        Main.dust[spelunkerdust].noLight = true;
                                                    }
                                                }
                                            }
                                            num3 = playerY2;
                                        }
                                        num3 = playerX2;
                                    }
                                }
                            }
                        }
                        */

                        //Examplian
                        if (SoundType.Custom.Equals("Sounds/Derpkin_Hurt"))
                        {
                            Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Derpkin_Hurt"));
                        }
                    }
                }
            }
        }

        public void PostUpdateMiscEffects(Player player)
        {
            //What kills the Kobold sunlight debuff
            if (player.armor[0].type == ItemID.Sunglasses || player.armor[0].type == ItemID.Goggles || player.armor[0].type == ItemID.SteampunkGoggles)
            {
                player.buffImmune[_MrPlagueRaces.BuffType("KoboldSunlight")] = true;
            }
        }
    }
}