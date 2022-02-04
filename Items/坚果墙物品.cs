using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Items
{
	public class 坚果墙物品 : 植物
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
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/种植");
			item.shoot = ModContent.ProjectileType<坚果墙>();
			阳光 = 50;
			MaxCD = 2400;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("坚果墙");
			Tooltip.SetDefault("消耗50阳光");
		}
	}
}
