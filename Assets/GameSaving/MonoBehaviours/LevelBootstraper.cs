using Assets.Code.Helpers;
using Assets.UI;
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

		private async void Start()
		{
			if (!this.Main)
			{
				await SceneManager.LoadSceneAsync("Core", LoadSceneMode.Additive);
				await Observable.NextFrame();

                var level = Level.All[this.LevelName];
                this.Main.Value.GameController.LevelManager.CurrentLevel = level;
                this.Main.Value.GameController.VisitedLevels.Add(level.Id);
                this.Main.Value.GameController.BootstrapEntities(true);

				this.Router.Value.ShowGameHUDView();
			}
		}
	}
}