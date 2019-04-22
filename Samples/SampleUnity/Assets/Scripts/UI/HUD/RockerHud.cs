
using UnityEngine;
using System.Collections;

public class RockerHud : MonoBehaviour, FingerHandlerInterface, PinchHandlerInterface
{

    public Camera uiCamera;
    public RectTransform vaildArea = null;
    public GameObject rocker;
    public GameObject backGround;
    public GameObject rubber;
    public float rockerRange = 90.0f;
    //public float rubberLen = 1.3f;    需求改了，暂时不要.
    public bool canMove = true;
    public bool showImmediately = true;
    public float alphaMin = 0.5f;

    public delegate void OnRockerMoveEvent(float dx, float dy, float px, float py);
    public delegate void OnRockerStopEvent(float dx, float dy, float px, float py);
    public event OnRockerMoveEvent OnRockerMove;
    public event OnRockerStopEvent OnRockerStop;

    private Vector3 mDefaultPos = Vector3.zero;

    //private float X, Y;
    private RectTransform mBgTrans;
    private RectTransform mRockerTrans;
    //底盘半径（世界坐标系）.
    public float Radius { get; private set; }
    //底盘半径（屏幕坐标系）.
    private float mRadiusScn;
    //可拖动范围.
    private Rect mMoveArea;

    private bool mInitFinish;

    //摇杆头的可移动范围.
    //private const float RANGE = 110;

    //用来控制alpha值的一个状态标识.
    private int mRockerShow = 1;
    private CanvasGroup mCanvasGroup;

    private Vector2 mFingerPos;
    private int mRockerFingerIndex = -1;

    public bool HasFingerIndex
    {
        get
        {
            return mRockerFingerIndex >= 0;
        }
    }
    private enum Type
    {
        Begin,
        Move,
        End,
        Reset
    }

    void Start()
    {
        if (!mInitFinish)
        {
            if (uiCamera != null)
            {
                mDefaultPos = backGround.transform.position;
                mDefaultPos = uiCamera.WorldToScreenPoint(mDefaultPos);
                mDefaultPos = new Vector3(mDefaultPos.x, mDefaultPos.y, 0);
                mRockerTrans = rocker.GetComponent<RectTransform>();
                mBgTrans = backGround.GetComponent<RectTransform>();
                Radius = mBgTrans.rect.width * 0.5f;
                mRadiusScn = Radius * UGUIMgr.Scale;
                //X = bgTrans.anchoredPosition.x * scale;
                //Y = bgTrans.anchoredPosition.y * scale;
                //mMoveArea = this.GetComponent<RectTransform>().rect;
                //mMoveArea = new Rect(mMoveArea.x * scale, mMoveArea.y * scale, mMoveArea.width * scale, mMoveArea.height * scale);
                mMoveArea = new Rect(0, 0, Screen.width * 0.5f, Screen.height);

                mInitFinish = true;
            }

            mCanvasGroup = this.GetComponent<CanvasGroup>();
            gameObject.SetActive(showImmediately);
        }
    }

    public void ChangeDefaultPos(Vector3 pos)
    {
        RectTransform trans = (RectTransform)backGround.transform;
        trans.anchoredPosition = pos;
        mDefaultPos = trans.position;
        mDefaultPos = uiCamera.WorldToScreenPoint(mDefaultPos);
        mDefaultPos = new Vector3(mDefaultPos.x, mDefaultPos.y, 0);
    }

    private bool TryToMove(Vector2 pos2D, Type type, out Vector2 outPos)
    {
        if ((mInitFinish && gameObject.activeSelf) || type == Type.Reset)
        {
            Vector3 pos = Vector3.zero;

            float x = 0;
            float y = 0;

            if (pos2D != Vector2.zero)
            {
                if (type == Type.Begin)
                {
                    if (!UGUIMgr.CheckInRect(vaildArea, pos2D, true))
                    {
                        outPos = Vector2.zero;
                        return false;
                    }
                    else
                    {
                        pos.x = pos2D.x;
                        pos.y = pos2D.y;

                        pos = mDefaultPos;
                        mBgTrans.localPosition = ConvertToLocalPosition(transform, pos);

                        x = 0;
                        y = 0;
                    }
                }
                else
                {
                    pos.x = pos2D.x;
                    pos.y = pos2D.y;

                    Vector3 rockerPos = ConvertToLocalPosition(mBgTrans, pos);
                    //摇杆位置.
                    if (rockerPos.magnitude > rockerRange)
                        mRockerTrans.localPosition = rockerPos.normalized * rockerRange;
                    else
                        mRockerTrans.localPosition = rockerPos;

                    mRockerShow = 2;

                    if (canMove)
                    {
                        //底盘位置.
                        Vector3 p = pos;
                        if (p.x < mMoveArea.x)
                        {
                            p.x = mMoveArea.x;
                            mRockerShow = 0;
                        }
                        else if (p.x > mMoveArea.x + mMoveArea.width)
                        {
                            p.x = mMoveArea.x + mMoveArea.width;
                            mRockerShow = 0;
                        }
                        if (p.y < mMoveArea.y)
                        {
                            p.y = mMoveArea.y;
                            mRockerShow = 0;
                        }
                        else if (p.y > mMoveArea.y + mMoveArea.height)
                        {
                            p.y = mMoveArea.y + mMoveArea.height;
                            mRockerShow = 0;
                        }
                        if (rockerPos.magnitude > Radius)
                        {
                            Vector3 r = rockerPos.normalized * mRadiusScn;
                            mBgTrans.localPosition = ConvertToLocalPosition(transform, p - r);
                        }
                    }

                    //返回以1为半径的圆的轨迹的坐标.
                    x = mRockerTrans.localPosition.x / rockerRange;
                    y = mRockerTrans.localPosition.y / rockerRange;
                }
            }
            else
            {
                if (canMove)
                {
                    mBgTrans.localPosition = ConvertToLocalPosition(transform, mDefaultPos);
                }
                mRockerTrans.localPosition = Vector3.zero;
            }

            UpdateRubber();

            outPos = new Vector2(x, y);
            return true;
        }
        else
        {
            outPos = Vector2.zero;
            return false;
        }
    }

    private Vector3 ConvertToLocalPosition(Transform parent, Vector3 pos)
    {
        Vector3 p = parent.InverseTransformPoint(uiCamera.ScreenToWorldPoint(pos));
        return new Vector3(p.x, p.y, 0);
    }

    private void UpdateRubber()
    {
        rubber.SetActive(!mRockerTrans.localPosition.Equals(Vector3.zero));

        //float percentage = Vector2.Distance(Vector2.zero, mRockerTrans.localPosition) / rockerRange;
        //float scaleY = rubberLen * percentage;
        //rubber.transform.localScale = new Vector3(rubber.transform.localScale.x, scaleY, rubber.transform.localScale.z);
        float angle = Mathf.Atan2(-mRockerTrans.localPosition.x, mRockerTrans.localPosition.y) * Mathf.Rad2Deg;
        rubber.transform.localRotation = Quaternion.Euler(new Vector3(rubber.transform.localRotation.x, rubber.transform.localRotation.y, angle));
    }

    private void UpdateAlpha()
    {
        if (mCanvasGroup != null)
        {
            if (mRockerShow == 2)
            {
                if (mCanvasGroup.alpha < 1)
                {
                    mCanvasGroup.alpha += 0.1f;
                    if (mCanvasGroup.alpha > 1)
                        mCanvasGroup.alpha = 1;
                }
            }
            else if (mRockerShow == 1)
            {
                if (mCanvasGroup.alpha > alphaMin)
                {
                    mCanvasGroup.alpha -= 0.033f;
                    if (mCanvasGroup.alpha <= alphaMin)
                        mCanvasGroup.alpha = alphaMin;
                }
                else if (mCanvasGroup.alpha < alphaMin)
                {
                    mCanvasGroup.alpha += 0.033f;
                    if (mCanvasGroup.alpha > alphaMin)
                        mCanvasGroup.alpha = alphaMin;
                }
            }
            else
            {
                if (mCanvasGroup.alpha > 0)
                {
                    mCanvasGroup.alpha -= 0.1f;
                    if (mCanvasGroup.alpha <= 0)
                        mCanvasGroup.alpha = 0;
                }
            }
        }
    }

    void Update()
    {
        UpdateAlpha();
        if (mRockerFingerIndex != -1)
        {
            Vector2 posV2;
            if (TryToMove(mFingerPos, Type.Move, out posV2))
            {
                if (OnRockerMove != null)
                {
                    OnRockerMove(posV2.x, posV2.y, mFingerPos.x, mFingerPos.y);
                }
            }
        }
    }

    public void Reset(bool doRockerStop = false, float x = 0, float y = 0, float x1 = 0, float y1 = 0)
    {
        if (mRockerFingerIndex != -1)
        {
            mRockerFingerIndex = -1;
            mFingerPos = Vector2.zero;
            Vector2 v;
            this.TryToMove(Vector2.zero, Type.Reset, out v);
            mRockerShow = 1;
            rubber.SetActive(false);
            if (doRockerStop)
            {
                if (OnRockerStop != null)
                {
                    OnRockerStop(x, y, x1, y1);
                }
            }
        }
    }

    public bool Visible
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
        }
    }

    public bool OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        //关闭主菜单.
        EventManager.Fire("Event.Menu.CloseFuncEntryMenu", EventManager.defaultParam);
        if (mRockerFingerIndex == -1)
        {
            Vector2 pos;
            if (TryToMove(fingerPos, Type.Begin, out pos))
            {
                mRockerFingerIndex = fingerIndex;
                mFingerPos = fingerPos;
                return true;
            }
        }
        return false;
    }

    public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (mRockerFingerIndex != -1 && mRockerFingerIndex == fingerIndex)
        {
            mFingerPos = fingerPos;

            //不在屏幕内时，摇杆归位.
            if (!FingerControlBase.IsInScreen(fingerPos))
            {
                Reset(true);
            }

            return true;
        }
        return false;
    }

    public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        if (mRockerFingerIndex != -1 && mRockerFingerIndex == fingerIndex)
        {
            Vector2 pos;
            TryToMove(mFingerPos, Type.End, out pos);
            Reset(true, pos.x, pos.y, fingerPos.x, fingerPos.y);
            return true;
        }
        return false;
    }

    public void OnFingerClear()
    {
        Reset();
    }

    #region FingerGestures_Pinch
    public bool OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        return mRockerFingerIndex != -1;
    }

    public bool OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        return mRockerFingerIndex != -1;
    }

    public bool OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        return mRockerFingerIndex != -1;
    }

    #endregion

    public void Clear(bool reLogin, bool reConnect)
    {
        Reset();
        OnRockerMove = null;
        OnRockerStop = null;
    }

}
