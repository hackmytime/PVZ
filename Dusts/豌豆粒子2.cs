using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PVZ.Dusts
{
	public class 豌豆粒子2 : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale *= 1f;
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 10, 10);
		}
		int A;
		public override bool Update(Dust dust)
		{
			dust.scale += 0.05f;
			A++;
			if(A>120)
            {
				dust.active = false;
            }
			return true;
		}
	}
}
