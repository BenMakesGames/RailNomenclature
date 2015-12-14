using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class AndSwitch: Switch
    {
        private Switch _s1, _s2;

        public AndSwitch(Switch s1, Switch s2): base(null, 0, 0, 0, 0)
        {
            _s1 = s1;
            _s2 = s2;
        }

        public override string Name()
        {
            return "AND Switch";
        }

        public override bool IsActivated()
        {
            return _s1.IsActivated() && _s2.IsActivated();
        }

        public override void Draw(Camera c)
        {
            throw new NotImplementedException();
        }
    }
}
