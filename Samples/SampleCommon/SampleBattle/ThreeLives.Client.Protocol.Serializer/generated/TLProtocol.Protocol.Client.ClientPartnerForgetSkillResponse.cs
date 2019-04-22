
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientPartnerForgetSkillResponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00099018 : 626712
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_ClientPartnerForgetSkillResponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPartnerForgetSkillResponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutS32(data.s2c_partnerID);
            output.PutS32(data.s2c_skillID);
        }
        public static void R_TLProtocol_Protocol_Client_ClientPartnerForgetSkillResponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPartnerForgetSkillResponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_partnerID = input.GetS32();
            data.s2c_skillID = input.GetS32();
        }      
    }
}

