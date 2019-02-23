using Assets.Code.Helpers;
using Assets.UI;
using TeamZ.Assets.UI.Load;
using UniRx;
using UnityEngine;

public class LoadView : MonoBehaviour
{
	private UnityDependency<Main> Main;
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

		foreach (var slot in this.Main.Value.GameController.Storage.Slots)
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