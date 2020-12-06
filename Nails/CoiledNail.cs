using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class CoiledNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coiled Nail");
			Tooltip.SetDefault("Spells: [c/e3ffff:Vengeful Spirit], [c/d4ffff:Desolate Dive] \nAbilities: [c/def1ff:Mothwing Cloak], [c/cde7fa: Mantis Claw]  \nNail Arts: [c/2186cf:Great Slash], [c/870f31:Dash Slash] \n'You are starting to feel different.'");
		}
		public override void SetDefaults()
		{
			item.damage = 36;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 13;
			item.useAnimation = 13;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 3.2f;
			item.value = 75000; // 7.5 gold
			item.rare = 6;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("CoiledNail");
			item.shootSpeed = 11f;
		}
		
		public override bool CanUseItem(Player player) 
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("CoiledNailSlash")] + player.ownedProjectileCounts[mod.ProjectileType("CoiledNail2")] < 1;
		}
		
		
		public bool whichShot;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			whichShot = !whichShot;
			if(whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("CoiledNail2")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("CoiledNail"), damage, knockBack, player.whoAmI, 0f, 0f);
				}
			
			}
			if(!whichShot)
			{
				if(player.ownedProjectileCounts[mod.ProjectileType("CoiledNail")] <= 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("CoiledNail2"), damage / 3 * 4, knockBack, player.whoAmI, 0f, 0f);	
				}
			}
			
			return false;
		}
	}
}
