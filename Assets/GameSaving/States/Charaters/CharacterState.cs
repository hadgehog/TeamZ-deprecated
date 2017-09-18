using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace GameSaving.States.Charaters
{
    public abstract class CharacterState : MonoBehaviourState
    {
        [Index(0)]
        public virtual int Health
        {
            get;
            set;
        }

        [Index(1)]
        public virtual int Armor
        {
            get;
            set;
        }

        [Index(2)]
        public virtual int Damage
        {
            get;
            set;
        }
    }
}
