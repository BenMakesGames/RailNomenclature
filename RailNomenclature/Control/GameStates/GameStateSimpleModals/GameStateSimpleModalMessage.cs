using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace RailNomenclature
{
    public class GameStateSimpleModalMessage: GameStateSimpleModal
    {
        private List<string> _messages = new List<string>();
        private int _message_pixel_width, _message_pixel_height;
        private string _title;

        public GameStateSimpleModalMessage(GameState previousState, string title, string message): base(previousState)
        {
            _title = title;
            _messages.Add(message.MaxWidth(TheGame.WIDTH / 2 / 9));

            UpdateMessageDimensions();
        }

        public GameStateSimpleModalMessage(GameState previousState, string title, List<string> messages): base(previousState)
        {
            _title = title;

            foreach (string m in messages)
                _messages.Add(m.MaxWidth(TheGame.WIDTH / 2 / 9));

            UpdateMessageDimensions();
        }

        protected override bool NextMessage()
        {
            if (_messages.Count == 1)
                return false;
            else
            {
                _messages.RemoveAt(0);
                UpdateMessageDimensions();
                return true;
            }
        }

        private void UpdateMessageDimensions()
        {
            _message_pixel_width = _messages[0].Width() * 9;
            _message_pixel_height = _messages[0].Height() * 16 + (_title == null ? 0 : 24);
        }

        protected override void DrawModal()
        {
            Assets.WhitePixel.DrawRectangle((TheGame.WIDTH - _message_pixel_width) / 2 - 10, (TheGame.HEIGHT - _message_pixel_height) / 2 - 10, _message_pixel_width + 20, _message_pixel_height + 20, new RGBA(0, 0, 0, 192));

            if (_title != null)
            {
                Assets.Fonts[FontID.Consolas16].WriteText((TheGame.WIDTH - _message_pixel_width) / 2, (TheGame.HEIGHT - _message_pixel_height) / 2, _title + ":", RGBA.Gray);
                Assets.Fonts[FontID.Consolas16].WriteText((TheGame.WIDTH - _message_pixel_width) / 2, (TheGame.HEIGHT - _message_pixel_height) / 2 + 24, _messages[0], RGBA.White);
            }
            else
                Assets.Fonts[FontID.Consolas16].WriteText((TheGame.WIDTH - _message_pixel_width) / 2, (TheGame.HEIGHT - _message_pixel_height) / 2, _messages[0], RGBA.White);
        }
    }
}
