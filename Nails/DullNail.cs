using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class DullNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dull Nail");
			Tooltip.SetDefault("Spells: [c/d9d9ff:Vengeful Spirit] \nAbilities: [c/86a0d4:Mothwing Cloak] \nNail Arts: [c/1474A7:Great Slash] \n'You wonder what the Nail has instilled inside you.'");
		}
		public override void SetDefaults()
		{
			item.damage = 18;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 3.2f;
			item.value = 6000;
			item.rare = 3;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("DullNail");
			item.shootSpeed = 8f;
		}
		
		public override bool CanUseItem(Player player) 
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("DullNailSlash")] + player.ownedProjectileCounts[mod.ProjectileType("DullNail2")] < 1;
		}
		
		
		public bool whichShot;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			whichShot = !whichShot;
			if(whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("DullNail2")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("DullNail"), damage, knockBack, player.whoAmI, 0f, 0f);
				}
			
			}
			if(!whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("DullNail")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("DullNail2"), damage / 3 * 4, knockBack, player.whoAmI);	
				}
			}
			
			return false;
		}
	}
}
