
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Protocol.Client.ClientEnterZoneNotify


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace SampleRPG
{
    // DeepMMO.Protocol.Client
    public partial class Serializer
    {
        // msg id    : 0x00034003 : 212995
        // base type : DeepMMO.Protocol.Notify
        public static void W_DeepMMO_Protocol_Client_ClientEnterZoneNotify(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterZoneNotify)msg;
            W_DeepMMO_Protocol_Notify(output, data);
            output.PutUTF(data.s2c_ZoneUUID);
            output.PutS32(data.s2c_MapTemplateID);
            output.PutS32(data.s2c_ZoneTemplateID);
            output.PutS32(data.s2c_RoleUnitTemplateID);
            output.PutUTF(data.s2c_RoleDisplayName);
            output.PutOBJ(data.s2c_RoleData);
        }
        public static void R_DeepMMO_Protocol_Client_ClientEnterZoneNotify(IInputStream input, object msg)
        {
            var data = (DeepMMO.Protocol.Client.ClientEnterZoneNotify)msg;
            R_DeepMMO_Protocol_Notify(input, data);
            data.s2c_ZoneUUID = input.GetUTF();
            data.s2c_MapTemplateID = input.GetS32();
            data.s2c_ZoneTemplateID = input.GetS32();
            data.s2c_RoleUnitTemplateID = input.GetS32();
            data.s2c_RoleDisplayName = input.GetUTF();
            data.s2c_RoleData = input.GetOBJ<DeepCore.IO.ISerializable>();
        }      
    }
}
