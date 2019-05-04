using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.UserInput
{
    public class UserInputProvider : IUserInputProvider
    {
        private IDisposable subcription;
        private readonly string horizontalAxisName;
        private readonly string verticalAxisName;
        private readonly string jumpButton;
        private readonly string punchButton;
        private readonly string kickButton;
        private readonly string submitButton;
        private readonly string cancelButton;

        public ReactiveProperty<float> Horizontal { get; }
            = new ReactiveProperty<float>();

        public ReactiveProperty<float> Vertical { get; }
            = new ReactiveProperty<float>();

        public ReactiveProperty<bool> Jump { get; }
            = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> Kick { get; }
            = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> Punch { get; }
            = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> Submit { get; }
            = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> Cancel { get; }
            = new ReactiveProperty<bool>();

        public UserInputProvider(string horizontalAxisName, string verticalAxisName, string jumpButton, string punchButton, string kickButton, string submitButton, string cancelButton)
        {
            this.horizontalAxisName = horizontalAxisName;
            this.verticalAxisName = verticalAxisName;
            this.jumpButton = jumpButton;
            this.punchButton = punchButton;
            this.kickButton = kickButton;
            this.submitButton = submitButton;
            this.cancelButton = cancelButton;
        }

        public IDisposable Activate()
        {
            this.subcription?.Dispose();
            this.subcription = Observable.EveryUpdate().Subscribe(_ =>
            {
                this.Horizontal.Value = Input.GetAxis(this.horizontalAxisName);
                this.Vertical.Value = Input.GetAxis(this.verticalAxisName);

                this.Jump.Value = Input.GetButton(this.jumpButton);
                this.Punch.Value = Input.GetButton(this.punchButton);
                this.Kick.Value = Input.GetButton(this.kickButton);
                this.Submit.Value = Input.GetButton(this.submitButton);
                this.Cancel.Value = Input.GetButton(this.cancelButton);
            });

            return this.subcription;
        }

        public void Deactivate()
        {
            this.subcription?.Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Horizontal.Dispose();
                    this.Vertical.Dispose();
                    this.Punch.Dispose();
                    this.Kick.Dispose();
                    this.Jump.Dispose();
                    this.Submit.Dispose();
                    this.Cancel.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
