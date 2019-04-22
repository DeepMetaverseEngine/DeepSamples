
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientQuestDataRemoveNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00073010 : 471056
        // base type : DeepMMO.Protocol.Notify
        public static void W_TLProtocol_Protocol_Client_TLClientQuestDataRemoveNotify(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientQuestDataRemoveNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutS32(data.QuestID);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientQuestDataRemoveNotify(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientQuestDataRemoveNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.QuestID = input.GetS32();
        }      
    }
}
