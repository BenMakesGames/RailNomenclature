using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RailNomenclature
{
    public static class Texture2DUtil
    {
        /*
        public void DrawPixelatedLine(Coordinate<int> p1, Coordinate<int> p2, int pixelSize, RGBA color)
        {
            int x1 = (p1.X / pixelSize) * pixelSize;
            int x2 = (p2.X / pixelSize) * pixelSize;
            int y1 = (p1.Y / pixelSize) * pixelSize;
            int y2 = (p2.Y / pixelSize) * pixelSize;

            bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);

            if (steep)
            {
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }

            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int dx = x2 - x1;
            int dy = Math.Abs(y2 - y1);
            int err = dx / 2;
            int ystep = (y1 < y2) ? 1 : -1;
            int y = y1;

            for (int x = x1; x <= x2; x += pixelSize)
            {
                if (steep)
                    DrawRectangle(y, x, pixelSize, pixelSize, color);
                else
                    DrawRectangle(x, y, pixelSize, pixelSize, color);

                err = err - dy;

                if (err < 0)
                {
                    y += ystep * pixelSize;
                    err += dx;
                }
            }
        }

        public void DrawProgressBar(int x, int y, int width, int height, float progress, Color c)
        {
            int progressWidth = (int)((width - 4) * progress);

            DrawRectangle(x, y, width, height, Color.Black);
            
            if(progressWidth > 0)
                DrawRectangle(x + 2, y + 2, progressWidth, height - 4, c);
        }

        public void DrawTab(int x, int y, int innerWidth, int innerHeight)
        {
            DrawTab(x, y, innerWidth, innerHeight, Color.White, Color.LightGray);
        }

        public void DrawTab(int x, int y, int innerWidth, int innerHeight, Color fillColor, Color borderColor)
        {
            DrawRectangle(x - 2, y - 2, innerWidth + 4, innerHeight + 2, borderColor);
            DrawRectangle(x, y, innerWidth, innerHeight, fillColor);
        }

        public void DrawWindow(int x, int y, int innerWidth, int innerHeight)
        {
            DrawWindow(x, y, innerWidth, innerHeight, Color.White, Color.Gold);
        }

        public void DrawWindow(int x, int y, int innerWidth, int innerHeight, RGBA fillColor, RGBA borderColor)
        {
            DrawWindow(x, y, innerWidth, innerHeight, fillColor.Color, borderColor.Color);
        }

        public void DrawWindow(int x, int y, int innerWidth, int innerHeight, Color fillColor, Color borderColor)
        {
            DrawRectangle(x - 2, y - 10, innerWidth + 4, innerHeight + 20, Color.Black);

            DrawRectangle(x, y - 8, innerWidth, 6, borderColor);
            DrawRectangle(x, y + innerHeight + 2, innerWidth, 6, borderColor);

            DrawRectangle(x, y, innerWidth, innerHeight, fillColor);
        }
        */
        public static void DrawRectangle(this Texture2D texture, int x, int y, int w, int h, RGBA c, float alpha = 1f)
        {
            Assets.SpriteBatch.Draw(texture, new Rectangle(x, y, w, h), c.Color * alpha);
        }

        public static void DrawRectangle(this Texture2D texture, float x, float y, int w, int h, RGBA c, float alpha = 1f)
        {
            Assets.SpriteBatch.Draw(texture, new Rectangle((int)x, (int)y, w, h), c.Color * alpha);
        }
    }
}
