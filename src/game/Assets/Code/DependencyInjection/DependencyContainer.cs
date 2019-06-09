using System;
using System.Collections.Generic;
using Helpers;

namespace TeamZ.Assets.Code.DependencyInjection
{
	public class DependencyContainer : Singletone<DependencyContainer>
	{
		private Dictionary<Type, Lazy<object>> dependenies;
		private Dictionary<Type, ResetableLazy<object>> scopedDependenies;

        public Guid Scope { get; private set; }

        public DependencyContainer()
		{
			this.dependenies = new Dictionary<Type, Lazy<object>>();
			this.scopedDependenies = new Dictionary<Type, ResetableLazy<object>>();
            this.Scope = Guid.NewGuid();
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

        public void AddScoped<TDependency>()
            where TDependency : new()
        {
            var dependecyType = typeof(TDependency);
            this.scopedDependenies[dependecyType] = new ResetableLazy<object>(() => new TDependency());
        }

        public void NewScope()
        {
            this.Scope = Guid.NewGuid();
            foreach (var value in this.scopedDependenies.Values)
            {
                value.Reset();
            }
        }

        public TDependency Resolve<TDependency>()
		{
			var dependecyType = typeof(TDependency);
			if (this.dependenies.TryGetValue(dependecyType, out var lazy))
			{
			    return (TDependency)lazy.Value;
			}

            if (this.scopedDependenies.TryGetValue(dependecyType, out var resetableLazy))
            {
                return (TDependency)resetableLazy.Value;
            }

            throw new InvalidOperationException($"Dependency for type {dependecyType.FullName} is missing");
        }
    }
}
