using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PVZ.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PVZ.Items
{
	public class 阳光菇物品 : 植物
	{

		public override void SetDefaults()
		{
			item.damage = 20;
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
			item.shoot = ModContent.ProjectileType<阳光菇>();
			阳光 = 0;
			MaxCD = 0;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("阳光菇物品");
			Tooltip.SetDefault("消耗25阳光");
		}
	}
}
