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
        if (this.CurrentLevel != null)
            await SceneManager.UnloadSceneAsync(this.CurrentLevel.Scene);

        this.CurrentLevel = level;
        await SceneManager.LoadSceneAsync(level.Scene, LoadSceneMode.Additive);
        var scene = SceneManager.GetSceneByName(level.Scene);
        SceneManager.SetActiveScene(scene);
    }
}
