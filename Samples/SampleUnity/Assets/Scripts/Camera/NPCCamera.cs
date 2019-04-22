using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCCamera : MonoBehaviour
{

    public Vector3 positionNear = new Vector3(0, 5f, -7.5f);
    public float angleNear = 30;
    public float fovNear = 50;
    public float moveSmoothTime = 0.15f;
    public float switchSmoothTime = 0.4f;

    public Transform positionNode = null;
    public Transform pitchNode = null;
    public Camera btCamera;

    private Transform mTrans;

    //相机变换中的值.
    private Vector3 mCurPosGlobal;
    private Vector3 mCurPosLocal;
    private float mCurAngle;
    private float mCurFov;
    //相机将要移动到的目标点.
    private Vector3 mEndPosGlobal;
    private Vector3 mEndPosLocal;
    private float mEndAngle;
    private float mEndFov;
    //相机移动的起始点.
    private Vector3 mStartPosGlobal;
    private Vector3 mStartPosLocal;
    private float mStartAngle;
    private float mStartFov;
    //最后一次相机稳定后的点.
    private Vector3 mLastPosLocal;
    private Vector3 mLastPosGlobal;
    private float mLastAngle;
    private float mLastFov;
    //参与平滑运算的变量.
    private float mVelocity;
    private float mSmoothValue;
    private float mSmoothFrame;
    //摄像机平滑移动的时间.
    private float mSmoothTime = 0.5f;

    private System.Action mSwitchCb;

    void Start()
    {
        mTrans = gameObject.transform;
        Reset();
    }

    public void Reset()
    {
        mEndPosGlobal = mCurPosGlobal = mTrans.localPosition;
        mEndPosLocal = mCurPosLocal = positionNode.localPosition;
        mEndAngle = mCurAngle = pitchNode.localEulerAngles.x;
        mEndFov = mCurFov = btCamera.fieldOfView;
    }

    //public void LateUpdate()
    public void UpdateCamera()
    {
        //平滑修正,自定义移动下防止抖动
        if (mSmoothValue >= 0.99f && mSmoothTime != moveSmoothTime && mSmoothTime != switchSmoothTime)
        {
            Reset();
        }

        //相机绝对坐标位移.
        if (mCurPosGlobal != mEndPosGlobal)
        {
            mCurPosGlobal = mStartPosGlobal + (mEndPosGlobal - mStartPosGlobal) * GetSmoothDamp();
            mTrans.localPosition = mCurPosGlobal;
           
        }

        //相机相对坐标位移.
        if (mCurPosLocal != mEndPosLocal)
        {
            mCurPosLocal = mStartPosLocal + (mEndPosLocal - mStartPosLocal) * GetSmoothDamp();
            positionNode.localPosition = mCurPosLocal;
        }

        if (mCurFov != mEndFov)
        {
            mCurFov = mStartFov + (mEndFov - mStartFov) * GetSmoothDamp();
            btCamera.fieldOfView = mCurFov;
        }

        //相机角度变换.
        if (mCurAngle != mEndAngle)
        {
            mCurAngle = mStartAngle + (mEndAngle - mStartAngle) * GetSmoothDamp();
            pitchNode.rotation = Quaternion.AngleAxis(mCurAngle, Vector3.right);
        }
        else
        {
            if (mSwitchCb != null)
            {
                mSwitchCb();
                mSwitchCb = null;
            }
        }
    }

    private float GetSmoothDamp()
    {
        if (mSmoothFrame != Time.frameCount)    //优化操作，确保每帧只做一次.
        {
            if (mSmoothValue > 0.99f)
            {
                mSmoothValue = 1;
            }
            else
            {
                mSmoothValue = Mathf.SmoothDamp(mSmoothValue, 1, ref mVelocity, mSmoothTime);
            }
            mSmoothFrame = Time.frameCount;
        }
        return mSmoothValue;
    }

    public void Move(Vector3 pos, bool isSmooth)
    {
        if (!isSmooth)
        {
            mTrans.localPosition = pos;
        }
        mEndPosGlobal = pos;

        mCurPosGlobal = mStartPosGlobal = mTrans.localPosition;

        if(mSwitchCb == null)
        {
            mSmoothValue = 0;
            //mVelocity = 0;
            mSmoothTime = moveSmoothTime;
        }
    }


    public void Move(Vector3 pos, Vector3 localPos, float angle, float fov, float smoothTimeSec)
    {
        if (float.IsNaN(smoothTimeSec))
        {
            mTrans.localPosition = pos;
        }
        else
        {
            mSmoothTime = smoothTimeSec;
        }
        mEndPosGlobal = pos;
        mEndPosLocal = localPos;
        mEndAngle = angle;
        mEndFov = fov;

        mCurPosGlobal = mStartPosGlobal = mTrans.localPosition;
        mCurPosLocal = mStartPosLocal = positionNode.localPosition;
        mCurAngle = mStartAngle = pitchNode.localEulerAngles.x;
        mCurFov = mStartFov = btCamera.fieldOfView;

        mSmoothValue = 0;
        mVelocity = 0;
    }

    public void SwitchToNPCShot(Vector3 pos, bool isSmooth, System.Action cb)
    {
        mLastPosGlobal = mStartPosGlobal = mTrans.localPosition;
        mLastPosLocal = mStartPosLocal = positionNode.localPosition;
        mLastAngle = mStartAngle = pitchNode.localEulerAngles.x;
        mLastFov = mStartFov = btCamera.fieldOfView;

        if (isSmooth)
        {
            mEndPosGlobal = pos;
            mEndPosLocal = positionNear;
            mEndAngle = angleNear;
            mEndFov = fovNear;

            mCurPosGlobal = mTrans.localPosition;
            mCurPosLocal = positionNode.localPosition;
            mCurAngle = pitchNode.localEulerAngles.x;
            mCurFov = btCamera.fieldOfView;

            mSmoothTime = switchSmoothTime;
        }
        else
        {
            mTrans.localPosition = mCurPosGlobal = mEndPosGlobal = pos;
            positionNode.localPosition = mCurPosLocal = mEndPosLocal = positionNear;
            mCurAngle = mEndAngle = angleNear;
            mCurFov = mEndFov = btCamera.fieldOfView;
            pitchNode.rotation = Quaternion.AngleAxis(mLastAngle, Vector3.right);
        }

        mSmoothValue = 0;
        mVelocity = 0;

        mSwitchCb = cb;
    }

    public void RecoverLastShot(bool isSmooth, System.Action cb)
    {
        if (isSmooth)
        {
            mEndPosGlobal = mLastPosGlobal;
            mEndPosLocal = mLastPosLocal;
            mEndAngle = mLastAngle;
            mEndFov = mLastFov;

            mCurPosGlobal = mStartPosGlobal = mTrans.localPosition;
            mCurPosLocal = mStartPosLocal = positionNode.localPosition;
            mCurAngle = mStartAngle = pitchNode.localEulerAngles.x;
            mCurFov = mStartFov = btCamera.fieldOfView;

            mSmoothTime = switchSmoothTime;
        }
        else
        {
            mTrans.localPosition = mCurPosGlobal = mEndPosGlobal = mLastPosGlobal;
            positionNode.localPosition = mCurPosLocal = mEndPosLocal = mLastPosLocal;
            mCurAngle = mEndAngle = mLastAngle;
            pitchNode.rotation = Quaternion.AngleAxis(mLastAngle, Vector3.right);
            btCamera.fieldOfView = mCurFov = mEndFov = mLastFov;
        }

        mSmoothValue = 0;
        mVelocity = 0;

        mSwitchCb = cb;
    }

}
