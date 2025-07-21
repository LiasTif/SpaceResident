public interface IDataHandlingNotifier<T>
{
    void Attach(T observer);
    void Detach(T observer);
    void RaiseSave();
    void RaiseLoad();
}