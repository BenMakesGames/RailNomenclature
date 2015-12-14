using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class NotSwitch: Switch
    {
        private Switch _s;

        public NotSwitch(Switch s): base(null, 0, 0, 0, 0)
        {
            _s = s;
        }

        public override string Name()
        {
            return "NOT Switch";
        }

        public override bool IsActivated()
        {
            return !_s.IsActivated();
        }

        public override void Draw(Camera c)
        {
            throw new NotImplementedException();
        }
    }
}
