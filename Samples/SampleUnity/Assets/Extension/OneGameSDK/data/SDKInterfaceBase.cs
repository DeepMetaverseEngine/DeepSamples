using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public  abstract class SDKInterfaceBase
{
    protected SDKBaseData _userData = null;

    protected SDKBaseData _platformData = null;

    protected int AppId = 160;
    protected int Channel = 0;

    //0 Ã»ÓÐÍøÂç 1 wifi 2 2G 3 3G 4 4G
    protected int _networkType = 0;

    public SDKInterfaceBase ()
	{
	}


    public abstract int GetAppId();
    public abstract int GetChannel();
    public abstract void InitSDK();
	public abstract void ShowLogin(bool isAutoLogin);
    public abstract void ShowLogin(string loginType);
	public abstract void ShowLogout();
    public abstract void SwitchAccount();
    public abstract void ShowUserCenter();
	public abstract void HideUserCenter();
	public abstract void ShowToolBar();
	public abstract void HideToolBar();
	public abstract string PayItem(SDKBaseData _in_pay);
	public abstract int LoginState();
	public abstract void ShowShare(SDKBaseData _in_share);
	public abstract void SetPlayerInfo(SDKBaseData data);
	public abstract SDKBaseData GetUserData();
	public abstract SDKBaseData GetPlatformData();
	//
	public abstract void CopyClipboard(SDKBaseData data);
	public abstract bool IsHasRequest(string requestType);
	public abstract void Destory();
	public abstract void ExitGame();

	/**call any undefine function if success or return error*/
	public abstract string DoAnyFunction(string funcName,SDKBaseData _in_data);
	/**get phone info */
	public abstract string DoPhoneInfo();

	public abstract void AddLocalPush(SDKBaseData _push_data);

	public abstract void RemoveLocalPush(string pushid);

    public abstract void RemoveAllLocalPush();

	public abstract void GetUserFriends();
}


