using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using PVZ.Buffs;
using Terraria.DataStructures;

namespace PVZ.NPCs
{
	public abstract class 僵尸 : ModNPC
	{
		public abstract void AI2();
		public abstract void PDraw(SpriteBatch sprite,Color color);
		bool M;
		int 吃;
		public override void AI()
		{
			AI2();
			if(npc.velocity.Y>1)
            {
				npc.velocity.X = -1;
			}
			if(!M)
            {
				for (int A = 0; A < npc.buffImmune.Length; A++)
				{
					npc.buffImmune[A] = true;
					M = true;
				}
				npc.buffImmune[ModContent.BuffType<寒霜侵袭>()] = false;
            }
			npc.TargetClosestUpgraded();
			npc.velocity.Y = VelocityY*3;
			if (npc.Center.X > Main.player[npc.target].Center.X)
			{
				float A = -VelocityX;
				if (npc.GetGlobalNPC<PVZNPC>().冰冻)
				{
					A /= 2;
				}
				npc.velocity.X = A;
			}
			npc.timeLeft = 10;
			if (!PVZWorld.PlantsVsZombies)
			{
				npc.active = false;
			}
			if (Lifes)
			{
				if (npc.localAI[3] != 0)
				{
					npc.velocity.X = 0;
				}
				npc.localAI[2]++;
				if (npc.localAI[2] % 4 == 0)
				{
					if (npc.life > npc.lifeMax/100)
					{
						npc.life-= npc.lifeMax / 100;
					}
					else
					{
						npc.life-= npc.lifeMax / 100;
						npc.checkDead();
						npc.HitEffect(0, npc.lifeMax / 100);
					}
				}
			}
			else
            {
				if (npc.localAI[3] != 0)
				{
					if (npc.GetGlobalNPC<PVZNPC>().冰冻)
					{
						吃 -= 1;
					}
					else
					{
						吃 -= 2;
					}
					npc.velocity = Vector2.Zero;
					bool a = false;
					for (int I = 0; I < 1000; I++)
					{
						if (Main.projectile[I].active && Main.projectile[I].GetGlobalProjectile<PVZProj>().Plant)
						{
							float no = (npc.rotation - ((npc.position+ new Vector2(npc.width / 2, npc.height - 14))- (Main.projectile[I].position + new Vector2(Main.projectile[I].width / 2, Main.projectile[I].height - 14))).ToRotation()) % MathHelper.TwoPi;
							float E = Math.Min(MathHelper.TwoPi - no, no);
							if (E < 0)
							{
								E = -E;
							}
							float po = npc.position.X - Main.projectile[I].position.X;
							if (E < 0.01F&& po<16+npc.width/2)
							{
								npc.velocity = Vector2.Zero;
								a = true;
							}
						}
					}
					if (!a)
					{
						npc.localAI[3] = 0;
						吃 = 0;
					}
				}
				if (Main.rand.Next(10000) == 0)
				{
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPCs/groan" + Main.rand.Next(1, 7)), (int)npc.position.X, (int)npc.position.Y);
				}
			}
		}
		public float VelocityX;
		public float VelocityY;
		public bool Lifes;
		public bool Plant;
		public int Damage;

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			bool a = false;
			if (projectile.GetGlobalProjectile<PVZProj>().PlantAttack)
			{
				a = true;
			}
			if (!Lifes)
			{
				if (projectile.GetGlobalProjectile<PVZProj>().Plant && 吃 <= 0)
				{
					if (projectile.GetGlobalProjectile<PVZProj>().Life > Damage)
					{
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPCs/chomp" + Main.rand.Next(1, 4)), (int)npc.position.X, (int)npc.position.Y);
						projectile.GetGlobalProjectile<PVZProj>().Life -= Damage;
						projectile.localAI[1] = 5;
						npc.localAI[3] = 100;
						吃 = 100;
					}
					else
					{
						projectile.GetGlobalProjectile<PVZProj>().Life -= Damage;
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPCs/gulp"), (int)npc.position.X, (int)npc.position.Y);
					}
				}
			}
			Plant = a;
		}
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
			if(!Plant)
            {
				damage = 0;
				return false;
            }
			return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity,5, 1f);
				Gore.NewGore(npc.position, npc.velocity,4, 1f);
				Gore.NewGore(npc.position, npc.velocity,5, 1f);
			}
		}
		public int PVZ;
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			Main.LocalPlayer.KillMe(PlayerDeathReason.ByCustomReason(Main.LocalPlayer.name + "的脑子被僵尸吃掉了."), 100000, 0);
			damage = 1;

		}
        public override bool CheckDead()
        {
			PVZWorld.PlantsVsZombiesPoints1+=PVZ;

			return base.CheckDead();
        }
		public override void DrawEffects(ref Color drawColor)
		{
			if (npc.GetGlobalNPC<PVZNPC>().冰冻)
			{
				drawColor = new Color(29, 186, 255);
			}
		}
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
			PDraw(spriteBatch, drawColor);
		}
    }
}
