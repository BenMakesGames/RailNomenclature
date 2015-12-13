using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class GameStateLeftPanel: GameState
    {
        public const int BASE_WIDTH = 84;

        private int _selected = -1;
        public int X { get; private set; }
        private World _world;

        private LeftPanelObject _panel_object;

        public const int OPTION_NOTEPAD = 0;
        public const int OPTION_TELEPORTER = 1;

        private List<int> _options = new List<int>();

        public GameStateLeftPanel(World w)
            : base()
        {
            X = -44;
            _world = w;

            RecalculateOptions();
        }

        public bool IsMouseOnUIElement()
        {
            return MouseHandler.Instance.X() < X + Width();
        }

        public int Width()
        {
            return BASE_WIDTH + (_panel_object == null ? 0 : _panel_object.Width());
        }

        public override void Update()
        {
            _selected = -1;

            if (MouseHandler.Instance.X() < X + BASE_WIDTH)
            {
                if (MouseHandler.Instance.Y() >= 10 && MouseHandler.Instance.Y() < 10 + _options.Count * 64) // within range of any icons?
                    _selected = (MouseHandler.Instance.Y() - 10) / 64; // which icon?
            }

            if (MouseHandler.Instance.X() < X + Width())
            {
                if (X < 0) X += 4;
            }
            else
            {
                if (X > -44) X -= 4;

                _panel_object = null;
            }

            if (MouseHandler.Instance.IsLeftClicking(true) && _selected >= 0)
            {
                switch (_options[_selected])
                {
                    case OPTION_NOTEPAD: ChangeState(new GameStateNotePad(this)); break;
                    case OPTION_TELEPORTER:
                        new TeleportStation(_world.ActiveCharacter.Location, (int)_world.ActiveCharacter.X(), (int)_world.ActiveCharacter.Y() - 4);
                        _world.ActiveCharacter.TeleportStations--;
                        break;
                }
            }

            RecalculateOptions();
        }

        private void RecalculateOptions()
        {
            _options.Clear();

            _options.Add(OPTION_NOTEPAD);

            if (_world.ActiveCharacter.TeleportStations > 0)
                _options.Add(OPTION_TELEPORTER);
        }

        public override void Draw()
        {
            Assets.WhitePixel.DrawRectangle(X, 0, BASE_WIDTH, TheGame.HEIGHT, RGBA.Black, 0.6f);

            for(int i = 0; i < _options.Count; i++)
                Assets.SpriteSheets[SpriteSheetID.UI_LEFT_PANEL_ICONS].Draw(_options[i], 10 + X, 10 + i * 64, RGBA.White, _selected == i ? 1f : 0.3f);

            if(_panel_object != null)
                _panel_object.Draw();
        }

        private void FocusedDraw()
        {
        }

        private void ChangeState(LeftPanelObject s)
        {
            if (_panel_object != null && _panel_object.GetType() == s.GetType())
                _panel_object = null;
            else
                _panel_object = s;
        }
    }
}
