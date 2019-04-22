using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeEffect : MonoBehaviour {

    //public Texture2D FadeTexture;   //渐变贴图.
    //private Rect mRect;

    public float FadeInTime = 0.5f;   //淡入时长.
    public float FadeOutTime = 0.5f;   //淡出时长.
    public string Attribute = "_TintColor";
    public Color MaskColor = Color.black;

    private float mFadeTimeMax;
    private float mFadeTimer;
    private float mAlpha = 1.0f;

    private FadeState mFadeState;

    private List<Material> mMats = new List<Material>();

    private enum FadeState
    {
        Stop,
        FadeIn,
        FadeOut
    }

    void Start()
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rs.Length; i++)
        {
            foreach (var mat in rs[i].materials)
            {
                mMats.Add(mat);
            }
        }
    }

    void Update()
    {
        switch (mFadeState)
        {
            case FadeState.Stop:
                return;
            case FadeState.FadeIn:
                if (mFadeTimer <= 0)
                {
                    mFadeTimer = 0;
                    mFadeState = FadeState.Stop;
                }
                else
                    mFadeTimer -= Time.deltaTime;
                break;
            case FadeState.FadeOut:
                if (mFadeTimer >= mFadeTimeMax)
                {
                    mFadeTimer = mFadeTimeMax;
                    mFadeState = FadeState.Stop;
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
        foreach (var mat in mMats)
        {
            mat.SetColor(Attribute, color);
        }
    }

    //void OnGUI()
    //{
    //    if (FadeTexture != null && mFadeState != FadeState.Stop)
    //    {
    //        GUI.color = mColor;
    //        GUI.DrawTexture(mRect, FadeTexture);
    //    }
    //}

    void Stop()
    {
        mFadeState = FadeState.Stop;
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

}
