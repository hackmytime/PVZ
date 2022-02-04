using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 爆炸土豆 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("土豆地雷");
        }
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<PVZProj>().Life = 350;
            projectile.GetGlobalProjectile<PVZProj>().PlantAttack = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 100;
            Main.projFrames[projectile.type] = 1;
        }
        public override void AI()
        {
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
        }
    }
}