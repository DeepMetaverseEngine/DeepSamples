
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.ClientPartnerBattleProp


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00088002 : 557058
        // base type : 
        public static void W_TLProtocol_Data_ClientPartnerBattleProp(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.ClientPartnerBattleProp)msg;
                        
            output.PutS32(data.maxhp);
            output.PutS32(data.attack);
            output.PutS32(data.defend);
            output.PutS32(data.mdef);
            output.PutS32(data.through);
            output.PutS32(data.block);
            output.PutS32(data.crit);
            output.PutS32(data.hit);
        }
        public static void R_TLProtocol_Data_ClientPartnerBattleProp(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.ClientPartnerBattleProp)msg;
                        
            data.maxhp = input.GetS32();
            data.attack = input.GetS32();
            data.defend = input.GetS32();
            data.mdef = input.GetS32();
            data.through = input.GetS32();
            data.block = input.GetS32();
            data.crit = input.GetS32();
            data.hit = input.GetS32();
        }      
    }
}

