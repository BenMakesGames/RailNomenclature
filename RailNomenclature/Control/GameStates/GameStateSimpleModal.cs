using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace RailNomenclature
{
    abstract public class GameStateSimpleModal: GameState
    {
        private GameState _previous_state;

        public GameStateSimpleModal(GameState previousState)
        {
            _previous_state = previousState;
        }

        public override void Update()
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

        abstract protected bool NextMessage();

        public override void Draw()
        {
            _previous_state.Draw();

            DrawModal();
        }

        abstract protected void DrawModal();
    }
}
