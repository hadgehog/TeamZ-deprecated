using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamZ.Assets.Code.DependencyInjection
{
	public struct Dependency<TValue>
		where TValue : class
	{
		private TValue value;
        private Guid currentScope;

        public TValue Value
		{
			get
			{
				if (this.value is null || DependencyContainer.Instance.Scope != this.currentScope)
				{
					this.value = Resolve();
                    this.currentScope = DependencyContainer.Instance.Scope;
                }

				return this.value;
			}
		}

		public static TValue Resolve()
		{
			return DependencyContainer.Instance.Resolve<TValue>();
		}

		public static implicit operator TValue(Dependency<TValue> dependency)
		{
			return dependency.Value;
		}


		public static implicit operator bool(Dependency<TValue> dependency)
		{
			return dependency.Value != null;
		}
	}
}
