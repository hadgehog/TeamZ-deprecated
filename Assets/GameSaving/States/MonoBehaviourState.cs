using GameSaving.States.Charaters;
using ZeroFormatter;

namespace GameSaving.States
{
    public enum MonoBehaviourStateKind
    {
        Entity,
        Camera,
        Lizard,
    }

    [ZeroFormattable]
    [Union(typeof(EntityState),
           typeof(CameraState),
           typeof(LizardState))]
    public abstract class MonoBehaviourState
    {
        [UnionKey]
        public abstract MonoBehaviourStateKind Type { get; }
    }
}