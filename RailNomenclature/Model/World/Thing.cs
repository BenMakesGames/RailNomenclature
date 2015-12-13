using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    [Serializable]
    abstract public class Thing
    {
        protected SpriteSheetID _sprite_sheet_id;
        public Room Location { get; protected set; }
        public string Description { get; protected set; }
        
        protected float _x_center, _y_base;
        protected float _x_velocity = 0;
        protected float _y_velocity = 0;

        virtual public float X() { return _x_center; }
        virtual public float Y() { return _y_base; }

        public float LeftX() { return X() - Width / 2; }
        public float RightX() { return X() - Width / 2 + Width; } // we can't do X() + Width / 2; rounding may add 1 pixel, if we do
        public float TopY() { return Y() - Height; }
        public float BottomY() { return Y(); }

        public int Width { get; private set; } // in pixels
        public int Height { get; private set; } // in pixels

        public bool FlaggedForDestruction { get; protected set; }

        public bool ObeysCollisionRules = true;

        protected List<SimpleRectangle> _collsion_rectangles = new List<SimpleRectangle>();

        abstract public string Name();
        virtual public string Its() { return "it's"; }

        public Thing(Room r, float x, float y, int w, int h, string description = "")
        {
            ChangeDimensions(w, h);
            
            Description = description;

            MoveTo(r, x, y);
        }

        public void ChangeDimensions(int w, int h)
        {
            Width = w;
            Height = h;

            BuildCollisionRectangles();
        }

        virtual protected void BuildCollisionRectangles()
        {
            _collsion_rectangles.Add(new SimpleRectangle(-Width / 2, -Width / 2, Width, Width));
        }

        public void MoveTo(float x, float y)
        {
            if (Location != null)
                Location.RemoveThingAtY(this);

            _x_center = x;
            _y_base = y;

            if (Location != null)
                Location.AddThingAtY(this);
        }

        public void MoveTo(Room r, float x, float y)
        {
            if (r != Location && Location != null)
            {
                Location.RemoveThing(this);

                if (r == null || Location.World != r.World)
                    Location.World.Things.Remove(this);
            }
            else if(Location != null)
                Location.RemoveThingAtY(this);

            _x_center = x;
            _y_base = y;

            if(r != Location && r != null)
            {
                r.AddThing(this);

                if (Location == null || Location.World != r.World)
                    r.World.Things.Add(this);
            }
            else if(r != null)
                r.AddThingAtY(this);

            Location = r;
        }

        virtual public int ActionReach() { return Width; }

        virtual public bool AnyActionsAvailable() { return PrimaryAction() != "" || SecondaryAction() != ""; }

        virtual public string PrimaryAction() { return Description == "" ? "" : "Look"; }
        virtual public string SecondaryAction() { return ""; }

        virtual public int MaximumPrimaryActionDistance() { return TheGame.WIDTH * 10; }
        virtual public int MaximumSecondaryActionDistance() { return Width; }

        virtual public void DoPrimaryAction(Thing a) { a.Notify(null, Description); }
        virtual public void DoSecondaryAction(Thing a) { }

        private Room _needs_location_readding_for_y;

        virtual public void PreStep()
        {
            if (_x_velocity != 0 || _y_velocity != 0)
            {
                if(Location != null)
                    Location.RemoveThingAtY(this);
                
                _needs_location_readding_for_y = Location;
            }
        }

        virtual public void Step()
        {
            float oldXCenter = _x_center;
            float oldYBase = _y_base;

            _x_center += _x_velocity;
            _y_base += _y_velocity;
            _x_velocity *= 0.85f;
            _y_velocity *= 0.85f;

            if (ObeysCollisionRules && Location != null && Location.ThingCollidingWith(this) != null)
            {
                _x_center = oldXCenter;
                _y_base = oldYBase;
            }

            ConstrainToRoom();
        }

        virtual protected void ConstrainToRoom()
        {
            if (Location != null && ObeysCollisionRules)
            {
                if (_x_center - Width / 2 < Location.MinCharacterX)
                    _x_center = Location.MinCharacterX + Width / 2;
                else if (_x_center + Width / 2 > Location.MaxCharacterX)
                    _x_center = Location.MaxCharacterX - Width / 2;

                if (_y_base < Location.MinCharacterY)
                    _y_base = Location.MinCharacterY;
                else if (_y_base > Location.MaxCharacterY)
                    _y_base = Location.MaxCharacterY;
            }
        }

        virtual public void PostStep()
        {
            if (_needs_location_readding_for_y != null && _needs_location_readding_for_y == Location)
            {
                Location.AddThingAtY(this);
            }
        }

        virtual public void Draw(Camera c) { }

        virtual public void DrawInstructions(Camera c)
        {
            Assets.WhitePixel.DrawRectangle(LeftX() - 2 - c.X, TopY() - 2 - c.Y, 1, Height + 4, Location.SelectionColor);
            Assets.WhitePixel.DrawRectangle(LeftX() - 2 - c.X, TopY() - 2 - c.Y, Width + 4, 1, Location.SelectionColor);
            Assets.WhitePixel.DrawRectangle(RightX() + 2 - c.X, TopY() - 2 - c.Y, 1, Height + 4, Location.SelectionColor);
            Assets.WhitePixel.DrawRectangle(LeftX() - 2 - c.X, Y() + 2 - c.Y, Width + 4, 1, Location.SelectionColor);

            int spriteIndex = 0;

            if (PrimaryAction() != "")
            {
                spriteIndex += 1;
                Assets.Fonts[FontID.Consolas16].WriteText(X() - c.X - 10 - PrimaryAction().Width() * 9, TopY() - 25 - c.Y, PrimaryAction(), Location.InstructionTextColor);
            }

            if (SecondaryAction() != "")
            {
                spriteIndex += 2;
                Assets.Fonts[FontID.Consolas16].WriteText(X() - c.X + 10, TopY() - 25 - c.Y, SecondaryAction(), Location.InstructionTextColor);
            }

            Assets.SpriteSheets[SpriteSheetID.UI_MOUSE_ICONS].Draw(spriteIndex, (int)(X() - 7 - c.X), (int)(TopY() - 25 - c.Y), Location.InstructionTextColor);
        }

        virtual public bool CanBeTaken(Thing taker) { return false; }

        public float DistanceTo(float x, float y)
        {
            float dx = x - X();
            float dy = y - Y();

            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }

        public float DistanceTo(Thing t)
        {
            return DistanceTo(t.X(), t.Y());
        }

        public bool WithinDistance(float x, float y, int maxDistance)
        {
            float dx = x - X();
            float dy = y - Y();

            return (dx * dx) + (dy * dy) <= maxDistance * maxDistance;
        }

        public bool WithinDistance(Thing t, int maxDistance)
        {
            return WithinDistance(t.X(), t.Y(), maxDistance);
        }

        virtual public void Notify(SpriteSheetID picture)
        {
            Location.World.PlayingState.QueueState(new GameStateSimpleModalPicture(TheGame.Instance.CurrentState, picture));
        }

        virtual public void Notify(string title, string s)
        {
            Location.World.PlayingState.QueueState(new GameStateSimpleModalMessage(TheGame.Instance.CurrentState, title, s));
        }

        virtual public void Notify(string title, List<string> s)
        {
            Location.World.PlayingState.QueueState(new GameStateSimpleModalMessage(TheGame.Instance.CurrentState, title, s));
        }

        public void Destroy()
        {
            FlaggedForDestruction = true;
        }

        public bool Overlaps(int x, int y)
        {
            return x >= LeftX() && y >= TopY() && x < RightX() && y < BottomY();
        }

        virtual public bool IsOverlapping(Thing t)
        {
            if (this is Character && t is RectangleStructure)
            {

            }

            foreach (SimpleRectangle r in _collsion_rectangles)
            {
                int rX = (int)X() + r.X;
                int rY = (int)Y() + r.Y;

                foreach (SimpleRectangle rT in t._collsion_rectangles)
                {
                    int rTX = (int)t.X() + rT.X;
                    int rTY = (int)t.Y() + rT.Y;

                    if (
                        rX < rTX + rT.Width &&
                        rX + r.Width > rTX &&
                        rY < rTY + rT.Height &&
                        rY + r.Height > rTY
                    )
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
