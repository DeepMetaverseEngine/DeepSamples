using UnityEngine;
using System.Collections.Generic;
using System;
using DeepCore;
using DeepCore.Unity3D;
using TLBattle.Common.Plugins;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.Battle;

public class UI3DModelAdapter
{

    private static Dictionary<string, UIModelInfo> mModels = new Dictionary<string, UIModelInfo>();

    private static int mModelSerial = 0;

    public static UIModelInfo AddSingleModel(DisplayNode parent, Vector2 pos, float scale, int layerOrder, string fileName)
    {
        GameObject UI3DModelObj = new GameObject(fileName);
        AnimPlayer animPlayer = new AnimPlayer();
        DisplayCell dc = BattleFactory.Instance.CreateDisplayCell(UI3DModelObj);
        UIModelInfo info = CreateUIModelInfo(fileName, dc, animPlayer);
        animPlayer.AddAnimator(dc);
        //加载模型
        dc.LoadModel(fileName, System.IO.Path.GetFileNameWithoutExtension(fileName), (loader) =>
        {
            if (loader)
            {
                loader.DontMoveToCache = true;
                if (IsModelExist(info.Key))
                {
                    Transform trans = UI3DModelObj.transform;
                    trans.SetParent(parent.UnityObject.transform);
                    trans.localPosition = new Vector3(pos.x, -pos.y, -200);
                    trans.localRotation = Quaternion.Euler(0, 180, 0);
                    trans.localScale = Vector3.one * scale;
                    //animPlayer.AddAnimator(loader.gameObject.GetComponent<Animator>());
                    animPlayer.Play("n_idle");
                    SetModelLayer(info.Key, parent, UI3DModelObj, layerOrder);
                    info.InitDynamicBone();
                    if (info.Callback != null)
                    {
                        info.Callback(info);
                    }
                }
                else
                {
                    loader.Unload();
                }
            }
        });

        return info;
    }

    public static UIModelInfo AddAvatar(DisplayNode parent, Vector2 pos, float scale, int layerOrder, SLua.LuaTable avatar, int filter)
    {
        HashMap<int, TLAvatarInfo> aMap = new HashMap<int, TLAvatarInfo>();
        foreach (SLua.LuaTable.TablePair p in avatar)
        {
            SLua.LuaTable t = (SLua.LuaTable)p.value;
            int tag = Convert.ToInt32(t["PartTag"]);
            string filename = t["FileName"] as string;
            TLAvatarInfo aInfo = new TLAvatarInfo();
            aInfo.PartTag = (TLAvatarInfo.TLAvatar)tag;
            aInfo.FileName = filename;
            aMap.Add(tag, aInfo);
        }

        return AddAvatar(parent, pos, scale, layerOrder, aMap, filter);
    }

    public static UIModelInfo AddAvatar(DisplayNode parent, Vector2 pos, float scale, int layerOrder, HashMap<int, TLAvatarInfo> avatar, int filter)
    {
        //加载模型
        UIModelInfo info = null;
 
        info = LoadAvatar(avatar, filter, (_info) =>
        {
            if (_info != null)
            {
                if (UI3DModelAdapter.IsModelExist(_info.Key))
                {
                    Transform trans = _info.RootTrans;
                    trans.SetParent(parent.UnityObject.transform);
                    trans.localPosition = new Vector3(pos.x, -pos.y, -200);
                    trans.localRotation = Quaternion.Euler(0, 180, 0);
                    trans.localScale = Vector3.one * scale;
                    SetModelLayer(info.Key, parent, _info.RootTrans.gameObject, layerOrder);
                    _info.InitDynamicBone();
                    if (_info.Callback != null)
                    {
                        _info.Callback(_info);
                    }
                }
                
            }
        });

        return info;
    }

    public static void ReleaseModel(string key)
    {
        UIModelInfo info;
        if (mModels.TryGetValue(key, out info))
        {
            info.DC.ObjectRoot.transform.SetParent(null);
            info.DC.Dispose();
            if (info.RootTrans.gameObject.transform.childCount > 0)
            {
                Debug.LogError("[what the fuck]" + info.Key + " [child count]" + info.RootTrans.gameObject.transform.childCount);
            }
            DeepCore.Unity3D.UnityHelper.Destroy(info.RootTrans.gameObject);
            info.Callback = null;
            mModels.Remove(key);
        }
    }

    public static bool IsModelExist(string key)
    {
        return mModels.ContainsKey(key);
    }

    private static void SetModelLayer(string key, DisplayNode parent, GameObject obj, int layerOrder)
    {
        DisplayNode node = parent;
        while (node != null)
        {
            node = node.Parent;
            if (node is MenuBase)
                break;
        }
        if (node != null)
        {
            MenuBase menu = node as MenuBase;
            menu.Set3DModelLayer(key, obj); //这里用menu默认的order， 否则时间差会导致层级对不上
        }
        else
        {
            UILayerMgr.SetLayerOrder(obj, layerOrder, false, (int)PublicConst.LayerSetting.UI);
        }
    }

    public static void SetLoadCallback(string key, Action<UIModelInfo> callback)
    {
        UIModelInfo info;
        if (mModels.TryGetValue(key, out info))
        {
            info.Callback = callback;
        }
    }

    private static UIModelInfo CreateUIModelInfo(string name, DisplayCell dc, AnimPlayer anime)
    {
        string key = name + "_" + mModelSerial++;
        dc.ObjectRoot.name = key;
        UIModelInfo info = new UIModelInfo(key, dc, anime);
        mModels.Add(key, info);
        return info;
    }

    public static UIModelInfo LoadAvatar(HashMap<int, TLAvatarInfo> avatarMap, int filter, Action<UIModelInfo> callback)
    {
        GameObject ObjectRoot = new GameObject();
        var animPlayer = new AnimPlayer();
        var renderUnit = new RenderUnit(ObjectRoot);
        UIModelInfo info = CreateUIModelInfo("Avatar", renderUnit, animPlayer);
        animPlayer.AddAnimator(renderUnit);

        string bodyFile = string.Empty;
        TLAvatarInfo bodyInfo = GameUtil.GetTLAvatarInfo(avatarMap, (int)TLAvatarInfo.TLAvatar.Avatar_Body);
        if (bodyInfo != null)
        {
            bodyFile = bodyInfo.FileName;
        }

        if (!string.IsNullOrEmpty(bodyFile))
        {
            string assetBundleName = GameUtil.getUnitAssetName(bodyFile);
            string asset = System.IO.Path.GetFileNameWithoutExtension(assetBundleName);
            renderUnit.LoadModel(assetBundleName, asset, (aoe) =>
            {
                if (aoe)
                {
                    aoe.DontMoveToCache = true;
                    if (IsModelExist(info.Key)) 
                    {
                        aoe.gameObject.SetActive(false);
                        //animPlayer.AddAnimator(aoe.gameObject.GetComponent<Animator>());
                        animPlayer.Play("n_idle");

                        int count = 0;
                        bool allSuc = true;
                        foreach (var item in avatarMap)
                        {
                            if (item.Value.PartTag == TLAvatarInfo.TLAvatar.Avatar_Body || (1 << (int)item.Value.PartTag & filter) != 0)   //过滤掉不要的部位.
                            {
                                count++;
                                if (count == avatarMap.Count)
                                {
                                    if (!allSuc)
                                    {
                                        ReleaseModel(info.Key);
                                        callback(null);
                                    }
                                    else
                                    {
                                        aoe.gameObject.SetActive(true);
                                        callback(info);
                                    }
                                }
                                continue;
                            }

                            AddAvatarPart(info.Key, renderUnit, animPlayer, item.Value.FileName, item.Key, (succ1) =>
                            {
                                count++;
                                if (!succ1)
                                    allSuc = false;
                                if (count == avatarMap.Count)
                                {
                                    if (!allSuc)
                                    {
                                        ReleaseModel(info.Key);
                                        callback(null);
                                    }
                                    else
                                    {
                                        aoe.gameObject.SetActive(true);
                                        callback(info);
                                    }
                                }
                            });
                        }
                    }
                    else
                    {
                        aoe.Unload();
                    }
                }
            });
        }
        
        return info;
    }

    private static void AddAvatarPart(string key, RenderUnit parentRoot, AnimPlayer animPlayer, string assetName, int PartTag, Action<bool> callback = null)
    {
        if (string.IsNullOrEmpty(assetName))
        {
            callback(true);
            return;
        }
        string assetBundleName = GameUtil.getUnitAssetName(assetName);
        string dummy = GameUtil.getDummy(PartTag);
        string asset = System.IO.Path.GetFileNameWithoutExtension(assetBundleName);
        var displayCell = parentRoot.AttachPart(dummy, dummy, animPlayer) as RenderUnit;
        if (displayCell == null)
        {
            callback(false);
            return;
        }

        animPlayer.AddAnimator(displayCell);
        displayCell.LoadModel(assetBundleName, asset, (aoe) =>
        {
            if (aoe)
            {
                aoe.DontMoveToCache = true;
                if (IsModelExist(key))
                {
                    //animPlayer.AddAnimator(aoe.gameObject.GetComponent<Animator>());

                    //var modelInfo = aoe.gameObject.GetComponent<ModelInfo>();
                    //if (modelInfo != null)
                    //{
                    //    var newDummy = modelInfo.parentDummy;
                    //    if (!string.Equals(newDummy, dummy))
                    //    {
                    //        parentRoot.AttachPart(newDummy, newDummy, displayCell);
                    //    }
                    //}
                }
                else
                {
                    aoe.Unload();
                }
            }
            callback(aoe && IsModelExist(key));
        });
    }

    public class UIModelInfo
    {
        public string Key { get; private set; }
        public DisplayCell DC { get; private set; }
        public Transform RootTrans { get; private set; }
        public AnimPlayer Anime { get; private set; }
        public Action<UIModelInfo> Callback { get; set; }
        private DynamicBone[] mDynamicBonelist;
        public bool DynamicBoneEnable
        {
            get
            {
                if (mDynamicBonelist != null && mDynamicBonelist.Length > 0)
                {
                    return mDynamicBonelist[0].enabled;
                }
                return false;
            }
            set
            {

                if (mDynamicBonelist != null && mDynamicBonelist.Length > 0)
                {
                    foreach (var mDynamicBone in mDynamicBonelist)
                    {
                        mDynamicBone.enabled = value;
                    }
                }

            }
        }
        public void InitDynamicBone()
        {
            if (RootTrans != null)
            {
                this.mDynamicBonelist = RootTrans.gameObject.GetComponentsInChildren<DynamicBone>(true);
                //DynamicBoneEnable = false;
            }
        }
        public UIModelInfo(string key, DisplayCell dc, AnimPlayer anime)
        {
            Key = key;
            DC = dc;
            RootTrans = dc.ObjectRoot.transform.parent;
            Anime = anime;
        }

    }

}
