﻿using System.Linq;
using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using GameObjects.Activation.Core;
using GameSaving;
using GameSaving.States;
using UnityEngine;

public class Main : MonoBehaviour
{
	private readonly UnityDependency<BlackScreen> BlackScreen;
	private readonly UnityDependency<ViewRouter> ViewRouter;

	public GameController<GameState> GameController
	{
		get;
		private set;
	}

	public async void Update()
	{
		if (Input.GetKeyUp(KeyCode.F5))
		{
			await this.GameController.SaveAsync("test");
		}

		if (Input.GetKeyUp(KeyCode.E))
		{
			//TODO: rework in future to support several characters
			var currentCharacter = this.GameController.EnttiesStorage.Entities.Select(o => o.Value.GetComponent<Lizard>()).First(o => o);
			var hits = Physics.RaycastAll(currentCharacter.transform.position - Vector3.forward, Vector3.forward);
			var firstActivable = hits.Select(o => o.collider.gameObject.GetComponent<IActivable>()).Where(o => o != null).FirstOrDefault();
			firstActivable?.Activate();
		}

		if (Input.GetKeyUp(KeyCode.F9))
		{
			await this.BlackScreen.Value.ShowAsync();
			await this.GameController.LoadSavedGameAsync("test");
			await this.BlackScreen.Value.HideAsync();
		}

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (this.ViewRouter.Value.MainView.isActiveAndEnabled)
			{
				this.ViewRouter.Value.ShowGameHUDView();
				return;
			}

			this.ViewRouter.Value.ShowMainView();
		}
	}

	private void Start()
	{
		this.GameController = new GameController<GameState>();
	}
}