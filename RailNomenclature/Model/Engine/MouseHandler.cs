using System;
using Microsoft.Xna.Framework.Input;

namespace RailNomenclature
{
    public class MouseHandler
    {
        public static MouseHandler Instance = new MouseHandler();

        private MouseState _previous_state;
        private MouseState _current_state;

        private MouseHandler()
        {
            Update();
        }

        public void Update()
        {
            _previous_state = _current_state;
            _current_state = Mouse.GetState();
        }

        public bool IsLeftClicking(bool changedSincePreviousUpdate = false)
        {
            if (changedSincePreviousUpdate)
                return _current_state.LeftButton == ButtonState.Pressed && _previous_state.LeftButton != ButtonState.Pressed;
            else
                return _current_state.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightClicking(bool changedSincePreviousUpdate = false)
        {
            if (changedSincePreviousUpdate)
                return _current_state.RightButton == ButtonState.Pressed && _previous_state.RightButton != ButtonState.Pressed;
            else
                return _current_state.RightButton == ButtonState.Pressed;
        }

        public int X() { return _current_state.X; }
        public int Y() { return _current_state.Y; }
    }
}
