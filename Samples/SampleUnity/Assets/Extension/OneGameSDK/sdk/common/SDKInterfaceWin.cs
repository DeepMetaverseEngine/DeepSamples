using UnityEngine;
using System.Collections;
using System;
using System.IO;

/* 
* SDKInterfaceWin为Unity Windows编译环境下使用，仅为了编译不报错，故无具体实现。
* 具体Ａｎｄｒｏｉｄ、ＩＯＳ有相关实现，接入方无需过多关注，如有疑问请联系提供方。
* 
*/
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBPLAYER
public class SDKInterfaceWin : SDKInterfaceBase
{

	private SDKBaseData _win_userInfo = null;
	private SDKBaseData _win_plaform = null;
    

    public override int GetAppId()
    {
        return AppId;
    }

    public override int GetChannel()
    {
        return Channel;
    }

    public override void InitSDK()
    {
        Debug.Log("CallInitSDK");    
		OneGameSDK.Instance.NotifyInitFinish ((new SDKBaseData()).DataToString());	
    }

    public override void ShowLogin(bool isAutoLogin)
    {
        Debug.Log("CallLogin");
        SDKBaseData data = new SDKBaseData();
        data.SetData(SDKAttName.USER_ID, "testUserID");
        data.SetData(SDKAttName.USER_TOKEN, "testUserToken");
        OneGameSDK.Instance.NotifyLogin(data.DataToString());

    }
    public override void ShowLogin(string loginType)
    {
        Debug.Log("CallYSDKLogin");
        SDKBaseData data = new SDKBaseData();
        data.SetData(SDKAttName.USER_ID, "testUserID");
        data.SetData(SDKAttName.USER_TOKEN, "testUserToken");
        OneGameSDK.Instance.NotifyLogin(data.DataToString());
    }

    public override void ShowLogout()
    {
        Debug.Log("CallLogout");
        SDKBaseData data = new SDKBaseData();
        data.SetData(SDKAttName.USER_ID, "testUserID");
        data.SetData(SDKAttName.USER_TOKEN, "testUserToken");
        OneGameSDK.Instance.NotifyLogout(data.DataToString());
    }

    public override void SwitchAccount()
    {
        Debug.Log("CallSwitchAccount");
    }

    public override void ShowUserCenter()
    {
        Debug.Log("CallPersonCenter");

           
    }
    public override void HideUserCenter()
    {
        Debug.Log("CallHidePersonCenter");

           
    }
    public override void ShowToolBar()
    {
        Debug.Log("CallToolBar");

           
    }
    public override void HideToolBar()
    {
        Debug.Log("CallHideToolBar");

            
    }
    public override string PayItem(SDKBaseData _in_pay)
    {
        Debug.Log("CallPayItem" + "data: " + _in_pay.DataToString());

        SDKBaseData data = new SDKBaseData();
        data.SetData(SDKAttName.PAY_RESULT, "1");
        data.SetData(SDKAttName.PAY_RESULT_DATA, "testSuccess");
        OneGameSDK.Instance.NotifyPayResult(data.DataToString());
        return "test return billno";
    }

    public override int LoginState()
    {
        Debug.Log("CallLoginState");            
        return 0;
    }
    public override void ShowShare(SDKBaseData _in_data)
    {
        Debug.Log("CallShare" + "data: " + _in_data.DataToString());

    }
    public override void SetPlayerInfo(SDKBaseData _in_data)
    {
		_win_userInfo = _in_data;
    }

    public override SDKBaseData GetUserData()
    {
        if (_win_userInfo == null)
        {
            _win_userInfo = new SDKBaseData();
            _win_userInfo.SetData(SDKAttName.SDK_NAME, "test");
            _win_userInfo.SetData(SDKAttName.USER_ID, "testUserID_default");
            _win_userInfo.SetData(SDKAttName.USER_TOKEN, "testUserToken_default");
        }

        return _win_userInfo;
    }

    public override SDKBaseData GetPlatformData()
    {
        if (null == _win_plaform)
        {
            _win_plaform = new SDKBaseData();
            _win_plaform.SetData(SDKAttName.SDK_NAME, "OneGame");
            _win_plaform.SetData(SDKAttName.CHANNEL_ID, Channel);
            _win_plaform.SetData(SDKAttName.PLATFORM_ID, GameConfig.Instance.GetString(SDKAttName.PLATFORM_ID));
            _win_plaform.SetData(SDKAttName.QUERY_ORDER, 1);
            _win_plaform.SetData(SDKAttName.CP_ID, "123");
        }
        return _win_plaform;

    }

    public override void CopyClipboard(SDKBaseData _in_data)
    {
        Debug.Log("CallLogout" + " data: " + _in_data.DataToString());

            
    }

    public override bool IsHasRequest(string requestType)
    {
        Debug.Log("IsHasRequest" + " type " + requestType);           
        return true;
    }
    public override void Destory()
    {
        Debug.Log("CallDestory");

            
    }

    public override void ExitGame()
    {
        Debug.Log("ExitGame");
    }

    /**call any undefine function if success or return error*/
    public override string DoAnyFunction(string funcName, SDKBaseData _in_data)
    {
        Debug.Log("DoAnyFunction");
        return "";
    }

    public override string DoPhoneInfo()
    {
        Debug.Log("DoPhoneInfo");

        return "";
    }

    public override void AddLocalPush(SDKBaseData _push_data)
    {
        Debug.Log("AddLocalPush");

    }
    public override void RemoveLocalPush(string pushid)
    {
        Debug.Log("RemoveLocalPush");

    }
    public override void RemoveAllLocalPush()
    {
        Debug.Log("RemoveLocalPush");
          
    }
    public override void GetUserFriends()
    {
        Debug.Log("GetUserFriends");
           
    }

    public void CreateCredentialProvider(String appId, String region, String bucket, String tmpSecretId, String tmpSecretKey, String sessionToken, long beginTime, long expiredTime)
    {
        Debug.LogWarning("CreateCredentialProvider Windows不支持此方法，请使用真机测试.  ");
    }

    public void Upload(String filePath, String cosPath)
    {
        Debug.Log("Upload Windows不支持此方法，请使用真机测试.");
    }

    public void Cancel()
    {
        Debug.Log("Cancel Windows不支持此方法，请使用真机测试.");
    }

    public void OpenAlbum(String title, int aspectX, int aspectY)
    {
        Debug.Log("OpenAlbum Windows不支持此方法，请使用真机测试.");
    }

    
}
#endif
