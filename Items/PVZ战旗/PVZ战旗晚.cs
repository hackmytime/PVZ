using Microsoft.Xna.Framework;
using PVZ.Projectiles;
using PVZ.Worldgen;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Items.PVZ战旗
{
	public class PVZ战旗晚 : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 40;
			item.height = 28;
			item.maxStack = 20;
			item.value = 100;
			item.rare = 11;
			item.useAnimation = 5;
			item.useTime = 5;
			item.useStyle = 4;
			item.consumable = true;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/开始");

		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PVZ战旗(晚上)");
			Tooltip.SetDefault("使用后开启植物大战僵尸");
		}

		public override bool CanUseItem(Player player)
		{
			if (PVZWorld.PlantsVsZombies)
			return true;
			return true;
		}

		public override bool UseItem(Player player)
		{
			Main.NewText("游戏即将开始!", 39, 86, 134);
			int X = (int)(player.Center.X / 32)*2+5;
			int Y = (int)(player.Center.Y / 32)*2+5;
			int Tile1 = 59;
			int Tile2 = 70;
			int Tile3 = 38;
			for (int a = -200; a < 200; a++)
			{
				for (int b = -80; b < 80; b++)
				{
					Tile tile = Main.tile[X+a, Y-b];
					tile.type = 0;
					tile.sTileHeader = 0;
					tile.frameX = 0;
					tile.frameY = 0;
					tile.wall = 0;
					tile.bTileHeader = 0;
					tile.bTileHeader2 = 0;
					tile.bTileHeader3 = 0;
					tile.liquid = 0;
					tile.lava(false);
					tile.honey(false);
					if (Main.netMode == 2)
					{
						NetMessage.sendWater(X + a, Y - b);
					}
				}
			}
			Point origin = new Point(X - 25, Y - 13);
			new 小屋().Place(origin, WorldGen.structures);
			for (int a = 2; a < 100; a++)
			{
				for (int b = 0; b < 55; b += 11)
				{
					WorldGen.PlaceTile(X + a + 2, Y - 65, Tile1, true, false, -1, 0);
					WorldGen.PlaceTile(X + a + 2, Y - 65, Tile2, true, false, -1, 0);

					if (a < 76)
					{
						WorldGen.PlaceTile(X + a + 2, Y - b, Tile1, true, false, -1, 0);
						WorldGen.PlaceTile(X + a + 2, Y - b, Tile2, true, false, -1, 0);
						WorldGen.PlaceTile(X + a + 2, Y - b + 1, Tile1, true, false, -1, 0);
						WorldGen.PlaceTile(X + a + 2, Y - b + 1, Tile2, true, false, -1, 0);
						WorldGen.PlaceTile(X + a + 2, Y - b + 2, Tile1, true, false, -1, 0);
						WorldGen.PlaceTile(X + a + 2, Y - b + 2, Tile2, true, false, -1, 0);
					}
					else
					{
						WorldGen.PlaceTile(X + a + 2, Y - b, Tile3, true, false, -1, 0);
						WorldGen.PlaceTile(X + a + 2, Y - b + 1, Tile3, true, false, -1, 0);
						WorldGen.PlaceTile(X + a + 2, Y - b + 2, Tile3, true, false, -1, 0);
					}
					WorldGen.PlaceTile(X + 3, Y - b, Tile3, true, false, -1, 0);
					WorldGen.PlaceTile(X + 2, Y - b, Tile3, true, false, -1, 0);
					WorldGen.PlaceTile(X + 1, Y - b, Tile3, true, false, -1, 0);

					WorldGen.PlaceTile(X + 3, Y - b + 1, Tile3, true, false, -1, 0);
					WorldGen.PlaceTile(X + 2, Y - b + 1, Tile3, true, false, -1, 0);
					WorldGen.PlaceTile(X + 1, Y - b + 1, Tile3, true, false, -1, 0);

					WorldGen.PlaceTile(X + 3, Y - b + 2, Tile3, true, false, -1, 0);
					WorldGen.PlaceTile(X + 2, Y - b + 2, Tile3, true, false, -1, 0);
					WorldGen.PlaceTile(X + 1, Y - b + 2, Tile3, true, false, -1, 0);
				}
			}
			for (int a = 0; a < 100; a++)
			{
				for (int b = 0; b < 81; b++)
				{
					WorldGen.PlaceTile(X + a + 2, Y + b, Tile1, true, false, -1, 0);
				}
			}
			for (int a = 0; a < 5; a++)
			{
				WorldGen.PlaceTile(X+2, Y+a, Tile1, true, false, -1, 0);
				WorldGen.PlaceTile(X+2, Y+a, Tile2, true, false, -1, 0);
				Projectile.NewProjectile(new Vector2((X+2)*16,((Y-11*a)-1)*16), Vector2.Zero, ModContent.ProjectileType<割草机>(), 999999999, 0, player.whoAmI);
			}
			for (int a = 0; a < 100; a++)
			{
				for (int b = 0; b < 50; b ++)
				{
					WorldGen.PlaceTile(X + 2 - a, Y+b, Tile3, true, false, -1, 0);
				}
			}
			PVZWorld.PlantsVsZombiesCenter = player.Center + new Vector2(52 * 16, -23 * 16);
			for (int a = 0; a < 5; a++)
			{
				PVZWorld.Road[a] = player.Center + new Vector2(90 * 16, 14 - (a * 11) * 16);
			}
			PVZWorld.PlantsVsZombies = true;
			PVZWorld.Surroundings = 1;
			return true;
		}
	}
}
