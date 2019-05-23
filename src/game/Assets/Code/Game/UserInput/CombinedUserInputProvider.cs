using System;
using System.Linq;
using UniRx;

namespace TeamZ.Assets.Code.Game.UserInput
{
    public class CombinedUserInputProvider : IUserInputProvider
    {
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

        public ReactiveProperty<bool> Activate { get; }
            = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> Start { get; }
            = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> Cancel { get; }
            = new ReactiveProperty<bool>();

        public IUserInputProvider[] Providers { get; }

        public CombinedUserInputProvider(params IUserInputProvider[] providers)
        {
            this.Providers = providers;
            foreach (var provider in this.Providers)
            {
                provider.Horizontal.Subscribe(o => this.Horizontal.Value = o);
                provider.Vertical.Subscribe(o => this.Vertical.Value = o);
                provider.Jump.Subscribe(o => this.Jump.Value = o);
                provider.Kick.Subscribe(o => this.Kick.Value = o);
                provider.Punch.Subscribe(o => this.Punch.Value = o);
                provider.Activate.Subscribe(o => this.Activate.Value = o);
                provider.Start.Subscribe(o => this.Start.Value = o);
                provider.Cancel.Subscribe(o => this.Cancel.Value = o);
            }
        }

        public IDisposable StartMonitoring()
        {
            var activations = this.Providers.Select(o => o.StartMonitoring()).ToArray();
            return Disposable.Create(() => activations.Dispose());
        }

        public void StopMonitoring()
        {
            foreach (var provider in this.Providers)
            {
                provider.StopMonitoring();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var provider in this.Providers)
                    {
                        provider.Dispose();
                    }
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
