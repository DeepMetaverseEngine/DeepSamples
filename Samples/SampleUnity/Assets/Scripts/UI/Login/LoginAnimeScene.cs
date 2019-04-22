using UnityEngine;
using System.Collections;
using DeepCore.Unity3D;
using DeepCore.Unity3D.UGUIEditor.UI;
using System.Collections.Generic;
using DeepMMO.Data;
using TLBattle.Common.Plugins;
using DeepCore;
using TLProtocol.Data;

public class LoginAnimeScene : MonoBehaviour {

    public delegate void OnInitFinish();
    public OnInitFinish InitFinish;

    public delegate void OnAnimeCompleteEvent();

    private GameObject[] mAvatarPoint;
    //private Dictionary<string, AnimeData> mAvatarList = new Dictionary<string, AnimeData>();

    //private AnimeData mCurRoleAnime;
    //private GameObject mCurEffect;
    //private string mCurAvatarName;
    private CameraAnimeTag mCurSceneAnime;
    private GameObject mCurRoleModel;
    private GameObject mCurProScene;
    private Animator mCurRoleAnime;
    //private List<AnimeData> mPlayingList = new List<AnimeData>();

    private GameObject mSceneRoot;
    private GameObject mLoginScene;
    private GameObject mRoleScene;
    private GameObject[] mProScene;
    private GameObject[][] mRoleModel;
    private Dictionary<string, UI3DModelAdapter.UIModelInfo> mAvatarList = new Dictionary<string, UI3DModelAdapter.UIModelInfo>();
    private float mLogoTime;

    private HZImageBox mBgImg;

    private const string scene_path = "/res/scene/chuangjue_map01.assetbundles";
    private float mFadeInTime = 0.6f;
    private float mFadeOutTime = 0.6f;
    private float mBlackTime = 0.3f;
    private float mRotateSpeed = 1.0f;

    private bool mIsFadeState;

    private OnAnimeCompleteEvent mFadeOutCB, mFadeInCB;

    public enum CameraAnimeTag
    {
        Title = 1,
        Role = 2,
    }

    public enum RoleAnimeTag
    {
        Idle,
        In,
        Show,
        Out
    }

    void Start()
    {
        //Init2DBackground();
        Init3DRes();
    }

    private void Init3DRes()
    {
        LoadRoleScene(scene_path);
    }

    private void LoadRoleScene(string path)
    {
        TLBattleFactory.Instance.TerrainAdapter.Load(path, LoadRoleSceneCB);
    }

    private void LoadRoleSceneCB(bool isLoadOK, GameObject o)
    {
        if (isLoadOK)
        {
            mSceneRoot = o;
            LoginSceneConfig cfg = mSceneRoot.GetComponent<LoginSceneConfig>();
            mLoginScene = cfg.loginScene;
            mRoleScene = cfg.roleScene;
            mProScene = cfg.map;
            mRoleScene.isStatic = true;
            mRoleScene.transform.position = Vector3.zero;
            mAvatarPoint = cfg.cs;
            mRoleScene.SetActive(false);
            mLogoTime = cfg.logoTime;

            mRoleModel = new GameObject[2][];
            mRoleModel[0] = cfg.male;
            mRoleModel[1] = cfg.female;
            
            if (InitFinish != null)
                InitFinish();
        }
    }

    public void Reset()
    {

    }

    public void SwitchSceneAnime(CameraAnimeTag anime, bool showFade, OnAnimeCompleteEvent fadeoutCb, OnAnimeCompleteEvent fadeinCb)
    {
        mFadeOutCB = fadeoutCb;
        mFadeInCB = fadeinCb;
        //Reset();
        if (anime == CameraAnimeTag.Title)
        {
            GameGlobal.Instance.overlayEffect.FadeOut(showFade ? mFadeOutTime : 0, () =>
            {
                mCurSceneAnime = CameraAnimeTag.Title;
                //mBgImg.Visible = true;
                mLoginScene.SetActive(true);
                mRoleScene.SetActive(false);
                if (mCurRoleModel != null)
                    mCurRoleModel.SetActive(false);

                if (mFadeOutCB != null)
                    mFadeOutCB();

                GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(showFade ? mBlackTime : 0, () =>
                {
                    GameGlobal.Instance.overlayEffect.FadeIn(showFade ? mFadeInTime : 0, () =>
                    {
                        GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(mLogoTime, () =>
                        {
                            if (mFadeInCB != null)
                                mFadeInCB();
                        }));
                    });
                }));
            });
        }
        else if (anime == CameraAnimeTag.Role)
        {
            mIsFadeState = true;
            GameGlobal.Instance.overlayEffect.FadeOut(showFade ? mFadeOutTime : 0, () =>
            {
                mCurSceneAnime = CameraAnimeTag.Role;
                //mBgImg.Visible = false;
                mLoginScene.SetActive(false);
                mRoleScene.SetActive(true);

                if (mFadeOutCB != null)
                    mFadeOutCB();

                GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(showFade ? mBlackTime : 0, () =>
                {
                    GameGlobal.Instance.StartCoroutine(WaitToLoadRoleScene(showFade));
                }));
            });
        }
    }

    private IEnumerator WaitToLoadRoleScene(bool showFade)
    {
        while (mRoleScene == null)
        {
            yield return 1;
        }
        GameGlobal.Instance.overlayEffect.FadeIn(showFade ? mFadeInTime : 0, () =>
        {
            if (mFadeInCB != null)
                mFadeInCB();
            mIsFadeState = false;
        });
    }

    private void Init2DBackground()
    {
        mBgImg = new DeepCore.Unity3D.UGUIEditor.UI.HZImageBox();
        mBgImg.Size2D = new Vector2(HZUISystem.SCREEN_WIDTH, HZUISystem.SCREEN_HEIGHT);

        UnityEngine.UI.Image img = GameGlobal.Instance.loadingUI.Bg;
        Texture2D t2d = img.sprite.texture;
        DeepCore.Unity3D.Impl.UnityImage image = new DeepCore.Unity3D.Impl.UnityImage(t2d, "loadingBg");
        DeepCore.Unity3D.UGUIEditor.UILayout layout = DeepCore.Unity3D.UGUIEditor.UILayout.CreateUILayoutImage(DeepCore.GUI.Data.UILayoutStyle.IMAGE_STYLE_BACK_4, image, 8);
        mBgImg.Layout = layout;

        Rect root = HZUISystem.Instance.RootRect;
        float scale = root.width > HZUISystem.SCREEN_WIDTH ? root.width / (float)HZUISystem.SCREEN_WIDTH : root.height / (float)HZUISystem.SCREEN_HEIGHT;

        float mMaskW = mBgImg.Width * scale;
        float mMaskH = mBgImg.Height * scale;

        float mMaskOffsetX = (root.width - mMaskW) * 0.5f;
        float mMaskOffsetY = (root.height - mMaskH) * 0.5f;

        mBgImg.Position2D = new Vector2(mMaskOffsetX, mMaskOffsetY);
        mBgImg.Size2D = new Vector2(mMaskW, mMaskH);

        HZUISystem.Instance.HUDLayerAddChild(mBgImg);
    }

    public void SwitchRoleWithPro(int pro, int gen, OnAnimeCompleteEvent fadeCb)
    {
        if (!mIsFadeState)
        {
            GameGlobal.Instance.overlayEffect.FadeOut(mFadeOutTime, () =>
            {
                SwitchRoleScene(pro);
                SwitchRoleWithGen(pro, gen);
                if (fadeCb != null)
                    fadeCb();
                GameGlobal.Instance.overlayEffect.FadeIn(mFadeInTime, () =>
                {
                });
            });
        }
        else
        {
            SwitchRoleScene(pro);
            SwitchRoleWithGen(pro, gen);
            if (fadeCb != null)
                fadeCb();
        }
    }

    public void SwitchRoleWithGen(int pro, int gen)
    {
        SwitchRoleModel(mRoleModel[gen][pro - 1]);
        Transform roleTrans = mCurRoleModel.transform;
        roleTrans.SetParent(mAvatarPoint[pro - 1].transform);
        roleTrans.localPosition = Vector3.zero;
        roleTrans.localEulerAngles = Vector3.zero;
        roleTrans.localScale = Vector3.one;
        mCurRoleAnime = mCurRoleModel.GetComponentInChildren<Animator>(true);
        mCurRoleAnime.Play("f_skill01");
    }

    public void SwitchAvatar(string name, List<AvatarInfoSnap> avatarData, int pro, OnAnimeCompleteEvent fadeCb)
    {
        UI3DModelAdapter.UIModelInfo avatarInfo;
        if (!mAvatarList.TryGetValue(name, out avatarInfo))
        {
            HashMap<int, TLAvatarInfo> avatarMap = GameUtil.ConvertAvatarListToMap(avatarData);
            avatarInfo = UI3DModelAdapter.LoadAvatar(avatarMap, 1 << (int)TLAvatarInfo.TLAvatar.Ride_Avatar01, (info)=>
            {

            });
            mAvatarList.Add(name, avatarInfo);
            avatarInfo.RootTrans.SetParent(mAvatarPoint[pro - 1].transform);
            avatarInfo.RootTrans.localPosition = Vector3.zero;
            avatarInfo.RootTrans.localEulerAngles = Vector3.zero;
            avatarInfo.RootTrans.localScale = Vector3.one;
        }
        if (!mIsFadeState)
        {
            GameGlobal.Instance.overlayEffect.FadeOut(mFadeOutTime, () =>
            {
                SwitchRoleScene(pro);
                SwitchRoleModel(avatarInfo.RootTrans.gameObject);
                if (fadeCb != null)
                    fadeCb();
                GameGlobal.Instance.overlayEffect.FadeIn(mFadeInTime, () =>
                {
                });
            });
        }
        else
        {
            SwitchRoleScene(pro);
            SwitchRoleModel(avatarInfo.RootTrans.gameObject);
            if (fadeCb != null)
                fadeCb();
        }
    }

    private void SwitchRoleScene(int pro)
    {
        if (mCurProScene != null)
            mCurProScene.SetActive(false);
        mCurProScene = mProScene[pro - 1];
        mCurProScene.SetActive(true);
    }

    private void SwitchRoleModel(GameObject nextModel)
    {
        if (mCurRoleModel != null)
            mCurRoleModel.SetActive(false);
        mCurRoleModel = nextModel;
        mCurRoleModel.SetActive(true);
    }

    public void ModelRotate(float delta)
    {
        if (mCurRoleModel != null)
        {
            mCurRoleModel.transform.Rotate(Vector3.up, delta * 0.5f);
        }
    }

    //void Update()
    //{
    //    if (mCurRoleAnime != null)
    //    {
    //        var stateInfo = mCurRoleAnime.GetCurrentAnimatorStateInfo(0);
    //        if (stateInfo.IsName("f_skill01"))
    //        {
    //            if (stateInfo.normalizedTime >= 1f)
    //            {
    //                mCurRoleAnime.Play("f_idle");
    //            }
    //        }
    //    }
    //}

    public void Destroy()
    {
        Reset();
        mFadeOutCB = mFadeInCB = null;
        //HZUISystem.Instance.HUDLayerRemoveChild(mBgImg, true);
        foreach (var info in mAvatarList.Values)
        {
            UI3DModelAdapter.ReleaseModel(info.Key);
        }
        DeepCore.Unity3D.UnityHelper.Destroy(mSceneRoot);
        DeepCore.Unity3D.UnityHelper.Destroy(this);
        (DeepCore.Unity3D.Battle.BattleFactory.Instance.TerrainAdapter as DefaultTerrainAdapter).Clear();
    }

    private class AnimeData
    {
        public RenderUnit RUnit { get; set; }
        public Animation Anime { get; set; }
        public int Tag { get; set; }
        public int Pro { get; set; }
        public bool ReadyToPlay { get; set; }
        public OnAnimeCompleteEvent Callback { get; set; }

        public AnimeData(RenderUnit RUnit, Animation Anime, int Tag, int Pro, OnAnimeCompleteEvent Callback)
        {
            this.RUnit = RUnit;
            this.Anime = Anime;
            this.Tag = Tag;
            this.Pro = Pro;
            this.Callback = Callback;
            ReadyToPlay = false;
        }
    }

}
