using System.Linq;
using Assets.Code.Helpers;
using Effects;
using GameObjects.Activation.Core;
using GameSaving;
using GameSaving.States;
using UnityEngine;

public class Main : MonoBehaviour
{
	private readonly Dependency<BlackScreen> loadingEffect;

	public GameController<GameState> GameController
	{
		get;
		private set;
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
			await this.loadingEffect.Value.ShowAsync();
			await this.GameController.LoadAsync("test");
			await this.loadingEffect.Value.HideAsync();
		}
	}

	private void Start()
	{
		this.GameController = new GameController<GameState>();
	}
}