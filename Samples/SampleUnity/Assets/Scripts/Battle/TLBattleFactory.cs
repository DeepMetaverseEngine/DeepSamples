using UnityEngine;
using SLua;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using DeepCore.GameSlave.Client;
using DeepCore.GameSlave;
using DeepCore.GameData.Zone;
using System;
using System.Collections.Generic;
using DG.Tweening;

//class TLTerrainAdapter : DefaultTerrainAdapter
//{
    //protected override void OnLoadFinish(FuckAssetLoader loader)
    //{
    //    base.OnLoadFinish(loader);

    //    //重新定义nav层
    //    var nav = MapRoot.transform.FindRecursive("nav");
    //    if (nav != null)
    //    {
    //        nav.gameObject.layer = BattleFactory.Instance.StageNavLay;

    //        foreach (Transform e in nav.GetComponentsInChildren<Transform>())
    //        {
    //            MeshCollider bc = e.gameObject.GetComponent<MeshCollider>();
    //            if (bc == null)
    //            {
    //                bc = e.gameObject.AddComponent<MeshCollider>();
    //            }
    //            bc.gameObject.layer = nav.gameObject.layer;
    //        }
    //    }
    //}

    //protected override bool InitLightMapParam(GameObject mMapRoot)
    //{
    //    LightmapParam[] lmd = mMapRoot.GetComponentsInChildren<LightmapParam>(true);
    //    for (int i = 0 ; i < lmd.Length ; i++)
    //    {
    //        Renderer r = lmd[i].gameObject.GetComponent<Renderer>();
    //        if (r != null)
    //        {
    //            r.gameObject.isStatic = true;
    //            r.lightmapIndex = lmd[i].lightmapIndex;
    //            r.lightmapScaleOffset = lmd[i].lightmapScaleOffset;
    //        }
    //        else
    //        {
    //            Terrain t = lmd[i].gameObject.GetComponent<Terrain>();
    //            if (t != null)
    //            {
    //                t.gameObject.isStatic = true;
    //                t.lightmapIndex = lmd[i].lightmapIndex;
    //                t.lightmapScaleOffset = lmd[i].lightmapScaleOffset;
    //            }
    //        }
    //    }

    //    return false;
    //}
//}

public class TLBattleFactory : DefaultBattleFactory,IObserver<SettingData>
{
    //private TerrainAdapter mTerrainAdapter;
    private static TLBattleFactory mInstance;
    private float mBlurStrength = 0;
    private float mCameraStretch = 0;
    private HashSet<uint> mDisplayPool = new HashSet<uint>();
    public int blSwitch = 1;
    public int GameQuality = 1;
    private Sequence mCurBlurEffectSeq;
    private Sequence mCurDistanceSeq;
    private Tween mShakeTween;
    public static new TLBattleFactory Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new TLBattleFactory();
            }
            return mInstance;
        }
    }
    //public TLSoundAdapter SoundManager {
    //    get
    //    {
    //        return SoundAdapter as TLSoundAdapter;
    //    }

    //}


    public override int StageNavLay
    {
        get
        {
            return (int)PublicConst.LayerSetting.STAGE_NAV;
        }
    }

    public override DisplayCell CreateDisplayCell(GameObject root, string name = "DisplayCell")
    {
        return new RenderUnit(root, name);
        //return base.CreateDisplayCell(root, name);        
    }


    [DoNotToLua]
    public TLBattleFactory() : base()
    {
        BattleObject.OnPlayEffectHandle = OnPlayEffectHandle;
        mBlurStrength = 0;
        mCameraStretch = 0;
        //DataMgr.Instance.SettingData.AttachObserver(this);
        blSwitch = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.ISFOGGY);
        GameQuality = (int)DataMgr.Instance.SettingData.GetQuailty();
    }

    [DoNotToLua]
     ~TLBattleFactory()
    {
        //DataMgr.Instance.SettingData.DetachObserver(this);
    }

    public void RegisterListener()
    {
        DataMgr.Instance.SettingData.AttachObserver(this);
    }
    public void ClearListener()
    {
        DataMgr.Instance.SettingData.DetachObserver(this);
    }
    ////模糊效果

    private void BlurUpdate(System.Object obj)
    {
        float value = (float)obj;
        var _Camera = Camera as SceneCamera;
        if (_Camera != null && _Camera.BlurEffect != null)
        {
            _Camera.BlurEffect.strength = value;
            mBlurStrength = value;
        }
    }

    private void BlurComplete()
    {
        var _Camera = Camera as SceneCamera;
        if (_Camera != null && _Camera.BlurEffect != null)
        {
            mBlurStrength = 0;
            _Camera.BlurEffect.enabled = false;

        }
    }

    //镜头拉伸
    private void CameraUpdate(System.Object obj)
    {
        float value = (float)obj;
        var _Camera = Camera as SceneCamera;
        if (_Camera != null && _Camera.mainCamera != null)
        {
            var pos = _Camera.mainCamera.transform.localPosition;
            mCameraStretch = value;
            _Camera.mainCamera.transform.localPosition = new Vector3(pos.x, pos.y, value);
        }
    }

    private void CameraComplete()
    {
        var _Camera = Camera as SceneCamera;
        if (_Camera != null && _Camera.mainCamera != null)
        {
            var pos = _Camera.mainCamera.transform.localPosition;
            mCameraStretch = 0;
            _Camera.mainCamera.transform.localPosition = new Vector3(pos.x, pos.y, 0);
        }
    }
    //private void ResetCameraBlurEffect()
    //{
    //    mBlurStrength = 0;
    //}

    //private void ResetCameraStretch()
    //{
    //    mCameraStretch = 0;
    //}
    private int OnPlayEffectHandle(BattleObject _object, LaunchEffect eff, Vector3 pos, Quaternion rot)
    {
        var effID = 0;
        bool show = true;

        if (_object is TLAIPlayer)
        {
            if (GameQuality == SettingData.QUALITY_LOW)
            {
                show = false;
            }
            else if (GameQuality == SettingData.QUALITY_MED)
            {
                if (!(_object is TLAIActor))
                {
                    show = false;
                }
            }
        }

        if (!show)
        {
            if (_object != null)
            {
                _object.OnLoadNotShow(eff);
            }
            return 0;
        }
        //声音
        if (!string.IsNullOrEmpty(eff.SoundName) && !CutSceneManager.Instance.IsPlaying)
        {
            bool isPlayer = _object.SoundImportant();
            
            if (!isPlayer) //其他玩家音效处理
            {
                var vol = SoundManager.Instance.DefaultSoundVolume * (GameUtil.GetIntGameConfig("SoundVol_PlayerAlive") / 100f);
                if (TLBattleScene.Instance.Actor == null || TLBattleScene.Instance.Actor.IsDead)
                {
                    vol = SoundManager.Instance.DefaultSoundVolume * (GameUtil.GetIntGameConfig("SoundVol_PlayerDead") / 100f);
                }
                SoundManager.Instance.PlaySound(eff.SoundName, eff.EffectTimeMS, vol, eff.IsLoop);
            }
            else
            {
                SoundManager.Instance.PlaySound(eff.SoundName, eff.EffectTimeMS, SoundManager.Instance.DefaultSoundVolume, eff.IsLoop);
            }
            //SoundManager.Instance.PlaySound(eff.SoundName, eff.EffectTimeMS, pos, eff.IsLoop);
        }

        //震屏.
        if (!string.IsNullOrEmpty(eff.Tag))
        {
            bool flag = false;
            if (eff.Tag == "all") { flag = true; }//全同步.
            else if (eff.Tag == "actor" && _object.IsImportant()) { flag = true; }//只有主角自己会播.
            //var blSwitch= DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.ISFOGGY);
            if (blSwitch==1)
            {
                if (eff.BlurBeginTime > 0 && flag)
                {
                    //测试模糊效果
                    var _Camera = Camera as SceneCamera;
                    if (_Camera != null && _Camera.BlurEffect != null)
                    {
                        _Camera.BlurEffect.enabled = true;
                        //iTween.StopByName(_Camera.BlurEffect.gameObject, "BlurEffect");
                        if (mCurBlurEffectSeq != null)
                        {
                            mCurBlurEffectSeq.Kill(true);
                        }
                        //ResetCameraBlurEffect();
                        mCurBlurEffectSeq = DOTween.Sequence()
                            .Append
                            (
                                DOTween.To((v) =>
                                {
                                    BlurUpdate(v);
                                }, mBlurStrength, eff.BlurStrength, eff.BlurBeginTime / 1000f)
                            )
                            .AppendInterval((eff.BlurBeginTime + eff.BlurWaitTime) / 1000f)
                            .Append
                            (
                                DOTween.To((v) =>
                                {
                                    BlurUpdate(v);
                                }, eff.BlurStrength, 0, eff.BlurEndTime / 1000f)
                            )
                            .AppendCallback
                           (
                              () =>
                              {
                                  BlurComplete();
                              }
                            );


                        //var tween = DOTween.To((v) =>
                        //{
                        //    BlurUpdate(v);
                        //}, mBlurStrength, eff.BlurStrength, eff.BlurBeginTime / 1000f);
                        //iTween.ValueTo(_Camera.BlurEffect.gameObject, iTween.Hash("name", "BlurEffect", "from", mBlurStrength, "to", eff.BlurStrength,
                        //                                        "time", eff.BlurBeginTime / 1000f,
                        //                                        "onupdate", (System.Action<System.Object>)BlurUpdate
                        //                                         ));

                        //iTween.ValueTo(_Camera.BlurEffect.gameObject, iTween.Hash("name", "BlurEffect", "Delay", (eff.BlurBeginTime + eff.BlurWaitTime) / 1000f,
                        //                                    "from", eff.BlurStrength, "to", 0,
                        //                                    "time", eff.BlurEndTime / 1000f,
                        //                                    "onupdate", (System.Action<System.Object>)BlurUpdate,
                        //                                    "oncomplete", (System.Action)BlurComplete
                        //                                    ));

                    }
                }   
            }

            if (eff.CameraBeginTime > 0 && flag)
            {
                //拉伸镜头
                var _Camera = Camera as SceneCamera;
                if (_Camera != null && _Camera.mainCamera != null)
                {
                    //iTween.StopByName(_Camera.mainCamera.gameObject, "CameraStretch");
                    if (mCurDistanceSeq != null)
                    {
                        mCurDistanceSeq.Kill(true);
                    }
                    //ResetCameraStretch();

                    mCurDistanceSeq = DOTween.Sequence()
                            .Append
                            (
                                DOTween.To((v) =>
                                {
                                    CameraUpdate(v);
                                }, mCameraStretch, eff.CameraDistance, eff.CameraBeginTime / 1000f)
                            )
                            .AppendInterval((eff.CameraBeginTime + eff.CameraWaitTime) / 1000f)
                            .Append
                            (
                                DOTween.To((v) =>
                                {
                                    CameraUpdate(v);
                                }, eff.CameraDistance,0, eff.CameraEndTime / 1000f)
                            )
                            .AppendCallback
                           (
                              () =>
                              {
                                  CameraComplete();
                              }
                            );

                    //iTween.ValueTo(_Camera.mainCamera.gameObject, iTween.Hash("name", "CameraStretch", "from", mCameraStretch, "to", eff.CameraDistance,
                    //                                        "time", eff.CameraBeginTime / 1000f,
                    //                                        "onupdate", (System.Action<System.Object>)CameraUpdate
                    //                                         ));

                    //iTween.ValueTo(_Camera.mainCamera.gameObject, iTween.Hash("name", "CameraStretch", "Delay", (eff.CameraBeginTime + eff.CameraWaitTime) / 1000f,
                    //                                    "from", eff.CameraDistance, "to", 0,
                    //                                    "time", eff.CameraEndTime / 1000f,
                    //                                    "onupdate", (System.Action<System.Object>)CameraUpdate,
                    //                                    "oncomplete", (System.Action)CameraComplete
                    //                                    ));

                }
            }


            if (eff.EarthQuakeMS > 0 && flag)
            {
                var _Camera = Camera as SceneCamera;
                if (_Camera != null && _Camera.pitchNode != null)
                {
                    //TODO震屏.
                    //iTween.ShakePosition(_Camera.pitchNode.gameObject,
                    //      new Vector3(eff.EarthQuakeXYZ,
                    //                  eff.EarthQuakeXYZ,
                    //                  eff.EarthQuakeXYZ),
                    //      (float)eff.EarthQuakeMS / 1000);


                    //测试模糊效果
                    //_Camera.mainCamera.GetComponent<BlurEffect>().enabled = true;
                    //iTween.ValueTo(_Camera.mainCamera.gameObject, iTween.Hash("from",0, "to", 4, 
                    //                                        "time", 0.2f,
                    //                                        "onupdate", (System.Action<System.Object>)BlurUpdate
                    //                                        ));
                    //iTween.ValueTo(_Camera.mainCamera.gameObject, iTween.Hash("Delay",0.2f,
                    //                                        "from", 4, "to", 0,
                    //                                       "time", 0.1f,
                    //                                       "onupdate", (System.Action<System.Object>)BlurUpdate,
                    //                                       "oncomplete", (System.Action)BlurComplete
                    //                                       ));
                    if (mShakeTween != null)
                    {
                        mShakeTween.Kill(true);
                    }
                    //ResetCameraStretch();

                    mShakeTween = _Camera.pitchNode.gameObject.transform.DOShakePosition(eff.EarthQuakeMS / 1000f, new Vector3(eff.EarthQuakeXYZ,
                                                               eff.EarthQuakeXYZ,
                                                               eff.EarthQuakeXYZ),20,90,false,false);

                    //iTween.ShakePosition(_Camera.pitchNode.gameObject, iTween.Hash("amount", new Vector3(eff.EarthQuakeXYZ,
                    //                                           eff.EarthQuakeXYZ,
                    //                                           eff.EarthQuakeXYZ), "time",
                    //                                          (float)eff.EarthQuakeMS / 1000, "islocal", true));
                }
            }

        }
        //特效
        if (!string.IsNullOrEmpty(eff.Name))
        {
                effID = FuckAssetObject.GetOrLoad(eff.Name, System.IO.Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                {
                    if (loader)
                    {
                        if (_object.IsDisposed)
                        {
                            loader.Unload();
                            return;
                        }

                        _object.OnLoadEffectSuccess(loader, eff, pos, rot);
                    }
                });
        }

        //预警
        if (eff.WarnType != LaunchEffect.WarningType.WARNING_TYPE_NONE)
        {
            string warnAsset = string.Empty;
            switch (eff.WarnType)
            {
                case LaunchEffect.WarningType.WARNING_TYPE_SQUARE:
                    warnAsset = "/res/effect/ef_skillwarningsquare.assetbundles";
                    break;
                case LaunchEffect.WarningType.WARNING_TYPE_CIRCLE:
                    warnAsset = "/res/effect/ef_skillwarningcircle.assetbundles";
                    break;
                case LaunchEffect.WarningType.WARNING_TYPE_SECTOR:
                    warnAsset = "/res/effect/ef_skillwarningfan.assetbundles";
                    break;
                case LaunchEffect.WarningType.WARNING_TYPE_NONE:
                    warnAsset = "/res/effect/ef_skillwarningfan.assetbundles";
                    break;
            }
            effID = FuckAssetObject.GetOrLoad(warnAsset, System.IO.Path.GetFileNameWithoutExtension(warnAsset), (loader) =>
            {
                if (loader)
                {
                    if (_object.IsDisposed)
                    {
                        loader.Unload();
                        return;
                    }
                    var animator = loader.gameObject.GetComponent<Animator>();
                    animator.speed = eff.WarnSpeed;
                    loader.gameObject.transform.localScale = new Vector3(eff.WarnScaleX, 1, eff.WarnScaleZ);
                    var controller = loader.gameObject.GetComponentsInChildren<CircularSectorMeshRenderer>();
                    foreach (var component in controller)
                    {
                        component.degree = eff.WarnDegree;
                    }
                    _object.OnLoadWarningEffect(loader, eff, pos, rot);
                }
            });
        }
        return effID;
    }


    //private void OnCameraShakeComplete()
    //{
    //    var _Camera = Camera as SceneCamera;
    //    if (_Camera != null && _Camera.pitchNode != null)
    //    {
    //        _Camera.pitchNode.localPosition = Vector3.zero;
    //    }
    //}
    [DoNotToLua]
    public override BattleScene CreateBattleScene(AbstractBattle battle)
    {
        return new TLBattleScene(battle);
    }

    [DoNotToLua]
    public override TerrainAdapter TerrainAdapter
    {
        get
        {
            if (mTerrainAdapater == null)
                mTerrainAdapater = new DefaultTerrainAdapter();
            return mTerrainAdapater;
        }
    }

    [DoNotToLua]
    public override ICamera Camera
    {
        get
        {
            return GameSceneMgr.Instance.SceneCameraNode as ICamera;
        }
    }

    [DoNotToLua]
    public override void MakeDamplingJoint(GameObject body, GameObject from, GameObject to)
    {
        DampingJoint dj = body.transform.GetComponentInChildren<DampingJoint>();
        if (dj != null)
        {
            dj.Hook(from.transform, to.transform);
        }
    }

    [DoNotToLua]
    public override ComAICell CreateComAICell(BattleScene battleScene, ZoneObject obj)
    {
        ComAICell cell = null;
        // Debug.Log("CreateComAICell:" + obj);
        if (obj is ZoneActor)
        {
            cell = new TLAIActor(battleScene, obj as ZoneActor);
        }
        else if (obj is ZoneUnit)
        {
            var type = (obj as ZoneUnit).Info.UType;

            switch (type)
            {
                case UnitInfo.UnitType.TYPE_NPC:
                    cell = new TLAINPC(battleScene, obj as ZoneUnit);
                    break;
                case UnitInfo.UnitType.TYPE_PLAYERMIRROR:
                    cell = new TLAIPlayerMirror(battleScene, obj as ZoneUnit);
                    break;
                case UnitInfo.UnitType.TYPE_MONSTER:
                    cell = new TLAIMonster(battleScene, obj as ZoneUnit);
                    break;
                case UnitInfo.UnitType.TYPE_PLAYER:
                    cell = new TLAIPlayer(battleScene, obj as ZoneUnit);
                    break;
                case UnitInfo.UnitType.TYPE_PET:
                    cell = new TLAIPet(battleScene, obj as ZoneUnit);
                    break;
                case UnitInfo.UnitType.TYPE_FOLLOW_UNIT:
                    cell = new TLAINPC(battleScene, obj as ZoneUnit);
                    break;
                default:
                    cell = new TLAIUnit(battleScene, obj as ZoneUnit);
                    break;
            }
        }
        else if (obj is ZoneItem)
        {
            cell = new TLAIItem(battleScene, obj as ZoneItem);
        }
        else if (obj is ZoneSpell)
        {
            var zspell = obj as ZoneSpell;
            bool show = true;
            
            TLAIPlayer launcher = null;
            if (zspell.Launcher != null)
            {
                launcher = TLBattleScene.Instance.GetBattleObject(zspell.Launcher.ObjectID) as TLAIPlayer;
               
            }
            if (GameQuality == SettingData.QUALITY_LOW)
            {
                if (launcher != null)
                {
                    show = false;
                }
            }
            else if (GameQuality == SettingData.QUALITY_MED)
            {
                if (launcher != null && !(launcher is TLAIActor))
                {
                    show = false;
                }
            }
            if (!show) { return null; }

            cell = new TLAISpell(battleScene, obj as ZoneSpell);
        }

        if (cell != null)
        {
            cell.OnCreate();
        }

        return cell;
    }

    public override BattleDecoration CreateBattleDecoration(BattleScene battleScene, ZoneEditorDecoration zf)
    {
        return new TLDecoration(battleScene, zf);
    }

    public bool AddDisplayPool(uint id)
    {
        if (mDisplayPool.Count < DataMgr.Instance.SettingData.TryGetIntAttribute(SettingData.NotifySettingState.PERSONCOUNT) + 1)
        {
            mDisplayPool.Add(id);
            return true;
        }
        return false;
    }

    public void RemoveDisplayPool(uint id)
    {
        if (mDisplayPool.Contains(id))
        {
            mDisplayPool.Remove(id);
        }
    }

    public bool Notify(long status, SettingData subject)
    {
        if ((status & (long)global::SettingData.NotifySettingState.ISFOGGY) != 0)
        {
            blSwitch = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.ISFOGGY);
        }
        if ((status & (long)global::SettingData.NotifySettingState.QUALITY) != 0)
        {
            GameQuality = (int)DataMgr.Instance.SettingData.GetQuailty();
        }
        return true;
    }

    public void BeforeDetach(SettingData subject) { }
    
    public void BeforeAttach(SettingData subject) { }

}