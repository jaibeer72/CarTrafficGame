using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
public class GameConfig : ScriptableObject, IObservable<GameConfigData>
{
    public GameConfigData configData;

    private List<IObserver<GameConfigData>> observers = new List<IObserver<GameConfigData>>();

    public IDisposable Subscribe(IObserver<GameConfigData> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<GameConfigData>(observers, observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(configData);
        }
    }

    public void UpdateConfigData(GameConfigData newConfigData)
    {
        configData = newConfigData;
        Notify();
    }

    private class Unsubscriber<GameConfigData> : IDisposable
    {
        private List<IObserver<GameConfigData>> _observers;
        private IObserver<GameConfigData> _observer;

        public Unsubscriber(List<IObserver<GameConfigData>> observers, IObserver<GameConfigData> observer)
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

