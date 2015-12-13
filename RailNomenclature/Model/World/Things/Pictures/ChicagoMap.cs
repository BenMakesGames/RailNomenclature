﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class RailMap: Picture
    {
        public RailMap(Room r, int x, int y)
            : base(r, x, y, 25, 20, RGBA.White, SpriteSheetID.RAIL_MAP, "Map")
        {
        }

        public override void DoPrimaryAction(Thing a)
        {
            base.DoPrimaryAction(a);

            Location.World.SetQuestValue(World.QUEST_PLAYER_SAW_RAIL_MAP, 1);
        }
    }
}
