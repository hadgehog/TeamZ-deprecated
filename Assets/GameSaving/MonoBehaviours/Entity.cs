using System;
using GameSaving.States;
using UniRx;
using UnityEngine.AddressableAssets;

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
                AssetGuid = this.Path,
                Position = cachedTransform.localPosition,
                Rotation = cachedTransform.localRotation,
                Scale = cachedTransform.localScale,
            };
        }

        public override void SetState(EntityState state)
        {
            this.Id = state.Id;
            this.Path = state.AssetGuid;
            this.LevelId = state.LevelId;

            var cachedTransform = this.transform;
            cachedTransform.localPosition = state.Position;
            cachedTransform.localRotation = state.Rotation;
            cachedTransform.localScale = state.Scale;
        }

        public class EntityDestroyed
        {
            public Entity Entity { get; set; }
        }

        private void OnDestroy()
        {
            MessageBroker.Default.Publish(new EntityDestroyed { Entity = this } );
        }
    }

}