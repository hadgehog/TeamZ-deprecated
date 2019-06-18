using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace UniRx
{
    public static class ReactivePropertyExtentions
    {
        public static IObservable<bool> True(this IObservable<bool> reactiveProperty)
        {
            return reactiveProperty.Where(o => o);
        }

        public static IObservable<Unit> HoldFor(this IObservable<bool> reactiveProperty, TimeSpan time)
        {
            return reactiveProperty.HoldFor(false, time);
        }

        public static IObservable<Unit> HoldFor<TValue>(this IObservable<TValue> reactiveProperty, TValue negativeValue, TimeSpan time)
        {
            var observable = new Subject<Unit>();
            var id = Guid.NewGuid();
            reactiveProperty
                .Where(o => !o.Equals(negativeValue))
                .Subscribe(async o =>
                {
                    var initialGuid = id;
                    await Task.Delay((int)time.TotalMilliseconds);
                    if (initialGuid == id)
                    {
                        observable.OnNext(Unit.Default);
                    }
                });

            reactiveProperty
                .Where(o => o.Equals(negativeValue))
                .Subscribe(_ => id = Guid.NewGuid());

            return observable.AsObservable();
        }
    }
}
