using Microsoft.Xna.Framework;
using PVZ.Items;
using PVZ.NPCs;
using PVZ.Projectiles;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace PVZ
{
	public class PVZWorld : ModWorld
	{
		//0是白天,1是晚上,2是池塘,3是迷雾,4是屋顶
		public static int Surroundings = 0;
		public static int PlantsVsZombiesPoints = 0;
		public static int PlantsVsZombiesPoints1;
		public static int PlantsVsZombiesPointsMax = 100;
		public static int time = 1000;
		public static bool PlantsVsZombies;
		public static bool ChoosePlants;
		public static int PlantsVsZombieswave = 1;
		public static Vector2 PlantsVsZombiesCenter;
		public static Vector2[] Road = new Vector2[5];
		public static int Time = 0;
		public int SunTime;
		public bool wave;
		public int waveTime;

		public override void Initialize()
		{
			PlantsVsZombiesPoints1 = 0;
			PlantsVsZombies = false;
		}
		/*public override TagCompound Save()
		{
			var tc = new TagCompound
			{
				{"PlantsVsZombies",PlantsVsZombies},
				{"PlantsVsZombiesPoints1", PlantsVsZombiesPoints1}
			};
			return tc;
		}

        public override void Load(TagCompound tag)
		{
			PlantsVsZombies = tag.GetBool("PlantsVsZombies");
			PlantsVsZombiesPoints1 = tag.GetAsInt("PlantsVsZombiesPoints1");
		}*/
		public override void PreUpdate()
		{
			Vector2 vector = Main.rand.Next(Road);
			bool a = true;
			for(int A = 0;A<255;A++)
            {
				if(Main.player[A].dead)
                {
					a = false;
				}
            }
			if(!a&& PlantsVsZombies)
			{
				PlantsVsZombies = false;
            }
			if (PlantsVsZombiesPoints1 >= PlantsVsZombiesPointsMax)
			{
				PlantsVsZombiesPoints1 = PlantsVsZombiesPointsMax;
				wave = true;
			}
			if(wave)
            {
				int N = 0;
				waveTime++;
				if (waveTime==1)
				{
					NPC.NewNPC((int)vector.X, (int)vector.Y, ModContent.NPCType<旗帜僵尸>());
					Projectile.NewProjectile(PlantsVsZombiesCenter, Vector2.Zero, ModContent.ProjectileType<一大波>(),0,0);
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/开始游戏"), (int)Main.player[0].position.X, (int)Main.player[0].position.Y);
				}
				if (waveTime > 300)
				{
					for (int A = 0; A < 200; A++)
					{
						if (Main.npc[A].active && Main.npc[A].GetGlobalNPC<PVZNPC>().Zombie)
						{
							N++;
						}
					}
					if(N==0)
                    {
						PlantsVsZombiesPoints1 = 0;
						PlantsVsZombiesPointsMax = 100* (PlantsVsZombieswave+1);
						PlantsVsZombieswave++;
						time = 1000;
						waveTime = 0;
						wave = false;
					}
				}
            }
			if (PlantsVsZombies && ChoosePlants)
			{
				if (Surroundings == 0 || Surroundings == 2 || Surroundings == 4)
				{
					SunTime++;
					if (SunTime >= 1200 + Main.rand.Next(4000))
					{
						int A = Item.NewItem(PlantsVsZombiesCenter - new Vector2(Main.rand.Next(-500, 500), 1000), ModContent.ItemType<阳光>());
						Main.item[A].noMelee = true;
						SunTime = 0;
					}
				}
				if (time == 1)
				{
					NPC.NewNPC((int)vector.X, (int)vector.Y, ModContent.NPCType<普通僵尸>());
				}
				if (time != 0)
				{
					time--;
				}
				else
				{
					Time++;
					int t = 1000 / PlantsVsZombieswave;
					if (waveTime < 300)
					{
						if (wave)
						{
							t /= 30;
							if (t < 2)
							{
								t = 2;
							}
						}
						else
						{
							if (t < 12)
							{
								t = 12;
							}
						}
						if (Time >= t)
						{
							if(Surroundings==0) DayZombieSpawn.NPCs();
							if(Surroundings==1) NightZombieSpawn.NPCs();
							Time = 0;
						}
					}
				}
				for (int i = 0; i < 100; i++)
				{
					if (Main.combatText[i].active && Main.combatText[i].text == "SPUDOW!!")
					{
						Main.combatText[i].scale = 1.5f;
					}
					if (Main.combatText[i].active && Main.combatText[i].text == "0")
					{
						Main.combatText[i].active = false;

					}
				}
				for (int A = -200; A < 200; A++)
				{
					for (int b = -200; b < 200; b++)
					{
						if (Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 0 && Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 2
							&& Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 38 && Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 70
							&& Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 10 && Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 4
							&& Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 11&& Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 59
							&& Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 30 && Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type != 19)
						{
							Tile tile = Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)];
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
								NetMessage.sendWater((int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b));
							}
						}
						if (Main.tile[(int)(PlantsVsZombiesCenter.X / 16 + A), (int)(PlantsVsZombiesCenter.Y / 16 - b)].type == 70)
						{
							Lighting.AddLight(new Vector2((int)(PlantsVsZombiesCenter.X + (A*16)), (int)(PlantsVsZombiesCenter.Y - (b*16))), 1.58f * 0.5f, 2.42f * 0.5f, 2.55f * 0.8f);
						}
					}
				}
			}
			if(!PlantsVsZombies)
			{
				ChoosePlants = false;
				time = 1000;
				Time = 0;
				PlantsVsZombiesPoints1 = 0;
				PlantsVsZombieswave = 1;
				PlantsVsZombiesPointsMax = 100;
				waveTime = 0;
				wave = false;
			}
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(PlantsVsZombies);
			writer.Write(PlantsVsZombiesPoints1);
		}

		public override void NetReceive(BinaryReader reader)
		{
			PlantsVsZombies = reader.ReadBoolean();
			PlantsVsZombiesPoints1 = reader.ReadInt32();
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();
				PlantsVsZombies = flags[0];
				PlantsVsZombiesPoints1 = reader.ReadInt32();
			}
			else
			{
				ErrorLogger.Log("Tremor: Unknown loadVersion: " + loadVersion);
			}
		}
	}
}