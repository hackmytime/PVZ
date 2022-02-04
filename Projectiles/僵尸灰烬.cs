using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 僵尸灰烬 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("僵尸灰烬");
        }
        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 100;
            Main.projFrames[projectile.type] = 6;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 20 && projectile.frame != 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            if (projectile.frame == 5)
            {
                projectile.alpha++;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int F = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, F, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type])), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] /2)), projectile.scale, 0, 0f);
            return false;
        }
    }
}