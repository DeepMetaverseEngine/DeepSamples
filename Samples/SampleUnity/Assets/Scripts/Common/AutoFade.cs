using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class AutoFade : MonoBehaviour {

    [Range(0, 1)]
    public float fadeLevel = 0.2f;
    
    private List<Material> mMats = new List<Material>();
    private List<Shader> mOrgShader = new List<Shader>();
    private static Shader FadeShader;
    private bool mIsFading;

    void Awake()
    {
        if (FadeShader == null)
            FadeShader = Shader.Find("TL/Eff/DiffuseAlphaFade");
    }
    
	void Start ()
    {
        Transform root = this.transform.parent;
        Renderer[] renders = root.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            Material[] mats = renders[i].materials;
            for (int j = 0; j < mats.Length; j++)
            {
                Material mat = mats[j];
                mMats.Add(mat);
                mOrgShader.Add(mat.shader);
            }
        }
        StartFade();
    }

    public void StartFade()
    {
        if (!mIsFading)
        {
            mIsFading = true;
            for (int i = 0; i < mMats.Count; i++)
            {
                ChangeFadeShader(mMats[i], FadeShader);
            }
        }
    }

    public void StopFade()
    {
        if (mIsFading)
        {
            mIsFading = false;
            for (int i = 0; i < mMats.Count; i++)
            {
                ChangeShader(mMats[i], mOrgShader[i]);
            }
        }
    }

    void ChangeFadeShader(Material mat, Shader fadeShader)
    {
        if (mat.shader != fadeShader)
        {
              //mat.SetFloat("_Opacity", 0.2f);
                mat.shader = fadeShader;
                mat.DOFloat(fadeLevel, "_Opacity", 0.5f).SetEase(Ease.Linear);
        }
    }

    void ChangeShader(Material mat ,Shader orgShader)
    {
        if (mat.shader != orgShader)
        {

            mat.shader = orgShader;
            //mat.DOFloat(0.5f, "_Opacity", 0.5f)
            //  .SetEase(Ease.Linear)
            // .OnComplete(() => mat.shader = orgShader);
        }
    }

}
