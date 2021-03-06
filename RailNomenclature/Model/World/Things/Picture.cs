﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class Picture : Thing
    {
        private SpriteSheetID _picture;
        private string _name;
        
        private RGBA _color;
        private SpriteSheetID _thumbnail = SpriteSheetID.NONE;
        
        private int _y_offset;

        public Picture(Room r, int x, int y, int w, int h, int yOffset, SpriteSheetID thumbNail, SpriteSheetID picture, string name)
            : base(r, x, y, w, h)
        {
            _picture = picture;
            _name = name;
            _thumbnail = thumbNail;
            _y_offset = yOffset;
        }

        public Picture(Room r, int x, int y, int w, int h, int yOffset, RGBA color, SpriteSheetID picture, string name)
            : base(r, x, y, w, h)
        {
            _picture = picture;
            _name = name;
            _color = color;
            _y_offset = yOffset;
        }

        public override float Y()
        {
            return base.Y() + _y_offset;
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
            if (_thumbnail != SpriteSheetID.NONE)
                Assets.SpriteSheets[_thumbnail].Draw(0, LeftX() - c.X, TopY() - c.Y);
            else
                Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, Height, _color);
        }
    }
}
