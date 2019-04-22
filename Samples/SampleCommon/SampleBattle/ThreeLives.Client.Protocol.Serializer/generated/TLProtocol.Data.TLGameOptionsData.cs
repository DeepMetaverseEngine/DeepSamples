
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.TLGameOptionsData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00062003 : 401411
        // base type : 
        public static void W_TLProtocol_Data_TLGameOptionsData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.TLGameOptionsData)msg;
                        
            output.PutBool(data.AutoUseItem);
            output.PutBool(data.SmartSelect);
            output.PutBool(data.AutoRecharge);
            output.PutS32(data.itemID);
            output.PutU8(data.UseThreshold);
            output.PutDateTime(data.ItemCoolDownTimeStamp);
            output.PutDateTime(data.MedicinePoolTimeStamp);
            output.PutMap(data.Options, output.PutUTF, output.PutUTF);
        }
        public static void R_TLProtocol_Data_TLGameOptionsData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.TLGameOptionsData)msg;
                        
            data.AutoUseItem = input.GetBool();
            data.SmartSelect = input.GetBool();
            data.AutoRecharge = input.GetBool();
            data.itemID = input.GetS32();
            data.UseThreshold = input.GetU8();
            data.ItemCoolDownTimeStamp = input.GetDateTime();
            data.MedicinePoolTimeStamp = input.GetDateTime();
            data.Options = input.GetMap<string, string>(input.GetUTF, input.GetUTF);
        }      
    }
}

