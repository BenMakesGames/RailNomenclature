using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class RailMap : Picture
    {
        public RailMap(Room r, int x, int y)
            : base(r, x, y, 25, 28, -28, SpriteSheetID.RAIL_MAP_ICON, SpriteSheetID.RAIL_MAP, "Map")
        {
        }

        public override void DoPrimaryAction(Thing a)
        {
            if (a.DistanceTo(this) > 100)
                a.Notify(null, "Some kind of map. It's impossible to make out details from this distance.");
            else
            {
                base.DoPrimaryAction(a);
                Location.World.SetQuestValue(World.QUEST_PLAYER_SAW_RAIL_MAP, 1);
            }
        }
    }
}
