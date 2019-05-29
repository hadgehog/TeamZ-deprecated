using ZeroFormatter;

namespace GameSaving.States.Charaters
{
    [ZeroFormattable]
    public class HedgehogState : CharacterState
    {
        public override MonoBehaviourStateKind Type
        {
            get
            {
                return MonoBehaviourStateKind.Hedgehog;
            }
        }
    }
}