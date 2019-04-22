using System.Collections.Generic;
using UnityEngine;

public class UIFollowPosition : MonoBehaviour
{

    public Transform target;
    public Vector3 position;

    public Camera gameCamera;
    public Camera uiCamera;

    public bool disableIfInvisible = true;

    public int depth = 0;

    public Vector3 offset = Vector3.zero;

    private Transform mTrans;
    protected bool mIsVisible = false;

    public delegate void OnUpdateFunc(GameObject o, bool is_visible);
    public OnUpdateFunc OnUpdate;

    void Awake() { mTrans = transform; }


    void Start()
    {
        if (target != null)
        {
            position = target.position;
            SetVisible(false);
        }
        else
        {
            //Debugger.LogError("Expected to have 'target' set to a valid transform", this);
            enabled = false;
        }
    }

    public void Reset(Transform t)
    {
        target = t;
        if (target)
        {
            position = target.position;
            OnEnable();
            Update();
        }
           
    }

    void SetVisible(bool val)
    {
        mIsVisible = val;
        //for (int i = 0, imax = mTrans.childCount; i < imax; ++i)
        //{
        //    UITools.SetActive(mTrans.GetChild(i).gameObject, val);
        //}
    }

    private bool CheckCamera()
    {
        bool result = false;
        if (!gameCamera || gameCamera.Equals(null) || !gameCamera.isActiveAndEnabled 
            || (GameUtil.IsObjectExists(target) && GameUtil.IsObjectExists(target.gameObject) && (gameCamera.cullingMask & 1 << target.gameObject.layer) == 0))
        {
            gameCamera = UITools.FindCameraForLayer(target.gameObject.layer);
            result = gameCamera != null;
        }
        else
        {
            result = true;
        }
        return result;
    }

    void Update()
    {
        if (target == null || uiCamera == null || !CheckCamera())
            return;

        Vector3 pos = gameCamera.WorldToViewportPoint(position + offset);

        // Determine the visibility and the target alpha
        bool isVisible = gameCamera.enabled && (gameCamera.orthographic || pos.z > 0f) && (!disableIfInvisible || (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f));

        // Update the visibility flag
        if (mIsVisible != isVisible) SetVisible(isVisible);

        // If visible, update the position
        if (isVisible)
        {
            transform.position = uiCamera.ViewportToWorldPoint(pos);
//             pos = mTrans.localPosition;
//             pos.x = Mathf.FloorToInt(pos.x);
//             pos.y = Mathf.FloorToInt(pos.y);
//             pos.z = 0f;
//             mTrans.localPosition = pos;
        }
        if(OnUpdate != null)
        {
            OnUpdate(gameObject, isVisible);
        }
    }

    void OnEnable()
    {
        if (target != null)
        {
            if (gameCamera == null)
                gameCamera = UITools.FindCameraForLayer(target.gameObject.layer);
            if (uiCamera == null)
                uiCamera = UITools.FindCameraForLayer(gameObject.layer);
        }
    }

    /// <summary>
    /// Custom update function.
    /// </summary>

    //protected virtual void OnUpdate(bool isVisible) { }
}
