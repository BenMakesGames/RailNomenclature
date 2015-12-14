using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace RailNomenclature
{
    abstract public class GameStateSimpleModal: GameState
    {
        private GameState _previous_state;

        public GameStateSimpleModal()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            if (_previous_state == null)
                _previous_state = TheGame.Instance.CurrentState;
        }

        public override void HandleInput()
        {
            if (!TheGame.Instance.IsMouseOnUIElement())
            {
                if (MouseHandler.Instance.IsLeftClicking(true) || MouseHandler.Instance.IsRightClicking(true))
                {
                    if (!NextMessage())
                        TheGame.Instance.ChangeState(_previous_state);
                }
            }
        }

        public override void Update()
        {
        }

        abstract protected bool NextMessage();

        public override void Draw()
        {
            _previous_state.Draw();

            DrawModal();
        }

        abstract protected void DrawModal();
    }
}
