using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 寒冰射手 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("寒冰射手");
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
        }
        public override void AI()
        {
            projectile.frameCounter++;
            projAI(projectile);
            if (敌(projectile, false))
            {
                projectile.ai[1]++;
                if (projectile.ai[1] > 168)
                {
                    projectile.ai[1] = 0;
                    Projectile.NewProjectile(projectile.Center, new Vector2(5, 0), ModContent.ProjectileType<冰豌豆>(), projectile.damage, 0, projectile.owner);
                }
                if (projectile.ai[1] >= 138)
                {
                    if (projectile.ai[1] == 148)
                    {
                        projectile.frame = 4;
                    }
                }
                else
                {
                    if (projectile.frameCounter >= 40)
                    {
                        projectile.frameCounter = 0;
                        projectile.frame++;
                    }
                    if (projectile.frame > 3)
                    {
                        projectile.frame = 0;
                    }
                }
            }
            else
            {
                if (projectile.frameCounter >= 40)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int F = Main.projectileTexture[projectile.type].Height / 6 * projectile.frame;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, F, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/6)), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 12-4)), projectile.scale, 0, 0f);
            if (projectile.localAI[1] != 0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255,255, 255, 0), Color.LightBlue) * 30f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, F, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/6)), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 12 - 4)), projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}