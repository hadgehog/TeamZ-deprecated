using Assets.Code.Helpers;
using Effects;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class MainView : MonoBehaviour
{
	public readonly Dependency<Main> Main;
	private Dependency<BlackScreen> blackScreen;

	public async void PlayAsync()
	{
		var gameController = this.Main.Value.GameController;

		await this.blackScreen.Value.ShowAsync();
		this.gameObject.SetActive(false);
		await gameController.LoadAsync(Level.Laboratory);
		await this.blackScreen.Value.HideAsync();
	}

	public void Quit()
	{
	}

	public void Settings()
	{
	}
}