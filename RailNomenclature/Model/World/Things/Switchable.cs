using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    abstract public class Switchable: Thing
    {
        private Switch _switch;

        public Switchable(Room r, int x, int y, int w, int h, string description = "")
            : base(r, x, y, w, h, description)
        {
        }

        public void AttachSwitch(Switch s) { _switch = s; }

        public bool IsOn()
        {
            return _switch == null || _switch.IsActivated();
        }
    }
}
