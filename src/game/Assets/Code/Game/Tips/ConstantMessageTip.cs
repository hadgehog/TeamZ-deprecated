using System;
using Assets.Code.Helpers;
using TeamZ.Assets.Code.Game.Notifications;
using TeamZ.Assets.Code.Game.Tips.Core;

namespace TeamZ.Assets.Code.Game.Tips
{
    public class ConstantMessageTip : Tip
    {
        public string Message;
        private UnityDependency<NotificationService> Notifications;
        private IDisposable message;

        public override void Activate()
        {
            this.message = this.Notifications.Value.ShowMessage(this.Message, true);
        }

        public override void Deactivate()
        {
            this.message?.Dispose();
        }

        private void OnDestroy()
        {
            this.message?.Dispose();
        }
    }
}
