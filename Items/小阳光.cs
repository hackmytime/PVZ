using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace PVZ.Items
{
    public class 小阳光 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("小阳光");
        }

        public override string Texture => "PVZ/Items/阳光";
        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 26;
            item.height = 30;
            item.uniqueStack = true;
            item.rare = ItemRarityID.White;
            item.consumable = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.noMelee = false;
            item.UseSound = SoundID.Item1;
            item.tileWand = 1;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 2));
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Items/阳光");
            Rectangle value = new Rectangle(0, 0, texture.Width, texture.Height);
            Texture2D texture2 = mod.GetTexture("Items/阳光2");
            Rectangle value2 = new Rectangle(0, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture2, position, new Rectangle?(value), Color.White, a/10, origin / 2, sc2 * 0.5f, (SpriteEffects)1, 0f);
            spriteBatch.Draw(texture2, position, new Rectangle?(value), Color.White, a/10, origin / 2, -sc2 * 0.5f, (SpriteEffects)1, 0f);
            spriteBatch.Draw(texture, position, new Rectangle?(value), Color.White, 0f, origin / 2, scale*0.5f, (SpriteEffects)1, 0f);
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Rectangle value = new Rectangle(0, 0, texture.Width, texture.Height);
            Texture2D texture2 = mod.GetTexture("Items/阳光2");
            Rectangle value2 = new Rectangle(0, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture2, item.Center - Main.screenPosition, new Rectangle?(value), Color.White, (float)a / 50+(3.14F/4), new Vector2(texture2.Width / 2, texture2.Height / 2), (item.scale + (sc2*2)) * 0.5f, (SpriteEffects)1, 0f);
            spriteBatch.Draw(texture2, item.Center - Main.screenPosition, new Rectangle?(value), Color.White, (float)a / 50, new Vector2(texture2.Width / 2, texture2.Height / 2), (item.scale + (-sc2*2)) * 0.5f, (SpriteEffects)1, 0f);
            spriteBatch.Draw(texture, item.Center - Main.screenPosition, new Rectangle?(value), Color.White, 0f, new Vector2(texture.Width/2, texture.Height/2), item.scale * 0.5f, (SpriteEffects)1, 0f);
            return false;
        }
        bool A = false;
        float p;
        Vector2 po;
        float np;
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Vector2 vector = Vector2.Zero;
            for (int A = 0; A < 255; A++)
            {
                if (Main.player[A].active)
                {
                    item.owner = A;
                    vector = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - item.Center;
                }
            }
            float R = vector.Length();
            if (item.noMelee)
            {
                if (p == 0)
                {
                    po = item.position;
                    np = Main.rand.Next(700, 1400);
                }
                p+=2;
                if (p < np)
                {
                    item.position.Y = po.Y + p;
                    maxFallSpeed = 0;
                }
                else
                {
                    item.noMelee = false;
                    maxFallSpeed = 1;
                }
            }
            if (R < 18)
            {
                A = true;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/阳光"), (int)item.position.X, (int)item.position.Y);
            }
            if (A)
            {
                item.noMelee = false;
                Player player = Main.player[item.owner];
                Vector2 v4 = player.Center - item.Center;
                v4.Normalize();
                v4 *= 12;
                item.position += v4;
            }
            a++;
            if (a % 5 == 0)
            {
                if (sc == 0)
                {
                    sc2 += 0.01f;
                }
                if (sc == 1)
                {
                    sc2 -= 0.01f;
                }
            }
            if (sc2 > 0.15f)
            {
                sc = 1;
            }
            if (sc2 < -0.15f)
            {
                sc = 0;
            }
            if (!item.noMelee)
            {
                光++;
                if(光>480&& !A)
                {
                    item.scale -= 0.03F;
                }
                if (item.scale <= 0.1)
                {
                    item.active = false;
                }
            }
        }
        int a;
        int sc;
        float sc2;
        float 光;
        public override bool OnPickup(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                player.GetModPlayer<PVZPlayer>().阳光 += 15;
            }
            return false;
        }
    }
}