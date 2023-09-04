using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "ScriptableObjects/PlayerStatsData")]
public class PlayerStatsData : ScriptableObject, IObservable<PlayerStats>
{
    public PlayerStats stats;
    private List<IObserver<PlayerStats>> observers = new List<IObserver<PlayerStats>>();
    public IDisposable Subscribe(IObserver<PlayerStats> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<PlayerStats>(observers, observer);
    }

    public void Notify(PlayerStats stats)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(stats);
        }
    }

    public void ResetDataForObservable()
    {
        stats.Reset();
    }

    private class Unsubscriber<PlayerStats> : IDisposable
    {
        private List<IObserver<PlayerStats>> _observers;
        private IObserver<PlayerStats> _observer;

        public Unsubscriber(List<IObserver<PlayerStats>> observers, IObserver<PlayerStats> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
