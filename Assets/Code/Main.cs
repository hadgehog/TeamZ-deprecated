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
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            this.GameController.SaveAsync("test");
        }

        if (Input.GetKeyUp(KeyCode.F9))
        {
            this.GameController.LoadAsync("test");
        }
    }
}
