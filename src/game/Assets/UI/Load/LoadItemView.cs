using TeamZ.Assets.Code.Game.Messages.GameSaving;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TeamZ.Assets.UI.Load
{
    public class LoadItemView : MonoBehaviour
    {
        public Text SlotView;
        public string SlotName { get; set; }

        public void Start()
        {
            this.SlotView.text = this.SlotName;
        }

        public void Load()
        {
            MessageBroker.Default.Publish(new LoadGameRequest { SlotName = this.SlotName });
        }
    }
}