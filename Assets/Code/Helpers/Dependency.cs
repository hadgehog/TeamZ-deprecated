using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Helpers
{
	public struct Dependency<TValue>
		where TValue : MonoBehaviour
	{
		private TValue value;

		public TValue Value
		{
			get
			{
				if (this.value == null)
				{
					this.value = GameObject.FindObjectOfType<TValue>();
				}

				return this.value;
			}
		}
	}
}