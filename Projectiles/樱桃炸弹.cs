using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 樱桃炸弹 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("樱桃炸弹");
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<PVZProj>().Life = 100000;
            projectile.GetGlobalProjectile<PVZProj>().Plant = true;
            projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            if (projectile.ai[1] >= 1200 + Main.rand.Next(5000))
            {
                projectile.ai[1] = 0;
                projectile.ai[0] = 0.01f;
            }
            if(projectile.ai[0]>0)
            {
                projectile.ai[0]+=0.006F;
            }
            else
            {
                projectile.ai[1]++;
            }
            if(projectile.ai[0]>=1)
            {
                projectile.ai[0] = 0;
                Item.NewItem(projectile.Center, ModContent.ItemType<阳光>());
            }
            projAI(projectile);
            projectile.frameCounter++;

            if (projectile.frameCounter >= 30)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            if (projectile.frame == 2 && projectile.frameCounter == 29)
            {
                for (int A = 0; A < 200; A++)
                {
                    float X = Main.npc[A].Center.X - projectile.Center.X;
                    float Y = Main.npc[A].Center.Y - projectile.Center.Y;
                    if (X < 0) X = -X;
                    if (Y < 0) Y = -Y;
                    if (X < 14 * 16 && Y < 14 * 16 && Main.npc[A].active)
                    {
                    }
                }
            }
            if (projectile.frame == 3)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 38, 20, -0.8f);
            Projectile.NewProjectile(projectile.Center + new Vector2(0,0), Vector2.Zero, ModContent.ProjectileType<爆炸>(), 0, 0, projectile.owner, 0, 1);
            for (int A = 0; A < 200; A++)
            {
                float X = Main.npc[A].Center.X - projectile.Center.X;
                float Y = Main.npc[A].Center.Y - projectile.Center.Y;
                if (X < 0) X = -X;
                if (Y < 0) Y = -Y;
                if (X <14 * 16&&Y<14*16&& Main.npc[A].active)
                {
                    if (Main.npc[A].life > projectile.damage)
                    {
                        Main.npc[A].life -= projectile.damage;
                        CombatText.NewText(new Rectangle((int)Main.npc[A].position.X, (int)Main.npc[A].position.Y, Main.npc[A].width, Main.npc[A].height), Color.Red, projectile.damage, true, false);
                    }
                    else
                    {
                        Main.npc[A].life -= projectile.damage;
                        CombatText.NewText(new Rectangle((int)Main.npc[A].position.X, (int)Main.npc[A].position.Y, Main.npc[A].width, Main.npc[A].height), Color.Red, projectile.damage, true, false);
                        Main.npc[A].GetGlobalNPC<PVZNPC>().Cherry = true;
                        Main.npc[A].checkDead();
                        Main.npc[A].HitEffect(0, projectile.damage);
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("Projectiles/向日葵光效");
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / 3* projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/3)), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 6)), projectile.scale, 0, 0f);
            return false;
        }
    }
}