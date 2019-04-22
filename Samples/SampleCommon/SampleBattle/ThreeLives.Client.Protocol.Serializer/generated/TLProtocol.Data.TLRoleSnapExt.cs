
// Warning: do not edit this file.
// 警告: 不要编辑此文件

// ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// TLProtocol.Data.TLRoleSnapExt


using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLClient
{
    // TLProtocol.Data
    public partial class Serializer
    {
        // msg id    : 0x00062007 : 401415
        // base type : 
        public static void W_TLProtocol_Data_TLRoleSnapExt(IOutputStream output, object msg)
        {
            var data = (TLProtocol.Data.TLRoleSnapExt)msg;
                        
            output.PutUTF(data.uuid);
            output.PutS32(data.PKValue);
            output.PutS32(data.MountId);
            output.PutS32(data.VeinId);
            output.PutS32(data.WingLv);
            output.PutMap(data.SuitEquipMap, output.PutS32, output.PutS32);
            output.PutObj(data.PropSnap);
            output.PutMap(data.ArtifactMap, output.PutS32, output.PutS32);
            output.PutList(data.ArtifactSkillList, output.PutObj);
            output.PutObj(data.GodSkillData);
            output.PutMap(data.GoldMap, output.PutS32, output.PutS32);
            output.PutMap(data.EquipMap, output.PutS32, output.PutObj);
            output.PutMap(data.GemMap, output.PutS32, output.PutObj);
            output.PutMap(data.RefineMap, output.PutS32, output.PutObj);
            output.PutMap(data.SkillMap, output.PutS32, output.PutS32);
            output.PutDateTime(data.ExpiredUtc);
        }
        public static void R_TLProtocol_Data_TLRoleSnapExt(IInputStream input, object msg)
        {
            var data = (TLProtocol.Data.TLRoleSnapExt)msg;
                        
            data.uuid = input.GetUTF();
            data.PKValue = input.GetS32();
            data.MountId = input.GetS32();
            data.VeinId = input.GetS32();
            data.WingLv = input.GetS32();
            data.SuitEquipMap = input.GetMap<int, int>(input.GetS32, input.GetS32);
            data.PropSnap = input.GetObj<TLProtocol.Data.TLUnitPropSnap>();
            data.ArtifactMap = input.GetMap<int, int>(input.GetS32, input.GetS32);
            data.ArtifactSkillList = input.GetList<TLBattle.Common.Plugins.GameSkill>(input.GetObj<TLBattle.Common.Plugins.GameSkill>);
            data.GodSkillData = input.GetObj<TLBattle.Common.Plugins.GodData>();
            data.GoldMap = input.GetMap<int, int>(input.GetS32, input.GetS32);
            data.EquipMap = input.GetMap<int, TLProtocol.Data.EntityItemData>(input.GetS32, input.GetObj<TLProtocol.Data.EntityItemData>);
            data.GemMap = input.GetMap<int, TLProtocol.Data.GridGemData>(input.GetS32, input.GetObj<TLProtocol.Data.GridGemData>);
            data.RefineMap = input.GetMap<int, TLProtocol.Data.GridRefineData>(input.GetS32, input.GetObj<TLProtocol.Data.GridRefineData>);
            data.SkillMap = input.GetMap<int, int>(input.GetS32, input.GetS32);
            data.ExpiredUtc = input.GetDateTime();
        }      
    }
}
