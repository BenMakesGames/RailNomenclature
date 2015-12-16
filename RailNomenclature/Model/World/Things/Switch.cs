using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    abstract public class Switch : Thing
    {
        abstract public bool IsActivated();

        public Switch(Room r, int x, int y, int w, int h, string description = "")
            : base(r, x, y, w, h, description)
        {
        }
    }
}
