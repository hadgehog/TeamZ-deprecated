using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UIObservable : MonoBehaviour
{
    private readonly static Subject<Unit> everyUpdate = new Subject<Unit>();

    static UIObservable()
    {
        new GameObject("~UIObserver").AddComponent<UIObservable>();
    }

    public static IObservable<Unit> EveryUpdate()
    {
        return everyUpdate;
    }

    public void Update()
    {
        everyUpdate.OnNext(Unit.Default);
    }

}