using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class FixedObservable : MonoBehaviour
{
    private readonly static Subject<Unit> everyUpdate = new Subject<Unit>();

    static FixedObservable()
    {
        new GameObject("~UIObserver").AddComponent<FixedObservable>();
    }

    public static IObservable<Unit> EveryUpdate()
    {
        return everyUpdate;
    }

    public static IObservable<Unit> Timer(TimeSpan time)
    {
        var subject = new Subject<Unit>();

        var timer = new System.Timers.Timer(time.TotalMilliseconds);
        timer.Elapsed += Elapsed;
        timer.Start();

        return subject;

        void Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Elapsed -= Elapsed;
            timer.Dispose();
            subject.OnNext(Unit.Default);
            subject.OnCompleted();
        }
    }


    public void Update()
    {
        everyUpdate.OnNext(Unit.Default);
    }
}

