using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles
{

	public class CrystalDashAttack : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Heart's Border");
		}

		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 48;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 23;
			projectile.hide = true;
			projectile.scale = 1;
		}

		int ihatetimers;
		int centerPosition = 30;
		bool isDirectionRight = false;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			ihatetimers++;
			projectile.height -= 1;
			
			if(ihatetimers <= 1)
			{
				if(player.direction == -1)
				{
					isDirectionRight = false;
				}
				else isDirectionRight = true;
			}
			if(isDirectionRight == true)
			{
				centerPosition--;
				projectile.Center = new Vector2(player.Center.X - centerPosition, player.Center.Y);
			}
			else 
			{
				centerPosition--;
				projectile.Center = new Vector2(player.Center.X + centerPosition, player.Center.Y);
			}
			Lighting.AddLight(projectile.Center, Color.Purple.ToVector3() * 0.68f);
			
			for (int i = 0; i < 6; i++) 
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 254, 0f, 0f, 100, Color.Purple, 2f);
				Main.dust[dustIndex].scale *= 0.75f;
				Main.dust[dustIndex].noGravity = true;
			}
			
		}
		
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 8; i++) 
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 254, 0f, 0f, 100, Color.Purple, 2f);
				Main.dust[dustIndex].scale *= 0.75f;
				Main.dust[dustIndex].noGravity = true;
			}
		}
	}
}
