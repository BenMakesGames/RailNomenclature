using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class MoveTargetThingAction: MoveTarget
    {
        private Thing _target;
        private string _action;

        public MoveTargetThingAction(Room r, Character actor, Thing target, string action)
            : base(r, actor, target.X(), target.Y())
        {
            _target = target;
            _action = action;
        }

        public override void Step()
        {
            base.Step();

            if(X() != _target.X() || Y() != _target.Y())
                MoveTo(_target);
        }

        public override void OnBlocked()
        {
            TryTargetAction();
        }

        public override void OnArrive()
        {
            TryTargetAction();
        }

        protected void TryTargetAction()
        {
            if (_target.PrimaryAction() == _action)
                Location.World.TryPrimaryAction(_user, _target);
            else if (_target.SecondaryAction() == _action)
                Location.World.TrySecondaryAction(_user, _target);
        }
    }
}
