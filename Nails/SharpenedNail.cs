using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class SharpenedNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sharpened Nail");
			Tooltip.SetDefault("Spells: [c/d9d9ff:Vengeful Spirit], [c/d9d8ee:Desolate Dive] \nAbilities: [c/86a0d4:Mothwing Cloak] \nNail Arts: [c/1474A7:Great Slash] \n'Something doesn't feel right...'");
		}
		public override void SetDefaults()
		{
			item.damage = 22;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 3.2f;
			item.value = 10000; // 1 gold
			item.rare = 4;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("SharpenedNail");
			item.shootSpeed = 9f;
		}
		
		public override bool CanUseItem(Player player) 
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("SharpenedNailSlash")] + player.ownedProjectileCounts[mod.ProjectileType("SharpenedNail2")] < 1;
		}
		
		
		public bool whichShot;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			whichShot = !whichShot;
			if(whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("SharpenedNail2")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("SharpenedNail"), damage, knockBack, player.whoAmI, 0f, 0f);
				}
			
			}
			if(!whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("SharpenedNail")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("SharpenedNail2"), damage / 3 * 4, knockBack, player.whoAmI);	
				}
			}
			
			return false;
		}
	}
}
