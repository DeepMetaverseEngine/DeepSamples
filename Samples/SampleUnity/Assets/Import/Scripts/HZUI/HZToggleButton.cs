
using DeepCore.GUI.Data;
using UnityEngine.EventSystems;
using DeepCore.GUI.Sound;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUI;
namespace DeepCore.Unity3D.UGUIEditor.UI
{
	public class HZToggleButton : HZTextButton
	{
        protected bool m_IsChecked = false;

        public override bool IsPressDown { get { return m_IsChecked; } }
        public virtual bool IsChecked
        {
            get { return m_IsChecked; }
            set
            {
                m_IsChecked = value;
                if (Selected != null)
                {
                    Selected(this);
                }
            }
        }

        public TouchClickHandle Selected { get; set; }


        protected override void DecodeFields(UIEditor.Decoder editor, UIComponentMeta e)
        {
            base.DecodeFields(editor, e);
            this.m_IsChecked = (e as UEToggleButtonMeta).isChecked;
        }


        public enum LockState
        {
            eNone = 0,
            eLockSelect = 1,
            eLockUnSelect = 2

        }

        private LockState mCurLockState;

        protected override void OnPointerClick(PointerEventData e)
        {
            if (!CheckLockState())
            {
                this.IsChecked = !m_IsChecked;
                base.OnPointerClick(e);
            }
        }

	    public static string DefaultCheckedSoundKey;
	    public static string DefaultUnCheckedSoundKey;
        
        protected override void PlayClickSound()
	    {
            string soundKey;
	        if (IsChecked)
	        {
	            soundKey = GetAttributeAs<string>("checkedSound");
	            if (string.IsNullOrEmpty(soundKey))
	            {
	                soundKey = GetAttributeAs<string>("sound");
	            }
	        }
	        else
	        {
	            soundKey = GetAttributeAs<string>("sound");
	        }

	        if (string.IsNullOrEmpty(soundKey))
	        {
	            if (IsChecked)
	            {
	                soundKey = DefaultCheckedSoundKey;
	            }
	            else
	            {
	                soundKey = DefaultUnCheckedSoundKey;
	            }
	        }
	        if (!string.IsNullOrEmpty(soundKey))
	        {
	            SoundManager.Instance.PlaySoundByKey(soundKey);
	        }
	    }

        protected override void OnDispose()
        {
            base.OnDispose();
            Selected = null;
        }

        public void SetBtnLockState(LockState state)
        {
            mCurLockState = state;
        }

        private bool CheckLockState()
        {
            if (mCurLockState == LockState.eNone) { return false; }
            if (mCurLockState == LockState.eLockSelect && m_IsChecked == true) { return true; }
            if (mCurLockState == LockState.eLockUnSelect && m_IsChecked == false) { return true; }
            return false;
        }

		public HZToggleButton()
        {

        }

        public static HZToggleButton CreateToggleButton(UETextButtonMeta m)
        {
            if (m == null)
            {
                m = new UETextButtonMeta();
                m.textFontSize = 18;
                m.focusTextColor = 0xffffffff;
                m.unfocusTextColor = 0xffffffff;
                m.layout_down = new UILayoutMeta();
                m.Layout = new UILayoutMeta();
                m.Visible = true;
            }
            UIEditor e = (UIFactory.Instance as UIEditor);
            HZToggleButton l = (HZToggleButton)e.CreateFromMeta(m, (meta) =>
            {
                return new HZToggleButton();
            });
            return l;
        }

        public static HZToggleButton CreateToggleButton()
        {
            return CreateToggleButton(null);
        }
    }
}
