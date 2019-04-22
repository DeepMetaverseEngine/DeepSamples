using DeepCore.Unity3D;
using UnityEngine;

public class UIFollowTarget3D : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = Vector3.zero;
    public float aoiDistance;
    public Transform gameCamera;

    private Transform mTrans;
    private Vector3 mLastTargetPos, mLastCameraPos, mLastCameraRotation;

    private static readonly Vector3 HideCamera = new Vector3(100000, 100000, 100000);

    void Start()
    {
        if (gameCamera == null && Camera.main != null)
        {
            gameCamera = Camera.main.transform;
        }
        mTrans = GetComponent<Transform>();
    }

    void OnEnable()
    {
        mLastTargetPos = Vector3.zero;
        mLastCameraPos = Vector3.zero;
        mLastCameraRotation = Vector3.zero;
    }

    void LateUpdate()
    {
        if (CheckNeedUpdate())
        {
            //计算AOI 
            if(aoiDistance != -1)
            {
                float dis = Vector3.Distance(target.position, gameCamera.position);
                if (dis > aoiDistance)
                {
                    mTrans.position = HideCamera;
                    return;
                }
            }

            //同步位置 
            Vector3 pos = target.position + offset;
            mTrans.position = pos;
            
            //面朝屏幕 
            mTrans.eulerAngles = gameCamera.eulerAngles;

            mLastTargetPos = target.position;
            mLastCameraPos = gameCamera.position;
            mLastCameraRotation = gameCamera.eulerAngles;
        }
    }

    private bool CheckNeedUpdate()
    {
        if (!UnityHelper.IsObjectExist(target) || !UnityHelper.IsObjectExist(target.gameObject) || gameCamera == null)
        {
            mTrans.position = HideCamera;
            return false;
        }

        if (!mLastTargetPos.Equals(target.position))
            return true;
        if (!mLastCameraPos.Equals(gameCamera.position))
            return true;
        if (!mLastCameraRotation.Equals(gameCamera.eulerAngles))
            return true;
        return false;
    }


}
