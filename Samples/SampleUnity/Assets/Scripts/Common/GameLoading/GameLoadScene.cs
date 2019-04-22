using Assets.Scripts;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DeepCore.Unity3D;

public class GameLoadScene : MonoBehaviour, FingerHandlerInterface, PinchHandlerInterface
{
    
    public GameObject infoNode;
    public Slider ProgressObject;
    public Text CurVersion;
    public Text DownloadPercent;
    public Text ShowText;
    public Text TipText;
    public Image Logo;
    public Image Bg;

    private Sprite mBgSprite;
    private int mCurBg;
    private int mBgImgCount;

    void Awake()
    {
        Reset();
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        Percent = 0;
        ShowRandomTips();
    }

    public bool IsShow()
    {
        return this.gameObject.activeSelf;
    }

    /// <summary>
    /// 显示或隐藏整个loading界面
    /// </summary>
    /// <param name="isShow"></param>
    public virtual void Show()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
            //SoundManager.Instance.ChangeVolume();
            RandomBg();
            GameGlobal.Instance.FGCtrl.AddFingerHandler(this, (int)PublicConst.FingerLayer.Loading);
            GameGlobal.Instance.FGCtrl.AddPinchHandler(this, (int)PublicConst.FingerLayer.Loading);
        }
    }

    public void Hide()
    {
        if (this.gameObject.activeSelf)
        {
            if (mBgSprite != null)
            {
                Bg.sprite = null;
                Resources.UnloadAsset(mBgSprite.texture);
                mBgSprite = null;
            }
            HZUISystem.Instance.Editor.ReleaseTextureExt("loading_" + mCurBg, "/dynamic/dynamic_new/loading/");
            this.gameObject.SetActive(false);
            GameGlobal.Instance.FGCtrl.RemoveFingerHandler(this);
            GameGlobal.Instance.FGCtrl.RemovePinchHandler(this);
        }
    }

    /// <summary>
    /// 设置进度
    /// </summary>
    public float Percent
    {
        get { return ProgressObject.value; }
        set
        {
            ProgressObject.value = value;
            int per = System.Convert.ToInt32(value * 100);
            DownloadPercent.text = per.ToString() + "%";
        }
    }

    protected virtual void RandomBg()
    {
        mBgImgCount = 4;
        mCurBg = UnityEngine.Random.Range(0, mBgImgCount + 1);
        if (mCurBg == 0)
        {
            var texture2d = Resources.Load<Texture2D>("UpdataBG");
            mBgSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);
            Bg.sprite = mBgSprite;
        }
        else
        {
            GameUtil.ConvertToUnityUISprite(Bg, "/dynamic/loadingbg/loading_" + mCurBg);
        }
        AspectRatioFitter arf = Bg.GetComponent<AspectRatioFitter>();
        if (arf != null)
        {
            arf.aspectRatio = Bg.preferredWidth / Bg.preferredHeight;
        }
    }

    /// <summary>
    /// 显示或隐藏进度信息
    /// </summary>
    /// <param name="isShow"></param>
    public virtual void ShowProcessInfo(bool isShow)
    {
        if (infoNode.activeSelf != isShow)
        {
            infoNode.SetActive(isShow);
        }
    }

    /// <summary>
    /// 刷新一条随机提示
    /// </summary>
    public virtual void ShowRandomTips()
    {
        int maxnum  =  GameUtil.GetIntGameConfig("loading_tips_num")+1;
        int i = UnityEngine.Random.Range(1, maxnum);
        string loadingtips = "loading_tips" + i;
        TipText.text = HZLanguageManager.Instance.GetString(loadingtips) ;
    }

    /// <summary>
    /// 设置显示文本
    /// </summary>
    /// <param name="content"></param>
    public virtual void SetContent(string content)
    {
        ShowText.text = content;
    }

    /// <summary>
    /// 设置版本号
    /// </summary>
    /// <param name="version"></param>
    public virtual void SetVersion(string version)
    {
        CurVersion.text = version;
    }

    //单个手指按下时调用
    public bool OnFingerDown(int fingerIndex, Vector2 fingerPos) { return true; }
    //单个手指移动时调用
    public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta) { return true; }
    //单个手指抬起时
    public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown) { return true; }
    //所有手指全部抬起时调用
    public void OnFingerClear() { }

    public bool OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2) { return true; }
    public bool OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta) { return true; }
    public bool OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2) { return true; }

}

