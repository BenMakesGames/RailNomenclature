﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class Picture: Thing
    {
        private SpriteSheetID _picture;
        private string _name;
        private RGBA _color;

        public Picture(Room r, int x, int y, int w, int h, RGBA color, SpriteSheetID picture, string name)
            : base(r, x, y, w, h)
        {
            _picture = picture;
            _name = name;
            _color = color;
        }

        public override string Name() { return _name; }

        public override string PrimaryAction()
        {
            return "Look at " + Name();
        }

        public override void DoPrimaryAction(Thing a)
        {
            a.Notify(_picture);
        }

        public override void Draw(Camera c)
        {
            Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, Height, _color);
        }
    }
}