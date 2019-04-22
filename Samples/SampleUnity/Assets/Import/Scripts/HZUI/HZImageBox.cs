using UnityEngine.EventSystems;
using DeepCore.GUI.Data;
using DeepCore.GUI.Sound;
using UnityEngine;

namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public class HZImageBox : UEImageBox
    {
        public TouchClickHandle TouchClick { get; set; }

        protected override void OnPointerClick(PointerEventData e)
        {
            base.OnPointerClick(e);
            if (TouchClick != null)
                TouchClick(this);
            var soundKey = GetAttributeAs<string>("sound");
            if (!string.IsNullOrEmpty(soundKey))
                SoundManager.Instance.PlaySoundByKey(soundKey);
        }

        private UIEraserTexture mEraser;
        private bool mLastEnable;

        public void StartEraserMode(int brush = 4)
        {
            if (mEraser == null)
            {
                mEraser = UnityObject.AddComponent<UIEraserTexture>();
                mLastEnable = Enable;
                IsInteractive = true;
                Enable = true;
            }
            UnityEngine.UI.Graphic g;
            if (mImageContent != null)
            {
                g = mImageContent.Graphics;
            }
            else
            {
                g = Graphics;
            }
            if(mEraser.brushImg == null)
            {
                mEraser.brushImg = Resources.Load<Texture2D>("brush");
            }
            mEraser.SetGraphic(g, (int)Width, (int)Height);
        }

        protected override void OnStart()
        {
            if (mEraser)
            {
                mEraser.CanvasWorldCamera = Root.Canvas.worldCamera;
            }
        }

        public float EraserPercent
        {
            get { return mEraser != null ? mEraser.Percent : 0; }
        }

        public void StopEraserMode()
        {
            if (mEraser)
            {
                DeepCore.Unity3D.UnityHelper.Destroy(mEraser);
                Enable = mLastEnable;
            }
        }

        private Vector2 mStart = InvalidPos;
        private Vector2 mEnd;

        protected static Vector2 InvalidPos = new Vector2(99999, 99999);

        protected override void OnPointerDown(PointerEventData e)
        {
            if (mEraser)
            {
                mStart = ScreenToLocalPoint2D(e);
                mStart.y = Height - mStart.y;
            }
            base.OnPointerDown(e);
        }

        protected override void OnPointerUp(PointerEventData e)
        {
            base.OnPointerUp(e);
            mStart = InvalidPos;
        }

        protected override void OnPointerMove(PointerEventData e)
        {
            base.OnPointerMove(e);
            if (mEraser)
            {
                mEnd = ScreenToLocalPoint2D(e);
                mEnd.y = Height - mEnd.y;
                mEraser.Draw(mEnd);
                if (InvalidPos.Equals(mStart))
                {
                    mStart = mEnd;
                    return;
                }
                //todo draw¬ﬂº≠  ≈‰anchor≤¢“∆»ÎUIEraserTexture ÷–
                //mEnd = ConvertSceneToUI(Input.mousePosition);
                var tx = (mStart + mEnd).x / 2;
                var ty = (mStart + mEnd).y / 2;
                var tw = Mathf.Abs(mEnd.x - mStart.x);
                var th = Mathf.Abs(mEnd.y - mStart.y);
                var rect = new Rect(tx, ty, tw, th);
                for (var x = (int)rect.xMin; x <= (int)rect.xMax; x+=mEraser.brushImg.width/2)
                {
                    for (var y = (int)rect.yMin; y <= (int)rect.yMax; y += mEraser.brushImg.height / 2)
                    {
                        mEraser.Draw(new Vector2(x, y));
                    }
                }
                mStart = mEnd;
                //mEraser.ApplyTexture();

                //mEraser.Draw(ScreenToLocalPoint2D(e));
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            TouchClick = null;
        }

        protected override void DecodeEnd(UIEditor.Decoder editor, UIComponentMeta e)
        {
            base.DecodeEnd(editor, e);
            Enable = false;
        }

        public static HZImageBox CreateImageBox()
        {
            return new HZImageBox();
        }
    }
}