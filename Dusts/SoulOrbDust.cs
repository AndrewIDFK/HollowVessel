using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace HollowVessel.Dusts
{
	// This Dust will show off Dust.customData, using vanilla dust texture, and some neat movement.
	internal class SoulOrbDust : ModDust
	{
		/*
			Spawning this dust is a little more involved because we need to assign a rotation, customData, and fix the position. 
			Position must be fixed here because otherwise the first time the dust is drawn it'll draw in the incorrect place.
			This dust is not used in ExampleMod yet, so you'll have to add some code somewhere. Try ExamplePlayer.DrawEffects.
			Dust dust = Dust.NewDustDirect(player.Center, 0, 0, DustType<Dusts.AdvancedDust>(), Scale: 2);
			dust.rotation = Main.rand.NextFloat(6.28f);
			dust.customData = player;
			dust.position = player.Center + Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;
		*/
		public override void OnSpawn(Dust dust) 
		{
			dust.noGravity = true;

			dust.velocity = Vector2.Zero;
		}
		
		// This Update method shows off some interesting movement. Using customData assigned to a Player, we spiral around the Player while slowly getting closer. In practice, it looks like a vortex.
		public override bool Update(Dust dust) 
		{
			
			
			if (dust.alpha > 0)
			{
				dust.scale += 0.09f;
				if (dust.scale >= 1f)
				{
					dust.scale = 1f;
					dust.alpha = 255;
				}
			}
			
			
			Color color = Lighting.GetColor((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));
			byte num75 = (byte)((color.R + color.G + color.B) / 3);
			float num76 = ((float)(int)num75 / 270f + 1f) / 2f;
			float num77 = ((float)(int)num75 / 270f + 1f) / 2f;
			float num78 = ((float)(int)num75 / 270f + 1f) / 2f;
			float num79 = 1f;
			num76 *= dust.scale * 0.9f;
			num77 *= dust.scale * 0.9f;
			num78 *= dust.scale * 0.9f;
			
			num76 *= 0.8f;
			num77 *= 181f / 255f;
			num78 *= 24f / 85f;
			num79 = 1.1f;
			// Here we rotate and scale down the dust. The dustIndex % 2 == 0 part lets half the dust rotate clockwise and the other half counter clockwise
			dust.rotation += 0.1f * (dust.dustIndex % 2 == 0 ? -1 : 1);
			dust.scale -= 0.05f;

			// Here we use the customData field. If customData is the type we expect, Player, we do some special movement.
			if (dust.customData != null && dust.customData is Player player) 
			{
				// Here we assign position to some offset from the player that was assigned. This offset scales with dust.scale. The scale and rotation cause the spiral movement we desired.
				dust.position = player.Center + Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;
			}
			num76 *= num79;
			num77 *= num79;
			num78 *= num79;
			// Here we make sure to kill any dust that get really small.
			if (dust.scale < 0.1f) 
			{
				dust.active = false;
			}
			return false;
		}
	}
}