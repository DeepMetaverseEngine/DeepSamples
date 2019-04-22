
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Client.TLClientAchievementListReponse


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00040203 : 262659
        // base type : DeepMMO.Protocol.Response
        public static void W_TLProtocol_Protocol_Client_TLClientAchievementListReponse(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientAchievementListReponse)msg;
            W_DeepMMO_Protocol_Response(output, data);
            output.PutList(data.s2c_data, output.PutObj);
            output.PutList(data.s2c_catalogdata, output.PutObj);
            output.PutS32(data.s2c_curFinishPoints);
            output.PutS32(data.s2c_totalFinishPoints);
        }
        public static void R_TLProtocol_Protocol_Client_TLClientAchievementListReponse(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Client.TLClientAchievementListReponse)msg;
            R_DeepMMO_Protocol_Response(input, data);
            data.s2c_data = input.GetList<TLProtocol.Data.AchievementDataSnap>(input.GetObj<TLProtocol.Data.AchievementDataSnap>);
            data.s2c_catalogdata = input.GetList<TLProtocol.Data.CatalogData>(input.GetObj<TLProtocol.Data.CatalogData>);
            data.s2c_curFinishPoints = input.GetS32();
            data.s2c_totalFinishPoints = input.GetS32();
        }      
    }
}

