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
using Assets.Code.Helpers;

namespace GameSaving
{
    public class GameController<TGameState> : IGameController
        where TGameState : GameState, new()
    {
        private Dependency<BlackScreen> blackScreen;

        public GameController()
        {
            this.Storage = new GameStorage<TGameState>();
            this.LevelManager = new LevelManager();
            this.Loaded = new Subject<Unit>();
            this.EnttiesStorage.Root = null;
            this.EnttiesStorage.Entities.Clear();
        }

        public EntitiesStorage EnttiesStorage
        {
            get
            {
                return EntitiesStorage.Instance;
            }
        }

        public LevelManager LevelManager
        {
            get;
        }

        public Subject<Unit> Loaded
        {
            get;
            set;
        }

        public GameStorage<TGameState> Storage
        {
            get;
        }

        public void Bootstrap(bool loaded = false)
        {
            foreach (var entity in GameObject.FindObjectsOfType<Entity>())
            {
                entity.LevelId = this.LevelManager.CurrentLevel.Id;
                this.EnttiesStorage.Entities.Add(entity.Id, entity);
            };

			if (loaded)
			{
				this.Loaded.OnNext(Unit.Default);
			}

            //await this.SaveAsync("temp");
            //await this.LoadAsync("temp");
        }

        public void BootstrapFromEditor()
        {
            var levelBootstraper = GameObject.FindObjectOfType<LevelBootstraper>();
            this.LevelManager.CurrentLevel = Level.All[levelBootstraper.LevelName];
        }

        public async Task LoadAsync(string slotName)
        {
            await this.LoadAsync(await this.Storage.LoadAsync(slotName));
        }

        public async Task LoadAsync(Level level)
        {
            await this.LevelManager.LoadAsync(level);
            this.Bootstrap();
            this.Bootstrap(this.GetState());
        }

        public async Task LoadAsync(TGameState gameState)
        {
            await this.LevelManager.LoadAsync(Level.All.First(o => o.Value.Id == gameState.LevelId).Value);
            this.Bootstrap(gameState);
        }

        public async Task SaveAsync(string slotName)
        {
            await this.SaveAsync(this.GetState(), slotName);
        }

        public async Task SaveAsync(TGameState gameState, string slotName)
        {
            await this.Storage.SaveAsync(gameState, slotName);
        }

        public async void SwitchLevelAsync(Level level, string locationName)
        {
            await this.blackScreen.Value.ShowAsync();
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
            await this.blackScreen.Value.HideAsync();
        }

        private void Bootstrap(TGameState gameState)
        {
            this.CleanupLevel();

            this.EnttiesStorage.Root = new GameObject("Root");
            this.EnttiesStorage.Root.SetActive(false);

            this.RestoreGameState(gameState);

            GC.Collect();

            this.EnttiesStorage.Root.SetActive(true);
            this.Loaded.OnNext(Unit.Default);
        }

        private void CleanupLevel()
        {
            this.EnttiesStorage.Entities.Clear();
            this.EnttiesStorage.Root = GameObject.Find("Root");
            GameObject.DestroyImmediate(this.EnttiesStorage.Root);
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

        private void RestoreGameState(TGameState gameState)
        {
            var cache = new Dictionary<string, GameObject>();
            var monoBehaviours = new LinkedList<IMonoBehaviourWithState>();

            foreach (var gameObjectState in gameState.GameObjectsStates.Where(o => o.Entity.LevelId == this.LevelManager.CurrentLevel.Id))
            {
                var entity = gameObjectState.Entity;
                if (!cache.ContainsKey(entity.Path))
                {
                    cache.Add(entity.Path, Resources.Load<GameObject>(entity.Path));
                }

                var gameObject = GameObject.Instantiate<GameObject>(cache[entity.Path], this.EnttiesStorage.Root.transform);

                var states = gameObjectState.MonoBehaviousStates.ToList();
                states.Add(entity);

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