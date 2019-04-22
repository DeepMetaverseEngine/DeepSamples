
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.ClientPartnerSkillData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00088003 : 557059
        // base type : 
        public static void W_TLProtocol_Data_ClientPartnerSkillData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.ClientPartnerSkillData)msg;
                        
            output.PutS32(data.skillID);
            output.PutS32(data.lv);
            output.PutS32(data.index);
            output.PutEnum8(data.skillType);
        }
        public static void R_TLProtocol_Data_ClientPartnerSkillData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.ClientPartnerSkillData)msg;
                        
            data.skillID = input.GetS32();
            data.lv = input.GetS32();
            data.index = input.GetS32();
            data.skillType = input.GetEnum8<TLProtocol.Data.ClientPartnerSkillData.SkillType>();
        }      
    }
}
