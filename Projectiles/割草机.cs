using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 割草机 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("割草机");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 18;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            Main.projFrames[projectile.type] = 2;
        }
        public override void AI()
        {
            projectile.height = 18;
            if (projectile.GetGlobalProjectile<PVZProj>().PlantAttack)
            {
                projectile.velocity.X = 3;
                projectile.velocity.Y = 3;
            }
            else
            {
                projectile.velocity.X = 0;
                projectile.velocity.Y = 3;
                projectile.timeLeft = 10000;
            }
            if(!PVZWorld.PlantsVsZombies)
            {
                projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.GetGlobalProjectile<PVZProj>().PlantAttack=true;
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/2)), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / 4)), 1f, 0, 0f);
            return false;
        }
    }
}