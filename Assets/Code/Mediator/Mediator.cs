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
		void Handle(TCommand command);
	}

	public class Mediator : Singletone<Mediator>
	{
		private Dictionary<Type, IHandler> handlers;

		public Mediator()
		{
			this.handlers = new Dictionary<Type, IHandler>();
		}

		public void Add<TCommand>(IHandler<TCommand> handler)
			where TCommand : ICommand
		{
			this.handlers.Add(typeof(TCommand), handler);
			MessageBroker.Default.Receive<TCommand>().
				ObserveOnMainThread().
				Subscribe(command => this.Handle(command));
		}

		public void Add<TCommand>(Action<TCommand> handler)
			where TCommand : ICommand
		{
			MessageBroker.Default.Receive<TCommand>().
				ObserveOnMainThread().
				Subscribe(command => handler(command));
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
