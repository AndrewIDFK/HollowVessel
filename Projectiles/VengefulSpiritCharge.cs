using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles
{

	public class VengefulSpiritCharge : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chargin' Vengeful Spirit");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.penetrate = -1;
		}

		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		int ihatetimers;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			Vector2 idlePosition = player.Center;
			idlePosition.X = player.Center.X - 7;
			idlePosition.Y = player.Center.Y - 6;
			float distanceToIdlePosition = (idlePosition - projectile.Center).Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
			{
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}
			projectile.position = idlePosition;
			float projectileAngle = (Main.MouseWorld - projectile.position).ToRotation() % 6.28318548f;
			
			if (projectileAngle < 1.57079637f && projectileAngle > -1.57079637f)
			{
				projectile.rotation = projectileAngle;
				projectile.spriteDirection = 1;
			}
			else
			{
				projectile.rotation = projectileAngle + 3.14159274f;
				projectile.spriteDirection = -1;
			}
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
			
			if (projectile.owner == Main.myPlayer)
            {
				Main.PlaySound(SoundID.Item13, projectile.position);
                ihatetimers++;
                if (ihatetimers > 60)
                {
					player.GetModPlayer<HollowPlayer>().shakeTimer = 15;
					
					Main.PlaySound(SoundID.Item13, projectile.position);
                    Projectile.NewProjectile(projectile.Center, new Vector2(8f, 0f).RotatedBy(projectileAngle), mod.ProjectileType("VengefulSpirit"), 50, 7.5f, projectile.owner);
					projectile.netUpdate = true;
                    
					player.velocity = new Vector2(-8f, 0f).RotatedBy(projectileAngle) * 0.75f;
					//player.velocity.Y = playerAngle * 1.2f;
					
					ihatetimers = 0;
					projectile.Kill();
                }
				else if (ihatetimers < 60)
				{
					for (int i = 0; i < 3; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
						Main.dust[dustIndex].scale *= 0.55f;
						Main.dust[dustIndex].noGravity = true;
					}
				}
			}
		}
		
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 8; i++) 
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].scale *= 0.75f;
				Main.dust[dustIndex].noGravity = true;
			}
		}
	}
}
