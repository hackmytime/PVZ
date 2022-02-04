using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PVZ.Dusts
{
	public class 冰豌豆粒子1 : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale *= 1f;
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 12, 10);
		}
		int A;
		public override bool Update(Dust dust)
		{
			dust.scale += 0.02f;
			A++;
			if (A > 120)
			{
				dust.active = false;
			}
			return true;
		}
	}
}
