using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class ThePlayer: Character
    {
        public ThePlayer(Room r, int x, int y): base(r, x, y, 10, 44, "Me", "It's me!")
        {
            ShirtColor = Microsoft.Xna.Framework.Color.Orange;
            PantsColor = Microsoft.Xna.Framework.Color.DarkBlue;
        }

        public override void DoSecondaryAction(Thing a)
        {
            if (a is Character)
                a.DoSecondaryAction(this);
        }
    }
}
