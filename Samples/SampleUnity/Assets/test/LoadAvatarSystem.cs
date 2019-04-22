using System;
using System.Collections.Generic;
using System.Linq;
using DeepCore.Unity3D;
using TLBattle.Common.Plugins;
using UnityEngine;

class LoadAvatarSystem : ISystem
{
    public bool Filter(UnitEntity entity)
    {
        if (entity.Avatar == null)
        {
            return false;
        }

        return (entity.Model.Asset && entity.Avatar.IsDirty) || entity.Avatar.IsBodyDirty;
    }

    private void OnLoadOk(UnitEntity entity, TLAvatarInfo.TLAvatar part, AssetComp comp, AssetGameObject ao)
    {
        var partName = GameUtil.getDummy((int) part);
        var obj = entity.Model.Asset.FindNode(partName);
        if (!obj)
        {
            Debug.LogWarning("not found part " + partName);
            comp.LoadID = comp.PreviousLoadID;
            ao.Unload();
        }
        else
        {
            if (comp.PreviousLoadID != 0)
            {
                RenderSystem.Instance.Unload(comp.LoadID);
            }

            comp.Asset = ao;
            comp.LoadID = ao.ID;
        }

        ao.transform.SetParent(obj.transform, false);
    }

    private void LoadAvatarPart(UnitEntity entity, TLAvatarInfo.TLAvatar part, AssetComp comp)
    {
        if (part == TLAvatarInfo.TLAvatar.Avatar_Body)
        {
            //load model
            entity.Avatar.SetAvatar(part, null);
            entity.Model.AssetBundleName = comp.AssetBundleName;
            return;
        }

        if (string.IsNullOrEmpty(comp.AssetBundleName))
        {
            if (comp.LoadID != 0)
            {
                RenderSystem.Instance.Unload(comp.LoadID);
            }

            comp.Asset = null;
        }

        var ao = AssetGameObject.FromCache(comp.AssetBundleName);
        if (ao)
        {
            OnLoadOk(entity, part, comp, ao);
        }

        comp.LoadID = FuckAssetLoader.Load(comp.AssetBundleName, loader =>
        {
            ao = AssetGameObject.Create(loader);
            OnLoadOk(entity, part, comp, ao);
        }).ID;
    }

    private void LoadAvatar(UnitEntity entity)
    {
        foreach (var entry in entity.Avatar.Avatars)
        {
            if (entry.Value.IsDirty)
            {
                LoadAvatarPart(entity, entry.Key, entry.Value);
            }
        }
    }


    public void Execute(ICollection<UnitEntity> entities)
    {
        foreach (var entity in entities)
        {
            LoadAvatar(entity);
        }
    }
}