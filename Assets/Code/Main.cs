using System.Collections;
using System.Collections.Generic;
using GameSaving;
using GameSaving.States;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameController<GameState> GameController
    {
        get;
        private set;
    }

    private void Start()
    {
        this.GameController = new GameController<GameState>();
        //this.GameController.Boostrap();
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
