using System;

namespace RailNomenclature
{
    public class SimpleRectangle
    {
        public int X, Y, Width, Height;

        public SimpleRectangle(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        public SimpleRectangle(float x, float y, int w, int h)
        {
            X = (int)x;
            Y = (int)y;
            Width = w;
            Height = h;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + "), " + Width + "x" + Height;
        }
    }
}
