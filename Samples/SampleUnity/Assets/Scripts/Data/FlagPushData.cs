
using System.Collections.Generic;
using SLua;

public class FlagPushData : ISubject<FlagPushData>
{

    //角标类型的值 大类+100  小类+1
    public const int FLAG_SKILL = 100;
    public const int FLAG_MAIL = 200;

    //角标的KEY 和 VALUE 都是int  VALUE非0则显示 0隐藏
    Dictionary<int, int> mAttributes = new Dictionary<int, int>();

    HashSet<IObserver<FlagPushData>> mObservers = new HashSet<IObserver<FlagPushData>>();
    Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();

    public void InitNetWork()
    {
        //监听网络消息
        if (TLNetManage.Instance.IsNet)
        {
            //TLNetManage.Instance.NetClient.GameSocket.onSuperScriptPush(PushFlagMsg);  
        }
    }

    private void PushFlagMsg(string msg)
    {
        //foreach(var ob in msg.s2c_data)
        //{
        //    int key = ob.type;
        //    int val = ob.number;
        //    // 对服务端发疯似的狂推做出的客户端层面的保护
        //    int lastV = 0;
        //    if(!mAttributes.TryGetValue(key, out lastV) || lastV != val)
        //    {
        //        SetAttribute(key, val, true);
        //    }
        //}
    }

    public int SetAttribute(int key, int val, bool notify = false)
    {
        int notify_key = key;
        mAttributes[notify_key] = val;
        if (notify)
            Notify(notify_key);
        return key;
    }


    public int GetFlagState(int s)
    {
        if (mAttributes.ContainsKey(s))
            return mAttributes[s];
        else
            return 0;
    }

    public void AttachObserver(IObserver<FlagPushData> ob)
    {
        mObservers.Add(ob);
    }

    public void DetachObserver(IObserver<FlagPushData> ob)
    {
        mObservers.Remove(ob);
    }

    public void AttachLuaObserver(string key, LuaTable t)
    {
        mLuaObservers[key] = t;
    }

    public void DetachLuaObserver(string key)
    {
        mLuaObservers.Remove(key);
    }

    public void Notify(int status)
    {
        foreach (var ob in mObservers)
        {
            ob.Notify(status, this);
        }

        foreach (var ob in mLuaObservers)
        {
            ob.Value.invoke("Notify", new object[] { status, this, ob.Value });
        }
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            mObservers.Clear();
            mLuaObservers.Clear();
        }
    }

}
