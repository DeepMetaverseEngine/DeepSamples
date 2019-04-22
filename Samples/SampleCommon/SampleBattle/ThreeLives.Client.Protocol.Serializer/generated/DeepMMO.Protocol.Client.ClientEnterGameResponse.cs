
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.ClientEnterGameResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x0003300E : 208910
        // base type : DeepMMO.Protocol.Response
        public static void W_DeepMMO_Protocol_Client_ClientEnterGameResponse(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterGameResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutDateTime(data.s2c_suspendTime);
            if (data.IsSuccess==true) {
            output.PutObj(data.s2c_role);
            }
        }
        public static void R_DeepMMO_Protocol_Client_ClientEnterGameResponse(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterGameResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_suspendTime = input.GetDateTime();
            if (data.IsSuccess==true) {
            data.s2c_role = input.GetObj<DeepMMO.Data.ClientRoleData>();
            }
        }      
    }
}

