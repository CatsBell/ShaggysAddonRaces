using Terraria.ModLoader;
using CustomSlot;
using CustomSlot.UI;
using Terraria.UI;

namespace ShaggyAddonRaces
{
    public class ShaggyAddonRaces : Mod
    {

        public string RaceDisplayName;
        public CustomItemSlot tailSlot;

        public static ShaggyAddonRaces Instance { get; private set; }

        public override void Load()
        {
            //this is essential, loads this mod's custom races
            MrPlagueRaces.Core.Loadables.LoadableManager.Autoload(this);
            
            tailSlot = new CustomItemSlot(ItemSlot.Context.EquipAccessory, 1f, CustomItemSlot.ArmorType.Head)
            {
                ItemVisible = true,
                HoverText = "You shouldn't see this.",
                IsValidItem = item => item.type > 0
            };
            tailSlot.Left.Set(56, 0);
            tailSlot.Top.Set(64, 0);
        }
        public override void Unload()
        {
            //this is essential, unloads this mod's custom races
            MrPlagueRaces.Common.Races.RaceLoader.Races.Clear();
            MrPlagueRaces.Common.Races.RaceLoader.RacesByLegacyIds.Clear();
            MrPlagueRaces.Common.Races.RaceLoader.RacesByFullNames.Clear();
            MrPlagueRaces.Core.Loadables.LoadableManager.Unload();
        }
    }
}