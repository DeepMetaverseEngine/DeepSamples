using UnityEngine;
using System.Collections.Generic;

public class FingerGesturesCtrl : FingerControlBase
{

    public FingerGestures fingerGestures;

    public delegate void OnKeyDownEvent(KeyCode key);
    public event OnKeyDownEvent OnKeyDown;

    public delegate void OnMoveKeyEvent(float dx, float dy, float px, float py);
    public event OnMoveKeyEvent OnMoveKeyDown;
    public event OnMoveKeyEvent OnMoveKeyUp;

    public delegate void OnGlobalTouchEvent(GameObject gameObject, Vector2 point);
    private OnGlobalTouchEvent OnGlobalTouchDown;
    private OnGlobalTouchEvent OnGlobalTouchUp;

    private SortedList<int, List<FingerHandlerInterface>> mFingerQueue = new SortedList<int, List<FingerHandlerInterface>>();
    private SortedList<int, List<PinchHandlerInterface>> mPinchQueue = new SortedList<int, List<PinchHandlerInterface>>();
    
    private Dictionary<string, GlobalTouchData> mGlobalTouchQueue = new Dictionary<string, GlobalTouchData>();
    private int mGlobalTouchKeySerial = 0;

    public bool TouchEnable { get; private set; }

    public int FingerCount { get; set; }

    private bool isPinching;

    public FingerGesturesCtrl()
    {
        TouchEnable = true;
    }

    public void AddFingerHandler(FingerHandlerInterface handler, int priority)
    {
        if (!mFingerQueue.ContainsKey(priority))
        {
            mFingerQueue[priority] = new List<FingerHandlerInterface>();
        }
        mFingerQueue[priority].Add(handler);
    }

    public void RemoveFingerHandler(FingerHandlerInterface handler)
    {
        foreach (KeyValuePair<int, List<FingerHandlerInterface>> q in mFingerQueue)
        {
            List<FingerHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                handlers.Remove(handler);
            }
        }
    }

    public void AddPinchHandler(PinchHandlerInterface handler, int priority)
    {
        if (!mPinchQueue.ContainsKey(priority))
        {
            mPinchQueue[priority] = new List<PinchHandlerInterface>();
        }
        mPinchQueue[priority].Add(handler);
    }

    public void RemovePinchHandler(PinchHandlerInterface handler)
    {
        foreach (KeyValuePair<int, List<PinchHandlerInterface>> q in mPinchQueue)
        {
            List<PinchHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                handlers.Remove(handler);
            }
        }
    }

    public string AddGlobalTouchDownHandler(string key, OnGlobalTouchEvent handler)
    {
        string realKey = key + "_down" + mGlobalTouchKeySerial++;
        GlobalTouchData data = new GlobalTouchData();
        data.DownEvent = handler;
        mGlobalTouchQueue[realKey] = data;
        return realKey;
    }

    public string AddGlobalTouchUpHandler(string key, OnGlobalTouchEvent handler)
    {
        string realKey = key + "_up" + mGlobalTouchKeySerial++;
        GlobalTouchData data = new GlobalTouchData();
        data.UpEvent = handler;
        mGlobalTouchQueue[realKey] = data;
        return realKey;
    }

    public void RemoveGlobalTouchDownHandler(string key)
    {
        if (mGlobalTouchQueue.ContainsKey(key))
        {
            mGlobalTouchQueue.Remove(key);
        }
    }

    public void RemoveGlobalTouchUpHandler(string key)
    {
        if (mGlobalTouchQueue.ContainsKey(key))
        {
            mGlobalTouchQueue.Remove(key);
        }
    }

    /// <summary>
    /// 点击事件down.
    /// </summary>
    /// <param name="fingerIndex"></param>
    /// <param name="fingerPos"></param>
    public override void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        FingerCount++;
        if (!TouchEnable) return;
        if (IsOutOfGameScreen()) { return; }
        bool isBreak = false;
        foreach (KeyValuePair<int, List<FingerHandlerInterface>> q in mFingerQueue)
        {
            List<FingerHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                for (int i = 0; i < handlers.Count; ++i)
                {
                    if (handlers[i].OnFingerDown(fingerIndex, fingerPos))
                    {
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak)
                    break;
            }
        }
        //foreach (var globalData in mGlobalTouchQueue.Values)
        //{
        //    if(globalData.FingerIndex == -1)
        //    {
        //        globalData.FingerIndex = fingerIndex;
        //        if(globalData.DownEvent != null)
        //            globalData.DownEvent.Invoke(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject, fingerPos);
        //    }
        //}

        List<string> touchkeylist = new List<string>(mGlobalTouchQueue.Keys);
        for (int i = 0; i < touchkeylist.Count; i++)
        {
            var globalData = mGlobalTouchQueue[touchkeylist[i]];
            if (globalData.FingerIndex == -1)
            {
                globalData.FingerIndex = fingerIndex;
                if (globalData.DownEvent != null)
                    globalData.DownEvent.Invoke(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject, fingerPos);
            }
        }
    }

    /// <summary>
    /// 拖拽事件.
    /// </summary>
    /// <param name="fingerIndex"></param>
    /// <param name="fingerPos"></param>
    /// <param name="delta"></param>
    public override void FingerGestures_OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (!TouchEnable) return;
        bool isBreak = false;
        foreach (KeyValuePair<int, List<FingerHandlerInterface>> q in mFingerQueue)
        {
            List<FingerHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                for (int i = 0; i < handlers.Count; ++i)
                {
                    if (handlers[i].OnDragMove(fingerIndex, fingerPos, delta))
                    {
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak)
                    break;
            }
        }
    }

    /// <summary>
    /// 弹起事件.
    /// </summary>
    /// <param name="fingerIndex"></param>
    /// <param name="fingerPos"></param>
    /// <param name="timeHeldDown"></param>
    override public void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        FingerCount = FingerCount - 1 < 0 ? 0 : FingerCount - 1;
        //if (!TouchEnable) return;
        bool isBreak = false;
        FingerHandlerInterface touchInterface = null;
        foreach (KeyValuePair<int, List<FingerHandlerInterface>> q in mFingerQueue)
        {
            List<FingerHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                for (int i = 0; i < handlers.Count; ++i)
                {
                    if (handlers[i].OnFingerUp(fingerIndex, fingerPos, timeHeldDown))
                    {
                        touchInterface = handlers[i];
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak)
                    break;
            }
        }
        string[] keys = new string[mGlobalTouchQueue.Count];
        mGlobalTouchQueue.Keys.CopyTo(keys, 0);
        foreach (var key in keys)
        {
            GlobalTouchData globalData = mGlobalTouchQueue[key];
            if (globalData.FingerIndex == fingerIndex)
            {
                globalData.FingerIndex = fingerIndex;
                if (globalData.UpEvent != null)
                {
                    GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
                    if (!obj && touchInterface is Component)
                    {
                        obj = ((Component)touchInterface).gameObject;
                    }
                    globalData.UpEvent.Invoke(obj, fingerPos);
                }
                    
            }
        }

        FingerClear();
    }

    private void FingerClear()
    {
        if (FingerCount == 0 && !isPinching)
        {
            foreach (KeyValuePair<int, List<FingerHandlerInterface>> q in mFingerQueue)
            {
                List<FingerHandlerInterface> handlers = q.Value;
                if (handlers != null)
                {
                    for (int i = 0; i < handlers.Count; ++i)
                    {
                        handlers[i].OnFingerClear();
                    }
                }
            }
            foreach (var globalData in mGlobalTouchQueue.Values)
            {
                globalData.FingerIndex = -1;
            }
        }
    }

    public void SetTouchEnable(bool enable, bool forceClearFinger = false)
    {
        TouchEnable = enable;
        if (forceClearFinger && !enable)
        {
            FingerCount = 0;
            FingerClear();
        }
    }

    /// <summary>
    /// 拖拽开始.
    /// </summary>
    /// <param name="fingerIndex"></param>
    /// <param name="fingerPos"></param>
    /// <param name="startPos"></param>
    public override void FingerGestures_OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {
        if (!TouchEnable) return;
    }

    /// <summary>
    /// 拖拽结束.
    /// </summary>
    /// <param name="fingerIndex"></param>
    /// <param name="fingerPos"></param>
    public override void FingerGestures_OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        if (!TouchEnable) return;
    }

    public override void FingerGestures_OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        if (!TouchEnable) return;
        if (IsOutOfGameScreen()) { return; }
        isPinching = true;
        foreach (KeyValuePair<int, List<PinchHandlerInterface>> q in mPinchQueue)
        {
            List<PinchHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                for (int i = 0; i < handlers.Count; ++i)
                {
                    if (handlers[i].OnPinchBegin(fingerPos1, fingerPos2))
                    {
                        return;
                    }
                }
            }
        }
    }

    public override void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        if (!TouchEnable) return;
        if (IsOutOfGameScreen()) { return; }
        foreach (KeyValuePair<int, List<PinchHandlerInterface>> q in mPinchQueue)
        {
            List<PinchHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                for (int i = 0; i < handlers.Count; ++i)
                {
                    if (handlers[i].OnPinchMove(fingerPos1, fingerPos2, delta))
                    {
                        return;
                    }
                }
            }
        }
    }

    public override void FingerGestures_OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        if (!TouchEnable) return;
        if (IsOutOfGameScreen()) { return; }
        bool isBreak = false;
        foreach (KeyValuePair<int, List<PinchHandlerInterface>> q in mPinchQueue)
        {
            List<PinchHandlerInterface> handlers = q.Value;
            if (handlers != null)
            {
                for (int i = 0; i < handlers.Count; ++i)
                {
                    if (handlers[i].OnPinchEnd(fingerPos1, fingerPos2))
                    {
                        isBreak = true;
                        break;
                    }
                }
                if(isBreak)
                    break;
            }
        }
        isPinching = false;
        FingerClear();
    }

    #region 注册监听.

    void OnEnable()
    {
        FingerGestures.OnFingerDown += FingerGestures_OnFingerDown;
        FingerGestures.OnFingerUp += FingerGestures_OnFingerUp;
        FingerGestures.OnFingerDragMove += FingerGestures_OnFingerDragMove;
        //FingerGestures.OnFingerDragBegin += FingerGestures_OnFingerDragBegin;
        //FingerGestures.OnFingerDragEnd += FingerGestures_OnFingerDragEnd;

        FingerGestures.OnPinchMove += FingerGestures_OnPinchMove;
        FingerGestures.OnPinchBegin += FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchEnd += FingerGestures_OnPinchEnd;
    }
    void OnDisable()
    {
        FingerGestures.OnFingerDown -= FingerGestures_OnFingerDown;
        FingerGestures.OnFingerUp -= FingerGestures_OnFingerUp;
        FingerGestures.OnFingerDragMove -= FingerGestures_OnFingerDragMove;
        //FingerGestures.OnFingerDragBegin -= FingerGestures_OnFingerDragBegin;
        //FingerGestures.OnFingerDragEnd -= FingerGestures_OnFingerDragEnd;

        FingerGestures.OnPinchMove -= FingerGestures_OnPinchMove;
        FingerGestures.OnPinchBegin -= FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchEnd -= FingerGestures_OnPinchEnd;
    }

    #endregion

    override public void Update()
    {
        base.Update();
        //if (IsOutOfGameScreen()) { return; }

        #region KeyBoard

#if UNITY_STANDALONE_WIN

        if (!TouchEnable) return;
        if (OnMoveKeyDown != null && OnMoveKeyUp != null)
        {
            if (mWasd != Vector2.zero)
            {
                OnMoveKeyDown(mWasd.x * 10, mWasd.y * 10, 0, 0);
                mWasd = Vector2.zero;
                mInWasd = true;
            }
            else
            {
                if (mInWasd)
                {
                    OnMoveKeyUp(0, 0, 0, 0);
                    mInWasd = false;
                }
            }
        }

#endif
        #endregion
    }

    #region KeyBoard

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)

    private bool mInWasd = false;
    private Vector2 mWasd = Vector2.zero;
    private Vector2 mPosUp = new Vector2(0, 1);
    private Vector2 mPosDown = new Vector2(0, -1);
    private Vector2 mPosLeft = new Vector2(-1, 0);
    private Vector2 mPosRight = new Vector2(1, 0);

    override public void Up()
    {
        OnWASD(mPosUp);
    }
    override public void Down()
    {
        OnWASD(mPosDown);
    }
    override public void Left()
    {
        OnWASD(mPosLeft);
    }
    override public void Right()
    {
        OnWASD(mPosRight);
    }

    override public void KeyDown(KeyCode kc)
    {
        if (OnKeyDown != null)
        {
            OnKeyDown(kc);
        }

        OnWASD(Vector2.zero);
    }

    override public void KeyUp(KeyCode kc)
    {

    }

    private void OnWASD(Vector2 pos2D)
    {
        mWasd += pos2D;
    }

#endif

    #endregion

    private class GlobalTouchData
    {
        public int FingerIndex = -1;
        public OnGlobalTouchEvent DownEvent;
        public OnGlobalTouchEvent UpEvent;
    }

}
