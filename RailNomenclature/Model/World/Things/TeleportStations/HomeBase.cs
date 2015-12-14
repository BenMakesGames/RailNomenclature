using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class HomeBase: TeleportStation
    {
        public HomeBase(Room r, int x, int y)
            : base(r, x, y, 30, 40, "Home Base")
        {
        }

        public override string Name() { return "Home Base"; }

        public override string SecondaryAction()
        {
            return "";
        }
        
        public override void DoSecondaryAction(Thing a)
        {
            // nothing
        }
    }
}
