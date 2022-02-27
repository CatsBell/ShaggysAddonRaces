using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShaggyAddonRaces.Content.NPCs.NPCRaces
{
    [Label("Client Configuration")]
    public class RaceQoLRacialConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(typeof(bool), "true")]
        [Label("Racial ability can toggle")]
        public bool RacialToggle { get; set; }
    }
}