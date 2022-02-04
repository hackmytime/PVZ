using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 向日葵 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("向日葵");
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

            if (projectile.ai[0] > 0)
            {
                if(projectile.ai[0]<=0.33f)
                {
                    projectile.frame = 4;
                }
                else if (projectile.ai[0] <= 0.66f)
                {
                    projectile.frame = 5;
                }
                else
                {
                    projectile.frame = 6;
                }
            }
            else
            {
                if (projectile.frameCounter >= 30)
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
            Texture2D texture = mod.GetTexture("Projectiles/向日葵光效");
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / 7* projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/7)), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 14)-2), projectile.scale, 0, 0f);
            if (projectile.ai[0]>0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255, 247,161, 0), Color.LightBlue)*projectile.ai[0];
                spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / 7 * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/7)), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 14)-2), projectile.scale, 0, 0f);
                spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / 7 * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/7)), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 14)-2), projectile.scale, 0, 0f);
                spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / 7 * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/7)), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 14)-2), projectile.scale, 0, 0f);
            }
            if (projectile.localAI[1] != 0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255, 255, 255, 0), Color.LightBlue) * 30f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, Main.projectileTexture[projectile.type].Height / 7 * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/7)), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 14)-2), projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}