using DeepCore.Unity3D.Utils;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using UnityEngine;
using DeepCore.Unity3D;


public class FlyToActor : MonoBehaviour
{ 
    private bool start = false;

    private Vector3 startPosition;


    private int TotalHeight;
    private static Transform target;
    private Vector3 targetPos;

    private float value = 0;

    private float t = 0.0f;
    private float ty = 0.0f;
    private float ty2 = 0.0f;

    private float HHHH = 0;

    void Update()
    {
        if (start)
        {
            if (TLBattleScene.Instance == null 
                || TLBattleScene.Instance.Actor == null 
                || TLBattleScene.Instance.Actor.ObjectRoot == null)
            {
                finishCB.Invoke();
                return;
            }

            if (target == null)
            { 
                target = TLBattleScene.Instance.Actor.ObjectRoot.transform;
            }

            targetPos = target.position;

            var HeadTransform = TLBattleScene.Instance.Actor.HeadTransform;
            if (HeadTransform == null)
            {
                finishCB.Invoke();
                return;
            }

            HHHH = HeadTransform.position.y - target.position.y;


            //ZoneObject ZObj = target.ZActor;

            //TotalHeight = (int)(ZObj.Parent.TerrainSrc.TotalHeight - 0.5f);

            //TotalHeight = TLBattleScene.Instance.TotalHeight;
            //targetPos = Extensions.ZonePos2UnityPos(TotalHeight
            //         , target.ZObj.X, target.ZObj.Y, target.ZObj.Z);


            //var dis = Vector3.Distance(transform.position, targetPos + bodySize);

            if (t <= 1)
            {
                t += Time.deltaTime * 1f;


                if (ty <= 0.5f)
                {
                    ty += Time.deltaTime;
                    float t2 = ty * 2.2f;
                    value = Mathf.Lerp(0, 90, t2);
                }
                else
                {
                    ty2 += Time.deltaTime * 2.5f;
                    value = Mathf.Lerp(90, 135, ty2);
                }
 
                Vector3 pos = Vector3.Lerp(startPosition, targetPos, t);

                pos.y = startPosition.y  + HHHH * Mathf.Sin(Mathf.Deg2Rad * value);

 
                transform.position = pos;
 
                transform.localScale = Vector3.one * (1 - Mathf.InverseLerp(0.6f, 1.0f, t));
            }
            else
            {
                start = false;
                finishCB.Invoke();
            }
 
        }
    }

 
    public delegate void FinishCallback();

    private FinishCallback finishCB = null;

    public void Fly(FinishCallback callback)
    {
        finishCB = callback;

        if (TLBattleScene.Instance == null
             || TLBattleScene.Instance.Actor == null
             || TLBattleScene.Instance.Actor.ObjectRoot == null)
        {
            finishCB.Invoke();
            return;
        }
        target = TLBattleScene.Instance.Actor.ObjectRoot.transform;
        var HeadTransform = TLBattleScene.Instance.Actor.HeadTransform;
        if(HeadTransform == null || target == null)
        {
            finishCB.Invoke();
            return;
        }

        HHHH = HeadTransform.position.y - target.position.y;
        t = 0.0f;
        ty = 0.0f;
        //range = UnityEngine.Random.Range(1.5f, 2.0f);
        
        startPosition = this.transform.position;
        start = true;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }


}
