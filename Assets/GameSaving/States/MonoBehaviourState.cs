using GameSaving.States.Charaters;
using TeamZ.Assets.GameSaving.States;
using ZeroFormatter;

namespace GameSaving.States
{
    public enum MonoBehaviourStateKind
    {
        Entity,
        Camera,
        Lizard,
        CharacterController,
    }

    [ZeroFormattable]
    [Union(typeof(EntityState),
           typeof(CameraState),
           typeof(LizardState),
           typeof(CharacterControllerState))]
    public abstract class MonoBehaviourState
    {
        [UnionKey]
        public abstract MonoBehaviourStateKind Type { get; }
    }
}