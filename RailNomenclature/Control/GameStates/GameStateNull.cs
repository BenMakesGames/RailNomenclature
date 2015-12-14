using System;
using Microsoft.Xna.Framework;

namespace RailNomenclature
{
    public class GameStateNull: GameState
    {
        public override void Draw()
        {
            TheGame.Instance.GraphicsDevice.Clear(Color.DarkSeaGreen);
        }

        public override void Update()
        {
        }

        public override void HandleInput()
        {
        }
    }
}
