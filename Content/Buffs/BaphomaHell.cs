using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace ShaggyAddonRaces.Content.Buffs
{
	public class BaphomaHell : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Purifying");
			Description.SetDefault("You feel like you're on fire!");
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = true;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.lifeRegenTime = 0;
			player.lifeRegen -= 1;
			player.blockRange -= 1;
			player.tileSpeed -= 0.1f;
			player.wallSpeed -= 0.1f;
			player.jumpSpeedBoost -= 0.2f;
			player.pickSpeed += 0.2f;
			player.moveSpeed -= 0.5f;
		}
	}
}