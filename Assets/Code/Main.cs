using System.Linq;
using GameObjects.Activation.Core;
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

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async void Start()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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

        if (Input.GetKeyUp(KeyCode.E))
        {
            //TODO: rework in future to support several characters
            var currentCharacter = this.GameController.EnttiesStorage.Entities.Select(o => o.Value.GetComponent<Lizard>()).First(o => o);
            var hits = Physics.RaycastAll(currentCharacter.transform.position - Vector3.forward, Vector3.forward);
            var firstActivable = hits.Select(o => o.collider.gameObject.GetComponent<IActivable>()).Where(o => o != null).FirstOrDefault();
            firstActivable?.Activate();
        }

        if (Input.GetKeyUp(KeyCode.F9))
        {
            await this.GameController.LoadAsync("test");
        }
    }
}
