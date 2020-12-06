using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class DamagedNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Damaged Nail");
			Tooltip.SetDefault("Spells: [c/d9d9ff:Vengeful Spirit] \n'Somehow, you instinctively know how to use this nail.'");
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 2f;
			item.value = 1000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("DamagedNail");
			item.shootSpeed = 6f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] < 1;
		}
	}
}
