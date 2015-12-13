using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RailNomenclature
{
    public class Font
    {
        protected class CharacterSet
        {
            public int FirstCharacter, LastCharacter;
            public SpriteSheet SpriteSheet;
            public int YOffset;
        }

        private List<CharacterSet> _character_sets = new List<CharacterSet>();

        public int LineHeight { get; private set; }

        public Font(int lineHeight)
        {
            LineHeight = lineHeight;
        }

        public void AddCharacterSet(SpriteSheet spriteSheet, char firstChar, char lastChar)
        {
            CharacterSet c = new CharacterSet();
            c.SpriteSheet = spriteSheet;
            c.FirstCharacter = firstChar;
            c.LastCharacter = lastChar;
            c.YOffset = LineHeight - spriteSheet.SpriteHeight;

            _character_sets.Add(c);
        }

        public int WriteUnderlinedText(float x, float y, string text, RGBA color)
        {
            return WriteUnderlinedText((int)x, (int)y, text, color);
        }

        public int WriteUnderlinedText(int x, int y, string text, RGBA color)
        {
            int originalX = x;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 13 || text[i] == 10)
                {
                    x = originalX;
                    y += LineHeight + 4;
                    continue;
                }

                int addX = WriteCharacter(x, y, text[i], color.Color);

                Assets.WhitePixel.DrawRectangle(x, y + LineHeight, addX, 2, color);

                x += addX;
            }

            return y;
        }

        public int WriteText(float x, float y, string text, RGBA color)
        {
            return WriteText((int)x, (int)y, text, color);
        }

        public int WriteText(int x, int y, string text, RGBA color)
        {
            int originalX = x;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 13 || text[i] == 10)
                {
                    x = originalX;
                    y += LineHeight;
                    continue;
                }

                x += WriteCharacter(x, y, text[i], color.Color);
            }

            return y;
        }

        protected CharacterSet GetCharacterSetContaining(char c)
        {
            foreach (CharacterSet charSet in _character_sets)
            {
                if (c >= charSet.FirstCharacter && c <= charSet.LastCharacter)
                    return charSet;
            }

            return null;
        }

        public int WriteCharacter(int x, int y, char c, Color color)
        {
            CharacterSet charSet = GetCharacterSetContaining(c);

            if (charSet == null) return 0;

            int cOffset = c - charSet.FirstCharacter;
            int realY = y + charSet.YOffset;

            Texture2D texture = charSet.SpriteSheet.Texture;

            int spriteX = (cOffset % charSet.SpriteSheet.SheetColumns) * charSet.SpriteSheet.SpriteWidth;
            int spriteY = (cOffset / charSet.SpriteSheet.SheetColumns) * charSet.SpriteSheet.SpriteHeight;

            Assets.SpriteBatch.Draw(
                texture,
                new Rectangle(x, realY, charSet.SpriteSheet.SpriteWidth, charSet.SpriteSheet.SpriteHeight),
                new Rectangle(spriteX, spriteY, charSet.SpriteSheet.SpriteWidth, charSet.SpriteSheet.SpriteHeight),
                color
            );

            return charSet.SpriteSheet.SpriteWidth;
        }
    }
}
