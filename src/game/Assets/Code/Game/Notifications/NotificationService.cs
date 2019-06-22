using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.Notifications
{
    public class NotificationService : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public TextMeshProUGUI Tips;

        private Guid lastMessageId;

        public IDisposable ShowMessage(string message, bool isTip)
        {
            var messageId = Guid.NewGuid();
            this.lastMessageId = messageId;

            if (isTip)
            {
                this.Tips.text = message;
            }
            else
            {
                this.Text.text = message;
            }

            return Disposable.Create(() =>
            {
                if (this.lastMessageId != messageId)
                {
                    return;
                }

                if (isTip)
                {
                    this.Tips.text = string.Empty;
                }
                else
                {
                    this.Text.text = string.Empty;
                }
            });
        }

        public void ShowMessageWithDuration(string message, float seconds, bool isTip)
        {
            var disposer = this.ShowMessage(message, isTip);
            FixedObservable.Timer(TimeSpan.FromSeconds(seconds))
                .ObserveOnMainThread()
                .Subscribe(_ => disposer.Dispose());
        }

        public void ShowLongMessage(string message, bool isTip)
        {
            this.ShowMessageWithDuration(message, 5, isTip);
        }

        public void ShowShortMessage(string message, bool isTip)
        {
            this.ShowMessageWithDuration(message, 1, isTip);
        }
    }
}
