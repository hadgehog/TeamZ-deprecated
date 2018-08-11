using Assets.Code.Helpers;
using Assets.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SaveView : MonoBehaviour
{
	public InputField SlotName;
	public Transform ItemsRoot;

	public GameObject SaveItemTemplate;

	private Dependency<Main> Main;
	private Dependency<ViewRouter> ViewRouter;

	private Subject<SaveItemView> clicks = new Subject<SaveItemView>();

	private void Start()
	{
		this.clicks.Subscribe(o => this.SlotName.text = o.SlotName);
	}

	private async void OnEnable()
	{
		await Observable.NextFrame();

		foreach (Transform saveItem in this.ItemsRoot)
		{
			GameObject.Destroy(saveItem.gameObject);
		}

		foreach (var slot in this.Main.Value.GameController.Storage.Slots)
		{
			AddSlot(slot);
		}
	}

	private void AddSlot(string slot)
	{
		var loadItem = GameObject.Instantiate<GameObject>(this.SaveItemTemplate, this.ItemsRoot);
		var saveItemView = loadItem.GetComponent<SaveItemView>();
		saveItemView.SlotName = slot;
		saveItemView.Clicks.Subscribe(this.clicks);
	}

	public void Back()
	{
		this.ViewRouter.Value.ShowMainView();
	}

	public async void Save()
	{
		if (string.IsNullOrWhiteSpace(this.SlotName.text))
		{
			return;
		}

		await this.Main.Value.GameController.SaveAsync(this.SlotName.text);
		this.AddSlot(this.SlotName.text);
	}
}