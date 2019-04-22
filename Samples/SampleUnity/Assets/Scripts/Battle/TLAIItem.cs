
using Assets.Scripts;
using DeepCore.GameSlave;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using System;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;
using UnityEngine;

/// <summary>
/// 场景内掉落物品.
/// </summary>
public class TLAIItem : ComAIItem
{
    private BattleInfoBar mInfoBar;
    private bool mSyncPos = false;

    private static string[] effgroup = {"/res/effect/item/ef_item_blue.assetbundles",
                                        "/res/effect/item/ef_item_gold.assetbundles",
                                        "/res/effect/item/ef_item_gold_light.assetbundles",
                                        "/res/effect/item/ef_item_green.assetbundles",
                                        "/res/effect/item/ef_item_purple.assetbundles",
                                        "/res/effect/item/ef_item_purple_light.assetbundles"};

    private FuckAssetObject effectObj = null;
    public TLAIItem(BattleScene battleScene, ZoneItem obj) : base(battleScene, obj)
    {

    }

    public bool IsShowGetEff()
    {
        TLItemProperties zp = ZItem.Info.Properties as TLItemProperties;
        return zp.ShowGotEffect;

    }

    public TLItemProperties GetItemProperties()
    {
        TLItemProperties zp = ZItem.Info.Properties as TLItemProperties;
        return zp;
    }

    protected override void OnLoadModelFinish(FuckAssetObject aoe)
    {
        base.OnLoadModelFinish(aoe);
        var data = ZItem.SyncInfo.ExtData as TLBattle.Common.Data.TLDropItem;

        if (data != null)//掉落类道具.
        {
            InitInfoBar(data);
            InitEffect(data);
            PlayDropAnimation(data);
        }
        else//非掉落类道具.
        {
            InitInfoBar();
            mSyncPos = true;
        }
    }

    protected override void OnDispose()
    {
        base.OnDispose();

        if (mInfoBar != null)
        {
            mInfoBar.Remove();
            mInfoBar = null;
        }

        if (effectObj != null)
        {
            effectObj.Unload();
        }
    }

    protected override void SyncState()
    {
        if (mSyncPos)
            base.SyncState();
    }

    private string GetTranslatedItemName(int templateId)
    {
        var map = GameUtil.GetDBData("itemtemplate", templateId);
        if (map == null)
        {
            return templateId + " not found";
        }
        var key = Convert.ToString(map["item_name"]);
        return HZLanguageManager.Instance.GetString(key);
    }

        private void InitInfoBar(TLDropItem data)
    {
        DummyNode node = this.GetDummyNode("Head_Name");
        this.mInfoBar = BattleInfoBarManager.AddInfoBar(node.transform, Vector3.zero, false, false);
        string name = string.Format("<size={0}>{1}</size>", BattleInfoBar.FontSizeDefault - 2, data.Name);
        this.mInfoBar.SetName(name, GameUtil.GetQualityColorRGBA(data.Quality));
    }

    private void InitInfoBar()
    {
        //名字挂载点临时加偏移，后续改成直接取挂载点.
        DummyNode node = this.GetDummyNode("Head_Name");
        this.mInfoBar = BattleInfoBarManager.AddInfoBar(node.transform, Vector3.zero, false, false);
        var DisplayName = GetTranslatedItemName(ZItem.TemplateID);
        if (string.IsNullOrEmpty(DisplayName)){
            DisplayName = ZItem.DisplayName;
        } 
        string name = string.Format("<size={0}>{1}</size>", BattleInfoBar.FontSizeDefault - 2, DisplayName);
        this.mInfoBar.SetName(name, GameUtil.GetQualityColorRGBA(0));
    }

    private void PlayDropAnimation(TLDropItem data)
    {
        //设置动画初始点.
        if (ZObj.Parent.TerrainSrc != null)
        {
            this.ObjectRoot.ZonePos2NavPos(ZObj.Parent.TerrainSrc.TotalHeight
                , data.OriginX, ZObj.Y, data.OriginY);
        }
        this.ObjectRoot.ZoneRot2UnityRot(ZObj.Direction);

        //目标点.
        var tracer = this.ObjectRoot.AddComponent<Tracer>();

        if (tracer != null)
        {
            var targetPos = Extensions.ZonePos2NavPos(ZObj.Parent.TerrainSrc.TotalHeight
                        , ZObj.X, ZObj.Y, ZObj.Z);
            tracer.Drop(targetPos, 0.35f, DropFinishCallBack);
        }
    }

    private void DropFinishCallBack()
    {
        mSyncPos = true;
    }

    private void InitEffect(TLDropItem data)
    {
        string eff = null;

        if (data.EffectQuality >= 0 && data.EffectQuality < effgroup.Length)
        {
            eff = effgroup[data.EffectQuality];
            FuckAssetObject.GetOrLoad(eff, System.IO.Path.GetFileNameWithoutExtension(eff), (loader) =>
            {
                if (loader)
                {
                    if (IsDisposed)
                    {
                        loader.Unload();
                        return;
                    }

                    effectObj = loader;
                    effectObj.gameObject.Parent(this.DisplayRoot);
                    effectObj.gameObject.Position(EffectRoot.Position());
                    effectObj.gameObject.Rotation(EffectRoot.Rotation());
                }
            });
        }
    }
}
