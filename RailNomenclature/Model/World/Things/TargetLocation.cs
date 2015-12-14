using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class TargetLocation: Thing
    {
        Character _user;

        public TargetLocation(Room r, int x, int y, Character u)
            : base(r, x, y, 30, 15)
        {
            _user = u;
        }

        protected override void BuildCollisionRectangles()
        {
            // none
        }

        public override bool IsFlushWithFloor()
        {
            return true;
        }

        public override void Draw(Camera c)
        {
            base.Draw(c);

            Assets.SpriteSheets[SpriteSheetID.MOVE_CURSOR].Draw(0, (int)(_x_center - Width / 2 - c.X), (int)(_y_base - Height / 2 - c.Y), _user.ShirtColor);
        }

        public override string Name()
        {
            return _user.Name() + "'s Target Location";
        }

        public override string PrimaryAction() { return ""; }
        public override string SecondaryAction() { return ""; }
    }
}
