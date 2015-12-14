using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class RectangleStructure: Switchable
    {
        private RGBA _color;
        private RGBA _roof_color;
        private int _roof_height;

        public RectangleStructure(Room r, int x, int y, int w, int h, int roofHeight, RGBA color, RGBA roofColor)
            : base(r, x, y, w, h)
        {
            _color = color;
            _roof_height = roofHeight;
            _roof_color = roofColor;

            _collision_rectangles = new List<SimpleRectangle>();
            BuildCollisionRectangles();
        }

        public override string Name() { return "Building"; }

        protected override void BuildCollisionRectangles()
        {
            _collision_rectangles.Add(new SimpleRectangle(-Width / 2, -_roof_height, Width, _roof_height));
        }

        public override void Draw(Camera c)
        {
            if (IsOn())
            {
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, Height, _color);
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y - _roof_height, Width, _roof_height, _roof_color);
            }

            base.Draw(c);
        }

        public override void Step()
        {
            base.Step();

            ObeysCollisionRules = IsOn();
        }
    }
}
