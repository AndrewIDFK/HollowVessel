using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HollowVessel.Projectiles.Nails.AeleNail
{
    public class AeleNailCyclone : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aele Nail Cyclone");
            Main.projFrames[projectile.type] = 14;
		}
        public override void SetDefaults()
        {
            projectile.width = 420;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 6;
            projectile.extraUpdates = 1;
        }
		int Timer;
        public override bool PreAI()
        {
			Timer++;
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.ai[0]++;
			bool channeling = Timer < 70 * 2 && (player.controlUseItem || Timer < 65) && !player.noItems && !player.CCed;
			projectile.frame = (projectile.frame + 1) % 14;
		   
            /*if (projectile.ai[0] < 5)
			{
				projectile.frame = 0;
			}
            else if (projectile.ai[0] < 10)
			{
				projectile.frame = 1;
			}
            else if (projectile.ai[0] < 15)
			{
				projectile.frame = 2;
			}
            else if (projectile.ai[0] < 20)
			{
				projectile.frame = 3;
			}
            else if (projectile.ai[0] < 25)
			{
				projectile.frame = 4;
			}
            else if (projectile.ai[0] < 30)
			{
				projectile.frame = 5;
			}
            else if (projectile.ai[0] < 35)
			{
				projectile.frame = 6;
			}
            else if (projectile.ai[0] < 40)
			{
				projectile.frame = 7;
			} 
			else if (projectile.ai[0] < 45)
			{
				projectile.frame = 8;
			}
			else if (projectile.ai[0] < 50)
			{
				projectile.frame = 9;
			} 
			else if (projectile.ai[0] < 55)
			{
				projectile.frame = 10;
			}
			else if (projectile.ai[0] < 60)
			{
				projectile.frame = 11;
			}
			else if (projectile.ai[0] < 65)
			{
				projectile.frame = 12;
			}
			else if (projectile.ai[0] < 70)
			{
				projectile.frame = 13;
			}
            else
			{
				projectile.frame = 14;
			}*/
			
            if(Timer % 5 == 0)
            {
                Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 183);
            }

            player.ChangeDir(projectile.direction);
            projectile.position = vector - projectile.Size / 2f;
            projectile.rotation = 0;
            projectile.spriteDirection = projectile.direction;
            if (Timer % 25 >= 2 && Timer % 25 <= 8)
				player.heldProj = projectile.whoAmI;
            player.itemTime = 16;
            player.itemAnimation = 16;
            player.itemRotation = MathHelper.WrapAngle(projectile.rotation);
			
			if (!channeling)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}