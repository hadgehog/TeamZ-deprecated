using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace TeamZ.Assets.Code.Game.UserInput
{
    public class KeyboardUserInputProvider : IUserInputProvider
    {
        private IDisposable subcription;

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

        public IDisposable Activate()
        {
            this.subcription?.Dispose();
            this.subcription = Observable.EveryUpdate().Subscribe(_ =>
            {
                this.Horizontal.Value = Input.GetAxis("Horizontal");
                this.Vertical.Value = Input.GetAxis("Vertical");

                this.Jump.Value = Input.GetButton("Jump");
                this.Punch.Value = Input.GetButton("Punch");
                this.Kick.Value = Input.GetButton("Kick");
                this.Submit.Value = Input.GetButton("Submit");
                this.Cancel.Value = Input.GetButton("Cancel");
            });

            return this.subcription;
        }

        public void Deactivate()
        {
            this.subcription?.Dispose();
        }
    }
}
