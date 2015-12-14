using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    [Serializable]
    public class Door: Thing
    {
        public delegate bool LockedDelegate(Door door, Thing actor);

        public Door OtherSide { get; protected set; }

        public LockedDelegate IsLocked = null;

        private int _exit_x_offset, _exit_y_offset;

        public Door(Room l, int x, int y, int exitXOffset = 0, int exitYOffset = 0)
            : base(l, x, y, 32, 64)
        {
            _exit_x_offset = exitXOffset;
            _exit_y_offset = exitYOffset;
        }

        public override string Name()
        {
            return "Door to " + OtherSide.Location.Name;
        }

        public int ExitX() { return (int)X() + _exit_x_offset; }
        public int ExitY() { return (int)Y() + _exit_y_offset; }

        public bool PairWith(Door d)
        {
            if (d.OtherSide == null && OtherSide == null)
            {
                d.OtherSide = this;
                OtherSide = d;

                return true;
            }
            else
                return false;
        }

        protected override void BuildCollisionRectangles()
        {
            // no collision rectangles for doors!
        }

        public override void Draw(Camera c)
        {
            Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() - c.Y, Width, Height, RGBA.Black);
            
            base.Draw(c);
        }

        public override string PrimaryAction()
        {
            return "Enter";
        }

        public override int MaximumPrimaryActionDistance()
        {
            return 40;
        }

        public override void DoPrimaryAction(Thing a)
        {
            if (IsLocked == null || !IsLocked(this, a))
            {
                a.MoveTo(OtherSide.Location, OtherSide.ExitX(), OtherSide.ExitY());
                
                if (a == Location.World.ActiveCharacter)
                    Location.World.Camera.Center();
            }
        }
    }
}
