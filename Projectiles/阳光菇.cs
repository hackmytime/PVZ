using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 阳光菇 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("阳光菇");
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
            projectile.GetGlobalProjectile<PVZProj>().Life = 350;
            projectile.GetGlobalProjectile<PVZProj>().Plant = true;
            projectile.usesLocalNPCImmunity = true;
            Main.projFrames[projectile.type] = 11;
            if (Main.dayTime)
            {
                projectile.GetGlobalProjectile<PVZProj>().Sleep = true;
            }
        }
        int Growing;
        int UP;
        int A = 2;
        public override void AI()
        {
            projAI(projectile);

            projectile.frameCounter++;
            if (!projectile.GetGlobalProjectile<PVZProj>().Sleep)
            {
                UP++;
                if (UP == 7200)
                {
                    projectile.frameCounter = 0;
                    projectile.scale = 0.5F;
                    Growing = 1;
                    A = 20;
                }
                if (projectile.ai[1] >= 1400)
                {
                    projectile.ai[1] = 0;
                    projectile.ai[0] = 0.01f;
                }
                if (projectile.ai[0] > 0)
                {
                    projectile.ai[0] += 0.006F;
                }
                else
                {
                    projectile.ai[1]++;
                }
                if (projectile.ai[0] >= 1)
                {
                    projectile.ai[0] = 0;

                    if (Growing == 0)
                    {
                        Item.NewItem(projectile.Center, ModContent.ItemType<小阳光>());
                    }
                    else
                    {
                        Item.NewItem(projectile.Center, ModContent.ItemType<阳光>());
                    }
                }

                if (Growing == 0)
                {
                    if (projectile.ai[0] > 0)
                    {
                        if (projectile.ai[0] <= 0.33f)
                        {
                            projectile.frame = 2;
                        }
                        else if (projectile.ai[0] <= 0.66f)
                        {
                            projectile.frame = 3;
                        }
                        else
                        {
                            projectile.frame = 4;
                        }
                    }
                    else
                    {
                        if (projectile.frameCounter >= 60)
                        {
                            projectile.frameCounter = 0;
                            projectile.frame++;
                        }
                        if (projectile.frame > 1)
                        {
                            projectile.frame = 0;
                        }
                    }
                }
                else if (Growing == 1)
                {
                    projectile.frame = 5;
                    if (projectile.frameCounter < 50)
                    {
                        if (projectile.frameCounter % 8 == 0)
                        {
                            if (A > 2)
                            {
                                A -= 4;
                            }
                            else
                            {
                                A = 2;
                            }
                        }
                        projectile.scale += 0.01F;
                    }
                    else
                    {
                        projectile.scale = 1F;
                        projectile.frameCounter = 0;
                        Growing = 2;
                    }

                }
                else if (Growing == 2)
                {
                    if (projectile.ai[0] > 0)
                    {
                        if (projectile.ai[0] <= 0.9f)
                        {
                            if (projectile.frameCounter >= 10)
                            {
                                projectile.frameCounter = 0;
                                projectile.frame++;
                            }
                            if (projectile.frame > 10)
                            {
                                projectile.frame = 7;
                            }
                        }
                        else
                        {
                            projectile.frame = 10;
                        }
                    }
                    else
                    {
                        if (projectile.frameCounter >= 30)
                        {
                            projectile.frameCounter = 0;
                            projectile.frame++;
                        }
                        if (projectile.frame > 6)
                        {
                            projectile.frame = 5;
                        }
                    }
                }
            }
            else
            {
                projectile.frame = 2;
                if (projectile.frameCounter >= 240)
                {
                    projectile.frameCounter = 0;
                }
                if (projectile.frameCounter == 25 || projectile.frameCounter == 50 || projectile.frameCounter == 75)
                {
                    Gore.NewGore(projectile.Center, new Vector2(-2, -2), mod.GetGoreSlot("Gores/Z"), 1f);
                }
            }
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("Projectiles/阳光菇光效");
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/ Main.projFrames[projectile.type])), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2) - A), projectile.scale, 0, 0f);
            if (projectile.ai[0]>0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255, 247,161, 0), Color.LightBlue)*projectile.ai[0];
                spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/ Main.projFrames[projectile.type])), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2) - A), projectile.scale, 0, 0f);
                spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/Main.projFrames[projectile.type])), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2) - A), projectile.scale, 0, 0f);
                spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/ Main.projFrames[projectile.type])), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2) - A), projectile.scale, 0, 0f);
            }
            if (projectile.localAI[1] != 0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255, 255, 255, 0), Color.LightBlue) * 30f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/ Main.projFrames[projectile.type])), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2) - A), projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}