using System.Linq;
using Assets.Code.Helpers;
using Assets.UI;
using GameSaving;
using GameSaving.States;
using TeamZ.Assets.Code.DependencyInjection;
using TeamZ.Assets.UI.Load;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LoadView : View
{
	private Dependency<GameStorage> GameStorage;
	private UnityDependency<ViewRouter> Router;

	public GameObject Root;
	public GameObject LoadItemTemplate;
    public Selectable VerticalBar;

	public async void OnEnable()
	{
		await Observable.NextFrame();

		foreach (Transform loadItem in this.Root.transform)
		{
			GameObject.Destroy(loadItem.gameObject);
		}

        var slots = this.GameStorage.Value.Slots.OrderByDescending(o => o.Modified).ToArray();
        var firstSlot = this.CreateButton(slots.First());

        var navigation = this.VerticalBar.navigation;
        navigation.selectOnLeft = firstSlot;
        this.VerticalBar.navigation = navigation;

        foreach (var slot in slots.Skip(1))
        {
            this.CreateButton(slot);
        }

        this.VerticalBar.Select();
    }

    private Selectable CreateButton(GameSlot slot)
    {
        var loadItem = GameObject.Instantiate<GameObject>(this.LoadItemTemplate, this.Root.transform);
        loadItem.GetComponent<LoadItemView>().SlotName = slot.Name;

        var selectable = loadItem.GetComponent<Selectable>();
        var navigation = selectable.navigation;
        navigation.selectOnRight = this.VerticalBar;
        selectable.navigation = navigation;

        return selectable;
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