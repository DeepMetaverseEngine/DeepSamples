using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OneGameEventListener : MonoBehaviour {

    private static OneGameEventListener instance;
    private static GameObject _container;
    private static object syncRoot = new object();
    private static int createCount = 0;

    public delegate void DelegateLoginSuccess(SDKTypeEvent result);
    public DelegateLoginSuccess onLoginSuccess;

    public static OneGameEventListener Instance
    {
        get
        {
            if (null == instance)
            {
                _container = new GameObject();
                _container.name = "SDKController";
                UnityEngine.Object.DontDestroyOnLoad(_container);
                lock (syncRoot)
                {
                    if (null == instance)
                    {
                        createCount++;
                        Debug.Log("createCount::::" + createCount);
                        instance = _container.AddComponent(typeof(OneGameEventListener)) as OneGameEventListener;
                    }
                }
            }
            return instance;
        }
    }

    public void InitSelf()
    {
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_INIT_FINISH, NotifyInitFinish);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_UPDATE_FINISH, NotifyUpdateFinish);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_LOGIN_SUCCESS, NotifyLogin);
        //OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_PAY_RESULT, NotifyPayResult);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_LOGOUT, NotifyLogout);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_RELOGIN, NotifyRelogin);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_CANCEL_EXIT_GAME, NotifyCancelExit);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_SHARE_RESULT, NotifyCancelExit);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_RECEIVE_LOCAL_PUSH, NotifyCancelExit);
        OneGameSDK.Instance.AddEventDelegate(SDKEventType.EVENT_GET_FRIEND_RESULT, NotifyUserFriends);

        //自定义事件追踪eventOneSplashImage
        if (!PlayerPrefs.HasKey(SDKAttName.CUSTOM_EVENT_ONE_SPLASH_IMAGE))
        {
            var eEvent = new SDKBaseData();
            eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_ONE_SPLASH_IMAGE);
            OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
            PlayerPrefs.SetInt(SDKAttName.CUSTOM_EVENT_ONE_SPLASH_IMAGE, 1);
        }

        //订阅退出游戏事件
        EventManager.Subscribe("Event.System.Back.NoUI", (EventManager.ResponseData res) => {
            OneGameSDK.Instance.ExitGame();
        });

        //订阅序章自定义事件
        EventManager.Subscribe("Event.Scene.Tutorial", (EventManager.ResponseData res) => {
            Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
            object value;
            if (data.TryGetValue("value", out value))
                if (value.Equals(true))
                {
                    var eEvent = new SDKBaseData();
                    eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_TUTORIAL_START);
                    OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
                }
                else
                {
                    var eEvent = new SDKBaseData();
                    eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_TUTORIAL_COMPLETE);
                    OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
                }
        });

        //订阅第一章事件
        EventManager.Subscribe("Event.Scene.Chapter1", (EventManager.ResponseData res) => {
            if (!PlayerPrefs.HasKey(SDKAttName.CUSTOM_EVENT_ONE_CHAPTER1))
            {
                var eEvent = new SDKBaseData();
                eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_ONE_CHAPTER1);
                OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
                PlayerPrefs.SetInt(SDKAttName.CUSTOM_EVENT_ONE_CHAPTER1, 1);
            }
        });


        EventManager.Subscribe("Event.Scene.FirstInitFinish", OnEnterGame);
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnterGame(EventManager.ResponseData res)
    {
        var data = new SDKBaseData();
        OneGameSDK.Instance.DoAnyFunction("OnEnterGame", data);
        BuglyAgent.SetUserId(DataMgr.Instance.UserData.RoleID);
    }


    //初始化完成后回调函数
    void NotifyInitFinish(SDKTypeEvent evt)
    {
        Debug.Log("receive u3d init finish");
    }
    //登录操作完成后的回调函数
    void NotifyLogin(SDKTypeEvent evt)
    {
        //解析渠道登录成功返回的信息，一般有user_token,user_id...
        //CP方需要将信息解析为CP服务器约定的数据格式转发给游戏服务器以完成游戏的登录验证
        if(onLoginSuccess != null)
        {
            onLoginSuccess(evt);
        }
    }
    //更新用户信息完成后回调
    void NotifyUpdateFinish(SDKTypeEvent evt)
    {

    }
    
    //登出结果通知回调
    void NotifyLogout(SDKTypeEvent evt)
    {
        GameSceneMgr.Instance.ExitGame(null);
    }
    //重登录结果通知回调
    void NotifyRelogin(SDKTypeEvent evt)
    {
        GameSceneMgr.Instance.ExitGame(null);
    }
    //取消退出游戏通知回调
    void NotifyCancelExit(SDKTypeEvent evt)
    {

    }
    //本地推送通知回调，游戏需根据收到的数据实现相应的游戏逻辑
    void NotifyReceiveLocalPush(SDKTypeEvent evt)
    {

    }
    //获取好友列表通知回调
    void NotifyUserFriends(SDKTypeEvent evt)
    {

    }
    //分享结果通知回调
    void NotifyShareResult(SDKTypeEvent evt)
    {

    }
    
}
