using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class HomeBase: TeleportStation
    {
        private ProfessorRed _professor;

        public HomeBase(Room r, int x, int y, ProfessorRed professor)
            : base(r, x, y, "Home Base")
        {
            _professor = professor;
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

        public override void DoPrimaryAction(Thing a)
        {
            if (_professor.HasGivenIntroductionTalk())
                base.DoPrimaryAction(a);
            else
                a.Notify(null, "But there's no telling where it might go...");
        }
    }
}
