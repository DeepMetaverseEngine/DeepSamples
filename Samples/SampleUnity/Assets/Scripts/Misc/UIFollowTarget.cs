using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{

    public Transform target;

    public Camera gameCamera;
    public Camera uiCamera;

    public bool disableIfInvisible = true;

    public int depth = 0;

    public Vector3 offset = Vector3.zero;

    private Transform mTrans;
    protected bool mIsVisible = false;

    void Awake() { mTrans = transform; }


    void Start()
    {
        if (target != null)
        {
            if (gameCamera == null)
                gameCamera = UITools.FindCameraForLayer(target.gameObject.layer);
            if (uiCamera == null) uiCamera = UITools.FindCameraForLayer(gameObject.layer);
            SetVisible(false);
        }
        else
        {
            Debugger.LogError("Expected to have 'target' set to a valid transform", this);
            enabled = false;
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        if (target != null)
        {
            gameCamera = UITools.FindCameraForLayer(target.gameObject.layer);
            uiCamera = UITools.FindCameraForLayer(gameObject.layer);
            Update();
        }

    }

    void SetVisible(bool val)
    {
        mIsVisible = val;
        if(mTrans != null)
        mTrans.position = hideV3;

        //for (int i = 0, imax = mTrans.childCount; i < imax; ++i)
        //{
        //    UITools.SetActive(mTrans.GetChild(i).gameObject, val);
        //}
    }

    private bool CheckCamera()
    {
        bool result = false;
        if (!gameCamera || !gameCamera.isActiveAndEnabled || (gameCamera.cullingMask & 1 << target.gameObject.layer) == 0)
        {
            gameCamera = UITools.FindCameraForLayer(target.gameObject.layer);
            result = gameCamera != null;
        }
        else if (!uiCamera || !uiCamera.isActiveAndEnabled || (uiCamera.cullingMask & 1 << gameObject.layer) == 0)
        {
            uiCamera = UITools.FindCameraForLayer(gameObject.layer);
            result = uiCamera != null;
        }
        else
        {
            result = true;
        }
        return result;
    }

    private readonly Vector3 hideV3 = new Vector3(99999, 99999, 99999);
    void Update()
    {
        if (target == null || !CheckCamera())
        {
            SetVisible(false);
            return;
        }

        Vector3 pos = gameCamera.WorldToViewportPoint(target.position + offset);

        // Determine the visibility and the target alpha
        bool isVisible = gameCamera.enabled && (gameCamera.orthographic || pos.z > 0f) && (!disableIfInvisible || (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f));

        // Update the visibility flag
        if (mIsVisible != isVisible)
            SetVisible(isVisible);

        // If visible, update the position
        if (isVisible)
        {
            if(uiCamera != null && mTrans != null)
            {
                mTrans.position = uiCamera.ViewportToWorldPoint(pos);
            }
//             pos = mTrans.localPosition;
//             pos.x = Mathf.FloorToInt(pos.x);
//             pos.y = Mathf.FloorToInt(pos.y);
//             pos.z = 0f;
//             mTrans.localPosition = pos;
        }
        OnUpdate(isVisible);
    }

    public void Dispose()
    {
        target = null;
    }

    void OnEnable()
    {
        if (target != null)
            gameCamera = UITools.FindCameraForLayer(target.gameObject.layer);
    }

    /// <summary>
    /// Custom update function.
    /// </summary>

    protected virtual void OnUpdate(bool isVisible) { }
}
