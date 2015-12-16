using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class PressurePlate : Switch
    {
        private RGBA _color;

        public PressurePlate(Room r, int x, int y, int w, int h, RGBA color)
            : base(r, x, y, w, h)
        {
            _color = color;
            ObeysCollisionRules = false;
        }

        public override bool IsFlushWithFloor()
        {
            return true;
        }

        public override string Name()
        {
            return "Pressure Plate";
        }

        protected override void BuildCollisionRectangles()
        {
            _collision_rectangles.Add(new SimpleRectangle(-Width / 2, -Height, Width, Height));
        }

        public override bool IsActivated()
        {
            List<Thing> overlappingThings = Location.ThingsOverlappingWith(this);

            return overlappingThings.Count > 0;
        }

        public override void Draw(Camera c)
        {
            // gray outline/base
            Assets.WhitePixel.DrawRectangle(LeftX() - c.X - 1, TopY() - c.Y - 1, Width + 2, Height + 2, new RGBA(0, 0, 0, 64));

            if (IsActivated())
            {
                // pressed
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, Height, _color);
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, 2, RGBA.Black, 0.3f);
            }
            else
            {
                // released
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y - 2, Width, Height + 2, _color);
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, BottomY() - 2 - c.Y, Width, 2, RGBA.White, 0.3f);
            }

            base.Draw(c);
        }
    }
}
