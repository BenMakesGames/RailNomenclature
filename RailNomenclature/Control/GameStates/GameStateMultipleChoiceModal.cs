using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    class GameStateMultipleChoiceModal: GameState
    {
        public delegate void ChoiceDelegate(Thing actor);

        private string _title;
        private List<string> _options = new List<string>();
        private List<ChoiceDelegate> _delegates;

        private GameState _previous_state;

        private int _message_pixel_width, _message_pixel_height;
        private int _x, _y;

        private int _selection = 0;

        private Thing _target;

        public GameStateMultipleChoiceModal(Thing t, string title, List<string> options, List<ChoiceDelegate> delegates)
            : base()
        {
            _target = t;
            _title = title;
            _delegates = delegates;

            foreach (string o in options)
                _options.Add(o.MaxWidth(TheGame.WIDTH / 2 / 9));

            UpdateMessageDimensions();

            _previous_state = TheGame.Instance.CurrentState;
        }

        private void UpdateMessageDimensions()
        {
            _message_pixel_height = 24;

            int largestWidth = _title.Width() * 9;

            foreach (string option in _options)
            {
                _message_pixel_height += option.Height() * 16;

                if (option.Width() * 9 > largestWidth)
                    largestWidth = option.Width() * 9;
            }

            _message_pixel_width = largestWidth;

            _x = (TheGame.WIDTH - _message_pixel_width) / 2;
            _y = (TheGame.HEIGHT - _message_pixel_height) / 2;

        }

        public override void Draw()
        {
            Assets.WhitePixel.DrawRectangle(_x - 10, _y - 10, _message_pixel_width + 20, _message_pixel_height + 20, new RGBA(0, 0, 0, 192));

            Assets.Fonts[FontID.Consolas16].WriteText(_x, _y, _title, RGBA.Gray);

            Assets.WhitePixel.DrawRectangle(_x, _y + 24 + _selection * 16, _message_pixel_width, 16, RGBA.White, 0.4f);

            for(int line = 0; line < _options.Count; line++)
            {
                Assets.Fonts[FontID.Consolas16].WriteText(_x, _y + 24 + line * 16, _options[line], RGBA.White);
            }
        }

        public override void Update()
        {
            if (MouseHandler.Instance.X() >= _x && MouseHandler.Instance.X() < _x + _message_pixel_width)
            {
                if (MouseHandler.Instance.Y() >= _y + 24 && MouseHandler.Instance.Y() < _y + 24 + _options.Count * 16)
                {
                    _selection = (MouseHandler.Instance.Y() - (_y + 24)) / 16;

                    if (MouseHandler.Instance.IsLeftClicking(true))
                    {
                        TheGame.Instance.ChangeState(_previous_state);
                        _delegates[_selection](_target);
                    }
                }
            }
        }
    }
}
