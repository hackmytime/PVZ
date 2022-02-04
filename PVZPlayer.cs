using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Items;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ID;

namespace PVZ
{
    public class PVZPlayer : ModPlayer
    {
        public int 阳光 = 50;
        public void Bossperspective(Vector2 position, int Time, float Ti = 0.05f)
        {
            if (player.Distance(position) < 3000f)
            {
                ScreenPosition = position - new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
                focusTransition = 0f;
                startPoint = Main.screenPosition;
                LockTime = Time;
                Times = Ti;
                Start = true;
            }
        }
        //起点
        public Vector2 startPoint;
        //终点
        public Vector2 ScreenPosition;
        //锁定时间
        public int LockTime;
        //开始
        public bool Start;
        public float Times;
        private float focusTransition;
        public override void ModifyScreenPosition()
        {
            if (Start)
            {
                if (LockTime > 0f)
                {
                    if (focusTransition <= 1f)
                    {
                        Main.screenPosition = Vector2.SmoothStep(startPoint, ScreenPosition, focusTransition += Times);
                    }
                    else
                    {
                        Main.screenPosition = ScreenPosition;
                    }
                    LockTime -= 1;
                }
                else if (focusTransition >= 0f)
                {
                    Main.screenPosition = Vector2.SmoothStep(player.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), ScreenPosition, focusTransition -= Times);
                }
                else
                {
                    Start = false;
                }
            }
        }
        int A;
        public Vector2 position;
        public override void PreUpdate()
        {
            position.X = (int)(Main.MouseWorld.X / 32) * 32;
            position.Y = (int)(Main.MouseWorld.Y / 16) * 16 + 2;

            if (PVZWorld.PlantsVsZombies)
            {
                if (!PVZWorld.ChoosePlants)
                {
                    if (!Main.playerInventory)
                    {
                        int a = 0;
                        for (int A = 0; A < 9; A++)
                        {
                            if (player.inventory[A].type==ItemID.None|| player.inventory[A].GetGlobalItem<PVZItem>().Plant)
                            {
                                a++;
                            }

                        }
                        if (player.inventory[9].type == ItemID.None|| player.inventory[9].type == ModContent.ItemType<铲子>())
                        {
                            a++;
                        }
                        if (a >= 10)
                        {
                            PVZWorld.ChoosePlants = true;
                        }
                        else
                        {
                            for (int A = 0; A < 9; A++)
                            {
                                if (player.inventory[A].type != ItemID.None && !player.inventory[A].GetGlobalItem<PVZItem>().Plant)
                                {
                                    Main.NewText("请正确的放置好植物,错误格子："+(A+1));
                                }
                            }
                            if (player.inventory[9].type != ItemID.None && player.inventory[9].type != ModContent.ItemType<铲子>())
                            {
                                Main.NewText("请正确的放置好植物,错误格子：" + 10);
                            }
                            Main.playerInventory = true;
                        }
                    }
                }
                if (PVZWorld.ChoosePlants)
                {
                    Main.playerInventory = false;
                }
                if (A == 0 && PVZWorld.time == 0)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/僵尸来咯"), (int)player.position.X, (int)player.position.Y);
                    A = 1;
                }
                player.velocity = Vector2.Zero;
                player.Center = PVZWorld.PlantsVsZombiesCenter - new Vector2(68 * 16, -26 * 16+16);
                Bossperspective(PVZWorld.PlantsVsZombiesCenter - new Vector2(20 * 16, 0), 2, 0.2f);
                if (PVZWorld.time > 800)
                {
                    A = 0;
                }
            }
            else
            {
                阳光 = 50;
                A = 0;
            }
            阳光 = 500;
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (PVZWorld.PlantsVsZombies)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/游戏失败"), (int)player.position.X, (int)player.position.Y);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/NO"), (int)player.position.X, (int)player.position.Y);
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public static int FindClosest(Vector2 Position, int Width, int Height, int 目标 = 0)
        {
            int result = 0;
            if (目标 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].dontTakeDamage)
                    {
                        result = i;
                        break;
                    }
                }
                float num = -1f;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].active && !Main.npc[j].dontTakeDamage)
                    {
                        float num2 = Math.Abs(Main.npc[j].position.X + Main.npc[j].width / 2 - (Position.X + Width / 2)) + Math.Abs(Main.npc[j].position.Y + Main.npc[j].height / 2 - (Position.Y + Height / 2));
                        if (num == -1f || num2 < num)
                        {
                            num = num2;
                            result = (byte)j;
                        }
                    }
                }
            }
            else if (NPC.AnyNPCs(目标))
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == 目标 && Main.npc[i].active)
                    {
                        result = i;
                        break;
                    }
                }
                float num = -1f;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].type == 目标 && Main.npc[j].active)
                    {
                        float num2 = Math.Abs(Main.npc[j].position.X + Main.npc[j].width / 2 - (Position.X + Width / 2)) + Math.Abs(Main.npc[j].position.Y + Main.npc[j].height / 2 - (Position.Y + Height / 2));
                        if (num == -1f || num2 < num)
                        {
                            num = num2;
                            result = (byte)j;
                        }
                    }
                }
            }
            return result;
        }
    }
}