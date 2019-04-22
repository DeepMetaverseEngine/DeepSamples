using SLua;

public interface ISubject<T>
{
    void AttachObserver(IObserver<T> ob);

    void DetachObserver(IObserver<T> ob);

    void AttachLuaObserver(string k, LuaTable t);

    void DetachLuaObserver(string k);

    void Notify(int status);
}

public interface ISubjectExt<T>
{
    void AttachObserver(IObserverExt<T> ob);

    void DetachObserver(IObserverExt<T> ob);

    void Notify(int status, object opt);
}

public interface ISubject<T, P>
{
    void AttachObserver(IObserver<T, P> ob);

    void DetachObserver(IObserver<T, P> ob);

    void AttachLuaObserver(LuaTable t);

    void DetachLuaObserver(LuaTable t);

    void Notify(P message);
}