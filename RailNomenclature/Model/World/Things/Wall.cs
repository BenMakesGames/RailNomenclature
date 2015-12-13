using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class Wall: Thing
    {
        private RGBA _color;

        public Wall(Room r, int x, int y, int w, int h, RGBA color)
            : base(r, x, y, w, h)
        {
            _color = color;
        }

        public override void Draw(Camera c)
        {
            Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, Height, _color);

            base.Draw(c);
        }

        protected override void BuildCollisionRectangles()
        {
            _collsion_rectangles.Add(new SimpleRectangle(-Width / 2, -Height, Width, Height));
        }

        public override string Name() { return "Wall"; }
    }
}
