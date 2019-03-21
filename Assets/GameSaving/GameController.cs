using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using Game.Levels;
using GameSaving.Interfaces;
using GameSaving.MonoBehaviours;
using GameSaving.States;
using GameSaving.States.Charaters;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Messages.GameSaving;
using TeamZ.Assets.Code.Game.Notifications;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameSaving
{
	public class GameController : IGameController
	{
		private UnityDependency<BlackScreen> BlackScreen;
		private UnityDependency<ViewRouter> ViewRouter;
		private UnityDependency<NotificationService> Notifications;
		private UnityDependency<BackgroundImage> BackgroundImage;

		public HashSet<Guid> VisitedLevels { get; private set; }

		public GameController()
		{
			this.Storage = Dependency<GameStorage>.Resolve();
			this.LevelManager = Dependency<LevelManager>.Resolve();
			this.EnttiesStorage = Dependency<EntitiesStorage>.Resolve();

			this.Loaded = new Subject<Unit>();
			this.VisitedLevels = new HashSet<Guid>();

			this.EnttiesStorage.Root = null;
			this.EnttiesStorage.Entities.Clear();

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

		public EntitiesStorage EnttiesStorage
		{
			get;
		}

		public LevelManager LevelManager
		{
			get;
		}

		public Subject<Unit> Loaded
		{
			get;
		}

		public GameStorage Storage
		{
			get;
		}

		public void BootstrapEntities(bool loaded = false)
		{
			this.EnttiesStorage.Entities.Clear();
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

		public async Task LoadSavedGameAsync(string slotName)
		{
			this.BackgroundImage.Value.Hide();
			await this.BlackScreen.Value.ShowAsync();
			var gameState = await this.Storage.LoadAsync(slotName);
			await this.LoadGameStateAsync(gameState);
			await this.BlackScreen.Value.HideAsync();
		}

		public async Task LoadAsync(Level level)
		{
			this.VisitedLevels.Clear();
			await this.LevelManager.LoadAsync(level);

			var gameState = this.GetState();
			await this.BootstrapAsync(gameState);

			gameState.VisitedLevels.Add(level.Id);
		}

		public async Task LoadGameStateAsync(GameState gameState)
		{
			await this.LevelManager.LoadAsync(Level.All.First(o => o.Value.Id == gameState.LevelId).Value);
			await this.BootstrapAsync(gameState);
		}

		public async Task SaveAsync(string slotName)
		{
			await this.SaveAsync(this.GetState(), slotName);
			MessageBroker.Default.Publish(new GameSaved());
		}

		public async Task SaveAsync(GameState gameState, string slotName)
		{
			await this.Storage.SaveAsync(gameState, slotName);
		}

		public async void SwitchLevelAsync(Level level, string locationName)
		{
			await this.BlackScreen.Value.ShowAsync();
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

			await this.LoadGameStateAsync(gameState);

			// TODO: Think about how set position before scene loading.
			var locationPosition = GameObject.FindObjectsOfType<Location>().
				First(o => o.name == locationName).transform.position;

			foreach (var character in this.EnttiesStorage.Entities.Values.Where(o => o.GetComponent<ICharacter>() != null))
			{
				character.transform.localPosition = locationPosition;
			}

			this.VisitedLevels.Add(level.Id);

			Time.timeScale = 1;
			await this.BlackScreen.Value.HideAsync();
		}

		public async Task LoadLastSavedGameAsync()
		{
			var lastSave = this.Storage.Slots.OrderByDescending(o => o.Modified).First();
			await this.LoadSavedGameAsync(lastSave.Name);
		}

		private async Task BootstrapAsync(GameState gameState)
		{
			this.EnttiesStorage.Root = GameObject.Find("Root");
			if (gameState.VisitedLevels.Contains(this.LevelManager.CurrentLevel.Id))
			{
				GameObject.DestroyImmediate(this.EnttiesStorage.Root);
				this.EnttiesStorage.Root = new GameObject("Root");
			}

			this.BootstrapEntities();
			this.EnttiesStorage.Root.SetActive(false);

			await this.RestoreGameStateAsync(gameState);

			GC.Collect();

			this.EnttiesStorage.Root.SetActive(true);
			this.Loaded.OnNext(Unit.Default);

			this.VisitedLevels = gameState.VisitedLevels;
		}

		private GameState GetState()
		{
			Time.timeScale = 0;

			var gameState = new GameState();
			gameState.LevelId = this.LevelManager.CurrentLevel.Id;
			gameState.GameObjectsStates = this.EnttiesStorage.Entities.Values.
				Select(o => new GameObjectState().SetGameObject(o.gameObject)).ToList();

			gameState.VisitedLevels = this.VisitedLevels;

			Time.timeScale = 1;

			return gameState;
		}

		private async Task RestoreGameStateAsync(GameState gameState)
		{
			var cache = new Dictionary<string, GameObject>();
			var monoBehaviours = new LinkedList<IMonoBehaviourWithState>();

			foreach (var gameObjectState in gameState.GameObjectsStates.Where(o => o.Entity.LevelId == this.LevelManager.CurrentLevel.Id))
			{
				var entity = gameObjectState.Entity;
				if (!cache.ContainsKey(entity.AssetGuid))
				{
					var assetReference = new AssetReference(entity.AssetGuid);
					var load = assetReference.LoadAsset<GameObject>();
					await load.ToUniTask();

					cache.Add(entity.AssetGuid, load.Result);
				}

				var gameObject = GameObject.Instantiate<GameObject>(cache[entity.AssetGuid], this.EnttiesStorage.Root.transform);

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