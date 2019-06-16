using System;
using System.Collections.Generic;
using Helpers;
using TeamZ.Assets.GameSaving.Interfaces;

namespace TeamZ.Assets.Code.DependencyInjection
{
	public class DependencyContainer : Singletone<DependencyContainer>
	{
		private Dictionary<Type, object> dependenies;
		private Dictionary<Type, Func<object>> typeCreators;
		private Dictionary<Type, object> scopedDependenies;

        public Guid Scope { get; private set; }

        public DependencyContainer()
		{
			this.dependenies = new Dictionary<Type, object>();
			this.scopedDependenies = new Dictionary<Type, object>();
			this.typeCreators = new Dictionary<Type, Func<object>>();

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
			this.dependenies.Add(dependecyType, new TDependency());
		}

        public void AddScoped<TDependency>()
            where TDependency : new()
        {
            var dependecyType = typeof(TDependency);
            this.typeCreators.Add(dependecyType, () => new TDependency());
            this.scopedDependenies.Add(dependecyType, new TDependency());
        }

        public void NewScope()
        {
            this.Scope = Guid.NewGuid();
            foreach (var value in this.scopedDependenies.Values)
            {
                if (value is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            foreach (var creator in this.typeCreators)
            {
                this.scopedDependenies[creator.Key] = creator.Value();
            }
        }

        public TDependency Resolve<TDependency>()
		{
			var dependecyType = typeof(TDependency);
			if (this.dependenies.TryGetValue(dependecyType, out var lazy))
			{
			    return (TDependency)lazy;
			}

            if (this.scopedDependenies.TryGetValue(dependecyType, out var resetableLazy))
            {
                return (TDependency)resetableLazy;
            }

            throw new InvalidOperationException($"Dependency for type {dependecyType.FullName} is missing");
        }
    }
}
