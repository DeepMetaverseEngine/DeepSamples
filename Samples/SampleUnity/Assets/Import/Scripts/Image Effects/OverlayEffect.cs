using UnityEngine;
using System.Collections;

public class OverlayEffect : MonoBehaviour {

    public FadeEffectScreen FadeEffect;

    private System.Action mFinishCallback;

    private UnityEngine.UI.GraphicRaycaster mRaycaster;

	// Use this for initialization
    void Start()
    {
        mRaycaster = this.gameObject.GetComponent<UnityEngine.UI.GraphicRaycaster>();
        if (FadeEffect != null)
        {
            FadeEffect.OnFadeEffectFinish = OnEffectFinish;
        }
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void FadeIn(float time = -1, System.Action callback = null)
    {
        if (FadeEffect != null)
        {
            mRaycaster.enabled = true;
            FadeEffect.gameObject.SetActive(true);
            FadeEffect.FadeIn(time);
            mFinishCallback = callback;
        }
    }

    public void FadeOut(float time = -1, System.Action callback = null)
    {
        if (FadeEffect != null)
        {
            mRaycaster.enabled = true;
            FadeEffect.gameObject.SetActive(true);
            FadeEffect.FadeOut(time);
            mFinishCallback = callback;
        }
    }

    private void OnEffectFinish()
    {
        mRaycaster.enabled = false;
        if (mFinishCallback != null)
        {
            System.Action callback = mFinishCallback;
            mFinishCallback = null;
            callback.Invoke();
        }
    }

}
