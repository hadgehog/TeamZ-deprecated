using System;
using System.Collections.Generic;
using Helpers;

namespace TeamZ.Assets.Code.DependencyInjection
{
	public class DependencyContainer : Singletone<DependencyContainer>
	{
		private Dictionary<Type, Lazy<object>> dependenies;

		public DependencyContainer()
		{
			this.dependenies = new Dictionary<Type, Lazy<object>>();
		}

		public void AddOrSet<TDependency>(TDependency depedency)
		{
			var dependecyType = typeof(TDependency);
			this.dependenies[dependecyType] = new Lazy<object>(() => depedency);
		}

		public void Add<TDependency>()
			where TDependency : new()
		{
			var dependecyType = typeof(TDependency);
			this.dependenies[dependecyType] = new Lazy<object>(() => new TDependency());
		}

		public TDependency Resolve<TDependency>()
		{
			var dependecyType = typeof(TDependency);
			var lazy = this.dependenies[dependecyType];
			if (lazy is null)
			{
				throw new InvalidOperationException($"Dependency for type {dependecyType.FullName} is missing");
			}

			return (TDependency)lazy.Value;
		}
	}
}
