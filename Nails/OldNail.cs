using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Nails
{
	public class OldNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Old Nail");
			Tooltip.SetDefault("Spells: [c/d9d9ff:Vengeful Spirit] \nNail Arts: [c/1474A7:Great Slash] \n'Something feels... familiar about this nail.'");
		}
		public override void SetDefaults()
		{
			item.damage = 12;
			item.melee = true;
			item.width = 48;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 2f;
			item.value = 2500;
			item.rare = 2;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("OldNail");
			item.shootSpeed = 7f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("OldNailSlash")] + player.ownedProjectileCounts[mod.ProjectileType("OldNail2")] < 1;
		}
	}
}
