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
	public class 勇士僵尸 : 僵尸
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("勇士僵尸");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.damage = 1000;
			npc.width = 34;
			npc.height = 50;
			npc.aiStyle = 3;
			npc.defense = 0;
			npc.lifeMax = 1200;
			npc.knockBackResist = 0f;
			npc.alpha = 0;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			aiType = 3;
			animationType = 3;
			npc.GetGlobalNPC<PVZNPC>().Zombie = true;
			PVZ = 10;
			VelocityX = 1.6f;
			VelocityY = 6;
			Lifes = false;
			Damage = 50;
		}
		public override void PDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2 = mod.GetTexture("NPCs/金头盔");
			spriteBatch.Draw(texture2, npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, texture2.Width, texture2.Height)), drawColor, npc.rotation, new Vector2((float)(Main.npcTexture[npc.type].Width / 2 - 8), (float)(Main.npcTexture[npc.type].Height / 2) - 52), npc.scale, 0, 0f);

		}
		float R;
		bool D = false;
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = mod.GetTexture("NPCs/金剑");
			spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), drawColor, npc.rotation + R, new Vector2((float)(Main.npcTexture[npc.type].Width / 2 + 18), (float)(Main.npcTexture[npc.type].Height / 6) + 0), 1f, 0, 0f);

			return true;
		}
		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			bool a = false;
			if (projectile.GetGlobalProjectile<PVZProj>().PlantAttack)
			{
				a = true;
			}
			Plant = a;
			if (projectile.GetGlobalProjectile<PVZProj>().Plant && npc.localAI[3] <= 2)
			{
				if (!D)
				{
					projectile.localAI[1] = 5;
				}
				npc.localAI[3] = 100;
			}
		}
		public override void AI2()
		{
			if (Life == 0)
			{
				Life = npc.life;
			}
			if (npc.localAI[3] != 0)
			{
				if (npc.GetGlobalNPC<PVZNPC>().冰冻)
				{
					R -= 0.15f;
				}
				else
				{
					R -= 0.3f;
				}
			}
			else
			{
				R = 1.5f;
			}
			bool a = false;
			for (int I = 0; I < 1000; I++)
			{
				if (Main.projectile[I].active && Main.projectile[I].GetGlobalProjectile<PVZProj>().Plant)
				{
					float no = (npc.rotation - ((npc.position + new Vector2(npc.width / 2, npc.height - Main.projectile[I].height / 2) - Main.projectile[I].Center)).ToRotation()) % MathHelper.TwoPi;
					float E = Math.Min(MathHelper.TwoPi - no, no);
					if (E < 0)
					{
						E = -E;
					}
					float po = npc.position.X - Main.projectile[I].position.X;
					if (E < 0.01F && po < 16 + npc.width / 2)
					{
						if (R < -1F)
						{
							Main.PlaySound(SoundID.Item, npc.Center, 1);
							Main.projectile[I].GetGlobalProjectile<PVZProj>().Life -= Damage;
							Main.projectile[I].localAI[1] = 5;
							R = 1.5f;
						}
					}
				}
			}
			if (npc.life > 1730)
			{
				npc.GetGlobalNPC<PVZNPC>().Piranha = 2;
			}
			else
			{
				npc.GetGlobalNPC<PVZNPC>().Piranha = 0;
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
				if ((int)(damage - (Life + 270)) < 0)
				{
					Main.NewText((int)(damage - (Life + 270)));
					float ai = npc.localAI[3];
					int L = (int)(Life + 270 - damage);
					npc.Transform(ModContent.NPCType<持剑僵尸>());
					npc.life = L;
					npc.localAI[3] = ai;
					if (Main.netMode == NetmodeID.Server)
					{
						npc.localAI[3] = ai;
						npc.life = L;
						NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				else
				{
					if (npc.GetGlobalNPC<PVZNPC>().Devour)
					{
						Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/金剑"), 1f);
						Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/金剑2"), 1f);
					}
					else
					if (npc.GetGlobalNPC<PVZNPC>().Cherry)
					{
						int P = Projectile.NewProjectile(npc.Center + new Vector2(0, 8), Vector2.Zero, ModContent.ProjectileType<僵尸灰烬>(), 0, 0, 0, 0, 1);
						Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/金剑"), 1f);
						Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/金剑2"), 1f);
						Main.projectile[P].spriteDirection = -npc.spriteDirection;
					}
					else
					{
						Gore.NewGore(npc.position, npc.velocity, 3, 1f);
						Gore.NewGore(npc.position, npc.velocity, 4, 1f);
						Gore.NewGore(npc.position, npc.velocity, 5, 1f);
						Gore.NewGore(npc.position, npc.velocity, 4, 1f);
						Gore.NewGore(npc.position, npc.velocity, 5, 1f);
						Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/金剑"), 1f);
						Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/金剑2"), 1f);
					}
				}
			}
			else
			{
				npc.GetGlobalNPC<PVZNPC>().Cherry = false;
				Life = npc.life;
			}
		}
	}
}
