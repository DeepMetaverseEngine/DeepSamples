using System.Collections.Generic;
using System;
using TLBattle.Common.Plugins;

/**
 * 
 * __   (__`\
 * (__`\   \\`\
 *  `\\`\   \\ \
 *    `\\`\  \\ \
 *      `\\`\#\\ \#
 *        \_ ##\_ |##
 *        (___)(___)##
 *         (0)  (0)`\##
 *          |~   ~ , \##
 *          |      |  \##
 *          |     /\   \##         __..---'''''-.._.._
 *          |     | \   `\##  _.--'                _  `.
 *          Y     |  \    `##'                     \`\  \
 *         /      |   \                             | `\ \
 *        /_...___|    \                            |   `\\
 *       /        `.    |       新时装公用方法        /      ##
 *      |          |    |                         /      ####
 *      |          |    |                        /       ####
 *      | () ()    |     \     |          |  _.-'         ##
 *      `.        .'      `._. |______..| |-'|
 *        `------'           | | | |    | || |
 *                           | | | |    | || |
 *                           | | | |    | || |
 *                           | | | |    | || |     
 *                     _____ | | | |____| || |
 *                    /     `` |-`/     ` |` |
 *                    \________\__\_______\__\
 *                     """""""""   """""""'"""
 *
 */

public enum RES_START_SERIAL
{
    CHAR_HEAD_START = 100000,
    CHAR_CLOTHES_START = 200000,
}


/// <summary>
/// 工具
/// </summary>
public class AttachLoadInfo
{
    public string dummy;
    public string fileName;
    public string fileColor;
}

public static class AvatarHelp
{
    public static bool PopHeadAvatar(List<AttachLoadInfo> equipLoads, out AttachLoadInfo data)
    {
        foreach (var elem in equipLoads)
        {
            string assetName = System.IO.Path.GetFileNameWithoutExtension(elem.fileName);
            string[] subs = assetName.Split('_');
            if (subs.Length > 0)
            {
                int id = 0;
                int.TryParse(subs[subs.Length - 1], out id);
                if (id >= (int)RES_START_SERIAL.CHAR_HEAD_START && id < (int)RES_START_SERIAL.CHAR_CLOTHES_START)
                {
                    data = elem;
                    equipLoads.Remove(elem);
                    return true;
                }
            }
        }

        data = null;
        return false;
    }

    //处理部件信息
    public static void InitParts(List<TLAvatarInfo> _AvatarList, ref string _Body, ref string _Color, ref List<AttachLoadInfo> _EquipLoads)
    {
        foreach (TLAvatarInfo elem in _AvatarList)
        {
            string[] tmp = elem.FileName.Split(';');
            foreach (var elem2 in tmp)
            {
                string[] s = elem2.Split(',');
                if (s != null && s.Length == 2)
                {
                    AttachLoadInfo p = new AttachLoadInfo();
                    p.dummy = s[0];
                    p.fileName = s[1];
                    p.fileColor = elem.DefaultName;
                    if (p.dummy.Equals("Avatar_Body", StringComparison.OrdinalIgnoreCase))
                    {
                        _Body = p.fileName;
                        _Color = p.fileColor;
                    }
                    else
                    {
                        _EquipLoads.Add(p);
                    }
                }
            }
        }
    }

    //处理部件列表
    public static List<AttachLoadInfo> InitPartList(ref List<TLAvatarInfo> _oldList, ref List<TLAvatarInfo> _newList)
    {
        List<TLAvatarInfo> removeList = new List<TLAvatarInfo>();
        List<TLAvatarInfo> removeListold = new List<TLAvatarInfo>();

        List<AttachLoadInfo> removeParts = new List<AttachLoadInfo>();

        foreach (TLAvatarInfo item in _newList)
        {
            TLAvatarInfo info = null;
            bool needRemove = false;

            foreach (TLAvatarInfo itemold in _oldList)
            {
                if (itemold.PartTag == item.PartTag)
                {
                    info = itemold;
                    removeListold.Add(info);
                }
            }

            if (info != null)
            {
                if (string.IsNullOrEmpty(item.FileName))
                {
                    removeList.Add(item);
                    needRemove = true;
                }
                else
                {
                    //if (!item.FileName.ToLower().Equals(info.FileName.ToLower()))
                    {
                        needRemove = true;
                    }
                }
            }

            if (needRemove)
            {
                List<AttachLoadInfo> newParts = new List<AttachLoadInfo>();
                List<AttachLoadInfo> oldParts = new List<AttachLoadInfo>();

                string[] tmp = info.FileName.Split(';');
                foreach (var elem2 in tmp)
                {
                    string[] s = elem2.Split(',');
                    if (s != null && s.Length == 2)
                    {
                        AttachLoadInfo p = new AttachLoadInfo();
                        p.dummy = s[0];
                        p.fileName = s[1];
                        p.fileColor = info.DefaultName;

                        removeParts.Add(p);
                    }
                }
                
            }
        }

        foreach (TLAvatarInfo item in removeList)
        {
            _newList.Remove(item);
        }

        foreach (TLAvatarInfo item in removeListold)
        {
            _oldList.Remove(item);
        }

        _oldList.AddRange(_newList);

        removeList.Clear();
        removeList = null;
        removeListold.Clear();
        removeListold = null;

        return removeParts;
    }

    //添加部件
    public static void AddParts(ref List<TLAvatarInfo> _oldList, List<TLAvatarInfo> _newList)
    {
        List<TLAvatarInfo> removeList = new List<TLAvatarInfo>();
        foreach (TLAvatarInfo item in _oldList)
        {
            foreach (TLAvatarInfo additem in _newList)
            {
                if (item.PartTag == additem.PartTag)
                {
                    removeList.Add(item);
                }
            }
        }
        foreach (TLAvatarInfo item in removeList)
        {
            _oldList.Remove(item);
        }

        _oldList.AddRange(_newList);
    }
}
