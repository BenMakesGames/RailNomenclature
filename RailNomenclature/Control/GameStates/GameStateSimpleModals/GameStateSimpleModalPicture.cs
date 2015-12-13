using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace RailNomenclature
{
    public class GameStateSimpleModalPicture: GameStateSimpleModal
    {
        private GameState _previous_state;
        private SpriteSheetID _picture;
        private int _message_pixel_width, _message_pixel_height;

        public GameStateSimpleModalPicture(GameState previousState, SpriteSheetID picture): base(previousState)
        {
            _previous_state = previousState;
            _picture = picture;

            UpdateMessageDimensions();
        }

        private void UpdateMessageDimensions()
        {
            _message_pixel_width = Assets.SpriteSheets[_picture].SpriteWidth;
            _message_pixel_height = Assets.SpriteSheets[_picture].SpriteHeight;
        }

        protected override void DrawModal()
        {
            //Assets.WhitePixel.DrawRectangle((TheGame.WIDTH - _message_pixel_width) / 2 - 10, (TheGame.HEIGHT - _message_pixel_height) / 2 - 10, _message_pixel_width + 20, _message_pixel_height + 20, new RGBA(0, 0, 0, 192));
            Assets.SpriteSheets[_picture].Draw(0, (TheGame.WIDTH - _message_pixel_width) / 2, (TheGame.HEIGHT - _message_pixel_height) / 2);
        }

        override protected bool NextMessage()
        {
            return false;
        }
    }
}
