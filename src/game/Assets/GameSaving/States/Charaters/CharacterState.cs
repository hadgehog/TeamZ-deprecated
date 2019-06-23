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
        public virtual int PunchDamage
		{
            get;
            set;
        }

		[Index(3)]
		public virtual int KickDamage
		{
			get;
			set;
		}

        [Index(4)]
        public virtual string Name
        {
            get;
            set;
        }
    }
}
