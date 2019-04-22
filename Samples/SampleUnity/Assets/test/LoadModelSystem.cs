using DeepCore.Unity3D;
using System.Collections.Generic;
using UnityEngine;

public class LoadModelSystem : ISystem
{
    public bool Filter(UnitEntity entity)
    {
        return entity.Model != null && entity.Model.IsDirty;
    }

    private void OnLoadOk(UnitEntity entity, AssetGameObject ao)
    {
        var comp = entity.Model;
        if (comp.PreviousLoadID != 0)
        {
            RenderSystem.Instance.Unload(comp.PreviousLoadID);
        }

        comp.LoadID = ao.ID;
        comp.Asset = ao;
        entity.UnityObject = new GameObjectComp(new GameObject(entity.ToString()));
        ao.transform.SetParent(entity.UnityObject.Obj.transform, false);
    }

    private void LoadGameObject(UnitEntity entity)
    {
        var comp = entity.Model;
        if (string.IsNullOrEmpty(comp.AssetBundleName))
        {
            if (comp.LoadID != 0)
            {
                RenderSystem.Instance.Unload(comp.LoadID);
            }

            comp.Asset = null;
            comp.LoadID = 0;
        }
        var ao = AssetGameObject.FromCache(comp.AssetBundleName);
        if (ao)
        {
            OnLoadOk(entity, ao);
        }
        else
        {
            comp.LoadID = FuckAssetLoader.Load(comp.AssetBundleName, loader =>
            {
                ao = AssetGameObject.Create(loader);
                OnLoadOk(entity, ao);
            }).ID;
        }
    }

    public void Execute(ICollection<UnitEntity> entities)
    {
        foreach (var entity in entities)
        {
            LoadGameObject(entity);
        }
    }
}