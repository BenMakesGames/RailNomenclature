using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public abstract class GameStateCutScene: GameState
    {
        private GameState _previous_state;
        
        protected int _step = 0;

        public GameStateCutScene()
            : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            if(_previous_state == null)
                _previous_state = TheGame.Instance.CurrentState;
        }

        public override void HandleInput()
        {
            // nothin'
        }

        public override void Update()
        {
            _previous_state.Update();

            _step++;
        }

        public override void Draw()
        {
            _previous_state.Draw();
        }

        protected void EndCutScene()
        {
            TheGame.Instance.ChangeState(_previous_state);
        }
    }
}
