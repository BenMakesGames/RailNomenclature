using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class Camera
    {
        public float X = 0;
        public float Y = 0;

        public float CenterX { get { return X + TheGame.WIDTH / 2; } }
        public float CenterY { get { return Y + TheGame.HEIGHT / 2; } }

        public Thing _target;

        public Camera(Thing t)
        {
            _target = t;
            Center();
        }

        public void ChangeTarget(Thing t)
        {
            _target = t;
            Center();
        }

        public void Center()
        {
            CenterOn(_target.X(), _target.Y());
        }

        public void CenterOn(Thing t)
        {
            CenterOn(t.X(), t.Y());
        }

        public void CenterOn(float x, float y)
        {
            X = x - TheGame.WIDTH / 2;
            Y = y - TheGame.HEIGHT / 2;

            ConstrainWithinCameraBounds();
        }

        public void Step()
        {
            CenterToward(_target.X(), _target.Y());

            ConstrainWithinCameraBounds();
        }

        private void ConstrainWithinCameraBounds()
        {
            if (_target.Location != null)
            {
                if (X < _target.Location.MinCameraX)
                    X = _target.Location.MinCameraX;
                else if (X > _target.Location.MaxCameraX)
                    X = _target.Location.MaxCameraX;

                if (Y < _target.Location.MinCameraY)
                    Y = _target.Location.MinCameraY;
                else if (Y > _target.Location.MaxCameraY)
                    Y = _target.Location.MaxCameraY;
            }
        }

        public void CenterToward(float x, float y, float speedMultiplier = 1f)
        {
            x -= TheGame.WIDTH / 2;
            y -= TheGame.HEIGHT / 2;

            float dx = x - X;
            float dy = y - Y;

            // first, handle small numbers ('cause floats can be weird)
            if (Math.Abs(dx) <= 0.2f)
                X = x;
            else if (Math.Abs(dx) <= 2)
                X += dx / 4;
            else
            {
                if (dx > 0)
                    X += (float)Math.Log(dx, 2) * speedMultiplier;
                else if (dx < 0)
                    X -= (float)Math.Log(-dx, 2) * speedMultiplier;
            }

            // first, handle small numbers ('cause floats can be weird)
            if (Math.Abs(dy) <= 0.2f)
                Y = y;
            else if (Math.Abs(dy) <= 2)
                Y += dy / 4;
            else
            {
                if (dy > 0)
                    Y += (float)Math.Log(dy, 2) * speedMultiplier;
                else if (dy < 0)
                    Y -= (float)Math.Log(-dy, 2) * speedMultiplier;
            }
        }
    }
}
