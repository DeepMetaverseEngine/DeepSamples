#define newyht
using System;
using System.Collections.Generic;



using Assets.Scripts.Setting;
using TLClient.Net;
using DeepMMO.Protocol.Client;
using DeepMMO.Data;
using TLProtocol.Data;
using DeepMMO.Client;
using System.Text;
using Assets.Scripts;

public class LoginData
{

    //一号通请求地址.
    //public const string TestUrl = "http://192.168.1.44:18080/passport/common";

    public static string NoticeBoard { get; set; }

    public LoginData()
    {
        
    }

    public void RequestRegister(string username, string password, HttpRequest.Response response)
    {
        int msgId = (int)UserCenterMsgID.Register;
        Dictionary<string, string> arg = new Dictionary<string, string>();
        arg.Add("type", msgId.ToString());
        arg.Add("username", username);
        arg.Add("appid", OneGameSDK.Instance.AppId.ToString());
        arg.Add("channel", OneGameSDK.Instance.Channel.ToString());
        arg.Add("ostype", PublicConst.OSType.ToString());
        arg.Add("mac", PlatformMgr.PluginGetUUID());
        arg.Add("clientUA", PlatformMgr.PluginGetUserAgent());
        //arg.Add("returntype", "0");
        if (password.Length > 0)
        {
            string pswRsa = HttpMsgUtils.GetRSAStringWithDefaultKey(password);
            arg.Add("password", pswRsa);
        }
        //if (apnsId.Length > 0)    //ios平台要获取，先暂时注释
        //    arg.Add("apnsID", apnsId);

        HttpRequest httpRequest = GameGlobal.Instance.gameObject.AddComponent<HttpRequest>();
        //httpRequest.RequestPost(ProjectSetting.UserCenterUrl, arg, msgId, (result) =>
        //{
        //    httpRequest.Destroy();
        //    if (response != null)
        //        response(result);
        //});
    }

    public void RequestLogin(string username, string password, int logintype, string sdkExpansion, string sdkVersion, 
        string sdkToken, string sdkTime, string sdkChannelId, string sdkChannelName, HttpRequest.Response response)
    {
        int msgId = (int)UserCenterMsgID.Login;
        Dictionary<string, string> arg = new Dictionary<string, string>();
        arg.Add("type", msgId.ToString());
        arg.Add("username", username);
        arg.Add("appid", OneGameSDK.Instance.AppId.ToString());
        arg.Add("channel", OneGameSDK.Instance.Channel.ToString());
        arg.Add("ostype", PublicConst.OSType.ToString());
        arg.Add("logintype", logintype.ToString());
        arg.Add("version", PublicConst.LogicVersion.ToString());
        arg.Add("mac", PlatformMgr.PluginGetUUID());
        arg.Add("clientUA", PlatformMgr.PluginGetUserAgent());
        //arg.Add("returntype", "0");
        if (!string.IsNullOrEmpty(password))
        {
            string pswRsa = HttpMsgUtils.GetRSAStringWithDefaultKey(password);
            arg.Add("password", pswRsa);
        }
        if (!string.IsNullOrEmpty(sdkExpansion))
            arg.Add("expansion", sdkExpansion);
        if (!string.IsNullOrEmpty(sdkVersion))
            arg.Add("sdkversion", sdkVersion);
        if (!string.IsNullOrEmpty(DataMgr.Instance.AccountData.ApnsID))    //ios平台要获取
            arg.Add("apnsID", DataMgr.Instance.AccountData.ApnsID);

        if (!string.IsNullOrEmpty(sdkToken))
            arg.Add("token", sdkToken);
        if (!string.IsNullOrEmpty(sdkTime))
            arg.Add("Time", sdkTime);
        if (!string.IsNullOrEmpty(sdkChannelId))
            arg.Add("channelId", sdkChannelId);
        if (!string.IsNullOrEmpty(sdkChannelName))
            arg.Add("channelName", sdkChannelName);

        HttpRequest httpRequest = GameGlobal.Instance.gameObject.AddComponent<HttpRequest>();
        //httpRequest.RequestPost(ProjectSetting.UserCenterUrl, arg, msgId, (result) =>
        //{
        //    httpRequest.Destroy();
        //    if (response != null)
        //        response(result);
        //});
    }

    public void RequestActivation(string code, HttpRequest.Response response)
    {
        int msgId = (int)GameCenterMsgID.Activation;
        Dictionary<string, string> arg = new Dictionary<string, string>();
        arg.Add("type", msgId.ToString());
        arg.Add("activation", code);
        arg.Add("username", DataMgr.Instance.AccountData.Account);
        arg.Add("appid", OneGameSDK.Instance.AppId.ToString());
        arg.Add("channel", OneGameSDK.Instance.Channel.ToString());
        arg.Add("mac", PlatformMgr.PluginGetUUID());
        arg.Add("clientUA", PlatformMgr.PluginGetUserAgent());
        //arg.Add("returntype", "0");

        //HttpRequest httpRequest = GameGlobal.Instance.gameObject.AddComponent<HttpRequest>();
        //httpRequest.RequestPost(ProjectSetting.GameCenterUrl, arg, msgId, (result) =>
        //{
        //    httpRequest.Destroy();
        //    if (response != null)
        //        response(result);
        //});
    }

    public void RequestSvrList()
    {
        int msgId = (int)GameCenterMsgID.ServerList;
        StringBuilder args = new StringBuilder();
        args.Append("?type=" + msgId);
        args.Append("&username=" + DataMgr.Instance.AccountData.Account);
        args.Append("&appid=" + OneGameSDK.Instance.AppId);
        args.Append("&channel=" + OneGameSDK.Instance.Channel);
        args.Append("&clientVersion=" + PublicConst.LogicVersion);
        args.Append("&deviceType=" + PublicConst.OSType);
        args.Append("&deviceId=" + PlatformMgr.PluginGetUUID());
        args.Append("&deviceModel=" + PlatformMgr.PluginGetDeviceType());
        args.Append("&sdkVersion=" + OneGameSDK.Instance.GetPlatformData().GetData(SDKAttName.SDK_VERSION));
        args.Append("&sdkName=" + OneGameSDK.Instance.GetPlatformData().GetData(SDKAttName.SDK_NAME));
        args.Append("&time=" + DataMgr.Instance.AccountData.TimeStamp);
        args.Append("&sign=" + DataMgr.Instance.AccountData.Sign);
        if (!string.IsNullOrEmpty(DataMgr.Instance.AccountData.ApnsID))    //ios平台要获取
            args.Append("&apnsID" + DataMgr.Instance.AccountData.ApnsID);

        try
        {
            RPGClientTemplateManager.Instance.LoadServerList(ProjectSetting.SERVERLIST_URL + args);
        }
        catch (Exception e)
        {
            Debugger.LogError(e.ToString());
        }

        //HttpRequest httpRequest = GameGlobal.Instance.gameObject.AddComponent<HttpRequest>();
        //httpRequest.RequestGet(new Uri(new Uri(ProjectSetting.GMT_URL), "api/client/server_list").ToString(), msgId, (result) =>
        //{
        //    httpRequest.Destroy();
        //    if (response != null)
        //        response(result);
        //}, isWaiting);

        //string content = "{\"rolebasic\":\"\",\"recom\":\"1,0,1110,1110,1,1,内网测试服,1,192.168.4.88,19001,0,0,0,0,0,1,ping packet,,|1,0,1000,1000,1,1,本地开发服,1,127.0.0.1,19001,0,0,0,0,0,1,ping packet,,\",\"type\":2,\"message\":\"成功\",\"srvList\":\"1,0,1110,1110,1,1,内网测试服,1,192.168.4.88,19001,0,0,0,0,0,1,ping packet,,|1,0,1000,1000,1,1,本地开发服,1,127.0.0.1,19001,0,0,0,0,0,1,ping packet,,|1,0,1118,1118,1,1,六七,1,192.168.18.112,19001,0,0,0,0,0,1,ping packet,,|1,0,1111,1111,1,1,我叫HZ,1,192.168.18.74,19001,0,0,0,0,0,1,ping packet,,|1,0,1112,1112,1,1,luo,1,192.168.18.32,19001,0,0,0,0,0,1,ping packet,,|1,0,1113,1113,1,1,从现在开始,1,192.168.18.80,19001,0,0,0,0,0,1,ping packet,,|1,0,1114,1114,1,1,长坂坡,1,192.168.18.135,19001,0,0,0,0,0,1,ping packet,,|1,0,1115,1115,1,1,TBC,1,192.168.18.83,19001,0,0,0,0,0,1,ping packet,,|1,0,1116,1116,1,1,大刘,1,192.168.18.100,19001,0,0,0,0,0,1,ping packet,,|1,0,1117,1117,1,1,老蔡,1,192.168.18.105,19001,0,0,0,0,0,1,ping packet,,|1,0,1118,1118,1,1,小鹏,1,192.168.18.70,19001,0,0,0,0,0,1,ping packet,,|2,0,2000,2000,1,1,外网测试服,1,106.3.136.167,19001,0,0,0,0,0,1,ping packet,,\",\"status\":1,\"executeTime\":11}\n";
        //string content = ServerList.LoadLocalServerList();
        //HttpRequest.ResponseData result = new HttpRequest.ResponseData(msgId, HttpRequest.ResultType.Success, content, false);
        //if (response != null)
        //    response(result);
    }

    public void CancelEntryServer()
    {
        //ZeusNetManage.Instance.NetClient.LoginHandler.Cancel_Request_EntryServer();
    }

    public void RequestEntryServer(string serverID,string account, string sign, ulong time, string clientUA, string ip,
        int port, string c2s_deviceMac, int c2s_deviceType, string c2s_clientRegion, string c2s_clientChannel, string c2s_clientVersion,
        Action<ClientEnterServerResponse> callback)
    {
        //ip = "192.168.1.21";
        //account = "2018082303336926";
        //serverID = "4000";
        TLNetManage.Instance.LoginHandler.Request_EntryServer(ip, port, account, sign, serverID,(ClientEnterServerResponse response) =>
        {
            if (callback != null)
                callback(response);

            if (response == null || response.IsSuccess == false)
            {
                TLNetManage.Instance.Disconnect();
            }
        });
    }

    public void Reconnect(Action<ClientEnterServerResponse> callback)
    {
        TLNetManage.Instance.LoginHandler.ReConnect((ClientEnterServerResponse response) =>
        {
            if (callback != null)
                callback(response);

            if (response == null || response.IsSuccess == false)
            {
                TLNetManage.Instance.Disconnect();
            }
        });
    }

    public void RequestRandomName(int pro,byte gender, Action<ClientGetRandomNameResponse> callback)
    {
        TLNetManage.Instance.LoginHandler.Request_RandomName(pro,gender,(ClientGetRandomNameResponse response, Exception exp) =>
        {
            if (callback != null && response != null)
                callback(response);
        });
    }

    public void RequestCreateRole(int pro, int gen, string name, Action<ClientCreateRoleResponse> callback)
    {
        TLNetManage.Instance.LoginHandler.Request_CreateRole(name, (TLClientCreateRoleExtData.ProType)pro, (TLClientCreateRoleExtData.GenderType)gen, (ClientCreateRoleResponse response, Exception exp) =>
        {
            if (callback != null && response != null)
                callback(response);
        });
    }

    public void RequestRoleList(Action<ClientGetRolesResponse> callback)
    {
        TLNetManage.Instance.LoginHandler.Request_RoleList((ClientGetRolesResponse response, Exception exp) =>
        {
            if (callback != null && response != null)
                callback(response);
        });
    }

    //public DeletePlayerResponse GetDeleteRoleData(string playerid, Action<DeletePlayerResponse> action)
    //{
    //    ZeusNetManage.Instance.NetClient.LoginHandler.Request_DeleteRole(playerid, (DeletePlayerResponse msg) =>
    //    {
    //        if (action != null && msg != null)
    //            action(msg);
    //    });
    //    return ZeusNetManage.Instance.NetClient.LoginHandler.LastDeleteRole;    //return cache data
    //}

    public void RequestEnterGame(string playerId, Action<ClientEnterGameResponse> callback)
    {
        TLNetManage.Instance.LoginHandler.Request_EnterGame(playerId, (ClientEnterGameResponse response, Exception exp) =>
        {
            if (response.s2c_code == ClientEnterGameResponse.CODE_ROLE_SUSPEND)
            {
                GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, string.Format(HZLanguageManager.Instance.GetString("common_role_black"), response.s2c_suspendTime.ToLocalTime().ToString("yyyy/MM/dd HH:mm")), "", null, null);
                response = null;
            }

            if (callback != null)
                callback(response);
        });
    }

    public void Update(float delta)
    {
        //ZeusNetManage.Instance.NetClient.LoginHandler.UpdateTimeMS((int)(delta * 1000));
    }
}

public class AccountData {

    public string Account { get; set; }
    public string Password { get; set; }
    public string Sign { get; set; }
    public ulong TimeStamp { get; set; }
    public string CheckCode { get; set; }
    public int LoginType { get; set; }
    public string SdkExpansion { get; set; }
    public string SdkVersion { get; set; }
    public string SdkToken { get; set; }
    public string ApnsID { get; set; }

    public ServerInfo CurSrvInfo { get; set; }
    public string CurPlayerId { get; set; }

}
