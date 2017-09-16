using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSaving.MonoBehaviours;
using Helpers;
using UnityEngine;

namespace GameSaving
{
    public class EntitiesStorage : Singletone<EntitiesStorage>
    {
        public EntitiesStorage()
        {
            this.Entities = new Dictionary<Guid, Entity>();
        }

        public GameObject Root
        {
            get;
            set;
        }

        public Dictionary<Guid, Entity> Entities
        {
            get;
        }
    }
}
