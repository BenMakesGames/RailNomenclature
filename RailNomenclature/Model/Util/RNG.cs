using System;

namespace RailNomenclature
{
    public static class RNG
    {
        private static Random _rng = new Random();

        public static float NextFloat(float min, float max)
        {
            return (float)(_rng.NextDouble() * (max - min)) + min;
        }

        public static int NextInt()
        {
            return _rng.Next();
        }

        public static int NextInt(int min, int max)
        {
            return _rng.Next(min, max);
        }

        public static bool NextBool()
        {
            return (_rng.Next() & 1) == 0;
        }
    }
}
