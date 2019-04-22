using DeepCore.Unity3D;
using System;
using System.Collections.Generic;
using System.Linq;
using DeepCore.GameData.Zone;
using UnityEngine;
using DeepCore;
using SLua;
using TLBattle.Common.Plugins;
using Action = System.Action;
using Object = UnityEngine.Object;

public class TransformSet
{
    private static readonly Vector3 InvalidDeg = new Vector3(-999, -999, -999);
    private static readonly Quaternion InvalidRotation = new Quaternion(-999, -999, -999, -999);
    public const int InvalidLayerOrder = int.MinValue;
    public Transform Parent;
    public RectTransform Clip;
    public bool DisableToUnload;
    public Vector3 Pos;
    public Vector3 Scale = Vector3.one;
    public Vector3 Deg = InvalidDeg;
    public bool Visible = true;
    public Quaternion Rotation = InvalidRotation;
    public Vector2 Vectormove = Vector2.zero;
    public int Layer;
    public string AnimatorState;
    public int LayerOrder = InvalidLayerOrder;
    public string Name;
    public float FScale = 1.0f;

    public bool HasLayerOrder
    {
        get { return LayerOrder != InvalidLayerOrder; }
    }

    public bool HasDeg
    {
        get { return !Deg.Equals(InvalidDeg); }
    }

    public bool HasQuaternion
    {
        get { return !Rotation.Equals(InvalidRotation); }
    }

    public bool HasVectormove
    {
        get { return !Vectormove.Equals(Vector2.zero); }
    }
}

/// <summary>
/// 先临时用Instance，todo 后面有空挂在GameWorld中统一管理
/// todo ecs
/// todo 拆分System
/// </summary>
public class RenderSystem
{
    public static readonly RenderSystem Instance;

    static RenderSystem()
    {
        Instance = new RenderSystem();
    }

    private void CancelAvatarTask(int id)
    {
        var loading = mAvatarTasks.RemoveByKey(id);
        if (loading != null && loading.Part != null)
        {
            foreach (var entry in loading.Part)
            {
                entry.Value.Unload();
            }
        }
    }
    
    public void Unload(GameObject obj)
    {
        var comp = obj.GetComponent<AssetGameObject>();
        if (comp)
        {
            comp.Unload();
            CancelAvatarTask(comp.ID);
        }
        else
        {
            DeepCore.Unity3D.UnityHelper.Destroy(obj);
        }
    }

    public void Unload(int id)
    {
        if (id == 0)
        {
            return;
        }
        CancelAvatarTask(id);
        var loader = FuckAssetLoader.GetLoader(id);
        if (loader != null && !loader.IsDone)
        {
            loader.Discard();
        }
        else
        {
            var aoe = AssetGameObject.GetAssetObject(id);
            if (aoe)
            {
                aoe.Unload();
            }
        }
    }

    public bool IsLoadSuccess(int id)
    {
        var state = mAvatarTasks.Get(id);
        if (state != null)
        {
            return state.IsSuccess;
        }

        var aoe = AssetGameObject.GetAssetObject(id);
        if (aoe)
        {
            return !aoe.IsUnload;
        }

        return false;
    }


    public int LoadGameObject(string fileName, TransformSet t, Action<AssetGameObject> successCb)
    {
        return LoadGameObject(fileName, t, successCb, null);
    }

    public int LoadGameObject(string fileName, TransformSet t)
    {
        return LoadGameObject(fileName, t, null);
    }

    public int LoadGameObject(string fileName, Action<AssetGameObject> successCb)
    {
        return LoadGameObject(fileName, null, successCb);
    }

    public int LoadGameObject(string fileName, Action<AssetGameObject> successCb, Action failedCb)
    {
        return LoadGameObject(fileName, null, successCb, failedCb);
    }

    public int LoadGameObject(string fileName, TransformSet t, Action<AssetGameObject> successCb, Action failedCb)
    {
        return LoadGameObject(fileName, t, false, 0, successCb, failedCb);
    }

    private int LoadGameObject(string fileName, TransformSet t, bool allowImmediate, object state, Action<AssetGameObject, object> cb)
    {
        return LoadGameObject(fileName, t, allowImmediate, 0, state, cb);
    }

    private int LoadGameObject(string fileName, TransformSet t, bool allowImmediate, int parentID, Action<AssetGameObject> successCb, Action failedCb)
    {
        return LoadGameObject(fileName, t, allowImmediate, parentID, null, (ao, state) =>
        {
            if (ao)
            {
                if (successCb != null)
                {
                    successCb.Invoke(ao);
                }
            }
            else
            {
                if (failedCb != null)
                {
                    failedCb.Invoke();
                }
            }
        });
    }


    public static void ResetMaterial(AssetGameObject ao)
    {
        var renderers = ao.GetComponentsInChildren<Renderer>(true);
        foreach (var render in renderers)
        {
            var mm = render.gameObject.GetComponent<MaterialManager>();
            if (mm == null)
            {
                mm = render.gameObject.AddComponent<MaterialManager>();
                mm.SaveOrgMat();
            }
            else
            {
                mm.ResetMatState();
            }
        }
    }

    private void OnGameObjectLoadSuccess(AssetGameObject ao, TransformSet t)
    {
        if (t != null)
        {
            SetTransform(ao, t);
        }
        else
        {
            ao.gameObject.SetActive(false);
        }
    }

    private int LoadGameObject(string fileName, TransformSet t, bool allowImmediate, int parentID, object state, Action<AssetGameObject, object> cb)
    {
        var ao = AssetGameObject.FromCache(fileName);
        if (ao)
        {
            OnGameObjectLoadSuccess(ao, t);
            if (cb != null)
            {
                if (allowImmediate)
                {
                    cb.Invoke(ao, state);
                }
                else
                {
                    UnityHelper.WaitForEndOfFrame(() => { cb.Invoke(ao, state); });
                }
            }

            return ao.ID;
        }

        var cLoader = FuckAssetLoader.Load(fileName, loader =>
        {
            if (parentID != 0 && IsUnload(parentID))
            {
                HZUnityAssetBundleManager.GetInstance().CheckAssetRef(loader.BundleName);
                return;
            }
            ao = AssetGameObject.Create(loader);
            if (ao != null)
            {
                OnGameObjectLoadSuccess(ao, t);
            }

            if (!allowImmediate || !loader.ActualImmediate)
            {
                cb.Invoke(ao, state);
            }
            else
            {
                UnityHelper.WaitForEndOfFrame(() => { cb.Invoke(ao, state); });
            }
        });
        return cLoader.ID;
    }

    public void SetTransform(AssetGameObject aoe, TransformSet info)
    {
        if (!aoe)
        {
            return;
        }

        if (!string.IsNullOrEmpty(info.Name))
        {
            aoe.gameObject.name = info.Name;
        }

        aoe.DisableToUnload = info.DisableToUnload;
        aoe.transform.SetParent(info.Parent, false);
        aoe.gameObject.SetActive(info.Visible);
        aoe.transform.localPosition = info.Pos;
        aoe.transform.localScale = info.Scale * info.FScale;

        if (!info.HasDeg && !info.HasQuaternion)
        {
            aoe.transform.localRotation = Quaternion.identity;
        }

        if (info.Layer > 0)
        {
            SetLayer(aoe, info.Layer, info.LayerOrder);
            if (info.Layer == (int) PublicConst.LayerSetting.UI)
            {
                var lastv3 = aoe.transform.localPosition;
                if (Math.Abs(lastv3.z) < 0.0001)
                {
                    aoe.transform.localPosition = new Vector3(lastv3.x, lastv3.y, -200);
                }

                if (info.Clip)
                {
                    var effectclip = aoe.gameObject.AddComponent<EffectClip>();
                    effectclip.m_rectTrans = info.Clip;
                    aoe.OnBeforeUnload += (ao, tocache) =>
                    {
                        if (tocache)
                        {
                            Object.Destroy(effectclip);
                        }
                    };
                }
            }
        }
        else if (aoe.gameObject.layer != 0)
        {
            SetLayer(aoe, 0, 0);
        }

        if (info.HasQuaternion)
        {
            aoe.transform.localRotation = info.Rotation;
        }

        if (info.HasDeg)
        {
            aoe.transform.localRotation = Quaternion.Euler(info.Deg);
        }

        if (info.HasVectormove)
        {
            var vector3Move = aoe.transform.gameObject.GetComponentInChildren<Vector3Move>();
            if (vector3Move != null)
            {
                vector3Move.setRect((int) info.Vectormove.x, (int) info.Vectormove.y);
            }
        }

        if (!string.IsNullOrEmpty(info.AnimatorState))
        {
            aoe.Play(info.AnimatorState);
        }
    }

    public void SetLayer(AssetGameObject aoe, int layer, int layerOrder)
    {
        if (layerOrder != TransformSet.InvalidLayerOrder)
        {
            UILayerMgr.SetLayerOrder(aoe.gameObject, layerOrder, false, layer);
        }
        else
        {
            UILayerMgr.SetLayer(aoe.gameObject, layer);
        }
    }


    public bool IsUnload(int id)
    {
        var ao = AssetGameObject.GetAssetObject(id);
        return !ao || ao.IsUnload;
    }

    public AssetGameObject GetAssetGameObject(int id)
    {
        return AssetGameObject.GetAssetObject(id);
    }

    public int PlayEffect(string fileName, TransformSet info, float duration)
    {
        return PlayEffect(fileName, info, duration, null);
    }

    public int PlayEffect(string fileName, TransformSet info)
    {
        return PlayEffect(fileName, info, 0);
    }

    private void PlayEffect(AssetGameObject aoe, float duration, Action<int> finishcb = null)
    {
        aoe.SetAsEffect(duration);
        if (finishcb != null)
        {
            aoe.OnBeforeUnload += (ao, tocache) => { finishcb.Invoke(ao.ID); };
        }
    }

    public int PlayEffect(string fileName, TransformSet info, float duration, Action<int> finishcb)
    {
        if (!info.Parent && info.Layer == (int) PublicConst.LayerSetting.UI)
        {
            info.Parent = HZUISystem.Instance.GetPickLayer().Transform;
        }

        return LoadGameObject(fileName, info, true, 0, aoe => { PlayEffect(aoe, duration, finishcb); }, null);
    }

    public int PlayEffect(LaunchEffect effect, AssetGameObject owner)
    {
        return LoadGameObject(effect.Name, aoe =>
        {
            if (owner.IsUnload)
            {
                aoe.Unload();
                return;
            }

            if (effect.BindBody)
            {
                if (!string.IsNullOrEmpty(effect.BindPartName))
                {
                    var obj = owner.FindNode(effect.BindPartName);
                    if (obj)
                    {
                        aoe.transform.SetParent(obj.transform);
                    }
                    else
                    {
                        aoe.Unload();
                        Debugger.LogWarning("not found bindpart");
                        return;
                    }
                }
                else
                {
                    aoe.transform.SetParent(owner.transform);
                }

                aoe.transform.localPosition = Vector3.zero;
            }
            else
            {
                aoe.transform.position = owner.transform.position;
            }

            if (Math.Abs(effect.ScaleToBodySize) > 0.01)
            {
                aoe.transform.localScale = new Vector3(effect.ScaleToBodySize, effect.ScaleToBodySize, effect.ScaleToBodySize);
            }
            else
            {
                aoe.transform.localScale = Vector3.one;
            }

            aoe.transform.rotation = Quaternion.identity;
            PlayEffect(aoe, effect.EffectTimeMS);
        });
    }


    public void SetEffectVisible(int id, bool visible)
    {
        var aoe = AssetGameObject.GetAssetObject(id);
        if (aoe != null)
        {
            aoe.gameObject.SetActive(visible);
        }
    }

    public bool IsFinishPlayEffect(int id)
    {
        var aoe = AssetGameObject.GetAssetObject(id);
        var loader = FuckAssetLoader.GetLoader(id);
        if (loader != null && !loader.IsDone)
        {
            return false;
        }

        return !aoe || !aoe.gameObject.activeInHierarchy;
    }

    #region Avatar Load

    public class AvartarLoding
    {
        public HashMap<int, TLAvatarInfo> Avatarmap;
        public int LoadingCount;
        public AssetGameObject Body;
        public HashMap<TLAvatarInfo.TLAvatar, AssetGameObject> Part = new HashMap<TLAvatarInfo.TLAvatar, AssetGameObject>();
        public Action<AssetGameObject> SuccessCb;
        public Action FailCb;
        public TransformSet Setting;
        public bool ActualImmediate = true;

        public bool IsDone
        {
            get { return LoadingCount == Part.Count; }
        }

        public bool IsSuccess
        {
            get
            {
                if (!Body)
                {
                    return false;
                }

                if (!IsDone)
                {
                    return false;
                }

                if (LoadingCount > 0)
                {
                    return Part.All(m =>
                    {
                        if (m.Key == TLAvatarInfo.TLAvatar.Avatar_Body)
                        {
                            return true;
                        }

                        var partInfo = Avatarmap.Get((int) m.Key);
                        return m.Value || string.IsNullOrEmpty(partInfo.FileName);
                    });
                }
                else
                {
                    return true;
                }
            }
        }
    }

    private class AvartarPartLoading
    {
        public TLAvatarInfo PartInfo;
        public AvartarLoding State;
    }

    private readonly HashMap<int, AvartarLoding> mAvatarTasks = new HashMap<int, AvartarLoding>();

    private void LoadAvartarPart(TLAvatarInfo part, AvartarLoding state)
    {
        if (part.PartTag == TLAvatarInfo.TLAvatar.Avatar_Body)
        {
            return;
        }

        var partState = new AvartarPartLoading {PartInfo = part, State = state};
        if (string.IsNullOrEmpty(part.FileName))
        {
            OnAvartarPartLoadFinish(null, partState);
        }
        else
        {
            var partFileName = GameUtil.getUnitAssetName(part.FileName);
            LoadGameObject(partFileName, null, true, state.Body.ID, partState, OnAvartarPartLoadFinish);
        }
    }

    private void OnGameUnitLoadFinish(AvartarLoding loadingState)
    {
        if (loadingState.Body)
        {
            if (loadingState.Body.IsUnload)
            {
                CancelAvatarTask(loadingState.Body.ID);
                return;
            }
            mAvatarTasks.Remove(loadingState.Body.ID);
        }
        if (loadingState.IsSuccess)
        {
            OnGameUnitSuccess(loadingState);
        }
        else
        {
            OnGameUnitFail(loadingState);
        }
    }

    private void OnAvartarPartLoadFinish(AssetGameObject ao, object state)
    {
        var partState = (AvartarPartLoading) state;
        partState.State.Part.Add(partState.PartInfo.PartTag, ao);
        var loadingState = partState.State;
        if (loadingState.Part.Count == loadingState.LoadingCount)
        {
            OnGameUnitLoadFinish(loadingState);
        }
    }

    private void OnGameUnitSuccess(AvartarLoding state)
    {
        var hasRide = false;
        if (state.Part != null)
        {
            foreach (var entry in state.Part)
            {
                if (!entry.Value)
                {
                    continue;
                }

                var dummy = GameUtil.getDummy((int) entry.Key);
                var partNode = entry.Value;
                if (entry.Key == TLAvatarInfo.TLAvatar.Ride_Avatar01)
                {
                    hasRide = true;
                    partNode = state.Body;
                    partNode.transform.localPosition = Vector3.zero;
                    partNode.transform.localRotation = Quaternion.identity;
                    partNode.transform.localScale = Vector3.one;

                    state.Body = entry.Value;
                    state.Body.gameObject.SetActive(true);
                    state.Body.SwapID(partNode);
                    var foot = state.Part.Get(TLAvatarInfo.TLAvatar.Foot_Buff);
                    if (foot && foot.transform.parent)
                    {
                        var footNode = state.Body.FindNode(GameUtil.getDummy((int) TLAvatarInfo.TLAvatar.Foot_Buff));
                        foot.transform.SetParent(footNode.transform, false);
                    }
                }

                var bindNode = state.Body.FindNode(dummy);
                if (bindNode)
                {
                    partNode.gameObject.SetActive(true);
                    partNode.transform.SetParent(bindNode.transform, false);
                }
                else
                {
                    Debugger.LogWarning("not found attach node ： " + dummy);
                }
            }
        }

        ResetMaterial(state.Body);
        if (state.Setting != null)
        {
            SetTransform(state.Body, state.Setting);
        }
        else
        {
            state.Body.gameObject.SetActive(false);
        }

        if (hasRide && (state.Setting == null || string.IsNullOrEmpty(state.Setting.AnimatorState)))
        {
            state.Body.Play("m_idle01");
        }

        if (state.SuccessCb != null)
        {
            if (state.ActualImmediate)
            {
                UnityHelper.WaitForEndOfFrame(state.Body, state.SuccessCb);
            }
            else
            {
                state.SuccessCb.Invoke(state.Body);
            }
        }
    }

    private void OnGameUnitFail(AvartarLoding state)
    {
        if (state.Body)
        {
            state.Body.SetNewID();
            Unload(state.Body.ID);
        }

        if (state.ActualImmediate)
        {
            UnityHelper.WaitForEndOfFrame(state.FailCb);
        }
        else
        {
            state.FailCb.Invoke();
        }
    }

    private void OnGameUnitBodyLoadFinish(AssetGameObject ao, object state)
    {
        var loadState = (AvartarLoding) state;
        if (ao)
        {
            loadState.Body = ao;
            if (loadState.LoadingCount == 0)
            {
                OnGameUnitLoadFinish(loadState);
            }
            else
            {
                foreach (var entry in loadState.Avatarmap)
                {
                    LoadAvartarPart(entry.Value, loadState);
                }
            }
        }
        else
        {
            OnGameUnitLoadFinish(loadState);
        }
    }

    #endregion

    public int LoadGameUnit(Dictionary<int, string> avatarmap, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(avatarmap, null, null);
    }

    public int LoadGameUnit(Dictionary<int, string> avatarmap, TransformSet t)
    {
        return LoadGameUnit(avatarmap, t, null);
    }

    public int LoadGameUnit(Dictionary<int, string> avatarmap, TransformSet t, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(avatarmap, t, callBack, null);
    }

    public int LoadGameUnit(Dictionary<int, string> avatarmap, TransformSet t, Action<AssetGameObject> callBack, Action failCb)
    {
        var newmap = new HashMap<int, TLAvatarInfo>();
        foreach (var entry in avatarmap)
        {
            newmap.Add(entry.Key, new TLAvatarInfo {FileName = entry.Value, PartTag = (TLAvatarInfo.TLAvatar) entry.Key});
        }

        return LoadGameUnit(newmap, t, callBack, failCb);
    }


    public int LoadGameUnit(LuaTable avatarmap, TransformSet t)
    {
        return LoadGameUnit(avatarmap, t, null);
    }

    public int LoadGameUnit(LuaTable avatarmap, TransformSet t, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(avatarmap, t, callBack, null);
    }

    public int LoadGameUnit(LuaTable avatarmap, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(avatarmap, null, callBack, null);
    }

    /// <summary>
    /// avatarmap [int-string]
    /// </summary>
    /// <param name="avatarmap"></param>
    /// <param name="t"></param>
    /// <param name="callBack"></param>
    /// <param name="failCb"></param>
    /// <returns></returns>
    public int LoadGameUnit(LuaTable avatarmap, TransformSet t, Action<AssetGameObject> callBack, Action failCb)
    {
        var newmap = new HashMap<int, TLAvatarInfo>();
        foreach (var entry in avatarmap)
        {
            var subtable = entry.value as LuaTable;
            int partTag;
            string fileName;
            if (subtable != null)
            {
                partTag = Convert.ToInt32(subtable["PartTag"]);
                fileName = subtable["FileName"] as string;
            }
            else
            {
                partTag = Convert.ToInt32(entry.key);
                fileName = entry.value.ToString();
            }

            newmap.Add(partTag, new TLAvatarInfo {FileName = fileName, PartTag = (TLAvatarInfo.TLAvatar) partTag});
        }

        avatarmap.Dispose();
        return LoadGameUnit(newmap, t, callBack, failCb);
    }


    public int LoadGameUnit(string fileName, TransformSet t)
    {
        return LoadGameUnit(fileName, t, null);
    }

    public int LoadGameUnit(string fileName, TransformSet t, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(fileName, t, callBack, null);
    }

    public int LoadGameUnit(string fileName, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(fileName, null, callBack, null);
    }


    public int LoadGameUnit(string fileName, TransformSet t, Action<AssetGameObject> callBack, Action failCb)
    {
        var loading = new AvartarLoding
        {
            SuccessCb = callBack,
            FailCb = failCb,
            Setting = t
        };
        var id = LoadGameObject(fileName, null, true, 0, loading, OnGameUnitBodyLoadFinish);
        if (!loading.IsDone)
        {
            mAvatarTasks.Add(id, loading);
        }

        loading.ActualImmediate = false;
        return id;
    }

    public int LoadGameUnit(HashMap<int, TLAvatarInfo> avatarmap, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(avatarmap, null, callBack, null);
    }

    public int LoadGameUnit(HashMap<int, TLAvatarInfo> avatarmap, TransformSet t)
    {
        return LoadGameUnit(avatarmap, t, null, null);
    }

    public int LoadGameUnit(HashMap<int, TLAvatarInfo> avatarmap, TransformSet t, Action<AssetGameObject> callBack)
    {
        return LoadGameUnit(avatarmap, t, callBack, null);
    }

    public int LoadGameUnit(HashMap<int, TLAvatarInfo> avatarmap, TransformSet t, Action<AssetGameObject> callBack, Action failCb)
    {
        var loading = new AvartarLoding
        {
            SuccessCb = callBack,
            FailCb = failCb,
            Avatarmap = avatarmap,
            Setting = t
        };

        var body = avatarmap.Get((int) TLAvatarInfo.TLAvatar.Avatar_Body);
        if (body == null)
        {
            Debug.LogError("no body avatar, what the funck");
            OnGameUnitLoadFinish(loading);
            return 0;
        }

        loading.LoadingCount = avatarmap.Count - 1;
        var bodyFileName = GameUtil.getUnitAssetName(body.FileName);
        var id = LoadGameObject(bodyFileName, null, true, loading, OnGameUnitBodyLoadFinish);
        if (!loading.IsDone)
        {
            mAvatarTasks.Add(id, loading);
        }

        loading.ActualImmediate = false;
        return id;
    }

    public int LoadPart(AssetGameObject parent, string partFileName, string bindPartName)
    {
        return LoadPart(parent, partFileName, bindPartName, null, null);
    }

    public int LoadPart(AssetGameObject parent, string partFileName, string bindPartName, Action<AssetGameObject> callBack)
    {
        return LoadPartAndReplace(parent, partFileName, bindPartName, callBack, null);
    }

    public int LoadPart(AssetGameObject parent, string partFileName, string bindPartName, Action<AssetGameObject> callBack, Action failCb)
    {
        return LoadPart(parent, false, partFileName, bindPartName, callBack, failCb);
    }

    private int LoadPart(AssetGameObject parent, bool allowImmediate, string partFileName, string bindPartName, Action<AssetGameObject> callBack, Action failCb)
    {
        var bindNode = parent.FindNode(bindPartName);
        if (!bindNode)
        {
            Debug.LogError("not found attach node ：" + bindPartName);
            if (failCb != null)
            {
                failCb.Invoke();
            }
        }

        var rdr = parent.GetComponentInChildren<Renderer>();
        var t = new TransformSet
        {
            Parent = bindNode.transform,
            Layer = parent.gameObject.layer,
            LayerOrder = rdr ? rdr.sortingOrder : TransformSet.InvalidLayerOrder
        };
        return LoadGameObject(partFileName, t, allowImmediate, parent.ID, callBack, failCb);
    }

    public int LoadPartAndReplace(AssetGameObject parent, string partFileName, string bindPartName)
    {
        return LoadPartAndReplace(parent, partFileName, bindPartName, null, null);
    }

    public int LoadPartAndReplace(AssetGameObject parent, string partFileName, string bindPartName, Action<AssetGameObject> callBack)
    {
        return LoadPartAndReplace(parent, partFileName, bindPartName, callBack, null);
    }

    public int LoadPartAndReplace(AssetGameObject parent, string partFileName, string bindPartName, Action<AssetGameObject> callBack, Action failCb)
    {
        return LoadPartAndReplace(parent, false, partFileName, bindPartName, callBack, null);
    }

    private int LoadPartAndReplace(AssetGameObject parent, bool allowImmediate, string partFileName, string bindPartName, Action<AssetGameObject> callBack, Action failCb)
    {
        var bindNode = parent.FindNode(bindPartName);
        if (!bindNode)
        {
            Debug.LogError("not found attach node ：" + bindPartName);
            if (failCb != null)
            {
                failCb.Invoke();
            }

            return 0;
        }

        var child = bindNode.transform.Find(partFileName);
        var lastAoe = child ? child.GetComponent<AssetGameObject>() : null;
        var rdr = parent.GetComponentInChildren<Renderer>();
        var t = new TransformSet
        {
            Parent = bindNode.transform,
            Layer = parent.gameObject.layer,
            LayerOrder = rdr ? rdr.sortingOrder : TransformSet.InvalidLayerOrder
        };
        return LoadGameObject(partFileName, t, allowImmediate, parent.ID, aoe =>
        {
            if (callBack != null)
            {
                callBack.Invoke(aoe);
            }

            if (lastAoe != null)
            {
                lastAoe.Unload();
            }
        }, failCb);
    }
}