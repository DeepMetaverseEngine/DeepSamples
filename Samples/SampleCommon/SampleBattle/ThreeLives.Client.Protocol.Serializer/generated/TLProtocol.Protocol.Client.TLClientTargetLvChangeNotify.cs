
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientTargetLvChangeNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00030003 : 196611
        // base type : DeepMMO.Protocol.Notify
        public static void W_TLProtocol_Protocol_Client_TLClientTargetLvChangeNotify(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientTargetLvChangeNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutS32(data.s2c_lv);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientTargetLvChangeNotify(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientTargetLvChangeNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.s2c_lv = input.GetS32();
        }      
    }
}
