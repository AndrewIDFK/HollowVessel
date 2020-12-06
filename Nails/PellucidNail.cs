using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class PellucidNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Honed Nail");
			Tooltip.SetDefault("Spells: [c/a7e8e2:Vengeful Spirit], [c/86dbd3:Desolate Dive] \nAbilities: [c/badcf5:Mothwing Cloak], [c/9ac5e6:Mantis Claw], [c/78add6:Monarch Wings]  \nNail Arts: [c/3a8bc7:Great Slash], [c/a61750:Dash Slash] \n'You are starting to feel different.'");
		}
		public override void SetDefaults()
		{
			item.damage = 48;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 3.2f;
			item.value = 150000; // 15 gold
			item.rare = 7;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("PellucidNail");
			item.shootSpeed = 12f;
		}
		
		public override bool CanUseItem(Player player) 
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("PellucidNailSlash")] + player.ownedProjectileCounts[mod.ProjectileType("PellucidNail2")] < 1;
		}
		
		
		public bool whichShot;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			whichShot = !whichShot;
			if(whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("PellucidNail2")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("PellucidNail"), damage, knockBack, player.whoAmI, 0f, 0f);
				}
			
			}
			if(!whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("PellucidNail")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("PellucidNail2"), damage / 3 * 4, knockBack, player.whoAmI, 0f, 0f);	
				}
			}
			
			return false;
		}
	}
}
