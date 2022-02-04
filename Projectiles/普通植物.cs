using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public abstract class 普通植物 : ModProjectile
    {
        int a;
        public override void AI()
        {
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = 0;
        }
        public override void Kill(int timeLeft)
        {
        }
        public static void projAI(Projectile projectile)
        {
            int Tile = 2;
            if (PVZWorld.Surroundings == 1)
            {
                Tile = 70;
            }
            projectile.localNPCHitCooldown = 100;
            projectile.timeLeft = 10;
            if (projectile.damage == 0)
            {
                projectile.damage = 1;
            }
            Vector2 position;
            position.X = (int)(projectile.Center.X / 32) * 32;
            position.Y = (int)(projectile.Center.Y / 16) * 16;
            if (Main.tile[(int)(position.X / 16), (int)(position.Y / 16) + 1].type != Tile || projectile.GetGlobalProjectile<PVZProj>().Life <= 0 || !PVZWorld.PlantsVsZombies)
            {
                projectile.Kill();
            }
            if (projectile.localAI[1] != 0)
            {
                projectile.localAI[1]--;
            }
        }
        static float ANG(float a, float b)
        {
            return 2 ;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public static bool 敌(Projectile projectile, bool 左右)
        {
            for (int A = 0; A < 200; A++)
            {
                if (Main.npc[A].active && Main.npc[A].GetGlobalNPC<PVZNPC>().Zombie)
                {
                    float no = (projectile.rotation - (Main.npc[A].position+new Vector2(Main.npc[A].width/2, Main.npc[A].height-projectile.height/2) - projectile.Center).ToRotation()) % MathHelper.TwoPi;
                    float E = Math.Min(MathHelper.TwoPi - no, no);
                    float PC = Main.npc[A].Center.X-Main.player[projectile.owner].Center.X;
                    if(E<0)
                    {
                        E = -E;
                    }
                    if (E <0.01F && PC<1600)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}