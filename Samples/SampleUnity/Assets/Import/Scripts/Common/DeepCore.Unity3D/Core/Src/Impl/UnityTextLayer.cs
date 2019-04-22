using DeepCore.GUI.Display;
using System;

using UnityEngine;

namespace DeepCore.Unity3D.Impl
{


    public class UnityTextLayer : TextLayer
    {
        internal Texture2D mTexture;
        private UnityImage mBuffer;

        public UnityTextLayer(string text, GUI.Display.FontStyle style, float size)
            : base(text, (int)size, style)
        {
            isDirty = true;
        }



        internal void Refresh()
        {
            if (this.isDirty)
            {
                this.isDirty = false;
                if (mBuffer != null)
                {
                    mBuffer.Dispose();
                    mBuffer = null;
                    mTexture = null;
                }

                if (string.IsNullOrEmpty(mText))
                {
                    return;
                }

                int boundW = 0;
                int boundH = 0;
                mTexture = UnityDriver.Platform.SysFontTexture(
                    mText,
                    false,
                    mFontStyle,
                    Math.Max(1.0f, mFontSize),
                    isEnable ? mFontColorRGBA : DefaultDisableTextColorRGBA,
                    mBorderTime,
                    mBorderColorRGBA,
                    mExpectSize,
                    out boundW,
                    out boundH);
                if (mTexture != null)
                {
                    mBounds.width = boundW;
                    mBounds.height = boundH;
                    mBuffer = new UnityImage(mTexture, boundW, boundH, mText);
                }
            }
        }
        protected override void Disposing()
        {
            if (mBuffer != null)
            {
                mBuffer.Dispose();
            }
            mTexture = null;
        }

        public override GUI.Display.Image GetBuffer()
        {
            Refresh();
            return mBuffer;
        }
    }
}
