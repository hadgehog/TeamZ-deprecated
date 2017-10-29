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
using GameSaving.States.Charaters;
using GameObjects.Levels;

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

        public async void SwitchLevelAsync(Level level, string locationName)
        {
            var autoSaveSlot = $"Autosave [{level.Name}-{DateTime.Now.ToString("dd_MMMM_yyyy")}]";
            var gameState = this.GetState();
            await this.SaveAsync(gameState, autoSaveSlot);

            gameState.LevelId = level.Id;
            var mainCharacters = gameState.GameObjectsStates.
                Where(o => o.MonoBehaviousStates.OfType<CharacterState>().Any());

            foreach (var character in mainCharacters)
            {
                character.Entity.LevelId = level.Id;
            }

            await this.loadingEffect.ShowAsync();
            Time.timeScale = 0;

            await this.LoadAsync(gameState);

            // TODO: Think about how set position before scene loading.
            var locationPosition = GameObject.FindObjectsOfType<Location>().
                First(o => o.name == locationName).transform.position;

            foreach (var character in this.EnttiesStorage.Entities.Values.Where(o => o.GetComponent<ICharacter>() != null))
            {
                character.transform.localPosition = locationPosition;
            }

            Time.timeScale = 1;
            await this.loadingEffect.HideAsync();
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
            this.LevelManager.CurrentLevel = Level.All[levelBootstraper.LevelName];
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
            await this.LoadAsync(await this.Storage.LoadAsync(slotName));
            await this.loadingEffect.HideAsync();
        }

        public async Task LoadAsync(TGameState gameState)
        {
            await this.LevelManager.LoadAsync(Level.All.First(o => o.Value.Id == gameState.LevelId).Value);

            this.CleanupLevel();

            this.EnttiesStorage.Root = new GameObject("Root");
            this.EnttiesStorage.Root.SetActive(false);

            this.RestoreGameState(gameState);

            GC.Collect();

            this.EnttiesStorage.Root.SetActive(true);
            this.loaded.OnNext(Unit.Default);
        }

        public async Task SaveAsync(string slotName)
        {
            await this.SaveAsync(this.GetState(), slotName);
        }

        public async Task SaveAsync(TGameState gameState, string slotName)
        {
            await this.Storage.SaveAsync(gameState, slotName);
        }

        private TGameState GetState()
        {
            Time.timeScale = 0;

            var gameState = new TGameState();
            gameState.LevelId = this.LevelManager.CurrentLevel.Id;
            gameState.GameObjectsStates = this.EnttiesStorage.Entities.Values.
                Select(o => new GameObjectState().SetGameObject(o.gameObject)).ToList();

            Time.timeScale = 1;

            return gameState;
        }

        private void CleanupLevel()
        {
            this.EnttiesStorage.Entities.Clear();
            this.EnttiesStorage.Root = GameObject.Find("Root");
            GameObject.Destroy(this.EnttiesStorage.Root);
        }

        private void RestoreGameState(TGameState gameState)
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
        }
    }
}