using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RailNomenclature
{
    public class Assets
    {
        private static Assets _instance;

        public static Dictionary<SpriteSheetID, SpriteSheet> SpriteSheets = new Dictionary<SpriteSheetID, SpriteSheet>();
        public static Dictionary<FontID, Font> Fonts = new Dictionary<FontID, Font>();
        public static SpriteBatch SpriteBatch;
        public static Texture2D WhitePixel;

        private Assets()
        {
            SpriteBatch = new SpriteBatch(TheGame.Instance.GraphicsDevice);

            WhitePixel = new Texture2D(TheGame.Instance.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            WhitePixel.SetData(new[] { Color.White });

            Font consolas16 = new Font(16);
            consolas16.AddCharacterSet(LoadSpriteSheet("Graphics/font", 9, 16), '\u0000', '\u00ff');

            Fonts.Add(FontID.Consolas16, consolas16);

            SpriteSheets.Add(SpriteSheetID.RAIL_MAP, LoadSpriteSheet("Graphics/rail-map", 251, 274));
            SpriteSheets.Add(SpriteSheetID.UI_LEFT_PANEL_ICONS, LoadSpriteSheet("Graphics/ui-left-panel", 64, 64));
            SpriteSheets.Add(SpriteSheetID.UI_MOUSE_ICONS, LoadSpriteSheet("Graphics/ui-mouse-actions", 15, 23));
            SpriteSheets.Add(SpriteSheetID.TELEPORTER, LoadSpriteSheet("Graphics/teleporter", 26, 34));
        }

        private SpriteSheet LoadSpriteSheet(string file, int spriteWidth, int spriteHeight)
        {
            return new SpriteSheet(TheGame.Instance.Content.Load<Texture2D>(file), spriteWidth, spriteHeight);
        }

        public static void Load()
        {
            if (_instance != null)
                return;

            _instance = new Assets();
        }

        public static void Unload()
        {

        }
    }
}
