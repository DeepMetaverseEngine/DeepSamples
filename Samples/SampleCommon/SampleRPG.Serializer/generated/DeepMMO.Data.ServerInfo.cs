
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// DeepMMO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// DeepMMO.Data.ServerInfo


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace SampleRPG
{
    // DeepMMO.Data
    public partial class Serializer
    {
        // msg id    : 0x00020002 : 131074
        // base type : 
        public static void W_DeepMMO_Data_ServerInfo(IOutputStream output, object msg)
        {
            var data = (DeepMMO.Data.ServerInfo)msg;
                        
            output.PutUTF(data.ZoneID);
            output.PutUTF(data.IconID);
            output.PutUTF(data.ServerID);
            output.PutUTF(data.LogicID);
            output.PutUTF(data.ServerState);
            output.PutUTF(data.ServerType);
            output.PutUTF(data.ServerName);
            output.PutBool(data.IsPublic);
            output.PutUTF(data.ServerIP);
            output.PutS32(data.ServerPort);
            output.PutS32(data.ServerSort);
            output.PutS32(data.RoleNum);
        }
        public static void R_DeepMMO_Data_ServerInfo(IInputStream input, object msg)
        {
            var data = (DeepMMO.Data.ServerInfo)msg;
                        
            data.ZoneID = input.GetUTF();
            data.IconID = input.GetUTF();
            data.ServerID = input.GetUTF();
            data.LogicID = input.GetUTF();
            data.ServerState = input.GetUTF();
            data.ServerType = input.GetUTF();
            data.ServerName = input.GetUTF();
            data.IsPublic = input.GetBool();
            data.ServerIP = input.GetUTF();
            data.ServerPort = input.GetS32();
            data.ServerSort = input.GetS32();
            data.RoleNum = input.GetS32();
        }      
    }
}

