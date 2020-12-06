using HollowVessel.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace HollowVessel
{
	public class HollowVessel : Mod
	{

		public static ModHotKey VengefulSpirit;
		
		internal static HollowVessel Instance;

		public override void Load() 
		{
			VengefulSpirit = RegisterHotKey("Shoot a Vengeful Spirit", "Z");
		
			Instance = this;
		}
		
		public override void PostSetupContent()
        {
}
