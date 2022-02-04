using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using PVZ.Projectiles;
using PVZ.Projectiles.僵尸子弹;

namespace PVZ.NPCs
{
	public class 持枪僵尸 : 僵尸
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("持枪僵尸");
			Main.npcFrameCount[npc.type] =3;
		}

		public override void SetDefaults()
		{
			npc.damage = 1000;
			npc.width = 34;
			npc.height = 50;
			npc.aiStyle = 3;
			npc.defense = 0;
			npc.lifeMax = 135;
			npc.knockBackResist = 0f;
			npc.alpha = 0;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			aiType = 3;
			animationType = 3;
			npc.GetGlobalNPC<PVZNPC>().Zombie = true;
			PVZ = 10;
			VelocityX = 1;
			VelocityY = 6;
			Lifes = false;
			Damage = 50;
		}
		public override void PDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = mod.GetTexture("NPCs/手枪");
			spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), drawColor, npc.rotation, new Vector2((float)(Main.npcTexture[npc.type].Width/2 +26), (float)(Main.npcTexture[npc.type].Height / 6)-16), 0.8f, 0, 0f);

		}
		int R;
		public override void AI2()
        {
			if (npc.life <= (npc.lifeMax - 70) / 2 + 70)
			{
				Gore.NewGore(npc.position, npc.velocity, 4, 1f);
				Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪"), 0.8f);
				Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪2"), 0.8f);
				float ai = npc.localAI[3];
				npc.Transform(ModContent.NPCType<断手僵尸>());
				npc.localAI[3] = ai;
				if (Main.netMode == NetmodeID.Server)
				{
					npc.localAI[3] = ai;
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			R++;
			//Main.NewText(R);
			if (R > 60)
			{
				R = 0;

				Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 11, 20, 0);
				Projectile.NewProjectile(npc.Center, new Vector2(-5, 0), ModContent.ProjectileType<子弹>(), 1, 0, 0);
			}
			if (npc.life <= 200)
			{
				npc.GetGlobalNPC<PVZNPC>().Piranha = 1;
			}
		}

        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				if (npc.GetGlobalNPC<PVZNPC>().Devour)
				{
					Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪"), 0.8f);
					Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪2"), 0.8f);
				}
				else
				if (npc.GetGlobalNPC<PVZNPC>().Cherry)
				{
					int P = Projectile.NewProjectile(npc.Center + new Vector2(0, 8), Vector2.Zero, ModContent.ProjectileType<僵尸灰烬>(), 0, 0, 0, 0, 1);
					Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪"), 0.8f);
					Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪2"), 0.8f);
					Main.projectile[P].spriteDirection = -npc.spriteDirection;
				}
				else
				{
					Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪"), 0.8f);
					Gore.NewGore(npc.position, Vector2.Zero, mod.GetGoreSlot("Gores/手枪2"), 0.8f);
					Gore.NewGore(npc.position, npc.velocity, 3, 1f);
					Gore.NewGore(npc.position, npc.velocity, 4, 1f);
					Gore.NewGore(npc.position, npc.velocity, 5, 1f);
					Gore.NewGore(npc.position, npc.velocity, 4, 1f);
					Gore.NewGore(npc.position, npc.velocity, 5, 1f);
				}
			}
			npc.GetGlobalNPC<PVZNPC>().Cherry = false;
		}
    }
}
