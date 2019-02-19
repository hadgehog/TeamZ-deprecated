using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using TeamZ.Handlers;
using UniRx;

namespace TeamZ.Mediator
{
	public interface ICommand
	{

	}

	public interface IHandler
	{

	}

	public interface IHandler<TCommand> : IHandler
		where TCommand : ICommand
	{
		void Handle(TCommand characterDead);
	}

	public class Mediator : Singletone<Mediator>
	{
		private Dictionary<Type, IHandler> handlers;

		public Mediator()
		{
			this.handlers = new Dictionary<Type, IHandler>();

			MessageBroker.Default.Receive<ICommand>().
				ObserveOnMainThread().
				Subscribe(command => this.Handle(command));
		}

		public void Add<THandler>(THandler handler)
			where THandler : IHandler
		{
			this.handlers.Add(typeof(THandler), handler);
		}

		public void Handle<TCommand>(TCommand command)
			where TCommand : ICommand
		{
			if(!this.handlers.TryGetValue(command.GetType(), out var handler))
			{
				throw new InvalidOperationException("Handler is missing");
			}

			((IHandler<TCommand>)handler).Handle(command);
		}
	}
}
