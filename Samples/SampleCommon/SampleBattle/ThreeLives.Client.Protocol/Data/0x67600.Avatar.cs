using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_AVATAR_START + 1)]
    public class TLAvatarDataSnap : ISerializable
    {
        // avatarId 剩余天数
        public HashMap<int, int> avatarMap;

        public List<string> requireList;


    }

    //角色时装信息
    [MessageType(TLConstants.TL_AVATAR_START + 2)]
    public class AvatarInfoSnap : ISerializable
    {
        public int PartTag = 0;
        public string FileName = null;
        //新时装用来标识显示的颜色
        public string DefaultName = null;

        public AvatarInfoSnap()
        {
        }
    }
}