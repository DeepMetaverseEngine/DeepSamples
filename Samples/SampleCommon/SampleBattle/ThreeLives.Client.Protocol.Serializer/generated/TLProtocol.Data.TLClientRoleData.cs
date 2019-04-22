
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.TLClientRoleData


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00062001 : 401409
        // base type : DeepMMO.Data.ClientRoleData
        public static void W_TLProtocol_Data_TLClientRoleData(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.TLClientRoleData)msg;
            W_DeepMMO_Data_ClientRoleData(output, data);
            output.PutS64(data.exp);
            output.PutS64(data.needExp);
            output.PutS64(data.overflowExp);
            output.PutList(data.quests, output.PutObj);
            output.PutObj(data.gameOptionsData);
            output.PutMap(data.ClientModifyData, output.PutUTF, output.PutUTF);
            output.PutEnum8(data.proType);
            output.PutEnum8(data.gender);
            output.PutUTF(data.guildId);
            output.PutUTF(data.guildName);
            output.PutS64(data.FightPower);
            output.PutMap(data.funcOpen, output.PutUTF, output.PutU8);
            output.PutS32(data.PKValue);
            output.PutS32(data.practiceLv);
            output.PutS32(data.TargerSystemLv);
            output.PutS32(data.MasterId);
            output.PutS32(data.VipLv);
            output.PutS32(data.VipCurExp);
            output.PutS32(data.TitleID);
            output.PutUTF(data.TitleNameExt);
            output.PutUTF(data.spouseId);
            output.PutUTF(data.spouseName);
            output.PutS32(data.MedicinePoolCurCount);
            output.PutS32(data.AccumulativeCount);
        }
        public static void R_TLProtocol_Data_TLClientRoleData(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.TLClientRoleData)msg;
            R_DeepMMO_Data_ClientRoleData(input, data);
            data.exp = input.GetS64();
            data.needExp = input.GetS64();
            data.overflowExp = input.GetS64();
            data.quests = input.GetList<TLProtocol.Data.QuestDataSnap>(input.GetObj<TLProtocol.Data.QuestDataSnap>);
            data.gameOptionsData = input.GetObj<TLProtocol.Data.TLGameOptionsData>();
            data.ClientModifyData = input.GetMap<string, string>(input.GetUTF, input.GetUTF);
            data.proType = input.GetEnum8<TLProtocol.Data.TLClientCreateRoleExtData.ProType>();
            data.gender = input.GetEnum8<TLProtocol.Data.TLClientCreateRoleExtData.GenderType>();
            data.guildId = input.GetUTF();
            data.guildName = input.GetUTF();
            data.FightPower = input.GetS64();
            data.funcOpen = input.GetMap<string, byte>(input.GetUTF, input.GetU8);
            data.PKValue = input.GetS32();
            data.practiceLv = input.GetS32();
            data.TargerSystemLv = input.GetS32();
            data.MasterId = input.GetS32();
            data.VipLv = input.GetS32();
            data.VipCurExp = input.GetS32();
            data.TitleID = input.GetS32();
            data.TitleNameExt = input.GetUTF();
            data.spouseId = input.GetUTF();
            data.spouseName = input.GetUTF();
            data.MedicinePoolCurCount = input.GetS32();
            data.AccumulativeCount = input.GetS32();
        }      
    }
}

