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
                        if (player.HasBuff(_MrPlagueRaces.BuffType("VampireBat")))
                        {
                            player.AddBuff(_MrPlagueRaces.BuffType("VampireBat"), 2);
                        }
                    }

                    if (SoundType.Custom.Equals("Sounds/Derpkin_Hurt"))
                    {
                        Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Derpkin_Hurt"));
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