using System.Threading.Tasks;
using GameSaving.MonoBehaviours;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
	public Level CurrentLevel;

	public async Task LoadAsync(Level level)
	{
		if (this.CurrentLevel == null)
		{
			var levelName = GameObject.FindObjectOfType<LevelBootstraper>()?.LevelName ?? string.Empty;
			Level.All.TryGetValue(levelName, out this.CurrentLevel);
		}

		if (this.CurrentLevel != null)
		{
			await SceneManager.UnloadSceneAsync(this.CurrentLevel.Scene);
		}

		this.CurrentLevel = level;
		
		await SceneManager.LoadSceneAsync(level.Scene, LoadSceneMode.Additive);
		var scene = SceneManager.GetSceneByName(level.Scene);
		SceneManager.SetActiveScene(scene);
	}
}