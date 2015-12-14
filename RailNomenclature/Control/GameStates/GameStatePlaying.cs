using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class GameStatePlaying: GameState
    {
        private World _world;

        private List<GameState> _game_state_queue = new List<GameState>();

        public GameStatePlaying(World w)
        {
            _world = w;
            w.PlayingState = this;
        }

        public override void Update()
        {
            if (_game_state_queue.Count > 0)
            {
                TheGame.Instance.ChangeState(_game_state_queue[0]);
                _game_state_queue.RemoveAt(0);
            }
            else
            {
                _world.Step();
            }
        }

        public override void HandleInput()
        {
            if (_game_state_queue.Count == 0)
                _world.HandleInput();
        }

        public override void Draw()
        {
            _world.Draw();
        }

        public void QueueState(GameState s)
        {
            _game_state_queue.Add(s);
        }
    }
}
