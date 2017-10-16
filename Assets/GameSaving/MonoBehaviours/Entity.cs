using System;
using GameSaving.States;

namespace GameSaving.MonoBehaviours
{
    public class Entity : MonoBehaviourWithState<EntityState>
    {
        public Guid Id = Guid.NewGuid();
        public Guid LevelId;
        public string Path;

        public override EntityState GetState()
        {
            var cachedTransform = this.transform;
            return new EntityState
            {
                Id = this.Id,
                LevelId = this.LevelId,
                Path = this.Path,
                Position = cachedTransform.localPosition,
                Rotation = cachedTransform.localRotation,
                Scale = cachedTransform.localScale,
            };
        }

        public override void SetState(EntityState state)
        {
            this.Id = state.Id;
            this.Path = state.Path;
            this.LevelId = state.LevelId;

            var cachedTransform = this.transform;
            cachedTransform.localPosition = state.Position;
            cachedTransform.localRotation = state.Rotation;
            cachedTransform.localScale = state.Scale;
        }
    }
}