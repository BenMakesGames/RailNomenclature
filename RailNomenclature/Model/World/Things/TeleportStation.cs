using System;

namespace RailNomenclature
{
    public class TeleportStation : Thing
    {
        private int _animation_step = 0;

        public TeleportStation(Room r, int x, int y, int w = 26, int h = 34, string description = "Teleport Station")
            : base(r, x, y, w, h, description)
        {
            r.World.AddTeleporter(this);
        }

        public override void Draw(Camera c)
        {
            base.Draw(c);

            Assets.SpriteSheets[SpriteSheetID.TELEPORTER].Draw(_animation_step * 2 / TheGame.FPS, LeftX() - c.X, TopY() - c.Y);
        }

        protected override void BuildCollisionRectangles()
        {
            _collision_rectangles.Add(new SimpleRectangle(-Width / 2, -2, Width, 2));
        }

        public override void Step()
        {
            base.Step();

            if (_animation_step == TheGame.FPS - 1)
                _animation_step = 0;
            else
                _animation_step++;
        }

        public override string Name()
        {
            return Description;
        }

        public override string PrimaryAction() { return "Teleport"; }
        public override string SecondaryAction() { return "Pick Up"; }

        public override int MaximumPrimaryActionDistance()
        {
            return 40;
        }

        public override void DoPrimaryAction(Thing a)
        {
            TeleportStation t = Location.World.NextTeleporter(this);

            if (t != null && t != this)
            {
                a.MoveTo(t.Location, t.X(), t.Y() + 10);

                if (a == Location.World.ActiveCharacter)
                    Location.World.Camera.Center();
            }
            else
                a.Notify(null, "At least two teleports must be set up in order to use one.");
        }

        public override void DoSecondaryAction(Thing a)
        {
            if (a is Character)
            {
                Location.World.RemoveTeleporter(this);
                Location.RemoveThing(this);
                Location = null;

                (a as Character).TeleportStations++;
            }
        }
    }
}
