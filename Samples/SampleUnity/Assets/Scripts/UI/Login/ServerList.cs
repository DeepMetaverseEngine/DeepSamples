using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using DeepMMO.Data;
using DeepMMO.Client;

public class ServerList
{

    //服务器列表原始数据.
    private string mLastServerStr;
    //所有区服综合信息列表，1个区id(key)对应1个服务器列表(value).
    private Dictionary<int, List<ServerInfo>> mZoneList;
    //所有服务器的列表.
    private List<ServerInfo> mServerList;
    //所有区号的列表.
    public List<int> ZoneIdList { get; private set; }
    //推荐服列表.
    public List<ServerInfo> RecomSrvList { get; private set; }
    //有角色的服务器列表.
    public List<ServerInfo> RoleServerList { get; private set; }

    public int mServerCount { get; private set; }
    public int mDefaultServer { get; private set; }
    public int mAppointServer { get; private set; }

    public delegate void ServerListRefreshEvent(ServerList serverList);
    public event ServerListRefreshEvent OnServerListRefresh;

    public ServerList()
    {
        LoadSrvList();
    }

    private void LoadSrvList()
    {
        DataMgr.Instance.LoginData.RequestSvrList();
        mServerList = RPGClientTemplateManager.Instance.GetAllServers();
        RecomSrvList = RPGClientTemplateManager.Instance.GetRecommendServers();
        mZoneList = new Dictionary<int, List<ServerInfo>>();
        ZoneIdList = new List<int>();
        RoleServerList = new List<ServerInfo>();
        ParseSrvList();
        //ParseRoleBasic(roleBasic);
    }

    private void ParseSrvList()
    {
        mZoneList.Clear();
        ZoneIdList.Clear();
        for (int i = 0 ; i < mServerList.Count ; ++i)
        {
            ServerInfo serverInfo = mServerList[i];
            //if (!serverInfo.is_open)
            //    continue;
            
            int zoneId = serverInfo.view_realm_index;
            string serverId = serverInfo.id;
            if (!mZoneList.ContainsKey(zoneId))
            {
                mZoneList.Add(zoneId, new List<ServerInfo>());
                //对zoneId列表进行插入排序
                if (ZoneIdList.Count > 0)
                {
                    bool isInsert = false;
                    for (int j = 0 ; j < ZoneIdList.Count ; ++j)
                    {
                        if (zoneId < ZoneIdList[j])
                        {
                            ZoneIdList.Insert(j, zoneId);
                            isInsert = true;
                            break;
                        }
                    }
                    if (!isInsert)
                    {
                        ZoneIdList.Add(zoneId);
                    }
                }
                else
                {
                    ZoneIdList.Add(zoneId);
                }
            }
            mZoneList[zoneId].Add(serverInfo);
        }
        mServerCount = mServerList.Count;
    }

    //private void ParseRoleBasic(string roleBasic)
    //{
    //    if (!string.IsNullOrEmpty(roleBasic))
    //    {
    //        RoleServerList.Clear();
    //        string[] rbList = roleBasic.Split(new char[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries);
    //        for (int i = 0 ; i < rbList.Length ; ++i)
    //        {
    //            string[] rb = rbList[i].Split(',');
    //            int zoneId = int.Parse(rb[0]);
    //            string base64Str = rb[1];
    //            //base64字符串转json字符串.
    //            byte[] data = System.Convert.FromBase64String(base64Str);
    //            string jStr = DeepCore.CUtils.UTF8.GetString(data);
    //            //解析json字符串.
    //            var jArray = Json.Deserialize(jStr) as List<object>;
    //            foreach (var item in jArray)
    //            {
    //                var jObj = item as Dictionary<string, object>;
    //                string serverId = ((string)jObj["serverId"]);
    //                int roleNum = (int)((long)jObj["num"]);
    //                //设置每个服的角色数量.
    //                ServerInfo server = GetServerById(zoneId, serverId);
    //                if (server != null)
    //                {
    //                    server.RoleNum = roleNum;
    //                    AddToRoleServerList(server);
    //                }
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// 根据逻辑服id插入排序.
    /// </summary>
    /// <param name="server"></param>
    private void AddToRoleServerList(ServerInfo server)
    {
        for (int i = RoleServerList.Count - 1 ; i >= 0 ; --i)
        {
            if (server.view_index >= RoleServerList[i].view_index)
            {
                RoleServerList.Insert(i + 1, server);
                return;
            }
        }
        RoleServerList.Insert(0, server);
    }

    //public bool RefreshSrvList(string serverList, string recomList, string roleBasic)
    //{
    //    string curServerStr = serverList + recomList + roleBasic;
    //    if (!string.Equals(mLastServerStr, curServerStr))
    //    {
    //        mLastServerStr = curServerStr;
    //        ParseSrvList();
    //        //ParseRoleBasic(roleBasic);
    //        return true;
    //    }
    //    return false;
    //}

    public ServerInfo GetServerById(string serverId)
    {
        for (int i = 0 ; i < mServerList.Count; ++i)
        {
            if (mServerList[i].id == serverId)
            {
                return mServerList[i];
            }
        }
        return null;
    }

    public List<ServerInfo> GetServerListByZoneId(int zoneId)
    {
        if (mZoneList.ContainsKey(zoneId))
        {
            return mZoneList[zoneId];
        }
        return null;
    }

    public List<ServerInfo> GetServerListByZoneIndex(int index)
    {
        if (index >= 0 && index < ZoneIdList.Count)
        {
            int zoneId = ZoneIdList[index];
            return GetServerListByZoneId(zoneId);
        }
        return null;
    }

    public string GetZoneName(int zoneIndex)
    {
        List<ServerInfo> serverList;
        int zoneId = ZoneIdList[zoneIndex];
        if (mZoneList.TryGetValue(zoneId, out serverList))
        {
            if (serverList.Count > 0)
            {
                return serverList[0].view_realm_name;
            }
        }
        return "";
    }

    public ServerInfo GetLastLoginServer()
    {
        ServerInfo lastLoginServer = null;
        string lastLoginAccount = PlayerPrefs.GetString("lastLoginAccount");
        if (lastLoginAccount != null && lastLoginAccount.Equals(DataMgr.Instance.AccountData.Account))
        {
            string lastLoginServerId = PlayerPrefs.GetString("lastLoginServerId");
            lastLoginServer = this.GetServerById(lastLoginServerId);
        }
        return lastLoginServer;
    }

    public ServerInfo GetRecomServerByIndex(int index)
    {
        ServerInfo recomServer = null;
        if (RecomSrvList.Count > index)
        {
            recomServer = RecomSrvList[index];
        }
        return recomServer;
    }

    public void RequestSvrList()
    {
        LoadSrvList();
        if (OnServerListRefresh != null)
        {
            OnServerListRefresh(this);
        }
    }

    public static string LoadLocalServerList()
    {
        string result = "";
        var jsonStr = ApplicationHelper.LoadTextStreamingAssets("serverList.json");
        Dictionary<string, object> json = Json.Deserialize(jsonStr) as Dictionary<string, object>;
        if (json != null)
        {
            string content = "\"rolebasic\":\"\",\"recom\":\"{0}\",\"type\":2,\"message\":\"成功\",\"srvList\":\"{1}\",\"status\":1,\"executeTime\":11";
            string serverFormat = "{0},0,{1},{2},1,1,{3},1,{4},{5},0,0,0,0,0,1,ping packet,,|";

            //init recomList
            List<object> recomList = json["recomList"] as List<object>;
            string recomStr = "";

            //init serverList
            List<object> serverList = json["serverList"] as List<object>;
            string serverStr = "";
            foreach (Dictionary<string, object> server in serverList)
            {
                string id = (string)server["serverid"];
                string name = (string)server["name"];
                string ip = (string)server["ip"];
                long port = (long)server["port"];
                long zone = (long)server["zone"];
                string curServer = string.Format(serverFormat, zone, id, id, name, ip, port);
                serverStr += curServer;
                foreach (Dictionary<string, object> s in recomList)
                {
                    string rcmId = (string)s["serverid"];
                    if (id == rcmId)
                    {
                        recomStr += curServer;
                        break;
                    }
                }
            }
            recomStr = recomStr.Substring(0, recomStr.Length - 1);
            serverStr = serverStr.Substring(0, serverStr.Length - 1);

            result = "{" + string.Format(content, recomStr, serverStr) + "}\n";
        }

        return result;
    }

    public void Destroy()
    {
        mZoneList.Clear();
        mServerList.Clear();
        ZoneIdList.Clear();
        RecomSrvList.Clear();
        RoleServerList.Clear();
        OnServerListRefresh = null;
    }

}

//public class ServerInfo
//{
//    // 服务器状态 
//    public enum SrvState
//    {
//        kSvrStateInvalid = -1,
//        kSvrStateMaintain,     // 维护 
//        kSvrStateSmooth,       // 流畅 
//        kSvrStateCrowd,        // 火爆 
//        kSvrStateHot,          // 爆满 
//        kSvrStateMax
//    };

//    // 服务器类型 
//    public enum SrvType
//    {
//        kSvrTypeInvalid = -1,
//        kSvrTypeNormal,         // 普通 
//        kSvrTypeNew,            // 新服 
//        kSvrTypeHot,            // 热服
//        kSvrTypeMax
//    };

//    public int ZoneId { get; set; } //区id
//    public int IconId { get; set; } //区名称图片id
//    public string ServerId { get; set; }   //服务器id
//    public int LogicId { get; set; }    //逻辑服务器id
//    public byte ServerState { get; set; }   //服务器状态
//    public byte ServerType { get; set; }    //服务器类型
//    public string ServerName { get; set; }  //服务器名称
//    public bool IsPublic { get; set; }  //是否对玩家可见
//    public string ServerIp { get; set; }    //服务器ip
//    public int ServerPort { get; set; } //服务器端口
//    public int ServerSort { get; set; } //服务器排序
//    public int RoleNum { get; set; } //角色数量

//    public ServerInfo(string info)
//    {
//        string[] i = info.Split(',');

//        ZoneId = int.Parse(i[0]);//区id
//        IconId = int.Parse(i[1]);//区名称图片id
//        ServerId = i[2];//服务器id
//        LogicId = int.Parse(i[3]);//逻辑服务器id
//        ServerState = byte.Parse(i[4]);//服务器状态
//        ServerType = byte.Parse(i[5]);//服务器类型
//        ServerName = i[6];//服务器名称
//        IsPublic = int.Parse(i[7]) == 1;//是否对玩家可见
//        ServerIp = i[8];//服务器ip
//        ServerPort = int.Parse(i[9]);//服务器端口
//        ServerSort = int.Parse(i[10]);//服务器排序
//    }

//    public void CopyFrom(ServerInfo info)
//    {
//        ZoneId = info.ZoneId;//区id
//        IconId = info.IconId;//区名称图片id
//        ServerId = info.ServerId;//服务器id
//        LogicId = info.LogicId;//逻辑服务器id
//        ServerState = info.ServerState;//服务器状态
//        ServerType = info.ServerType;//服务器类型
//        ServerName = info.ServerName;//服务器名称
//        IsPublic = info.IsPublic;//是否对玩家可见
//        ServerIp = info.ServerIp;//服务器ip
//        ServerPort = info.ServerPort;//服务器端口
//        ServerSort = info.ServerSort;//服务器排序
//        RoleNum = info.RoleNum;//角色数量
//    }

//}
