using ZeroFormatter;

namespace GameSaving.States
{
    public enum MonoBehaviourStateKind
    {
        Prefab
    }

    [Union(typeof(PrefabState))]
    public abstract class MonoBehaviourState
    {
        [UnionKey]
        public abstract MonoBehaviourStateKind Type { get; }
    }
}