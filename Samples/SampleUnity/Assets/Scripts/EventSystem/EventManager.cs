using System.Collections.Generic;
using System;
using SLua;

//订阅QueuedAPICall的回调的方法
[CustomLuaClassAttribute]
public class EventManager
{
    public readonly static Dictionary<string, object> defaultParam = new Dictionary<string, object>();
    [DoNotToLua]
    private readonly static Dictionary<string, int> paramInt = new Dictionary<string, int>();
    [DoNotToLua]
    private readonly static Dictionary<string, string> paramStr = new Dictionary<string, string>();
    [DoNotToLua]
    private readonly static Dictionary<string, object> paramObj = new Dictionary<string, object>();

    [DoNotToLua]
    public static Dictionary<string, int> SimpleParam(string key, int value)
    {
        paramInt.Clear();
        paramInt.Add(key, value);
        return paramInt;
    }
    [DoNotToLua]
    public static Dictionary<string, string> SimpleParam(string key, string value)
    {
        paramStr.Clear();
        paramStr.Add(key, value);
        return paramStr;
    }
    [DoNotToLua]
    public static Dictionary<string, object> SimpleParam(string key, object value)
    {
        paramObj.Clear();
        paramObj.Add(key, value);
        return paramObj;
    }

    public delegate void EvtCallback(ResponseData res);

    public struct ResponseData
    {
        public bool ok;
        public string err;
        public List<object> data;
    }
    //private Queue<ResponseData> mResQueue = new Queue<ResponseData> ();

    //注册回调的数组
    private static Dictionary<string, HashSet<EvtCallback>> mSubscribed
        = new Dictionary<string, HashSet<EvtCallback>>();
    private static int mInCallLoops = 0;
    private static List<KeyValuePair<string, EvtCallback>> mItemsToRemove = new List<KeyValuePair<string, EvtCallback>>();
    private static Dictionary<string, HashSet<EvtCallback>> mReloginUnsubs
        = new Dictionary<string, HashSet<EvtCallback>>();
    //==========================================================
    //==========================================================
    [DoNotToLua]
    public static void Subscribe(string eventName, EvtCallback fun, bool UnsubOnReset = true)
    {
        if (!mSubscribed.ContainsKey(eventName))
            mSubscribed.Add(eventName, new HashSet<EvtCallback>());

        mSubscribed[eventName].Add(fun);
        //Debug.Log("[Event] Subscribe:" + eventName + ", " + fun.ToString() );
        if (UnsubOnReset)
        {
            if (!mReloginUnsubs.ContainsKey(eventName))
                mReloginUnsubs.Add(eventName, new HashSet<EvtCallback>());

            mReloginUnsubs[eventName].Add(fun);
        }
    }
    [DoNotToLua]
    public static void Unsubscribe(string eventName, EvtCallback fun)
    {
        if (!mSubscribed.ContainsKey(eventName))
            return;

        if (mInCallLoops > 0)  //如果在回调中调用Unsub
            mItemsToRemove.Add(new KeyValuePair<string, EvtCallback>(eventName, fun));
        else
        {
            mSubscribed[eventName].Remove(fun);
            if (mSubscribed[eventName].Count == 0)
                mSubscribed.Remove(eventName);
            HashSet<EvtCallback> item;
            if (mReloginUnsubs.TryGetValue(eventName, out item))
            {
                item.Remove(fun);
                if (item.Count == 0)
                {
                    mReloginUnsubs.Remove(eventName);
                }
            }
            //Debug.Log("[Event] Unsubscribe OK:" + eventName + ", " + fun.ToString());
        }

    }
    [DoNotToLua]
    public static void Reset()
    {
        mInCallLoops = 0; //因为如果在eventmanager执行中报错，会导致回到login是callloop不等于0，导致无法反注册

        foreach (var aevent in mReloginUnsubs)
        {
            foreach (var fun in aevent.Value)
            {
                Unsubscribe(aevent.Key, fun);
            }
        }

        //UnsubscribeAllLua();
    }
    [DoNotToLua]
    public static void ForceClearAll()
    {
        mSubscribed.Clear();
        mItemsToRemove.Clear();
        mInCallLoops = 0;

        //ForceClearAllLua();
    }

    //==========================================================
    //private static List<object> ListObj = new List<object>();
    [DoNotToLua]
    public static void Fire(string eventName, params object[] data)
    {
        var args = new List<object>();
        args.Add(eventName);
        //ListObj.Clear();
        //ListObj.Add(eventName);
        for (int i = 0; i < data.Length; i++)
        {
            args.Add(data[i]);
        }
        EventManager.Fire(null, args);
        //ListObj.Clear();
    }
    [DoNotToLua]
    public static void Fire(string err, List<object> evtData)
    {
        if (evtData == null || evtData.Count == 0 || !(evtData[0] is string))
            throw new System.Exception("Fire Event but EventName is NULL.");

        var eventName = evtData[0] as string;
        if (mSubscribed.ContainsKey(eventName))
        {
            //Debug.Log("[Event] Fired: " + eventName);
            ResponseData res;
            res.data = evtData;
            res.ok = err == null;
            res.err = err;
            mInCallLoops = mInCallLoops + 1;

            try
            {
                foreach (var v in mSubscribed[eventName])
                {
                    v(res);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                mInCallLoops = mInCallLoops - 1;
                if (mInCallLoops <= 0)
                {
                    mInCallLoops = 0;
                    foreach (var item in mItemsToRemove)
                    {
                        if (mSubscribed.ContainsKey(item.Key))
                        {
                            //因为有嵌套，所以remove时可能是子嵌套登记的event，所以要子嵌套告诉你eventname
                            if (mSubscribed[item.Key].Contains(item.Value))
                                mSubscribed[item.Key].Remove(item.Value);
                            if (mSubscribed[item.Key].Count == 0)
                                mSubscribed.Remove(item.Key);
                            HashSet<EvtCallback> it;
                            if (mReloginUnsubs.TryGetValue(item.Key, out it))
                            {
                                it.Remove(item.Value);

                                if (it.Count == 0)
                                {
                                    mReloginUnsubs.Remove(item.Key);
                                }
                            }
                            //Debug.Log("[Event] Unsubscribe OK:" + item.Key + ", " + item.ToString());
                        }

                    }
                    mItemsToRemove.Clear();
                }
            }
        }
        else
        {
            //Debug.Log("[Event] Fired, but no reicver: " + eventName);
        }
    }

    //============================================================
    //Lua Call Back is function(sEvntName, tJson, tReqdata)

    //public const string LIB_NAME = "EventManager.cs"; // 库的名称, 可以是任意字符串

    //public static int OpenLib(ILuaState lua) // 库的初始化函数
    //{
    //    var define = new NameFuncPair[]
    //    {
    //        new NameFuncPair("Subscribe", Subscribe),
    //        new NameFuncPair("Unsubscribe", Unsubscribe),
    //        new NameFuncPair("Fire", Fire),
    //        new NameFuncPair("DelayedStartFire", DelayedStartFire),
    //        new NameFuncPair("DelayedStopFire", DelayedStopFire),
    //    };

    //    lua.L_NewLib(define);
    //    return 1;
    //}

    //订阅事件的LuaState
    struct LuaEvents
    {
        //public System.WeakReference WeekState; //StatePoint
        // public int EventRef;                   //EventPoint
        public string name;
        public LuaFunction callback;
    }
    //考虑之后决定只支持单个lua vm
    private static Dictionary<string, LuaEvents> mLuaSubscribed =
        new Dictionary<string, LuaEvents>();
    private static int mIsInLuaCallLoop = 0;
    private static List<string> mLuaItemsToRemove = new List<string>();
    private static int luaRefCount = 0;
    private static string luaRefCountPrefix =
        "__luaRefCountPrefix__";

    //反注册Lua所有的事件，并去c#的EventManager里反注册
    [DoNotToLua]
    public static void UnsubscribeAllLua()
    {
        foreach (var item in mLuaSubscribed)
        {
            mLuaItemsToRemove.Add(item.Key);
        }
        foreach (var item in mLuaItemsToRemove)
        {
            mLuaSubscribed[item].callback.Dispose();
            mLuaSubscribed.Remove(item);
            //Unsubscribe(item.Key, LuaEventCallBack);
        }
        mLuaItemsToRemove.Clear();
        mIsInLuaCallLoop = 0;
    }

    //强制清空，不做任何处理
    private static void ForceClearAllLua()
    {
        UnsubscribeAllLua();
    }

    private static void LuaEventCallBack(ResponseData res)
    {
        string eventName = res.data[0] as string;
        if (mLuaSubscribed.ContainsKey(eventName))
        {
            var eventStats = mLuaSubscribed[eventName];
            mIsInLuaCallLoop = mIsInLuaCallLoop + 1;
            try
            {
                eventStats.callback.call(eventName, res.data.ToArray());
                //LuaScriptMgr.Instance.CallLuaFunction(eventStats.name, eventStats.EventRef, res.data.ToArray());
            }
            catch
            {
                //LuaScriptMgr.Instance.CallLuaFunction("debug.traceback", 
                //    new object[] { null, e.Message + "\n" + e.StackTrace });
                throw;
            }
            finally
            {
                mIsInLuaCallLoop = mIsInLuaCallLoop - 1;
                if (mIsInLuaCallLoop <= 0)
                {
                    mIsInLuaCallLoop = 0;
                    foreach (var item in mLuaItemsToRemove)
                    {
                        mLuaSubscribed.Remove(item);
                        Unsubscribe(item, LuaEventCallBack);

                    }
                    mLuaItemsToRemove.Clear();
                }
            }
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    [StaticExport]
    public static int Subscribe(IntPtr L)
    {
        try
        {
            //for (int i = 1; i <= LuaDLL.lua_gettop(L); ++i)
            //{
            //    object o = LuaScriptMgr.GetLuaObject(L, i);
            //    int j = 1;
            //}
            //string eventName = LuaScriptMgr.GetLuaString(L, 1); // 第一个参数
            //int luaRef;
            //LuaScriptMgr.StoreLuaFunction(L, 2, out luaRef);
            string eventName;
            LuaObject.checkType(L, 1, out eventName);
            LuaFunction func;
            LuaObject.checkType(L, 2, out func);


            LuaEvents evtitem = new LuaEvents();
            //evtitem.WeekState = new System.WeakReference(L, false);
            //evtitem.EventRef = luaRef;
            evtitem.name = eventName + luaRefCount.ToString();
            luaRefCount++;
            evtitem.callback = func;
            mLuaSubscribed.Add(eventName, evtitem);

            Subscribe(eventName, LuaEventCallBack);

            LuaObject.pushValue(L, true);
            return 1;
        }
        catch (Exception e)
        {
            return LuaObject.error(L, e);
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    [StaticExport]
    public static int Unsubscribe(IntPtr L)
    {
        try
        {
            //string eventName = LuaScriptMgr.GetLuaString(L, 1); // 第一个参数
            string eventName;
            LuaObject.checkType(L, 1, out eventName);

            if (!mLuaSubscribed.ContainsKey(eventName))
                return 0;

            var eventStats = mLuaSubscribed[eventName];

            if (mIsInLuaCallLoop > 0)//如果是在callback中调用Unsub
            {
                mLuaItemsToRemove.Add(eventName);
            }
            else
            {
                mLuaSubscribed[eventName].callback.Dispose();
                mLuaSubscribed.Remove(eventName);
                Unsubscribe(eventName, LuaEventCallBack);
            }

            LuaObject.pushValue(L, true);
            return 1;
        }
        catch (Exception e)
        {
            return LuaObject.error(L, e);
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    [StaticExport]
    public static int Fire(IntPtr L)
    {
        try
        {
            string eventName;
            LuaObject.checkType(L, 1, out eventName);

            LuaTable t;
            LuaObject.checkType(L, 2, out t);
            Dictionary<object, object> table = new Dictionary<object, object>();
            var iter = t.GetEnumerator();
            while (iter.MoveNext())
            {
                table[iter.Current.key] = iter.Current.value;
            }
            t.Dispose();

            EventManager.Fire(eventName, table);

            LuaObject.pushValue(L, true);
            return 1;
        }
        catch (Exception e)
        {
            return LuaObject.error(L, e);
        }
    }

    //public static int DelayedStopFire(ILuaState lua)
    //{
    //    DelayedEventManager.StopFire();

    //    return 0;
    //}

    //public static int DelayedStartFire(ILuaState lua)
    //{
    //    DelayedEventManager.StartFire();

    //    return 0;
    //}

    //static LuaField[] fields = new LuaField[]
    //{
    //    new LuaField("defaultParam", get_defaultParam, null),
    //};

    //public static LuaMethod[] regs = new LuaMethod[]
    //{
    //    new LuaMethod("Subscribe", Subscribe),
    //    new LuaMethod("Unsubscribe", Unsubscribe),
    //    new LuaMethod("Fire", Fire),
    //};

    //public static void Register(IntPtr L)
    //{
    //    LuaScriptMgr.RegisterLib(L, "EventManager", typeof(EventManager), regs, fields, null);
    //}

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    [StaticExport]
    static int get_defaultParam(IntPtr L)
    {
        System.Object o;
        LuaObject.checkType(L, 1, out o);
        EventManager obj = (EventManager)o;

        if (obj == null)
        {
            LuaTypes types = LuaDLL.lua_type(L, 1);

            if (types == LuaTypes.LUA_TTABLE)
            {
                LuaDLL.luaL_error(L, "unknown member name defaultParam");
            }
            else
            {
                LuaDLL.luaL_error(L, "attempt to index defaultParam on a nil value");
            }
        }
        LuaObject.pushObject(L, EventManager.defaultParam);
        return 1;
    }
}


