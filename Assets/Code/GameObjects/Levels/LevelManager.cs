using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class LevelManager
{
    public LevelInformation CurrentLevel;

    public async Task Load(LevelInformation level)
    {
        // TODO: Think about how to refactor it
        var loaded = false;

        Observable.FromCoroutine(() => this.LoadInternal(level)).
            Subscribe(_ => loaded = true);

        while (!loaded)
        {
            await Task.Delay(100);
        }
    }

    private IEnumerator LoadInternal(LevelInformation level)
    {
        if (this.CurrentLevel != null)
            yield return SceneManager.UnloadSceneAsync(this.CurrentLevel.Scene);

        this.CurrentLevel = level;
        yield return SceneManager.LoadSceneAsync(level.Scene, LoadSceneMode.Additive);
        var scene = SceneManager.GetSceneByName(level.Scene);
        SceneManager.SetActiveScene(scene);
    }
}
