using System;
using System.Linq;
using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using Game.Activation.Core;
using GameSaving;
using GameSaving.States;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Handlers;
using TeamZ.Mediator;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class Main : MonoBehaviour
{
	private readonly UnityDependency<ViewRouter> ViewRouter;
	private UnityDependency<BackgroundImage> BackgroundImage;
	private readonly Dependency<GameController> gameController;


	private async void Start()
	{
		Application.targetFrameRate = 60;

		this.RegisterHandlers();
		this.RegisterDependencies(DependencyContainer.Instance);

		this.gameController.Value.Loaded.Subscribe(_ => this.Loaded());

		await UniTask.DelayFrame(1);
		MessageBroker.Default.Publish(new GamePaused());
	}

	private void RegisterDependencies(DependencyContainer container)
	{
		container.Add<GameController>();
		container.Add<GameStorage>();
		container.Add<LevelManager>();
		container.Add<EntitiesStorage>();
	}

	private void RegisterHandlers()
	{
		Mediator.Instance.Add(new DeathHandler());
		Mediator.Instance.Add<GamePaused>(o => Time.timeScale = 0);
		Mediator.Instance.Add<GameResumed>(o => Time.timeScale = 1);
	}

	private void Loaded()
	{
		MessageBroker.Default.Publish(new GameResumed(this.gameController.Value.LevelManager.CurrentLevel.Name));
	}

	public async void Update()
	{
		if (Input.GetKeyUp(KeyCode.F5))
		{
			await this.gameController.Value.SaveAsync("test");
		}

		if (Input.GetKeyUp(KeyCode.E))
		{
			//TODO: rework in future to support several characters
			var currentCharacter = this.gameController.Value.EnttiesStorage.Entities.Select(o => o.Value.GetComponent<Lizard>()).First(o => o);
			var hits = Physics.RaycastAll(currentCharacter.transform.position - Vector3.forward, Vector3.forward);
			var firstActivable = hits.Select(o => o.collider.gameObject.GetComponent<IActivable>()).Where(o => o != null).FirstOrDefault();
			firstActivable?.Activate();
		}

		if (Input.GetKeyUp(KeyCode.F9))
		{
			await this.gameController.Value.LoadSavedGameAsync("test");
		}

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (this.ViewRouter.Value.MainView.isActiveAndEnabled && 
				this.gameController.Value.LevelManager.CurrentLevel != null)
			{
				this.BackgroundImage.Value.Hide();
				this.ViewRouter.Value.ShowGameHUDView();
				MessageBroker.Default.Publish(new GameResumed(this.gameController.Value.LevelManager.CurrentLevel.Name));
				return;
			}

			MessageBroker.Default.Publish(new GamePaused());
			this.BackgroundImage.Value.Show();
			this.ViewRouter.Value.ShowMainView();
		}
	}
}

public class GamePaused : ICommand
{
	public GamePaused()
	{
	}
}

public class GameResumed : ICommand
{
	public string Level;

	public GameResumed(string level)
	{
		this.Level = level;
	}
}