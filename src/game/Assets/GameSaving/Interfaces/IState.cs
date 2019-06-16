using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamZ.Assets.Code.Game.Players;
using TeamZ.Assets.Code.Game.UserInput;
using ZeroFormatter;

namespace TeamZ.Assets.GameSaving.Interfaces
{
    public enum StateKind
    {
        PlayerService
    }

    [ZeroFormattable]
    [Union(typeof(PlayerServiceState))]
    public abstract class State
    {
        [UnionKey]
        public abstract StateKind Kind { get; }
    }

    public interface IStateProvider
    {
        State GetState();

        void SetState(State state);
    }

    public interface IStateProvider<TState> : IStateProvider
        where TState : State
    {
        new TState GetState();

        void SetState(TState state);
    }

    public abstract class StateProvider<TState> : IStateProvider<TState>
        where TState : State
    {
        public abstract TState GetState();

        public abstract void SetState(TState state);

        void IStateProvider.SetState(State state)
        {
            this.SetState((TState)state);
        }

        State IStateProvider.GetState()
        {
            return this.GetState();
        }
    }
}
