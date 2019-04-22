using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IOS

public class SDKInterfaceIOS:SDKInterfaceBase
{
	[DllImport("__Internal")]
	private static extern void CallInitSDK();
	[DllImport("__Internal")]
	private static extern void CallShowLogin (bool isAutoLogin);
	[DllImport("__Internal")]
	private static extern void CallShowLogout ();
	[DllImport("__Internal")]
	private static extern void CallSwitchAccount ();
	[DllImport("__Internal")]
	private static extern void CallShowPersonCenter ();
	[DllImport("__Internal")]
	private static extern void CallHidePersonCenter ();
	[DllImport("__Internal")]
	private static extern void CallShowToolBar ();
	[DllImport("__Internal")]
	private static extern void CallHideToolBar ();
	[DllImport("__Internal")]
	private static extern string CallPayItem (string _json_string);
	[DllImport("__Internal")]
	private static extern int CallLoginState ();
	[DllImport("__Internal")]
	private static extern void CallShowShare (string _json_string);
	[DllImport("__Internal")]
	private static extern void CallSetPlayerInfo (string _json_string);
	[DllImport("__Internal")]
	private static extern string CallGetUserData ();
	[DllImport("__Internal")]
	private static extern string CallGetPlatformData ();
	[DllImport("__Internal")]
	private static extern void CallCopyClipboard (string _json_string);
	[DllImport("__Internal")]
	private static extern bool CallIsHasRequest (string _json_string);
	[DllImport("__Internal")]
	private static extern void CallDestory ();
	[DllImport("__Internal")]
	private static extern void CallExitGame ();
	[DllImport("__Internal")]
	private static extern string CallDoAnyFunction (string _funcName_string,string _json_string);
	[DllImport("__Internal")]
	private static extern string CallPhoneInfo ();
	[DllImport("__Internal")]
	private static extern void CallAddLocalPush(string _json_data);
	[DllImport("__Internal")]
	private static extern void CallRemoveLocalPush(string _json_data);
	[DllImport("__Internal")]
	private static extern void CallRemoveAllLocalPush();
	[DllImport("__Internal")]
	private static extern void CallGetUserFriends();
	[DllImport("__Internal")]
	private static extern void CallCreateCredentialProvider(string appId, string region, string bucket, string tmpSecretId, string tmpSecretKey, string sessionToken, long beginTime, long expiredTime);
	[DllImport("__Internal")]
	private static extern void CallUpload(string filePath, string cosPath);
	[DllImport("__Internal")]
	private static extern void CallCancel();
	[DllImport("__Internal")]
	private static extern void CallOpenAlbum(string title, int aspectX, int aspectY);


	public override int GetAppId()
	{
		int appId = 160;
		int.TryParse(GetPlatformData().GetData(SDKAttName.APP_ID), out appId);
		return appId;
	}

	public override int GetChannel()
	{
		int channel = 0;
		int.TryParse(GetPlatformData().GetData(SDKAttName.CHANNEL_ID), out channel);
		Debug.Log ("getChannel="+channel);
		return channel;
	}


	public override void InitSDK()
	{
		CallInitSDK();
	}
	public override void ShowLogin(bool isAutoLogin)
	{
		CallShowLogin(isAutoLogin);
	}
    public override void ShowLogin(string loginType)
	{
	}
	public override void ShowLogout()
	{
		CallShowLogout();
	}
	public override void SwitchAccount()
	{
		CallSwitchAccount();
	}
	public override void ShowUserCenter()
	{
		CallShowPersonCenter();
	}
	public override void HideUserCenter()
	{
		CallHidePersonCenter();
	}
	public override void ShowToolBar()
	{
		CallShowToolBar();
	}
	public override void HideToolBar()
	{
		CallHideToolBar();
	}
	public override string PayItem(SDKBaseData _in_pay)
	{
		string billNo ="";
		billNo= CallPayItem(_in_pay.DataToString());
		return billNo;
	}
	public override int LoginState()
	{
		int state = 0;
		state = CallLoginState();
		return state;
	}
	public override void ShowShare(SDKBaseData _in_share)
	{
		CallShowShare(_in_share.DataToString());
	}
	public override void SetPlayerInfo(SDKBaseData data)
	{
		CallSetPlayerInfo(data.DataToString());
	}
	public override SDKBaseData GetUserData()
	{
		if (_userData == null)
		{
			string value = CallGetUserData();
			_userData = new SDKBaseData();
			_userData.StringToData(value);
		}
		return _userData;

	}
	public override SDKBaseData GetPlatformData()
	{
		if (_platformData == null)
		{
			string value = CallGetPlatformData();
			_platformData = new SDKBaseData();
			_platformData.StringToData(value);
		}
		return _platformData;
	}

	public override void CopyClipboard(SDKBaseData data)
	{
		CallCopyClipboard(data.DataToString());
	}
	public override bool IsHasRequest(string requestType)
	{
		bool result = false;
		result =  CallIsHasRequest(requestType);
		return result;
	}
	public override void Destory()
	{
		CallDestory();
	}
	public override void ExitGame()
	{
		CallExitGame();
	}
	
	/**call any undefine function if success or return error*/
	public override string DoAnyFunction(string funcName,SDKBaseData _in_data)
	{
		string result = "";
		result = CallDoAnyFunction(funcName,_in_data.DataToString());
		return result;
	}

	public override string DoPhoneInfo()
	{

		string value = CallPhoneInfo();
		return value;
	}

	//1.增加一个本地推送
	public override void AddLocalPush(SDKBaseData data)
    {
        Debug.Log("AddLocalPush : "+ data.DataToString());
		CallAddLocalPush (data.DataToString());
    }		
			
	//2解除一个本地推送
	public override void RemoveLocalPush(string	_push_id)
	{
		Debug.Log("RemoveLocalPush : "+ _push_id);
		CallRemoveLocalPush (_push_id);
	}
			
	//3解除所有本地推送
	public override void RemoveAllLocalPush()
	{
		CallRemoveAllLocalPush ();
	}
	public override void GetUserFriends ()
	{
		CallGetUserFriends ();
	}

	public void CreateCredentialProvider(string appId, string region, string bucket, string tmpSecretId, string tmpSecretKey, string sessionToken, long beginTime, long expiredTime)
	{
		CallCreateCredentialProvider (appId, region, bucket, tmpSecretId, tmpSecretKey, sessionToken, beginTime, expiredTime);
	}

	public void Upload(string filePath, string cosPath)
	{
		CallUpload (filePath, cosPath);
	}

	public void Cancel()
	{
		CallCancel ();
	}

	public void OpenAlbum(string title, int aspectX, int aspectY)
	{
		CallOpenAlbum (title, aspectX, aspectY);
	}
}
#endif
