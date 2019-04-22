using UnityEngine;
using Assets.ParadoxNotion.SLATE_Cinematic_Sequencer.TLExtend;
using UnityEngine.UI;

namespace Slate.ActionClips
{

    [Category("Rendering")]
    [Description("Show text on screen")]
    public class TLOverlayText : OverlayText
    {

        public string ID = string.Empty;

        public override float blendIn
        {
            get
            {
                return base.blendIn;
            }

            set
            {
                base.blendIn = value;
            }
        }
        public override float blendOut
        {
            get
            {
                return base.blendOut;
            }

            set
            {
                base.blendOut = value;
            }
        }
        private GUIStyle mGUIStyle;
        private string overlayText;
        private Color overlayTextColor;
        private float overlayTextSize;
        private TextAnchor overlayTextAnchor;
        private Vector2 overlayTextPos;
        private System.Object mCutstomUIObject = null;
        //创建个自定义对象
        public delegate System.Object CreateOverTextHandle();
        //绘制个自定义对象
        public delegate void UpdateOverTextHandle(System.Object _object, string text, string ID, Color color, float size, TextAnchor alignment, Vector2 position);
        //销毁个自定义对象
        public delegate void DestoryOverTextHandle(System.Object _object);
        public static CreateOverTextHandle OnCreateObject = null;
        public static UpdateOverTextHandle OnUpdateOverText = null;
        public static DestoryOverTextHandle OnDestoryOverText = null;
        private bool bIsShow = false;
        protected override bool OnInitialize()
        {
            if (OnCreateObject != null)
            {
                mCutstomUIObject = OnCreateObject();
                return base.OnInitialize();
            }
            if (mGUIStyle == null)
            {
                mGUIStyle = new GUIStyle();
                mGUIStyle.normal.textColor = Color.white;
                mGUIStyle.richText = true;
            }
            mGUIStyle.font = DirectorGUI.current.overlayTextFont;
            //Debug.Log("TLOverlayTextOnInitialize ");
            return base.OnInitialize();
        }

        protected override void OnEnter()
        {
            bIsShow = true;
            if (TLLanguageManager.OnGetContent != null)
            {
                string _text = TLLanguageManager.OnGetContent(text, ID);
                if (!string.IsNullOrEmpty(_text))
                {
                    this.text = _text;
                }
                else
                {
                    Debug.LogError("not context in LanguageLib with id " + text);
                }
            }
            //Debug.Log("TLOverlayTextOnEnter " + text);
            if (mCutstomUIObject == null)
            {
                DirectorGUI.OnGUIUpdate -= DirectorGUI_OnGUIUpdate;
                DirectorGUI.OnGUIUpdate += DirectorGUI_OnGUIUpdate;
            }
            base.OnEnter();
        }
        protected override void OnReverse()
        {
            //Debug.Log("TLOverlayTextOnReverse " );
            bIsShow = false;
            if (OnUpdateOverText != null)
            {
                DirectorGUI.OnGUIUpdate -= DirectorGUI_OnGUIUpdate;
            }
            base.OnReverse();
        }
        protected override void OnReverseEnter()
        {
            //Debug.Log("TLOverlayTextOnReverseEnter ");
            base.OnReverseEnter();
        }
        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);
        }
        protected override void OnAfterValidate()
        {
            base.OnAfterValidate();
        }
        protected override void OnExit()
        {
            //Debug.Log("TLOverlayTextOnExit ");
            bIsShow = false;

            if (OnDestoryOverText != null)
            {
                OnDestoryOverText(mCutstomUIObject);
                mCutstomUIObject = null;
            }
            else
            {
                DirectorGUI.OnGUIUpdate -= DirectorGUI_OnGUIUpdate;
            }
            base.OnExit();
        }

        private void DirectorGUI_OnGUIUpdate()
        {
            if (!string.IsNullOrEmpty(overlayText))
            {
                OnGUI();
            }

        }

        void OnGUI()
        {
            if (!bIsShow)
            {
                return;
            }
            mGUIStyle.alignment = overlayTextAnchor;
            var rect = Rect.MinMaxRect(20, 10, Screen.width - 20, Screen.height - 10);
            overlayTextPos.y *= -1;
            rect.center += overlayTextPos;
            var finalText = string.Format("<size={0}><b>{1}</b></size>", overlayTextSize, overlayText);
            //shadow
            GUI.color = new Color(0, 0, 0, overlayTextColor.a);
            GUI.Label(rect, finalText, mGUIStyle);
            rect.center += new Vector2(2, -2);
            //text
            GUI.color = overlayTextColor;
            GUI.Label(rect, finalText, mGUIStyle);
            GUI.color = Color.white;
        }
        protected override void OnUpdate(float time)
        {
            var lerpColor = color;
            lerpColor.a = Easing.Ease(interpolation, 0, color.a, GetClipWeight(time));
            if (OnUpdateOverText != null)
            {
                OnUpdateOverText(mCutstomUIObject, text, ID, lerpColor, size, anchor, position);
            }
            else
            {
                overlayText = text;
                overlayTextColor = lerpColor;
                overlayTextSize = size;
                overlayTextAnchor = anchor;
                overlayTextPos = position;
            }

        }


    }
}
