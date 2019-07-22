using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamZ.Assets.Code.DependencyInjection
{
    public class ResetableLazy<TValue>
    {
        private readonly Func<TValue> creator;
        private TValue value;

        public ResetableLazy(Func<TValue> creator)
        {
            this.creator = creator;
        }

        public TValue Value
        {
            get
            {
                if (this.value.Equals(default))
                {
                    this.value = this.creator();
                }

                return this.value;
            }
        }

        public void Reset()
        {
            this.value = default;
        }
    }
}
