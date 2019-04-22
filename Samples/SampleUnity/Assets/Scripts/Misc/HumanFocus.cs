using UnityEngine;
using System.Collections;

public class HumanFocus : MonoBehaviour
{
    public static GameObject FindChild(GameObject go, string name)
    {
        if (string.Compare(go.name, name, true) == 0)
            return go;

        for (int i = 0; i < go.transform.childCount; i++)
        {
            GameObject tmp = go.transform.GetChild(i).gameObject;
            tmp = FindChild(tmp, name);
            if (tmp != null)
            {
                return tmp;
            }
        }
        return null;
    }

    private Transform mChest;
    private Animator mAnimator;
    private Vector3 mFoucs;
    private Vector3 mTmp;

    public Vector3 Foucs
    {
        private get
        {
            return mFoucs;
        }

        set
        {
            if (value == Vector3.zero)
            {
                mFoucs = value;
                return;
            }

            //低头高度不低于胸部节点
            if (value.y < mChest.position.y)
            {
                value.y = mChest.position.y;
            }

            //水平转向不大于90度
            var tmp = value;
            tmp.y = mChest.position.y;
            var target = tmp - transform.position;
            var angle = Vector3.Angle(transform.forward, target);
            if (angle > 90)
            {
                var angle2 = Vector3.Angle(transform.right, target);
                if (angle2 > 90)
                {
                    var left = Quaternion.AngleAxis(-60, Vector3.up) * transform.forward;
                    var tmp3 = transform.position + left * target.magnitude;
                    value.x = tmp3.x;
                    value.z = tmp3.z;
                }
                else
                {
                    var right = Quaternion.AngleAxis(60, Vector3.up) * transform.forward;
                    var tmp3 = transform.position + right * target.magnitude;
                    mTmp = tmp3;
                    value.x = tmp3.x;
                    value.z = tmp3.z;
                }
            }
            //抬头高度不大于60度
            //tmp = value;
            //tmp.x = transform.position.x;
            //angle = Vector3.Angle(transform.forward, tmp -)

            mFoucs = value;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (mTmp != Vector3.zero)
        {
            Gizmos.DrawLine(transform.position, mTmp);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100);
    }

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        var go = FindChild(gameObject, "Bip001 Spine1");
        if (go != null)
        {
            mChest = go.transform;
        }

        if (mChest == null)
        {
            Debugger.LogError("NO <Bip001 Spine1> DUMMY " + this.gameObject.name);
            mChest = this.transform;
        }
    }

    void OnAnimatorIK(int layer)
    {
        if (Foucs != Vector3.zero)
        {
            mAnimator.SetLookAtWeight(1f, 0.5f);
            mAnimator.SetLookAtPosition(Foucs);
        }
    }
}