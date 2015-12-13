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
        protected Coordinate<int> _target_location;

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

        public Character(Room l, float x, float y, string name, string description)
            : base(l, x, y, 0, 0, description)
        {
            _name = name;

            ChangeDimensions(RNG.NextInt(8, 12 + 1), RNG.NextInt(38, 53 + 1));

            SkinColor = SKIN_COLORS[RNG.NextInt(0, SKIN_COLORS.Length)];
            ShirtColor = CLOTHES_COLORS[RNG.NextInt(0, CLOTHES_COLORS.Length)];
            PantsColor = CLOTHES_COLORS[RNG.NextInt(0, CLOTHES_COLORS.Length)];

            WalkSpeed = Height / 100f - RNG.NextFloat(0, 0.1f);

            l.World.Characters.Add(this);
        }

        private float _hop_y, _hop_velocity;

        public void Hop()
        {
            if (_hop_y == 0)
                _hop_velocity = -2;
        }

        public void HandleInput()
        {
            if (_target_location != null)
            {
                WalkToward(_target_location.X, _target_location.Y);
                if (WithinDistance(_target_location.X, _target_location.Y, 5))
                    _target_location = null;
            }
        }

        public void WalkToward(int x, int y)
        {
            float dx = x - X();
            float dy = y - Y();
            float d = (float)Math.Sqrt((dx * dx) + (dy * dy));

            _x_velocity += WalkSpeed * dx / d;
            _y_velocity += WalkSpeed * dy / d;

            Hop();
        }

        public void SetPath(int x, int y)
        {
            _target_location = new Coordinate<int>(x, y);
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

        public override string SecondaryAction()
        {
            return Location.World.ActiveCharacter == this ? "" : "Talk";
        }

        public override int MaximumSecondaryActionDistance()
        {
            return TheGame.HEIGHT / 3;
        }
    }
}
