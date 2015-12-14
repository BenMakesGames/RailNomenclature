using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class Wall: Switchable
    {
        private RGBA _color;

        private const int SWITCH_AND = 0;
        private const int SWITCH_OR = 1;
        private const int SWITCH_XOR = 2;

        public Wall(Room r, int x, int y, int w, int h, RGBA color)
            : base(r, x, y, w, h)
        {
            _color = color;
        }

        public override void Draw(Camera c)
        {
            if(IsOn())
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, Height, _color);

            base.Draw(c);
        }

        protected override void BuildCollisionRectangles()
        {
            _collision_rectangles.Add(new SimpleRectangle(-Width / 2, -Height, Width, Height));
        }

        public override void Step()
        {
            base.Step();

            ObeysCollisionRules = IsOn();
        }

        public override string Name() { return "Wall"; }
    }
}
