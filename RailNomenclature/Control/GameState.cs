using System;
using Microsoft.Xna.Framework;

namespace RailNomenclature
{
    public abstract class GameState
    {
        virtual public void EnterState() { }
        virtual public void LeaveState() { }

        abstract public void Update();
        abstract public void Draw();
    }
}
