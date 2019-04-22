using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
using UnityEngine;
using UnityEngine.EventSystems;
using DeepCore.Unity3D.Impl;
using DeepCore.GUI.Cell;
using DeepCore.GUI.Sound;

namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public class HZTextButton : UETextButton
    {
        public delegate bool SetPressDownHandle();


        public HZTextButton()
        {
            IsPlaySound = true;
            //SetSound(SoundManager.Instance.GetDefaultBtnSound());
        }

        public static string DefaultSoundKey;

        public bool IsPlaySound { get; set; }

        protected override void OnStart()
        {
            base.OnStart();
            var transition = GetAttributeAs<string>("transition");
            if (transition == "1" && Selectable != null)
            {
                var selectable = Selectable.AsSelectable;
                if (selectable != null)
                {
                    selectable.transition = UnityEngine.UI.Selectable.Transition.ColorTint;
                    var colors = selectable.colors;
                    colors.fadeDuration = 0;
                    selectable.colors = colors;
                }
            }
        }

        public TouchClickHandle TouchClick { get; set; }

        public SetPressDownHandle SetPressDown { get; set; }

        public override bool IsPressDown
        {
            get { return SetPressDown != null ? SetPressDown() : base.IsPressDown; }
        }


        protected virtual void PlayClickSound()
        {
            if (!IsPlaySound) return;
            var soundKey = GetAttributeAs<string>("sound");
            if (string.IsNullOrEmpty(soundKey))
            {
                soundKey = DefaultSoundKey;
            }

            if (!string.IsNullOrEmpty(soundKey))
            {
                SoundManager.Instance.PlaySoundByKey(soundKey);
            }
        }
        
        protected override void OnPointerClick(PointerEventData e)
        {
            base.OnPointerClick(e);
                PlayClickSound();
            if (TouchClick != null)
                TouchClick(this);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            TouchClick = null;
        }

        //创建单张图片替换按钮背景图.
        public void SetLayout(UILayout up, UILayout down)
        {
            Layout = up;
            LayoutDown = down;
        }

        //创建单张图片作为按钮文字.
        public void SetImageText(UnityImage up, UnityImage down)
        {
            if (mImageTextUp == null)
            {
                mImageTextUp = new ImageSprite(Name + "ImageTextUp");
                AddChild(mImageTextUp);
            }
            if (mImageTextDown == null)
            {
                mImageTextDown = new ImageSprite(Name + "ImageTextDown");
                AddChild(mImageTextDown);
            }

            mImageTextUp.SetImage(up);
            mImageTextUp.Size2D = new Vector2(up.Width, up.Height);
            mImageTextDown.SetImage(down);
            mImageTextDown.Size2D = new Vector2(down.Width, down.Height);
        }

        //用key从图集创建图片作为按钮文字.
        public void SetAtlasImageText(CPJAtlas atlasUp, string keyUp, CPJAtlas atlasDown, string keyDown)
        {
            var indexUp = atlasUp.GetIndexByKey(keyUp);
            var indexDown = atlasDown.GetIndexByKey(keyDown);
            SetAtlasImageText(atlasUp, indexUp, atlasDown, indexDown);
        }

        //用index从图集创建图片作为按钮文字.
        public void SetAtlasImageText(CPJAtlas atlasUp, int indexUp, CPJAtlas atlasDown, int indexDown)
        {
            if ((indexUp == -1) || (indexDown == -1))
                return;

            if (mImageTextUp == null)
            {
                mImageTextUp = new ImageSprite(Name + "ImageTextUp");
                AddChild(mImageTextUp);
            }
            if (mImageTextDown == null)
            {
                mImageTextDown = new ImageSprite(Name + "ImageTextDown");
                AddChild(mImageTextDown);
            }

            var imageUp = atlasUp.GetTile(indexUp) as UnityImage;
            var imageDown = atlasDown.GetTile(indexDown) as UnityImage;
            var r2dUp = atlasUp.GetClipRect(indexUp);
            var r2dDown = atlasUp.GetClipRect(indexDown);
            var clipUp = new Rect(r2dUp.x, r2dUp.y, r2dUp.width, r2dUp.height);
            var clipDown = new Rect(r2dDown.x, r2dDown.y, r2dDown.width, r2dDown.height);

            mImageTextUp.SetImage(imageUp, clipUp, Vector2.zero);
            mImageTextUp.Size2D = new Vector2(clipUp.width, clipUp.height);
            mImageTextDown.SetImage(imageDown, clipDown, Vector2.zero);
            mImageTextDown.Size2D = new Vector2(clipDown.width, clipDown.height);
        }

        public static HZTextButton CreateTextButton(UETextButtonMeta m)
        {
            if (m == null)
            {
                m = new UETextButtonMeta
                {
                    textFontSize = 18,
                    focusTextColor = 0xffffffff,
                    unfocusTextColor = 0xffffffff,
                    layout_down = new UILayoutMeta(),
                    Layout = new UILayoutMeta(),
                    Visible = true
                };
            }
            var e = UIFactory.Instance as UIEditor;
            var l = (HZTextButton) e.CreateFromMeta(m, meta => new HZTextButton());
            return l;
        }

        public static HZTextButton CreateTextButton()
        {
            return CreateTextButton(null);
        }

        private bool mLastScale;

        protected override void OnPointerDown(PointerEventData e)
        {
            base.OnPointerDown(e);
            //if (LayoutDown == null && mImageTextDown == null && string.IsNullOrEmpty(TextDown))
            if (LayoutDown == null  && string.IsNullOrEmpty(TextDown))
                {
                //默认缩放，刘琦说的
                mLastScale = true;

                ButtonScale(0.9f);
            }
        }

        protected override void OnPointerUp(PointerEventData e)
        {
            base.OnPointerUp(e);
            if (mLastScale)
            {
                mLastScale = false;

                ButtonScale(1.0f);
            }
        }

        private void ButtonScale(float scale)
        {
            float x = this.Position2D.x;
            float y = this.Position2D.y;
            float w = this.Size2D.x;
            float h = this.Size2D.y;
            float offx = (Scale.x - scale) * w * 0.5f;
            float offy = (Scale.y - scale) * h * 0.5f;

            this.Scale = new Vector2(scale, scale);
            this.Position2D = new Vector2(x + offx, y + offy);
        }
    }
}