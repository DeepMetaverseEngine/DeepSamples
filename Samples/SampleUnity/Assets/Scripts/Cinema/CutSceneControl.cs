using Assets.ParadoxNotion.SLATE_Cinematic_Sequencer.TLExtend;
using DeepCore.GameData.Zone;
using DeepCore.Unity3D.Battle;
using Slate;
using Slate.ActionClips;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DeepCore.Unity3D;
using TLClient;
using UnityEngine;
using TLBattle.Common.Plugins;

//using UnityEngine.Experimental.Director;

namespace Assets.Scripts.Cinema
{
    public class CutSceneControl
    {
        private Cutscene m_CutScene;
        public Cutscene CurrentCutScene { get { return m_CutScene; } }
        private System.Action<CutSceneControl,bool> InitComplete;
        private List<TLPlayAudio> AudioList = new List<TLPlayAudio>();
        private int mInitNum = 0;
        //private List<TLCGUnit> CgUnitManager = new List<TLCGUnit>();
        //private List<TLCGUnit> CgEffectManager = new List<TLCGUnit>();
        //private List<AssetObjectExt> CgObjectManager = new List<AssetObjectExt>();
        private bool IsDisposed = false;
        private string[] mFileName = { "player:", "npc:", "ef:", "item:" ,"ef_item:"};
        private List<string> mFileNameList = new List<string>();
        public string FileName { get; set; }
        private DateTime mDateTime;
        private bool LoadSuccess = false;
        public bool CanSkip {private set; get; }
        private Stopwatch stopwatch;
        private bool _stopplay = false;
        public CutSceneControl(string _filename,bool canskip, Cutscene _CutScene, System.Action<CutSceneControl,bool> _InitComplete = null)
        {
            m_CutScene = _CutScene;
            CanSkip = canskip;
            InitComplete = _InitComplete;
            IsDisposed = false;
            FileName = _filename;
            RegisterCallbacks();
            //overlay text style

        }
        private void RegisterCallbacks()
        {
            //Cutscene.OnCutsceneStarted += CutsceneStarted;
            //Cutscene.OnCutsceneStopped += CutsceneFinished;
            //CurrentCutScene.OnGlobalMessageSend += OnGlobalMessageSend;
            //CurrentCutScene.OnSectionReached += OnSectionReached;
            //DirectorGUI.OnGUIUpdate += OnGUIUpdate;
            //TLPlayerAnimatorTrack.AnimatorTrackHandle = OnAnimatorTrackOverRide;
            //TLPlayerAnimatorTrack.OnInitAnimatorListHandle = OnInitAnimatorListHandle;

        }

        //private List<Animator> OnInitAnimatorListHandle(object userdata)
        //{
        //    if (userdata is TLCGUnit)
        //    {
        //        TLCGUnit player = userdata as TLCGUnit;
        //        return player.animPlayer.Animators;
        //    }
        //    return null;
        //}

        //private bool OnAnimatorTrackOverRide(GameObject actor, object userdata, AnimationClip playAnimClip, float clipTime, float clipPrevious, float weight)
        //{
        //    if (userdata is TLCGUnit)
        //    {
        //        TLCGUnit player = userdata as TLCGUnit;
        //        player.PlayAnimForPlayer(playAnimClip.name);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public void Update()
        {
            if (!LoadSuccess &&(DateTime.Now - mDateTime).Seconds >= 20)
            {
                if (InitComplete != null)
                {
                    InitComplete(this,false);
                    InitComplete = null;
                }
            }
        }
        private void UnRegisterCallbacks()
        {
            //Cutscene.OnCutsceneStarted -= CutsceneStarted;
            //Cutscene.OnCutsceneStopped -= CutsceneFinished;
            //CurrentCutScene.OnGlobalMessageSend -= OnGlobalMessageSend;
            //CurrentCutScene.OnSectionReached -= OnSectionReached;
            //DirectorGUI.OnGUIUpdate -= OnGUIUpdate;
            //TLPlayerAnimatorTrack.AnimatorTrackHandle = null;
            //TLPlayerAnimatorTrack.OnInitAnimatorListHandle = null ;

        }
        internal void InitAndPlay()
        { 
            stopwatch = Stopwatch.StartNew();
            mDateTime = DateTime.Now;
            InitActorAndEffect();
            LoadActorAndEffect();
        }

        //判断预加载总单位数量************************************
        private void InitActorAndEffect()
        {
            var actoriter = CurrentCutScene.groups;
            foreach (var actor in actoriter)
            {
                if (actor is ActorGroup)
                {
                    var ag = actor as ActorGroup;
                    if (ag != null && ag.isActive)
                    {
                        foreach (var file in mFileName)
                        {
                            if (!string.IsNullOrEmpty(ag.name))
                            {
                                if (ag.name.ToLower().IndexOf(file) != -1)
                                {
                                    mInitNum++;
                                    mFileNameList.Add(ag.name.ToLower());
                                    break;
                                }
                            }
                            else
                            {
                                Debugger.LogError("acotrgroup's name is Empty or acotrgroup is null");
                            }

                        }
                    }
                }
                else if (actor is DirectorGroup)
                {
                    DirectorGroup dg = actor as DirectorGroup;
                    if (dg.tracks.Count > 0)
                    {
                        foreach (var track in dg.tracks)
                        {
                            if (track is DirectorAudioTrack)
                            {
                                DirectorAudioTrack adt = track as DirectorAudioTrack;
                                if (adt.actions.Count > 0)
                                {
                                    foreach (var _action in adt.actions)
                                    {
                                        if (_action is TLPlayAudio)
                                        {
                                            TLPlayAudio dpa = _action as TLPlayAudio;
                                            if (!string.IsNullOrEmpty(dpa.AudioName) && dpa.AudioName.IndexOf("sound:") != -1)
                                            {
                                                AudioList.Add(_action as TLPlayAudio);
                                                mInitNum++;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        private void ErrorDebug(string info)
        {
            LoadCount();
            Debugger.LogError("loadInit error =" + info);
        }

        private IEnumerable<ActorGroup> GetActorGroup()
        {
            if (CurrentCutScene.groups == null || CurrentCutScene.groups.Count == 0)
            {
                Debugger.LogError("ActorGroup is Null");
                return null;
            }
            return CurrentCutScene.groups.OfType<ActorGroup>();
        }

        //开始加载************************************
        private void LoadActorAndEffect()
        {
            //Debugger.Log("LoadActorAndEffectmInitNum"+ mInitNum);
            if (mInitNum == 0)
            {
                if (InitComplete != null)
                {
                    InitComplete(this,true);
                }
                return;
            }
            foreach (var filename in mFileNameList)
            {
                //Debugger.Log("mFileNameList" + filename);
                LoadModel(filename);
            }

            foreach (var TLplayaudio in AudioList)
            {
                string[] namelist = TLplayaudio.AudioName.Split(':');
                if (namelist.Length > 1)
                {
                    LoadAudio(namelist[1], TLplayaudio);
                }
            }

        }

        private void LoadAudio(string fileName, TLPlayAudio playaudio)
        {

            string _fileName = "/res/sound/cg_sound/" + fileName + ".assetbundles";
            FuckAssetLoader.Load(_fileName, loader =>
            {
                if (loader.IsAudioClip)
                {
                    playaudio.audioClip = (AudioClip)loader.AssetObject;
                }
                LoadCount();
            });
        }
        private void LoadModel(string fileName)
        {
            //玩家类角色
            if (fileName.IndexOf("player:") != -1)
            {
                string[] splittext = fileName.Split(':');
                if (splittext.Length > 0)
                {
                    //玩家
                    if (splittext[1] == "0" && TLBattleScene.Instance.Actor != null)
                    {
                        var avatarMap = DataMgr.Instance.UserData.GetAvatarListClone();
                        //var list = avatarMap.Keys.ToList<int>();
                        avatarMap.RemoveByKey((int)TLAvatarInfo.TLAvatar.Foot_Buff);
                        avatarMap.RemoveByKey((int)TLAvatarInfo.TLAvatar.Ride_Avatar01);
                        //TLCGUnit cgUnit = new TLCGUnit(TLBattleScene.Instance, TLBattleScene.Instance.Actor.Info, avatarMap,
                        //    (unit) =>
                        //    {
                        //        var aniobj = unit.ObjectRoot.GetComponentInChildren<Animator>();
                        //        if (aniobj != null)
                        //        {

                        //        }
                        //        else
                        //        {
                        //            ErrorDebug(fileName + " Animator is not exist");
                        //        }
                        //        LoadModelOver(unit, fileName);
                        //    },
                        //    "1p");
                        //AddManager(cgUnit, false);
                        var t = new TransformSet
                        {
                            Layer = (int)PublicConst.LayerSetting.CG,
                            Name = "1p",
                            FScale = Math.Abs(TLBattleScene.Instance.Actor.Info.BodyScale) > 0.001? TLBattleScene.Instance.Actor.Info.BodyScale:1.0f,
                        };
                        RenderSystem.Instance.LoadGameUnit(avatarMap, t, (ao) =>
                        {
                            LoadModelOver(ao, fileName);
                        }, LoadCount);

                    }
                    else //其他玩家
                    {

                    }

                }
                else
                {
                    ErrorDebug(fileName);
                }
            }
            //其它单位
            else if (fileName.IndexOf("npc:") != -1)
            {
                string[] splittext = fileName.Split(':');
                if (splittext.Length > 0)
                {
                    int result = 0;
                    if (int.TryParse(splittext[1], out result))
                    {
                        UnitInfo info = TLClientBattleManager.Templates.GetUnit(result);
                        if (info == null)
                        {
                            Debugger.LogError("templateid " + result + " is not exist");
                            LoadCount();
                            return;
                        }

                        //TLCGUnit cgUnit = new TLCGUnit(TLBattleScene.Instance, info,
                        //        (unit) =>
                        //        {
                        //            LoadModelOver(unit, fileName);
                        //        },
                        //        "Npc" + result);

                        // AddManager(cgUnit, false);
                        var t = new TransformSet
                        {
                            Layer = (int)PublicConst.LayerSetting.CG,
                            Name = "Npc",
                            FScale = Math.Abs(info.BodyScale) > 0.001 ? info.BodyScale : 1.0f,
                        };
                        if (info.Properties != null)
                        {
                            var avatarlist = (info.Properties as TLUnitProperties).ServerData.AvatarList;
                            if (avatarlist != null && avatarlist.Count != 0)
                            {
                                var avatarmap = GameUtil.ToAvatarMap(avatarlist);
                                RenderSystem.Instance.LoadGameUnit(avatarmap, t, (ao) =>
                                {
                                    LoadModelOver(ao, fileName);
                                }, LoadCount);
                                return;
                            }
                        }
                        RenderSystem.Instance.LoadGameUnit(info.FileName, t, (ao) =>
                        {
                            LoadModelOver(ao, fileName);
                        }, LoadCount);
                    }
                    else
                    {
                        ErrorDebug(fileName);

                    }

                }
                else
                {
                    ErrorDebug(fileName);
                }
            }
            else if (fileName.IndexOf("ef:") != -1
                     || fileName.IndexOf("ef_item:") != -1
                     || fileName.IndexOf("item:") != -1)
            {
                string[] splittext = fileName.Split(':');
                if (splittext.Length > 1)
                {
                    if (!string.IsNullOrEmpty(splittext[1]))
                    {
                        string _fileName = "/res/effect/" + splittext[1] + ".assetbundles";
                        if (fileName.IndexOf("ef_item:") != -1)
                        {
                            _fileName = "/res/effect/item/" + splittext[1] + ".assetbundles";
                        }
                        else if (fileName.IndexOf("item:") != -1)
                        {
                            _fileName = "/res/unit/" + splittext[1] + ".assetbundles";
                        }
                        //TLCGUnit cgUnit = new TLCGUnit(TLBattleScene.Instance, _fileName,
                        //       (unit) =>
                        //       {
                        //           LoadModelOver(unit, fileName);
                        //       },
                        //       "Effect:" + splittext[1]);
                        // AddManager(cgUnit, true);
                        var t = new TransformSet
                        {
                            Layer = (int)PublicConst.LayerSetting.CG,
                            Name = "Effect:" + splittext[1],
                        };
                        RenderSystem.Instance.LoadGameUnit(_fileName, t, (ao) =>
                        {
                            LoadModelOver(ao, fileName);
                        }, LoadCount);
                    }
                }
            }
        }
		private void AddManager(TLCGUnit cgUnit, bool isEffect)
        {
            //cgUnit.LoadModel();
            //if (isEffect)
            //{
            //    CgEffectManager.Add(cgUnit);
            //}
            //else
            //    CgUnitManager.Add(cgUnit);
        }
        private void LoadModelOver(AssetGameObject ao, string fileName)
        {
            //Debug.Log("LoadModelOver.name=" + fileName);
            // var aniobj = unit.ObjectRoot.GetComponentInChildren<Animator>();
            //var actor = GameObject.Instantiate(unit.ObjectRoot);
            ////很重要 what the fuck?
            //ao.transform.SetParent(CurrentCutScene.transform, true);
            //var comps = actor.GetComponentsInChildren<AssetComponent>(true);
            //foreach (var c in comps)
            //{
            //    DeepCore.Unity3D.UnityHelper.DestroyImmediate(c);
            //}
            //unit.Dispose();
            if (IsDisposed)
            {
                ao.Unload();
                return;
            }
            ao.transform.SetParent(CurrentCutScene.transform, false);

            var cutsceneTrackAllGroup = GetActorGroup();
            if (cutsceneTrackAllGroup != null)
            {
                foreach (var _cutsceneGroup in cutsceneTrackAllGroup)
                {
                    if (_cutsceneGroup.isActive && !string.IsNullOrEmpty(_cutsceneGroup.name)&& _cutsceneGroup.actor  == null && _cutsceneGroup.name.ToLower() == fileName)
                    {
                        _cutsceneGroup.actor = ao.gameObject;
                        foreach (var track in _cutsceneGroup.tracks)
                        {
                            if (track is TLPlayerAnimatorTrack)
                            {
                                TLPlayerAnimatorTrack playtrack = track as TLPlayerAnimatorTrack;
                                playtrack.userdata = ao.gameObject;
                            }
                        }
                        break;
                    }
                }
            }
            LoadCount();
        }

        private void LoadCount()
        {
            mInitNum--;
            if (mInitNum == 0)
            {

                var cutsceneTrackAllGroup = GetActorGroup();
                if (cutsceneTrackAllGroup != null)
                {
                    
                    foreach (var _cutsceneGroup in cutsceneTrackAllGroup)
                    {
                        if (!string.IsNullOrEmpty(_cutsceneGroup.name))
                        {
                            foreach (var tracks in _cutsceneGroup.tracks)
                            {
                                foreach (var actionclip in tracks.actions)
                                {
                                    if (actionclip is TLSetTransformParent)
                                    {
                                        var settransformparent = actionclip as TLSetTransformParent;
                                        var objectName = settransformparent.newParent.ToLower().Split('|');
                                       
                                        foreach (var __cutsceneGroup in cutsceneTrackAllGroup)
                                        {
                                            if (objectName[0] == __cutsceneGroup.name.ToLower()
                                            && settransformparent.newParentTransform == null)
                                            {
                                               
                                                if (objectName.Length>1)
                                                {
                                                    List<Transform> mTransforms = new List<Transform>();
                                                    __cutsceneGroup.actor.GetComponentsInChildren<Transform>(mTransforms);
                                                    foreach (var tran in mTransforms)
                                                    {
                                                        if (tran.name.ToLower() == objectName[1].ToLower())
                                                        {
                                                            settransformparent.newParentTransform = tran;
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    settransformparent.newParentTransform = __cutsceneGroup.actor.transform;
                                                }
                                                break;
                                            }

                                            
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                if (InitComplete != null)
                {
                    LoadSuccess = true;
                    InitComplete(this,!_stopplay);
                    if (stopwatch != null)
                    {
                        stopwatch.Stop();
                        if (TLUnityDebug.DEBUG_MODE)
                        {
                            Debugger.LogWarning("InitCG cost time ="+ stopwatch.ElapsedMilliseconds/1000f);
                        }
                        
                    }
                }
            }
        }
       


        private void OnSectionReached(Section obj)
        {
            //Debugger.Log("{0} OnSectionReached" , obj);
        }

        private void OnGlobalMessageSend(string arg1, object arg2)
        {
            //Debugger.Log("{0}OnGlobalMessageSend {1}" ,arg1, arg2);
        }


        private void CutscenePaused(Cutscene _cutscene)
        {
            //Debugger.Log("{0} CutscenePaused", _cutscene.name);
        }
        private void CutsceneStopped(Cutscene _cutscene)
        {
            //Debugger.Log("{0} Has been stopped." , _cutscene.name);
        }
        public void StopCutScenePlay()
        {
            _stopplay = true;
            if (this.m_CutScene.isActive)
            {
                m_CutScene.Stop(Cutscene.StopMode.SkipRewindNoUndo);
            }
        }
        
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            IsDisposed = true;

            mFileNameList.Clear();
            //foreach (var unit in CgEffectManager)
            //{
            //    unit.Dispose();
            //}
            //foreach (var unit in CgUnitManager)
            //{
            //    unit.Dispose();
            //}
            //CgEffectManager.Clear();
            //CgUnitManager.Clear();
            AudioList.Clear();
        }

       
    }

}
