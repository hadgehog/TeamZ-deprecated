using System;
using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using GameSaving;
using TeamZ.Assets.Code.DependencyInjection;
using UniRx;
using UnityEngine;

public class MainView : MonoBehaviour
{
	public readonly Dependency<GameController> GameController;
	public readonly UnityDependency<ViewRouter> ViewRouter;

	private UnityDependency<BlackScreen> blackScreen;
	private UnityDependency<BackgroundImage> backgroundImage;

	public void Start()
	{
		this.backgroundImage.Value.Show();
	}

	public async void PlayAsync()
	{
		MessageBroker.Default.Publish(new GameResumed(string.Empty));

		await this.blackScreen.Value.ShowAsync();
		this.backgroundImage.Value.Hide();
		this.Deactivate();
		this.ViewRouter.Value.GameHUDView.Activate();
		await this.GameController.Value.LoadAsync(Level.Laboratory);
		await this.GameController.Value.SaveAsync($"new game {this.FormDateTimeString()}");
		await this.blackScreen.Value.HideAsync();
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
		Application.Quit();
	}

	public void Settings()
	{

	}

	private string FormDateTimeString()
	{
		var dateTimeString = 
			DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "_" + 
			DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
		return dateTimeString;
	}
}