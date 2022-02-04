using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 土豆地雷 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("土豆地雷");
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height =14;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<PVZProj>().Life = 350;
            projectile.GetGlobalProjectile<PVZProj>().Plant = true;
            projectile.usesLocalNPCImmunity = true;
            Main.projFrames[projectile.type] = 5;
        }
        int v = 0;
        public override void AI()
        {
            projAI(projectile);
            projectile.ai[0]++;
            if (v == 0)
            {
                v = 1;
                projectile.position.Y += 10;
            }
            if (projectile.ai[0]>900)
            {
                projectile.frameCounter++;
                if(projectile.frameCounter>=10&& projectile.frame<3)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                float G = 120;
                int result = 0;
                for (int i = 0; i < 200; i++)
                {
                    float x = Main.npc[i].Center.X - projectile.Center.X;
                    float y = Main.npc[i].Center.Y + Main.npc[i].height / 4 - projectile.Center.Y;
                    if (y < 0)
                    {
                        y *= -1;
                    }
                    if (x>0&& y < 32 && Main.npc[i].active && !Main.npc[i].dontTakeDamage)
                    {
                        result = i;
                        break;
                    }
                }
                float num = -1f;
                for (int j = 0; j < 200; j++)
                {
                    float x = Main.npc[j].Center.X - projectile.Center.X;
                    float y = Main.npc[j].Center.Y + Main.npc[j].height / 4 - projectile.Center.Y;
                    if (y < 0)
                    {
                        y *= -1;
                    }
                    if (x > 0 && y < 32 && Main.npc[j].active && !Main.npc[j].dontTakeDamage)
                    {
                        float num2 = Math.Abs(Main.npc[j].position.X + Main.npc[j].width / 2 - (projectile.Center.X)) + Math.Abs(Main.npc[j].position.Y + Main.npc[j].height / 2 - (projectile.Center.Y));
                        if (num == -1f || num2 < num)
                        {
                            num = num2;
                            result = (byte)j;
                        }
                    }
                }

                G = (Main.npc[result].Center - projectile.Center).Length() / 16;
                if (G > 120)
                {
                    G = 120;
                }
                if (projectile.frameCounter >= G)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if(projectile.frame>4)
                {
                    projectile.frame = 3;
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[0] > 900)
            {
                Projectile.NewProjectile(projectile.Center + new Vector2(0, -50), Vector2.Zero, ModContent.ProjectileType<爆炸土豆>(), 0, 0, projectile.owner, 0, 1);
                Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 38, 20, -0.8f);
                for (int A = 0; A < 200; A++)
                {
                    float R = (Main.npc[A].Center - projectile.Center).Length();
                    if(R<4*16)
                    {
                        if (Main.npc[A].life > projectile.damage)
                        {
                            Main.npc[A].life-= projectile.damage;
                            CombatText.NewText(new Rectangle((int)Main.npc[A].position.X, (int)Main.npc[A].position.Y, Main.npc[A].width, Main.npc[A].height), Color.Red, projectile.damage, true, false);
                        }
                        else
                        {
                            Main.npc[A].life-= projectile.damage;
                            CombatText.NewText(new Rectangle((int)Main.npc[A].position.X, (int)Main.npc[A].position.Y, Main.npc[A].width, Main.npc[A].height), Color.Red, projectile.damage, true, false);
                            Main.npc[A].checkDead();
                            Main.npc[A].HitEffect(0, projectile.damage);
                        }
                    }
                }
                damage = 0;
                if (projectile.ai[1] < 1)
                {
                    projectile.ai[1] = 1;
                    int A = Gore.NewGore(projectile.position + new Vector2(0, -30), Vector2.Zero, mod.GetGoreSlot("Gores/土豆泥"), 1f);
                    Main.gore[A].velocity = Vector2.Zero;
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 projDirection = Utils.RotatedBy(new Vector2(0, Main.rand.NextFloat(-3, -1)), Main.rand.NextFloat(-1.57f, 1.57f), default(Vector2));
                        Gore.NewGore(projectile.position, projDirection, mod.GetGoreSlot("Gores/薯条" + Main.rand.Next(1, 4)), 1f);
                    }
                }
                projectile.Kill();
            }
            else
            {
                projectile.localAI[1] = 5;
                damage = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int W = Main.projectileTexture[projectile.type].Height / 5 * projectile.frame;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, W, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type])), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 10+2)), projectile.scale, 0, 0f);
            if (projectile.localAI[1] != 0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255,255, 255, 0), Color.LightBlue) * 30f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, W, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/ Main.projFrames[projectile.type])), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 10+2)), projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}