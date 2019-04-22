using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEraserTexture : MonoBehaviour
{
    private Graphic mGraphic;

    private Color[] mCachedPixels;
    private int[] mFinish;
    private float[] mBrushPixels;
    private float mTimeCount = 0;
    private bool mDirty = false;

    public void SetGraphic(Graphic g, int tw = 0, int th = 0)
    {
        if (mGraphic)
        {
            mGraphic.material = mLastMaterial;
        }
        if (texRender)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(texRender);
        }
        mGraphic = g;
        TextureWidth = tw;
        TextureHeight = th;
        if (TextureWidth == 0 || TextureHeight == 0)
        {
            TextureWidth = mGraphic.mainTexture.width;
            TextureHeight = mGraphic.mainTexture.height;
        }
        texRender = new Texture2D(TextureWidth, TextureHeight, TextureFormat.ARGB32, true);
        mMaxPixel = TextureWidth * TextureHeight;
        mLastMaterial = mGraphic.material;
        mGraphic.material = mEraseMaterial;
        ApplyTexture();
        SetBrush();
        SetCachedPixels();
        Reset();
    }

    private void SetBrush()
    {
        //brushImg = Resources.Load<Texture2D>("brush");
        mBrushPixels = new float[brushImg.width * brushImg.height];
        var ps = brushImg.GetPixels();
        for (int i = 0; i < ps.Length; i++)
        {
            mBrushPixels[i] = ps[i].a;
        }  
    }

    private void SetCachedPixels()
    {
        mCachedPixels = new Color[mMaxPixel];
        for (int i = 0; i < mMaxPixel; i++)
        {
            mCachedPixels[i] = Color.white;
        }
        mFinish = new int[mMaxPixel];
    }
    
    public Camera CanvasWorldCamera;
    public int TextureWidth;
    public int TextureHeight;

    Texture2D texRender;
    private int mMaxPixel;
    private int mCurrentDrawed;
    public float Percent;
    public Texture2D brushImg;
    public float updateTime = 0.1f;

    private void Awake()
    {
        var shader = Shader.Find("Unlit/Transparent Colored Eraser");
        mEraseMaterial = new Material(shader);
    }


    private Material mLastMaterial;
    private Material mEraseMaterial;

    void OnDisable()
    {
        if (mGraphic)
        {
            mGraphic.material = mLastMaterial;
        }
        mLastMaterial = null;
    }

    void OnEnable()
    {
        if (mGraphic)
        {
            mLastMaterial = mGraphic.material;
            mGraphic.material = mEraseMaterial;
            Reset();
        }
    }

    void OnDestroy()
    {
        if (texRender)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(texRender);
        }
        if (mEraseMaterial)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(mEraseMaterial);
        }
    }

    void Update()
    {
        mTimeCount += Time.deltaTime;
        if(mTimeCount >= updateTime)
        {
            mTimeCount = 0;
            DoDraw();
        }
    }

    private void DoDraw()
    {
        if(mDirty)
        {
            texRender.SetPixels(mCachedPixels);
            ApplyTexture();
            mDirty = false;
        }
    }

    public void Draw(Vector2 pos)
    {
        Rect rect = new Rect(new Vector2(pos.x - brushImg.width/2, pos.y - brushImg.height/2), 
            new Vector2(brushImg.width, brushImg.height));
        var radX = (rect.xMax - rect.xMin) / 2;
        var radY = (rect.yMax - rect.yMin) / 2;

        for (var y = -radX; y < radX; y++)
        {
            for (var x = -radY; x < radY; x++)
            {
                //if (x * x + y * y <= radius * radius)
                {
                    int targetX = (int) (rect.center.x + x);
                    int targetY = (int) (rect.center.y + y);
                    if (targetX < 0 || targetX >= texRender.width || targetY < 0 || targetY >= texRender.height)
                    {
                        return;
                    }
                    var brush_index = (int)((x + radX) * (brushImg.height) + (y + radY));
                    if (brush_index >= mBrushPixels.Length) continue;
                    float a = mBrushPixels[brush_index];
                    int index = targetY * texRender.width + targetX;
                    //int index = targetX * texRender.height + targetY;
                    //int index = (texRender.height - targetY) * texRender.width + targetX;

                    if (index >= mCachedPixels.Length) continue;

                    Color color = mCachedPixels[index];
                    if (color.a > a)
                    {
                        
                        if (mFinish[index] < 1)
                        {
                            mCurrentDrawed++;
                            Percent = (float)mCurrentDrawed / mMaxPixel;
                            mFinish[index] = 1;
                        }
                        color.a = a;
                        //texRender.SetPixel(targetX, targetY, color);
                        mCachedPixels[index] = color;
                        mDirty = true;
                    }
                }
            }
        }

        //for (int x = (int)rect.xMin; x < (int)rect.xMax; x++)
        //{
        //    for (int y = (int)rect.yMin; y < (int)rect.yMax; y++)
        //    {
        //        if (x < 0 || x > texRender.width || y < 0 || y > texRender.height)
        //        {
        //            return;
        //        }
        //        Color color = texRender.GetPixel(x, y);
        //        if (color.a > 0)
        //        {
        //            mCurrentDrawed++;
        //            Percent = (float)mCurrentDrawed / mMaxPixel;
        //            color.a = 0;
        //            texRender.SetPixel(x, y, color);
        //        }
        //    }
        //}
    }

    public void ApplyTexture()
    {
        texRender.Apply();
        mGraphic.material.SetTexture("_RendTex", texRender);
    }

    void Reset()
    {
        for (int j = 0; j < texRender.height; j++)
        {
            for (int i = 0; i < texRender.width; i++)
            {
                Color color = texRender.GetPixel(i, j);
                color.a = 1;
                texRender.SetPixel(i, j, color);
                mCachedPixels[i * texRender.height + j] = color;
                mFinish[i * texRender.height + j] = 0;
            }
        }
        mCurrentDrawed = 0;
        Percent = 0;
        ApplyTexture();
        mDirty = false;
    }
}