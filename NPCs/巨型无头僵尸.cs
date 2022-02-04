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
	public class 巨型无头僵尸 : 僵尸
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("巨型僵尸");
			Main.npcFrameCount[npc.type] =3;
		}

		public override void SetDefaults()
		{
			npc.damage = 0;
			npc.width = 34 * 2;
			npc.height = 96;
			npc.aiStyle = 3;
			npc.defense = 0;
			npc.lifeMax = 2700;
			npc.knockBackResist = 0f;
			npc.alpha = 0;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			aiType = 3;
			animationType = 3;
			npc.GetGlobalNPC<PVZNPC>().Zombie = true;
			PVZ = 10;
			VelocityX = 0.3f;
			VelocityY = 6;
			Lifes = true;
		}
		public override void AI2()
		{
			if (npc.life > 2000)
			{
				npc.GetGlobalNPC<PVZNPC>().Piranha = 2;
			}
			else if (npc.life > 200)
			{
				npc.GetGlobalNPC<PVZNPC>().Piranha = 0;
			}
			else
			{
				npc.GetGlobalNPC<PVZNPC>().Piranha = 1;
			}
		}
		public override void PDraw(SpriteBatch spriteBatch, Color drawColor)
        {

        }
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 2f);
			}
			if (npc.life <= 0)
			{
				if (npc.GetGlobalNPC<PVZNPC>().Cherry)
				{
					int P = Projectile.NewProjectile(npc.Center + new Vector2(0, 8), Vector2.Zero, ModContent.ProjectileType<僵尸灰烬2>(), 0, 0, 0, 0, 1);
					Main.projectile[P].scale *= 2;
					Main.projectile[P].spriteDirection = -npc.spriteDirection;
				}
				else
				if (!npc.GetGlobalNPC<PVZNPC>().Devour)
				{
					Gore.NewGore(npc.position, npc.velocity, 5, 2f);
					Gore.NewGore(npc.position, npc.velocity, 4, 2f);
					Gore.NewGore(npc.position, npc.velocity, 5, 2f);
				}
			}
		}
        public override bool CheckDead()
        {
			PVZWorld.PlantsVsZombiesPoints1+=10;

			return base.CheckDead();
        }
    }
}
