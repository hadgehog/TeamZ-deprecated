using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SaveItemView : MonoBehaviour
{
	public string SlotName;
	public Text SlotNameView;

	public Subject<SaveItemView> Clicks { get; } = new Subject<SaveItemView>();

	private void Start()
	{
		this.SlotNameView.text = this.SlotName;
	}

	public void Click()
	{
		this.Clicks.OnNext(this);
	}

	private void OnDestroy()
	{
		this.Clicks.OnCompleted();
	}
}