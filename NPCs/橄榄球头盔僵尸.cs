using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using PVZ.Projectiles;

namespace PVZ.NPCs
{
	public class 橄榄球头盔僵尸 : 僵尸
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("橄榄球头盔僵尸");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.damage = 1000;
			npc.width = 34;
			npc.height = 50;
			npc.aiStyle = 3;
			npc.defense = 0;
			npc.lifeMax = 825;
			npc.knockBackResist = 0f;
			npc.alpha = 0;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath1;
			aiType = 3;
			animationType = 3;
			npc.GetGlobalNPC<PVZNPC>().Zombie = true;
			PVZ = 10;
			VelocityX = 1.4f;
			VelocityY = 6;
			Lifes = false;
			Damage = 50;
		}
		public override void AI2()
		{
			if (Life == 0)
			{
				Life = npc.life;
			}
		}
		int Life;
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/坏橄榄球头盔"), 1f);

				if ((int)(damage - (Life + 270)) < 0)
				{
					float ai = npc.localAI[3];
					int L = (int)((Life + 270) - damage);
					npc.Transform(ModContent.NPCType<普通僵尸>());
					npc.life = L;
					npc.localAI[3] = ai;
					if (Main.netMode == NetmodeID.Server)
					{
						npc.localAI[3] = ai;
						npc.life = 270;
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				else
				{
					if (npc.GetGlobalNPC<PVZNPC>().Devour)
					{
					}
					else
					if (npc.GetGlobalNPC<PVZNPC>().Cherry)
					{
						int P = Projectile.NewProjectile(npc.Center + new Vector2(0, 8), Vector2.Zero, ModContent.ProjectileType<僵尸灰烬>(), 0, 0, 0, 0, 1);
						Main.projectile[P].spriteDirection = -npc.spriteDirection;
					}
					else
					{
						Gore.NewGore(npc.position, npc.velocity, 3, 1f);
						Gore.NewGore(npc.position, npc.velocity, 4, 1f);
						Gore.NewGore(npc.position, npc.velocity, 5, 1f);
						Gore.NewGore(npc.position, npc.velocity, 4, 1f);
						Gore.NewGore(npc.position, npc.velocity, 5, 1f);
					}
				}
			}
			if (npc.life > 0)
			{
				Life = npc.life;
			}
		}
		public override void PDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = mod.GetTexture("NPCs/橄榄球头盔");
			if (npc.life > npc.lifeMax * 0.5f)
			{
				spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height / 2)), drawColor, npc.rotation, new Vector2((float)(Main.npcTexture[npc.type].Width / 2 - 8), (float)(Main.npcTexture[npc.type].Height / 4) - 16), npc.scale, 0, 0f);
			}
			else
			{
				spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, texture.Height / 2, texture.Width, texture.Height / 2)), drawColor, npc.rotation, new Vector2((float)(Main.npcTexture[npc.type].Width / 2 - 8), (float)(Main.npcTexture[npc.type].Height / 4) - 16), npc.scale, 0, 0f);
			}
		}
	}
}
