using System;
using System.Linq;
using System.Threading.Tasks;
using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using GameSaving;
using TeamZ.Assets.Code.DependencyInjection;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{
	public readonly Dependency<GameController> GameController;
	public readonly Dependency<LevelManager> LevelManager;
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
		this.SaveButton.SetActive(this.LevelManager.Value.CurrentLevel != null);
        Selectable.allSelectables.First().Select();
    }

    public void Play()
	{
        this.ViewRouter.Value.ShowView(this.ViewRouter.Value.CharacterSelectionView);
	}

	public void Load()
	{
        this.ViewRouter.Value.ShowView(this.ViewRouter.Value.LoadView);
	}

	public void Save()
	{
        this.ViewRouter.Value.ShowView(this.ViewRouter.Value.SaveView);
    }

	public void Quit()
	{
		Application.Quit();
	}

	public void Settings()
	{

	}

}