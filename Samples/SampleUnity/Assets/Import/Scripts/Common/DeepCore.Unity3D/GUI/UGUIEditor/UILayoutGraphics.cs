using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
using System;
using UnityEngine;
using UnityImage = DeepCore.Unity3D.Impl.UnityImage;

namespace DeepCore.Unity3D.UGUIEditor
{
    public partial class UILayoutGraphics : ImageGraphics
    {
        private UILayout mLayout;
        private float mAlpha = 1f;

        [SerializeField]
        private bool m_IsShowUILayout = true;
        public bool IsShowUILayout
        {
            get { return m_IsShowUILayout; }
            set
            {
                if (m_IsShowUILayout != value)
                {
                    m_IsShowUILayout = value;
                    base.SetVerticesDirty();
                }
            }
        }

        public bool HasUILayout
        {
            get
            {
                return mLayout != null;
            }
        }

        private Texture2D mLastLayoutTexture;

        public override Texture mainTexture
        {
            get { return (IsShowUILayout) ? mLayout.MainTexture : base.mainTexture; }
        }
        public float Alpha
        {
            get { return mAlpha; }
            set
            {
                if (value != mAlpha)
                {
                    mAlpha = value;
                    Color c = base.color;
                    c.a = value;
                    base.color = c;
                    this.SetAllDirty();
                }
            }
        }

        public UILayoutGraphics()
        {
        }

        public void UpdateSprite()
        {
            if (mLayout != null && mLayout.mSpriteController != null)
            {
                if (mLayout.mSpriteController.Update())
                {
                    this.SetVerticesDirty();
                }
            }
        }

        public UILayoutGraphics SetCurrentLayout(UILayout layout)
        {
            if (mLayout != layout ||
                (mLayout != null && mLayout.MainTexture != mLastLayoutTexture))
            {
                this.mLayout = layout;
                if (layout != null)
                {
                    this.enabled = true;
                    mLastLayoutTexture = mLayout.MainTexture;
                    if (mLayout.mImageSrc != null && mLayout.mImageRegion != null)
                    {
                        SetImage(mLayout.mImageSrc, mLayout.mImageRegion, Vector2.zero);
                    }
                }
                else
                {
                    this.enabled = false;
                }
                this.SetAllDirty();
            }
            return this;
        }
        public UILayoutGraphics SetFillMode(FillMethod fill, int fillOrigin, bool fillClockwise = false, bool fillCenter = true)
        {
            base.type = Type.Filled;
            base.fillMethod = fill;
            base.fillOrigin = fillOrigin;
            base.fillClockwise = fillClockwise;
            base.fillCenter = fillCenter;
            this.m_IsShowUILayout = false;
            this.SetAllDirty();
            return this;
        }
        public UILayoutGraphics SetFillPercent(float percent)
        {
            base.fillAmount = DeepCore.CMath.getInRange(percent, 0, 100) / 100f;
            return this;
        }

        public override void CalculateLayoutInputHorizontal() { }
        public override void CalculateLayoutInputVertical() { }
        public override void OnAfterDeserialize() { }
        public override void OnBeforeSerialize() { }
        public override void SetNativeSize()
        {
            if (mLayout != null)
            {
                this.SetAllDirty();
            }
            else
            {
                base.SetNativeSize();
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        protected override void OnPopulateMesh(UnityEngine.UI.VertexHelper vh)
        {
            if (IsShowUILayout)
            {
                vh.Clear();
                using (var o = new HelperVBO(vh, this.color))
                {
                    o.OnFillVBO(rectTransform.rect.size, mLayout);
                }
            }
            else
            {
                //                 if (base.sprite == null && mLayout != null)
                //                 {
                //                     if (mLayout.mImageSrc != null && mLayout.mImageRegion != null)
                //                     {
                //                         if (base.sprite != null)
                //                         {
                //                             DeepCore.Unity3D.UnityHelper.Destroy(sprite);
                //                         }
                //                         base.sprite = UIUtils.CreateSprite(mLayout.mImageSrc, mLayout.mImageRegion, Vector2.zero);
                //                     }
                //                 }
                base.OnPopulateMesh(vh);
            }
        }
        private static UIVertex[] s_UIVertexQuard = new UIVertex[4];
        private static UIVertex[,] s_UIVertex4x4 = new UIVertex[4, 4];
        private static float[] ax_4 = new float[4];
        private static float[] ax_2 = new float[2];
        private static float[] ay_4 = new float[4];
        private static float[] ay_2 = new float[2];
        private static float[] au_4 = new float[4];
        private static float[] au_2 = new float[2];
        private static float[] av_4 = new float[4];
        private static float[] av_2 = new float[2];
        private static void ArraySet4(float[] array, float a, float b, float c, float d)
        {
            array[0] = a;
            array[1] = b;
            array[2] = c;
            array[3] = d;
        }
        private static void ArraySet2(float[] array, float a, float b)
        {
            array[0] = a;
            array[1] = b;
        }

        private struct HelperVBO : VBO
        {
            private UnityEngine.UI.VertexHelper toFill;
            private VertexHelperBuffer vbo;

            public HelperVBO(UnityEngine.UI.VertexHelper mesh, Color color)
            {
                this.toFill = mesh;
                this.vbo = VertexHelperBuffer.AllocAutoRelease(mesh);
                this.vbo.BlendColor = color;
            }
            public void Dispose()
            {
                vbo.Dispose();
                vbo = null;
                toFill = null;
            }

            public void OnFillVBO(Vector2 size, UILayout layout)
            {
                if(layout == null)
                {
                    return;
                }
                switch (layout.Style)
                {
                    case UILayoutStyle.NULL:
                        break;
                    case UILayoutStyle.COLOR:
                        VertexFillColor(layout, size.x, size.y, layout.mFillColor);
                        break;
                    case UILayoutStyle.SPRITE:
                        if (layout.mSpriteController != null) VertexSprite(layout, size.x, size.y);
                        break;
                    case UILayoutStyle.IMAGE_STYLE_ALL_8:
                        if (layout.mImageSrc != null) VertexAll9(layout, size.x, size.y);
                        break;
                    case UILayoutStyle.IMAGE_STYLE_ALL_9:
                        if (layout.mImageSrc != null) VertexAll9(layout, size.x, size.y);
                        break;
                    case UILayoutStyle.IMAGE_STYLE_H_012:
                        if (layout.mImageSrc != null) VertexH012(layout, size.x, size.y);
                        break;
                    case UILayoutStyle.IMAGE_STYLE_V_036:
                        if (layout.mImageSrc != null) VertexV036(layout, size.x, size.y);
                        break;
                    case UILayoutStyle.IMAGE_STYLE_HLM:
                        break;
                    case UILayoutStyle.IMAGE_STYLE_VTM:
                        break;
                    case UILayoutStyle.IMAGE_STYLE_BACK_4:
                        if (layout.mImageSrc != null) VertexBack4(layout, size.x, size.y);
                        break;
                    case UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER:
                        if (layout.mImageSrc != null) VertexBack4Center(layout, size.x, size.y);
                        break;
                    default:
                        break;
                }
            }

            private void VertexBuffer(UnityImage src, float[] ax, float[] ay, float[] au, float[] av)
            {
                for (int iy = 0; iy < ay.Length; ++iy)
                {
                    for (int ix = 0; ix < ax.Length; ++ix)
                    {
                        s_UIVertex4x4[ix, iy] = UIUtils.CreateVertex(src, vbo.BlendColor, au[ix], av[iy], ax[ix], ay[iy]);
                    }
                }
                for (int iy = 0; iy < ay.Length - 1; ++iy)
                {
                    for (int ix = 0; ix < ax.Length - 1; ++ix)
                    {
                        s_UIVertexQuard[0] = s_UIVertex4x4[ix + 0, iy + 0];
                        s_UIVertexQuard[1] = s_UIVertex4x4[ix + 1, iy + 0];
                        s_UIVertexQuard[2] = s_UIVertex4x4[ix + 1, iy + 1];
                        s_UIVertexQuard[3] = s_UIVertex4x4[ix + 0, iy + 1];
                        toFill.AddUIVertexQuad(s_UIVertexQuard);
                    }
                }
            }


            private void VertexAll9(UILayout mLayout, float w, float h)
            {
                float cw = mLayout.mClipSize;
                float ch = mLayout.mClipSize;
                if (cw * 2 > w) { cw = w / 2f; }
                if (ch * 2 > h) { ch = h / 2f; }
                ArraySet4(ax_4, 0, cw, w - cw, w);
                ArraySet4(ay_4, 0, ch, h - ch, h);
                ArraySet4(au_4,
                             mLayout.mImageRegion.x,
                             mLayout.mImageRegion.x + mLayout.mClipSize,
                             mLayout.mImageRegion.x + mLayout.mImageRegion.width - mLayout.mClipSize,
                             mLayout.mImageRegion.x + mLayout.mImageRegion.width
                         );
                ArraySet4(av_4,
                            mLayout.mImageRegion.y,
                             mLayout.mImageRegion.y + mLayout.mClipSize,
                             mLayout.mImageRegion.y + mLayout.mImageRegion.height - mLayout.mClipSize,
                             mLayout.mImageRegion.y + mLayout.mImageRegion.height
                          );
                VertexBuffer(mLayout.mImageSrc, ax_4, ay_4, au_4, av_4);
            }
            private void VertexH012(UILayout mLayout, float w, float h)
            {
                float cw = mLayout.mClipSize;
                if (cw * 2 > w) { cw = w / 2; }
                ArraySet4(ax_4, 0, cw, w - cw, w);
                ArraySet2(ay_2, 0, h);
                ArraySet4(au_4,
                             mLayout.mImageRegion.x,
                             mLayout.mImageRegion.x + mLayout.mClipSize,
                             mLayout.mImageRegion.x + mLayout.mImageRegion.width - mLayout.mClipSize,
                             mLayout.mImageRegion.x + mLayout.mImageRegion.width
                         );
                ArraySet2(av_2,
                             mLayout.mImageRegion.y,
                             mLayout.mImageRegion.y + mLayout.mImageRegion.height
                         );
                VertexBuffer(mLayout.mImageSrc, ax_4, ay_2, au_4, av_2);
            }
            private void VertexV036(UILayout mLayout, float w, float h)
            {
                float ch = mLayout.mClipSize;
                if (ch * 2 > h) { ch = h / 2; }

                ArraySet2(ax_2, 0, w);
                ArraySet4(ay_4, 0, ch, h - ch, h);
                ArraySet2(au_2,
                            mLayout.mImageRegion.x,
                            mLayout.mImageRegion.x + mLayout.mImageRegion.width
                         );
                ArraySet4(av_4,
                            mLayout.mImageRegion.y,
                            mLayout.mImageRegion.y + mLayout.mClipSize,
                            mLayout.mImageRegion.y + mLayout.mImageRegion.height - mLayout.mClipSize,
                            mLayout.mImageRegion.y + mLayout.mImageRegion.height
                         );
                VertexBuffer(mLayout.mImageSrc, ax_2, ay_4, au_2, av_4);
            }

            private void VertexBack4(UILayout mLayout, float w, float h)
            {
                ArraySet2(ax_2, 0, w);
                ArraySet2(ay_2, 0, h);
                ArraySet2(au_2,
                            mLayout.mImageRegion.x,
                            mLayout.mImageRegion.x + mLayout.mImageRegion.width
                         );
                ArraySet2(av_2,
                           mLayout.mImageRegion.y,
                           mLayout.mImageRegion.y + mLayout.mImageRegion.height
                        );
                VertexBuffer(mLayout.mImageSrc, ax_2, ay_2, au_2, av_2);
            }
            private void VertexBack4Center(UILayout mLayout, float w, float h)
            {
                float tx = (w - mLayout.mImageRegion.width) * 0.5f;
                float ty = (h - mLayout.mImageRegion.height) * 0.5f;
                ArraySet2(ax_2, tx, tx + mLayout.mImageRegion.width);
                ArraySet2(ay_2, ty, ty + mLayout.mImageRegion.height);
                ArraySet2(au_2,
                             mLayout.mImageRegion.x,
                             mLayout.mImageRegion.x + mLayout.mImageRegion.width
                         );
                ArraySet2(av_2,
                             mLayout.mImageRegion.y,
                             mLayout.mImageRegion.y + mLayout.mImageRegion.height
                          );
                VertexBuffer(mLayout.mImageSrc, ax_2, ay_2, au_2, av_2);
            }

            private void VertexFillColor(UILayout mLayout, float w, float h, Color c)
            {
                UIUtils.CreateVertexQuardColor(c * vbo.BlendColor, 0, 0, w, h, toFill);
            }

            private void VertexSprite(UILayout mLayout, float w, float h)
            {
                mLayout.mSpriteController.Meta.addVertex(vbo,
                    mLayout.mSpriteController.CurrentAnimate,
                    mLayout.mSpriteController.CurrentFrame,
                    w / 2, h / 2);
            }

        }
        

        //---------------------------------------------------------------------------------------------------------------

        interface VBO : IDisposable
        {
            void OnFillVBO(Vector2 size, UILayout layout);

        }

        //---------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------
    }


}

