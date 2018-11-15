﻿using Assets.Code.Helpers;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSaving.MonoBehaviours
{
	public class LevelBootstraper : MonoBehaviour
	{
		public string LevelName;

		private Dependency<Main> Main;
		private Dependency<MainView> MainView;

		private async void Start()
		{
			if (!this.Main)
			{
				await SceneManager.LoadSceneAsync("Core", LoadSceneMode.Additive);
				await Observable.NextFrame();

				this.Main.Value.GameController.LevelManager.CurrentLevel = Level.All[this.LevelName];
				this.Main.Value.GameController.Bootstrap(true);
				this.MainView.Value.Deactivate();
			}
		}
	}
}