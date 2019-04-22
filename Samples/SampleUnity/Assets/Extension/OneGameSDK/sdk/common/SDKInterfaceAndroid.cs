using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using Assets.Scripts;

#if UNITY_ANDROID

public class SDKInterfaceAndroid : SDKInterfaceBase 
{
    private AndroidJavaObject javaObject;

    public SDKInterfaceAndroid()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            javaObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }

    public T SDKCall<T>(string method, params object[] args)
    {
        try
        {
            var result = javaObject.Call<T>(method, args);
            if (Debug.isDebugBuild)
                Debug.Log("SDKCall method " + method + " params: " + MiniJSON.Json.Serialize(args) +" result: " + result);
            return result;
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return default(T);
        }
    }

    public void SDKCall(string method, params object[] args)
    {
        try
        {
            if (Debug.isDebugBuild)
                Debug.Log("SDKCall method " + method +" params: " + MiniJSON.Json.Serialize(args));

            javaObject.Call(method, args);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
        }
    }

    public override int GetAppId()
    {
        int.TryParse(GetPlatformData().GetData(SDKAttName.APP_ID), out AppId);
        return AppId;
    }

    public override int GetChannel()
    {
        int.TryParse(GetPlatformData().GetData(SDKAttName.CHANNEL_ID), out Channel);
        return Channel;
    }

    public override void InitSDK()
	{
        SDKCall("CallInitSDK");
	}
	
	public override void ShowLogin(bool isAutoLogin)
	{
        SDKCall("CallLogin", isAutoLogin);
	}
    public override void ShowLogin(string loginType)
    {
        SDKCall("CallLogin", loginType);

    }
	public override void ShowLogout()
	{
		SDKCall("CallLogout");
	}

    public override void SwitchAccount()
    {
        SDKCall("CallSwitchAccount");
    }

    public override void ShowUserCenter()
	{
		SDKCall("CallPersonCenter");
	}
	public override void HideUserCenter()
	{
		SDKCall("CallHidePersonCenter");
	}
	public override void ShowToolBar()
	{
		SDKCall("CallToolBar");
	}
	public override void HideToolBar()
	{
		SDKCall("CallHideToolBar");
	}
	public override string PayItem(SDKBaseData _in_pay)
	{
        return SDKCall<string>("CallPayItem", _in_pay.DataToString());
	}
	public override int LoginState()
	{
        return SDKCall<int>("CallLoginState");
	}
	public override void ShowShare(SDKBaseData _in_data)
	{
		SDKCall("CallShare", _in_data.DataToString());
	}
	public override void SetPlayerInfo(SDKBaseData _in_data)
	{
		SDKCall("CallSetPlayerInfo", _in_data.DataToString());
	}
	public override SDKBaseData GetUserData ()
	{
        if (_userData == null)
        {
            string value = SDKCall<string>("CallUserData");
            _userData = new SDKBaseData();
            _userData.StringToData(value);
        }
		return _userData;
	}
	public override SDKBaseData GetPlatformData ()
	{
        if (_platformData == null)
        {
            string value = SDKCall<string>("CallPlatformData");
            _platformData = new SDKBaseData();
            _platformData.StringToData(value);
        }
        return _platformData;
    }

	public override void CopyClipboard (SDKBaseData _in_data)
	{
		SDKCall("CallCopyClipboard", _in_data.DataToString());
	}
	public override bool IsHasRequest(string requestType)
	{
		return SDKCall<bool>("CallIsHasRequest",requestType);
	}
	public override void Destory()
	{
		SDKCall("CallDestory");
	}
	public override void ExitGame()
	{
		var result = SDKCall<bool>("CallExitGame");

        if (!result)
        {
            string acceptStr = HZLanguageManager.Instance.GetString("common_accept");
            string refuseStr = HZLanguageManager.Instance.GetString("common_refuse");
            GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_SYSTEM + 101, HZLanguageManager.Instance.GetString("common_exit_game"), acceptStr, refuseStr, "", (object param) =>
            {
                Application.Quit();
            }
            , null);
        }
	}
	
	/**call any undefine function if success or return error*/
	public override string DoAnyFunction(string funcName,SDKBaseData _in_data)
	{
		return SDKCall<string>("CallAnyFunction",funcName,_in_data.DataToString());
	}

	public override string DoPhoneInfo()
	{
		return SDKCall<string>("CallPhoneInfo");
	}

	public override void AddLocalPush (SDKBaseData _push_data)
	{
		SDKCall("AddLocalPush", _push_data.DataToString());
	}
	public override void RemoveLocalPush (string _push_id)
	{
		SDKCall("RemoveLocalPush",_push_id);
	}
	public override void RemoveAllLocalPush ()
	{
		SDKCall("RemoveAllLocalPush");
	}
	public override void GetUserFriends ()
	{
		SDKCall("GetUserFriends");
	}

    public void CreateCredentialProvider(String appId, String region, String bucket, String tmpSecretId, String tmpSecretKey, String sessionToken, long beginTime, long expiredTime)
    {
        SDKCall("CreateCredentialProvider", appId, region, bucket, tmpSecretId, tmpSecretKey, sessionToken, beginTime, expiredTime);
    }

    public void Upload(String filePath, String cosPath)
    {
        SDKCall("Upload",filePath, cosPath);
    }

    public void Cancel()
    {
        SDKCall("Cancel");
    }

    public void OpenAlbum(String title, int aspectX, int aspectY)
    {
        SDKCall("OpenAlbum",title,aspectX, aspectY, AndroidPlugin.GetStoragePath());
    }
}

#endif