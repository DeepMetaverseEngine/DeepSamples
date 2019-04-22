using System.Collections.Generic;
using System.Linq;
using DeepCore;
using DeepCore.GameData.Zone;
using TLBattle.Common.Plugins;
using UnityEngine;

public class GameObjectComp
{
    public GameObject Obj;

    public GameObjectComp(GameObject obj)
    {
        Obj = obj;
    }
}

public class AssetComp
{
    private AssetGameObject mAsset;

    public AssetComp(string name)
    {
        AssetBundleName = name;
    }

    public AssetGameObject Asset
    {
        get { return mAsset; }
        set
        {
            mAsset = value;
            PreviousLoadID = 0;
        }
    }

    private int mLoadID;

    public int LoadID
    {
        get { return mLoadID; }
        set
        {
            if (mLoadID != 0 && PreviousLoadID != value)
            {
                PreviousLoadID = mLoadID;
            }

            mLoadID = value;
            IsDirty = false;
        }
    }

    public bool IsEmpty
    {
        get { return LoadID == 0 && mAssetBundleName == null; }
    }

    public bool IsDirty { get; private set; }

    private string mAssetBundleName;

    public string AssetBundleName
    {
        get { return mAssetBundleName; }
        set
        {
            if (mAssetBundleName == value)
            {
                return;
            }

            if (LoadID != 0 || value != null)
            {
                IsDirty = true;
            }

            mAssetBundleName = value;
        }
    }

    public int PreviousLoadID { get; private set; }

    public bool IsLoadOK
    {
        get { return Asset && !IsDirty; }
    }
}

public interface ISystem
{
    bool Filter(UnitEntity entity);
    void Execute(ICollection<UnitEntity> entities);
}

public class AvatarComp
{
    /// <summary>
    /// part-asset
    /// </summary>
    public readonly HashMap<TLAvatarInfo.TLAvatar, AssetComp> Avatars = new HashMap<TLAvatarInfo.TLAvatar, AssetComp>();

    public bool IsLoadOK
    {
        get { return Avatars.All(m => m.Value.IsLoadOK); }
    }

    public void SetAvatar(TLAvatarInfo.TLAvatar part, string abName)
    {
        AssetComp comp;
        if (Avatars.TryGetValue(part, out comp))
        {
            comp.AssetBundleName = abName;
        }
        else if (!string.IsNullOrEmpty(abName))
        {
            Avatars.Add(part, new AssetComp(abName));
        }
    }

    public bool IsDirty
    {
        get { return Avatars.Any(m => m.Value.IsDirty); }
    }

    public AssetComp GetAvatar(TLAvatarInfo.TLAvatar part)
    {
        return Avatars.Get(part);
    }

    public bool ContainsPart(TLAvatarInfo.TLAvatar part)
    {
        var comp = GetAvatar(part);
        return comp != null && !comp.IsEmpty;
    }

    public bool IsBodyDirty
    {
        get
        {
            var comp = GetAvatar(TLAvatarInfo.TLAvatar.Avatar_Body);
            return comp != null && comp.IsDirty;
        }
    }
}

public class ZoneComp
{
    public Vector2 Pos;
    public float Direction;
}

public class AnimationComp
{
    public string Name;
}

public class UnitEntity
{
    /// <summary>
    /// 资源
    /// </summary>
    public AssetComp Model;

    /// <summary>
    /// Avartar
    /// </summary>
    public AvatarComp Avatar;

    /// <summary>
    /// Unity位置信息
    /// </summary>
    public TransformSet Location = new TransformSet();

    /// <summary>
    /// 挂载的节点
    /// </summary>
    public GameObjectComp UnityObject;

    /// <summary>
    /// 动画
    /// </summary>
    public AnimationComp Animation;

    /// <summary>
    /// 逻辑位置
    /// </summary>
    public ZoneComp LogicLocation = new ZoneComp();

    public readonly int ID;

    public UnitEntity(int id)
    {
        ID = id;
    }
}