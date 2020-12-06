using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class PureNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pure Nail");
			Tooltip.SetDefault("Spells: [c/a7e8e2:Vengeful Spirit], [c/86dbd3:Desolate Dive] \nAbilities: [c/badcf5:Mothwing Cloak], [c/9ac5e6:Mantis Claw], [c/78add6:Monarch Wings], [c/63a3d4:Isma's Tear]  \nNail Arts: [c/3a8bc7:Great Slash], [c/a61750:Dash Slash] \n'A nail that's been forged to perfection'");
		}
		public override void SetDefaults()
		{
			item.damage = 62;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 11;
			item.useAnimation = 11;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 4.6f;
			item.value = 300000; // 15 gold
			item.rare = 8;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("PureNail");
			item.shootSpeed = 13f;
		}
		
		public override bool CanUseItem(Player player) 
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("PureNailSlash")] + player.ownedProjectileCounts[mod.ProjectileType("PureNail2")] < 1;
		}
		
		
		public bool whichShot;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			whichShot = !whichShot;
			if(whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("PureNail2")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("PureNail"), damage, knockBack, player.whoAmI, 0f, 0f);
				}
			
			}
			if(!whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("PureNail")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("PureNail2"), damage / 3 * 4, knockBack, player.whoAmI, 0f, 0f);	
				}
			}
			
			return false;
		}
	}
}
