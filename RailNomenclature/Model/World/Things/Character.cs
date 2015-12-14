using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RailNomenclature
{
    [Serializable]
    public class Character: Thing
    {
        protected string _name;
        protected TargetLocation _target_location;

        public float WalkSpeed { get; protected set; }

        public int TeleportStations = 0;

        private List<Thing> _inventory = new List<Thing>();

        public static RGBA[] SKIN_COLORS = new RGBA[] {
            Color.Wheat, Color.Tan, Color.Moccasin, Color.SaddleBrown, Color.Chocolate, Color.PeachPuff,
        };

        public static RGBA[] CLOTHES_COLORS = new RGBA[] {
            Color.Green, Color.DodgerBlue, Color.DarkRed, Color.Ivory, Color.MidnightBlue, Color.SlateGray, Color.DarkKhaki,
        };

        public RGBA SkinColor { get; protected set; }
        public RGBA ShirtColor { get; set; }
        public RGBA PantsColor { get; set; }

        public Character(Room l, float x, float y, int width, int height, string name, string description)
            : base(l, x, y, width, height, description)
        {
            _name = name;

            SkinColor = SKIN_COLORS[RNG.NextInt(0, SKIN_COLORS.Length)];
            ShirtColor = CLOTHES_COLORS[RNG.NextInt(0, CLOTHES_COLORS.Length)];
            PantsColor = CLOTHES_COLORS[RNG.NextInt(0, CLOTHES_COLORS.Length)];

            WalkSpeed = 0.6f;

            l.World.Characters.Add(this);
        }

        private float _hop_y, _hop_velocity;
        private int _ignore_location_history_steps = 0; // used to allow player to start moving, when a new location is picked (see HandleInput() and SetPath())

        public void Hop()
        {
            if (_hop_y == 0)
                _hop_velocity = -2;
        }

        public void HandleInput()
        {
            if (_target_location != null)
            {
                if (_ignore_location_history_steps == 0 && _previous_10_locations[0].X == (int)_x_center && _previous_10_locations[0].Y == (int)_y_base)
                    CancelPath();
                else
                {
                    WalkToward(_target_location.X(), _target_location.Y());

                    if (WithinDistance(_target_location, 5))
                        CancelPath();
                }
            }
        }

        public void WalkToward(float x, float y)
        {
            float dx = x - X();
            float dy = y - Y();
            float d = (float)Math.Sqrt((dx * dx) + (dy * dy));

            _x_velocity += WalkSpeed * dx / d;
            _y_velocity += WalkSpeed * dy / d;

            IsAccelerating = true;

            Hop();
        }

        public void SetPath(float x, float y)
        {
            SetPath((int)x, (int)y);
        }

        public void CancelPath()
        {
            if (_target_location != null)
            {
                Location.RemoveThing(_target_location);
                _ignore_location_history_steps = 0;
                _target_location = null;
            }
        }

        public void SetPath(int x, int y)
        {
            CancelPath();

            _ignore_location_history_steps = 5;
            _target_location = new TargetLocation(Location, x, y, this);
        }

        public override void Step()
        {
            base.Step();

            _hop_y += _hop_velocity;
            _hop_velocity += 0.5f;

            if (_hop_y >= 0)
            {
                _hop_y = 0;
                _hop_velocity = 0;
            }

            if (_ignore_location_history_steps > 0)
                _ignore_location_history_steps--;
        }

        public override string Name()
        {
            return _name;
        }

        public override void Draw(Camera c)
        {
            int legHeight = Height * 4 / 10;
            int torsoHeight = Height - legHeight - 8;

            // head
            Assets.WhitePixel.DrawRectangle(LeftX() - c.X + 2, TopY() + _hop_y - c.Y, Width - 4, 6, SkinColor); // head

            // torso
            Assets.WhitePixel.DrawRectangle(LeftX() - c.X, TopY() + 8 + _hop_y - c.Y, Width, torsoHeight, ShirtColor != null ? ShirtColor : SkinColor); // torso

            // legs (left and right, respectively)
            Assets.WhitePixel.DrawRectangle(LeftX() - c.X, Y() - legHeight + _hop_y - c.Y, Width / 3, legHeight, PantsColor != null ? PantsColor : SkinColor);
            Assets.WhitePixel.DrawRectangle(RightX() - c.X - Width / 3, Y() - legHeight + _hop_y - c.Y, Width / 3, legHeight, PantsColor != null ? PantsColor : SkinColor);

            base.Draw(c);
        }

        public override int ActionReach()
        {
            return Width * 2;
        }

        public override string Its()
        {
            return Name() + " is";
        }

        public static readonly string[] FIRST_NAMES = {
            "Aileen", "Brianna", "Chloe", "Dana", "Eve", "Freya", "Gabby", "Haley", "India", "Jen",
            "Katie", "Leia", "Megan", "Natalie", "Olivia", "Paige", "Quinn", "Rina", "Sandy", "Tricia",
            "Uma", "Valerie", "Wavery", "Xia", "Yvette", "Zakia",

            "Art", "Ben", "Clayton", "Daniel", "Easton", "Finn", "Gabriel", "Hank", "Ian", "Jake",
            "Karl", "Lyle", "Mark", "Nathan", "Oakley", "Perry", "Quinton", "Rupert", "Sam", "Toby",
            "Umar", "Van", "Wesley", "X", "York", "Zach",
        };

        public static string RandomName()
        {
            return FIRST_NAMES[RNG.NextInt(0, FIRST_NAMES.Length)];
        }

        public bool DoTake(Thing target)
        {
            if (!target.CanBeTaken(this))
            {
                return false;
            }
            else
            {
                AddInventory(target);
                Location.RemoveThing(target);
                return true;
            }
        }

        public bool HasInventory(Thing t)
        {
            return _inventory.Contains(t);
        }

        public void AddInventory(Thing t)
        {
            _inventory.Add(t);
        }

        public bool RemoveInventory(Thing t)
        {
            return _inventory.Remove(t);
        }

        public override string PrimaryAction()
        {
            return Location.World.ActiveCharacter == this ? "" : base.PrimaryAction();
        }

        public override string SecondaryAction()
        {
            return Location.World.ActiveCharacter == this ? "" : "Talk";
        }

        public override int MaximumSecondaryActionDistance()
        {
            return TheGame.HEIGHT / 3;
        }

        public override void MoveTo(Room newLocation, float x, float y)
        {
            CancelPath();

            base.MoveTo(newLocation, x, y);
        }
    }
}
