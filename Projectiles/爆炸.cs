using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 爆炸 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("樱桃炸弹");
        }
        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 100;
            projectile.scale = 5f;
            Main.projFrames[projectile.type] = 7;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            if (projectile.frame > 7)
            {
                projectile.Kill();
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int F = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, F, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type])), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2)), projectile.scale, 0, 0f);
            return false;
        }
    }
}