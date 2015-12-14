using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class GameStateNotePad: LeftPanelObject
    {
        public GameStateNotePad(GameStateLeftPanel p)
            : base(p)
        {
        }

        public override void Draw()
        {
            base.Draw();

            // @TODO: draw stuff
        }

        public override void HandleInput()
        {
        }

        public override void Update()
        {
        }

        public override int Width()
        {
            return 300;
        }
    }
}
