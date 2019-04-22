using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ColliderRotateZ : MonoBehaviour
{
    public Collider collider;

    void Start()
    {
        var trigger = GetComponent<EventTrigger>();
        if (trigger.triggers == null)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        var beginDrag = new EventTrigger.Entry
        {
            eventID = EventTriggerType.BeginDrag,
            callback = new EventTrigger.TriggerEvent()
        };
        beginDrag.callback.AddListener(BeginDrag);
        var endDrag = new EventTrigger.Entry
        {
            eventID = EventTriggerType.EndDrag,
            callback = new EventTrigger.TriggerEvent()
        };
        endDrag.callback.AddListener(EndDrag);
        var drag = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Drag,
            callback = new EventTrigger.TriggerEvent()
        };
        drag.callback.AddListener(Drag);


        trigger.triggers.Add(beginDrag);
        trigger.triggers.Add(endDrag);
        trigger.triggers.Add(drag);
    }

    private bool mStarted;

    private bool CheckHit(PointerEventData posEventData)
    {
        Ray ray = GameSceneMgr.Instance.UICamera.ScreenPointToRay(posEventData.position);
        RaycastHit hit;
        if (collider.Raycast(ray, out hit, 100))
        {
            OnHit(posEventData);
            return true;
        }
        else
        {
            mLastPos = Vector2.zero;
            return false;
        }
    }

    private void OnHit(PointerEventData posEventData)
    {
        if (!mLastPos.Equals(Vector2.zero))
        {
            var currentpos = GameSceneMgr.Instance.UICamera.WorldToScreenPoint(collider.transform.position);
            var angleCurrent = Mathf.Atan2(posEventData.position.y - currentpos.y, posEventData.position.x - currentpos.x);
            var angleLast = Mathf.Atan2(mLastPos.y - currentpos.y, mLastPos.x - currentpos.x);

            var deg = Mathf.Rad2Deg * (angleCurrent - angleLast);
            //Debug.Log(" angle " + currentpos + " " + angleCurrent * Mathf.Rad2Deg + " " + angleLast * Mathf.Rad2Deg);
            var last = collider.transform.localEulerAngles;
            collider.transform.localEulerAngles = new Vector3(last.x, last.y, last.z - deg);
        }

        mLastPos = posEventData.position;
    }

    private Vector2 mLastPos;

    public void Drag(BaseEventData data)
    {
        if (!mStarted)
        {
            return;
        }
        CheckHit((PointerEventData) data);
    }

    public void BeginDrag(BaseEventData data)
    {
        Debugger.Log(collider.name + " ColliderRotateZ BeginDrag" + data);
        if (CheckHit((PointerEventData) data))
        {
            Debugger.Log(collider.name + "ColliderRotateZ BeginDrag hit");
            mStarted = true;
        }
    }

    public void EndDrag(BaseEventData data)
    {
		Debugger.Log(collider.name + "ColliderRotateZ EndDrag " + data);
        mLastPos = Vector2.zero;
        mStarted = false;
    }
}