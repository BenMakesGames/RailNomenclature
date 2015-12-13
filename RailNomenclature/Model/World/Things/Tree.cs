using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    [Serializable]
    public class Tree: Thing
    {
        public static readonly RGBA LEAF_COLOR = new RGBA(61, 103, 33);
        public static readonly RGBA BRANCH_COLOR = new RGBA(120, 120, 30);

        private int _random_seed = RNG.NextInt();

        private RGBA _leaf_color, _branch_color;

        public Tree(Room l, int x, int y)
            : base(l, x, y, 0, 0)
        {
            ChangeDimensions(RNG.NextInt(50, 80), RNG.NextInt(100, 200));

            _leaf_color = new RGBA(LEAF_COLOR.R + RNG.NextInt(-10, 11), LEAF_COLOR.G + RNG.NextInt(-10, 11), LEAF_COLOR.B + RNG.NextInt(-10, 11));
            _branch_color = new RGBA(BRANCH_COLOR.R + RNG.NextInt(-10, 11), BRANCH_COLOR.G + RNG.NextInt(-10, 11), BRANCH_COLOR.B + RNG.NextInt(-10, 11));
        }

        protected override void BuildCollisionRectangles()
        {
            // no collision rectangle!
        }

        public override void Draw(Camera c)
        {
            Random rng = new Random(_random_seed);

            int trunkWidth = rng.Next(4, 8 + 1);
            int trunkOffset = rng.Next(-6, 2 + 1);
            int leaflessHeight = rng.Next(Height / 4, Height * 3 / 4);
            int leafHeight = Height - leaflessHeight;

            int trunkHeight = leaflessHeight + leafHeight / 2;

            Assets.WhitePixel.DrawRectangle(X() - trunkOffset - c.X, Y() - trunkHeight - c.Y, trunkWidth, trunkHeight, _branch_color);

            int leafArea = Width * leafHeight;

            int leafCount = (int)Math.Sqrt(leafArea) / 2;

            for (int i = 0; i < leafCount; i++)
            {
                int size = rng.Next(4, 12 + 1);
                int x = rng.Next(0, Width - size);
                int y = rng.Next(0, leafHeight - size);

                Assets.WhitePixel.DrawRectangle(LeftX() + x - c.X, TopY() + y - c.Y, size, size, _leaf_color);
            }

            base.Draw(c);
        }

        public override string Name()
        {
            return "Tree";
        }
    }
}
