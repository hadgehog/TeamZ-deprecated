using System;
using GameSaving.States;

namespace GameSaving.MonoBehaviours
{
    public class PrefabMonoBehaviour : MonoBehaviourWithState<PrefabState>
    {
        public Guid Id;

        public string Path;

        public override PrefabState GetState()
        {
            var cachedTransform = this.transform;
            return new PrefabState
            {
                Id = this.Id,
                Path = this.Path,
                Position = cachedTransform.localPosition,
                Rotation = cachedTransform.localRotation,
                Scale = cachedTransform.localScale,
            };
        }

        public override void SetState(PrefabState state)
        {
            this.Id = state.Id;
            this.Path = state.Path;

            var cachedTransform = this.transform;
            cachedTransform.localPosition = state.Position;
            cachedTransform.localRotation = state.Rotation;
            cachedTransform.localScale = state.Scale;
        }
    }
}