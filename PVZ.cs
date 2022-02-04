using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Items;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.UI;

namespace PVZ
{
	public class PVZ : Mod
	{
		private ModHotKey 自杀 = null;
		private ModHotKey 僵尸 = null;
		public override void Load()
        {
			自杀 = RegisterHotKey("自杀", "R");
			僵尸 = RegisterHotKey("僵尸", "L");
		}
		public override void PostUpdateInput()
		{
			if (PVZWorld.PlantsVsZombies)
			{
				if (PVZWorld.Surroundings == 0 || PVZWorld.Surroundings == 2|| PVZWorld.Surroundings == 4)
				{
					Main.time = 12000;
					Main.dayTime = true;
				}
				if (PVZWorld.Surroundings ==1 || PVZWorld.Surroundings == 3)
				{
					Main.time = 8000;
					Main.dayTime = false;
				}
				if(自杀 == null)
                {
					自杀 = RegisterHotKey("自杀","r");
				}
				if(自杀 == null)
                {
					僵尸 = RegisterHotKey("自杀","l");
				}
				if (Main.gameMenu)
				{
					return;
				}

				if (自杀.JustPressed)
				{
					Main.LocalPlayer.KillMe(PlayerDeathReason.ByCustomReason(Main.LocalPlayer.name + "自杀了."), 100000, 0);
				}
				if (僵尸.JustPressed)
				{
					PVZWorld.Time = 3000;
				}
			}
		}
		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (PVZWorld.PlantsVsZombies)
			{
				if (PVZWorld.Surroundings == 0)
				{
					music = GetSoundSlot(SoundType.Music, "Sounds/Music/白天");
				}
				if (PVZWorld.Surroundings == 1)
				{
					music = GetSoundSlot(SoundType.Music, "Sounds/Music/晚上");
					priority = (MusicPriority)4;
				}
			}
		}
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			if (PVZWorld.PlantsVsZombies)
			{
				int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
				LegacyGameInterfaceLayer orionProgress = new LegacyGameInterfaceLayer("Tremor: Invasion2",
					delegate
					{
						DrawOrionEvent(Main.spriteBatch);
						return true;
					},
					InterfaceScaleType.UI);
				layers.Insert(index, orionProgress);
			}
		}
		public void DrawOrionEvent(SpriteBatch spriteBatch)
		{
			if (PVZWorld.PlantsVsZombies && !Main.gameMenu)
			{
				float scaleMultiplier = 0.5f + 1 * 0.5f;
				float alpha = 0.5f;
				Texture2D progressBg = Main.colorBarTexture;
				Texture2D progressColor = Main.colorBarTexture;
				Texture2D orionIcon = GetTexture("Items/PVZ战旗/PVZ战旗白");
				Texture2D orionIcon2 = GetTexture("Items/阳光UI");
				string orionDescription = "第"+ PVZWorld.PlantsVsZombieswave+"波";
				Color descColor = new Color(39, 86, 134);

				Color waveColor = new Color(255, 241, 51);
				Color barrierColor = new Color(255, 241, 51);

				try
				{
					//draw the background for the waves counter
					const int offsetX = 20;
					const int offsetY = 20;
					int width = (int)(200f * scaleMultiplier);
					int height = (int)(46f * scaleMultiplier);
					Rectangle waveBackground = Utils.CenteredRectangle(new Vector2(Main.screenWidth/2 - offsetX, offsetY + 50f), new Vector2(width, height));
					Utils.DrawInvBG(spriteBatch, waveBackground, new Color(63, 65, 151, 255) * 0.785f);

					//draw wave text
					float A = (float)PVZWorld.PlantsVsZombiesPoints1 / PVZWorld.PlantsVsZombiesPointsMax;
					string waveText = "进度 " + (int)(A * 100) + "%";
					Utils.DrawBorderString(spriteBatch, waveText, new Vector2(waveBackground.X + waveBackground.Width / 2, waveBackground.Y), Color.White, scaleMultiplier, 0.5f, -0.1f);

					//draw the progress bar

					if (PVZWorld.PlantsVsZombiesPoints1 == 0)
					{

					}

					Rectangle waveProgressBar = Utils.CenteredRectangle(new Vector2(waveBackground.X + waveBackground.Width * 0.5f, waveBackground.Y + waveBackground.Height * 0.75f), new Vector2(progressColor.Width, progressColor.Height));
					Rectangle waveProgressAmount = new Rectangle(0, 0, (int)(progressColor.Width * A), progressColor.Height);
					Vector2 offset = new Vector2((waveProgressBar.Width - (int)(waveProgressBar.Width * scaleMultiplier)) * 0.5f, (waveProgressBar.Height - (int)(waveProgressBar.Height * scaleMultiplier)) * 0.5f);

					spriteBatch.Draw(progressBg, waveProgressBar.Location.ToVector2() + offset, null, Color.White * alpha, 0f, new Vector2(0f), scaleMultiplier, SpriteEffects.None, 0f);
					spriteBatch.Draw(progressBg, waveProgressBar.Location.ToVector2() + offset, waveProgressAmount, waveColor, 0f, new Vector2(0f), scaleMultiplier, SpriteEffects.None, 0f);

					//draw the icon with the event description

					//draw the background
					const int internalOffset = 6;
					Vector2 descSize = new Vector2(154, 40) * scaleMultiplier;
					Rectangle barrierBackground = Utils.CenteredRectangle(new Vector2(Main.screenWidth/2 - offsetX, offsetY + 50f), new Vector2(width, height));
					Rectangle descBackground = Utils.CenteredRectangle(new Vector2(barrierBackground.X + barrierBackground.Width * 0.5f, barrierBackground.Y - internalOffset - descSize.Y * 0.5f), descSize);
					Utils.DrawInvBG(spriteBatch, descBackground, descColor * alpha);

					//draw the icon
					int descOffset = (descBackground.Height - (int)(32f * scaleMultiplier)) / 2;
					Rectangle icon = new Rectangle(descBackground.X + descOffset, descBackground.Y + descOffset, (int)(32 * scaleMultiplier), (int)(32 * scaleMultiplier));
					spriteBatch.Draw(orionIcon, icon, Color.White);
					Utils.DrawBorderString(spriteBatch, orionDescription, new Vector2(barrierBackground.X + barrierBackground.Width * 0.5f, barrierBackground.Y - internalOffset - descSize.Y * 0.5f), Color.White, 0.80f, 0.3f, 0.4f);

					Rectangle barrierBackground2 = Utils.CenteredRectangle(new Vector2(offsetX+100, offsetY + 140), new Vector2(width, height));
					Rectangle descBackground2 = Utils.CenteredRectangle(new Vector2(barrierBackground2.X + barrierBackground2.Width * 0.5f, barrierBackground2.Y - internalOffset - descSize.Y * 0.5f), descSize);

					Rectangle icon2 = new Rectangle(descBackground2.X + descOffset+20, descBackground2.Y + descOffset, (int)(orionIcon2.Width * scaleMultiplier), (int)(orionIcon2.Height * scaleMultiplier));
					spriteBatch.Draw(orionIcon2, icon2,new Rectangle ?(new Rectangle(0,0, orionIcon2.Width, orionIcon2.Height)), Color.White);

					Utils.DrawBorderString(spriteBatch,""+ Main.LocalPlayer.GetModPlayer<PVZPlayer>().阳光, new Vector2(barrierBackground2.X + barrierBackground2.Width * 0.5f, barrierBackground2.Y - internalOffset - descSize.Y * 0.5f), Color.White, 0.80f, 0.3f, 0.4f);

					//draw text
					if (!PVZWorld.ChoosePlants)
					{
						barrierBackground2 = Utils.CenteredRectangle(new Vector2(Main.screenWidth/4, Main.screenHeight/1.8F), new Vector2(width, height));
						descBackground2 = Utils.CenteredRectangle(new Vector2(barrierBackground2.X + barrierBackground2.Width * 0.5f, barrierBackground2.Y - internalOffset - descSize.Y * 0.5f), descSize);
						Utils.DrawBorderString(spriteBatch, "1-9选择你的植物,10选择你的铲子", new Vector2(barrierBackground2.X + barrierBackground2.Width * 0.5f, barrierBackground2.Y - internalOffset - descSize.Y * 0.5f), Color.White, 3f, 0f, 0.4f);
						Utils.DrawBorderString(spriteBatch, "选择栏可以为空", new Vector2(barrierBackground2.X + barrierBackground2.Width * 0.5f, barrierBackground2.Y - internalOffset - descSize.Y * 0.5f+100), Color.White, 3f, 0f, 0.4f);
					}

				}
				catch (Exception e)
				{
					ErrorLogger.Log(e.ToString());
				}
			}
		}
	}
	public class PVZProj : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;

		public bool Plant = false;
		public bool PlantAttack = false;
		public bool Sleep = false;
		public int Life;
		
	}
	public class PVZNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;

		public bool Zombie = false;
		public bool 冰冻 = false;
		public bool Cherry = false;
		//0被吃后消化//1被吃后不用消化//2不会被吃
		public int Piranha = 0;
		public bool Devour = false;
		public override void ResetEffects(NPC npc)
        {
			冰冻 = false;

		}
        public override bool PreAI(NPC npc)
        {
			if(npc.life>npc.lifeMax)
            {
				npc.life = npc.lifeMax;
            }
            return base.PreAI(npc);
        }


    }
	public class PVZItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;

		public bool Plant = false;
		
	}
}