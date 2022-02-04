using Microsoft.Xna.Framework;
using PVZ.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 一大波 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("一大波");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<PVZProj>().PlantAttack = true;
        }
        public override void AI()
        {
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //damage = 0;
            //target.localAI[3] = 60;
        }
    }
}