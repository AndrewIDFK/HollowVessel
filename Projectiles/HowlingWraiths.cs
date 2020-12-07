using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles
{

	public class HowlingWraiths : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pew Pew upwards");
		}

		public override void SetDefaults()
		{
			projectile.width = 0;
			projectile.height = 0;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.penetrate = -1;
		}

		int Timer;
		public override void AI()
		{
			
			Player player = Main.player[projectile.owner];
			projectile.velocity.X = 0;
			Timer++;
			if(Timer < 1)
			{
				projectile.Center = new Vector2(player.Center.X, player.Center.Y - 240);
			}
			if(Timer <= 8)
			{
				projectile.width += 25;
				projectile.height += 50;
				projectile.velocity.Y = -5;
			}
			else projectile.velocity.Y = 0;
		}
		
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 12; i++) 
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].scale *= 1.75f;
				Main.dust[dustIndex].noGravity = true;
			}
		}
	}
}
