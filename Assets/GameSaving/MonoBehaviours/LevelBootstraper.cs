﻿using Assets.Code.Helpers;
using Assets.UI;
using GameSaving.States;
using TeamZ.Assets.Code.DependencyInjection;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSaving.MonoBehaviours
{
	public class LevelBootstraper : MonoBehaviour
	{
		public string LevelName;

		private UnityDependency<Main> Main;
		private UnityDependency<ViewRouter> Router;
		private Dependency<GameController> GameController;
		private Dependency<LevelManager> LevelManager;

		private async void Start()
		{
			if (!this.Main)
			{
				await SceneManager.LoadSceneAsync("Core", LoadSceneMode.Additive);
				await Observable.NextFrame();

                var level = Level.All[this.LevelName];
                this.LevelManager.Value.CurrentLevel = level;
                this.GameController.Value.VisitedLevels.Add(level.Id);
                this.GameController.Value.BootstrapEntities(true);

				this.Router.Value.ShowGameHUDView();
			}
		}
	}
}