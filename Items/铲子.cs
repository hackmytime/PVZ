using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Items
{
	public class 铲子 : ModItem
	{

		public override void SetDefaults()
		{
			item.damage = 0;
			item.width = 46;
			item.height = 46;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 8000;
			item.rare = ItemRarityID.Orange;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("铲子");
			Tooltip.SetDefault("铲掉你的植物");
		}
        public override bool UseItem(Player player)
		{
			Vector2 position;
			position = Main.player[item.owner].GetModPlayer<PVZPlayer>().position;
			bool A = false;
			for (int I = 0; I < 1000; I++)
			{
				if ((Main.projectile[I].Center-position).Length()<16  && Main.projectile[I].GetGlobalProjectile<PVZProj>().Life > 0)
				{
					Main.projectile[I].GetGlobalProjectile<PVZProj>().Life = 0;
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/种植"), (int)player.position.X, (int)player.position.Y);
					A = true;
				}
			}
			return true;
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			position = Main.player[item.owner].GetModPlayer<PVZPlayer>().position;
			bool A = false;
			for (int I = 0; I < 1000; I++)
			{
				if ((Main.projectile[I].Center - position).Length() < 16 && Main.projectile[I].GetGlobalProjectile<PVZProj>().Life > 0)
				{
					A = true;
				}
			}
			if (A  && !WorldGen.SolidTile((int)position.X / 16, (int)position.Y / 16))
			{
				if (Main.player[item.owner].HeldItem.type == item.type)
				{
					spriteBatch.Draw(Main.itemTexture[item.type], position - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height)), Color.Green * 0.8F, 0, new Vector2(Main.itemTexture[item.type].Width / 2F - (11 * 16) + Main.itemTexture[item.type].Width / 8F, Main.itemTexture[item.type].Height / 2F - (6 * 16) + Main.itemTexture[item.type].Height / 8F), 1, 0, 0f);
				}
			}
			else
			{
				if (Main.player[item.owner].HeldItem.type == item.type)
				{
					spriteBatch.Draw(Main.itemTexture[item.type], position - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height)), Color.Red * 0.8F, 0, new Vector2(Main.itemTexture[item.type].Width / 2F - (11 * 16) + Main.itemTexture[item.type].Width / 8F, Main.itemTexture[item.type].Height / 2F - (6 * 16) + Main.itemTexture[item.type].Height / 8F), 1, 0, 0f);
				}
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position.X = (int)(Main.MouseWorld.X/32)*32;
			position.Y = (int)(Main.MouseWorld.Y/16)*16;
			return true;
		}
	}
}
