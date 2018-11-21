using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Helpers
{
	public struct UnityDependency<TValue>
		where TValue : MonoBehaviour
	{
		private TValue value;

		public TValue Value
		{
			get
			{
				if (!this.value)
				{
					this.value = GameObject.FindObjectOfType<TValue>();
				}

				return this.value;
			}
		}

		public static implicit operator TValue(UnityDependency<TValue> dependency)
		{
			return dependency.Value;
		}

		public static implicit operator GameObject(UnityDependency<TValue> dependency)
		{
			return dependency.Value.gameObject;
		}

		public static implicit operator Transform(UnityDependency<TValue> dependency)
		{
			return dependency.Value.transform;
		}

		public static implicit operator bool(UnityDependency<TValue> dependency)
		{
			return dependency.Value != null;
		}
	}
}