using System;
using DeepCore.Unity3D.Utils;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class SceneCamera : MonoBehaviour, FingerHandlerInterface, PinchHandlerInterface, ICamera,IObserver<SettingData>
{

    public enum EaseType
    {
        Linear,
        Circle
    }
    public struct CameraArgument
    {
        public Vector2[] pos;
        public float[] agl;
        public float[] fov ;
        public float zoomSpeed;
        public float followDeltaY;               //跟随Y轴偏移
        public EaseType easeType;
        public bool freeZoom;                     //是否无极缩放

        public CameraArgument Clone()
        {
            var ret = (CameraArgument)MemberwiseClone();
            ret.pos = new Vector2[pos.Length];
            ret.agl = new float[agl.Length];
            ret.fov = new float[fov.Length];

            Array.Copy(pos, ret.pos, pos.Length);
            Array.Copy(agl, ret.agl, agl.Length);
            Array.Copy(fov, ret.fov, fov.Length);
            return ret;
        }
    }

    //相机参数.
    public Vector2[] pos = { new Vector2(4f, -10f), new Vector2(10f, -14f), new Vector2(13f, -13.8f) };
    public float[] agl = { 20, 35, 43 };
    public float[] fov = { 30, 40, 45 };
    public float zoomSpeed = 8;
    public float followDeltaY = 0.84f;               //跟随Y轴偏移
    public EaseType easeType = EaseType.Circle;
    public int defaultZoom = 1;
    public bool freeZoom = true;                     //是否无极缩放

    public Transform followTarget;                  //指定摄像机的跟随目标

    public Transform positionNode = null;
    public Transform pitchNode = null;
    private Weather weather = null;
    public Camera mainCamera;

    private BlurEffect mBlurEffect;
    //public bool forceRefresh = false;       //debug用，正式版本关掉

    //数组下标索引枚举值.
    private const int N = 0;
    private const int M = 1;
    private const int F = 2;

    //圆弧算法相关变量.
    private Vector2 mCenter;    //圆心
    private float mR;       //半径
    private float[] mZoom;  //圆弧上点的轨迹

    //档位切换相关变量.
    private int mZoomEndIndex;
    private int mZoomStartIndex;
    private float mDelta;
    private float mZoomCur;
    private float mZoomStart;
    private float mZoomEnd;
    private float mAglStart;
    private float mAglEnd;
    private float mFovStart;
    private float mFovEnd;

    private float mZoomAmount = 0;

    private NPCCamera mNpcCamera;
    private Vector3 mLastMovePos;

    private bool mRotation180;
    public float FollowOffsetY { get; set; }

    private int mFingerIndex = -1;
    private bool mPinchState = false;

    //return 0-1 value
    public delegate void ZoomEventHandler(float zoom);
    private event ZoomEventHandler ZoomEvent;

    private CameraState State { get; set; }

    public BlurEffect BlurEffect { get { return mBlurEffect; } }

    public Vector3 SourceOffset { get; private set; }
    public Vector3 SourceAngle { get; private set; }
    public CameraArgument SourceArgument { get; private set; }
    public CameraArgument CurrentArgument { get; private set; }
    private enum CameraState
    {
        Battle,
        NPC,
        Custom,
    }

    void Awake()
    {
        SourceOffset = positionNode.localPosition;
        SourceAngle = pitchNode.localEulerAngles;
        if (weather == null)
        {
            weather = this.gameObject.GetComponentInChildren<Weather>();
        }
        SourceArgument = new CameraArgument
        {
            pos = new Vector2[pos.Length],
            agl = new float[agl.Length],
            fov = new float[fov.Length],
            followDeltaY = followDeltaY,
            freeZoom = freeZoom,
            zoomSpeed = zoomSpeed
        };
        Array.Copy(pos, SourceArgument.pos, pos.Length);
        Array.Copy(agl, SourceArgument.agl, agl.Length);
        Array.Copy(fov, SourceArgument.fov, fov.Length);
        
        
     
        
    }
    
    void Start()
    {
        // mainCamera.depthTextureMode = DepthTextureMode.Depth;
        mainCamera.depthTextureMode = DepthTextureMode.None;
        mainCamera.transform.localPosition = Vector3.zero;

        ////小物件单独culling距离
        //var layer = Assets.Scripts.Setting.ProjectSetting.LAYER_SMALLITEM;
        //float[] distances = new float[32];
        //distances[layer] = 50;
        //mainCamera.layerCullDistances = distances;
        //mainCamera.layerCullSpherical = true;

        mNpcCamera = this.gameObject.GetComponent<NPCCamera>();
        mBlurEffect = mainCamera.GetComponent<BlurEffect>();
        mBlurEffect.enabled = false;
        GameGlobal.Instance.FGCtrl.AddFingerHandler(this, (int)PublicConst.FingerLayer.CameraLayer);
        GameGlobal.Instance.FGCtrl.AddPinchHandler(this, (int)PublicConst.FingerLayer.CameraLayer);
        
        if (followTarget != null)
        {
            transform.localPosition = followTarget.localPosition;
        }
        InitParam(pos);
        mZoomStartIndex = N;
        mZoomEndIndex = M;
       
    }

    void OnDestroy ()
    {
       
    }
    //PostProcessLayer 跟着设置走
    private void SetNewBloom(bool isShow)
    {
        if (mainCamera != null)
        {
            var postlayer = mainCamera.GetComponentInChildren<PostProcessLayer>(true);
            var postvol = mainCamera.GetComponentInChildren<PostProcessVolume>(true);
            postlayer.enabled = isShow;
            postvol.enabled = isShow;
        }
    }
    public void SetCameraArgument(CameraArgument arg)
    {
        pos = arg.pos;
        agl = arg.agl;
        fov = arg.fov;
        followDeltaY = arg.followDeltaY;
        freeZoom = arg.freeZoom;
        zoomSpeed = arg.zoomSpeed;
        InitParam(pos);
        CurrentArgument = arg;
    }

    public void SetCameraOffset(Vector3 offset)
    {
        positionNode.localPosition = offset;
    }

    public void SetCameraAngle(Vector3 angle)
    {
        pitchNode.localEulerAngles = angle;
    }

    public void SetCameraLocation(Vector3 p, Vector3 angle, Vector3 scale)
    {
        transform.localPosition = p;
        transform.localEulerAngles = angle;
        transform.localScale = scale;
    }

    public Tweener DoMoveTo(Vector3 targetPos, float duration)
    {
        var tweener = transform.DOLocalMove(targetPos, duration);
        return tweener;
    }

    public Tweener DoRotate(Vector3 targetPos, float duration)
    {
        var tweener = transform.DOLocalRotate(targetPos, duration);
        return tweener;
    }

    public Tweener DoMoveToAndSet(Vector3 targetPos, float duration)
    {
        var tweener = positionNode.transform.DOLocalMove(targetPos, duration);
        return tweener;
    }

    public Tweener DoRotateAndSet(Vector3 targetPos, float duration)
    {
        var tweener = pitchNode.transform.DOLocalRotate(targetPos, duration);
        return tweener;
    }

    public void Reset()
    {
        SetCameraOffset(SourceOffset);
        SetCameraAngle(SourceAngle);
        SetCameraArgument(SourceArgument);

        State = CameraState.Battle;
        this.enabled = true;
        mLastMovePos = Vector3.zero;
        Rotate180(false);

        mNpcCamera.Reset();
        DataMgr.Instance.SettingData.AttachObserver(this);
        
        int bloom = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.BLOOM);
        SetNewBloom(bloom == 1);
    }

    public void Clear()
    {
        mLastRaycast.Clear();
        mExpiredRaycast.Clear();
        if (weather != null)
        {
            weather.Clear();
        }
        DataMgr.Instance.SettingData.DetachObserver(this);
       
    }

    private void InitParam(Vector2[] point)
    {
        if (point == null || point.Length < 3)
        {
            Debugger.LogError("少于3个点，不能构成圆！");
            return;
        }

        Vector2 A = point[N];
        Vector2 B = point[M];
        Vector2 C = point[F];
        float x, y;
        float a = A.x;
        float b = A.y;
        float c = B.x;
        float d = B.y;
        float e = C.x;
        float f = C.y;

        if (A.Equals(B) || A.Equals(C) || C.Equals(B))
        {
            Debugger.LogError("3点两两重合，不能构成圆！");
            return;
        }

        if (easeType == EaseType.Circle && Mathf.Atan2(B.y - A.y, B.x - A.x) == Mathf.Atan2(C.y - A.y, C.x - A.x))
        {
            //Debugger.LogError("3点在同一直线上，强制使用线性模式！");
            easeType = EaseType.Linear;
        }

        if (easeType == EaseType.Circle)
        {
            //求出圆心坐标.
            x = ((f - d) * (a * a + b * b) + (b - f) * (c * c + d * d) + (d - b) * (e * e + f * f)) / (2 * (b * c + a * f - c * f - b * e - a * d + d * e));
            if (b != d)
                y = (a * a + b * b - c * c - d * d - 2 * a * x + 2 * c * x) / (2 * (b - d));
            else
                y = (a * a + b * b - e * e - f * f - 2 * a * x + 2 * e * x) / (2 * (b - f));
            mCenter = new Vector2(x, y);
            //Debugger.Log("x = " + x + ", y = " + y);

            //求半径.
            float r = Mathf.Sqrt((x - a) * (x - a) + (y - b) * (y - b));
            mR = r;
            //Debugger.Log("r = " + r);

            //求ABC相对于圆心的坐标.
            Vector2 origin = new Vector2(x, y);
            Vector2[] p = new Vector2[pos.Length];
            for (int i = 0; i < pos.Length; ++i)
            {
                p[i] = CoordinateConvert(Vector2.zero, origin, point[i]);
                //Debugger.Log("p[" + i + "]=" + p[i]);
            }

            //求起始点的角度.
            mZoom = new float[pos.Length];
            for (int i = 0; i < pos.Length; ++i)
            {
                mZoom[i] = Mathf.Atan2(p[i].y, p[i].x) * Mathf.Rad2Deg;
                mZoom[i] = (mZoom[i] + 360) % 360;
                //Debugger.Log("mZoom[" + i + "]=" + mZoom[i]);
            }

            //根据变化角度求范围内点的轨迹(调试用，别删掉).
            //float len = mZoom[pos.Length-1] - mZoom[0];
            //float density = 10;
            //float delta = len / density;
            //for (int i = 1; i <= density; ++i)
            //{
            //    float radian = (mZoom[0] + i * delta) * Mathf.Deg2Rad;
            //    float px = Mathf.Cos(radian) * r;
            //    float py = Mathf.Sin(radian) * r;
            //    Debugger.Log("x=" + px + " , y=" + py);
            //}
        }
        else if (easeType == EaseType.Linear)
        {
            mZoom = new float[pos.Length];
            for (int i = 0; i < pos.Length; ++i)
            {
                mZoom[i] = Vector2.Distance(pos[0], pos[i]);
            }
        }
    }

    private Vector2 CoordinateConvert(Vector2 origin_src, Vector2 origin_dst, Vector2 point)
    {
        Vector2 result = new Vector2();
        float offsetX = origin_src.x - origin_dst.x;
        float offsetY = origin_src.y - origin_dst.y;
        result.x = point.x + offsetX;
        result.y = point.y + offsetY;
        return result;
    }

    private float ZoomAmount
    {
        get { return mZoomAmount; }
        set
        {
            mZoomAmount = Mathf.Clamp(value, Mathf.Min(mZoomStart, mZoomEnd), Mathf.Max(mZoomStart, mZoomEnd));
            float ratio = (mZoomAmount - mZoomStart) / (mZoomEnd - mZoomStart);

            float angle;
            float fov;
            if (mZoomStart == mZoomEnd)
            {
                angle = mAglEnd;
                fov = mFovEnd;
            }
            else
            {
                angle = mAglStart + ratio * (mAglEnd - mAglStart);
                fov = mFovStart + ratio * (mFovEnd - mFovStart);
            }

            pitchNode.rotation = Quaternion.Euler(angle, mRotation180 ? 180 : 0, 0);
            mainCamera.fieldOfView = fov;

            Vector2 pos = Vector2.zero;
            if (mZoomStart == mZoomEnd)
            {
                pos = this.pos[mZoomStartIndex];
            }
            else
            {
                if (easeType == EaseType.Circle)
                {
                    //圆弧运动
                    float radian = mZoomAmount * Mathf.Deg2Rad;
                    float px = Mathf.Cos(radian) * mR;
                    float py = Mathf.Sin(radian) * mR;
                    pos = CoordinateConvert(mCenter, Vector2.zero, new Vector2(px, py));
                }
                else if (easeType == EaseType.Linear)
                {
                    //线性运动
                    pos.x = this.pos[mZoomStartIndex].x + ratio * (this.pos[mZoomEndIndex].x - this.pos[mZoomStartIndex].x);
                    pos.y = this.pos[mZoomStartIndex].y + ratio * (this.pos[mZoomEndIndex].y - this.pos[mZoomStartIndex].y);
                }
            }

            positionNode.localPosition = new Vector3(0, pos.x, pos.y);
            SendZoomEventMessage();
        }
    }

    /// <summary>
    /// 设置相机档位
    /// </summary>
    /// <param name="zoom">近中远 012</param>
    /// <returns></returns>
    public bool SwitchZoom(int zoom)
    {
        if (zoom != mZoomEndIndex)
        {
            if (zoom >= 0 && zoom < mZoom.Length)
            {
                return SwitchZoom(mZoomEndIndex, zoom);
            }
        }
        return false;
    }

    public bool SwitchZoom(bool nearToFar)
    {
        if (nearToFar)
        {
            if(mZoomStartIndex >= mZoomEndIndex || mZoomAmount == mZoomEnd)
            {
                if (mZoomEndIndex < mZoom.Length - 1)
                    return SwitchZoom(mZoomEndIndex, mZoomEndIndex + 1);
            }
            else
                return SwitchZoom(mZoomStartIndex, mZoomEndIndex);
        }
        else
        {
            if (mZoomStartIndex <= mZoomEndIndex || mZoomAmount == mZoomEnd)
            {
                if (mZoomEndIndex > 0)
                    return SwitchZoom(mZoomEndIndex, mZoomEndIndex - 1);
            }
            else
                return SwitchZoom(mZoomStartIndex, mZoomEndIndex);
        }
        return false;
    }

    private bool SwitchZoom(int start, int end, bool immediately = false)
    {
        //Debugger.Log("--------SwitchZoom--------- " + start + ", " + end);
        if (start < 0 || start >= pos.Length || end < 0 || end >= pos.Length)
            return false;

        mZoomStartIndex = start;
        mZoomEndIndex = end;

        mZoomStart = mZoom[start];
        mZoomEnd = mZoom[end];
        mAglStart = agl[start];
        mAglEnd = agl[end];
        mFovStart = fov[start];
        mFovEnd = fov[end];

        if (immediately)
        {
            ZoomAmount = mZoomEnd;
            mDelta = 0;
        }
        else
        {
            mDelta = mZoomStart < mZoomEnd ? zoomSpeed : -zoomSpeed;
            mDelta = easeType == EaseType.Linear ? mDelta * 0.3f : mDelta;
        }

        return true;
    }

    public void Rotate180(bool rotate180)
    {
        if (rotate180 != mRotation180)
        {
            mRotation180 = rotate180;
            for (int i = 0; i < pos.Length; ++i)
            {
                pos[i].y *= -1;
            }
            InitParam(pos);
        }
        if (freeZoom)
        {
            //default
            SwitchZoom(0, defaultZoom, true);
            //custom
            //float zoomValue = ((float)GameSetting.GetValue(GameSetting.VISUAL_RANGE)) / 1000.0f;
            //ZoomAmount = Mathf.Lerp(mZoom[F], mZoom[N], zoomValue);
        }
        else
        {
            int target = defaultZoom;// GameSetting.GetValue(GameSetting.VISUAL_RANGE);
            SwitchZoom(mZoomStartIndex, target, true);
        }
    }

    private void ChangeMat(GameObject o)
    {
        foreach (MaterialManager e in o.GetComponentsInChildren<MaterialManager>(true))
        {
            e.AddMatState(StateMaterial.COVER);
        }
    }

    private void RecoverMat(GameObject o)
    {
        foreach (MaterialManager e in o.GetComponentsInChildren<MaterialManager>(true))
        {
            e.RemoveMatState(StateMaterial.COVER);
        }
    }

    public void SetFollowTarget(Transform target, bool atonce = false)
    {
        followTarget = target;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        TryFollow(atonce);
    }

    void LateUpdate()
    {
        TryFollow(false);

        if (mDelta != 0)
        {
            ZoomAmount += mDelta;
            if (ZoomAmount == mZoomEnd)
            {
                mDelta = 0;
            }
        }
    }

    void FixedUpdate()
    {
        CheckCameraHit();

        //debug
        //if (forceRefresh)
        //{
        //    InitParam(pos);
        //}
    }

    //控制摄像机的跟随
    private void TryFollow(bool atonce)
    {
        mNpcCamera.UpdateCamera();
        if (followTarget != null)
        {
            Vector3 pos = followTarget.position;
            Vector3 movePos = new Vector3(pos.x, pos.y + followDeltaY + FollowOffsetY, pos.z);
            if (!Vector3.Equals(movePos, mLastMovePos))
            {
                mNpcCamera.Move(movePos, !atonce);
                mLastMovePos = movePos;
            }
        }
    }

    private HashSet<GameObject> mLastRaycast = new HashSet<GameObject>();
    private HashSet<GameObject> mExpiredRaycast = new HashSet<GameObject>();
    private void CheckCameraHit()
    {
        Vector3 origin = mainCamera.transform.position;
        Vector3 target = transform.position;
        //得到方向
        Vector3 direction = (target - origin).normalized;
        float distance = Vector3.Distance(target, origin);

        //在场景视图中可以看到这条射线
        Debug.DrawLine(origin, target, Color.red);
        
        if (mLastRaycast.Count > 0)
        {
            mExpiredRaycast.Clear();
            foreach (GameObject hit in mLastRaycast)
            {
                mExpiredRaycast.Add(hit);
            }
            mLastRaycast.Clear();
        }
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, distance, 1 << (int)PublicConst.LayerSetting.CAGE);
        if (hits != null)
        {
            for (int i = 0; i < hits.Length; ++i)
            {
                GameObject hit = hits[i].transform.gameObject;
                mLastRaycast.Add(hit);
                if (!mExpiredRaycast.Contains(hit))
                {
                    AutoFade fade = hit.transform.gameObject.GetComponent<AutoFade>();
                    if (fade == null)
                        fade = hit.transform.gameObject.AddComponent<AutoFade>();
                    else
                        fade.StartFade();
                }
                else
                {
                    mExpiredRaycast.Remove(hit);
                }
                Debug.DrawLine(origin, hits[i].point, Color.green);
            }
        }

        if (mExpiredRaycast.Count > 0)
        {
            foreach (GameObject hit in mExpiredRaycast)
            {
                AutoFade fade = hit.transform.gameObject.GetComponent<AutoFade>();
                fade.StopFade();
            }
            mExpiredRaycast.Clear();
        }
    }

    public void SwitchToNPCShot(Vector3 pos, bool isSmooth, System.Action cb = null)
    {
        State = CameraState.NPC;
        mNpcCamera.SwitchToNPCShot(pos, isSmooth, cb);
    }

    public void RecoverLastShot(bool isSmooth, System.Action cb = null)
    {
        if (State == CameraState.NPC)
        {
            State = CameraState.Battle;
            //mNpcCamera.RecoverLastShot(isSmooth, cb);
            mNpcCamera.RecoverLastShot(isSmooth, () =>
            {
                //mState = CameraState.Battle;
                if (cb != null)
                    cb();
            });
        }
        else
        {
            if (cb != null)
                cb();
        }
    }

    public bool OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        if (mFingerIndex == -1)
        {
            if (UGUIMgr.CheckInRect(UGUIMgr.TouchRects.battleCamera, fingerPos))
            {
                mFingerIndex = fingerIndex;
                return true;
            }
        }
        return false;
    }

    public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (mPinchState)
            return true;
        
        return mFingerIndex != -1;
    }

    public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        mFingerIndex = -1;
        return true;
    }

    public bool OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        if (UGUIMgr.CheckInRect(UGUIMgr.TouchRects.battleCamera, fingerPos1) && UGUIMgr.CheckInRect(UGUIMgr.TouchRects.battleCamera, fingerPos2))
        {
            mPinchState = true;
            return true;
        }
        return false;
    }

    bool PinchHandlerInterface.OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        //Debugger.Log("--------OnPinchMove--------- " + delta);
        if (State != CameraState.Battle)
        {
            return mPinchState;

        }
        //ZoomAmount += zoomSpeed * (delta >= 0 ? mDelta : -mDelta);
        if (freeZoom)
        {
            if (mDelta == 0)
            {
                if (Mathf.Abs(delta) < 2)
                {
                    mDelta = 0;
                }
                else if (delta > 0)
                {
                    //镜头拉近.
                    SwitchZoom(false);
                }
                else
                {
                    //镜头拉远.
                    SwitchZoom(true);
                }
            }
            else
            {
                if (delta * mDelta > 0) //方向相反，重新确定档位.
                {
                    SwitchZoom(delta <= 0);
                }
            }
        }
        else
        {
            if (ZoomAmount == mZoomEnd || mDelta == 0) //相机处于静止状态，尝试切换档位.
            {
                if (delta > 0)
                {
                    //镜头拉近.
                    SwitchZoom(false);
                }
                else
                {
                    //镜头拉远.
                    SwitchZoom(true);
                }
            }
            else //相机处于运动状态，修正方向.
            {
                if (delta * mDelta > 0) //方向相反，重新确定档位.
                {
                    SwitchZoom(delta <= 0);
                }
            }
        }
        return mPinchState;
    }

    public bool OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        bool pinchState = mPinchState;
        if (pinchState)
        {
            mPinchState = false;
            if(freeZoom)
                mDelta = 0;
            int zoomValue = (int)(Mathf.InverseLerp(mZoom[F], mZoom[N], ZoomAmount) * 1000);
            //GameSetting.SetValue(GameSetting.VISUAL_RANGE, zoomValue);
        }

        return pinchState;
    }

    public void OnFingerClear()
    {
        mFingerIndex = -1;
        mPinchState = false;
    }

    public void AddZoomEventListener(ZoomEventHandler observer)
    {
        ZoomEvent += observer;
        SendZoomEventMessage();
    }

    public void RemoveZoomEventListener(ZoomEventHandler observer)
    {
        ZoomEvent -= observer;
    }

    private void SendZoomEventMessage()
    {
        if (ZoomEvent != null && mZoom != null)
        {
            float zoom = Mathf.InverseLerp(Mathf.Min(mZoom[0], mZoom[mZoom.Length - 1]), Mathf.Max(mZoom[0], mZoom[mZoom.Length - 1]), mZoomAmount);
            ZoomEvent(zoom);
        }
    }

    public void RotateToWithFocus2D(Vector3 forward) { }
    public void RotateWithActorDirection(Vector3 forward) { }

    public bool Notify(long status, SettingData subject)
    {
        //throw new NotImplementedException();
        if ((status & (long)global::SettingData.NotifySettingState.BLOOM) != 0)
        {
            int bloom = DataMgr.Instance.SettingData.GetAttribute(SettingData.NotifySettingState.BLOOM);
            SetNewBloom(bloom == 1);
        }
        return true;
    }

    public void BeforeDetach(SettingData subject)
    {
        //throw new NotImplementedException();
    }

    public void BeforeAttach(SettingData subject)
    {
        //throw new NotImplementedException();
    }
}