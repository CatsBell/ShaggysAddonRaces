using Terraria.ModLoader;

namespace ShaggyAddonRaces
{
    public class ShaggyAddonRaces : Mod
    {

        public string RaceDisplayName;

        public static ShaggyAddonRaces Instance { get; private set; }
        public override void Load()
        {
            //this is essential, loads this mod's custom races
            MrPlagueRaces.Core.Loadables.LoadableManager.Autoload(this);
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