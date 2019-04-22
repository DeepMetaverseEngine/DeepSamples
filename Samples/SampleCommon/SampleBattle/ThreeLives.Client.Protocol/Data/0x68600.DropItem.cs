using DeepCore.IO;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    /// <summary>
    /// 客户端掉落物品假数据 
    /// </summary>
    [MessageType(TLConstants.TL_DROPITEM_START + 1)]
    public class TLDropItemSnapData : ISerializable
    {
        public int TemplateID;//模板ID
        public long Qty;     //数量.
        public int itemType;
        public int pileNum;  //每堆数量
        public int pileMax; //最大堆数
        public int system_notice;
    }

    [MessageType(TLConstants.TL_DROPITEM_START + 2)]
    public class TLMonsterDropData : ISerializable
    {
      
        public int posX;
        public int posY;

        public List<TLDropItemSnapData> dropItemList;

        public int exp;
        public int monsterId;
    }
}
