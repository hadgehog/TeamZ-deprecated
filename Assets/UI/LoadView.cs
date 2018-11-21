using Assets.Code.Helpers;
using Assets.UI;
using GameSaving;
using TeamZ.Assets.UI.Load;
using UniRx;
using UnityEngine;

public class LoadView : MonoBehaviour
{
	private UnityDependency<Main> Main;

	public GameObject LoadItemTemplate;

	public async void OnEnable()
	{
		await Observable.NextFrame();

		foreach (Transform loadItem in this.transform)
		{
			GameObject.Destroy(loadItem.gameObject);
		}

		foreach (var slot in this.Main.Value.GameController.Storage.Slots)
		{
			var loadItem = GameObject.Instantiate<GameObject>(this.LoadItemTemplate, this.transform);
			loadItem.GetComponent<LoadItemView>().SlotName = slot;
		}
	}
}