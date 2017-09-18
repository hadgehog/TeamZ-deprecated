using ZeroFormatter;

namespace GameSaving.States
{
    public enum MonoBehaviourStateKind
    {
        Entity,
        Camera
    }

    [Union(
        typeof(EntityState), 
        typeof(CameraState))]
    public abstract class MonoBehaviourState
    {
        [UnionKey]
        public abstract MonoBehaviourStateKind Type { get; }
    }
}