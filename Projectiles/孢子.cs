using Microsoft.Xna.Framework;
using PVZ.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 孢子 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("孢子");
        }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<PVZProj>().PlantAttack = true;
        }
        public override void AI()
        {
            int A =Dust.NewDust(projectile.Center, projectile.width, projectile.height, 33, -projectile.velocity.X/2, -2F, 0, new Color(54,23,81), 1f);
            Main.dust[A].noGravity = true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //damage = 0;
            //target.localAI[3] = 60;
        }
        public override void Kill(int timeLeft)
        {

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
    }
}