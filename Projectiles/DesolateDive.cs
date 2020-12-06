using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles
{

	public class DesolateDive : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goin' Down!");
		}

		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 12;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.penetrate = -1;
		}

		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		int ihatetimers;
		int fallTimer;
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
			
			if(projectile.owner == Main.myPlayer)
			{
				if (player.velocity.Y != 0)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X - 5, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].scale *= 0.925f;
					Main.dust[dustIndex].noGravity = true;
					
					int dustFeet = Dust.NewDust(new Vector2(projectile.position.X - 5, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustFeet].scale *= 0.625f;
					Main.dust[dustFeet].noGravity = true;
					Main.PlaySound(SoundID.Item13, projectile.position);
					player.GetModPlayer<HollowPlayer>().desolateDiveFall = true;

			
				}
				if (player.velocity.Y == 0f && player.oldVelocity.Y == 0f && !(player.mount.CanFly && player.mount.Active))
				
				{
					player.GetModPlayer<HollowPlayer>().desolateDiveFall = false;
					player.GetModPlayer<HollowPlayer>().shakeTimer = 27;
					Main.PlaySound(SoundID.Item13, projectile.position);
					Projectile.NewProjectile(player.Center, new Vector2(0f, 0f), mod.ProjectileType("DesolateDivePound"), 150 * 3, 10.5f, projectile.owner);
					projectile.netUpdate = true;
					
					fallTimer = 0;
					Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Splat"));
					for (int i = 0; i < 12; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
						Main.dust[dustIndex].scale *= 0.85f;
						Main.dust[dustIndex].noGravity = true;
					}
					projectile.Kill();
				}
			}
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
