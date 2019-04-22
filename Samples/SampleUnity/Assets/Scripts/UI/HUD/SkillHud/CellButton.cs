using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TL.UGUI.Skill
{

    public class CellButton : MonoBehaviour, FingerHandlerInterface
    {

        public Image icon = null;
        public RectTransform[] vaildArea = null;
        public int layerOffset = 0;         //触摸层级，默认是0，数字越高优先级越高

        public Vector2 DefaultPos { get; set; }
        public Vector2 ShowSize { get; set; }

        protected string mName;
        protected RectTransform mParentTrans;

        private int mFingerIndex = -1;
        private static Dictionary<int, int> GlobalFingerIndex = new Dictionary<int, int>();

        protected ButtonTouchListener mTouchListener;
        private bool mStartLongPressed;
        private float mLongPressedStartTime;
        private const float LongPressedTimeMax = 1.5f;

        protected virtual void Awake()
        {
            mParentTrans = GetComponent<RectTransform>().parent as RectTransform;
            mName = mParentTrans.parent.name;
            DefaultPos = mParentTrans.anchoredPosition;
            ShowSize = mParentTrans.sizeDelta;
        }

        protected virtual void Start()
        {
            InitVaildArea();
            GameGlobal.Instance.FGCtrl.AddFingerHandler(this, (int)PublicConst.FingerLayer.SKillBar + layerOffset);
        }

        private void InitVaildArea()
        {
            if (vaildArea == null || vaildArea.Length == 0)
            {
                vaildArea = new RectTransform[] { mParentTrans };
            }
        }

        public void SetTouchListener(ButtonTouchListener touchListener)
        {
            this.mTouchListener = touchListener;
        }

        public virtual void SetIcon(string iconName)
        {
            if (string.IsNullOrEmpty(iconName))
            {
                icon.enabled = false;
            }
            else
            {
                icon.enabled = true;
                GameUtil.ConvertToUnityUISpriteFromAtlas(icon, iconName);
            }
        }

        public bool IsInBtnArea(Vector2 pos)
        {
            if (vaildArea != null && vaildArea.Length > 0)
            {
                for (int i = 0; i < vaildArea.Length; ++i)
                {
                    if (UGUIMgr.CheckInRect(vaildArea[i], pos))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool OnFingerDown(int fingerIndex, Vector2 fingerPos)
        {
            if (!this.gameObject.activeInHierarchy)
                return false;

            if (mFingerIndex == -1)
            {
                if (!GlobalFingerIndex.ContainsKey(fingerIndex) && IsInBtnArea(fingerPos))
                {
                    mStartLongPressed = true;
                    mLongPressedStartTime = 0;

                    mFingerIndex = fingerIndex;
                    GlobalFingerIndex[fingerIndex] = fingerIndex;
                    return OnTouchDown(fingerPos);
                }
            }

            return false;
        }

        public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
        {
            if (!this.gameObject.activeInHierarchy)
                return false;

            if (mFingerIndex == fingerIndex)
            {
                return OnTouchMove(fingerPos);
            }
            return false;
        }

        public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
        {
            if (!this.gameObject.activeInHierarchy)
                return false;

            if (mFingerIndex == fingerIndex)
            {
                mStartLongPressed = false;
                if (mLongPressedStartTime >= LongPressedTimeMax)
                    return true;

                mFingerIndex = -1;
                GlobalFingerIndex.Remove(fingerIndex);
                return OnTouchUp(fingerPos);
            }
            return false;
        }

        public void OnFingerClear()
        {
            mFingerIndex = -1;
            GlobalFingerIndex.Clear();
        }

        protected virtual void OnDestroy()
        {
            if (GameGlobal.Instance != null)
            {
                GameGlobal.Instance.FGCtrl.RemoveFingerHandler(this);
            }
        }

        protected virtual bool OnTouchDown(Vector2 fingerPos)
        {
            if (mTouchListener != null)
                mTouchListener.OnButtonDown(mParentTrans.gameObject, fingerPos);
            return true;
        }

        protected virtual bool OnTouchMove(Vector2 fingerPos)
        {
            if (mTouchListener != null)
                mTouchListener.OnButtonDrag(mParentTrans.gameObject, fingerPos);
            return true;
        }

        protected virtual bool OnTouchUp(Vector2 fingerPos)
        {
            if (mTouchListener != null)
                mTouchListener.OnButtonUp(mParentTrans.gameObject, fingerPos);
            return true;
        }

        void Update()
        {
            if (mStartLongPressed)
            {
                mLongPressedStartTime += Time.deltaTime;
                if (mLongPressedStartTime >= LongPressedTimeMax)
                {
                    mStartLongPressed = false;
                    if (mTouchListener != null)
                        mTouchListener.OnButtonLongPressed(mParentTrans.gameObject);
                }
            }
        }

    }

    public interface ButtonTouchListener
    {
        void OnButtonDown(GameObject obj, Vector2 pos);
        void OnButtonDrag(GameObject obj, Vector2 pos);
        void OnButtonUp(GameObject obj, Vector2 pos);
        void OnButtonLongPressed(GameObject obj);
    }

}
