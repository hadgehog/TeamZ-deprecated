using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSaving.MonoBehaviours;
using Helpers;
using UniRx;
using UnityEngine;
using static GameSaving.MonoBehaviours.Entity;

namespace GameSaving
{
    public class EntitiesStorage
    {
        public EntitiesStorage()
        {
            this.Entities = new ReactiveDictionary<Guid, Entity>();
            MessageBroker.Default.Receive<EntityDestroyed>().Subscribe(o => this.Entities.Remove(o.Entity.Id));
        }

        public GameObject Root
        {
            get;
            set;
        }

        public ReactiveDictionary<Guid, Entity> Entities
        {
            get;
        }
    }
}
