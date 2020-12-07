using System;
using HollowVessel.UI;
using HollowVessel.Projectiles.Orbs;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel
{
	public class HollowGlobalProjectile : GlobalProjectile
	{	
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			if (projectile.owner == Main.myPlayer)
			{
				//Normal Nails
				if(projectile.type == mod.ProjectileType("DamagedNail") || projectile.type == mod.ProjectileType("OldNail") || projectile.type == mod.ProjectileType("OldNail2") || projectile.type == mod.ProjectileType("DullNail") || projectile.type == mod.ProjectileType("DullNail") || projectile.type == mod.ProjectileType("SharpenedNail") || projectile.type == mod.ProjectileType("SharpenedNail2") || projectile.type == mod.ProjectileType("ChannelledNail") || projectile.type == mod.ProjectileType("ChannelledNail2") || projectile.type == mod.ProjectileType("CoiledNail") || projectile.type == mod.ProjectileType("CoiledNail2") || projectile.type == mod.ProjectileType("PellucidNail") || projectile.type == mod.ProjectileType("PellucidNail2") || projectile.type == mod.ProjectileType("PureNail") || projectile.type == mod.ProjectileType("PureNail2") || projectile.type == mod.ProjectileType("AeleNail") || projectile.type == mod.ProjectileType("AeleNail2"))
				{
					var modPlayer = Main.LocalPlayer.GetModPlayer<SoulMeterPlayer>();
					modPlayer.soulMeterCurrent += 3;
					Player player = Main.player[projectile.owner];
					HollowPlayer mPlayer = player.GetModPlayer<HollowPlayer>();
					if(mPlayer.soulOrbActive == true && player.ownedProjectileCounts[mod.ProjectileType("SoulMeterOrb1")] == 0)
					{
						Projectile.NewProjectile(player.Center.X + 40, player.Center.Y - 35, 0f, 0f, ModContent.ProjectileType<SoulMeterOrb1>(), 0, 0, Main.myPlayer, 0f, 0f);
					}
					if(mPlayer.soulOrbActive2 == true && player.ownedProjectileCounts[mod.ProjectileType("SoulMeterOrb2")] == 0)
					{
						Projectile.NewProjectile(player.Center.X + 50, player.Center.Y - 55, 0f, 0f, ModContent.ProjectileType<SoulMeterOrb2>(), 0, 0, Main.myPlayer, 0f, 0f);
					}
					if(mPlayer.soulOrbActive3 == true && player.ownedProjectileCounts[mod.ProjectileType("SoulMeterOrb3")] == 0)
					{
						Projectile.NewProjectile(player.Center.X + 50, player.Center.Y - 55, 0f, 0f, ModContent.ProjectileType<SoulMeterOrb3>(), 0, 0, Main.myPlayer, 0f, 0f);
					}
				}
				
				//Nail Arts
				if(projectile.type == mod.ProjectileType("OldNailSlash") || projectile.type == mod.ProjectileType("DullNailSlash") || projectile.type == mod.ProjectileType("SharpenedNailSlash") || projectile.type == mod.ProjectileType("ChannelledNailSlash") || projectile.type == mod.ProjectileType("CoiledNailSlash") || projectile.type == mod.ProjectileType("PellucidNailSlash") || projectile.type == mod.ProjectileType("PellucidDashSlash") || projectile.type == mod.ProjectileType("PureNailSlash") || projectile.type == mod.ProjectileType("PureDashSlash") || projectile.type == mod.ProjectileType("AeleDashSlash") || projectile.type == mod.ProjectileType("AeleNailSlash") || projectile.type == mod.ProjectileType("AeleNailCyclone"))
				{
					var modPlayer = Main.LocalPlayer.GetModPlayer<SoulMeterPlayer>();
					modPlayer.soulMeterCurrent += 11;
					Player player = Main.player[projectile.owner];
					HollowPlayer mPlayer = player.GetModPlayer<HollowPlayer>();
					if(mPlayer.soulOrbActive == true && player.ownedProjectileCounts[mod.ProjectileType("SoulMeterOrb1")] == 0)
					{
						Projectile.NewProjectile(player.Center.X + 40, player.Center.Y - 35, 0f, 0f, ModContent.ProjectileType<SoulMeterOrb1>(), 0, 0, Main.myPlayer, 0f, 0f);
					}
					if(mPlayer.soulOrbActive2 == true && player.ownedProjectileCounts[mod.ProjectileType("SoulMeterOrb2")] == 0)
					{
						Projectile.NewProjectile(player.Center.X + 50, player.Center.Y - 55, 0f, 0f, ModContent.ProjectileType<SoulMeterOrb2>(), 0, 0, Main.myPlayer, 0f, 0f);
					}
					if(mPlayer.soulOrbActive3 == true && player.ownedProjectileCounts[mod.ProjectileType("SoulMeterOrb3")] == 0)
					{
						Projectile.NewProjectile(player.Center.X + 50, player.Center.Y - 55, 0f, 0f, ModContent.ProjectileType<SoulMeterOrb3>(), 0, 0, Main.myPlayer, 0f, 0f);
					}
				}
			}
		}
		
		
	}
}
