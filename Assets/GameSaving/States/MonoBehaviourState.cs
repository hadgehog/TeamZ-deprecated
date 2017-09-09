using ZeroFormatter;

namespace GameSaving.States
{
    public enum MonoBehaviourStateKind
    {
        Prefab,
        Camera
    }

    [Union(
        typeof(PrefabState), 
        typeof(CameraState))]
    public abstract class MonoBehaviourState
    {
        [UnionKey]
        public abstract MonoBehaviourStateKind Type { get; }
    }
}