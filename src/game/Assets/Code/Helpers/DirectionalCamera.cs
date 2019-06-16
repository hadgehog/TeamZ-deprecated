using System.Linq;
using System.Threading.Tasks;
using Assets.Code.Helpers;
using GameSaving;
using GameSaving.States;
using TeamZ.Assets.Code.DependencyInjection;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class DirectionalCamera : MonoBehaviour
{
    public float dampTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Dependency<GameController> gameController;
    private Dependency<EntitiesStorage> entitiesStorage;
    private Transform[] targets;
    private Camera mainCamera;

    private void Start()
    {
        this.gameController.Value.Loaded.Subscribe(_ => this.SearchForPlayers());
        this.mainCamera = this.GetComponent<Camera>();
    }

    public async Task SearchForPlayers()
    {
        await UniTask.Delay(500);
        this.targets = this.entitiesStorage.Value.Entities.Values.
            Where(o => o.GetComponent<Lizard>() || o.GetComponent<Hedgehog>()).Select(o => o.transform).ToArray();
    }

    private void Update()
    {
        if (this.targets?.Any() ?? false)
        {
            var sum = Vector3.zero;
            for (int i = 0; i < this.targets.Length; i++)
            {
                var target = this.targets[i];
                // it can be broken during level switch
                if (!target)
                {
                    continue;
                }

                var point = this.mainCamera.WorldToViewportPoint(new Vector3(target.position.x, target.position.y + 1.5f, target.position.z));
                var delta = new Vector3(target.position.x, target.position.y + 1.5f, target.position.z) - this.mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                var destination = this.transform.position + delta;

                sum += destination;
            }

            var approximateDestination = sum / this.targets.Length;
            approximateDestination.z = -20;

            this.transform.position = Vector3.SmoothDamp(this.transform.position, approximateDestination, ref this.velocity, this.dampTime);
        }
    }
}