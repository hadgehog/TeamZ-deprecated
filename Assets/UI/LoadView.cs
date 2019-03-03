using Assets.Code.Helpers;
using Assets.UI;
using GameSaving;
using GameSaving.States;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.UI.Load;
using UniRx;
using UnityEngine;

public class LoadView : MonoBehaviour
{
	private Dependency<GameStorage> GameController;
	private UnityDependency<ViewRouter> Router;

	public GameObject Root;
	public GameObject LoadItemTemplate;

	public async void OnEnable()
	{
		await Observable.NextFrame();

		foreach (Transform loadItem in this.Root.transform)
		{
			GameObject.Destroy(loadItem.gameObject);
		}

		foreach (var slot in this.GameController.Value.Slots)
		{
			var loadItem = GameObject.Instantiate<GameObject>(this.LoadItemTemplate, this.Root.transform);
			loadItem.GetComponent<LoadItemView>().SlotName = slot.Name;
		}
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			this.Cancel();
		}
	}

	public void Cancel()
	{
		this.Router.Value.ShowMainView();
	}
}