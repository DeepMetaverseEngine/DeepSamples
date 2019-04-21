
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.PlayerDynamicNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace SampleRPG
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00035003 : 217091
        // base type : DeepMMO.Protocol.Notify
        public static void W_DeepMMO_Protocol_Client_PlayerDynamicNotify(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.PlayerDynamicNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutList(data.s2c_data, output.PutOBJ(data.););
        }
        public static void R_DeepMMO_Protocol_Client_PlayerDynamicNotify(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.PlayerDynamicNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.s2c_data = input.GetList<DeepMMO.Data.PropertyStruct>(data. = input.GetOBJ<DeepMMO.Data.PropertyStruct>(););
        }      
    }
}
