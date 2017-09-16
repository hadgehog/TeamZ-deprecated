using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelStorage))]
public class LevelController : MonoBehaviour
{
    public LevelStorage Storage;

    private string currentScene;

    private void Start()
    {
        this.currentScene = GameObject.FindObjectOfType<LevelInformation>().Name;
    }

    public IEnumerator Load(string sceneName)
    {
        if (!string.IsNullOrWhiteSpace(this.currentScene))
            yield return SceneManager.UnloadSceneAsync(this.currentScene);

        this.currentScene = sceneName;
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
