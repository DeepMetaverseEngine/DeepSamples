
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.ClientGetRolesResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace SampleRPG
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00033008 : 208904
        // base type : DeepMMO.Protocol.Response
        public static void W_DeepMMO_Protocol_Client_ClientGetRolesResponse(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientGetRolesResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            if (data.IsSuccess==true) {
            output.PutList(data.s2c_roles, output.PutOBJ(data.););
            }
        }
        public static void R_DeepMMO_Protocol_Client_ClientGetRolesResponse(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientGetRolesResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            if (data.IsSuccess==true) {
            data.s2c_roles = input.GetList<DeepMMO.Data.RoleSnap>(data. = input.GetOBJ<DeepMMO.Data.RoleSnap>(););
            }
        }      
    }
}

