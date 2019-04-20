using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace GameSaving.States.Charaters
{
    [ZeroFormattable]
    public class LizardState : CharacterState
    {
        public override MonoBehaviourStateKind Type
        {
            get
            {
                return MonoBehaviourStateKind.Lizard;
            }
        }
    }
}