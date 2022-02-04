using Microsoft.Xna.Framework;
using PVZ.Buffs;
using PVZ.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 冰豌豆 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("冰豌豆");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
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
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (!target.HasBuff(ModContent.BuffType<寒霜侵袭>()))
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/冰冻"), target.Center); 
            }
            target.AddBuff(ModContent.BuffType<寒霜侵袭>(), 300);
            //damage = 0;
            //target.localAI[3] = 60;
        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<冰豌豆粒子1>(), -projectile.velocity.X, -projectile.velocity.Y,0,default,1f);
            Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<冰豌豆粒子2>(), -projectile.velocity.X, -projectile.velocity.Y, 0, default, 1f);

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
    }
}