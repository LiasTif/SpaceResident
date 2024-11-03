public interface ISpeedObservable
{
    void AddObserver(ISpeedObserver o);
    void RemoveObserver(ISpeedObserver o);
    void NotifyObservers();
}