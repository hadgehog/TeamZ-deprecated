using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSaving;
using GameSaving.MonoBehaviours;
using TeamZ.Assets.Code.DependencyInjection;
using UnityEngine;

namespace UnityEngine
{
    public static class SpawnExtentions
    {
        static Dependency<LevelManager> LevelManager;
        static Dependency<EntitiesStorage> EntitiesStorage;

        public static Entity Spawn(this GameObject template, Vector3 localLocation, Transform parent)
        {
            var character = GameObject.Instantiate(template);

            if (parent)
            {
                character.transform.SetParent(parent, false);
            }

            character.transform.localPosition = localLocation;

            var entity = character.GetComponent<Entity>();
            entity.LevelId = LevelManager.Value.CurrentLevel.Id;
            EntitiesStorage.Value.Entities.Add(entity.Id, entity);

            return entity;
        }
    }
}
