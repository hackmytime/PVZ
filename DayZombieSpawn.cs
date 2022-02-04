using Microsoft.Xna.Framework;
using PVZ.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace PVZ
{
	internal class DayZombieSpawn
	{
		private static Dictionary<string, int[]> Spawn;

		public static void NPCs()
		{
			Vector2 vector = Main.rand.Next(PVZWorld.Road);
			SNPC();
			int pvz = PVZWorld.PlantsVsZombieswave;
			switch (pvz)
			{
				case 1: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs1)); break;
				case 2: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs2)); break;
				case 3: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs3)); break;
				case 4: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs4)); break;
				case 5: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs5)); break;
				case 6: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs6)); break;
				case 7: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs7)); break;
				case 8: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs8)); break;
				case 9: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs9)); break;
				default: NPC.NewNPC((int)vector.X, (int)vector.Y, Main.rand.Next(NPCs9)); break;
			}
		}
		public static void SNPC()
		{
		}
		public static int[] NPCs1 = new int[]
		{
			ModContent.NPCType<普通僵尸>()
		};
		public static int[] NPCs2 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>()
		};
		public static int[] NPCs3 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<路障僵尸>()
		};
		public static int[] NPCs4 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<铁桶僵尸>()
		};
		public static int[] NPCs5 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<巨型普通僵尸>(),ModContent.NPCType<铁桶僵尸>()
		};
		public static int[] NPCs6 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<巨型普通僵尸>(),ModContent.NPCType<巨型普通僵尸>(),ModContent.NPCType<铁桶僵尸>()
		};
		public static int[] NPCs7 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<巨型普通僵尸>(),ModContent.NPCType<铁桶僵尸>(),ModContent.NPCType<橄榄球头盔僵尸>()
		};
		public static int[] NPCs8 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<铁桶僵尸>(),ModContent.NPCType<橄榄球头盔僵尸>(),ModContent.NPCType<橄榄球头盔僵尸>()
		};
		public static int[] NPCs9 = new int[]
		{
			ModContent.NPCType<普通僵尸>(),ModContent.NPCType<路障僵尸>(),ModContent.NPCType<持枪僵尸>(),ModContent.NPCType<巨型路障僵尸>(),ModContent.NPCType<橄榄球头盔僵尸>()
		};
	}
}