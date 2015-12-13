using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RailNomenclature
{
    [Serializable]
    public class Room
    {
        public World World { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public string Name { get; protected set; }
        public RGBA BackgroundColor { get; set; }
        public RGBA InstructionTextColor { get; set; }
        public bool IsOutdoors { get; set; }
        public RGBA SelectionColor { get; protected set; }

        private List<Thing> _things = new List<Thing>();

        private Dictionary<int, List<Thing>> _things_sorted_by_y = new Dictionary<int, List<Thing>>();

        public int MinCameraX { get; set; }
        public int MinCameraY { get; set; }
        public int MaxCameraX { get; set; }
        public int MaxCameraY { get; set; }
        public int MinCharacterX { get; set; }
        public int MinCharacterY { get; set; }
        public int MaxCharacterX { get; set; }
        public int MaxCharacterY { get; set; }

        public Room(World world, int width, int height, string name)
        {
            World = world;

            Width = width;
            Height = height;
            Name = name;

            SetBackgroundColor(Color.MediumTurquoise);

            World.Locations.Add(this);

            IsOutdoors = false;

            for (int i = 0; i < MaxAllowedHeight; i++)
                _things_sorted_by_y.Add(i, new List<Thing>());

            MinCameraX = 0;
            MaxCameraX = Width - TheGame.WIDTH;
            MinCameraY = 0;
            MaxCameraY = Height - TheGame.HEIGHT;

            MinCharacterX = 0;
            MaxCharacterX = Width;
            MinCharacterY = 0;
            MaxCharacterY = Height;
        }

        public int MaxAllowedHeight { get { return Height + 100; } }

        public void SetBackgroundColor(RGBA background)
        {
            BackgroundColor = background;

            if (BackgroundColor.Brightness() < 130)
            {
                InstructionTextColor = RGBA.White;
                SelectionColor = RGBA.White;
            }
            else
            {
                InstructionTextColor = new RGBA(64);
                SelectionColor = new RGBA(64);
            }
        }

        public void RemoveThing(Thing t)
        {
            _things.Remove(t);
            RemoveThingAtY(t);
        }

        public void RemoveThingAtY(Thing t)
        {
            _things_sorted_by_y[(int)t.Y()].Remove(t);
        }

        public void AddThingAtY(Thing t)
        {
            _things_sorted_by_y[(int)t.Y()].Add(t);
        }

        public void AddThing(Thing t)
        {
            _things.Add(t);
            AddThingAtY(t);

            if (t.Y() < 0)
                t.MoveTo(t.X(), 0);
            else if (t.Y() >= MaxAllowedHeight)
                t.MoveTo(t.X(), MaxAllowedHeight - 1);
        }

        public void Draw()
        {
            TheGame.Instance.GraphicsDevice.Clear(BackgroundColor.Color);

            for(int i = 0; i < MaxAllowedHeight; i++)
            {
                foreach (Thing t in _things_sorted_by_y[i])
                    t.Draw(World.Camera);
            }
        }

        virtual public void Step()
        {
        }

        public Thing FindActionableThingAtCursor(Camera c)
        {
            if (TheGame.Instance.IsMouseOnUIElement()) return null;

            int x = MouseHandler.Instance.X() + (int)c.X, y = MouseHandler.Instance.Y() + (int)c.Y;

            foreach (Thing t in _things)
            {
                if (t.Overlaps(x, y) && t.AnyActionsAvailable())
                {
                    return t;
                }
            }

            return null;
        }

        public Thing ThingCollidingWith(Thing target)
        {
            foreach (Thing t in _things)
            {
                if (target != t && target.IsOverlapping(t))
                    return t;
            }

            return null;
        }
    }
}
