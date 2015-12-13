using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RailNomenclature
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; protected set; }

        public int SpriteWidth { get; protected set; }
        public int SpriteHeight { get; protected set; }
        public int SheetColumns { get { return Texture.Width / SpriteWidth; } }
        public int SheetRows { get { return Texture.Height / SpriteHeight; } }

        public SpriteSheet(Texture2D spriteSheet, int spriteWidth, int spriteHeight)
        {
            Texture = spriteSheet;
            SpriteWidth = spriteWidth;
            SpriteHeight = spriteHeight;
        }

        public void DrawScaled(int spriteIndex, int x, int y, float scale)
        {
            DrawScaled(spriteIndex, x, y, scale, RGBA.White);
        }

        public void DrawScaled(int spriteIndex, int x, int y, float scale, RGBA tint, float alpha = 1.0f)
        {
            int spriteX = (spriteIndex % SheetColumns) * SpriteWidth;
            int spriteY = (spriteIndex / SheetColumns) * SpriteHeight;

            Assets.SpriteBatch.Draw(Texture, new Rectangle(x, y, (int)(SpriteWidth * scale), (int)(SpriteHeight * scale)), new Rectangle(spriteX, spriteY, SpriteWidth, SpriteHeight), tint.Color * alpha);
        }

        public void DrawStanding(int spriteIndex, int x, int y)
        {
            DrawStanding(spriteIndex, x, y, RGBA.White);
        }

        public void DrawStanding(int spriteIndex, int x, int y, RGBA tint, float alpha = 1.0f)
        {
            int spriteX = (spriteIndex % SheetColumns) * SpriteWidth;
            int spriteY = (spriteIndex / SheetColumns) * SpriteHeight;

            Assets.SpriteBatch.Draw(Texture, new Vector2(x - SpriteWidth / 2, y - SpriteHeight), new Rectangle(spriteX, spriteY, SpriteWidth, SpriteHeight), tint.Color * alpha);
        }

        public void Draw(int spriteIndex, int x, int y)
        {
            Draw(spriteIndex, x, y, RGBA.White);
        }

        public void Draw(int spriteIndex, float x, float y)
        {
            Draw(spriteIndex, (int)x, (int)y, RGBA.White);
        }

        public void Draw(int spriteIndex, int x, int y, RGBA tint, float alpha = 1.0f)
        {
            int spriteX = (spriteIndex % SheetColumns) * SpriteWidth;
            int spriteY = (spriteIndex / SheetColumns) * SpriteHeight;

            Assets.SpriteBatch.Draw(Texture, new Vector2(x, y), new Rectangle(spriteX, spriteY, SpriteWidth, SpriteHeight), tint.Color * alpha);
        }
    }
}
