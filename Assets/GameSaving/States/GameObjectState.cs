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
            this.MonoBehaviousStates = gameObject.GetComponents<IMonoBehaviourWithState>().
                                    Select(o => (MonoBehaviourState)o.GetState()).ToList();

            return this;
        }
    }
}