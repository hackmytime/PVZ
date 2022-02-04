using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 坚果墙 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("坚果墙");
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
            projectile.GetGlobalProjectile<PVZProj>().Life = 9000;
            projectile.GetGlobalProjectile<PVZProj>().Plant = true;
            projectile.usesLocalNPCImmunity = true;
        }
        int a;
        public override void AI()
        {
            projAI(projectile);
            a++;
            if (a % 5 == 0)
            {
                if (projectile.ai[0] == 0)
                {
                    projectile.ai[1] += 0.01f;
                }
                if (projectile.ai[0] == 1)
                {
                    projectile.ai[1] -= 0.01f;
                }
            }
            if (projectile.ai[1] > 0.15f)
            {
                projectile.ai[0] = 1;
            }
            if (projectile.ai[1] < -0.15f)
            {
                projectile.ai[0] = 0;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter >= 120 && projectile.frame==0)
            {
                projectile.frameCounter = 0;
                projectile.frame=1;
            }
            if (projectile.frameCounter >= 10 && projectile.frame==1)
            {
                projectile.frameCounter = 0;
                projectile.frame=0;
            }
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int A = Main.projectileTexture[projectile.type].Height / 6*projectile.frame;
            if (projectile.GetGlobalProjectile<PVZProj>().Life >= 6000)
            {
                A += Main.projectileTexture[projectile.type].Height / 6 * 0;
            }
            else if (projectile.GetGlobalProjectile<PVZProj>().Life >= 3000)
            {
                A += Main.projectileTexture[projectile.type].Height / 6 * 2;
            }
            else
            {
                A += Main.projectileTexture[projectile.type].Height / 6 * 4;
            }
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0,A, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 6)), drawColor, projectile.ai[1], new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 12) - 4), projectile.scale, 0, 0f);
            if (projectile.localAI[1] != 0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255, 255, 255, 0), Color.LightBlue) * 30f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, A, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 6)), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 12) - 4), projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}