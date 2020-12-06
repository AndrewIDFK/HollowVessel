using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles
{
    public class DesolateDivePound : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desolate Dive");
		}
		
        public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 340;
			projectile.height = 28;			
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.soundDelay = 3;
			projectile.hide = true;
			projectile.timeLeft = 2;
			Main.PlaySound(SoundID.Item69, projectile.position);
		}
		
		
		public override void AI()
		{
			for (int i = 0; i < 24; i++) 
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].scale *= 1.15f;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity.Y -= 4f;
			}
			for (int i = 0; i < 12; i++) 
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 264, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].scale *= 1.2f;
				Main.dust[dustIndex].noGravity = true;
			}		
		}	
    }
}