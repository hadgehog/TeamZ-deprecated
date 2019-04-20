using System.Collections.Generic;
using System.Linq;
using GameSaving.Interfaces;
using UnityEngine;
using ZeroFormatter;

namespace GameSaving.States
{
    [ZeroFormattable]
    public class GameObjectState
    {
        [Index(0)]
        public virtual EntityState Entity
        {
            get;
            set;
        }

        [Index(1)]
        public virtual IEnumerable<MonoBehaviourState> MonoBehaviousStates
        {
            get;
            set;
        }

        public GameObjectState()
        {
        }

        public GameObjectState SetGameObject(GameObject gameObject)
        {
            var states = gameObject.GetComponents<IMonoBehaviourWithState>().
                                    Select(o => (MonoBehaviourState)o.GetState()).ToList();

            this.Entity = (EntityState)states.First(o => o.Type == MonoBehaviourStateKind.Entity);
            states.Remove(this.Entity);
            this.MonoBehaviousStates = states;

            return this;
        }
    }
}