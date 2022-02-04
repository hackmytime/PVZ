using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace PVZ.Buffs
{
	public class 寒霜侵袭 : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("寒霜侵袭");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
			canBeCleared = false;
		}
        public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<PVZNPC>().冰冻= true;
		}
	}
}