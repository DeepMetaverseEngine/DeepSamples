using DeepCore.IO;
using DeepCore.IO;
using DeepMMO.Data;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    /// <summary>
    /// 玩家坐骑数据
    /// </summary>
    [MessageType(TLConstants.TL_MOUNT_START + 1)]
    public class TLRoleMountSnapData : ISerializable
    {
        // 拥有的坐骑列表
        public List<int> mounts = new List<int>();
        // 当前驾驭的坐骑id
        public int currentId = 0;

        // 灵脉Id
        public int veinId = 0;
    }
 
}
