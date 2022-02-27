using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MrPlagueRaces.Content.Projectiles
{
	public class MushfolkHeal : ModProjectile
	{
		public void SetDefaults(Player player)
		{
			projectile.width = 34;
			projectile.height = 14;
            projectile.alpha = 10;
            projectile.friendly = false;
			projectile.alpha = 200;
            projectile.tileCollide = true;
			projectile.timeLeft = 6000;
			projectile.ignoreWater = false;
			aiType = ProjectileID.Bullet;
		}

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override void AI()
		{
			Rectangle bounds = projectile.getRect();
		}
	}
}