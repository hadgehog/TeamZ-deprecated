using GameSaving;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TeamZ.Assets.UI.Load
{
	public class LoadItem : MonoBehaviour
	{
		public Text SlotView;
		public string SlotName { get; set; }

		public void Start()
		{
			this.SlotView.text = this.SlotName;
		}

		public void Load()
		{
			MessageBroker.Default.Publish(new LoadFromSlotName { SlotName = this.SlotName });
		}
	}
}