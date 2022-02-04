using Microsoft.Xna.Framework;
using PVZ.Dusts;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles.僵尸子弹
{
    public class 子弹 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("子弹");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            for (int a = 0; a < 1000; a++)
            {
                if (Main.projectile[a].active&&Main.projectile[a].GetGlobalProjectile<PVZProj>().Plant)
                {
                    Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value2 = new Rectangle((int)Main.projectile[a].position.X, (int)Main.projectile[a].position.Y, Main.projectile[a].width, Main.projectile[a].height);
                    if (rectangle.Intersects(value2))
                    {
                        Main.projectile[a].GetGlobalProjectile<PVZProj>().Life -= 50;
                        Main.projectile[a].localAI[1] = 5;
                        projectile.Kill();
                    }
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //damage = 0;
            //target.localAI[3] = 60;
        }
        public override void Kill(int timeLeft)
        {
            // Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<豌豆粒子1>(), -projectile.velocity.X, -projectile.velocity.Y,0,default,1f);
            // Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<豌豆粒子2>(), -projectile.velocity.X, -projectile.velocity.Y, 0, default, 1f);

        }
    }
}