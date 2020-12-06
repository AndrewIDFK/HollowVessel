using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles
{
    public class VengefulSpirit : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vengeful Spirit");
		}
		
        public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 50;
			projectile.height = 48;
			projectile.aiStyle = 14;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.soundDelay = 3;
			projectile.tileCollide = false;
			Main.PlaySound(SoundID.Item69, projectile.position);
		}
		
		int timer;
		public override void AI()
		{
			int num = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 264, projectile.velocity.X * -0.5f, projectile.velocity.Y * -0.5f, 0, default(Color), 1f);
			Main.dust[num].velocity /= 180f;
			Main.dust[num].scale = 1.2f;
			Main.dust[num].noGravity = true;
			//projectile.rotation = projectile.velocity;
			timer++;
			if(timer < 1)
			{
				projectile.velocity *= 8;
			}
		}	
    }
}