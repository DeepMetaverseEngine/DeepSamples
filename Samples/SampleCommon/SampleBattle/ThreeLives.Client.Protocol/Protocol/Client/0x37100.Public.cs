using DeepCore.IO;
using DeepMMO.Protocol;
using TLProtocol.Data;
using TLProtocol.Protocol.Data;

namespace TLProtocol.Protocol.Client
{
    [MessageType(Constants.PUBLIC_START + 1)]
    public class GetRoleSnapRequest : Request
    {
        public string[] c2s_rolesID;
    }

    [MessageType(Constants.PUBLIC_START + 2)]
    public class GetRoleSnapResponse : Response
    {
        public TLClientRoleSnap[] s2c_data;
    }

    [MessageType(Constants.PUBLIC_START + 3)]
    public class GetGuildSnapRequest : Request
    {
        public string[] c2s_rolesID;
    }

    [MessageType(Constants.PUBLIC_START + 4)]
    public class GetGuildSnapResponse : Response
    {
        public TLClientGuildSnap[] s2c_data;
    }

    [MessageType(Constants.PUBLIC_START + 5)]
    public class GetRoleSnapExtRequest : Request
    {
        public string[] c2s_rolesID;
    }

    [MessageType(Constants.PUBLIC_START + 6)]
    public class GetRoleSnapExtResponse : Response
    {
        public TLClientRoleSnapExt[] s2c_data;
    }

}