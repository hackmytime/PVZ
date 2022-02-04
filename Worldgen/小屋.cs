using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace PVZ.Worldgen
{
	public class 小屋 : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 0, 255)] = 30;
			colorToTile[new Color(255, 255, 255)] = -2;
			colorToTile[new Color(0, 255, 255)] = 189;
			colorToTile[new Color(0, 255, 0)] = 196;
			colorToTile[new Color(255, 0, 0)] = 38;
			colorToTile[new Color(100, 100, 100)] = -2;
			colorToTile[Color.Black] = -1;

			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(255, 255, 255)] = 4;
			colorToWall[Color.Black] = -1;

			TexGen gen = BaseWorldGenTex.GetTexGenerator(ModContent.GetTexture("PVZ/Worldgen/小屋"), colorToTile, ModContent.GetTexture("PVZ/Worldgen/小屋2"), colorToWall, null);
			int genX = origin.X;
			int genY = origin.Y;
			gen.Generate(genX, genY, true, true);
			WorldGen.PlaceObject(genX + 6, genY + 7, 19, false, 0);
			WorldGen.PlaceObject(genX + 7, genY + 7, 19, false, 0);
			WorldGen.PlaceObject(genX + 8, genY + 7, 19, false, 0);
			WorldGen.PlaceObject(genX + 7, genY + 8, 4, false, 0);
			WorldGen.PlaceObject(genX +11, genY + 11, 11, false, 0);
			return true;
		}
	}
}