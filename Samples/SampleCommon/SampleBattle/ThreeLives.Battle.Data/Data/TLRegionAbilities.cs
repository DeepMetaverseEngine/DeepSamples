using System;
using System.Collections.Generic;
using System.Text;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.IO;
using DeepCore.Reflection;

namespace ThreeLives.Battle.Data.Data
{
    [MessageType(0x8802)]
    [Desc("TL扩展传送点功能")]
    public class TLUnitTransportData : RegionAbilityData
    {
        [Expandable]
        [DescAttribute("传送点基础数据", "基础数据")]
        public UnitTransportAbilityData Transport;

        [DescAttribute("区域范围内玩家数量判断，限制达成将无法传送", "区域传送条件")]
        public int AreaCheckPlayerCount = 0;

        [DescAttribute("区域范围内玩家数量判断，是否为全阵营，此处为true则AreaCheckForce失效", "区域传送条件")]
        public bool AreaCheckForceForAll;

        [DescAttribute("区域范围内玩家数量判断，检测指定阵营", "区域传送条件")]
        public byte AreaCheckForce;

        public override string ToString()
        {
            return Transport == null ? "传送到-> " : "传送到-> " + Transport.NextPosition;
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutExt(Transport);
            output.PutS32(AreaCheckPlayerCount);
            output.PutBool(AreaCheckForceForAll);
            output.PutU8(AreaCheckForce);
        }

        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            Transport = input.GetExt<UnitTransportAbilityData>();
            AreaCheckPlayerCount = input.GetS32();
            AreaCheckForceForAll = input.GetBool();
            AreaCheckForce = input.GetU8();
        }
    }

    [MessageType(0x8803)]
    [Desc("玩家复活点-扩展")]
    public class TLRebirthAbilityData : RegionAbilityData
    {
        [DescAttribute("阵营")]
        public int START_Force;

        public override string ToString()
        {
            return "玩家复活点: " + START_Force;
        }

        public override void WriteExternal(IOutputStream output)
        {
            base.WriteExternal(output);
            output.PutS32(START_Force);

        }
        public override void ReadExternal(IInputStream input)
        {
            base.ReadExternal(input);
            this.START_Force = input.GetS32();
        }
    }
}