using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasRenderer))]
public class UIMeshRenderer : MonoBehaviour 
{
    public Mesh mesh;
    public Material material;
    public Texture texture;
    public Color color = Color.white;

    public bool enableMask;

    private CanvasRenderer canvasRenderer;
    private Material drawMaterial;

    void Awake()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();

        if (material)
            drawMaterial = new Material(material);
    }

    void OnRenderObject()
    {
        if (mesh)
            DrawMesh();
    }

    void DrawMesh()
    {
        canvasRenderer.SetMesh(mesh);
        if (drawMaterial)
            canvasRenderer.SetMaterial(drawMaterial, texture);
        canvasRenderer.SetColor(color);
        if (enableMask)
        {
            Mask clipMask = canvasRenderer.GetComponentInParent<Mask>();
            if (clipMask && clipMask.enabled)
            {
                RectTransform maskTrans = clipMask.GetComponent<RectTransform>();
                Rect maskRect = maskTrans.rect;
                Vector2 mPos = maskTrans.localPosition;
                Rect clipRect = new Rect(maskRect.x + mPos.x, maskRect.y + mPos.y, maskRect.width, maskRect.height);
                canvasRenderer.EnableRectClipping(maskRect);
            }
        }
        else
        {
            canvasRenderer.DisableRectClipping();
        }          
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }

    public void SetUV(string propertyName, Vector4 uv)
    {
        if (drawMaterial)
        {
            drawMaterial.SetVector(propertyName, uv);
        }
    }

    public void SetValue(string propertyName, float v)
    {
        if (drawMaterial)
        {
            drawMaterial.SetFloat(propertyName, v);
        }
    }
}
