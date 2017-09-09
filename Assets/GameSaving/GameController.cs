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

        public GameStorage<TGameState> Storage
        {
            get;
        }

        public GameController()
        {
            this.Storage = new GameStorage<TGameState>();
            this.loaded = new Subject<Unit>();
        }

        public async void Boostrap()
        {
            await this.SaveAsync("temp");
            await this.LoadAsync("temp");
        }

        public async Task LoadAsync(string slotName)
        {
            var gameState = await this.Storage.LoadAsync(slotName);

            Time.timeScale = 0;
            this.CleanupLevel();

            this.InstantiateGameState(gameState);

            GC.Collect();
            Time.timeScale = 1;
        }

        public async Task SaveAsync(string slotName)
        {
            var gameState = new TGameState();

            Time.timeScale = 0;
            gameState.GameObjectsStates = GameObject.FindObjectsOfType<PrefabMonoBehaviour>().
                Select(o => new GameObjectState().SetGameObject(o.gameObject)).ToList();
            Time.timeScale = 1;

            await this.Storage.SaveAsync(gameState, slotName);
        }

        private void CleanupLevel()
        {
            foreach (var gameObject in GameObject.FindObjectsOfType<PrefabMonoBehaviour>().Select(o => o.gameObject))
            {
                GameObject.Destroy(gameObject);
            }
        }

        private void InstantiateGameState(TGameState gameState)
        {
            var cache = new Dictionary<string, GameObject>();
            var monoBehaviours = new LinkedList<IMonoBehaviourWithState>();
            foreach (var gameObjectState in gameState.GameObjectsStates)
            {
                var prefabInformation = gameObjectState.MonoBehaviousStates.OfType<PrefabState>().First();
                if (!cache.ContainsKey(prefabInformation.Path))
                {
                    cache.Add(prefabInformation.Path, Resources.Load<GameObject>(prefabInformation.Path));
                }

                var gameObject = GameObject.Instantiate<GameObject>(cache[prefabInformation.Path]);

                foreach (var monoBehaviour in gameObject.GetComponents<IMonoBehaviourWithState>())
                {
                    var stateType = monoBehaviour.GetStateType();
                    var monoBehaviourState = gameObjectState.MonoBehaviousStates.First(o => stateType.IsInstanceOfType(o));
                    monoBehaviour.SetState(monoBehaviourState);

                    monoBehaviours.AddLast(monoBehaviour);
                }
            }

            foreach (var monoBehaviour in monoBehaviours)
            {
                monoBehaviour.Loaded();
            }

            this.loaded.OnNext(Unit.Default);
        }
    }
}