using System;
using HollowVessel.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles.Orbs
{

	public class SoulMeterOrb2 : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Meter Second Orb");
			Main.projFrames[projectile.type] = 1;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 0f;
			projectile.penetrate = -1;
			projectile.timeLeft = 666666;
			projectile.hide = true;
			//projectile.alpha = 145;
		}

		public override bool? CanCutTiles()
		{
			return new bool?(false);
		}

		public override bool MinionContactDamage()
		{
			return false;
		}

		int ihatetimers;
		int ihatetimers2;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			
			var modPlayer = Main.LocalPlayer.GetModPlayer<SoulMeterPlayer>();
			
			
			
			Vector2 idlePosition = player.Center;
			idlePosition.X = player.Center.X - 30;
			idlePosition.Y = player.Center.Y - 45;
			float distanceToIdlePosition = (idlePosition - projectile.Center).Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
			{
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}
			projectile.position = idlePosition;
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.32f);
			
			ihatetimers++;
			/*ihatetimers2++;
			if(ihatetimers > 1)
			{*/
				/*int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType("SoulOrbDust"), 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].scale *= 0.65f;
				Main.dust[dustIndex].noGravity = true;
				ihatetimers2++;
				if(ihatetimers2 > 20)
				{
					Main.dust[dustIndex].active = false;
					ihatetimers2 = 0;
				}*/
				
			HollowPlayer mPlayer = player.GetModPlayer<HollowPlayer>();
			
			
			if(mPlayer.soulOrbActive2 == true)
			{	
				Vector2 drawPos = projectile.Center - Main.screenPosition;
				
				Vector2 circularMotion = new Vector2(projectile.width, projectile.height) * projectile.scale * 0.95f;
				circularMotion /= 2f;
				Vector2 realCircularMotion = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * circularMotion;
				for (int i = 0; i < 2; i++) 
				{
					int numDust = Dust.NewDust(projectile.Center + circularMotion, 0, 0, 264);
					Main.dust[numDust].position = realCircularMotion + projectile.Center;
					Main.dust[numDust].position += player.velocity;
					Main.dust[numDust].velocity.X = player.velocity.X / 3;
					Main.dust[numDust].velocity.Y = player.velocity.Y / 3;
					Main.dust[numDust].scale *= 1.15f;
				
					/*if(Main.dust[numDust].scale <= 0.65f)
					{
						Main.dust[numDust].active = false;
						Main.dust[numDust].alpha = 255;
					}*/
					Main.dust[numDust].noGravity = true;
				}
			}
			else
			{
				projectile.Kill();
				//projectile.timeLeft = 2;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++) 
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 263, 0f, 0f, 100, new Color(189, 189, 189, 100), 2f);
				Main.dust[dustIndex].scale *= 0.6f;
				Main.dust[dustIndex].noGravity = true;
			}
		}
	}
}
