using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UGUIMgr : MonoBehaviour {

    public RectTransform HudRoot;
    public GameObject rocker;
    public GameObject skill;
    //public BattleInfoBarManager infoBar;
    public GameObject textLabel;
    //public Navigate navigate;

    public GameObject extendUI;

    public UGUITouchRects touchRects;

    public const int SCREEN_WIDTH = 1136;
    public const int SCREEN_HEIGHT = 640;

    public static Vector2 Size { get; private set; }

    private static Dictionary<int, Rect> mRects = new Dictionary<int, Rect>();
    public static float Scale { get; private set; }
    public static Camera UGUICamera { get; private set; }
    public static UGUITouchRects TouchRects { get; private set; }
    public SkillBarHud SkillBar { get; private set; }
    public RockerHud Rock { get; private set; }

    public bool HasRockFingerIndex
    {
        get { return Rock.HasFingerIndex; }
    }

    public event Action<bool> OnRockFingerUse;

    void Awake () {
        //init scale info
        CanvasScaler cs = GetComponent<CanvasScaler>();
        float baseRatio = (float)SCREEN_WIDTH / (float)SCREEN_HEIGHT;
        float targetRatio = (float)Screen.width / (float)Screen.height;
        if (targetRatio < baseRatio)   //宽高比小于基准分辨率，缩放模式以宽度为基准.
        {
            cs.matchWidthOrHeight = 0;
            Scale = (float)Screen.width / (float)SCREEN_WIDTH;
            Size = new Vector2(SCREEN_WIDTH, Screen.height / Scale);
        }
        else   //宽高比大于基准分辨率，缩放模式以高度为基准.
        {
            cs.matchWidthOrHeight = 1;
            Scale = (float)Screen.height / (float)SCREEN_HEIGHT;
            Size = new Vector2(Screen.width / Scale, SCREEN_HEIGHT);
        }

        //ResetScreenOffset();

        //init component
        SkillBar = skill.GetComponent<SkillBarHud>();
        Rock = rocker.GetComponent<RockerHud>();

        //init camera
        Canvas canvas = GetComponent<Canvas>();
        GameObject uguiCameraObj = GameObject.Find("HZUGUI_Camera");
        if (uguiCameraObj != null)
        {
            canvas.worldCamera = uguiCameraObj.GetComponent<Camera>();
        }
        UGUICamera = canvas.worldCamera;
        TouchRects = touchRects;
    }

    public void ResetScreenOffset()
    {
        float iphoneXOffsetX = GameUtil.GetNotchX() / Scale;
        HudRoot.offsetMin = new Vector2(iphoneXOffsetX, 0);
        HudRoot.offsetMax = new Vector2(-iphoneXOffsetX, 0);
    }

    public static bool CheckInRect(RectTransform rectTrans, Vector2 fingerPos, bool forceCalculate = false)
    {
        Rect r = ConvertToScreenRect(rectTrans, forceCalculate);
        if (fingerPos.x > r.xMin && fingerPos.x < r.xMax && fingerPos.y > r.yMin && fingerPos.y < r.yMax)
        {
            return true;
        }

        return false;
    }

    private static Rect ConvertToScreenRect(RectTransform trans, bool forceCalculate)
    {
        Rect rect = new Rect();
        int key = trans.GetInstanceID();
        if (forceCalculate || !mRects.TryGetValue(key, out rect))
        {
            Vector3 v = UGUICamera.WorldToScreenPoint(trans.position);
            float x = v.x;
            float y = v.y;
            float w = trans.rect.width * Scale;
            float h = trans.rect.height * Scale;
            Vector2 piovt = trans.pivot;
            float rx = x - w * piovt.x;
            float ry = y - h * piovt.y;
            //转换成以左下为anchor的坐标系（Unity屏幕坐标系）
            rect = Rect.MinMaxRect(rx, ry, rx + w, ry + h);
            mRects[key] = rect;
        }

        return rect;
    }

    public void HideExtendUI(bool hide)
    {
        bool isShow = !hide;
        if (extendUI != null && extendUI.activeSelf != isShow)
        {
            extendUI.SetActive(isShow);
        }
    }

    private bool mLastHasFingerIndex;
	void Update () {
	    if (mLastHasFingerIndex != HasRockFingerIndex && OnRockFingerUse != null)
	    {
	        OnRockFingerUse.Invoke(HasRockFingerIndex);
	    }
	    mLastHasFingerIndex = HasRockFingerIndex;
    }

    private void OnDestroy()
    {
        OnRockFingerUse = null;
    }


}
