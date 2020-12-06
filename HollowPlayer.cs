using System;
using HollowVessel.Projectiles;
using HollowVessel.Projectiles.Orbs;
using HollowVessel.UI;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
//using CalamityMod.CalPlayer;

namespace HollowVessel
{
	public class HollowPlayer : ModPlayer
	{	

		public bool soulOrbActive;
		public bool soulOrbActive2;
		public bool soulOrbActive3;
		
		public bool mothwingCloak;
		public bool crystalHeart;
		public bool shadeCloak;
		public bool desolateDive;
		public bool desolateDiveFall;
		public bool mantisClaw;
		public bool monarchWings;
		public bool monarchWingsAgain;
		public int monarchCount;
		public bool ismasTear;
		
		public bool dashSlash;
		public int dashSlashTimer;
	
		public int cloakDash;
		public int shadeDash;
		public int cloakDashTime;
		public int shadeDashTime;
		public int crystalDashTime;
		
		public int shakeTimer;
		public static float jumpSpeed = 5.01f;
		public static int jumpHeight = 15;
		
		public override void ResetEffects()
		{
			mothwingCloak = false;
			shadeCloak = false;
			crystalHeart = false;
			desolateDive = false;
			desolateDiveFall = false;
			mantisClaw = false;
			monarchWings = false;
			monarchWingsAgain = false;
			dashSlash = false;
			ismasTear = false;
			cloakDash = 0;
			shadeDash = 0;
		}
		
		public override void UpdateDead()
		{
			mothwingCloak = false;
			shadeCloak = false;
			crystalHeart = false;
			desolateDive = false;
			desolateDiveFall = false;
			mantisClaw = false;
			monarchWings = false;
			monarchWingsAgain = false;
			dashSlash = false;
			ismasTear = false;
			
		}
		
		public override void Initialize()
		{
			soulOrbActive = false;
			soulOrbActive2 = false;
			soulOrbActive3 = false;
			monarchCount = 1;
		}
		
		
		public override void ModifyScreenPosition()
		{
			if (shakeTimer > 0)
			{
				shakeTimer--;
				Vector2 shake = new Vector2(Main.rand.NextFloat(shakeTimer), Main.rand.NextFloat(shakeTimer));
				Main.screenPosition += shake;
			}
		}
		
		public override void PostUpdateRunSpeeds()
		{
			if (player.pulley && cloakDash > 0 || player.pulley && shadeDash > 0) 
			{
				ModDashMovement();
				return;
			}
			if (player.grappling[0] == -1 && !player.tongued)
			{
				ModHorizontalMovement();
				if (cloakDash > 0)
				{
					ModDashMovement();
				}
				if (shadeDash > 0)
				{
					ModDashMovement();
				}
			}
		}
		public void ModDashMovement()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<SoulMeterPlayer>();
			if (player.dashDelay > 0)
			{
				return;
			}
			
			if (cloakDash == 1 && player.dashDelay < 0 && player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle5 = new Rectangle((int)((double)player.position.X + (double)player.velocity.X * 0.5 - 4.0), (int)((double)player.position.Y + (double)player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
				for (int m = 0; m < 200; m++)
				{
					if (Main.npc[m].active && !Main.npc[m].dontTakeDamage && !Main.npc[m].friendly && !Main.npc[m].townNPC && Main.npc[m].immune[player.whoAmI] <= 0 && Main.npc[m].damage > 0)
					{
						NPC npc5 = Main.npc[m];
						Rectangle rect5 = npc5.getRect();
						if (rectangle5.Intersects(rect5) && (npc5.noTileCollide || player.CanHit(npc5)))
						{
							this.OnDodge();
							break;
						}
					}
				}
				for (int n = 0; n < 1000; n++)
				{
					if (Main.projectile[n].active && !Main.projectile[n].friendly && Main.projectile[n].hostile && Main.projectile[n].damage > 0)
					{
						Rectangle rect6 = Main.projectile[n].getRect();
						if (rectangle5.Intersects(rect6))
						{
							this.OnDodge();
							break;
						}
					}
				}
			}
			if (shadeDash == 1 && player.dashDelay < 0 && player.whoAmI == Main.myPlayer)
			{
				Rectangle rectangle5 = new Rectangle((int)((double)player.position.X + (double)player.velocity.X * 0.5 - 4.0), (int)((double)player.position.Y + (double)player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
				for (int m = 0; m < 200; m++)
				{
					if (Main.npc[m].active && !Main.npc[m].dontTakeDamage && !Main.npc[m].friendly && !Main.npc[m].townNPC && Main.npc[m].immune[player.whoAmI] <= 0 && Main.npc[m].damage > 0)
					{
						NPC npc5 = Main.npc[m];
						Rectangle rect5 = npc5.getRect();
						if (rectangle5.Intersects(rect5) && (npc5.noTileCollide || player.CanHit(npc5)))
						{
							this.OnDodge();
							break;
						}
					}
				}
				for (int n = 0; n < 1000; n++)
				{
					if (Main.projectile[n].active && !Main.projectile[n].friendly && Main.projectile[n].hostile && Main.projectile[n].damage > 0)
					{
						Rectangle rect6 = Main.projectile[n].getRect();
						if (rectangle5.Intersects(rect6))
						{
							this.OnDodge();
							break;
						}
					}
				}
			}
			
			if (player.dashDelay < 0)
			{				
				int dashDelay = 25;
				
				if (cloakDash == 1) //Mothwing Cloak
				{
					for (int num16 = 0; num16 < 6; num16++)
					{
						int dustz;
						int dustz2;
						if (player.velocity.Y == 0f)
						{							
							if(Main.rand.Next(2) == 1)
							{
								dustz = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 264, 0f, 0f, 100, default(Color), 1.4f);
								Main.dust[dustz].velocity *= 0.87f;
								Main.dust[dustz].noGravity = true;
								Main.dust[dustz].scale *= 0.65f + (float)Main.rand.Next(65) * 0.021f;
							}
						}
						else
						{		
							if(Main.rand.Next(2) == 1)
							{
								dustz = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 264, 0f, 0f, 100, default(Color), 1.4f);
								
								Main.dust[dustz].velocity *= 0.87f;
								Main.dust[dustz].noGravity = true;
								Main.dust[dustz].scale *= 0.6f + (float)Main.rand.Next(65) * 0.021f;
							}
						}
					}
				}
				
				if (cloakDash == 1)
				{
					float num12 = 26.5f;
					float num13 = 0.92f;
					float num14 = Math.Max(player.accRunSpeed / 5, player.maxRunSpeed);
					float num15 = 0.84f;
					
					player.vortexStealthActive = false;
					if (player.velocity.X > num12 || player.velocity.X < -num12)
					{
						player.velocity.Y = 0;
						player.noFallDmg = true;
						//player.gravity = 0;
						player.velocity.X = player.velocity.X * num13;
						return;
					}
					if (player.velocity.X > num14 || player.velocity.X < -num14)
					{
						player.velocity.Y = 0;
						player.noFallDmg = true;
						//player.gravity = 0;
						player.velocity.X = player.velocity.X * num15;
						return;
					}
					player.dashDelay = dashDelay;
					
					if (player.velocity.X < 0f)
					{
						player.velocity.X = -num14;
						return;
					}
					if (player.velocity.X > 0f)
					{

						player.velocity.X = num14;
						return;
					}
				}
				
				if (shadeDash == 1) //Shade Cloak
				{
					for (int num16 = 0; num16 < 6; num16++)
					{
						int dustz;
						int dustz2;
						if (player.velocity.Y == 0f)
						{							
							if(Main.rand.Next(2) == 1)
							{
								dustz = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 264, 0f, 0f, 0, Color.Black, 1.1f);
								Main.dust[dustz].velocity *= 0.87f;
								Main.dust[dustz].noGravity = true;
								Main.dust[dustz].scale *= 0.65f + (float)Main.rand.Next(65) * 0.021f;
							}
						}
						else
						{		
							if(Main.rand.Next(2) == 1)
							{
								dustz = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 264, 0f, 0f, 0, Color.Black, 1.1f);
								
								Main.dust[dustz].velocity *= 0.87f;
								Main.dust[dustz].noGravity = true;
								Main.dust[dustz].scale *= 0.6f + (float)Main.rand.Next(65) * 0.021f;
							}
						}
					}
				}
				
				if (shadeDash == 1)
				{
					float num12 = 26.5f;
					float num13 = 0.92f;
					float num14 = Math.Max(player.accRunSpeed / 5, player.maxRunSpeed);
					float num15 = 0.84f;
					
					player.vortexStealthActive = false;
					if (player.velocity.X > num12 || player.velocity.X < -num12)
					{
						player.velocity.Y = 0;
						player.noFallDmg = true;
						//player.gravity = 0;
						player.velocity.X = player.velocity.X * num13;
						return;
					}
					if (player.velocity.X > num14 || player.velocity.X < -num14)
					{
						player.velocity.Y = 0;
						player.noFallDmg = true;
						//player.gravity = 0;
						player.velocity.X = player.velocity.X * num15;
						return;
					}
					player.dashDelay = dashDelay;
					
					if (player.velocity.X < 0f)
					{
						player.velocity.X = -num14;
						return;
					}
					if (player.velocity.X > 0f)
					{

						player.velocity.X = num14;
						return;
					}
				}
				
				if (cloakDash == 2) //Crystal Heart
				{
					for (int num16 = 0; num16 < 6; num16++)
					{
						int dustz;
						int dustz2;
						if (player.velocity.Y == 0f)
						{							
							if(Main.rand.Next(2) == 1)
							{
								dustz = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 264, 0f, 0f, 100, Color.Purple, 1.4f);
								Main.dust[dustz].velocity *= 0.87f;
								Main.dust[dustz].noGravity = true;
								Main.dust[dustz].scale *= 0.65f + (float)Main.rand.Next(65) * 0.021f;
							}
						}
						else
						{		
							if(Main.rand.Next(2) == 1)
							{
								dustz = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 264, 0f, 0f, 100, default(Color), 1.4f);
								
								Main.dust[dustz].velocity *= 0.87f;
								Main.dust[dustz].noGravity = true;
								Main.dust[dustz].scale *= 0.6f + (float)Main.rand.Next(65) * 0.021f;
							}
							if(Main.rand.Next(8) == 1)
							{
								dustz = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 141, 0f, 0f, 100, default(Color), 1.4f);
								
								Main.dust[dustz].velocity *= 0.87f;
								Main.dust[dustz].noGravity = true;
								Main.dust[dustz].scale *= 0.6f + (float)Main.rand.Next(50) * 0.021f;
							}
						}
					}
				}
				
				if (cloakDash == 2)
				{
					float num12 = 29.5f;
					float num13 = 0.98f;
					float num14 = Math.Max(player.accRunSpeed / 4, player.maxRunSpeed);
					float num15 = 0.88f;
					
					player.vortexStealthActive = false;
					if (player.velocity.X > num12 || player.velocity.X < -num12)
					{
						player.velocity.Y = 0;
						player.noFallDmg = true;
						//player.gravity = 0;
						player.velocity.X = player.velocity.X * num13;
						return;
					}
					if (player.velocity.X > num14 || player.velocity.X < -num14)
					{
						player.velocity.Y = 0;
						player.noFallDmg = true;
						//player.gravity = 0;
						player.velocity.X = player.velocity.X * num15;
						return;
					}
					player.dashDelay = dashDelay;
					
					if (player.velocity.X < 0f)
					{
						player.velocity.X = -num14;
						return;
					}
					if (player.velocity.X > 0f)
					{

						player.velocity.X = num14;
						return;
					}
				}
				
			}
			else if (cloakDash > 0 && !player.mount.Active || shadeDash > 0 && !player.mount.Active)
			{
				
				if (cloakDash == 1)
				{
					int num28 = 0;
					bool flag = false;
					if (cloakDashTime > 0)
					{
						cloakDashTime--;
					}
					if (cloakDashTime < 0)
					{
						cloakDashTime++;
					}
					if (player.controlRight && player.releaseRight)
					{
						if (cloakDashTime > 0)
						{
							num28 = 1;
							flag = true;
							cloakDashTime = 0;	
						}
						else
						{
							cloakDashTime = 15;
						}
						
					}
					else if (player.controlLeft && player.releaseLeft)
					{
						if (cloakDashTime < 0)
						{
							num28 = -1;
							flag = true;
							cloakDashTime = 0;
						}
						else
						{
							cloakDashTime = -15;
						}
						
					}
					if (flag)
					{
						if(dashSlash)
						{	
							dashSlashTimer = 1;
						}	
						player.velocity.X = 29.5f * (float)num28;
						Point point = (player.Center + new Vector2((float)(num28 * player.width / 2 + 2), 0f)).ToTileCoordinates();
						Point point2 = (player.Center + new Vector2((float)(num28 * player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
						{
							player.velocity.X = player.velocity.X / 2f;
						}
						player.dashDelay = -1;
						for (int num29 = 0; num29 < 20; num29++)
						{
							int num30 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 263, 0f, 0f, 100, default(Color), 2f);
							Dust dust = Main.dust[num30];
							dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
							Dust dust2 = Main.dust[num30];
							dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num30].velocity *= 0.2f;
							Main.dust[num30].noGravity = true;
							Main.dust[num30].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num30].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
						}
						return;
					}
				}
				
				if (shadeDash == 1)
				{
					int num28 = 0;
					bool flag = false;
					if (shadeDashTime > 0)
					{
						shadeDashTime--;
					}
					if (shadeDashTime < 0)
					{
						shadeDashTime++;
					}
					if (player.controlRight && player.releaseRight)
					{
						if (shadeDashTime > 0)
						{
							num28 = 1;
							flag = true;
							shadeDashTime = 0;	
						}
						else
						{
							shadeDashTime = 15;
						}
						
					}
					else if (player.controlLeft && player.releaseLeft)
					{
						if (shadeDashTime < 0)
						{
							num28 = -1;
							flag = true;
							shadeDashTime = 0;
						}
						else
						{
							shadeDashTime = -15;
						}
						
					}
					if (flag)
					{
						if(dashSlash)
						{	
							dashSlashTimer = 1;
						}	
						player.velocity.X = 29.5f * (float)num28;
						Point point = (player.Center + new Vector2((float)(num28 * player.width / 2 + 2), 0f)).ToTileCoordinates();
						Point point2 = (player.Center + new Vector2((float)(num28 * player.width / 2 + 2), 0f)).ToTileCoordinates();
						if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
						{
							player.velocity.X = player.velocity.X / 2f;
						}
						player.dashDelay = -1;
						for (int num29 = 0; num29 < 20; num29++)
						{
							int num30 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 263, 0f, 0f, 100, default(Color), 2f);
							Dust dust = Main.dust[num30];
							dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
							Dust dust2 = Main.dust[num30];
							dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-5, 6);
							Main.dust[num30].velocity *= 0.2f;
							Main.dust[num30].noGravity = true;
							Main.dust[num30].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
							Main.dust[num30].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
						}
						return;
					}
				}
				
				if (cloakDash == 2)
				{
					if(soulOrbActive)
					{
						int num28 = 0;
						bool flag = false;
						if (crystalDashTime > 0)
						{
							crystalDashTime--;
						}
						if (crystalDashTime < 0)
						{
							crystalDashTime++;
						}
						if (player.controlRight && player.releaseRight)
						{
							if (crystalDashTime > 0)
							{
								num28 = 1;
								flag = true;
								crystalDashTime = 0;	
							}
							else
							{
								crystalDashTime = 15;
							}
							
						}
						else if (player.controlLeft && player.releaseLeft)
						{
							if (crystalDashTime < 0)
							{
								num28 = -1;
								flag = true;
								crystalDashTime = 0;
							}
							else
							{
								crystalDashTime = -15;
							}
							
						}
						if (flag)
						{
							player.immune = true;
							player.immuneTime = 35;
							if (player.longInvince)
							{
								player.immuneTime += 35;
							}
							for (int i = 0; i < player.hurtCooldowns.Length; i++)
							{
								player.hurtCooldowns[i] = player.immuneTime;
							}
							if(dashSlash)
							{	
								dashSlashTimer = 1;
							}	
							modPlayer.soulMeterCurrent -= 33;
							Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<CrystalDashAttack>(), 480, 12.5f, player.whoAmI);
							player.velocity.X = 32f * (float)num28;
							Point point = (player.Center + new Vector2((float)(num28 * player.width / 2 + 2), 0f)).ToTileCoordinates();
							Point point2 = (player.Center + new Vector2((float)(num28 * player.width / 2 + 2), 0f)).ToTileCoordinates();
							if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
							{
								player.velocity.X = player.velocity.X / 2f;
							}
							player.dashDelay = -1;
							for (int num29 = 0; num29 < 20; num29++)
							{
								int num30 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 254, 0f, 0f, 100, default(Color), 2f);
								Dust dust = Main.dust[num30];
								dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
								Dust dust2 = Main.dust[num30];
								dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-5, 6);
								Main.dust[num30].velocity *= 0.2f;
								Main.dust[num30].noGravity = true;
								Main.dust[num30].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
								Main.dust[num30].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
							}
							return;
						}
					}
				}
			}
		}
		
		public void ModHorizontalMovement()
		{
			float num = (player.accRunSpeed + player.maxRunSpeed) / 2f;
			if (player.controlLeft && player.velocity.X > -player.accRunSpeed && player.dashDelay >= 0)
			{
				if (player.velocity.X < -num && player.velocity.Y == 0f && !player.mount.Active)
				{
					int num2 = 0;
					if (player.gravDir == -1f)
					{
						num2 -= player.height;
					}
					if (cloakDash == 1)
					{
						int num3 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num2), player.width + 8, 4, 263, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
						Main.dust[num3].velocity.X = Main.dust[num3].velocity.X * 0.2f;
						Main.dust[num3].velocity.Y = Main.dust[num3].velocity.Y * 0.2f;
						Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
						Main.dust[num3].scale *= 0.75f + (float)Main.rand.Next(10) * 0.014f;
					}
					if (shadeDash == 1)
					{
						//player.immune = true;
						//player.immuneTime = 70;
						int num3 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num2), player.width + 8, 4, 263, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 0, Color.Black, 1.5f);
						Main.dust[num3].velocity.X = Main.dust[num3].velocity.X * 0.2f;
						Main.dust[num3].velocity.Y = Main.dust[num3].velocity.Y * 0.2f;
						Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
						Main.dust[num3].scale *= 0.75f + (float)Main.rand.Next(10) * 0.014f;
					}
					if (cloakDash == 2)
					{
						int num3 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num2), player.width + 8, 4, 254, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, Color.Purple, 1.5f);
						Main.dust[num3].velocity.X = Main.dust[num3].velocity.X * 0.2f;
						Main.dust[num3].velocity.Y = Main.dust[num3].velocity.Y * 0.2f;
						Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
						Main.dust[num3].scale *= 0.75f + (float)Main.rand.Next(10) * 0.014f;
					}
					
					
				}

			}
			else if (player.controlRight && player.velocity.X < player.accRunSpeed && player.dashDelay >= 0 && player.velocity.X > num && player.velocity.Y == 0f && !player.mount.Active)
			{
				int num9 = 0;
				if (player.gravDir == -1f)
				{
					num9 -= player.height;
				}
				if (cloakDash == 1)
				{
					int num10 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num9), player.width + 8, 4, 263, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, default(Color), 1.5f);
					Main.dust[num10].velocity.X = Main.dust[num10].velocity.X * 0.2f;
					Main.dust[num10].velocity.Y = Main.dust[num10].velocity.Y * 0.2f;
					Main.dust[num10].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
					Main.dust[num10].scale *= 0.75f + (float)Main.rand.Next(10) * 0.0112f;
				}
				if (shadeDash == 1)
				{
					int num10 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num9), player.width + 8, 4, 263, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 0, Color.Black, 1.5f);
					Main.dust[num10].velocity.X = Main.dust[num10].velocity.X * 0.2f;
					Main.dust[num10].velocity.Y = Main.dust[num10].velocity.Y * 0.2f;
					Main.dust[num10].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
					Main.dust[num10].scale *= 0.75f + (float)Main.rand.Next(10) * 0.0112f;
				}
				if (cloakDash == 2)
				{
					int num10 = Dust.NewDust(new Vector2(player.position.X - 4f, player.position.Y + (float)player.height + (float)num9), player.width + 8, 4, 254, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 50, Color.Purple, 1.5f);
					Main.dust[num10].velocity.X = Main.dust[num10].velocity.X * 0.2f;
					Main.dust[num10].velocity.Y = Main.dust[num10].velocity.Y * 0.2f;
					Main.dust[num10].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
					Main.dust[num10].scale *= 0.75f + (float)Main.rand.Next(10) * 0.014f;
				}
			}
			else if (player.altFunctionUse == 1 && player.velocity.X < player.accRunSpeed && player.dashDelay >= 0 && player.velocity.X > num && player.velocity.Y == 0f && !player.mount.Active)
			{
				int num10 = 0;
				if (player.gravDir == -1f)
				{
					num10 -= player.height;
				}
			}
		}

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (HollowVessel.VengefulSpirit.JustPressed) 
			{	
				var modPlayer = Main.LocalPlayer.GetModPlayer<SoulMeterPlayer>();
				
				if(player.ownedProjectileCounts[mod.ProjectileType("SoulMeterOrb1")] >= 1)
				{
					if(player.HeldItem.type == mod.ItemType("DamagedNail"))
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
					if(player.HeldItem.type == mod.ItemType("OldNail"))
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
					if(player.HeldItem.type == mod.ItemType("DullNail"))
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
					if(player.HeldItem.type == mod.ItemType("SharpenedNail"))
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
					if(player.HeldItem.type == mod.ItemType("ChannelledNail"))
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
					if(player.HeldItem.type == mod.ItemType("CoiledNail")) 
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
					if(player.HeldItem.type == mod.ItemType("PellucidNail")) 
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
					if(player.HeldItem.type == mod.ItemType("PureNail")) 
					{	
						modPlayer.soulMeterCurrent -= 33;
						float speedX = 0f;
						float speedY = 0f;
						int projectile = Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, ModContent.ProjectileType<VengefulSpiritCharge>(), 0, 2f, player.whoAmI);
					}
				}					
			}
		}		
		
		private void OnDodge()
		{
			if (player.whoAmI == Main.myPlayer && mothwingCloak)
			{	
				for (int j = 0; j < 100; j++)
				{
					int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 263, 0f, 0f, 100, default(Color), 2f);
					Dust dust = Main.dust[num];
					dust.position.X = dust.position.X + (float)Main.rand.Next(-20, 21);
					Dust dust2 = Main.dust[num];
					dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-25, 26);
					Main.dust[num].velocity *= 0.4f;
					Main.dust[num].scale *= 0.6f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(player.cWaist, player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						Main.dust[num].noGravity = true;
					}
				}
				if (player.whoAmI == Main.myPlayer)
				{
					NetMessage.SendData(62, -1, -1, null, player.whoAmI, 1f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (player.whoAmI == Main.myPlayer && crystalHeart)
			{				
				player.immune = true;
				player.immuneTime = 35;
				if (player.longInvince)
				{
					player.immuneTime += 80;
				}
				for (int i = 0; i < player.hurtCooldowns.Length; i++)
				{
					player.hurtCooldowns[i] = player.immuneTime;
				}
				
				for (int j = 0; j < 100; j++)
				{
					int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 254, 0f, 0f, 100, default(Color), 2f);
					Dust dust = Main.dust[num];
					dust.position.X = dust.position.X + (float)Main.rand.Next(-20, 21);
					Dust dust2 = Main.dust[num];
					dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-25, 26);
					Main.dust[num].velocity *= 0.4f;
					Main.dust[num].scale *= 0.6f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(player.cWaist, player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						Main.dust[num].noGravity = true;
					}
				}
				if (player.whoAmI == Main.myPlayer)
				{
					NetMessage.SendData(62, -1, -1, null, player.whoAmI, 1f, 0f, 0f, 0, 0, 0);
				}
			}
			
			if (player.whoAmI == Main.myPlayer && shadeCloak)
			{				
				player.immune = true;
				player.immuneTime = 35;
				if (player.longInvince)
				{
					player.immuneTime += 80;
				}
				for (int i = 0; i < player.hurtCooldowns.Length; i++)
				{
					player.hurtCooldowns[i] = player.immuneTime;
				}
				
				for (int j = 0; j < 100; j++)
				{
					int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 254, 0f, 0f, 0, Color.Black, 2f);
					Dust dust = Main.dust[num];
					dust.position.X = dust.position.X + (float)Main.rand.Next(-20, 21);
					Dust dust2 = Main.dust[num];
					dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-25, 26);
					Main.dust[num].velocity *= 0.4f;
					Main.dust[num].scale *= 0.6f + (float)Main.rand.Next(40) * 0.01f;
					Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(player.cWaist, player);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						Main.dust[num].noGravity = true;
					}
				}
				if (player.whoAmI == Main.myPlayer)
				{
					NetMessage.SendData(62, -1, -1, null, player.whoAmI, 1f, 0f, 0f, 0, 0, 0);
				}
			}
		}
		
		/*public override void PostSetupContent()
        {
            try
            {
                CalamityCompatibility = new CalamityCompatibility(this).TryLoad() as CalamityCompatibility;
			}
			catch (Exception e)
            {
                ErrorLogger.Log("HollowVessel PostSetupContent Error: " + e.StackTrace + e.Message);
           }
		}*/

		//private readonly Mod calamity = ModLoader.GetMod("CalamityMod");


		int resetDashSlashTimer;
		public override void PostUpdateMiscEffects() 
		{
			
			if (!player.mount.Active)
			{
				if(player.HeldItem.type == mod.ItemType("DullNail"))
				{
					cloakDash = 1;
					mothwingCloak = true;
				}
				if(player.HeldItem.type == mod.ItemType("SharpenedNail"))
				{
					cloakDash = 1;
					mothwingCloak = true;
				}
				if(player.HeldItem.type == mod.ItemType("ChannelledNail"))
				{
					cloakDash = 1; //cloak
					mothwingCloak = true;
					mantisClaw = true;
				}
				if(player.HeldItem.type == mod.ItemType("CoiledNail")) // From this nail forward, add Dash Slash
				{
					cloakDash = 1; //cloak
					mothwingCloak = true;
					mantisClaw = true;
					dashSlash = true;
				}
				if(player.HeldItem.type == mod.ItemType("PellucidNail"))
				{
					cloakDash = 1; //cloak
					mothwingCloak = true;
					monarchWings = true;
					mantisClaw = true;
					dashSlash = true;
					
				}
				if(player.HeldItem.type == mod.ItemType("PureNail"))
				{
					//cloakDash = 2; //Crystal Heart
					//shadeDash = 1; //Crystal Heart
					shadeCloak = true;
					//crystalHeart = true;
					//monarchWings = true;
					mantisClaw = true;
					dashSlash = true;
					ismasTear = true;
					
					//if Calamity is active, do stuff inside this, modPlayer takes calamityPlayer stuff and makes it usable here, some don't work cuz of weak references maybe? Or maybe im looking at a really old calamityplayer code for these.
					/*if (HollowVessel.Instance.CalamityLoaded)
					{
						CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			
						if (Collision.DrownCollision(base.player.position, base.player.width, base.player.height, base.player.gravDir) && base.player.wet && !base.player.lavaWet && !base.player.honeyWet && !base.player.mount.Active)
						{
							if (modPlayer.aquaticBoost > 0f)
							{
								modPlayer.aquaticBoost -= 0.0002f;
								if ((double)modPlayer.aquaticBoost <= 0.0)
								{
									modPlayer.aquaticBoost = 0f;
									if (Main.netMode == 1)
									{
										NetMessage.SendData(84, -1, -1, null, base.player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
									}
								}
							}
						}
						else
						{
							modPlayer.aquaticBoost += 0.0002f;
							if (modPlayer.aquaticBoost > 1f)
							{
								modPlayer.aquaticBoost = 1f;
							}
							if (base.player.mount.Active)
							{
								modPlayer.aquaticBoost = 1f;
							}
						}
						base.player.moveSpeed -= (1f - modPlayer.aquaticBoost) * 0.1f;
						
						modPlayer.absorber = true;
						modPlayer.bloodPact = true;
						modPlayer.sDefense = true;
						modPlayer.draedonsHeart = true;
						player.buffImmune[calamity.BuffType("Irradiated")] = true;
						
					}*/
				}
				
			}
			if(dashSlashTimer >= 1)
			{
				resetDashSlashTimer++;
				if(resetDashSlashTimer >= 30)
				{
					dashSlashTimer = 0;
					resetDashSlashTimer = 0;
				}
				
			}
			
			
			
			if(player.ownedProjectileCounts[mod.ProjectileType("DesolateDive")] >= 1)
			{
				player.velocity.Y += 8.5f;
				player.maxFallSpeed += 7f;
				player.maxFallSpeed++;
				player.extraFall += 20;
				player.noKnockback = true;
				player.noFallDmg = true;
			}
			if(monarchWings)
			{
				if(player.controlJump)
				{
					
					if(player.velocity.Y >= 1 && monarchCount >= 1)
					{
						monarchCount = 0;
						
						int height = player.height;
						float gravDir2 = player.gravDir;
						Main.PlaySound(16, (int)player.position.X, (int)player.position.Y);
						player.velocity.Y = (2f - jumpSpeed) * player.gravDir;
						player.jump = jumpHeight / 2 * 3;
						for (int l = 0; l < 24; l++)
						{
							Vector2 circularMotion = new Vector2(player.width, player.height / 4) * 1.15f;
							circularMotion /= 1.1f;
							Vector2 realCircularMotion = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * circularMotion;
						
							Vector2 circularMotion2 = new Vector2(player.width, player.height / 5) * 1.15f;
							circularMotion2 /= 2f;
							Vector2 realCircularMotion2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * circularMotion2;
							
							for (int i = 0; i < 12; i++) 
							{
								int numDust = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y + 26), 0, 0, 264);
								Main.dust[numDust].position = realCircularMotion + new Vector2(player.Center.X, player.Center.Y + 26);
								Main.dust[numDust].position += player.velocity;
								Main.dust[numDust].velocity.X = player.velocity.X / 1.8f;
								Main.dust[numDust].velocity.Y = player.velocity.Y / 1.8f;
								Main.dust[numDust].scale = 1.25f;
								Main.dust[numDust].noGravity = true;
								
								int numDust2 = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y + 22), 0, 0, 264);
								Main.dust[numDust2].position = realCircularMotion2 + new Vector2(player.Center.X, player.Center.Y + 22);
								Main.dust[numDust2].position += player.velocity;
								Main.dust[numDust2].velocity.X = player.velocity.X / 3;
								Main.dust[numDust2].velocity.Y = player.velocity.Y / 3;
								Main.dust[numDust2].noGravity = true;
							}
						}	
					}
					player.releaseJump = true;
				}
				if (player.velocity.Y == 0 && player.oldVelocity.Y == 0 && !player.mount.Active)
				{
					monarchCount = 1;
					monarchWingsAgain = true;	
				}
			}
		
			if(mantisClaw)
			{
				player.spikedBoots = 0;
				bool flag = false;
				float num = player.position.X;
				if (player.slideDir == 1)
				{
					num += (float)player.width;
				}
				num += (float)player.slideDir;
				float num2 = player.position.Y + (float)player.height + 1f;
				if (player.gravDir < 0f)
				{
					num2 = player.position.Y - 1f;
				}
				num /= 16f;
				num2 /= 16f;
				if (WorldGen.SolidTile((int)num, (int)num2) && WorldGen.SolidTile((int)num, (int)num2 - 1))
				{
					flag = true;
				}
				
				
				if (!flag || ((!(player.velocity.Y > 0f) || player.gravDir != 1f) && (!(player.velocity.Y < player.gravity) || player.gravDir != -1f)))
				{
					return;
				}
				float num3 = player.gravity;
				if (player.slowFall)
				{
					num3 = ((!player.controlUp) ? (player.gravity / 3f * player.gravDir) : (player.gravity / 10f * player.gravDir));
				}
				player.fallStart = (int)(player.position.Y / 16f);
				if ((player.gravDir == 1f) || (player.controlUp && player.gravDir == -1f))
				{
					player.velocity.Y = 3f * player.gravDir;
					monarchCount = 0;
					if(player.controlJump)
					{
						monarchCount = 1;
						for (int l = 0; l < 24; l++)
						{
							Vector2 circularMotion = new Vector2(player.width, player.height / 4) * 1.15f;
							circularMotion /= 1.1f;
							Vector2 realCircularMotion = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * circularMotion;
						
							Vector2 circularMotion2 = new Vector2(player.width, player.height / 5) * 1.15f;
							circularMotion2 /= 2f;
							Vector2 realCircularMotion2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * circularMotion2;
						
							for (int i = 0; i < 24; i++) 
							{
								int numDust = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y + 26), 0, 0, 263);
								Main.dust[numDust].position = realCircularMotion + new Vector2(player.Center.X, player.Center.Y + 26);
								Main.dust[numDust].position += player.velocity;
								Main.dust[numDust].velocity.X = player.velocity.X / 1.8f;
								Main.dust[numDust].velocity.Y = player.velocity.Y / 1.8f;
								Main.dust[numDust].scale = 1.25f;
								Main.dust[numDust].noGravity = true;
							
								int numDust2 = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y + 22), 0, 0, 264);
								Main.dust[numDust2].position = realCircularMotion2 + new Vector2(player.Center.X, player.Center.Y + 22);
								Main.dust[numDust2].position += player.velocity;
								Main.dust[numDust2].velocity.X = player.velocity.X / 3;
								Main.dust[numDust2].velocity.Y = player.velocity.Y / 3;
								Main.dust[numDust2].noGravity = true;
							}
						}	
					}
					int num4 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)((player.width / 2 - 4) *player.slideDir), player.position.Y + (float)(player.height / 2) + (float)(player.height / 2 - 4) * player.gravDir), 16, 16, 262);
					if (player.slideDir < 0)
					{
						Dust dust = Main.dust[num4];
						dust.position.X = dust.position.X - 10f;
						dust.position.X = player.velocity.X;
					}
					if (player.gravDir < 0f)
					{
						Dust dust2 = Main.dust[num4];
						dust2.position.Y = dust2.position.Y - 12f;
						dust2.position.X = player.velocity.X;
					}
					Main.dust[num4].velocity *= 0.2f;
					Main.dust[num4].scale *= 1.2f;
					Main.dust[num4].noGravity = true;
				}
				else if (player.gravDir == -1f)
				{
					player.velocity.Y = (0f - num3 + 1E-05f) * player.gravDir;
					
				}
				else
				{
					player.velocity.Y = (0f - num3 + 1E-05f) * player.gravDir;
					
				}
				player.sliding = true;
				
			
				if ((flag && (double)player.velocity.Y > 0.5 && player.gravDir == 1f) || ((double)player.velocity.Y < -0.5 && player.gravDir == -1f))
				{
					player.fallStart = (int)(player.position.Y / 16f);
					player.velocity.Y = 3f * player.gravDir;					
					player.sliding = true;
					int num5 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)((player.width / 2 - 4) * player.slideDir), player.position.Y + (float)(player.height / 2) + (float)(player.height / 2 - 4) * player.gravDir), 14, 14, 264);
					
					if (player.slideDir < 0)
					{
						Dust dust3 = Main.dust[num5];
						dust3.position.X = dust3.position.X - 10f;
						
					}
					if (player.gravDir < 0f)
					{
						Dust dust4 = Main.dust[num5];
						dust4.position.Y = dust4.position.Y - 12f;
					}
					Main.dust[num5].velocity *= 0.2f;
					Main.dust[num5].scale *= 1.2f;
					Main.dust[num5].noGravity = true;
				}
			}
			if(ismasTear)
			{
				player.buffImmune[20] = true;
				player.accMerman = true;
				player.hideMerman = true;
				player.lavaMax += 600;	
				
			}
		}
	}
}
