using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class ToggleSwitch : Switch
    {
        private RGBA _color;
        private bool _is_on = false;

        public ToggleSwitch(Room r, int x, int y, RGBA color)
            : base(r, x, y, 18, 17)
        {
            _color = color;
        }

        public override string Name()
        {
            return "Switch";
        }

        protected override void BuildCollisionRectangles()
        {
            // no
        }

        public override string SecondaryAction()
        {
            return "Switch";
        }

        public override void DoSecondaryAction(Thing a)
        {
            _is_on = !_is_on;
        }

        public override bool IsActivated()
        {
            return _is_on;
        }

        public override void Draw(Camera c)
        {
            Assets.SpriteSheets[SpriteSheetID.LEVER].Draw(_is_on ? 1 : 0, LeftX() - c.X, TopY() - c.Y);

            Assets.WhitePixel.DrawRectangle(LeftX() - c.X + (_is_on ? 11 : 1), TopY() - c.Y, 5, 5, _color);

            base.Draw(c);
        }
    }
}
