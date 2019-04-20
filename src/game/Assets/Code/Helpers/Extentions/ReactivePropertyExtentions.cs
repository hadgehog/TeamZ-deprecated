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
    }
}
