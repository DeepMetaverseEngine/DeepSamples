using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIAction;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using System.IO;
using Assets.Scripts;
using DeepCore.Unity3D;
using UnityEngine;

public class InteractiveHud : DisplayNode
{
    private HZRoot mRoot = null;
    private const string FILE_PATH = "xml/hud/ui_hud_event.gui.xml";

    public HZRoot Root
    {
        get
        {
            return mRoot;
        }
    }


    private HZCanvas mFrame = null;
    private HZLabel mLabel = null;
    private HZTextButton mPickButton = null;
    private HZImageBox mPickImg = null;
    private string mPcikImgName = null;

    private HZGauge mGauge = null;


    private GameObject mPickButtonEffect = null;
    public delegate void OnPickBtnClickEvent();
    public OnPickBtnClickEvent OnPickBtnClick;

    private Vector2 mFramePos;
  
    public InteractiveHud()
    {
        Init();
    }

    //每次进场景前会调用
    public void OnEnterScene()
    {

    }

    private void Init()
    {
        HZUISystem.SetNodeFullScreenSize(this);
        mRoot = (HZRoot)HZUISystem.CreateFromFile(FILE_PATH);
        this.Enable = false;
        mRoot.Enable = false;
        this.EnableChildren = true;
        if (mRoot != null) { this.AddChild(mRoot); }

        HZCanvas topright = mRoot.FindChildByEditName<HZCanvas>("cvs_event");
        HudManager.Instance.InitAnchorWithNode(topright, HudManager.HUD_BOTTOM);

        InitCompmont();
 
    }

    private void InitCompmont()
    {
        mFrame = mRoot.FindChildByEditName<HZCanvas>("cvs_event");
        if (mFrame != null)
        {
            if(GameUtil.IsIPhoneX())
            {
                mFrame.Y = mFrame.Y - HZUISystem.Instance.IPhoneXOffY;
            }

            mLabel = mFrame.FindChildByEditName<HZLabel>("lb_ms");
            var pos = mFrame.Position2D;
            mFrame.Position2D += mFrame.Size2D * 0.5f;
            mFrame.Transform.pivot = new Vector2(0.5f, 0.5f);
            mFramePos = mFrame.Position2D;
            mFrame.Y = 10000;

            mPickButton = mFrame.FindChildByEditName<HZTextButton>("btn_anniu");
            mPickImg = mFrame.FindChildByEditName<HZImageBox>("ib_event");

            mPickButton.TouchClick = (sender) =>
            {
                if (OnPickBtnClick != null)
                {
                    OnPickBtnClick();
                }
            };

            mGauge = mFrame.FindChildByEditName<HZGauge>("gg_jindu1");
            if (mGauge != null)
            {
                mGauge.SetFillMode(UnityEngine.UI.Image.FillMethod.Radial360, (int)UnityEngine.UI.Image.Origin360.Top, true);
                //mGauge.Orientation = GaugeOrientation.LEFT_2_RIGHT;

                mGauge.SetGaugeMinMax(0, 1);

            }
        }
    }

 
    public void ShowPick(string pickName)
    {
        //mGaugeTitle.Text = pickName;
        mLabel.Text = pickName;
    }

 
    public void setAutoImag(string pickImg)
    {
        if (string.IsNullOrEmpty(pickImg))
        {
            return;
        }

        if(pickImg == mPcikImgName)
        {
            return;
        }

        this.mPcikImgName = pickImg;

        UILayoutStyle imgStyle = mPickImg.Layout.Style;
        UILayout img = HZUISystem.CreateLayoutFromAtlas(pickImg, imgStyle, 0);
        mPickImg.Layout = img;
 
    }

    private void OnShowDialogEffectLoaded(FuckAssetObject loader)
    {
        if (!loader)
            return;

        if (mPickButtonEffect != null)
            DeepCore.Unity3D.UnityHelper.Destroy(mPickButtonEffect);
        var go = loader.gameObject;
        mPickButtonEffect = go;
        UILayerMgr.SetLayer(go, (int)PublicConst.LayerSetting.UI);
        go.transform.SetParent(mPickButton.Transform, false);
        go.transform.localPosition = new Vector3(mPickButton.Width / 2, -mPickButton.Height / 2, 0);
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
        UILayerMgr.SetLocalLayerOrder(go, 1, false);
    }

    //对话
    public void ShowDialog(bool isShow,int value = 0)
    {
        if (mFrame != null)
        {
            if (isShow)
            {
                if (mFrame.Y >= 10000 - 1)
                {
                    mFrame.Scale = new Vector2(0.5f, 0.5f);
                    var action = new ScaleAction();
                    action.Duration = 0.15f;
                    action.ScaleX = 1f;
                    action.ScaleY = 1f;
                    action.ActionFinishCallBack = (sender) =>
                    {
                        if (mPickButtonEffect != null)
                        {
                            mPickButtonEffect.SetActive(true);
                        }else
                        {
                            string path = "/res/effect/ui/ef_ui_frame_01.assetbundles";
                            FuckAssetObject.GetOrLoad(path, Path.GetFileNameWithoutExtension(path), OnShowDialogEffectLoaded);
                        }
                    };
                    mFrame.AddAction(action);
                }

                mFrame.Visible = true;
                mFrame.Y = mFramePos.y;
                string text = string.Format(HZLanguageManager.Instance.GetString("common_autopick"), value);
                mLabel.Text = text;

                mPickButton.Visible = true;
                mPickImg.Visible = true;
                mGauge.Visible = false;
            }
            else
            {
                mFrame.Y = 10000;
                mFrame.RemoveAllAction();
                mFrame.Visible = false;
                mFrame.Scale = Vector2.one;
                if (mPickButtonEffect != null)
                    mPickButtonEffect.SetActive(false);
            }
           
        }
    }

 

    //采集
    public void ShowGauge(bool isShow)
    {
        if (mFrame != null)
        {
            if (isShow)
            {
                mFrame.Visible = true;
                mFrame.Y = mFramePos.y;


                mPickButton.Visible = true;
                mPickImg.Visible = true;
                mGauge.Visible = true;
            }
            else
            { 
                mFrame.Visible = false;
                mFrame.Scale = Vector2.one;
                mFrame.Y = 10000;
            }
             
        }
    }


    private string formatTime(int ExpireTimeMS)
    {
        int min = ExpireTimeMS / 1000 / 60;
        int sec = ExpireTimeMS / 1000 % 60;
        string text = string.Format("{0:0#}:{1:0#}", min, sec);
        return text;
    }
    /// <summary>
    /// 设置进度
    /// </summary>
    /// <param name="percent">0～1</param>
    public void SetPercent(float percent,float ExpireTimeMS = 0)
    {
        if (mGauge != null)
        {
            ShowGauge(percent > 0);
            mGauge.SetGaugeMinMax(0, 1);
            mGauge.Value = percent;

            return;

            //小石说不显示时间
            if (ExpireTimeMS != 0 && mLabel != null)
            {
                mLabel.Text = formatTime((int)ExpireTimeMS);
            }
        }
    }

    public void ShowHandPickButton(string pickImg)
    {
        this.setAutoImag(pickImg);
        this.ShowGauge(true);
        if (mGauge != null)
        {
            mGauge.Value = 0;
        }

        string text = HZLanguageManager.Instance.GetString("common_pick");
        ShowHandPickDialog(text);
    }

    public void ShowHandPickDialog(string text)
    {
        if (mFrame != null)
        {
            mFrame.Scale = new Vector2(0.5f, 0.5f);
            var action = new ScaleAction();
            action.Duration = 0.15f;
            action.ScaleX = 1f;
            action.ScaleY = 1f;
            action.ActionFinishCallBack = (sender) =>
            {
                if (mPickButtonEffect != null)
                {
                    mPickButtonEffect.SetActive(true);
                }

            };
            mFrame.AddAction(action);

            mFrame.Visible = true;
            mFrame.Y = mFramePos.y; 
            mLabel.Text = text;
            mLabel.Visible = true;
            mPickButton.Visible = true;
            mPickImg.Visible = true;
            mGauge.Visible = false;
        }
    }

    public void Clear(bool relogin, bool reConnect)
    {
        SetPercent(0);
        ShowGauge(false);
    }

}
