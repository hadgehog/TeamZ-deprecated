using System;
using GameSaving.States;

namespace GameSaving.Interfaces
{
    public interface IMonoBehaviourWithState
    {
        Type GetStateType();

        MonoBehaviourState GetState();

        void SetState(MonoBehaviourState state);
    }

    public interface IMonoBehaviourWithState<TState> : IMonoBehaviourWithState
        where TState : MonoBehaviourState
    {
        new TState GetState();

        void SetState(TState state);
    }
}