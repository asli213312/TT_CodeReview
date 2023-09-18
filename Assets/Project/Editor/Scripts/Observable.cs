using System;

public interface IObservable
{
    void RegisterObserver(Mole observer);
    void RemoveObserver(Mole observer);
}

public interface IObserver
{
    void OnObservableUpdate(Spawner observable);
}
