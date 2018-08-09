using Assets.Code.Helpers;
using Assets.UI;
using Effects;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class MainView : MonoBehaviour
{
	public readonly Dependency<Main> Main;
	public readonly Dependency<ViewRouter> ViewRouter;

	private Dependency<BlackScreen> blackScreen;

	public async void PlayAsync()
	{
		var gameController = this.Main.Value.GameController;

		await this.blackScreen.Value.ShowAsync();
		this.Deactivate();
		await gameController.LoadAsync(Level.Laboratory);
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
}