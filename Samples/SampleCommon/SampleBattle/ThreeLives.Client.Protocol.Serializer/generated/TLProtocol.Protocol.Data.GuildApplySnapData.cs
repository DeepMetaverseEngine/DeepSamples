
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Data.GuildApplySnapData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00087006 : 552966
        // base type : 
        public static void W_TLProtocol_Protocol_Data_GuildApplySnapData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Data.GuildApplySnapData)msg;
                        
            output.PutUTF(data.msgId);
            output.PutUTF(data.roleId);
            output.PutUTF(data.name);
            output.PutS32(data.level);
            output.PutS32(data.gender);
            output.PutS32(data.pro);
            output.PutS64(data.power);
            output.PutBool(data.online);
        }
        public static void R_TLProtocol_Protocol_Data_GuildApplySnapData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Data.GuildApplySnapData)msg;
                        
            data.msgId = input.GetUTF();
            data.roleId = input.GetUTF();
            data.name = input.GetUTF();
            data.level = input.GetS32();
            data.gender = input.GetS32();
            data.pro = input.GetS32();
            data.power = input.GetS64();
            data.online = input.GetBool();
        }      
    }
}
