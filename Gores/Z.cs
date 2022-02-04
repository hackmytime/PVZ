using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PVZ.Gores
{
	public class Z : ModGore
	{
		public override void OnSpawn(Gore gore)
		{
			gore.frame = (byte)Main.rand.Next(1);
			gore.frameCounter = (byte)Main.rand.Next(1);
			gore.velocity.X = 0.4f;
			gore.numFrames = 1;
		}
		int A;
		int B;
		public override bool Update(Gore gore)
		{
			gore.scale += 0.02f;
			gore.alpha += 3;
			if(gore.alpha%40==0)
            {
				gore.velocity.X = -1;
			}
			if(gore.alpha%40==20)
            {
				gore.velocity.X = 1;
			}
			
			gore.velocity.Y = -1;
			if (gore.alpha >= 255f || gore.velocity.Y == 0)
			{
				gore.active = false;
			}
			gore.rotation = 0;
			Lighting.AddLight(gore.position, new Vector3(2.45F, 0.8F, 0.8F) * 0.3F);
			return base.Update(gore);
        }
    }
}