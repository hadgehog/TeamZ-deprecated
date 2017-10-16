using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSaving.Interfaces;
using GameSaving.MonoBehaviours;
using GameSaving.States;
using UniRx;
using UnityEngine;

namespace GameSaving
{
    public class GameController<TGameState>
        where TGameState : GameState, new()
    {
        private UniRx.Subject<Unit> loaded;

        public UniRx.IObservable<Unit> Loaded
        {
            get
            {
                return this.loaded;
            }
        }

        public EntitiesStorage EnttiesStorage
        {
            get
            {
                return EntitiesStorage.Instance;
            }
        }

        public GameStorage<TGameState> Storage
        {
            get;
        }

        public GameController()
        {
            this.Storage = new GameStorage<TGameState>();
            this.loaded = new Subject<Unit>();
            this.EnttiesStorage.Root = GameObject.Find("Root");
            this.EnttiesStorage.Entities.Clear();
        }

        public async void Boostrap()
        {
            foreach (var entity in GameObject.FindObjectsOfType<Entity>())
            {
                this.EnttiesStorage.Entities.Add(entity.Id, entity);
            };

            await this.SaveAsync("temp");
            await this.LoadAsync("temp");
        }

        public async Task LoadAsync(string slotName)
        {
            var gameState = await this.Storage.LoadAsync(slotName);

            this.CleanupLevel();

            this.EnttiesStorage.Root = new GameObject("Root");
            this.EnttiesStorage.Root.SetActive(false);

            this.InstantiateGameState(gameState);

            GC.Collect();

            this.EnttiesStorage.Root.SetActive(true);
        }

        public async Task SaveAsync(string slotName)
        {
            var gameState = new TGameState();

            Time.timeScale = 0;
            gameState.GameObjectsStates = this.EnttiesStorage.Entities.Values.Select(o => new GameObjectState().SetGameObject(o.gameObject)).ToList();
            Time.timeScale = 1;

            await this.Storage.SaveAsync(gameState, slotName);
        }

        private void CleanupLevel()
        {
            if (this.EnttiesStorage.Root)
            {
                this.EnttiesStorage.Entities.Clear();
                GameObject.Destroy(this.EnttiesStorage.Root);
            }
            else
            {
#if UNITY_EDITOR
                var entities = GameObject.FindObjectsOfType<Entity>();
                if (entities.Any())
                {
                    Debug.LogError("Move all entities to \"Root\" gameObject!");
                    Debug.LogError("There are next entities outside:");
                    foreach (var entity in entities)
                    {
                        Debug.LogError(entity.gameObject.name);
                    }
                }
#endif
            }
        }

        private void InstantiateGameState(TGameState gameState)
        {
            var cache = new Dictionary<string, GameObject>();
            var monoBehaviours = new LinkedList<IMonoBehaviourWithState>();

            foreach (var gameObjectState in gameState.GameObjectsStates)
            {
                var prefabInformation = gameObjectState.MonoBehaviousStates.OfType<EntityState>().First();
                if (!cache.ContainsKey(prefabInformation.Path))
                {
                    cache.Add(prefabInformation.Path, Resources.Load<GameObject>(prefabInformation.Path));
                }

                var gameObject = GameObject.Instantiate<GameObject>(cache[prefabInformation.Path], this.EnttiesStorage.Root.transform);

                foreach (var monoBehaviour in gameObject.GetComponents<IMonoBehaviourWithState>())
                {
                    var stateType = monoBehaviour.GetStateType();
                    var monoBehaviourState = gameObjectState.MonoBehaviousStates.First(o => stateType.IsInstanceOfType(o));
                    monoBehaviour.SetState(monoBehaviourState);
                    monoBehaviours.AddLast(monoBehaviour);
                }

                var prefab = gameObject.GetComponent<Entity>();
                this.EnttiesStorage.Entities.Add(prefab.Id, prefab);
            }

            foreach (var monoBehaviour in monoBehaviours)
            {
                monoBehaviour.Loaded();
            }

            this.loaded.OnNext(Unit.Default);
        }
    }
}