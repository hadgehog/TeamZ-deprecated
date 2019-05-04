using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamZ.Assets.Code.Helpers.Extentions
{
    public static class LinqExtentions
    {
        public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<TValue> action)
        {
            foreach (var item in values)
            {
                action(item);
            }
        }
    }
}
