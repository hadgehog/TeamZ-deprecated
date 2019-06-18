using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.Notifications
{
    public class NotificationService : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        private Guid lastMessageId;

        public IDisposable ShowMessage(string message)
        {
            var messageId = Guid.NewGuid();
            this.lastMessageId = messageId;

            this.Text.text = message;

            return Disposable.Create(() =>
            {
                if (this.lastMessageId != messageId)
                {
                    return;
                }

                this.Text.text = string.Empty;
            });
        }

        public void ShowMessageWithDuration(string message, float seconds)
        {
            var disposer = this.ShowMessage(message);
            FixedObservable.Timer(TimeSpan.FromSeconds(seconds))
                .ObserveOnMainThread()
                .Subscribe(_ => disposer.Dispose());
        }

        public void ShowLongMessage(string message)
        {
            this.ShowMessageWithDuration(message, 5);
        }

        public void ShowShortMessage(string message)
        {
            this.ShowMessageWithDuration(message, 1);
        }
    }
}
