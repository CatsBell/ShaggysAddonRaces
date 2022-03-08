using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ShaggyAddonRaces.Content.Buffs
{
	public class ImmuneToSun : ModBuff
	{
		private MrPlagueRaces.MrPlagueRaces _MrPlagueRaces = ModLoader.GetMod("MrPlagueRaces") as MrPlagueRaces.MrPlagueRaces;
		private MrPlagueRaces.MrPlagueRacesPlayer modPlayer;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sun Immunity");
			Description.SetDefault("The sun seems so dim...");
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
			canBeCleared = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[_MrPlagueRaces.BuffType("KoboldSunlight")] = true;
			player.buffImmune[_MrPlagueRaces.BuffType("VampireBurn")] = true;
		}
	}
}