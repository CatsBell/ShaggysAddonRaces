using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ShaggyAddonRaces.Content.Buffs
{
	public class Home : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Comfortable");
			Description.SetDefault("You feel safe here.");
			Description.SetDefault("Stats increase by 20%.");
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
			canBeCleared = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.lifeRegen += (int)0.2f;
            player.manaRegenBonus += (int)0.2f;
            player.statDefense += 2;
            player.meleeSpeed += 0.2f;
            player.armorPenetration += (int)0.2f;
            player.bulletDamage += 0.2f;
            player.rocketDamage += 0.2f;
            player.manaCost += 0.2f;
            player.minionKB += 0.2f;
            player.meleeCrit += (int)0.2f;
            player.rangedCrit += (int)0.2f;
            player.magicCrit += (int)0.2f;
            player.pickSpeed += 0.2f;
            player.tileSpeed += 0.2f;
            player.wallSpeed += 0.2f;
            player.blockRange += (int)0.2f;
            player.arrowDamage += 0.2f;
            player.moveSpeed += (int)0.2f;
            player.extraFall += (int)0.2f;
            player.allDamage += (int)0.2f;
            player.maxRunSpeed += 0.2f;
        }
	}
}