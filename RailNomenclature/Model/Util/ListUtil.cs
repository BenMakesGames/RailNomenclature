using System;
using System.Collections.Generic;

namespace RailNomenclature
{
    public static class ListUtil
    {
        // from http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        // alters the list directly rather than creating a shuffled copy
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = RNG.NextInt(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T PickOne<T>(this T[] list)
        {
            return list[RNG.NextInt(0, list.Length)];
        }

        public static T PickOne<T>(this List<T> list)
        {
            return list[RNG.NextInt(0, list.Count)];
        }
    }
}
