using UnityEngine;
using System.Collections;
using DeepCore.Unity3D.Impl;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class HZUITouchHandler : FingerHandlerInterface, PinchHandlerInterface
{

    private UnityDriver mUnityDriver;

    public delegate void OnPinchMoveHandler(Vector2 fingerPos1, Vector2 fingerPos2, float delta);
    public event OnPinchMoveHandler OnPinchMoveEvent;

    //private bool mTouchValid;
    private int mFingerCount;
    private int[] touchIds;

    public HZUITouchHandler(UnityDriver unityDriver)
    {
        touchIds = new int[5];
        for (int i = 0; i < touchIds.Length; i++)
            touchIds[i] = 0;
        mUnityDriver = unityDriver;
        GameGlobal.Instance.FGCtrl.AddFingerHandler(this, (int)PublicConst.FingerLayer.HZUI);
        GameGlobal.Instance.FGCtrl.AddPinchHandler(this, (int)PublicConst.FingerLayer.HZUI);
    }

    public bool OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        //GameDebug.Log("HZUITouchHandler.OnFingerDown");

        if (fingerIndex >= touchIds.Length)
            return mFingerCount > 0;
        
        if (CheckUITouch(fingerPos))
        {
            touchIds[fingerIndex] = 1;
            mFingerCount++;
            return true;
        }
        
        return mFingerCount > 0;
    }

    public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (fingerIndex >= touchIds.Length)
            return false;
        //GameDebug.Log("HZUITouchHandler.OnDragMove");
        return touchIds[fingerIndex] > 0;
        //return mFingerIndex == fingerIndex;
    }

    public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        if (fingerIndex >= touchIds.Length)
            return false;
        

        bool oldDown = touchIds[fingerIndex] > 0;
        
        touchIds[fingerIndex] = 0;
        mFingerCount--;

        return oldDown;
    }

    public void OnFingerClear()
    {
        for (int i = 0; i < touchIds.Length; i++)
            touchIds[i] = 0;
        mFingerCount = 0;
    }

    #region FingerGestures_Pinch
    public bool OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        return mFingerCount > 0;
    }

    public bool OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        if (mFingerCount == 0)
            return false;
        
        if (OnPinchMoveEvent != null)
        {
            OnPinchMoveEvent(fingerPos1, fingerPos2, delta);
        }
        return true;
    }

    public bool OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        return mFingerCount > 0;
    }

    private bool CheckUITouch(Vector2 fingerPos)
    {
        //直接选中UI控件
        if (EventSystem.current.currentSelectedGameObject != null)
            return true;

        //UI控件被AB里的东西覆盖的情况
        var result = new List<RaycastResult>();
        var pointData = new PointerEventData(EventSystem.current);
        pointData.position = fingerPos;
        EventSystem.current.RaycastAll(pointData, result);
        if (result.Count > 0)
        {
            float t = Time.realtimeSinceStartup;
            for (int i = 0; i < result.Count; ++i)
            {
                var trans = result[i].gameObject.transform;
                while (trans != null)
                {
                    if (trans.GetComponentInParent<DeepCore.Unity3D.UGUI.DisplayNodeBehaviour>() != null)
                    {
                        return true;
                    }
                    trans = trans.parent;
                }
            }
        }

        return false;
    }

    #endregion

    public void Destroy()
    {
        GameGlobal.Instance.FGCtrl.RemoveFingerHandler(this);
        GameGlobal.Instance.FGCtrl.RemovePinchHandler(this);
    }

}
