using System;
using System.Collections.Generic;
using DeepCore;
using UnityEngine;

public class DramaContext : IDisposable
{
    private readonly HashMap<int, UnitEntity> mEntities = new HashMap<int, UnitEntity>();
    private readonly List<ISystem> mSystems = new List<ISystem>();
    private int mGenIndex = 1;

    public UnitEntity CreatEntity()
    {
        var ret = new UnitEntity(mGenIndex++);
        mEntities.Add(ret.ID, ret);
        return ret;
    }

    public UnitEntity GetEntity(int id)
    {
        return mEntities.Get(id);
    }

    //todo cleanupSystem
    private void InternalDestroyEntity(UnitEntity entity)
    {
        if (entity.Model != null)
        {
            //todo move to Util
            RenderSystem.Instance.Unload(entity.Model.LoadID);
            entity.Model = null;
        }

        if (entity.Avatar != null)
        {
            foreach (var entry in entity.Avatar.Avatars)
            {
                //todo move to Util
                RenderSystem.Instance.Unload(entry.Value.LoadID);
            }

            entity.Avatar = null;
        }

        if (entity.UnityObject != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(entity.UnityObject.Obj);
        }
    }

    public void DestroyEntity(UnitEntity entity)
    {
        InternalDestroyEntity(entity);
        mEntities.Remove(entity.ID);
    }

    public void DestroyEntity(int id)
    {
        var entity = mEntities.RemoveByKey(id);
        if (entity != null)
        {
            InternalDestroyEntity(entity);
        }
    }

    public void AddSystem(ISystem system)
    {
        mSystems.Add(system);
    }

    public void Update()
    {
        //todo 对象池
        var collections = new List<UnitEntity>[mSystems.Count];
        foreach (var entry in mEntities)
        {
            for (var i = 0; i < mSystems.Count; i++)
            {
                var system = mSystems[i];
                if (system.Filter(entry.Value))
                {
                    if (collections[i] == null)
                    {
                        collections[i] = new List<UnitEntity>();
                    }

                    collections[i].Add(entry.Value);
                }
            }
        }

        for (var i = 0; i < mSystems.Count; i++)
        {
            if (collections[i] != null)
            {
                mSystems[i].Execute(collections[i]);
            }
        }
    }

    public void Dispose()
    {
        foreach (var entry in mEntities)
        {
            InternalDestroyEntity(entry.Value);
        }

        mEntities.Clear();
    }
}