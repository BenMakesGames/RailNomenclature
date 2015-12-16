using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    abstract public class MoveTarget : Thing
    {
        protected Character _user;

        public MoveTarget(Room r, Character u, float x, float y)
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

        virtual public void OnArrive() { }
        virtual public void OnBlocked() { }
        virtual public void OnCancel() { }
    }
}
