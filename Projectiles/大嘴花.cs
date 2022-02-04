using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Projectiles
{
    public class 大嘴花 : 普通植物
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("大嘴花");
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height =28;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<PVZProj>().Life = 350;
            projectile.GetGlobalProjectile<PVZProj>().Plant = true;
            projectile.usesLocalNPCImmunity = true;
            Main.projFrames[projectile.type] = 14;
        }
        public override void AI()
        {
            projAI(projectile);
            float G;
            int result = 0;
            for (int i = 0; i < 200; i++)
            {
                float x = Main.npc[i].Center.X - projectile.Center.X;
                float y = Main.npc[i].Center.Y + Main.npc[i].height / 4 - projectile.Center.Y;
                if (y < 0)
                {
                    y *= -1;
                }
                if (x > 0 && y < 32 && Main.npc[i].active && !Main.npc[i].dontTakeDamage)
                {
                    result = i;
                    break;
                }
            }
            float num = -1f;
            for (int j = 0; j < 200; j++)
            {
                float x = Main.npc[j].Center.X - projectile.Center.X;
                float y = Main.npc[j].Center.Y + Main.npc[j].height / 4 - projectile.Center.Y;
                if (y < 0)
                {
                    y *= -1;
                }
                if (x > 0 && y < 32 && Main.npc[j].active && !Main.npc[j].dontTakeDamage)
                {
                    float num2 = Math.Abs(Main.npc[j].position.X + Main.npc[j].width / 2 - (projectile.Center.X)) + Math.Abs(Main.npc[j].position.Y + Main.npc[j].height / 2 - (projectile.Center.Y));
                    if (num == -1f || num2 < num)
                    {
                        num = num2;
                        result = (byte)j;
                    }
                }
            }

            G = ((Main.npc[result].position + new Vector2(0, Main.npc[result].height / 2)) -( projectile.position+new Vector2(projectile.width,projectile.height/2))).Length();
            projectile.frameCounter++;
            if (吃 == 0)
            {
                if (projectile.frameCounter >= 20)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }
            else if(吃 == 1)
            {
                if (projectile.frameCounter >= 20)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame > 7)
                {
                    projectile.frame = 0;
                    吃 = 0;
                }
            }
            else if(吃 ==2)
            {
                if (projectile.frameCounter >= 20)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame > 8)
                {
                    projectile.frame = 7;
                }
            }
            else if(吃 ==3)
            {
                if (projectile.frameCounter >= 20)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame >= 13&& projectile.frameCounter>18)
                {
                    projectile.frame = 0;
                    吃 = 0;
                }
            }
            if (G < 50)
            {
                if (吃 == 0)
                {
                    吃 = 1;
                }
                if (Main.npc[result].GetGlobalNPC<PVZNPC>().Piranha==1)
                {
                    if (projectile.frame == 6&& 吃 ==1)
                    {
                        Main.npc[result].GetGlobalNPC<PVZNPC>().Devour = true;
                        吃 = 3;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/吃"), projectile.Center);
                        Main.npc[result].life -= 200;
                        CombatText.NewText(new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height), Color.Red, "美味", true, false);
                        Main.npc[result].checkDead();
                        Main.npc[result].HitEffect(0,2000);
                    }
                }
                if (Main.npc[result].GetGlobalNPC<PVZNPC>().Piranha ==0)
                {
                    if (projectile.frame ==6 && 吃 == 1)
                    {
                        Main.npc[result].GetGlobalNPC<PVZNPC>().Devour = true;
                        吃 = 2;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/吃"), projectile.Center);
                        Main.npc[result].life -= 2000;
                        CombatText.NewText(new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height), Color.Red, "大饱嘴福", true, false);
                        Main.npc[result].checkDead();
                        Main.npc[result].HitEffect(0, 2000);
                    }
                }
                if (Main.npc[result].GetGlobalNPC<PVZNPC>().Piranha == 2)
                {
                    if (projectile.frame == 7&& 吃 == 1)
                    {
                        吃 = 0;
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/吃"), projectile.Center);
                        Main.npc[result].life -= 40;
                        CombatText.NewText(new Rectangle((int)Main.npc[result].position.X, (int)Main.npc[result].position.Y, Main.npc[result].width, Main.npc[result].height), Color.Red, "40", false, true);
                        Main.npc[result].checkDead();
                        Main.npc[result].HitEffect(0, 2000);
                    }
                }
            }
            if (吃 == 2)
            {
                消耗++;
                if(消耗>1200)
                {
                    吃 = 3;
                }
            }
            else
            {
                消耗 = 0;
            }
        }
        int 消耗;
        int 吃;
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.localAI[1] = 5;
            damage = 0;
        }
        public override void Kill(int timeLeft)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int W = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] * projectile.frame;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, W, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type])), drawColor, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2-0), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2+4)), projectile.scale, 0, 0f);
            if (projectile.localAI[1] != 0)
            {
                Color RGBA = Utils.MultiplyRGBA(new Color(255,255, 255, 0), Color.LightBlue) * 30f;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, W, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height/ Main.projFrames[projectile.type])), RGBA, projectile.rotation, new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2-0), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]/2+4)), projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}