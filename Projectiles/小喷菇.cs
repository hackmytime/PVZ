using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 小喷菇 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("小喷菇");
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<PVZProj>().Life = 350;
            projectile.GetGlobalProjectile<PVZProj>().Plant = true;
            projectile.usesLocalNPCImmunity = true;
            if(Main.dayTime)
            {
                projectile.GetGlobalProjectile<PVZProj>().Sleep = true;
            }
        }
        int v = 0;
        public override void AI()
        {
            projAI(projectile);
            projectile.frameCounter++;
            if (v == 0)
            {
                projectile.position.Y += 4;
                v = 1;
            }
            if (!projectile.GetGlobalProjectile<PVZProj>().Sleep)
            {
                if (敌(projectile, false))
                {
                    int a = PVZPlayer.FindClosest(projectile.Center, 1, 1);
                    float t = Main.npc[a].Center.X - projectile.Center.X;
                    if (t < 320)
                    {
                        projectile.ai[1]++;
                        if (projectile.ai[1] > 84)
                        {
                            projectile.ai[1] = 0;
                            Projectile.NewProjectile(projectile.Center+new Vector2(0,4), new Vector2(5, 0), ModContent.ProjectileType<孢子>(), projectile.damage, 0, projectile.owner);
                        }
                    }
                }
                if (projectile.frameCounter >= 40)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame > 1)
                {
                    projectile.frame = 0;
                }
            }
            else
            {
                projectile.frame = 2;
                if (projectile.frameCounter >=240)
                {
                    projectile.frameCounter = 0;
                }
                if (projectile.frameCounter == 25||projectile.frameCounter ==50||projectile.frameCounter == 75)
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
            int F = Main.projectileTexture[projectile.type].Height / 3 * projectile.frame;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, F, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/3)), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 6-2)), projectile.scale, 0, 0f);
            if (projectile.localAI[1] != 0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255,255, 255, 0), Color.LightBlue) * 30f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, F, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/3)), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 6-2)), projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}