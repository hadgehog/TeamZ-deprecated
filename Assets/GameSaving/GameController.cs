using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSaving.Interfaces;
using GameSaving.MonoBehaviours;
using GameSaving.States;
using UniRx;
using UnityEngine;
using Effects;

namespace GameSaving
{
    public class GameController<TGameState> : IGameController 
        where TGameState : GameState, new()
    {
        private Subject<Unit> loaded;
        private BlackScreen loadingEffect;

        public IObservable<Unit> Loaded
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

        public LevelManager LevelManager
        {
            get;
        }

        public GameController()
        {
            this.Storage = new GameStorage<TGameState>();
            this.LevelManager = new LevelManager();
            this.loaded = new Subject<Unit>();
            this.loadingEffect = GameObject.FindObjectOfType<BlackScreen>();
            this.EnttiesStorage.Root = null;
            this.EnttiesStorage.Entities.Clear();
        }

        public void BootstrapFromEditor()
        {
            var levelBootstraper = GameObject.FindObjectOfType<LevelBootstraper>();
            this.LevelManager.CurrentLevel = Level.All.First(o => o.Name == levelBootstraper.LevelName);
        }

        public void Boostrap()
        {
            foreach (var entity in GameObject.FindObjectsOfType<Entity>())
            {
                entity.LevelId = this.LevelManager.CurrentLevel.Id;
                this.EnttiesStorage.Entities.Add(entity.Id, entity);
            };

            //await this.SaveAsync("temp");
            //await this.LoadAsync("temp");
        }

        public async Task LoadAsync(string slotName)
        {
            await this.loadingEffect.ShowAsync();

            var gameState = await this.Storage.LoadAsync(slotName);

            await this.LevelManager.Load(Level.All.First(o => o.Id == gameState.LevelId));
            this.EnttiesStorage.Root = GameObject.Find("Root");

            this.CleanupLevel();

            this.EnttiesStorage.Root = new GameObject("Root");
            this.EnttiesStorage.Root.SetActive(false);

            this.InstantiateGameState(gameState);
            this.Boostrap();

            GC.Collect();

            this.EnttiesStorage.Root.SetActive(true);

            await this.loadingEffect.HideAsync();
        }

        public async Task SaveAsync(string slotName)
        {
            var gameState = new TGameState();

            Time.timeScale = 0;
            gameState.LevelId = this.LevelManager.CurrentLevel.Id;
            gameState.GameObjectsStates = this.EnttiesStorage.Entities.Values.Select(o => new GameObjectState().SetGameObject(o.gameObject)).ToList();

            Time.timeScale = 1;

            await this.Storage.SaveAsync(gameState, slotName);
        }

        private void CleanupLevel()
        {
            this.EnttiesStorage.Entities.Clear();
            GameObject.Destroy(this.EnttiesStorage.Root);
        }

        private void InstantiateGameState(TGameState gameState)
        {
            var cache = new Dictionary<string, GameObject>();
            var monoBehaviours = new LinkedList<IMonoBehaviourWithState>();

            foreach (var gameObjectState in gameState.GameObjectsStates.Where(o => o.Entity.LevelId == this.LevelManager.CurrentLevel.Id))
            {
                var entityInfotration = gameObjectState.Entity;
                if (!cache.ContainsKey(entityInfotration.Path))
                {
                    cache.Add(entityInfotration.Path, Resources.Load<GameObject>(entityInfotration.Path));
                }

                var gameObject = GameObject.Instantiate<GameObject>(cache[entityInfotration.Path], this.EnttiesStorage.Root.transform);

                var states = gameObjectState.MonoBehaviousStates.ToList();
                states.Add(entityInfotration);

                foreach (var monoBehaviour in gameObject.GetComponents<IMonoBehaviourWithState>())
                {
                    var stateType = monoBehaviour.GetStateType();
                    var monoBehaviourState = states.First(o => stateType.IsInstanceOfType(o));
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