using DeepCore.IO;
using DeepCore.IO;
using System;
using System.Collections.Generic;
using TLBattle.Common.Plugins;

namespace TLBattle.Common.Data
{

    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 10)]
    public class TLDropItem : ICloneable, IExternalizable
    {
        public string ObjID;
        public string Name;
        public string FileName;//
        public int Qty;//数量.
        public int TTL;//存在时间.
        public byte Quality;//品质.
        public int ItemId;
        public int TemplateID;//模板ID
        public int FreezeTime;//不可捡时间时间.
        public int ProtectTime;//保护时间.
        public List<string> HeirsList = null;

        public string IconName;//图标.
        public byte EffectQuality; //特效.
        public bool VirtualItem;//是否为虚拟道具(是否计算背包格子).
        public byte Mode;//拾取模式.
        public float OriginX;//初始坐标X.
        public float OriginY;//初始坐标Y.

        public object Clone()
        {
            TLDropItem ret = new TLDropItem();
            ret.ObjID = ObjID;
            ret.Name = Name;
            ret.FileName = FileName;
            ret.Qty = Qty;
            ret.TTL = TTL;
            ret.Quality = Quality;
            ret.ItemId = ItemId;
            ret.TemplateID = TemplateID;
            ret.FreezeTime = FreezeTime;
            ret.ProtectTime = ProtectTime;
            ret.HeirsList = HeirsList;

            ret.IconName = IconName;
            ret.EffectQuality = EffectQuality;
            ret.VirtualItem = VirtualItem;
            ret.Mode = Mode;
            ret.OriginX = OriginX;
            ret.OriginY = OriginY;
            return ret;
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutUTF(ObjID);
            output.PutUTF(Name);
            output.PutUTF(FileName);
            output.PutS32(Qty);
            output.PutS32(TTL);
            output.PutU8(Quality);
            output.PutS32(ItemId);
            output.PutS32(TemplateID);
            output.PutS32(FreezeTime);
            output.PutS32(ProtectTime);
            output.PutList<string>(HeirsList, output.PutUTF);

            output.PutUTF(IconName);
            output.PutU8(EffectQuality);
            output.PutBool(VirtualItem);
            output.PutU8(Mode);
            output.PutF32(OriginX);
            output.PutF32(OriginY);
        }

        public void ReadExternal(IInputStream input)
        {
            ObjID = input.GetUTF();
            Name = input.GetUTF();
            FileName = input.GetUTF();
            Qty = input.GetS32();
            TTL = input.GetS32();
            Quality = input.GetU8();
            ItemId = input.GetS32();
            TemplateID = input.GetS32();
            FreezeTime = input.GetS32();
            ProtectTime = input.GetS32();
            HeirsList = input.GetUTFList();

            IconName = input.GetUTF();
            EffectQuality = input.GetU8();
            VirtualItem = input.GetBool();
            Mode = input.GetU8();
            OriginX = input.GetF32();
            OriginY = input.GetF32();
        }

    }
}
