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
        LevelObject,
	}

    [ZeroFormattable]
    [Union(typeof(EntityState),
           typeof(CameraState),
           typeof(LizardState),
           typeof(CharacterControllerState),
		   typeof(LevelObjectState))]
    public abstract class MonoBehaviourState
    {
        [UnionKey]
        public abstract MonoBehaviourStateKind Type { get; }
    }
}