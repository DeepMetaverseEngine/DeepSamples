using Slate;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using DeepCore.Unity3D.Utils;
using DeepCore;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.UGUI;
using Assets.Scripts.Cinema;
using Assets.Scripts;
using UnityEngine.UI;
using Slate.ActionClips;
using Assets.ParadoxNotion.SLATE_Cinematic_Sequencer.TLExtend;
using DeepCore.Unity3D;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

public class CutSceneManager : MonoBehaviour
{
    enum PlayState
    {
        None = 1,
        Ready = 2,
        Play = 3,
    }

    private class CutScenePlay
    {
        public PlayState PlayState = PlayState.None;
        public System.Action Action;
        public bool CanSkip = false;
        public string fileName;
        public int MapId;
    }


    private static CutSceneManager mInstance;
    private FuckAssetObject mCSObject = null;
    private CutSceneControl mCutScene;
    public CutSceneControl CurrentCutScene { get { return mCutScene; } }
    private bool IsDispose = false;
    //private PlayState mPlayState = PlayState.None;
    private bool mCanSkip = false;
    
    public TLAIActor.GState LastState { get; set; }
    private HashMap<GameObject, BubbleChatFrameUIBase> mBubblelist = new HashMap<GameObject, BubbleChatFrameUIBase>();
    private List<CutScenePlay> mCutSceneList = new List<CutScenePlay>();
    private bool mLastHide = false;
    private HZLabel mSkipLabel = null;
    private HZLabel mRichTextLabel = null;
    public LetterBoxEffect LetterBox;
    public FadeEffectScreen FadeEffect;
    private float LastTimeScale = 0;
    private bool mActorLoad = false;
    
    private string mCurPlayfileName;
    private bool IsFinished = false;
    private int _curPlaySceneId = 0;
    private bool _stopAll = false;
    //private TimeExpire<int> mTimeExpire = new TimeExpire<int>(10000);
    protected HZLabel OnCreatLabel()
    {
        DisplayNode parent = HZUISystem.Instance.GetCGLayer();
        var _Label = HZLabel.CreateLabel();
        _Label.UnityObject.name = "RichTextLabel";
        _Label.Transform.anchorMin = Vector2.zero;
        _Label.Transform.anchorMax = Vector2.one;
        _Label.Transform.pivot = new Vector2(0.5f, 0.5f);
        _Label.Bounds2D = new Rect(0, 0, 0, 0);
        parent.AddChild(_Label);
        _Label.Width = UGUIMgr.SCREEN_WIDTH;
        return _Label;
    }
    private void Awake()
    {
        //GameObject.DontDestroyOnLoad(this);
        if (mInstance == null)
        {
            mActorLoad = false;
            mInstance = this;
            mLastHide = false;
            
            mCutSceneList.Clear();
            EventManager.Subscribe("CGBubbleChatEnable", ShowBubbleChat);
            EventManager.Subscribe("CGBubbleChatDisable", HideBubbleChat);
            EventManager.Subscribe("Event.Scene.ChangeFinish", ChangeScene);
            EventManager.Subscribe("Event.Actor.DeadEvent", StopCutScene);

            RegisterCallbacks();
            LetterBox.Init();
            Debugger.Log("CinemaManager Init");
        }
    }

    private void StopCutScene(EventManager.ResponseData res)
    {
        StopAllPlay();
    }

    private void ChangeScene(EventManager.ResponseData res)
    {
        InitLabel();
        mActorLoad = true;
    }
    
    //private void ActorLoadOk(EventManager.ResponseData res)
    //{
    //    var dict = res.data[1] as Dictionary<string, object>;
    //    if (dict != null)
    //    {
    //        object t;
    //        if (dict.TryGetValue("ActorLoaded", out t))
    //        {
    //            mActorLoad = (bool)t;
    //            if (mActorLoad)
    //            {
    //                QuenePlay();
    //            }
    //        }
    //    }
    //}

    private void RegisterCallbacks()
    {
        Cutscene.OnCutsceneStarted += CutsceneStarted;
        Cutscene.OnCutsceneStopped += CutsceneFinished;
        DirectorGUI.OnScreenFadeGUI += DirectorGUI_OnScreenFadeGUI;
        DirectorGUI.OnLetterboxGUI += DirectorGUI_OnLetterboxGUI;
        DirectorGUI.OnTextOverlayGUI += DirectorGUI_OnTextOverlayGUI;
        DirectorGUI.OnGUIUpdate += OnGUIUpdate;
        TLPlayAudio.OnSoundVolumeHandle = OnSoundVolume;
        TLLanguageManager.OnGetContent = OnGetContent;
        TLOverlayText.OnCreateObject = OnCreateObject;
        TLOverlayText.OnDestoryOverText = OnDestoryOverText;
        TLOverlayText.OnUpdateOverText = OnUpdateOverText;
    }

    private void OnUpdateOverText(System.Object _object, string text, string ID, Color color, float size, TextAnchor alignment, Vector2 position)
    {
        if (IsFinished) return;
        if (_object is HZLabel)
        {
            HZLabel _label = (HZLabel)_object;
            _label.Visible = true;
            if (_label.Text != text)
            {
                _label.Text = text;
                (_label.TextGraphics as TextGraphics).alignment = alignment;
            }
            (_label.TextGraphics as TextGraphics).color = color;
            _label.FontSize = (int)size;
            position.y = position.y * 2;
            if (((_label.TextGraphics as TextGraphics).alignment == TextAnchor.LowerRight
               || (_label.TextGraphics as TextGraphics).alignment == TextAnchor.LowerLeft
              || (_label.TextGraphics as TextGraphics).alignment == TextAnchor.LowerCenter)
                 && GameUtil.IsIPhoneX())
            {
                position.y -= HZUISystem.Instance.IPhoneXOffY;
            }
            _label.TextOffset = position;

        }
    }

    private void OnDestoryOverText(System.Object _object)
    {
        if (_object is HZLabel)
        {
            HZLabel _label = (HZLabel)_object;
            _label.Visible = false;
            _label.RemoveFromParent(true);
        }

    }

    private System.Object OnCreateObject()
    {
        return OnCreatLabel();
    }

    private void DirectorGUI_OnTextOverlayGUI(string text, Color color, float size, TextAnchor alignment, Vector2 position)
    {
        if (mRichTextLabel != null)
        {
            if (mRichTextLabel.Text != text)
            {
                mRichTextLabel.Visible = true;
                mRichTextLabel.Text = text;
                (mRichTextLabel.TextGraphics as TextGraphics).alignment = alignment;
            }
            (mRichTextLabel.TextGraphics as TextGraphics).color = color;
            mRichTextLabel.FontSize = (int)size;
            mRichTextLabel.TextOffset = position;
            if (((mRichTextLabel.TextGraphics as TextGraphics).alignment == TextAnchor.LowerRight
                 || (mRichTextLabel.TextGraphics as TextGraphics).alignment == TextAnchor.LowerLeft
                || (mRichTextLabel.TextGraphics as TextGraphics).alignment == TextAnchor.LowerCenter)
                   && GameUtil.IsIPhoneX())
            {
                var pos = position;
                pos.y -= HZUISystem.Instance.IPhoneXOffY;
                mRichTextLabel.TextOffset = pos;
            }
        }


    }

    private void DirectorGUI_OnLetterboxGUI(float completion)
    {
        if (LetterBox != null)
        {
            if (!LetterBox.gameObject.activeSelf)
            {
                LetterBox.gameObject.SetActive(true);
            }
            if (completion == 1)
            {
                if (mSkipLabel != null && mCanSkip)
                {
                    mSkipLabel.Visible = mCanSkip;
                }
            }
            LetterBox.Show(completion);
        }

    }

    private void DirectorGUI_OnScreenFadeGUI(Color color)
    {
        if (FadeEffect != null)
        {
            if (!FadeEffect.gameObject.activeSelf)
            {
                FadeEffect.gameObject.SetActive(true);
            }
            FadeEffect.FadeCustom(color);
        }

    }
    void OnDestroy()
    {
        UnRegisterCallbacks();
    }
    private void UnRegisterCallbacks()
    {

        Cutscene.OnCutsceneStarted -= CutsceneStarted;
        Cutscene.OnCutsceneStopped -= CutsceneFinished;
        DirectorGUI.OnScreenFadeGUI -= DirectorGUI_OnScreenFadeGUI;
        DirectorGUI.OnLetterboxGUI -= DirectorGUI_OnLetterboxGUI;
        DirectorGUI.OnTextOverlayGUI -= DirectorGUI_OnTextOverlayGUI;
        DirectorGUI.OnGUIUpdate -= OnGUIUpdate;
        TLLanguageManager.OnGetContent = null;
        TLPlayAudio.OnSoundVolumeHandle = null;
        TLOverlayText.OnCreateObject = null;
        TLOverlayText.OnDestoryOverText = null;
        TLOverlayText.OnUpdateOverText = null;
        mCutSceneList.Clear();
        EventManager.Unsubscribe("CGBubbleChatEnable", ShowBubbleChat);
        EventManager.Unsubscribe("CGBubbleChatDisable", HideBubbleChat);
        EventManager.Unsubscribe("Event.Scene.ChangeFinish", ChangeScene);
        EventManager.Unsubscribe("Event.Actor.DeadEvent", StopCutScene);
    }

    private float OnSoundVolume(float Volume)
    {
        return SoundManager.Instance.DefaultSoundVolume;
    }

    private string OnGetContent(string text, string textid)
    {
        if (string.IsNullOrEmpty(textid))
        {
            Debugger.LogError(text + " is not in lang");
        }
        else
        {
            var db = GameUtil.GetDBData2("CGLang", "{ cg_key = \"" + textid + "\"}");
            if (db != null)
            {
                if (db.Count == 0)
                {
                    Debugger.LogError("TextID " + textid + " is not exist @ small stone or small liang");
                }
                else
                {
                    return HZLanguageManager.Instance.GetString(db[0]["cg_lang"].ToString());
                }
            }
        }
        return HZLanguageManager.Instance.GetString(text);
    }
    private void CutsceneStarted(Cutscene _cutscene)
    {
        Debugger.Log("{0} Has just started.", _cutscene.name);
    }


    private void CutsceneFinished(Cutscene _cutscene)
    {
        Debugger.Log("{0} CutsceneFinished.", _cutscene.name);
        PlaysceneFinished(_cutscene);
    }

    private void OnGUIUpdate()
    {
        DoSkipText();
    }


    void DoSkipText()
    {
        if (!mCanSkip)
        {
            return;
        }
        //if(mOverlayTextStyle == null)
        //{
        //    mOverlayTextStyle = new GUIStyle();
        //    mOverlayTextStyle.normal.textColor = Color.white;
        //    mOverlayTextStyle.richText = true;
        //    mOverlayTextStyle.alignment = TextAnchor.UpperRight;
        //}

        //var rect = Rect.MinMaxRect(Screen.width - 60, 10, Screen.width - 20, 60);
        //rect.center = new Vector2(Screen.width - 30, 40);
        //var finalText = HZLanguageManager.Instance.GetString("quest_story_skip");
        ////text
        //GUI.Label(rect, finalText, mOverlayTextStyle);
    }
    public void InitLabel()
    {
        if (mSkipLabel == null)
        {
            DisplayNode parent = HZUISystem.Instance.GetCGLayer();
            Vector2 _position = new Vector2(-10, -10);
            mSkipLabel = OnCreatLabel();
            parent.AddChild(mSkipLabel);
            mSkipLabel.Enable = true;
            mSkipLabel.IsInteractive = true;
            mSkipLabel.UnityObject.name = "SkipLabel";
            mSkipLabel.FontSize = 36;
            (mSkipLabel.TextGraphics as TextGraphics).alignment = TextAnchor.UpperRight;
            mSkipLabel.SetAnchor(new Vector2(1, 1));
            mSkipLabel.Transform.pivot = new Vector2(1, 1);
            mSkipLabel.Text = HZLanguageManager.Instance.GetString("quest_story_skip");
            mSkipLabel.Transform.anchoredPosition = _position;
            mSkipLabel.Size2D = mSkipLabel.PreferredSize;
            mSkipLabel.event_PointerDown += SkipLableDown;
        }

        if (mRichTextLabel == null)
        {
            mRichTextLabel = OnCreatLabel();
        }
        mSkipLabel.Visible = false;
        mRichTextLabel.Text = "";
        mRichTextLabel.Visible = false;
        SaveCameraState();
        IsDispose = false;

    }
    private void SkipLableDown(DisplayNode sender, PointerEventData e)
    {
        if (mCanSkip && IsPlaying )
        {
            //mCutScene.CurrentCutScene.Pause();
            StopPlay();
        }
    }

    public static CutSceneManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                //new CutSceneManager();
                return null;
            }
            return mInstance;
        }
    }
    private void Update()
    {
        if (mCutScene != null)
        {
            mCutScene.Update();
        }
        else
            QuenePlay();
        //float deltaTime = Time.deltaTime;
        //if (mPlayState == PlayState.Ready && mTimeExpire.Update((int)(deltaTime * 1000)))
        //{
        //    StopPlay();
        //}
    }
    public void Play(string fileName,bool canSkip, System.Action callback = null,int Mapid = 0)
    {
       
        foreach (var playdata in mCutSceneList)
        {
            if (playdata.fileName == fileName)
            {
                Debugger.LogError("Same CgfileName = " + fileName + " is playing");
                return;
            }
        }
        CutScenePlay play = new CutScenePlay();
        play.PlayState = PlayState.None;
        play.Action = callback;
        play.CanSkip = canSkip;
        play.fileName = fileName;
        play.MapId = Mapid == 0?DataMgr.Instance.UserData.MapTemplateId:Mapid;
        //Debugger.LogError("PlaycgMapid="+DataMgr.Instance.UserData.MapTemplateId);
        mCutSceneList.Add(play);
        
    }
    private bool HasCutScene()
    {
        return mCutSceneList == null ? false : mCutSceneList.Count > 0;
    }


    public bool IsPlaying {
        get {
            if (mCutSceneList != null && mCutSceneList.Count > 0)
            {
                var cutPlayScene = mCutSceneList[0];
                if (cutPlayScene != null)
                {
                    return cutPlayScene.PlayState == PlayState.Ready||cutPlayScene.PlayState == PlayState.Play;
                }
            }
            return false;
        } }
    private void QuenePlay()
    {
        if (!IsPlaying && mActorLoad && !IsDispose)
        {
            if (mCutSceneList.Count > 0)
            {
                var cutScenePlay = mCutSceneList[0];
                if (cutScenePlay.PlayState == PlayState.None && cutScenePlay.MapId == DataMgr.Instance.UserData.MapTemplateId)
                {
                    Dictionary<object, object> param = new Dictionary<object, object>
                    {
                        { "PlayCG", true }
                    };
                    EventManager.Fire("EVENT_PLAYCG_START", param);
                    InitLabel();
                    LetterBox.gameObject.SetActive(true);
                    FadeEffect.gameObject.SetActive(true);
                    var node = cutScenePlay.fileName;
                    DramaUIManage.Instance.highlightMask.SetArrowTransform(true, null, 1);
                    LastTimeScale = Time.timeScale;
                    cutScenePlay.PlayState = PlayState.Ready;
                    //Debugger.LogError("cgMapid="+DataMgr.Instance.UserData.MapTemplateId);
                    Play(node, cutScenePlay.CanSkip);
                    
                }
            }
        }
      
    }
    public void CanSkip(bool value)
    {
        mCanSkip = value;

    }
    private void Play(string filename,bool canSkip, FuckAssetObject sceneobj)
    {
        mCSObject = sceneobj;
        if (mCSObject != null)
        {
            var cutscene = mCSObject.gameObject.GetComponent<Cutscene>();

            if (cutscene != null)
            {
                if (cutscene.isActive)
                {
                    cutscene.Stop();
                    cutscene.Rewind();
                }
                mCutScene = new CutSceneControl(filename, canSkip,cutscene, CutsceneInitComplete);
                mCutScene.InitAndPlay();
            }
            else
            {
                Debugger.LogError("not exist USSequencer");
            }
        }
    }
    private void Play(string fileName,bool canSkip)
    {
        //mTimeExpire.Reset();
        mCanSkip = false;
        IsFinished = false;
        Debugger.Log("CGLoad file [" + fileName + "]");
        var playfileName = "/res/cg/" + fileName + ".assetbundles";
        mCurPlayfileName = playfileName;
        //mPlayState = PlayState.Ready;
        SaveCameraState();
        HideCamera(true);
        FuckAssetLoader fuckloader = null;
        Action<FuckAssetObject> func = (aoe) =>
         {
             if (aoe)
             {
                 if (IsDispose)
                 {
                     aoe.Unload();
                     return;
                 }
                 aoe.DontMoveToCache = true;
                 var acs = fuckloader.Bundle.AssetBundle.LoadAllAssets<AudioClip>();
                 foreach (var item in acs)
                 {
                     item.LoadAudioData();
                 }
                 this.Play(fileName, canSkip, aoe);
             }
             else
             {
                 Debugger.LogError("cg file [" + fileName + "]  is not exist");
                 DramaUIManage.Instance.highlightMask.SetArrowTransform(false);
                 PlaysceneFinished(null);
                 if (mCutSceneList[0] != null)
                 {
                     mCutSceneList[0].Action.Invoke();
                 }
             }
         };


        _curPlaySceneId = FuckAssetObject.GetOrLoad(playfileName, null, func);
        fuckloader = FuckAssetLoader.GetLoader(_curPlaySceneId);
    }

//    private void StopCutScenePlay()
//    {
//        if (mCutScene != null && mCutScene.CurrentCutScene != null)
//        {
//            mCutScene.CurrentCutScene.Stop(Cutscene.StopMode.SkipRewindNoUndo);
//        }
//    }
    private void StopPlay()
    {
        if (mCutScene != null)
        {
            mCutScene.StopCutScenePlay();
        }

    }
    private void StopAllPlay()
    {
        if (mCutScene == null && _curPlaySceneId != 0)
        {
           var fuckloader = FuckAssetLoader.GetLoader(_curPlaySceneId);
           fuckloader.Discard();
        }
        
        StopPlay();

        if (mCutSceneList != null)
            mCutSceneList.Clear();
        BlackScreenOver();
    }

    //初始化完成 开始播放
    private void CutsceneInitComplete(CutSceneControl obj,bool isSuccess)
    {
        if (!isSuccess)
        {
            if (obj!=null)
            {
                CutsceneFinished(obj.CurrentCutScene);
            }
            return;
        }

        if (obj != null)
        {
            //mPlayState = PlayState.Play;
            //mTimeExpire.Reset();
            SoundManager.Instance.StopAllSound();
            var bgmvol = GameUtil.GetIntGameConfig("BGM_Percentage_reduction");
            SoundManager.Instance.SetCurrentBGMVol(SoundManager.Instance.DefaultBGMVolume*bgmvol/100f);
            
            DramaUIManage.Instance.highlightMask.SetArrowTransform(true);
            Debugger.Log("CurrentCutScene " + CurrentCutScene.CurrentCutScene.name + " is init Complete ");

            StopAutoRun();
            string[] laynames = { "NavLayer", "CG", "Water", "LightLayer", "TransparentFX", "SMALLITEM","Camera"};
            //先手动改下
            bool bloom = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.BLOOM) == 1;
            var post = DirectorCamera.current.cam.GetAddComponent<PostProcessLayer>();
            var vol = DirectorCamera.current.cam.GetAddComponent<PostProcessVolume>();
            post.enabled = bloom;
            vol.enabled = bloom;
            if (bloom)
            {
                var resources = Resources.Load<PostProcessResources>("PostProcessResources");
                post.Init(resources);
                post.volumeTrigger = DirectorCamera.current.cam.transform;
                post.volumeLayer = LayerMask.GetMask(laynames);
                vol.isGlobal = true;
                vol.weight = 0.6f;
                var mainCameraPostProcessing = Resources.Load<PostProcessProfile>("MainCamera Profile");
                vol.profile = mainCameraPostProcessing;
            }
            
            
            DirectorCamera.current.cam.farClipPlane = 1000;
            DirectorCamera.current.cam.nearClipPlane = 0.3f;
            
            var layer = Assets.Scripts.Setting.ProjectSetting.LAYER_SMALLITEM;
            if (layer >= 0 && layer < 32)
            {
                float[] distances = new float[32];
                distances[layer] = 50;
                DirectorCamera.current.cam.layerCullDistances = distances;
                DirectorCamera.current.cam.layerCullSpherical = true;
            }
            DirectorCamera.current.cam.cullingMask = LayerMask.GetMask(laynames);// (1 << 8) + (1 << 4) + (1 << 10) + (1 << 11) + 2;
            DirectorCamera.current.cam.depthTextureMode = DepthTextureMode.None;
#if UNITY_5_6_OR_NEWER
            DirectorCamera.current.cam.allowMSAA = false;
            DirectorCamera.current.cam.allowHDR = false;
#endif
            if (mCutSceneList!=null && mCutSceneList.Count > 0)
            {
                mCutSceneList[0].PlayState = PlayState.Play;
            }
            obj.CurrentCutScene.Play();
            mCanSkip = obj.CanSkip;

        }
    }
    private void StopAutoRun()
    {
        if (TLBattleScene.Instance.Actor != null)
        {
            if (TLBattleScene.Instance.Actor.CurGState is TLAIActor.PlayCGState)
            {
                return;
            }
            LastState = TLBattleScene.Instance.Actor.CurGState;
            TLBattleScene.Instance.Actor.ChangeActorState(new TLAIActor.PlayCGState(TLBattleScene.Instance.Actor));
        }
    }

    private void ResumeAutoRun()
    {
        if (LastState != null)
        {
            if (!(LastState is TLAIActor.ManualOperateState))
            {
                TLBattleScene.Instance.Actor.ChangeActorState(LastState);
                LastState = null;
            }
        }

    }
    //播放完成
    private void PlaysceneFinished(Cutscene csc)
    {
        Time.timeScale = LastTimeScale;
        HideCamera(false);
        if (mCutScene != null)
        {
            IsFinished = true;
            mCanSkip = false;
            mCutScene.Dispose();
            if (mCSObject)
            {
                mCSObject.Unload();
                mCSObject = null;
            }

            foreach (var bubble in mBubblelist.Values)
            {
                bubble.Close();
            }
            mBubblelist.Clear();
            //mPlayState = PlayState.None;
            
            //Resources.UnloadUnusedAssets();
            if (mCutSceneList != null && mCutSceneList.Count > 0)
            {
                mCutSceneList[0].Action.Invoke();
                mCutSceneList.RemoveAt(0);
            }
            mCutScene = null;

        }
        if (!HasCutScene())
        {
            Dictionary<object, object> param = new Dictionary<object, object>();
            param.Add("PlayCG", false);
            EventManager.Fire("EVENT_PLAYCG_START", param);
            BlackScreenOver();
            ResumeAutoRun();
            SoundManager.Instance.SetCurrentBGMVol(SoundManager.Instance.DefaultBGMVolume);
        }
    }
    private int SceneCameraCullMask;
    private int UICameraCullMask;
    private CameraClearFlags SceneCameraClearFlag;
    //fprivate int NpcTalkCameraMask;
    private int MainCameraCullMask;
    private CameraClearFlags MainCameraClearFlag;

    public void SaveCameraState()
    {
        if (IsPlaying)
            return;
        if (GameSceneMgr.Instance.SceneCamera != null)
        {
            SceneCameraCullMask = GameSceneMgr.Instance.SceneCamera.cullingMask;
            SceneCameraClearFlag = GameSceneMgr.Instance.SceneCamera.clearFlags;
        }
        if (GameSceneMgr.Instance.UICamera != null)
        {
            UICameraCullMask = GameSceneMgr.Instance.UICamera.cullingMask;
        }
        //if (GameSceneMgr.Instance.NpcTalkCamera != null)
        //{
        //    NpcTalkCameraMask = GameSceneMgr.Instance.NpcTalkCamera.cullingMask;
        //}
        if (GameSceneMgr.Instance.SceneCameraNode.mainCamera != null)
        {
            MainCameraCullMask = GameSceneMgr.Instance.SceneCameraNode.mainCamera.cullingMask;
            MainCameraClearFlag = GameSceneMgr.Instance.SceneCameraNode.mainCamera.clearFlags;
        }
    }

    public void HideCamera(bool isHide)
    {
        //Debugger.Log("HideCamera1");
        if (mLastHide == isHide)
        {
            return;
        }
        mLastHide = isHide;
        if (GameSceneMgr.Instance.SceneCamera != null)
        {
            GameSceneMgr.Instance.SceneCamera.cullingMask = isHide ? 0 : SceneCameraCullMask;
            GameSceneMgr.Instance.SceneCamera.clearFlags = isHide ? CameraClearFlags.SolidColor : SceneCameraClearFlag;
        }
        BattleInfoBarManager.HideAllName(isHide);
        BattleInfoBarManager.HideAllTitle(isHide);
        BattleInfoBarManager.HideAllGuild(isHide);
        BattleInfoBarManager.HideAllHPBar(isHide);
        BattleInfoBarManager.HideAllNPCSign(isHide);
        BubbleChatFrameManager.Instance.HideBubbleChat(isHide);
        BubbleChatFrameManager.Instance.HideCGBubbleChat(!isHide);
        MenuMgr.Instance.HideHud(isHide);
        MenuMgr.Instance.HideMenu(isHide);
        HZUISystem.Instance.GetUILayer().Visible = !isHide;
        HZUISystem.Instance.GetPickLayer().Visible = !isHide;
        HZUISystem.Instance.GetUIAlertLayer().Visible = !isHide;
        HZUISystem.Instance.GetCGLayer().Visible = isHide;
        if (BattleNumberManager.Instance != null)
        {
            BattleNumberManager.Instance.gameObject.SetActive(!isHide);
        }

        //if (GameSceneMgr.Instance.UICamera != null)
        //{
        //    GameSceneMgr.Instance.UICamera.cullingMask = isHide ? 0 : UICameraCullMask;
        //}
        //Debugger.Log("HideCamera3");

        //if (GameSceneMgr.Instance.NpcTalkCamera != null)
        //{
        //    GameSceneMgr.Instance.NpcTalkCamera.cullingMask = isHide ? 0 : NpcTalkCameraMask;
        //}
        //Debugger.Log("HideCamera4");
        if (GameSceneMgr.Instance.SceneCameraNode.mainCamera != null)
        {
            GameSceneMgr.Instance.SceneCameraNode.mainCamera.cullingMask = isHide ? 0 : MainCameraCullMask;

            GameSceneMgr.Instance.SceneCameraNode.mainCamera.clearFlags = isHide ?  CameraClearFlags.SolidColor : MainCameraClearFlag;

        }

        //Debugger.Log("HideCamera5");
    }
    public void BlackScreenOver()
    {
        DramaUIManage.Instance.highlightMask.SetArrowTransform(false);
        if (LetterBox != null)
        {
            LetterBox.gameObject.SetActive(false);
        }

        if (FadeEffect != null)
        {
            FadeEffect.ResetColor();
            FadeEffect.gameObject.SetActive(false);
        }
        
        _curPlaySceneId = 0;
        _stopAll = false;
    }

    public void Clear(bool relogin)
    {
        mActorLoad = false;
        IsDispose = true;

        HideCamera(false);
        if (IsPlaying)
        {
            StopPlay();
        }
        if (mSkipLabel != null)
        {
            mSkipLabel.event_PointerDown -= SkipLableDown;
            mSkipLabel.RemoveFromParent(true);
            mSkipLabel = null;
        }

        if (mRichTextLabel != null)
        {
            mRichTextLabel.RemoveFromParent(true);
            mRichTextLabel = null;
        }
        
        //HZLabel _label = null;
        //while (mLabelPool.TryPop(out _label))
        //{
        //    _label.RemoveFromParent(true);
        //}

        DisplayNode parent = HZUISystem.Instance.GetCGLayer();
        parent.RemoveAllChildren(true);

        if (mCutSceneList != null)
        {
            mCutSceneList.Clear();
        }
        
        BlackScreenOver();
    }


    private void ShowBubbleChat(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object value;
        string Content = string.Empty;
        if (data.TryGetValue("Content", out value))
        {
            Content = (string)value;
        }
        GameObject target = null;
        if (data.TryGetValue("Target", out value))
        {
            target = (GameObject)value;
        }
        float time;
        if (data.TryGetValue("Time", out value))
        {
            time = (float)value;
        }
        if (target != null)
        {
            BubbleChatFrameUIBase bubble = null;
            if (mBubblelist.TryGetValue(target, out bubble))
            {
                if (bubble is BubbleChatFrameTimeUI)
                {
                    (bubble as BubbleChatFrameTimeUI).SetContent(Content, -1);
                }
            }
            else
            {
                var headName = target.transform.FindRecursive("Head_Name");
                if (headName == null)
                {
                    headName = target.transform;
                }
                var bubblechat = BubbleChatFrameManager.Instance.ShowBubbleChatFrameByObject(headName.gameObject, Content);
                mBubblelist.Add(target, bubblechat);
            }

        }


    }

    private void HideBubbleChat(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object value;
        string Content = string.Empty;
        GameObject target = null;
        if (data.TryGetValue("Target", out value))
        {
            target = (GameObject)value;
        }
        if (target != null)
        {
            BubbleChatFrameUIBase bubble = null;
            if (mBubblelist.TryGetValue(target, out bubble))
            {
                bubble.Close();
                mBubblelist.RemoveByKey(target);
            }

        }
    }
}

