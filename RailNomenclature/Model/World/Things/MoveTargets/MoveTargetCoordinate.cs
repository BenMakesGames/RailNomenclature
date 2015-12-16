using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class MoveTargetCoordinate: MoveTarget
    {
        public MoveTargetCoordinate(Room r, Character actor, int x, int y)
            : base(r, actor, x, y)
        {
        }
    }
}
