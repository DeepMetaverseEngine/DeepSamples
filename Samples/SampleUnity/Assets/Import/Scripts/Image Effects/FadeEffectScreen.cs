using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeEffectScreen : MonoBehaviour {

    public float FadeInTime = 0.5f;   //淡入时长.
    public float FadeOutTime = 0.5f;   //淡出时长.
    public Color MaskColor = Color.black;

    public delegate void OnFadeEffectFinishEvent();
    public OnFadeEffectFinishEvent OnFadeEffectFinish;

    private UnityEngine.UI.Image mFadeImg;

    private float mFadeTimeMax;
    private float mFadeTimer;
    private float mAlpha = 1.0f;

    private FadeState mFadeState;
    private Color mOrgColor;
    private enum FadeState
    {
        Stop,
        FadeIn,
        FadeOut,
        FadeCustom
    }
    private void SaveColor()
    {
        mOrgColor = mFadeImg.color;
    }
    public  void ResetColor()
    {
        mFadeImg.color = mOrgColor;
    }
    void Start()
    {
        mFadeImg = this.gameObject.GetComponent<UnityEngine.UI.Image>();
        SaveColor();
    }

    void Update()
    {
        switch (mFadeState)
        {
            case FadeState.FadeCustom:
                return;
            case FadeState.Stop:
                return;
            case FadeState.FadeIn:
                if (mFadeTimer <= 0)
                {
                    mFadeTimer = 0;
                    Stop();
                }
                else
                    mFadeTimer -= Time.deltaTime;
                break;
            case FadeState.FadeOut:
                if (mFadeTimer >= mFadeTimeMax)
                {
                    mFadeTimer = mFadeTimeMax;
                    Stop();
                }
                else
                {
                    mFadeTimer += Time.deltaTime;
                }
                break;
        }
        mAlpha = mFadeTimer / mFadeTimeMax;
        mAlpha = Mathf.Clamp01(mAlpha);
        Color color = MaskColor;
        color.a = mAlpha;
        mFadeImg.color = color;
    }

    private void Stop()
    {
        mFadeState = FadeState.Stop;
        if (OnFadeEffectFinish != null)
        {
            OnFadeEffectFinish();
        }
    }

    public void FadeIn(float time = -1)
    {
        if (mFadeState == FadeState.FadeIn) return;
        time = time < 0 ? FadeInTime : time;
        mFadeState = FadeState.FadeIn;
        mFadeTimeMax = time;
        mFadeTimer = time;
    }

    public void FadeOut(float time = -1)
    {
        if (mFadeState == FadeState.FadeOut) return;
        time = time < 0 ? FadeOutTime : time;
        mFadeState = FadeState.FadeOut;
        mFadeTimeMax = time;
        mFadeTimer = 0;
    }

    public void FadeCustom(Color color)
    {
        mFadeState = FadeState.FadeCustom;
        mFadeImg.color = color;
    }

}
