using System.Collections;
using System.Collections.Generic;
using GameSaving;
using GameSaving.States;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameController<GameState> GameController
    {
        get;
        private set;
    }

    private async void Start()
    {
        this.GameController = new GameController<GameState>();

#if UNITY_EDITOR
        this.GameController.BootstrapFromEditor();
#else
        await this.GameController.LevelManager.Load(Level.Laboratory);
#endif
        this.GameController.Boostrap();
    }

    public async void Update()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            await this.GameController.SaveAsync("test");
        }

        if (Input.GetKeyUp(KeyCode.F9))
        {
            await this.GameController.LoadAsync("test");
        }
    }
}
