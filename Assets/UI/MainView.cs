using System;
using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using UnityEngine;

public class MainView : MonoBehaviour
{
	public bool IsGameStarted = false;

	public readonly UnityDependency<Main> Main;
	public readonly UnityDependency<ViewRouter> ViewRouter;

	private UnityDependency<BlackScreen> blackScreen;

	public async void PlayAsync()
	{
		var gameController = this.Main.Value.GameController;

		await this.blackScreen.Value.ShowAsync();
		this.Deactivate();
		this.ViewRouter.Value.GameHUDView.Activate();
		await gameController.LoadAsync(Level.Laboratory);
		await gameController.SaveAsync($"new game {this.FormDateTimeString()}");
		await this.blackScreen.Value.HideAsync();
		this.IsGameStarted = true;
	}

	public void Load()
	{
		this.Deactivate();
		this.ViewRouter.Value.LoadView.Activate();
	}

	public void Save()
	{
		this.Deactivate();
		this.ViewRouter.Value.SaveView.Activate();
	}

	public void Quit()
	{
		this.IsGameStarted = false;
		Application.Quit();
	}

	public void Settings()
	{

	}

	private string FormDateTimeString()
	{
		var dateTimeString = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
		return dateTimeString;
	}
}