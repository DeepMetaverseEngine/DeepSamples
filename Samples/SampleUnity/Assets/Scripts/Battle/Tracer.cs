using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Tracer : MonoBehaviour {
    private Transform target = null;
    float elps;
    public enum MoveType { Time, Speed };
    public enum AccMode { Average, X3 };

    public delegate void FinishCallback();

    //// Use this for initialization
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void Drop(Vector3 tar, float duration,FinishCallback callback = null)
    {
        StartCoroutine(Curve(transform, transform.position, tar, duration, callback));
    }

    public IEnumerator Curve(Transform thisTransform, Vector3 startPos, Vector3 endPos, float value, FinishCallback callback = null)
    {
        List<Vector3> vs = new List<Vector3>();
        var rate = 0.75f / value;
        float t = 0.0f;
        //float arg = 10;
        float arg = 30;

      
        while (t < 1.0f)
        {
            //if(!target) break;
            t += Time.deltaTime * 1.5f;
            float res = t;
            {
                Vector3 pos = Vector3.Lerp(startPos, endPos, t);
               
                //float add = Mathf.Sqrt(-arg*t*t*t+arg*t);
                //if(float.IsNaN(add))
                //    add = 0;
                //pos.y += add;
                //Debugger.LogError("StartCoroutine pos:" + pos);
                thisTransform.position = pos;
 

                vs.Add(pos);
            }

            yield return 0;

        }

       

        if (callback != null)
        {
            //绘制轨迹.
            for (int i = 0; i < vs.Count - 2; i++)
            {
                Debug.DrawLine(vs[i], vs[i + 1], Color.red, 200);
            }

            callback();
        }

       

    }

    public void Go(AccMode acc, float e, FinishCallback callback = null)
    {
        elps = e;
        StartCoroutine(Translation(transform, transform.position, target.position, elps, MoveType.Time, acc, callback));
    }

    public IEnumerator Translation(Transform thisTransform, Vector3 startPos, Vector3 endPos, float value, MoveType moveType,
        AccMode accMode, FinishCallback callback)
    {
        var rate = (moveType == MoveType.Time) ? 1.0f / value : 1.0f / Vector3.Distance(startPos, endPos) * value;
        float t = 0.0f;
        while (t < (float)1.0f)
        {
            //if(!target) break;
            t += Time.deltaTime * rate;
            float res = t;
            if(accMode == AccMode.X3)
            {
                res = t * t * t;
            }
            if (target)
            {
                thisTransform.position = Vector3.Lerp(startPos, target.position, res);
                endPos = target.position;
            }
            else
            {
                thisTransform.position = Vector3.Lerp(startPos, endPos, res);
            }


            yield return 0;
        }

        if (callback != null)
        {
            callback();
        }
        //Destroy(gameObject);
    }
    public IEnumerator Translation(Transform thisTransform, Vector3 endPos, float value, MoveType moveType, 
        AccMode accMode, FinishCallback callback)
    {
        yield return Translation(thisTransform, thisTransform.position, endPos, value, moveType, accMode, callback);
    }

    public IEnumerator Rotation(Transform thisTransform, Vector3 degrees, float value, MoveType moveType)
    {
        Vector3 old_forward = transform.forward;
        var startRotation = thisTransform.rotation;
        var endRotation = Quaternion.LookRotation(degrees);
        var rate = 0.0f;
        if (moveType == MoveType.Time)
        {
            rate = 1.0f / value;
        }
        else
        {
            float angle = Vector3.Angle(old_forward, degrees);
            rate = value / angle;
        }
        float t = 0.0f;
        while (t < (float)1.0f)
        {
            t += Time.deltaTime * rate;
            Quaternion r = Quaternion.Slerp(startRotation, endRotation, t);
            //if(!r.Equals(thisTransform.rotation))
            thisTransform.rotation = r;
            yield return 0;
        }
    }
}
