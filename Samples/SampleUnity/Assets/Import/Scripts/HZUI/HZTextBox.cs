
using System;
using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
using UnityEngine.EventSystems;
using DeepCore.GUI.Display.Text;
using DeepCore.GUI.Sound;
using UnityEngine;

namespace DeepCore.Unity3D.UGUIEditor.UI
{

    public class HZTextBoxHtml : UETextBoxHtml
    {
        public delegate void LinkClickHandler(string link_str);

        /// <summary>
        /// link字符串点击后的回调
        /// </summary>
        public LinkClickHandler LinkClick { get; set; }

        protected override void OnPointerClick(PointerEventData e)
        {
            base.OnPointerClick(e);
            var box = TextComponent as RichTextBox;
            if (box != null && LinkClick != null)
            {
                RichTextClickInfo info;
                var pos = ScreenToLocalPoint2D(e);
                if (box.TestClick(pos, out info) && !string.IsNullOrEmpty(info.mRegion.Attribute.link))
                {
                    string sound_key = GetAttributeAs<string>("sound");
                    if (!IsAttribute("sound") || !string.IsNullOrEmpty(sound_key))
                    {
                        if (!string.IsNullOrEmpty(sound_key))
                        {
                            SoundManager.Instance.PlaySoundByKey(sound_key);
                        }
                    }

                    LinkClick.Invoke(info.mRegion.Attribute.link);
                }
            }
        }



        public HZTextBoxHtml()
        {
            //mTextSprite.Graphics.resizeTextForBestFit = false;
        }

    }

    public class HZTextBox : UETextBox
    {
        public delegate void LinkClickHandler(string link_str);
        private bool mIsCenterShow;
        /// <summary>
        /// link字符串点击后的回调
        /// </summary>
        public LinkClickHandler LinkClick { get; set; }

        public HZTextBox()
        {
            //  mTextSprite.Graphics.resizeTextForBestFit = false;
        }

        public new string XmlText
        {
            set
            {
                try
                {
                    base.XmlText = value;
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(value + " \n" + e.Message);
                    Text = value;
                }
            }
        }

        public void SetCenterShow(bool isCenter = true)
        {
            mIsCenterShow = isCenter;
            if (mIsCenterShow)
            {
                var panel = (this.TextComponent as RichTextBox).Container;
                (this.TextComponent as RichTextBox).Anchor = GUI.Data.TextAnchor.L_C;
            }
        }
        
        protected override void OnPointerClick(PointerEventData e)
        {
            base.OnPointerClick(e);
            var box = TextComponent as RichTextBox;
            if(box != null && LinkClick != null)
            {
                RichTextClickInfo info;
                var pos = ScreenToLocalPoint2D(e);
                if(box.TestClick(pos, out info) && !string.IsNullOrEmpty(info.mRegion.Attribute.link))
                {
                    string sound_key = GetAttributeAs<string>("sound");
                    if (!IsAttribute("sound") || !string.IsNullOrEmpty(sound_key))
                    {
                        if (!string.IsNullOrEmpty(sound_key))
                        {
                            SoundManager.Instance.PlaySoundByKey(sound_key);
                        }
                    }

                    LinkClick.Invoke(info.mRegion.Attribute.link);
                }
            }
        }

        /// <summary>
        /// 把link属性的字符串附加上下划线
        /// </summary>
        /// <param name="xmltext"></param>
        public void DecodeAndUnderlineLink(string xmltext)
        {
            var box = TextComponent as RichTextBox;
            try
            {
                var atext = UIFactory.Instance.DecodeAttributedString(xmltext, box.RichTextLayer.DefaultTextAttribute);
                if (atext != null)
                {
                    TextAttribute textAttr;
                    int index = 0;
                    do
                    {
                        textAttr = atext.GetAttribute(index++);
                        if (textAttr != null && !string.IsNullOrEmpty(textAttr.link))
                        {
                            textAttr.underline = true;
                        }
                    } while (textAttr != null);
                    box.AText = atext;
                }
                else
                {
                    Text = xmltext;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(xmltext + " \n" + e.Message);
				Text = xmltext;
            }

        }

        public static HZTextBox CreateTextBox()
        {
            return CreateTextBox(null);
        }

        public static HZTextBox CreateTextBox(UETextBoxMeta m)
        {
            if (m == null)
            {
                m = new UETextBoxMeta
                {
                    Visible = true
                };
            }
            var e = UIFactory.Instance as UIEditor;
            var l = (HZTextBox)e.CreateFromMeta(m, meta => new HZTextBox());
            return l;
        }
    }
}
