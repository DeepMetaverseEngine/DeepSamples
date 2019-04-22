using Assets.Scripts;
using DeepCore.GameData.Zone;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using System;
using System.IO;
using DeepCore.Unity3D;
using TLBattle.Common.Data;
using TLProtocol.Data;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 场景内掉落物品.
/// </summary>
public class TLDropItemUnit : BattleObject
{
    private BattleInfoBar mInfoBar;
    //private bool mSyncPos = false;

    //private static string[] effgroup = {
    //                                    "/res/effect/item/ef_item_green.assetbundles",
    //                                     "/res/effect/item/ef_item_blue.assetbundles",
    //                                    "/res/effect/item/ef_item_purple.assetbundles",
    //                                    "/res/effect/item/ef_item_gold.assetbundles",
    //                                    "/res/effect/item/ef_item_purple_light.assetbundles",
    //                                    "/res/effect/item/ef_item_gold_light.assetbundles",

    //                                    };
  
    private static string effectName = "/res/effect/item/{0}.assetbundles";

    //private static string strPickItemEffect = "/res/effect/ui/ef_ui_tailing.assetbundles";

    private static string strPickItemEffect = "/res/effect/ef_tailing.assetbundles";

    private static string[] pickItemEffects = 
    {
        "/res/effect/ef_tailing_green.assetbundles",
        "/res/effect/ef_tailing_blue.assetbundles",
        "/res/effect/ef_tailing_purple.assetbundles",
        "/res/effect/ef_tailing_gold.assetbundles",
        "/res/effect/ef_tailing_red.assetbundles",

    }; 

    private string mDropMessage;

    private long mRealQty;
    private TLDropItemData ItemData;

    public TLDropItemUnit(BattleScene battleScene, TLDropItemData ItemData,long realQty = 0) :
        base(battleScene, string.Format("DropItem_{0}", ItemData.Name))
    {
        this.ItemData = ItemData;
        this.mRealQty = realQty;
        
    }

    public float posX;
    public float posY;

    public float h;
    public float x;
    public float y;
    public float z;

    

    private FuckAssetObject DropEff = null;

    private FuckAssetObject PickEff = null;

    public void Load()
    {
        //string FileName = "/res/unit/item_shell_bomb.assetbundles"; //ItemData.FileName
        //string FileName = "/res/effect/item/" + ItemData.FileName + ".assetbundles";
        string FileName = string.Format(effectName,ItemData.FileName);
        //加载模型
        if (!string.IsNullOrEmpty(FileName))
        {
            DisplayCell.LoadModel(FileName, Path.GetFileNameWithoutExtension(FileName), (loader) =>
            {
                if (loader)
                {
                    if (this.IsDisposed)
                    {
                        DisplayCell.Unload();
                        return;
                    }
                }
  
                OnLoadModelFinish(loader);
            });
        }
    }

    protected void OnLoadModelFinish(FuckAssetObject aoe)
    {
        if (aoe)
        {
            CorrectDummyNode();
        }

        this.ObjectRoot.ZonePos2NavPos(h, posX, posY, z);
        //var data = ZItem.SyncInfo.ExtData as TLBattle.Common.Data.TLDropItem;

        InitEffect();


    }

 
    private void InitInfoBar()
    {
        if (ItemData != null && ItemData.itemType != 0)
        {
            DummyNode node = this.GetDummyNode("Head_Name");
            this.mInfoBar = BattleInfoBarManager.AddInfoBar(node.transform, Vector3.zero, false, false);
            string name = string.Format("<size={0}>{1}</size>", BattleInfoBar.FontSizeDefault - 2, HZLanguageManager.Instance.GetString(ItemData.Name));
            this.mInfoBar.SetName(name, GameUtil.GetQualityColorRGBA(ItemData.Quality));
        }
    }

    private void InitInfoBarPlayDropAnimation()
    {
        InitInfoBar();

        PlayDropAnimation();
    }

    private void InitEffect()
    { 
        string configName = string.Empty;
        if (ItemData != null && ItemData.QualityEffect > 0)
        {
            configName = GameUtil.GetStringGameConfig("effect_quality" + ItemData.QualityEffect);
        }
 
        if(string.IsNullOrEmpty(configName))
        {
            InitInfoBarPlayDropAnimation();
        }
        else
        {
            string eff = string.Format(effectName, configName);
            FuckAssetObject.GetOrLoad(eff, Path.GetFileNameWithoutExtension(eff), (loader) =>
            {
                if (loader)
                {
                    if (IsDisposed)
                    {
                        loader.Unload();
                        return;
                    }
                    loader.ResetTrailRenderer();
                    DropEff = loader;
                    DropEff.gameObject.Parent(this.DisplayRoot);
                    DropEff.gameObject.Position(EffectRoot.Position());
                    DropEff.gameObject.Rotation(EffectRoot.Rotation());

                    InitInfoBarPlayDropAnimation();
                }
            });
        }

      
    }

    private void PlayDropAnimation( )
    {
        SoundManager.Instance.PlaySoundByKey("drop");
        //目标点.
        var tracer = this.ObjectRoot.AddComponent<Tracer>();

        if (tracer != null)
        {
            var targetPos = Extensions.ZonePos2NavPos(h, x, y, z);
            tracer.Drop(targetPos, 0.35f, DropFinishCallBack);
        }
        else
        {
            //this.CreatePickItemEffect();
            this.Dispose();
        }
    }

    
    private string GetPickEffectName()
    {
        int quality = ItemData.Quality;
        string effectName;
        if (quality >= 1 && quality <= 5)
        {
            effectName = pickItemEffects[quality - 1];
        }
        else
        {
            effectName = strPickItemEffect;
        }
        return effectName;
    }

    private void DropFinishCallBack()
    {
        float sec = 2;
       
        GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(sec, () =>
        {
            this.DisplayCell.activeSelf = false;
            this.CreatePickItemEffect();
        }));
    }


    // pick
    private void CreatePickItemEffect()
    {
        var eff = this.GetPickEffectName();
        FuckAssetObject.GetOrLoad(eff, Path.GetFileNameWithoutExtension(eff), (loader) =>
        {
            if (loader)
            {
                if (IsDisposed)
                {
                    loader.Unload();
                }
                else
                {
                    loader.ResetTrailRenderer();

                    var pos = this.Position;
                    loader.gameObject.Position(pos);

                    this.TryDestroyDropEff();

                    this.PickEff = loader;
                    var c = loader.gameObject.AddComponent<FlyToActor>();

                    c.Fly(PickFinishCallBack);
                }
            }
        });
    }

 
    private void TryDestroyDropEff()
    {
        if (this.DropEff == null)
            return;
        this.DropEff.transform.SetParent(null);
        var script = DropEff.gameObject.GetComponent<EffectAutoDestroy>();
        if (script)
        {
            script.DoDestroy();
        }
        else
        {
            DropEff.Unload();
           
        }
        DropEff = null;
    }

    private void TryDestroyPickEff()
    {
        if (this.PickEff == null)
            return;
        this.PickEff.transform.SetParent(null);
        var script = this.PickEff.gameObject.GetComponent<EffectAutoDestroy>();
        if (script)
        {
            script.DoDestroy();
        }
        else
        {
            PickEff.Unload();
        }
        this.PickEff = null;
    }
     
    private void PickFinishCallBack()
    {
        //string title = "<a>获得<f color='{0}'>{1}</f>x{2}</a>";
        var title = HZLanguageManager.Instance.GetString("common_get");
        var itemName = HZLanguageManager.Instance.GetString(ItemData.Name);
        uint argb = GameUtil.GetQualityColorARGB(ItemData.Quality);
        var message = string.Format(title, argb.ToString("x16"), itemName, mRealQty); 
        GameAlertManager.Instance.ShowFloatingTips(message);

        //弹出自动装备弹窗
        EventManager.Fire("ShowUI");
 
        this.Dispose();
    }


    protected override void OnDispose()
    {
        this.TryDestroyDropEff();
        this.TryDestroyPickEff();
        base.OnDispose();
    }


}