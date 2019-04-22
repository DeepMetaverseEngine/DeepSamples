using UnityEngine;
using System.Collections;

public class BlurEffect : MonoBehaviour
{
    public float dist = 1f;
    public float strength = 4f;

    public Shader blurShader = null;

    private Material m_Material = null;
    protected Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(blurShader);
                m_Material.hideFlags = HideFlags.DontSave;
            }
            return m_Material;
        }
    }

    protected void OnDisable()
    {
        if (m_Material)
        {
            DeepCore.Unity3D.UnityHelper.DestroyImmediate(m_Material);
        }
    }

    protected void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        if (!blurShader || !material.shader.isSupported)
        {
            enabled = false;
            return;
        }

    }
     void Update()
    {
        if (material == null)
            return;
        material.SetFloat("_BlurDist", dist);
        material.SetFloat("_BlurStrength", strength);

    }



    // Downsamples the texture to a quarter resolution.
    private void DownSample4x(RenderTexture source, RenderTexture dest)
    {
        Graphics.Blit(source, dest, material);
    }

    // Called by the camera to apply the image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int rtW = source.width / 2;
        int rtH = source.height / 2;
        RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);

        // Copy source to the 4x4 smaller texture.
        DownSample4x(source, buffer);

        Graphics.Blit(buffer, destination);

        RenderTexture.ReleaseTemporary(buffer);
    }
    private void OnEnable()
    {
        
    }

}