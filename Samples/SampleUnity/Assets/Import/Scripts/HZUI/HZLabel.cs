using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public class HZLabel : UELabel
    {
        private bool _mSupportRichtext;

        public bool SupportRichtext
        {
            get { return _mSupportRichtext; }
            set
            {
                _mSupportRichtext = value;
                if (mTextGraphics != null)
                {
                    var tg = mTextGraphics as TextGraphics;
                    if (tg)
                        tg.supportRichText = value;
                }
                else
                {
                    var ts = mTextSprite as TextSprite;
                    if (ts != null)
                        ts.Graphics.supportRichText = value;
                }
            }
        }

        public uint FontColorRGBA
        {
            get { return UIUtils.Color_To_UInt32_RGBA(FontColor); }
            set { FontColor = UIUtils.UInt32_RGBA_To_Color(value); }
        }

        public uint FontColorRGB
        {
            get
            {
                var ret = UIUtils.Color_To_UInt32_RGBA(FontColor);
                return ret >> 8;
            }
            set
            {
                value = (value << 8) | 0x000000ff;
                FontColor = UIUtils.UInt32_RGBA_To_Color(value);
            }
        }

        public static HZLabel CreateLabel(UETextComponentMeta m)
        {
            if (m == null)
            {
                m = new UELabelMeta
                {
                    textFontSize = 18,
                    textColor = 0xffffffff,
                    Visible = true
                };
            }

            var e = UIFactory.Instance as UIEditor;
            var l = (HZLabel) e.CreateFromMeta(m, meta => new HZLabel());
            //l.DecodeFromXML(e, m);
            return l;
        }

        public static HZLabel CreateLabel()
        {
            return CreateLabel(null);
        }
    }
}