using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace HollowVessel.UI
{
	public class SoulMeterPlayer : ModPlayer
	{
		public static SoulMeterPlayer ModPlayer(Player player) 
		{
			return player.GetModPlayer<SoulMeterPlayer>();
		}

		
		public int soulMeterCurrent;
		public const int DefaultSoulMeterMax = 165;
		public int soulMeterMax;
		public int soulMeterMax2;
		public float soulMeterRegenRate;
		internal int soulMeterRegenTimer = 0;
		public static readonly Color HealSoulMeter = new Color(255, 255, 255);

		public override void Initialize() 
		{
			soulMeterMax = DefaultSoulMeterMax;
		}

		public override void ResetEffects() 
		{
			ResetVariables();
		}

		public override void UpdateDead() 
		{
			ResetVariables();
		}

		private void ResetVariables() 
		{
			soulMeterMax2 = soulMeterMax;
		}

		public override void PostUpdateMiscEffects() 
		{
			UpdateResource();
		}

		private void UpdateResource() 
		{
			soulMeterCurrent = Utils.Clamp(soulMeterCurrent, 0, soulMeterMax2);
			
			HollowPlayer mPlayer = player.GetModPlayer<HollowPlayer>();
			if(soulMeterCurrent >= 33)
			{
				mPlayer.soulOrbActive = true;
			}
			if(soulMeterCurrent < 33)
			{
				mPlayer.soulOrbActive = false;
			}
			if(soulMeterCurrent >= 66)
			{
				mPlayer.soulOrbActive2 = true;
			}
			if(soulMeterCurrent < 66)
			{
				mPlayer.soulOrbActive2 = false;
			}
			if(soulMeterCurrent >= 99)
			{
				mPlayer.soulOrbActive3 = true;
			}
			if(soulMeterCurrent < 99)
			{
				mPlayer.soulOrbActive3 = false;
			}
		}
	}
}