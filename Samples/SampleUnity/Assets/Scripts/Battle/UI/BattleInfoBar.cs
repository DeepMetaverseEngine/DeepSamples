using Assets.Scripts;
using DeepCore.GameData.Zone;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoBar : MonoBehaviour
{
    public Slider spriteHP;
    public Image hpBar;
    public Image hpFrame;
    public GameObject hpCtrl;
    public Text showName;
    public Image titleBg;
    public Image left;
    public Image right;
    public Image top;
    public Image npc;
    public Image hptype;

    public Sprite[] hpBarSprites;
    public Sprite[] monstertypeSprites;
    public Sprite[] pvpSprites;
    public Sprite[] carriageSprites;
    public Sprite revengeSprite;
    public Sprite ownershipSprite;

    private RectTransform mTrans;
    public Transform Parent { get; set; }

    //缩放比例(分3个档位).
    public float[] zoomSize = { 1.7f, 1f, 0.5f };

    public Vector3 LastPos { get; set; }
    public Vector3 Offset { get; set; }

    //名字label
    public Text LabelText { get; set; }
    private int mTitleId = 0;
    private int mTitleResId = 0;
    private int mTitleResHeight = 0;
    private bool mIsShowTitle = true;
    private bool mIsShowName = true;
    private int mHideNpcSignCount = 0;
    //private int mNpcFunId = -1;
    private int mNpcSignId = -1;
    private int mCurrentTileId = -1;

    public Camera GameCamera { get; set; }

    public bool mNeedShowHpBar { get; set; } //总开关，优先级最高
    private bool mShowHpCtrl = false;   //根据条件显示，之前是进入战斗状态，目前是选中状态，优先级低于mNeedShowHpBar
    private int mHideHpCount = 0;       //其他临时条件控制显示，比如死亡，剧情等，优先级最低

    private string mNameStr, mNameColor, mGuildStr, mGuildColor, mTitleStr;
    //private string mShowNameStr, mShowLvStr, mShowUplvStr, mShowGuildStr;
    private Text mMeasureText;
    private int mPLv;

    public const int FontSizeDefault = 28;
    public const int FontSizeMonster = 28;

    // Use this for initialization

    public enum HPBarType : byte
    {
        GREEN = 0,
        RED = 1,
        YELLOW = 2,
        GRAY = 3,
    }

    public enum MonsterType : byte
    {
        Null,
        Elite,
        Boss,
    }


    void Awake()
    {
        mTrans = GetComponent<RectTransform>();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        LabelText.gameObject.SetActive(active);
    }

    public void Remove()
    {
        //回收对象，放入缓存节点下.
        gameObject.SetActive(true);
        spriteHP.gameObject.SetActive(true);
        mHideHpCount = 0;
        mHideNpcSignCount = 0;
        mCurrentTileId = -1;
        //mNpcFunId = -1;
        mNpcSignId = -1;
        mShowHpCtrl = false;
        BattleInfoBar infoBar = gameObject.GetComponent<BattleInfoBar>();
        infoBar.Parent = null;
        this.gameObject.transform.SetParent(BattleInfoBarManager.mCacheNode);
        TextLabel.Remove(LabelText);
        SetTitle(0);
        SetGuild(null);
        SetPractice(0);
        SetPVPIcon(0);
        SetTopIcon(null, false);
        HideNpcFlag(true);
        SetMonsterType(MonsterType.Null);
        mNameStr = mNameColor = null;
        mIsShowName = true;
        if (this == BattleInfoBarManager.ActorInfoBar)
        {
            BattleInfoBarManager.ActorInfoBar = null;
        }
    }

    private bool CheckCamera()
    {
        bool result = false;
        if (!GameCamera || !GameCamera.isActiveAndEnabled || (GameCamera.cullingMask & 1 << Parent.gameObject.layer) == 0)
        {
            GameCamera = UITools.FindCameraForLayer(Parent.gameObject.layer);
            result = GameCamera != null;
        }
        else
        {
            result = true;
        }
        return result;
    }

    public bool ShowHpCtrl
    {
        get
        {
            return mShowHpCtrl;
        }
        set
        {
            mShowHpCtrl = value;
            RefreshByConfig();
        }
    }

    public void RefreshByConfig()
    {
        ////血条
        //if (GameSetting.GetValue(GameSetting.BLOOD) == 2) //按默认规则显示
        //{
        if (hpCtrl.activeSelf != mShowHpCtrl)
        {
            hpCtrl.SetActive(mShowHpCtrl);
        }
        //}
        //else //按照设置显示
        //{
        //    mHPCtrl.SetActive(this == BattleInfoBarManager.ActorInfoBar ? true : (GameSetting.GetValue(GameSetting.BLOOD) == 0 ? false : true));
        //}

        ////名字
        //if (GameSetting.GetValue(GameSetting.NAME) == 2) //按默认规则显示
        //{
        //    HideName(!mShowHpCtrl);
        //}
        //else //按照设置显示
        //{
        //    HideName(this == BattleInfoBarManager.ActorInfoBar ? false : (GameSetting.GetValue(GameSetting.NAME) == 0 ? true : false));
        //}

        ////称号
        //if (GameSetting.GetValue(GameSetting.TITLE) == 2) //按默认规则显示
        //{
        //    HideTitle(!mShowHpCtrl);
        //}
        //else //按照设置显示
        //{
        //    HideTitle(GameSetting.GetValue(GameSetting.TITLE) == 0 ? true : false);
        //}

        ////公会
        //if (GameSetting.GetValue(GameSetting.GUILD) == 2) //按默认规则显示
        //{
        //    HideGuild(!mShowHpCtrl);
        //}
        //else //按照设置显示
        //{
        //    HideGuild(GameSetting.GetValue(GameSetting.GUILD) == 0 ? true : false);
        //}
    }

    public void HideAll(bool hide)
    {
        this.gameObject.SetActive(!hide);
        HideName(hide);
    }

    public void HideHp(bool hide)
    {
        mHideHpCount = hide ? mHideHpCount + 1 : mHideHpCount - 1;
        mHideHpCount = System.Math.Max(0, mHideHpCount);
        hide = mHideHpCount > 0;

        if (hide || mNeedShowHpBar)
        {
            hpFrame.gameObject.SetActive(!hide);
        }
        else if (!mNeedShowHpBar)
        {
            hpFrame.gameObject.SetActive(false);
        }
    }

    public void SetHP(float cur, float max)
    {
        if (spriteHP != null && max > 0)
        {
            if (!hpBar.gameObject.activeSelf)
                hpBar.gameObject.SetActive(true);
            spriteHP.value = cur / max;
            if (cur > 0 && !spriteHP.gameObject.activeSelf)
                spriteHP.gameObject.SetActive(true);
            else if (cur <= 0 && spriteHP.gameObject.activeSelf)
                spriteHP.gameObject.SetActive(false);
        }
    }

    public void SetName(string name, uint rgba = 0)
    {
        if (LabelText)
        {
            this.gameObject.name = name;

            mNameStr = name;
            if (rgba != 0)
                mNameColor = rgba.ToString("x8");
            RefresahTextLabel();
        }
    }

    public void SetName(string name, int quality)
    {
        if (LabelText)
        {
            uint rgba = GameUtil.GetQualityColorRGBA(quality);
            SetName(name, rgba);
            //TextLabel.SetColor(LabelText, GameUtil.RGBA2Color(rgba));
        }
    }

    public void SetGuild(string name, bool isEnemy = false)
    {
        if (LabelText)
        {
            mGuildStr = name;
            mGuildColor = isEnemy ? "fb1919ff" : "82f72dff";
            RefresahTextLabel();
        }
    }

    private void RefresahTextLabel()
    {
        string text = "";
        if (mIsShowName && !string.IsNullOrEmpty(mTitleStr))
        {
            text = string.Format("<color=#b430e5>{0}</color>\n", mTitleStr);
        }
        if (mIsShowName && !string.IsNullOrEmpty(mGuildStr))
        {
            text = string.Format("{0}<color=#{1}>{2}</color>\n", text, mGuildColor, mGuildStr);
        }
        if (mIsShowName && !string.IsNullOrEmpty(mNameStr))
        {
            text = string.Format("{0}<color=#{1}>{2}</color>", text, mNameColor, mNameStr);
        }
        TextLabel.SetText(LabelText, text);
        RefresahTitlePos();
        RefreshTopNodePos();
    }

    public void SetForce(byte force, UnitInfo.UnitType type, bool isUser, bool isDead)
    {
        if (isUser)
        {
            if (isDead)
            {
                hpBar.gameObject.SetActive(false);
            }
            else
            {
                hpBar.gameObject.SetActive(true);
                SetHPBarType(HPBarType.GREEN);
                SetName(mNameStr, GameUtil.ARGB_To_RGBA(0xff4dd3ff));
                TextLabel.SetShadow(LabelText, GameUtil.ARGB2Color(0xff000000));
                TextLabel.SetSize(LabelText, FontSizeDefault);
            }
        }
        else if (force == DataMgr.Instance.UserData.Force || force == 0)
        {
            if (isDead)
            {
                hpBar.gameObject.SetActive(false);
            }
            else
            {
                hpBar.gameObject.SetActive(true);
                SetHPBarType(HPBarType.GREEN);
                SetName(mNameStr, GameUtil.ARGB_To_RGBA(force == 0 ? 0xff6dff55 : 0xff4dd3ff));
                TextLabel.SetShadow(LabelText, GameUtil.ARGB2Color(0xff000000));
                TextLabel.SetSize(LabelText, FontSizeDefault);
            }
        }
        else
        {
            if (isDead)
            {
                hpBar.gameObject.SetActive(false);
            }
            else
            {
                hpBar.gameObject.SetActive(true);
                SetHPBarType(HPBarType.RED);
                SetName(mNameStr, GameUtil.ARGB_To_RGBA(0xffff5858));
                TextLabel.SetShadow(LabelText, GameUtil.ARGB2Color(0xff000000));
                TextLabel.SetSize(LabelText, FontSizeDefault);
            }
        }
    }

    public void SetHPBarType(HPBarType type)
    {
        hpBar.sprite = hpBarSprites[(byte)type];
        hpBar.type = Image.Type.Tiled;
        mTrans.sizeDelta = new Vector2(hpBar.sprite.rect.width, hpBar.sprite.rect.height);
    }

    public void SetMonsterType(MonsterType type)
    {
        if (type == MonsterType.Null)
        {
            hptype.gameObject.SetActive(false);
        }
        else
        {
            hptype.sprite = monstertypeSprites[(byte)(type-1)];
            hptype.type = Image.Type.Simple;
            hptype.rectTransform.sizeDelta = new Vector2(hptype.sprite.rect.width, hptype.sprite.rect.height);
            hptype.gameObject.SetActive(true);
        }
    }

    public void SetPractice(int pLv)
    {
        mPLv = pLv;
        if (pLv == 0)
        {
            left.gameObject.SetActive(false);
        }
        else
        {
            left.gameObject.SetActive(true);
            GameUtil.ConvertToUnityUISpriteFromAtlas(left, "static/TL_hud/output/TL_hud.xml", "TL_hud", "practice_" + pLv);
            left.type = Image.Type.Simple;
            //left.rectTransform.sizeDelta = new Vector2(left.sprite.rect.width, left.sprite.rect.height);
            float nameW = MeasureNameWidth();
            left.rectTransform.anchoredPosition = new Vector2(-nameW / 2, left.rectTransform.anchoredPosition.y);
        }
    }

    public void SetPVPIcon(int force)
    {
        if (force == 2 || force == 3)
        {
            right.gameObject.SetActive(true);
            right.sprite = pvpSprites[force - 2];
            float nameW = MeasureNameWidth();
            right.rectTransform.anchoredPosition = new Vector2(nameW / 2, right.rectTransform.anchoredPosition.y);
        }
        else
        {
            right.gameObject.SetActive(false);
        }
    }

    public void SetRevenge(bool isRevenge)
    {
        if (isRevenge)
        {
            right.gameObject.SetActive(true);
            right.sprite = revengeSprite;
            float nameW = MeasureNameWidth();
            right.rectTransform.anchoredPosition = new Vector2(nameW / 2, right.rectTransform.anchoredPosition.y);
        }
        else
        {
            right.gameObject.SetActive(false);
        }
    }

    public void SetChaseOrder(bool flag)
    {
        SetTopIcon(carriageSprites[3], flag);
    }

    public void SetCarriage(int force)
    {
        SetTopIcon(carriageSprites[force - 2], force == 2 || force == 3);
    }

    public void SetMyCarriage(bool show)
    {
        SetTopIcon(carriageSprites[2], show);
    }

    public void ShowOwnership(bool show)
    {
        SetTopIcon(ownershipSprite, show);
    }

    private void SetTopIcon(Sprite sprite, bool isShow)
    {
        if (top.sprite != null)
        {
            if (sprite == null || (top.sprite == sprite && !isShow))
            {
                top.gameObject.SetActive(false);
                top.sprite = null;
            }
            else if(sprite != null && isShow)
            {
                top.gameObject.SetActive(true);
                top.sprite = sprite;
                RefreshTopNodePos();
            }
        }
        else if(sprite != null && isShow)
        {
            top.gameObject.SetActive(true);
            top.sprite = sprite;
            RefreshTopNodePos();
        }
    }

    public void SetTitle(int titleId, string ext = "")
    {
        if (titleId > 0)
        {
            var dbtt = GameUtil.GetDBData2("title", string.Format("{{ title_id = {0} }}", titleId));
            ClearTitleRes();
            mTitleStr = "";
            mTitleId = titleId;
            string wordStr = dbtt[0]["word_res"].ToString();
            string resPath = dbtt[0]["effect_res"].ToString();
            int type = System.Convert.ToInt32(dbtt[0]["title_type"]);
            if (!string.IsNullOrEmpty(wordStr))
            {
                if (type == 3)
                    mTitleStr = HZLanguageManager.Instance.GetFormatString(wordStr, ext);
                else
                {
                    string showStr = HZLanguageManager.Instance.GetString(wordStr);
                    if (showStr != wordStr)
                        mTitleStr = showStr;
                }
            }
            if(!string.IsNullOrEmpty(resPath))
            {
                var transSet = new TransformSet();
                transSet.Pos = Vector3.zero;// new Vector3(0, 0, -5);
                transSet.Scale = Vector3.one;
                transSet.Parent = titleBg.transform;
                transSet.Layer = (int)PublicConst.LayerSetting.FuckUI;
                //transSet.LayerOrder = 1500;
                mTitleResId = RenderSystem.Instance.PlayEffect(resPath, transSet);
                mTitleResHeight = int.Parse(dbtt[0]["title_high"].ToString());
            }
        }
        else
        {
            ClearTitleRes();
            mTitleStr = "";
        }
        RefreshTitleVisible();
    }

    private void ClearTitleRes()
    {
        if (mTitleResId != 0)
        {
            RenderSystem.Instance.Unload(mTitleResId);
            mTitleResId = 0;
            mTitleResHeight = 0;
        }
    }

    public void HideTitle(bool hide)
    {
        mIsShowTitle = !hide;
        RefreshTitleVisible();
    }

    public void HideName(bool hide)
    {
        mIsShowName = !hide;
        RefresahTextLabel();
        if(mPLv > 0 && !hide)
            left.gameObject.SetActive(true);
        else
            left.gameObject.SetActive(false);
    }

    private void RefreshTitleVisible()
    {
        bool isActive = mTitleId != 0 && mTitleResId != 0 && mIsShowTitle;
        if (titleBg.gameObject.activeSelf != isActive)
            titleBg.gameObject.SetActive(isActive);
        RefresahTextLabel();
    }

    private void RefresahTitlePos()
    {
        if (mTitleId != 0)
        {
            RectTransform trans = titleBg.rectTransform;
            RectTransform lbTrans = showName.transform as RectTransform;
            Vector2 pos = trans.anchoredPosition;
            pos.y = lbTrans.anchoredPosition.y + TextLabel.GetTextHeight(LabelText) * LabelText.rectTransform.localScale.y + 2;
            trans.anchoredPosition = pos;
        }
    }

    private void RefreshTopNodePos()
    {
        RectTransform trans = top.rectTransform;
        RectTransform lbTrans = showName.transform as RectTransform;
        Vector2 pos = trans.anchoredPosition;
        pos.y = lbTrans.anchoredPosition.y + TextLabel.GetTextHeight(LabelText) * LabelText.rectTransform.localScale.y + mTitleResHeight + 2;
        trans.anchoredPosition = pos;
    }

    private float MeasureNameWidth()
    {
        string text = LabelText.text;
        LabelText.text = mNameStr;
        float w = LabelText.preferredWidth * LabelText.rectTransform.localScale.x;
        LabelText.text = text;
        return w;
    }


    public void HideNpcFlag(bool hide)
    {
        if (npc == null) return;
        mHideNpcSignCount = hide ? mHideNpcSignCount + 1 : mHideNpcSignCount - 1;
        mHideNpcSignCount = System.Math.Max(0, mHideNpcSignCount);
        hide = mHideNpcSignCount > 0 || ( mNpcSignId == -1);

        if (npc.gameObject.activeSelf != !hide)
        {
            npc.gameObject.SetActive(!hide);
        }
        //int tileid = mNpcSignId != -1 ? mNpcSignId : mNpcFunId;
        if (!hide &&mCurrentTileId != mNpcSignId)
        {
            mCurrentTileId = mNpcSignId;
            GameUtil.ConvertToUnityUISpriteFromAtlas(npc, "static/TL_hud/output/TL_hud.xml", "TL_hud", mCurrentTileId);
            npc.SetNativeSize();
            npc.transform.localScale = Vector3.one * 0.6f;
        }
    }
    public void ShowNPCQuest(int tileId)
    {
        mNpcSignId = tileId;
        if (mNpcSignId == -1)
        {
            HideNpcFlag(true);
            return;
        }
        HideNpcFlag(false);
    }

}
