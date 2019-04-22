using UnityEngine;
using System.Collections;

public class EventFunction : MonoBehaviour {

    public string eventName;

    public enum Actions
    {
        ActiveAllChild = 0,
        InactiveAllChild = 1,
        BringToForward = 2,
        DestroySelfGameObject = 3,
        DestroySelfComponent = 4,
        FireClick = 5,
    }
    public Actions[] actions = new Actions[1];

    private bool mSubed = false;

	// Use this for initialization
    void Awake()
    {
        if (gameObject.activeInHierarchy && !string.IsNullOrEmpty(eventName))
        {
            EventManager.Subscribe(eventName, OnEventFired);
            mSubed = true;
        }
	}

    void OnEnable()
    {
        if (!string.IsNullOrEmpty(eventName) && !mSubed)
        {
            EventManager.Subscribe(eventName, OnEventFired);
            mSubed = true;
        }
    }

    void OnDisable()
    {
        if (mSubed)
        {
            EventManager.Unsubscribe(eventName, OnEventFired);
            mSubed = false;
        }
    }

    void OnEventFired(EventManager.ResponseData res)
    {
        foreach (var action in actions)
        {
            switch (action)
            {
                case Actions.ActiveAllChild:
                    //NGUITools.SetActiveChildren(gameObject, true);
                    break;
                case Actions.InactiveAllChild:
                    //NGUITools.SetActiveChildren(gameObject, false);
                    break;
                case Actions.DestroySelfGameObject:
                    DeepCore.Unity3D.UnityHelper.Destroy(gameObject);
                    break;
                case Actions.DestroySelfComponent:
                    DeepCore.Unity3D.UnityHelper.Destroy(this);
                    break;
                case Actions.BringToForward:
                    //NGUITools.BringForward(gameObject);
                    break;
                case Actions.FireClick:
                    gameObject.SendMessage("OnClick");
                    break;
            }
        }
    }
	
}
