using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Code.Helpers;
using Assets.UI;
using Assets.UI.Texts;
using Effects;
using Game.Levels;
using GameSaving.Interfaces;
using GameSaving.MonoBehaviours;
using GameSaving.States;
using GameSaving.States.Charaters;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Characters;
using TeamZ.Assets.Code.Game.Levels;
using TeamZ.Assets.Code.Game.Messages.GameSaving;
using TeamZ.Assets.Code.Game.Notifications;
using TeamZ.Assets.Code.Game.Players;
using TeamZ.Assets.Code.Game.UserInput;
using TeamZ.Assets.Code.Helpers;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace GameSaving
{
    public class GameController : IGameController
    {
        private UnityDependency<BlackScreen> BlackScreen;
        private UnityDependency<ViewRouter> ViewRouter;
        private UnityDependency<NotificationService> Notifications;
        private UnityDependency<BackgroundImage> BackgroundImage;
        private UnityDependency<LoadingText> LoadingText;
        private Dependency<UserInputMapper> UserInputMapper;
        private Dependency<PlayerService> PlayerService;
        private Dependency<EntitiesStorage> EntitiesStorage;
        private Dependency<LevelManager> LevelManager;
        private Dependency<GameStorage> Storage;

        private bool loading;

        public HashSet<Guid> VisitedLevels { get; private set; }

        public GameController()
        {
            this.Loaded = new Subject<Unit>();
            this.VisitedLevels = new HashSet<Guid>();

            MessageBroker.Default.Receive<GameSaved>().
                Subscribe(_ => this.Notifications.Value.ShowShortMessage("Game saved"));

            MessageBroker.Default.Receive<LoadGameRequest>().
                Subscribe(async o =>
                {
                    MessageBroker.Default.Publish(new GameResumed(string.Empty));
                    this.ViewRouter.Value.ShowGameHUDView();
                    this.BackgroundImage.Value.Hide();
                    await this.BlackScreen.Value.ShowAsync();
                    await this.LoadSavedGameAsync(o.SlotName);
                    MessageBroker.Default.Publish(new GameLoaded());
                    await this.BlackScreen.Value.HideAsync();
                });
        }


        public Subject<Unit> Loaded
        {
            get;
        }


        public void BootstrapEntities(bool loaded = false)
        {
            this.EntitiesStorage.Value.Entities.Clear();
            this.EntitiesStorage.Value.Root = GameObject.Find("Root");
            foreach (var entity in GameObject.FindObjectsOfType<Entity>())
            {
                entity.LevelId = this.LevelManager.Value.CurrentLevel.Id;
                this.EntitiesStorage.Value.Entities.Add(entity.Id, entity);
            };

            //await this.SaveAsync("temp");
            //await this.LoadAsync("temp");
        }

        public void BootstrapFromEditor()
        {
            var levelBootstraper = GameObject.FindObjectOfType<LevelBootstraper>();
            this.LevelManager.Value.CurrentLevel = Level.All[levelBootstraper.LevelName];
        }

        public async Task LoadSavedGameAsync(string slotName)
        {
            if (this.loading)
            {
                return;
            }

            this.loading = true;

            this.BackgroundImage.Value.Hide();
            await this.BlackScreen.Value.ShowAsync();
            var gameState = await this.Storage.Value.LoadAsync(slotName);

            var level = Level.AllById[gameState.LevelId];
            var levelName = Texts.GetLevelText(level.Name);

            this.LoadingText.Value.DisplayNewText(levelName);
            await this.LoadGameStateAsync(gameState);
            await Task.Delay(2000);
            this.LoadingText.Value.HideText();
            await this.BlackScreen.Value.HideAsync();

            this.loading = false;
        }

        public async Task LoadAsync(Level level)
        {
            this.VisitedLevels.Clear();

            await this.LevelManager.Value.LoadAsync(level);

            var gameState = this.GetState();
            await this.BootstrapAsync(gameState);

            gameState.VisitedLevels.Add(level.Id);
        }

        public async Task LoadGameStateAsync(GameState gameState)
        {
            DependencyContainer.Instance.NewScope();

            var level = Level.AllById[gameState.LevelId];
            await this.LevelManager.Value.LoadAsync(level);
            await this.BootstrapAsync(gameState);

            this.Loaded.OnNext(Unit.Default);
        }

        public async Task SaveAsync(string slotName)
        {
            await this.SaveAsync(this.GetState(), slotName);
            MessageBroker.Default.Publish(new GameSaved());
        }

        public async Task SaveAsync(GameState gameState, string slotName)
        {
            await this.Storage.Value.SaveAsync(gameState, slotName);
        }

        public async void SwitchLevelAsync(Level level, string locationName)
        {
            await this.BlackScreen.Value.ShowAsync();
            this.LoadingText.Value.DisplayNewText(Texts.GetLevelText(level.Name));

            var time = DateTime.Now.ToTeamZDateTime();
            var beforeAutoSave = $"Switching to {level.Name} {time}";

            var gameState = this.GetState();
            await this.SaveAsync(gameState, beforeAutoSave);

            gameState.LevelId = level.Id;
            var mainCharacters = gameState.GameObjectsStates.
                Where(o => o.MonoBehaviousStates.OfType<CharacterState>().Any());

            foreach (var character in mainCharacters)
            {
                character.Entity.LevelId = level.Id;
            }

            Time.timeScale = 0;

            await this.LoadGameStateAsync(gameState);

            // TODO: Think about how set position before scene loading.
            var locationPosition = GameObject.FindObjectsOfType<Location>().
                First(o => o.name == locationName).transform.position;

            foreach (var character in this.EntitiesStorage.Value.Entities.Values.Where(o => o.GetComponent<ICharacter>() != null))
            {
                character.transform.localPosition = locationPosition;
            }

            gameState = this.GetState();
            var afterAutoSave = $"Switched to {level.Name} {time}";

            this.VisitedLevels.Add(level.Id);
            await this.SaveAsync(gameState, afterAutoSave);

            Time.timeScale = 1;

            await Task.Delay(2000);
            this.LoadingText.Value.HideText();
            this.Loaded.OnNext(Unit.Default);
            await this.BlackScreen.Value.HideAsync();
        }

        public async Task LoadLastSavedGameAsync()
        {
            var lastSave = this.Storage.Value.Slots.OrderByDescending(o => o.Modified).First();
            await this.LoadSavedGameAsync(lastSave.Name);
        }

        public async Task StartNewGameAsync(params CharacterDescriptor[] characterDescriptors)
        {
            DependencyContainer.Instance.NewScope();

            MessageBroker.Default.Publish(new GameResumed(string.Empty));

            await this.BlackScreen.Value.ShowAsync();
            this.BackgroundImage.Value.Hide();
            this.LoadingText.Value.DisplayNewText("Level 1: Laboratory \n Stage 1: Initializing system");
            this.ViewRouter.Value.ShowGameHUDView();

            await this.LoadAsync(Level.Laboratory);

            var startLocation = GameObject.FindObjectOfType<StartLocation>().transform.localPosition;
    
            foreach (var descriptor in characterDescriptors)
            {
                this.PlayerService.Value.AddPlayer(descriptor, startLocation);
            }

            this.UserInputMapper.Value.Bootstrap();

            MessageBroker.Default.Publish(new GameLoaded());
            await Task.Delay(2000);

            this.LoadingText.Value.HideText();

            await this.SaveAsync($"new game {this.FormDateTimeString()}");

            this.Loaded.OnNext(Unit.Default);
            await this.BlackScreen.Value.HideAsync();
        }


        private string FormDateTimeString()
        {
            var dateTimeString =
                DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "_" +
                DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            return dateTimeString;
        }

        private async Task BootstrapAsync(GameState gameState)
        {
            this.EntitiesStorage.Value.Root = GameObject.Find("Root");
            if (gameState.VisitedLevels.Contains(this.LevelManager.Value.CurrentLevel.Id))
            {
                GameObject.DestroyImmediate(this.EntitiesStorage.Value.Root);
                this.EntitiesStorage.Value.Root = new GameObject("Root");
            }

            this.BootstrapEntities();
            this.EntitiesStorage.Value.Root.SetActive(false);

            await this.RestoreGameStateAsync(gameState);

            GC.Collect();

            this.EntitiesStorage.Value.Root.SetActive(true);

            this.VisitedLevels = gameState.VisitedLevels;
            this.PlayerService.Value.SetState(gameState.PlayerServiceState);
        }

        private GameState GetState()
        {
            Time.timeScale = 0;

            var gameState = new GameState();
            gameState.LevelId = this.LevelManager.Value.CurrentLevel.Id;
            gameState.GameObjectsStates = this.EntitiesStorage.Value.Entities.Values.
                Select(o => new GameObjectState().SetGameObject(o.gameObject)).ToList();

            gameState.VisitedLevels = this.VisitedLevels;
            gameState.PlayerServiceState = this.PlayerService.Value.GetState();

            Time.timeScale = 1;

            return gameState;
        }

        private async Task RestoreGameStateAsync(GameState gameState)
        {
            var cache = new Dictionary<string, GameObject>();
            var monoBehaviours = new LinkedList<IMonoBehaviourWithState>();

            foreach (var gameObjectState in gameState.GameObjectsStates.Where(o => o.Entity.LevelId == this.LevelManager.Value.CurrentLevel.Id))
            {
                var entity = gameObjectState.Entity;
                if (!cache.ContainsKey(entity.AssetGuid))
                {
                    var template = Resources.Load<GameObject>(entity.AssetGuid);
                    cache.Add(entity.AssetGuid, template);
                }

                var gameObject = GameObject.Instantiate<GameObject>(cache[entity.AssetGuid], this.EntitiesStorage.Value.Root.transform);

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
                this.EntitiesStorage.Value.Entities.Add(prefab.Id, prefab);
            }

            foreach (var monoBehaviour in monoBehaviours)
            {
                monoBehaviour.Loaded();
            }
        }
    }
}