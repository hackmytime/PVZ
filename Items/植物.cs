using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Items
{
	public abstract class 植物 : ModItem
	{

		public int 阳光 = 100;
		public int CD;
		public int MaxCD = 450;
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D texture = Main.inventoryBack11Texture;
			if (CD > 0)
			{
				float R = 1;
				if (Main.player[item.owner].HeldItem.type != item.type)
				{
					int W = (int)(texture.Height * ((float)CD / MaxCD));
					R *= 0.75F;
					spriteBatch.Draw(texture, position + new Vector2(Main.itemTexture[item.type].Width / 2 + 2, Main.itemTexture[item.type].Height / 2 + 2), new Rectangle?(new Rectangle(0, 0, texture.Width, W)), Color.Red * 0.8F, 3.14F, new Vector2((int)(52 * scale) / 2F, (int)(52 * scale) / 2F), R, 0, 0f);
				}
				else
				{
					int W = (int)(texture.Height * ((float)CD / MaxCD));
					spriteBatch.Draw(texture, position + new Vector2(Main.itemTexture[item.type].Width / 2, Main.itemTexture[item.type].Height / 2), new Rectangle?(new Rectangle(0, 0, texture.Width, W)), Color.Red * 0.8F, 3.14F, new Vector2((int)(52 * scale) / 2F, (int)(52 * scale) / 2F), R, 0, 0f);
				}
			}
			Utils.DrawBorderString(spriteBatch, "" + 阳光, position + new Vector2(-8, 20), Color.White, 0.6f);
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			int Tile = 2;
			if (PVZWorld.Surroundings == 1)
			{
				Tile = 70;
			}
			position = Main.player[item.owner].GetModPlayer<PVZPlayer>().position;
			bool A = false;
			for (int I = 0; I < 1000; I++)
			{
				if (position == Main.projectile[I].Center && Main.projectile[I].GetGlobalProjectile<PVZProj>().Life > 0)
				{
					A = true;
				}
			}
			if (Main.tile[(int)(position.X / 16) + 1, (int)(position.Y / 16) + 1].type == Tile && !A && Main.tile[(int)(position.X / 16) - 1, (int)(position.Y / 16) + 1].type == Tile && Main.player[item.owner].GetModPlayer<PVZPlayer>().阳光 >= 阳光 && CD < 0 && !WorldGen.SolidTile((int)position.X / 16, (int)position.Y / 16))
			{
				if (Main.player[item.owner].HeldItem.type == item.type)
				{
					spriteBatch.Draw(Main.itemTexture[item.type], position - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height)), Color.Green * 0.8F, 0, new Vector2(Main.itemTexture[item.type].Width / 2F-(11*16) + Main.itemTexture[item.type].Width / 8F, Main.itemTexture[item.type].Height / 2F - (6 * 16)+ Main.itemTexture[item.type].Height / 8F), 1, 0, 0f);
				}
			}
			else
			{
				if (Main.player[item.owner].HeldItem.type == item.type)
				{
					spriteBatch.Draw(Main.itemTexture[item.type], position-Main.screenPosition, new Rectangle?(new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height)), Color.Red * 0.8F, 0, new Vector2(Main.itemTexture[item.type].Width / 2F - (11 * 16) + Main.itemTexture[item.type].Width / 8F, Main.itemTexture[item.type].Height /2F-(6*16) + Main.itemTexture[item.type].Height /8F), 1, 0, 0f);
				}
			}
			return true;
		}
        public override void UpdateInventory(Player player)
		{
			item.GetGlobalItem<PVZItem>().Plant = true;
			CD--;
			if(!PVZWorld.ChoosePlants)
			{
				CD=1;
			}
			if(item.GetGlobalItem<PVZItem>().Plant)
            {
				int a = 0;
				for (int A = 0; A < player.inventory.Length; A++)
                {
					if(item.type == player.inventory[A].type)
                    {
						a++;
                    }
                }
				if(a>1)
                {
					item.type = 0;
                }
			}
		}
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool UseItem(Player player)
		{
			return base.UseItem(player);
		}
        public override bool CanUseItem(Player player)
		{
			Vector2 position = player.GetModPlayer<PVZPlayer>().position;
			bool A = false;
			for(int I = 0;I<1000;I++)
            {
				if (position == Main.projectile[I].Center&&Main.projectile[I].GetGlobalProjectile<PVZProj>().Life>0)
                {
					A = true;
                }
            }
			int Tile = 2;
			if(PVZWorld.Surroundings==1)
            {
				Tile = 70;
            }
			if (Main.tile[(int)(position.X / 16) + 1, (int)(position.Y / 16) + 1].type == Tile && !A && Main.tile[(int)(position.X / 16) - 1, (int)(position.Y / 16) + 1].type == Tile && player.GetModPlayer<PVZPlayer>().阳光 >= 阳光 && CD < 0 && !WorldGen.SolidTile((int)position.X / 16, (int)position.Y / 16))
			{
				return true;
			}
			return false;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position.X = (int)(Main.MouseWorld.X/32)*32;
			position.Y = (int)(Main.MouseWorld.Y/16)*16+2;
			player.GetModPlayer<PVZPlayer>().阳光 -= 阳光;
			CD = MaxCD;
			return true;
		}
	}
}
