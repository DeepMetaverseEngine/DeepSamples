using DeepMMO.Client;
using DeepMMO.Data;
using DeepMMO.Protocol.Client;
using System;
using System.Collections.Generic;
using TLProtocol.Data;

namespace TLClient.Net
{
    public class TLLoginHandler
    {

        private RPGClient mClient;
        private List<RoleIDSnap> mAccountRoleList;

        public TLLoginHandler(RPGClient client)
        {
            this.mClient = client;
        }

        public void Request_EntryServer(string host, int port, string account, string token, string serverID, Action<ClientEnterServerResponse> callback)
        {
            mClient.GateClient.Disconnect();
            mClient.GameClient.Disconnect();
            mClient.Gate_Connect(host, port, account, token, serverID, (rsp) =>
            {
                if (rsp != null && ClientEnterServerResponse.CheckSuccess(rsp))
                {
                    mAccountRoleList = rsp.s2c_roleIDList;
                    mClient.Connect_Connect((rsp2) =>
                    {
                        if (callback != null)
                        {
                            callback.Invoke(rsp2);
                        }
                    });
                }
                else
                {
                    if (callback != null)
                    {
                        callback.Invoke(null);
                    }
                }
            });
        }

        public void ReConnect(Action<ClientEnterServerResponse> callback)
        {
            mClient.Connect_Connect((rsp) =>
            {
                if (callback != null)
                {
                    callback.Invoke(rsp);
                }
            });
        }

        public void Request_RoleList(Action<ClientGetRolesResponse, Exception> callback)
        {
            ClientGetRolesRequest request = new ClientGetRolesRequest();
            mClient.GameClient.Request<ClientGetRolesResponse>(new ClientGetRolesRequest() { }, (err, rsp) =>
            {
                if (callback != null)
                {
                    callback.Invoke(rsp, err);
                }
            });
        }

        public void Request_CreateRole(string name, int pro, Action<ClientCreateRoleResponse, Exception> callback)
        {
            ClientCreateRoleRequest request = new ClientCreateRoleRequest();
            request.c2s_name = name;
            request.c2s_template_id = pro;
            TLClientCreateRoleExtData extData = new TLClientCreateRoleExtData();
            extData.RolePro = (TLClientCreateRoleExtData.ProType)pro;
            request.c2s_extension_data = extData;
            mClient.GameClient.Request<ClientCreateRoleResponse>(request, (err, rsp) =>
            {
                if (callback != null)
                {
                    callback.Invoke(rsp, err);
                }
            });
        }

        public void Request_CreateRole(string name, TLClientCreateRoleExtData.ProType pro, TLClientCreateRoleExtData.GenderType gender, Action<ClientCreateRoleResponse, Exception> callback)
        {
            ClientCreateRoleRequest request = new ClientCreateRoleRequest();
            request.c2s_name = name;
            request.c2s_template_id = (int)pro;
            TLClientCreateRoleExtData extData = new TLClientCreateRoleExtData();
            extData.RolePro = pro;
            extData.gender = gender;
            request.c2s_extension_data = extData;

            mClient.GameClient.Request<ClientCreateRoleResponse>(request, (err, rsp) =>
            {
                if (callback != null)
                {
                    callback.Invoke(rsp, err);
                }
            });
        }

        public void Request_RandomName(int pro, byte gender, Action<ClientGetRandomNameResponse, Exception> callback)
        {
            ClientGetRandomNameRequest request = new ClientGetRandomNameRequest();
            request.c2s_role_template_id = pro;
            request.c2s_role_gender = gender;
            mClient.GameClient.Request<ClientGetRandomNameResponse>(request, (err, rsp) =>
            {
                if (callback != null)
                {
                    callback.Invoke(rsp, err);
                }
            });
        }

        public void Request_SelectRole(string roleId, Action<ClientSelectRoleResponse, Exception> callback)
        {
            ClientSelectRoleRequest request = new ClientSelectRoleRequest();
            request.c2s_roleUUID = roleId;
            mClient.GameClient.Request<ClientSelectRoleResponse>(request, (err, rsp) =>
            {
                if (callback != null)
                {
                    callback.Invoke(rsp, err);
                }
            });
        }

        public void Request_EnterGame(string roleId, Action<ClientEnterGameResponse, Exception> callback)
        {
            ClientEnterGameRequest request = new ClientEnterGameRequest();
            request.c2s_roleUUID = roleId;
            mClient.GameClient.Request<ClientEnterGameResponse>(request, (err, rsp) =>
            {
                if (callback != null)
                {
                    callback.Invoke(rsp, err);
                }
            });
        }

        public List<RoleIDSnap> GetAcountRoleSnapList()
        {
            return mAccountRoleList;
        }
    }

}
