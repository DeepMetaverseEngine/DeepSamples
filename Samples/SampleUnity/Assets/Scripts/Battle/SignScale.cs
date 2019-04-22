using UnityEngine;
using System.Collections;
using uTools;
using UnityEngine.Events;


public class SignScale : MonoBehaviour {

	// Use this for initialization
    private float durationtime = 0;
    private float limittime = 0;
    private UnityEvent CloseEvent = new UnityEvent();
    private uTweenScale tweenScale;

    private Vector3 mFrom = new Vector3(0.8f, 0.8f, 0.8f);
    private Vector3 mEnd = Vector3.one;

    void Start () {
	     //TianyuConfig extConfig = TLBattleManager.Templates.ExtConfig as TLConfig;
         //if (extConfig != null)
         //{
         //    durationtime = extConfig.SignTime*0.8f/1000f;
         //}
          tweenScale = this.gameObject.GetComponent<uTweenScale>();
      
        if(tweenScale != null){
            tweenScale.duration = durationtime;
            tweenScale.onFinished = CloseEvent;
            CloseEvent.AddListener(onClose);

        }
	}
    public void Show(){
        if (tweenScale)
        {
            this.gameObject.transform.localScale = mFrom;
            tweenScale.Reset();
            tweenScale.from = mFrom;
            tweenScale.to = mEnd;
            tweenScale.duration = durationtime/2;
            tweenScale.onFinished = CloseEvent;
            tweenScale.enabled = true;
        }
    }
    public void onClose()
    {
        this.gameObject.SetActive(false);
    }


}
