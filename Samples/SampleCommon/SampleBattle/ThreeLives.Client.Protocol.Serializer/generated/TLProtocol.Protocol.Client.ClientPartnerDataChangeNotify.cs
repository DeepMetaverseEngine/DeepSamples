
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.ClientPartnerDataChangeNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00099007 : 626695
        // base type : DeepMMO.Protocol.Notify
        public static void W_TLProtocol_Protocol_Client_ClientPartnerDataChangeNotify(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPartnerDataChangeNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutObj(data.s2c_data);
        }
        public static void R_TLProtocol_Protocol_Client_ClientPartnerDataChangeNotify(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.ClientPartnerDataChangeNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.s2c_data = input.GetObj<TLProtocol.Data.ClientPartnerData>();
        }      
    }
}

