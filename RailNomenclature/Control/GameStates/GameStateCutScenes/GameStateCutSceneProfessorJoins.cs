using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class GameStateCutSceneProfessorJoins: GameStateCutScene
    {
        private ThePlayer _player;
        private ProfessorRed _professor;

        private int _initial_x, _initial_y;

        public GameStateCutSceneProfessorJoins(ThePlayer player, ProfessorRed professor)
            : base()
        {
            _player = player;
            _professor = professor;
            
            _initial_x = (int)_player.X();
            _initial_y = (int)_player.Y();
        }

        public override void EnterState()
        {
 	        base.EnterState();
        }

        public override void Update()
        {
            if (_step == 0)
            {
                _player.SetPath(_player.X() + 100, _player.Y() - 50);
            }
            else if (_step == TheGame.FPS / 2)
            {
                _professor.MoveTo(_player.Location, _initial_x, _initial_y);
                _professor.SetPath(_initial_x + 50, _initial_y - 100);
            }

            else if (_step == TheGame.FPS * 3 / 2)
            {
                _player.Notify(_professor.Name(), new List<string>() {
                    "What's all this?! An actual rail platform!?",
                    "It's fantastic! Most of the other sites found so far have been completely burried in hardened volcanic ash, but this..."
                });
            }
            else if (_step == TheGame.FPS * 3 / 2 + 5)
            {
                _professor.SetPath(_professor.X() + 140, _professor.Y() - 20);
            }
            else if (_step == TheGame.FPS * 5 / 2)
            {
                _player.Notify(_professor.Name(), new List<string>() {
                    "This door is sealed, or barred, or something... but if I'm not mistaken, that switch over there...",
                    "Ah! This is too exciting!",
                    "Let's look around!",
                    "And if you need my help with anything, don't hesitate to ask."
                });
            }
            else if (_step == TheGame.FPS * 5 / 2 + 5)
            {
                _professor.SetPath(_professor.X() - 20, _professor.Y() + 20);
            }
            else if (_step == TheGame.FPS * 5 / 2 + 30)
            {
                _professor.BeAstounded();
                EndCutScene();

                _player.Location.World.SetQuestValue(World.QUEST_CAN_SWITCH_BETWEEN_CHARACTERS, 1);

                _player.Notify(null, "(You can now switch which character you control! Use the spinning arrow icon in the left-hand bar!)");
            }

            base.Update();
        }
    }
}
