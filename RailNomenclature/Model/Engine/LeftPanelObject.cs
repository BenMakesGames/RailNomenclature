using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    abstract public class LeftPanelObject: GameState
    {
        public GameStateLeftPanel LeftPanel { get; private set; }

        public LeftPanelObject(GameStateLeftPanel p)
            : base()
        {
            LeftPanel = p;
        }

        public override void Draw()
        {
            Assets.WhitePixel.DrawRectangle(LeftPanel.X + GameStateLeftPanel.BASE_WIDTH, 0, Width(), TheGame.HEIGHT, RGBA.Black, 0.6f);
        }

        abstract public int Width();
    }
}
