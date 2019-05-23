using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace TeamZ.Assets.Code.Game.UserInput
{
    public interface IUserInputProvider : IDisposable
    {
        ReactiveProperty<float> Horizontal { get; }
        ReactiveProperty<float> Vertical { get; }

        ReactiveProperty<bool> Jump { get; }

        ReactiveProperty<bool> Kick { get; }
        ReactiveProperty<bool> Punch { get; }
        ReactiveProperty<bool> Activate { get; }
        ReactiveProperty<bool> Start { get; }
        ReactiveProperty<bool> Cancel { get; }

        IDisposable StartMonitoring();

        void StopMonitoring();
    }
}
