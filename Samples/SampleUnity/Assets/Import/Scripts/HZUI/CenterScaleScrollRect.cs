using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectMask2D))]
public class CenterScaleScrollRect : UIBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2 CellSize;
    public float MaxScale = 1.5f;
    public float Smooth = 0.1f;

    public int StartMoveDelta = 3;
    public float PageChangeNormalized = 0.25f;
    public event Action<int, RectTransform> OnPageChange;
    public event Action OnBeginMove;
    public event Action OnEndMove;


    private bool mMoving;
    private float mMovePointer;
    private Rect mCenterRect;
    private float mSpace;
    private int mShowPageCount;

    public int CurrentPage
    {
        get { return CalcScalePage(); }
    }

    public new RectTransform transform
    {
        get { return (RectTransform) base.transform; }
    }

    public void CalcPt(float p)
    {
        foreach (RectTransform t in transform)
        {
            t.anchoredPosition = new Vector2(p, t.anchoredPosition.y);
            float s = 1;

            float dx = t.anchoredPosition.x, ddx = t.anchoredPosition.x + CellSize.x * t.localScale.x;
            var leftIn = dx >= mCenterRect.xMin && dx <= mCenterRect.xMax;
            var rightIn = ddx >= mCenterRect.xMin && ddx <= mCenterRect.xMax;

            if (leftIn && rightIn)
            {
                s = MaxScale;
            }
            else if (leftIn)
            {
                s = MaxScale - (dx - mCenterRect.xMin) / mCenterRect.width * (MaxScale - 1);
            }
            else if (rightIn)
            {
                s = MaxScale - (mCenterRect.xMax - ddx) / mCenterRect.width * (MaxScale - 1);
            }

            t.localScale = new Vector3(s, s, s);
            p = p + CellSize.x * s + mSpace;
        }
    }

    private RectTransform FirstPage
    {
        get { return transform.childCount > 0 ? (RectTransform) transform.GetChild(0) : null; }
    }

    private RectTransform LastPage
    {
        get { return transform.childCount > 0 ? (RectTransform) transform.GetChild(transform.childCount - 1) : null; }
    }

    private int mOffsetPage;
    private int mLastPage;

    public void Initialize(int startPage = 0)
    {
        mLastPage = -1;
        var scaleS = CellSize.x * MaxScale;
        mShowPageCount = (int) Math.Floor((transform.sizeDelta.x - scaleS) / CellSize.x) + 1;
        if (mShowPageCount == 0)
        {
            throw new ArgumentException();
        }

        var tdf = transform.sizeDelta.x - scaleS - CellSize.x * (mShowPageCount - 1);
        if (mShowPageCount == 1)
        {
            mSpace = tdf;
        }
        else
        {
            mSpace = tdf / (mShowPageCount - 1);
        }

        var x = 0.5f * (transform.sizeDelta.x - scaleS);
        //todo centerRect 支持可调节位置
        mOffsetPage = mShowPageCount / 2;
        if (mShowPageCount % 2 != 0)
        {
            mCenterRect = new Rect(x, 0, scaleS, transform.sizeDelta.y);
            mOffsetPage++;
        }
        else
        {
            mCenterRect = new Rect((mOffsetPage - 1) * (CellSize.x + mSpace), 0, scaleS, transform.sizeDelta.y);
        }
        MoveTo(startPage, false);
    }

    public void MoveTo(int pageIndex, bool smooth)
    {
        if (pageIndex >= transform.childCount)
        {
            pageIndex = transform.childCount - 1;
        }
        else if (pageIndex < 0)
        {
            pageIndex = 0;
        }
        var d = CalcScalePageDistance(pageIndex);
        OnMoveElement(d, smooth);
    }

    public void MoveTo(int pageIndex)
    {
        MoveTo(pageIndex, true);
    }

    private bool CheckMovable(ref float delta)
    {
        if (transform.childCount == 0)
        {
            return false;
        }
        var nextMovePointer = mMovePointer + delta;
        if (!mMoving && Math.Abs(nextMovePointer) < StartMoveDelta)
        {
            return false;
        }
        //限制一次只能翻一页
        //if (Math.Abs(nextMovePointer) > CellSize.x)
        //{
        //    return false;
        //}
        var first = FirstPage;
        if (nextMovePointer > 0 && first.anchoredPosition.x >= mCenterRect.xMin)
        {
            return false;
        }

        var last = LastPage;
        if (nextMovePointer < 0 && last.anchoredPosition.x + CellSize.x <= mCenterRect.xMax)
        {
            return false;
        }
        return true;
    }


    private void OnMoveElement(float pt, bool smooth)
    {
        if (smooth)
        {
            mInSmooth = true;
            mStartSmooth = FirstPage.anchoredPosition.x;
            mTargetSmooth = pt;
        }
        else
        {
            CalcPt(pt);
            OnMoveEnd();
        }
    }

    private void OnMoveEnd()
    {
        if (!mMoving && !mInSmooth)
        {
            var page = CalcScalePage();

            if (OnPageChange != null && page != mLastPage)
            {
                OnPageChange.Invoke(page, (RectTransform) transform.GetChild(page));
            }
            if (mLastPage != -1)
            {
                if (OnEndMove != null)
                {
                    OnEndMove.Invoke();
                }
            }
            mLastPage = page;
            mMovePointer = 0;
        }
    }

    private float mStartSmooth;
    private float mTargetSmooth;
    private float mCurrentVelocity;
    private bool mInSmooth;

    private void Update()
    {
        if (!mInSmooth) return;
        mStartSmooth = Mathf.SmoothDamp(mStartSmooth, mTargetSmooth, ref mCurrentVelocity, Smooth);
        if (Math.Abs(mStartSmooth - mTargetSmooth) < 0.1)
        {
            CalcPt(mTargetSmooth);
            mInSmooth = false;
            OnMoveEnd();
        }
        else
        {
            OnMoveElement(mStartSmooth, false);
        }
    }

    protected override void OnDestroy()
    {
        OnPageChange = null;
        OnBeginMove = null;
        OnEndMove = null;
    }


    private int CalcScalePage()
    {
        return CalcPage(FirstPage.anchoredPosition.x) + mOffsetPage - 1;
    }

    private float CalcScalePageDistance(int pageIndex)
    {
        var pageMoved = pageIndex - mOffsetPage + 1;
        return CaclPageDistance(pageMoved);
    }

    private float CaclPageDistance(int pageIndex)
    {
        if (mShowPageCount == 1 && pageIndex == 0)
        {
            return mSpace * 0.5f;
        }
        var offset = mShowPageCount == 1 ? mSpace * 0.5f : 0;
        var target = -(mSpace + CellSize.x) * pageIndex + offset;
        return target;
    }

    private int CalcPage(float firstPageDistance)
    {
        var index = -firstPageDistance / (CellSize.x + mSpace);
        if (mMovePointer > 0)
        {
            if (index > 0)
            {
                index = index + PageChangeNormalized;
            }
            else
            {
                index = index - (1 - PageChangeNormalized);
            }
        }
        else if (mMovePointer < 0)
        {
            index = index + (1 - PageChangeNormalized);
        }
        if (index > transform.childCount - mOffsetPage)
        {
            index = transform.childCount - mOffsetPage;
        }
        if (index < -mOffsetPage + 1)
        {
            index = -mOffsetPage + 1;
        }
        return (int) index;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta.x;
        if (!CheckMovable(ref delta))
        {
            return;
        }
        mMovePointer += delta;
        if (!mMoving)
        {
            mMoving = true;
            if (OnBeginMove != null)
            {
                OnBeginMove.Invoke();
            }
        }
        OnMoveElement(FirstPage.anchoredPosition.x + delta, false);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        mMovePointer = 0;
        mMoving = false;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!mMoving) return;
        mMoving = false;
        var pageIndex = CalcPage(FirstPage.anchoredPosition.x);
        var d = CaclPageDistance(pageIndex);
        OnMoveElement(d, true);
    }

    //private void OnGUI()
    //{
    //    GUI.Button(mCenterRect, "abc");
    //}
}