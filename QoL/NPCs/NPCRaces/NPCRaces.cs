using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace ShaggyAddonRaces.QoL.NPCs.NPCRaces
{
    public class NPCRaces : GlobalNPC
    {
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            Mod mod = ModLoader.GetMod("ShaggyAddonRaces");

            //Merchant's Dragonkin race.
            if (npc.GivenName == "Alfred" || npc.GivenName == "Eugene" || npc.GivenName == "Harold" || npc.GivenName == "Louis" || npc.GivenName == "Walter" || npc.GivenName == "Howard")
            {
                Main.npcTexture[NPCID.Merchant] = mod.GetTexture("QoL/NPCs/NPCRaces/Merchant_Dra");
                
            }

            //Merchant's Kobold race.
            if (npc.GivenName == "Milton" || npc.GivenName == "Wilbur" || npc.GivenName == "Barney" || npc.GivenName == "Frank" || npc.GivenName == "Humphrey" || npc.GivenName == "Mortimer")
            {
                Main.npcTexture[NPCID.Merchant] = mod.GetTexture("QoL/NPCs/NPCRaces/Merchant_Kob");
            }

            //Merchant's Derpkin race.
            if (npc.GivenName == "Calvin" || npc.GivenName == "Frederick" || npc.GivenName == "Isaac" || npc.GivenName == "Edmund" || npc.GivenName == "Gilbert" || npc.GivenName == "Joseph")
            {
                Main.npcTexture[NPCID.Merchant] = mod.GetTexture("QoL/NPCs/NPCRaces/Merchant_Der");
            }

            //Guide's Kenku race.
            if (npc.GivenName == "Andrew" || npc.GivenName == "Brandon" || npc.GivenName == "Cody" || npc.GivenName == "Connor" || npc.GivenName == "Garrett" || npc.GivenName == "Jacob" || npc.GivenName == "Jeffrey" || npc.GivenName == "Kyle" || npc.GivenName == "Luke")
            {
                Main.npcTexture[NPCID.Guide] = mod.GetTexture("QoL/NPCs/NPCRaces/Guide_Ken");
            }

            //Guide's Mushfolk race.
            if (npc.GivenName == "Asher" || npc.GivenName == "Brett" || npc.GivenName == "Cole" || npc.GivenName == "Daniel" || npc.GivenName == "Harley" || npc.GivenName == "Jake" || npc.GivenName == "Joe" || npc.GivenName == "Levi" || npc.GivenName == "Marty")
            {
                Main.npcTexture[NPCID.Guide] = mod.GetTexture("QoL/NPCs/NPCRaces/Guide_Mus");
            }

            //Guide's Tabaxi race.
            //if (npc.GivenName == "Bradley" || npc.GivenName == "Brian" || npc.GivenName == "Colin" || npc.GivenName == "Dylan" || npc.GivenName == "Jack" || npc.GivenName == "Jeff" || npc.GivenName == "Kevin" || npc.GivenName == "Logan" || npc.GivenName == "Maxwell")
            //{
            //    Main.npcTexture[NPCID.Guide] = mod.GetTexture("QoL/NPCs/NPCRaces/Guide_Tab");
            //}

            //Guide's voodoo doll
            if (Main.npcTexture[NPCID.Guide] == mod.GetTexture("QoL/NPCs/NPCRaces/Guide_Ken"))
            {
                Main.itemTexture[ItemID.GuideVoodooDoll] = mod.GetTexture("QoL/NPCs/NPCRaces/Guide_VoodooDoll_Kenku");
            }
            else if (Main.npcTexture[NPCID.Guide] == mod.GetTexture("QoL/NPCs/NPCRaces/Guide_Mus"))
            {
                Main.itemTexture[ItemID.GuideVoodooDoll] = mod.GetTexture("QoL/NPCs/NPCRaces/Guide_VoodooDoll_Mush");
            }

            return true;
        }
    }
}