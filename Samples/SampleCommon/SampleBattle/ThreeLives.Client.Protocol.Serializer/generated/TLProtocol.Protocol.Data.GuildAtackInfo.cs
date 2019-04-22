
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Protocol.Data.GuildAtackInfo


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Protocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00000007 : 7
        // base type : 
        public static void W_TLProtocol_Protocol_Data_GuildAtackInfo(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Protocol.Data.GuildAtackInfo)msg;
                        
            output.PutS32(data.attackCount);
            output.PutMap(data.attackType, output.PutS32, output.PutS32);
            output.PutDateTime(data.lastAttackTime);
        }
        public static void R_TLProtocol_Protocol_Data_GuildAtackInfo(IInputStream input, object msg)
        {
            var data = (TLProtocol.Protocol.Data.GuildAtackInfo)msg;
                        
            data.attackCount = input.GetS32();
            data.attackType = input.GetMap<int, int>(input.GetS32, input.GetS32);
            data.lastAttackTime = input.GetDateTime();
        }      
    }
}

