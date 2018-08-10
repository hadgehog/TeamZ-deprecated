using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class LevelManager
{
	public Level CurrentLevel;

	public async Task LoadAsync(Level level)
	{
		if (this.CurrentLevel != null)
			await SceneManager.UnloadSceneAsync(this.CurrentLevel.Scene);

		this.CurrentLevel = level;
		await SceneManager.LoadSceneAsync(level.Scene, LoadSceneMode.Additive);
		var scene = SceneManager.GetSceneByName(level.Scene);
		SceneManager.SetActiveScene(scene);
	}
}