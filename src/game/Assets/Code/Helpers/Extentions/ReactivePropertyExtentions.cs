using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace UniRx
{
    public static class ReactivePropertyExtentions
    {
        public static IObservable<bool> True(this ReactiveProperty<bool> reactiveProperty)
        {
            return reactiveProperty.Where(o => o);
        }

        public static IObservable<Unit> HoldFor(this ReactiveProperty<bool> reactiveProperty, TimeSpan time)
        {
            return reactiveProperty.HoldFor(false, time);
        }

        public static IObservable<Unit> HoldFor<TValue>(this ReactiveProperty<TValue> reactiveProperty, TValue negativeValue, TimeSpan time)
        {
            var observable = new Subject<Unit>();
            IDisposable timer = null;
            reactiveProperty
                .Where(o => !o.Equals(negativeValue))
                .Subscribe(o =>
                {
                    timer?.Dispose();
                    timer = Observable.Timer(time).Subscribe(async _ =>
                    {
                        observable.OnNext(Unit.Default);
                    });
                });

            reactiveProperty
                .Where(o => o.Equals(negativeValue))
                .Subscribe(_ => timer?.Dispose());

            return observable.AsObservable();
        }
    }
}
