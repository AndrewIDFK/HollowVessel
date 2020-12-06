using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class BrokenNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Nail");
			Tooltip.SetDefault("'You don't know where you got this, but you can feel its importance.'");
		}
		public override void SetDefaults()
		{
			item.damage = 6;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.autoReuse = false;
			item.knockBack = 1.5f;
			item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("BrokenNail");
			item.shootSpeed = 4f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] < 1;
		}
	}
}
