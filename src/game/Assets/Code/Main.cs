using System;
using System.Linq;
using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using Game.Activation.Core;
using GameSaving;
using GameSaving.MonoBehaviours;
using GameSaving.States;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.Code.Game.Players;
using TeamZ.Assets.Code.Game.UserInput;
using TeamZ.Handlers;
using TeamZ.Mediator;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class Main : MonoBehaviour
{
	private readonly UnityDependency<ViewRouter> ViewRouter;
	private readonly UnityDependency<BackgroundImage> BackgroundImage;
    private readonly UnityDependency<LevelBootstraper> LevelBootstrapper;

    private readonly Dependency<GameController> gameController;
    private readonly Dependency<UserInputMapper> userInputMapper;
    private readonly Dependency<LevelManager> levelManager;



    private async void Start()
	{
		Application.targetFrameRate = 60;

		this.RegisterHandlers();
		this.RegisterDependencies(DependencyContainer.Instance);

		this.gameController.Value.Loaded.Subscribe(_ => this.Loaded());

		await UniTask.DelayFrame(1);
		MessageBroker.Default.Publish(new GamePaused());

        if (!this.LevelBootstrapper)
        {
            this.ViewRouter.Value.ShowMainView();
        }
    }

    private void RegisterDependencies(DependencyContainer container)
	{
		container.Add<GameController>();
		container.Add<GameStorage>();
		container.Add<LevelManager>();
		container.Add<EntitiesStorage>();
        container.AddScoped<UserInputMapper>();
        container.AddScoped<PlayerService>();
    }

    private void RegisterHandlers()
	{
		Mediator.Instance.Add(new DeathHandler());
		Mediator.Instance.Add<GamePaused>(o => Time.timeScale = 0);
		Mediator.Instance.Add<GameResumed>(o => Time.timeScale = 1);
	}

	private void Loaded()
	{
		MessageBroker.Default.Publish(new GameResumed(this.levelManager.Value.CurrentLevel.Name));
    }

    public async void Update()
	{
		if (Input.GetKeyUp(KeyCode.F5))
		{
			await this.gameController.Value.SaveAsync("test");
		}

		if (Input.GetKeyUp(KeyCode.F9))
		{
			await this.gameController.Value.LoadSavedGameAsync("test");
		}

		if (Input.GetKeyUp(KeyCode.Escape) &&
            this.levelManager.Value.CurrentLevel != null)
		{
			if (this.ViewRouter.Value.CurrentView is MainView &&
                this.levelManager.Value.CurrentLevel != null)
			{
				this.BackgroundImage.Value.Hide();
				this.ViewRouter.Value.ShowGameHUDView();
				MessageBroker.Default.Publish(new GameResumed(this.levelManager.Value.CurrentLevel.Name));
				return;
			}

			MessageBroker.Default.Publish(new GamePaused());
			this.BackgroundImage.Value.Show();
			this.ViewRouter.Value.ShowMainView();
		}
	}

    private void OnDestroy()
    {
        Mediator.Instance.Reset();
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