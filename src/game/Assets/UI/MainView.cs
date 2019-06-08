using System;
using System.Threading.Tasks;
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
	private UnityDependency<LoadingText> loadingText;

	public GameObject SaveButton;

	public void Start()
	{
		this.backgroundImage.Value.Show();
	}

	private async void OnEnable()
	{
		await Observable.NextFrame();
		this.SaveButton.SetActive(this.GameController.Value.LevelManager.CurrentLevel != null);
	}

	public async void PlayAsync()
	{
		MessageBroker.Default.Publish(new GameResumed(string.Empty));

		await this.blackScreen.Value.ShowAsync();
		this.backgroundImage.Value.Hide();
		this.loadingText.Value.DisplayNewText("Level 1: Laboratory \n Stage 1: Initializing system");
		this.Deactivate();
		this.ViewRouter.Value.ShowGameHUDView();
		await this.GameController.Value.LoadAsync(Level.Laboratory);
		await Task.Delay(2000);
		this.loadingText.Value.HideText();
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