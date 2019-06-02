using System;
using GameSaving.States;
using UnityEditor;

namespace GameSaving.Interfaces
{
    public interface IMonoBehaviourWithState
    {
        Type GetStateType();

        void SetState(MonoBehaviourState state);
        MonoBehaviourState GetState();


        void Loaded();
    }

    public interface IMonoBehaviourWithState<TState> : IMonoBehaviourWithState
        where TState : MonoBehaviourState
    {
        new TState GetState();

        void SetState(TState state);
    }
}