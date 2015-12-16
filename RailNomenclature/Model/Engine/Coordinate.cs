using System;

namespace RailNomenclature
{
    public class Coordinate<T>
    {
        public T X, Y;

        public Coordinate() { }

        public Coordinate(Coordinate<T> c)
        {
            X = c.X;
            Y = c.Y;
        }

        public Coordinate(T x, T y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
