using System;
using Slate;
using UnityEngine;
using System.Collections.Generic;

public class LetterBoxEffect : MonoBehaviour {

    public UnityEngine.UI.Image mTopBox;
    public UnityEngine.UI.Image mBottomBox;
    public int BoxHeight = 50;
    private RectTransform mTopRect;
    private RectTransform mBottomRect;
    private bool mIsShow = false;
    private float mShowTime = 0;
    public void Awake()
    {
      
        mTopRect = mTopBox.GetComponent<RectTransform>();
        mTopRect.sizeDelta = new Vector2(mTopRect.sizeDelta.x, BoxHeight);
        mBottomRect = mBottomBox.GetComponent<RectTransform>();
        if (GameUtil.IsIPhoneX())
        {
            mBottomRect.sizeDelta = new Vector2(mBottomRect.sizeDelta.x, BoxHeight + HZUISystem.Instance.IPhoneXOffY);
        }
        else
        {
            mBottomRect.sizeDelta = new Vector2(mBottomRect.sizeDelta.x, BoxHeight);
        }
     
    }
    public void Init()
    {
        EventManager.Unsubscribe("ShowLetterBox", ShowLetterBox);
        EventManager.Subscribe("ShowLetterBox", ShowLetterBox);
    }
    private void ShowLetterBox(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object value;
        if (data.TryGetValue("ShowBox", out value))
        {
            mIsShow = (bool)value;
            this.gameObject.SetActive(mIsShow);
            mShowTime += Time.deltaTime;
            mShowTime = Math.Min(mShowTime, 1);
            Show(mIsShow?1:0);
            DramaUIManage.Instance.highlightMask.SetArrowTransform(mIsShow, null, 0);
            
        }
    }
  
    public void Show(float time)
    {
        var lerp = Easing.Ease(EaseType.QuadraticInOut, 0, 1, time);
        Vector3 pos = new Vector3(mTopBox.gameObject.transform.localPosition.x, Mathf.Lerp(BoxHeight, 0, lerp), mTopBox.gameObject.transform.localPosition.z);
        mTopRect.anchoredPosition3D = pos;
        pos.y = -pos.y;
        mBottomRect.anchoredPosition3D = pos;
    }
    
}
