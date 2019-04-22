
public interface IObserver<T>
{
    bool Notify(long status, T subject);
    void BeforeDetach(T subject);
    void BeforeAttach(T subject);

}

public interface IObserverExt<T>
{
    void Notify(int status,T subject, object opt );

}

public interface IObserver<T, P>
{
    bool Notify(T subject, P message);
    void BeforeDetach(T subject);
    void BeforeAttach(T subject);
}